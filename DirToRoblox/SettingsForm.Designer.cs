namespace DirToRoblox
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.localExtensionsBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scriptExtensionsBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.truncateLocalScriptCheckBox = new System.Windows.Forms.CheckBox();
            this.truncateLuaCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.autoMinimizeTextBox = new System.Windows.Forms.CheckBox();
            this.showNotificationCheckBox = new System.Windows.Forms.CheckBox();
            this.showInTaskbarCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.restoreDefaultsButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(503, 181);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File extensions";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.localExtensionsBox);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(8, 101);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(487, 70);
            this.panel2.TabIndex = 1;
            // 
            // localExtensionsBox
            // 
            this.localExtensionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.localExtensionsBox.Location = new System.Drawing.Point(183, 0);
            this.localExtensionsBox.Margin = new System.Windows.Forms.Padding(0);
            this.localExtensionsBox.Name = "localExtensionsBox";
            this.localExtensionsBox.Size = new System.Drawing.Size(303, 69);
            this.localExtensionsBox.TabIndex = 1;
            this.localExtensionsBox.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 70);
            this.label2.TabIndex = 0;
            this.label2.Text = "If a file\'s name ends with any of these, it will be sent to Roblox as a LocalScri" +
    "pt (one item per line)";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.scriptExtensionsBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 23);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(487, 70);
            this.panel1.TabIndex = 0;
            // 
            // scriptExtensionsBox
            // 
            this.scriptExtensionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scriptExtensionsBox.Location = new System.Drawing.Point(183, 0);
            this.scriptExtensionsBox.Margin = new System.Windows.Forms.Padding(0);
            this.scriptExtensionsBox.Name = "scriptExtensionsBox";
            this.scriptExtensionsBox.Size = new System.Drawing.Size(303, 69);
            this.scriptExtensionsBox.TabIndex = 1;
            this.scriptExtensionsBox.Text = "";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 70);
            this.label1.TabIndex = 0;
            this.label1.Text = "If a file\'s name ends with any of these, it will be sent to Roblox as a regular S" +
    "cript (one item per line)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.truncateLocalScriptCheckBox);
            this.groupBox2.Controls.Add(this.truncateLuaCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(16, 203);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(503, 117);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Naming";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 76);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(483, 34);
            this.label4.TabIndex = 3;
            this.label4.Text = "Warning: if changing any of the above settings while working on a project in prog" +
    "ress, make sure to rename your scripts in Studio first!";
            // 
            // truncateLocalScriptCheckBox
            // 
            this.truncateLocalScriptCheckBox.AutoSize = true;
            this.truncateLocalScriptCheckBox.Location = new System.Drawing.Point(8, 52);
            this.truncateLocalScriptCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.truncateLocalScriptCheckBox.Name = "truncateLocalScriptCheckBox";
            this.truncateLocalScriptCheckBox.Size = new System.Drawing.Size(334, 21);
            this.truncateLocalScriptCheckBox.TabIndex = 2;
            this.truncateLocalScriptCheckBox.Text = "Truncate Script/LocalScript extensions in Roblox";
            this.truncateLocalScriptCheckBox.UseVisualStyleBackColor = true;
            // 
            // truncateLuaCheckBox
            // 
            this.truncateLuaCheckBox.AutoSize = true;
            this.truncateLuaCheckBox.Location = new System.Drawing.Point(8, 23);
            this.truncateLuaCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.truncateLuaCheckBox.Name = "truncateLuaCheckBox";
            this.truncateLuaCheckBox.Size = new System.Drawing.Size(247, 21);
            this.truncateLuaCheckBox.TabIndex = 1;
            this.truncateLuaCheckBox.Text = "Truncate .lua extensions in Roblox";
            this.truncateLuaCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.autoMinimizeTextBox);
            this.groupBox3.Controls.Add(this.showNotificationCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(16, 327);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(503, 52);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "After synchronization started";
            // 
            // autoMinimizeTextBox
            // 
            this.autoMinimizeTextBox.AutoSize = true;
            this.autoMinimizeTextBox.Location = new System.Drawing.Point(191, 23);
            this.autoMinimizeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.autoMinimizeTextBox.Name = "autoMinimizeTextBox";
            this.autoMinimizeTextBox.Size = new System.Drawing.Size(128, 21);
            this.autoMinimizeTextBox.TabIndex = 2;
            this.autoMinimizeTextBox.Text = "Minimize to tray";
            this.autoMinimizeTextBox.UseVisualStyleBackColor = true;
            // 
            // showNotificationCheckBox
            // 
            this.showNotificationCheckBox.AutoSize = true;
            this.showNotificationCheckBox.Location = new System.Drawing.Point(8, 23);
            this.showNotificationCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.showNotificationCheckBox.Name = "showNotificationCheckBox";
            this.showNotificationCheckBox.Size = new System.Drawing.Size(148, 21);
            this.showNotificationCheckBox.TabIndex = 1;
            this.showNotificationCheckBox.Text = "Show a notification";
            this.showNotificationCheckBox.UseVisualStyleBackColor = true;
            // 
            // showInTaskbarCheckBox
            // 
            this.showInTaskbarCheckBox.AutoSize = true;
            this.showInTaskbarCheckBox.Location = new System.Drawing.Point(8, 23);
            this.showInTaskbarCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.showInTaskbarCheckBox.Name = "showInTaskbarCheckBox";
            this.showInTaskbarCheckBox.Size = new System.Drawing.Size(130, 21);
            this.showInTaskbarCheckBox.TabIndex = 1;
            this.showInTaskbarCheckBox.Text = "Show in taskbar";
            this.showInTaskbarCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.portBox);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.showInTaskbarCheckBox);
            this.groupBox4.Location = new System.Drawing.Point(16, 386);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(503, 94);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Other settings";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 53);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(481, 34);
            this.label5.TabIndex = 4;
            this.label5.Text = "Warning: if you change the port here, make sure to change it manually inside the " +
    "DirToRoblox Studio plugin too! (PORT value)";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(297, 21);
            this.portBox.Margin = new System.Windows.Forms.Padding(4);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(192, 22);
            this.portBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Listen on port:";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(16, 489);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(159, 28);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save and exit";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(183, 489);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(165, 28);
            this.exitButton.TabIndex = 6;
            this.exitButton.Text = "Exit without saving";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // restoreDefaultsButton
            // 
            this.restoreDefaultsButton.Location = new System.Drawing.Point(356, 489);
            this.restoreDefaultsButton.Margin = new System.Windows.Forms.Padding(4);
            this.restoreDefaultsButton.Name = "restoreDefaultsButton";
            this.restoreDefaultsButton.Size = new System.Drawing.Size(163, 28);
            this.restoreDefaultsButton.TabIndex = 7;
            this.restoreDefaultsButton.Text = "Restore defaults";
            this.restoreDefaultsButton.UseVisualStyleBackColor = true;
            this.restoreDefaultsButton.Click += new System.EventHandler(this.restoreDefaultsButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 527);
            this.Controls.Add(this.restoreDefaultsButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingsForm";
            this.Text = "DirToRoblox settings";
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox localExtensionsBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox scriptExtensionsBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox truncateLocalScriptCheckBox;
        private System.Windows.Forms.CheckBox truncateLuaCheckBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox autoMinimizeTextBox;
        private System.Windows.Forms.CheckBox showNotificationCheckBox;
        private System.Windows.Forms.CheckBox showInTaskbarCheckBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button restoreDefaultsButton;
    }
}