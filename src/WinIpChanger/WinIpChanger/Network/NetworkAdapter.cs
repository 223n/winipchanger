using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Net;

namespace WinIPChanger.Network
{
    /// <summary>
    /// ネットワークアダプタ
    /// </summary>
    public class NetworkAdapter
    {

        #region Constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NetworkAdapter() { return; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="adapter">Win32_NetworkAdapter</param>
        /// <param name="config">Win32_NetworkAdapterConfiguration</param>
        public NetworkAdapter(ManagementObject adapter, ManagementObject config) : this()
        {
            if (adapter == null) throw new ArgumentNullException("adapter");
            if (config == null) throw new ArgumentNullException("config");
            Name = (string)adapter["NetConnectionID"];
            IsDhcpEnabled = (bool)config["DHCPEnabled"];
            string tmpIPAddress = ((string[])config["IPAddress"]).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(tmpIPAddress)) IPAddress = IPAddress.Parse(tmpIPAddress);
            string tmpSubnetMask = ((string[])config["IPSubnet"]).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(tmpSubnetMask)) SubnetMask = IPAddress.Parse(tmpSubnetMask);
            string tmpDefaultGateway = config["DefaultIPGateway"] == null ? null : ((string[])config["DefaultIPGateway"]).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(tmpDefaultGateway) && tmpDefaultGateway != "0.0.0.0" && tmpDefaultGateway != tmpIPAddress) DefaultGateway = IPAddress.Parse(tmpDefaultGateway);
            string[] tmpDnsServers = config["DNSServerSearchOrder"] == null ? null : ((string[])config["DNSServerSearchOrder"]);
            if (tmpDnsServers != null && 0 < tmpDnsServers.Length)
                foreach (var dns in tmpDnsServers.Select(dns => IPAddress.Parse(dns)))
                    DnsServers.Add(dns);
        }

        #endregion

        #region Property

        /// <summary>
        /// ネットワーク接続名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DHCPサーバを使用するかどうか
        /// </summary>
        /// <value>true = DHCPを使用する / false = 手動設定を使用する</value>
        public bool IsDhcpEnabled { get; set; }

        /// <summary>
        /// IPアドレス
        /// </summary>
        public IPAddress IPAddress { get; set; }

        /// <summary>
        /// サブネットマスク
        /// </summary>
        public IPAddress SubnetMask { get; set; }

        /// <summary>
        /// デフォルトゲートウェイ
        /// </summary>
        public IPAddress DefaultGateway { get; set; }

        /// <summary>
        /// DNSサーバの一覧
        /// </summary>
        public Collection<IPAddress> DnsServers { get; } = new Collection<IPAddress>();

        #endregion

        #region Method

        /// <summary>
        /// 現在のオブジェクトを表す文字列を返します。
        /// </summary>
        /// <returns>ネットワーク接続名</returns>
        public override string ToString() => Name;

        /// <summary>
        /// 文字列配列型のIPアドレス
        /// </summary>
        public string[] GetIPAddressStringArray() => IPAddress == null ? new string[] { } : new string[] { IPAddress.ToString() };

        /// <summary>
        /// 文字列配列型のサブネットマスクを取得します。
        /// </summary>
        public string[] GetSubnetMaskStringArray() => SubnetMask == null ? new string[] { "255.255.255.0" } : new string[] { SubnetMask.ToString() };

        /// <summary>
        /// 文字列配列型のデフォルトゲートウェイを取得します。
        /// </summary>
        public string[] GetDefaultGatewayStringArray() => DefaultGateway == null || DefaultGateway.ToString() == "0.0.0.0" || DefaultGateway == IPAddress ? null : new string[] { DefaultGateway.ToString() };

        /// <summary>
        /// 文字列配列型のDNSサーバの一覧を取得します。
        /// </summary>
        public string[] GetDnsServersStringArray() => DnsServers == null ? new string[] { } : DnsServers.Select(dns => dns.ToString()).ToArray();

    #endregion

}
}
