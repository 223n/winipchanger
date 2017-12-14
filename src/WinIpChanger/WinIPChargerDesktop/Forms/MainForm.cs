using System;
using System.Collections.Generic;
using System.IO;
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
                    child.Dispose();
                    child = null;
                }
                // Add List
                foreach (var detail in setting.Details)
                {
                    var child = new ToolStripMenuItem() { Text = string.Format("{0} [IP: {1}]", detail.Name, detail.IPAddress), Tag = detail };
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
            foreach (var adapter in Network.NetworkAdapterUtility.NetworkAdaptersForIPEnabled)
            {
                var detail = new Common.WinIPChangerSettingDetail()
                {
                    No = setting.Details.Count + 1,
                    Name = adapter.Name,
                    IsDhcpEnabled = adapter.IsDhcpEnabled,
                    IPAddress = adapter.IPAddress?.ToString(),
                    SubnetMask = adapter.SubnetMask?.ToString(),
                    DefaultGateway = adapter.DefaultGateway?.ToString(),
                    DnsServers = new List<Common.IPAddressValue>()
                };
                foreach (var dns in adapter.DnsServers)
                    detail.DnsServers.Add(new Common.IPAddressValue() { IPAddress = dns.ToString() });
                setting.Details.Add(detail);
            }
            Common.YamlHelper.Serialize(SettingFilePath, setting);
        }

        #endregion

    }
}
