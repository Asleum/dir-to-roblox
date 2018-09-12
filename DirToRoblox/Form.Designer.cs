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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.synchronizationButton = new System.Windows.Forms.Button();
            this.watcher = new System.IO.FileSystemWatcher();
            this.sendManualUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBox = new StatusBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.watcher)).BeginInit();
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
            this.toolStripSeparator1,
            this.openToolStripMenuItem,
            this.recentToolStripMenuItem,
            this.toolStripSeparator2,
            this.openProjectInExplorerToolStripMenuItem,
            this.sendManualUpdateToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // recentToolStripMenuItem
            // 
            this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
            this.recentToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.recentToolStripMenuItem.Text = "Recent";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openProjectInExplorerToolStripMenuItem
            // 
            this.openProjectInExplorerToolStripMenuItem.Name = "openProjectInExplorerToolStripMenuItem";
            this.openProjectInExplorerToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openProjectInExplorerToolStripMenuItem.Text = "Open project in explorer";
            this.openProjectInExplorerToolStripMenuItem.Click += new System.EventHandler(this.openProjectInExplorerToolStripMenuItem_Click);
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
            // watcher
            // 
            this.watcher.EnableRaisingEvents = true;
            this.watcher.IncludeSubdirectories = true;
            this.watcher.SynchronizingObject = this;
            // 
            // sendManualUpdateToolStripMenuItem
            // 
            this.sendManualUpdateToolStripMenuItem.Name = "sendManualUpdateToolStripMenuItem";
            this.sendManualUpdateToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.sendManualUpdateToolStripMenuItem.Text = "Send manual update";
            // 
            // statusBox
            // 
            this.statusBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.statusBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.statusBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.statusBox.Location = new System.Drawing.Point(112, 10);
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(293, 48);
            this.statusBox.TabIndex = 1;
            this.statusBox.Text = "";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 92);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "DirToRoblox";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.watcher)).EndInit();
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
        private StatusBox statusBox;
        private System.IO.FileSystemWatcher watcher;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openProjectInExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendManualUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

