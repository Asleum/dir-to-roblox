using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DirToRoblox
{
    public partial class Form : System.Windows.Forms.Form
    {
        private bool toggling = false;
        private Properties.Settings settings = Properties.Settings.Default;
        private Synchronizer synchronizer;

        public Form()
        {
            InitializeComponent();
            synchronizer = new Synchronizer(filesWatcher, directoriesWatcher, this);
            UpdateVisuals();
            UpdateRecentProjectsList();
            ApplySettings();

            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        /// <summary>
        /// Update the list of recently opened projects in the top menu bar
        /// </summary>
        private void UpdateRecentProjectsList()
        {
            if (settings.RecentPaths.Count == 0)
            {
                recentToolStripMenuItem.Enabled = false;
            }
            else
            {
                recentToolStripMenuItem.DropDownItems.Clear();
                recentToolStripMenuItem.DropDownItems.Insert(0, clearRecentProjectsToolStripMenuItem);
                recentToolStripMenuItem.DropDownItems.Insert(0, recentProjectsSeparator);
                foreach (string path in settings.RecentPaths)
                {
                    ToolStripMenuItem button = new ToolStripMenuItem(path);
                    button.Click += RecentProjectButton_Click;
                    recentToolStripMenuItem.DropDownItems.Insert(0, button);
                }
                recentToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// Begin synchronizing if idle, and stop synchronizing if active
        /// </summary>
        private void ToggleSynchronization()
        {
            if (toggling)
                return;
            toggling = true;

            if (synchronizer.IsSynchronizing())
                synchronizer.StopSynchronization();
            else
            {
                synchronizer.BeginSynchronization();
                if (settings.ShowNotification)
                    notifyIcon.ShowBalloonTip(5000);
                if (settings.AutoMinimize)
                    this.WindowState = FormWindowState.Minimized;
            }

            UpdateVisuals();
            toggling = false;
        }

        /// <summary>
        /// Update the interface to reflect thew current state of the program
        /// </summary>
        public void UpdateVisuals()
        {
            var path = synchronizer.GetPath();
            var exists = Directory.Exists(path);
            synchronizationButton.Enabled = exists;
            if (synchronizer.IsSynchronizing())
                synchronizationButton.Text = "Stop synchronization";
            else
                synchronizationButton.Text = "Begin synchronization";

            if (path == "" || path == null)
                statusBox.Text = "Select a directory to synchronize using the \"Project\" menu button";
            else if (!exists)
                statusBox.Text = "The selected path does not exist anymore:\n" + path;
            else if (!synchronizer.IsSynchronizing())
                statusBox.Text = "Ready to synchronize:\n" + path;
            else if (!synchronizer.GotOneRequest())
                statusBox.Text = "Activate DirToRoblox plugin inside Roblox Studio to begin synchronization";
            else
                statusBox.Text = "Synchronizing:\n" + path;

            if (synchronizer.IsSynchronizing())
            {
                toggleSynchronizationToolStripMenuItem.Text = "Stop synchonization";
                notifyIcon.Text = "DirToRoblox - Synchronizing...";
            }
            else
            {
                toggleSynchronizationToolStripMenuItem.Text = "Begin synchonization";
                notifyIcon.Text = "DirToRoblox";
            }
            toggleSynchronizationToolStripMenuItem.Enabled = path != null;

            openProjectInExplorerToolStripMenuItem.Enabled = path != null;
            sendManualUpdateToolStripMenuItem.Enabled = synchronizer.IsSynchronizing() && !toggling && synchronizer.GotOneRequest();

        }

        /// <summary>
        /// Reflect settings in the application's behavior
        /// </summary>
        private void ApplySettings()
        {
            this.ShowInTaskbar = settings.ShowInTaskbar;
            synchronizer.UpdatePort();
        }

        private void RecentProjectButton_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            synchronizer.SetPath(item.Text);
            UpdateVisuals();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                var path = browserDialog.SelectedPath;
                synchronizer.SetPath(path);
                // Register the path in the recent projects list
                if (settings.RecentPaths.Contains(path))
                    settings.RecentPaths.Remove(path);
                settings.RecentPaths.Add(path);
                if (settings.RecentPaths.Count > 10)
                    settings.RecentPaths.RemoveAt(0);
                settings.Save();
                UpdateRecentProjectsList();
            }
            UpdateVisuals();
        }

        private void synchronizationButton_Click(object sender, EventArgs e)
        {
            ToggleSynchronization();
        }

        private void openProjectInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = synchronizer.GetPath();
            if (Directory.Exists(path))
                Process.Start(path);
        }

        private void sendManualUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            synchronizer.MakeCreationEvents(synchronizer.GetPath());
        }

        private void clearRecentProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.RecentPaths.Clear();
            settings.Save();
            UpdateRecentProjectsList();
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (synchronizer.IsSynchronizing())
            {
                var prompt = MessageBox.Show("Interrupt synchronization and close DirToRoblox?", "Warning: synchronization in progress", MessageBoxButtons.OKCancel);
                e.Cancel = prompt == DialogResult.Cancel;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toggleSynchronizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSynchronization();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

    
}
