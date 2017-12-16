using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace WinIPChanger.Desktop.Forms
{
    /// <summary>
    /// Main Form
    /// </summary>
    public partial class MainForm : Form
    {

        #region Property

        /// <summary>
        /// Setting File Name
        /// </summary>
        private readonly string SettingFileName = "WinIPChanger.yml";

        /// <summary>
        /// Setting File Path
        /// </summary>
        private string SettingFilePath { get { return Path.Combine(Application.StartupPath, SettingFileName); } }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeControl();
            InitializeEventHandler();
        }

        /// <summary>
        /// Initialize Control
        /// </summary>
        private void InitializeControl()
        {
            ShowInTaskbar = false;
            Text = nameof(WinIPChanger);
            MainNotifyIcon.Text = nameof(WinIPChanger);
        }

        /// <summary>
        /// Initialize Event Handler
        /// </summary>
        private void InitializeEventHandler()
        {
            FormClosing += (sender, e) => { MainNotifyIcon.Visible = false; MainNotifyIcon.Icon = null; };
            loadInfoFileToolStripMenuItem.Click += (sender, e) => { LoadInfoFile(); };
            ExitToolStripMenuItem.Click += (sender, e) => { MainNotifyIcon.Visible = false; MainNotifyIcon.Icon = null; Application.Exit(); };
        }

        #endregion

        #region Event

        /// <summary>
        /// Click Update Setting Item
        /// </summary>
        /// <param name="sender">ToolStripMenuItem</param>
        /// <param name="args">Empty EventArgs</param>
        private void Click_UpdateToolStripMenuItem(object sender, EventArgs args)
        {
            var item = sender as ToolStripMenuItem;
            var setting = item.Tag as Common.WinIPChangerSettingDetail;
            var adapter = new Network.NetworkAdapter() {
                Name = setting.NetworkAdapterName,
                IsDhcpEnabled = setting.IsDhcpEnabled,
                IPAddress = (string.IsNullOrWhiteSpace(setting.IPAddress) ? null : IPAddress.Parse(setting.IPAddress)),
                SubnetMask = (string.IsNullOrWhiteSpace(setting.SubnetMask) ? null : IPAddress.Parse(setting.SubnetMask)),
                DefaultGateway = (string.IsNullOrWhiteSpace(setting.DefaultGateway) ? null : IPAddress.Parse(setting.DefaultGateway))
            };
            foreach (var dns in setting.DnsServers)
                adapter.DnsServers.Add(IPAddress.Parse(dns));
            var result = Network.NetworkAdapterUtility.SetAdapterConfig(adapter, false);
            switch (result)
            {
                case Network.Results.Success:
                    MainNotifyIcon.ShowBalloonTip(10, "Success!", "Update Network Info.", ToolTipIcon.Info);
                    break;
                case Network.Results.SuccessRebootRequired:
                    MainNotifyIcon.ShowBalloonTip(10, "Success!", "Update Network Info. But, Reboot Required!", ToolTipIcon.Warning);
                    break;
                case Network.Results.ApiFailedError:
                    MainNotifyIcon.ShowBalloonTip(10, "Error!", string.Format("API Error! Error Code: {0}.", Network.NetworkAdapterUtility.LastApiErrorCode), ToolTipIcon.Error);
                    break;
                case Network.Results.TargetIsNotEnableIPError:
                    MainNotifyIcon.ShowBalloonTip(10, "Error!", "Target Network Adapter is not enable IP.", ToolTipIcon.Error);
                    break;
                case Network.Results.TargetNotFoundError:
                    MainNotifyIcon.ShowBalloonTip(10, "Error!", "Target Network Adapter is not found.", ToolTipIcon.Error);
                    break;
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// Load Info File
        /// </summary>
        public void LoadInfoFile()
        {
            try
            {
                if (!ExistsInfoFile()) CreateInfoFile();
                var setting = Common.YamlHelper.Deserialize<Common.WinIPChangerSetting>(SettingFilePath);
                // Clear List
                while (updateConnectionInfoToolStripMenuItem.HasDropDownItems)
                {
                    var child = updateConnectionInfoToolStripMenuItem.DropDownItems[0];
                    updateConnectionInfoToolStripMenuItem.DropDownItems.RemoveAt(0);
                    child.Click -= Click_UpdateToolStripMenuItem;
                    child.Dispose();
                    child = null;
                }
                // Add List
                foreach (var detail in setting.Details.OrderBy(d => d.No))
                {
                    var child = new ToolStripMenuItem() { Text = string.Format("{0} ( {1} )", detail.SettingName, detail.IPAddress), Tag = detail };
                    child.Click += Click_UpdateToolStripMenuItem;
                    updateConnectionInfoToolStripMenuItem.DropDownItems.Add(child);
                }
                updateConnectionInfoToolStripMenuItem.Enabled = updateConnectionInfoToolStripMenuItem.HasDropDown;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exists Setting Info File
        /// </summary>
        /// <returns>true = exists / false = not exists</returns>
        private bool ExistsInfoFile() => (File.Exists(SettingFilePath));

        /// <summary>
        /// Craete Setting Info File
        /// </summary>
        private void CreateInfoFile()
        {
            var setting = new Common.WinIPChangerSetting() { Details = new List<Common.WinIPChangerSettingDetail>() };
            foreach (var adapter in Network.NetworkAdapterUtility.GetNetworkAdaptersForIPEnabled())
            {
                var detail = new Common.WinIPChangerSettingDetail()
                {
                    No = setting.Details.Count + 1,
                    SettingName = adapter?.Name,
                    NetworkAdapterName = adapter?.Name,
                    IsDhcpEnabled = adapter.IsDhcpEnabled,
                    IPAddress = adapter.IPAddress?.ToString(),
                    SubnetMask = adapter.SubnetMask?.ToString(),
                    DefaultGateway = adapter.DefaultGateway?.ToString()
                };
                foreach (var dns in adapter.DnsServers)
                    detail.DnsServers.Add(dns.ToString());
                setting.Details.Add(detail);
            }
            Common.YamlHelper.Serialize(SettingFilePath, setting);
        }

        #endregion

    }
}
