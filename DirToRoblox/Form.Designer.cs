namespace DirToRoblox
{
    partial class Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentProjectsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.clearRecentProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openProjectInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendManualUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusBox = new System.Windows.Forms.Label();
            this.synchronizationButton = new System.Windows.Forms.Button();
            this.filesWatcher = new System.IO.FileSystemWatcher();
            this.directoriesWatcher = new System.IO.FileSystemWatcher();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toggleSynchronizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.directoriesWatcher)).BeginInit();
            this.trayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.preferencesToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(415, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "Menu";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.recentToolStripMenuItem,
            this.toolStripSeparator2,
            this.openProjectInExplorerToolStripMenuItem,
            this.sendManualUpdateToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // recentToolStripMenuItem
            // 
            this.recentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recentProjectsSeparator,
            this.clearRecentProjectsToolStripMenuItem});
            this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
            this.recentToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.recentToolStripMenuItem.Text = "Recent";
            // 
            // recentProjectsSeparator
            // 
            this.recentProjectsSeparator.Name = "recentProjectsSeparator";
            this.recentProjectsSeparator.Size = new System.Drawing.Size(197, 6);
            // 
            // clearRecentProjectsToolStripMenuItem
            // 
            this.clearRecentProjectsToolStripMenuItem.Name = "clearRecentProjectsToolStripMenuItem";
            this.clearRecentProjectsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.clearRecentProjectsToolStripMenuItem.Text = "Clear recent projects list";
            this.clearRecentProjectsToolStripMenuItem.Click += new System.EventHandler(this.clearRecentProjectsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // openProjectInExplorerToolStripMenuItem
            // 
            this.openProjectInExplorerToolStripMenuItem.Name = "openProjectInExplorerToolStripMenuItem";
            this.openProjectInExplorerToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openProjectInExplorerToolStripMenuItem.Text = "Open project in explorer";
            this.openProjectInExplorerToolStripMenuItem.Click += new System.EventHandler(this.openProjectInExplorerToolStripMenuItem_Click);
            // 
            // sendManualUpdateToolStripMenuItem
            // 
            this.sendManualUpdateToolStripMenuItem.Name = "sendManualUpdateToolStripMenuItem";
            this.sendManualUpdateToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.sendManualUpdateToolStripMenuItem.Text = "Send manual update";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // browserDialog
            // 
            this.browserDialog.Description = "Select the root directory to synchronize with Studio";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.statusBox);
            this.panel1.Controls.Add(this.synchronizationButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(415, 68);
            this.panel1.TabIndex = 4;
            // 
            // statusBox
            // 
            this.statusBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.statusBox.Location = new System.Drawing.Point(112, 10);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(293, 48);
            this.statusBox.TabIndex = 1;
            // 
            // synchronizationButton
            // 
            this.synchronizationButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.synchronizationButton.Location = new System.Drawing.Point(10, 10);
            this.synchronizationButton.Name = "synchronizationButton";
            this.synchronizationButton.Size = new System.Drawing.Size(96, 48);
            this.synchronizationButton.TabIndex = 0;
            this.synchronizationButton.Text = "Begin synchronization";
            this.synchronizationButton.UseVisualStyleBackColor = true;
            this.synchronizationButton.Click += new System.EventHandler(this.synchronizationButton_Click);
            // 
            // filesWatcher
            // 
            this.filesWatcher.EnableRaisingEvents = true;
            this.filesWatcher.IncludeSubdirectories = true;
            this.filesWatcher.NotifyFilter = ((System.IO.NotifyFilters)((System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.LastWrite)));
            this.filesWatcher.SynchronizingObject = this;
            // 
            // directoriesWatcher
            // 
            this.directoriesWatcher.EnableRaisingEvents = true;
            this.directoriesWatcher.IncludeSubdirectories = true;
            this.directoriesWatcher.NotifyFilter = System.IO.NotifyFilters.DirectoryName;
            this.directoriesWatcher.SynchronizingObject = this;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Activate the DirToRoblox studio plugin to proceed";
            this.notifyIcon.BalloonTipTitle = "Synchronization initiated";
            this.notifyIcon.ContextMenuStrip = this.trayContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "DirToRoblox";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // trayContextMenu
            // 
            this.trayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleSynchronizationToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.trayContextMenu.Name = "trayContextMenu";
            this.trayContextMenu.Size = new System.Drawing.Size(192, 70);
            // 
            // toggleSynchronizationToolStripMenuItem
            // 
            this.toggleSynchronizationToolStripMenuItem.Name = "toggleSynchronizationToolStripMenuItem";
            this.toggleSynchronizationToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.toggleSynchronizationToolStripMenuItem.Text = "Begin synchronization";
            this.toggleSynchronizationToolStripMenuItem.Click += new System.EventHandler(this.toggleSynchronizationToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 92);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "DirToRoblox";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.filesWatcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.directoriesWatcher)).EndInit();
            this.trayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog browserDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button synchronizationButton;
        private System.IO.FileSystemWatcher filesWatcher;
        private System.Windows.Forms.ToolStripMenuItem openProjectInExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendManualUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.IO.FileSystemWatcher directoriesWatcher;
        private System.Windows.Forms.Label statusBox;
        private System.Windows.Forms.ToolStripSeparator recentProjectsSeparator;
        private System.Windows.Forms.ToolStripMenuItem clearRecentProjectsToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toggleSynchronizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}

