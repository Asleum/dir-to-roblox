using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirToRoblox
{
    public partial class SettingsForm : System.Windows.Forms.Form
    {

        private Properties.Settings settings = Properties.Settings.Default;

        public SettingsForm()
        {
            InitializeComponent();
            populateFields();
        }

        /// <summary>
        /// Fill the various fields to reflect the currently stored settings
        /// </summary>
        private void populateFields()
        {
            showNotificationCheckBox.Checked = settings.ShowNotification;
            autoMinimizeTextBox.Checked = settings.AutoMinimize;
            showInTaskbarCheckBox.Checked = settings.ShowInTaskbar;
            truncateLuaCheckBox.Checked = settings.TruncateLua;
            truncateLocalScriptCheckBox.Checked = settings.TruncateLocalScript;
            portBox.Text = settings.Port.ToString();
            scriptExtensionsBox.Text = "";
            foreach (string line in settings.ScriptExtensions)
            {
                scriptExtensionsBox.Text += line + "\n";
            }
            localExtensionsBox.Text = "";
            foreach (string line in settings.LocalExtensions)
            {
                localExtensionsBox.Text += line + "\n";
            }
        }

        /// <summary>
        /// Save the contents of the various fields inside the settings
        /// </summary>
        private void saveSettings()
        {
            settings.ShowNotification = showNotificationCheckBox.Checked;
            settings.AutoMinimize = autoMinimizeTextBox.Checked;
            settings.ShowInTaskbar = showInTaskbarCheckBox.Checked;
            settings.TruncateLua = truncateLuaCheckBox.Checked;
            settings.TruncateLocalScript = truncateLocalScriptCheckBox.Checked;
            int port;
            if (Int32.TryParse(portBox.Text, out port))
                settings.Port = port;
            else
                settings.Port = 3260;
            settings.ScriptExtensions.Clear();
            foreach (string line in scriptExtensionsBox.Text.Split("\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                settings.ScriptExtensions.Insert(settings.ScriptExtensions.Count, line);
            }
            settings.Save();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveSettings();
            this.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void restoreDefaultsButton_Click(object sender, EventArgs e)
        {
            settings.Reset();
            populateFields();
        }
    }
}
