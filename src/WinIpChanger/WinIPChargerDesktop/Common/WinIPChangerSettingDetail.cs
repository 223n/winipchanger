using System.Collections.Generic;

namespace WinIPChanger.Desktop.Common
{
    /// <summary>
    /// Setting Detail Info for WinIPChangerDesktop
    /// </summary>
    public class WinIPChangerSettingDetail
    {

        /// <summary>
        /// Sort No
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// Setting Name
        /// </summary>
        public string SettingName { get; set; }

        /// <summary>
        /// Network Adapter Name
        /// </summary>
        public string NetworkAdapterName { get; set; }

        /// <summary>
        /// DHCP Enabled
        /// </summary>
        public bool IsDhcpEnabled { get; set; }

        /// <summary>
        /// IP Address
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Subnet Mask
        /// </summary>
        public string SubnetMask { get; set; }

        /// <summary>
        /// Default Gateway
        /// </summary>
        public string DefaultGateway { get; set; }

        /// <summary>
        /// DNS Servers
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<string> DnsServers { get; set; } = new List<string>();

    }
}
