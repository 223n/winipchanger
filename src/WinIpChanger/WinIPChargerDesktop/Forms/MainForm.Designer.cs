namespace WinIPChanger.Desktop.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.MainContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateConnectionInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingComboBox = new System.Windows.Forms.ComboBox();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.loadInfoFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainNotifyIcon
            // 
            this.MainNotifyIcon.ContextMenuStrip = this.MainContextMenuStrip;
            resources.ApplyResources(this.MainNotifyIcon, "MainNotifyIcon");
            // 
            // MainContextMenuStrip
            // 
            this.MainContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateConnectionInfoToolStripMenuItem,
            this.loadInfoFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExitToolStripMenuItem});
            this.MainContextMenuStrip.Name = "MainContextMenuStrip";
            resources.ApplyResources(this.MainContextMenuStrip, "MainContextMenuStrip");
            // 
            // updateConnectionInfoToolStripMenuItem
            // 
            resources.ApplyResources(this.updateConnectionInfoToolStripMenuItem, "updateConnectionInfoToolStripMenuItem");
            this.updateConnectionInfoToolStripMenuItem.Name = "updateConnectionInfoToolStripMenuItem";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            resources.ApplyResources(this.ExitToolStripMenuItem, "ExitToolStripMenuItem");
            // 
            // SettingComboBox
            // 
            resources.ApplyResources(this.SettingComboBox, "SettingComboBox");
            this.SettingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SettingComboBox.FormattingEnabled = true;
            this.SettingComboBox.Name = "SettingComboBox";
            // 
            // UpdateButton
            // 
            resources.ApplyResources(this.UpdateButton, "UpdateButton");
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.UseVisualStyleBackColor = true;
            // 
            // loadInfoFileToolStripMenuItem
            // 
            this.loadInfoFileToolStripMenuItem.Name = "loadInfoFileToolStripMenuItem";
            resources.ApplyResources(this.loadInfoFileToolStripMenuItem, "loadInfoFileToolStripMenuItem");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.SettingComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.MainContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon MainNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip MainContextMenuStrip;
        private System.Windows.Forms.ComboBox SettingComboBox;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateConnectionInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem loadInfoFileToolStripMenuItem;
    }
}