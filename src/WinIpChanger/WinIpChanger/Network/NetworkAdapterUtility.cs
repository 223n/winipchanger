using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Management;

namespace WinIPChanger.Network
{
    /// <summary>
    /// ネットワーク接続アダプタ設定ユーティリティ
    /// </summary>
    public static class NetworkAdapterUtility
    {

        /// <summary>
        /// WMI APIが最後に返却したエラーコード
        /// </summary>
        [CLSCompliantAttribute(false)]
        public static uint LastApiErrorCode { get; set; } = 0;

        /// <summary>
        /// すべてのIPが有効なネットワーク接続情報を取得します。
        /// </summary>
        /// <returns>IPが有効なネットワーク接続情報の一覧</returns>
        public static Collection<NetworkAdapter> GetNetworkAdaptersForIPEnabled()
        {
            var results = new Collection<NetworkAdapter>();
            string getNetworkAdapterSql = "select * from Win32_NetworkAdapter";
            using (var adapterSearcher = new ManagementObjectSearcher(getNetworkAdapterSql))
                foreach (var adapter in adapterSearcher.Get().OfType<ManagementObject>())
                {
                    string getNetworkAdapterConfigSql = string.Format(CultureInfo.CurrentCulture, "select * from Win32_NetworkAdapterConfiguration where SettingID = '{0}'", adapter["GUID"]);
                    using (var configSercher = new ManagementObjectSearcher(getNetworkAdapterConfigSql))
                        foreach (var item in configSercher.Get().OfType<ManagementObject>().Where(conf => (bool)conf["IPEnabled"]).Select(conf => new NetworkAdapter(adapter, conf)))
                            results.Add(item);
                }
            return results;
        }

        /// <summary>
        /// ネットワークアダプタの設定を反映します。
        /// </summary>
        /// <param name="value">ネットワークアダプタの設定</param>
        /// <returns>設定結果</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static Results SetAdapterConfig(NetworkAdapter value)
        {
            if (value == null) throw new ArgumentNullException("value");
            uint apiResult = 0;
            bool isFound = false;
            string getNetworkAdapterSql = string.Format(CultureInfo.CurrentCulture, "select * from Win32_NetworkAdapter where NetConnectionID = '{0}'", value.Name);
            using (var adapterSearcher = new ManagementObjectSearcher(getNetworkAdapterSql))
                foreach (var adapter in adapterSearcher.Get().OfType<ManagementObject>())
                {
                    string getNetworkAdapterConfigSql = string.Format(CultureInfo.CurrentCulture, "select * from Win32_NetworkAdapterConfiguration where SettingID = '{0}'", adapter["GUID"]);
                    using (var configSearcher = new ManagementObjectSearcher(getNetworkAdapterConfigSql))
                        foreach (var config in configSearcher.Get().OfType<ManagementObject>())
                        {
                            isFound = true;
                            if (!(bool)config["IPEnabled"]) return Results.TargetIsNotEnableIPError;
                            // Enable DHCP
                            apiResult |= (uint)config.InvokeMethod("EnableDHCP", null);
                            if (apiResult != 0 && apiResult != 1)
                            {
                                LastApiErrorCode = apiResult;
                                return Results.ApiFailedError;
                            }
                            // Static
                            if (!value.IsDhcpEnabled)
                            {
                                apiResult |= (uint)config.InvokeMethod("EnableStatic", new object[] { value.GetIPAddressStringArray(), value.GetSubnetMaskStringArray() });
                                if (apiResult != 0 && apiResult != 1)
                                {
                                    LastApiErrorCode = apiResult;
                                    return Results.ApiFailedError;
                                }
                                var gateway = value.GetDefaultGatewayStringArray();
                                if (gateway != null)
                                {
                                    apiResult |= (uint)config.InvokeMethod("SetGateways", new object[] { gateway });
                                    if (apiResult != 0 && apiResult != 1)
                                    {
                                        LastApiErrorCode = apiResult;
                                        return Results.ApiFailedError;
                                    }
                                }
                            }
                            // DNS
                            apiResult |= (uint)config.InvokeMethod("SetDNSServerSearchOrder", new object[] { value.GetDnsServersStringArray() });
                            if (apiResult != 0 && apiResult != 1)
                            {
                                LastApiErrorCode = apiResult;
                                return Results.ApiFailedError;
                            }
                        }
                }
            if (!isFound) return Results.TargetNotFoundError;
            return apiResult == 1 ? Results.SuccessRebootRequired : Results.Success;
        }

    }
}
