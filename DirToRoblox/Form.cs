using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirToRoblox
{
    public partial class Form : System.Windows.Forms.Form
    {
        private bool synchronizing = false;
        private bool toggling = false;
        private bool gotOneRequest = false;
        private string path;
        private Properties.Settings settings = Properties.Settings.Default;
        private List<Dictionary<string, string>> toSend = new List<Dictionary<string, string>>();

        private HttpListener listener;

        public Form()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:3260/dirtoroblox/");

            if (settings.RecentPaths.Count > 0)
                path = settings.RecentPaths[settings.RecentPaths.Count - 1];
            InitializeComponent();
            UpdateVisuals();
            UpdateRecentProjectsList();

            filesWatcher.Changed += OnFileChanged;
            filesWatcher.Renamed += OnRenamed;
            filesWatcher.Deleted += OnFileDeleted;
            filesWatcher.Created += OnCreated;
            directoriesWatcher.Renamed += OnRenamed;
            directoriesWatcher.Deleted += OnDirectoryDeleted;
            directoriesWatcher.Created += OnCreated;
        }

        /// <summary>
        /// Converts a path to one relative to a folder. Also strips the file extension.
        /// </summary>
        /// <param name="file">The path to shorten</param>
        /// <param name="folder">The new root folder</param>
        /// <returns></returns>
        string GetRelativePath(string file, string folder)
        {
            Uri pathUri = new Uri(System.IO.Path.ChangeExtension(file, null));
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                folder += Path.DirectorySeparatorChar;
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

        /// <summary>
        /// Get the Roblox type corresponding to a file or directory
        /// </summary>
        /// <param name="path">The item in the local filesystem to convert</param>
        /// <returns></returns>
        string GetClass(string path)
        {
            if (Directory.Exists(path))
                return "Folder";
            if (path.EndsWith(".local.lua"))
                return "LocalScript";
            if (path.EndsWith(".server.lua"))
                return "Script";
            if (path.EndsWith(".lua"))
                return "ModuleScript";
            return null;
        }

        /// <summary>
        /// Read the contents of a file as a string
        /// </summary>
        /// <param name="path">The file to read</param>
        /// <returns>The contents of the given file</returns>
        string GetFileContents(string path)
        {
            for (int i = 0; i < 5; i++)
            {
                try {
                    return File.ReadAllText(path);
                } catch (IOException) when (i < 5) { // Fired when the file is already being read
                    Thread.Sleep(30);
                }
            }
            return null;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            var robloxClass = GetClass(e.FullPath);
            if (robloxClass == null)
                return;
            // To send:
            // - Indication that a file is created
            // - Path of the created file
            // - Contents of the created file (in case it was copied or moved)
            // - Roblox class associated to the element
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Type", "Creation");
            data.Add("Path", GetRelativePath(e.FullPath, path));
            data.Add("Class", robloxClass);
            if (File.Exists(e.FullPath))
                data.Add("Content", GetFileContents(e.FullPath));
            toSend.Add(data);
            // Manually make creation event for a directory's child
            if (Directory.Exists(e.FullPath))
                MakeCreationEvents(e.FullPath);
            Console.WriteLine("Created: " + e.Name);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            var robloxClass = GetClass(e.FullPath);
            if (robloxClass == null)
                return;
            // To send:
            // - Indication that a file is deleted
            // - Path of the file to delete
            // - Roblox class associated to the element
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Type", "Deletion");
            data.Add("Path", GetRelativePath(e.FullPath, path));
            data.Add("Class", robloxClass);
            toSend.Add(data);
            Console.WriteLine("Deleted: " + e.Name);
        }

        private void OnDirectoryDeleted(object sender, FileSystemEventArgs e)
        {
            // To send:
            // - Indication that a directory is deleted
            // - Path of the directory to delete
            // - Roblox class associated to the element
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Type", "Deletion");
            data.Add("Path", GetRelativePath(e.FullPath, path));
            data.Add("Class", "Folder");
            toSend.Add(data);
            Console.WriteLine("Deleted: " + e.Name);
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("Renaming: " + e.Name);
            var oldClass = GetClass(e.OldFullPath);
            var newClass = GetClass(e.FullPath);
            //If the renamed element is a directory, oldClass will be null because Directory.Exists returned null, since oldFullPath does not exists anymore. Let's fix that!
            if (newClass == "Folder")
                oldClass = "Folder";
            else if (oldClass == null && newClass == null)
                return;

            if (oldClass == newClass)
            {
                // To send:
                // - Indication that something is renamed
                // - Old path of the file
                // - New name of the file
                // - Roblox class associated to the element
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("Type", "Renaming");
                data.Add("Path", GetRelativePath(e.OldFullPath, path));
                data.Add("NewName", Path.GetFileNameWithoutExtension(e.Name));
                data.Add("Class", newClass);
                toSend.Add(data);
            } else
            {
                // Send a deletion for the old item, followed by a creation for the new one
                if (oldClass != null)
                {
                    Dictionary<string, string> data = new Dictionary<string, string>();
                    data.Add("Type", "Deletion");
                    data.Add("Path", GetRelativePath(e.OldFullPath, path));
                    data.Add("Class", oldClass);
                    toSend.Add(data);
                }
                if (newClass != null)
                {
                    Dictionary<string, string> data = new Dictionary<string, string>();
                    data.Add("Type", "Creation");
                    data.Add("Path", GetRelativePath(e.FullPath, path));
                    data.Add("Class", newClass);
                    if (File.Exists(e.FullPath))
                        data.Add("Content", GetFileContents(e.FullPath));
                    toSend.Add(data);
                }
            }
            Console.WriteLine("Renamed: " + e.Name);
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            var robloxClass = GetClass(e.FullPath);
            if (robloxClass == null)
                return;
            // To send:
            // - Indication that something is changed
            // - Path of the file
            // - Content of the file
            // - Roblox class associated to the element
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Type", "Modification");
            data.Add("Path", GetRelativePath(e.FullPath, path));
            data.Add("Class", robloxClass);
            if (File.Exists(e.FullPath))
                data.Add("Content", GetFileContents(e.FullPath));
            toSend.Add(data);
            Console.WriteLine("Changed: " + e.Name);
        }

        /// <summary>
        /// Register creation events for everything in the synchronized directory
        /// <param name="path">The directory to send</param>
        /// </summary>
        private void MakeCreationEvents(string parentDirectory)
        {
            foreach (string directory in Directory.GetDirectories(parentDirectory, "*", SearchOption.AllDirectories))
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("Type", "Creation");
                data.Add("Path", GetRelativePath(directory, path));
                data.Add("Class", "Folder");
                toSend.Add(data);
            }
            foreach (string file in Directory.GetFiles(parentDirectory, "*.lua", SearchOption.AllDirectories))
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                data.Add("Type", "Creation");
                data.Add("Path", GetRelativePath(file, path));
                data.Add("Class", GetClass(file));
                data.Add("Content", GetFileContents(file));
                toSend.Add(data);
            }
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
                    Console.WriteLine(path);
                    ToolStripMenuItem button = new ToolStripMenuItem(path);
                    button.Click += RecentProjectButton_Click;
                    recentToolStripMenuItem.DropDownItems.Insert(0, button);
                }
                recentToolStripMenuItem.Enabled = true;
            }
        }

        private void RecentProjectButton_Click(object sender, EventArgs e)
        {
            if (!synchronizing && !toggling)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                path = item.Text;
                UpdateVisuals();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                path = browserDialog.SelectedPath;
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
            if (synchronizing)
                StopSynchronization();
            else
                BeginSynchronization();
        }

        /// <summary>
        /// Executed when a Get request is received on the local server
        /// </summary>
        /// <param name="result">The passed result</param>
        private void HandleGet(IAsyncResult result)
        {
            if (!listener.IsListening)
                return;
            try
            {
                if (!gotOneRequest)
                {
                    gotOneRequest = true;
                    this.Invoke(new MethodInvoker(delegate
                    {
                        UpdateVisuals();
                    }));
                }
                // Process the data to send
                var json = JsonConvert.SerializeObject(toSend);
                toSend.Clear();
                // Get the response and write into it
                var context = listener.EndGetContext(result);
                var response = context.Response;
                response.StatusCode = 200;
                var buffer = Encoding.UTF8.GetBytes(json);
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
                // All done, now we can wait for another request
                listener.BeginGetContext(HandleGet, null);
            } catch (HttpListenerException) { }
        }

        /// <summary>
        /// Update the interface to reflect thew current state of the program
        /// </summary>
        private void UpdateVisuals()
        {
            var exists = Directory.Exists(path);
            synchronizationButton.Enabled = exists && !toggling;
            if (synchronizing)
                synchronizationButton.Text = "Stop synchronization";
            else
                synchronizationButton.Text = "Begin synchronization";

            if (path == "" || path == null)
                statusBox.Text = "Select a directory to synchronize using the \"Project\" menu button";
            else if (!exists)
                statusBox.Text = "The selected path does not exist anymore:\n" + path;
            else if (!synchronizing)
                statusBox.Text = "Ready to synchronize:\n" + path;
            else if (!gotOneRequest)
                statusBox.Text = "Activate DirToRoblox plugin inside Roblox Studio to begin synchronization";
            else
                statusBox.Text = "Synchronizing:\n" + path;

            openProjectInExplorerToolStripMenuItem.Enabled = path != null;
            sendManualUpdateToolStripMenuItem.Enabled = synchronizing && !toggling && gotOneRequest;
            
        }

        /// <summary>
        /// Begin reacting to get requests
        /// </summary>
        private void BeginSynchronization()
        {
            if (toggling || synchronizing || !Directory.Exists(path))
                return;
            toggling = true;
            Console.WriteLine("Beginning synchronization");
            UpdateVisuals();
            toSend.Clear();
            MakeCreationEvents(path);


            filesWatcher.Path = path;
            filesWatcher.EnableRaisingEvents = true;
            directoriesWatcher.Path = path;
            directoriesWatcher.EnableRaisingEvents = true;
            listener.Start();
            listener.BeginGetContext(HandleGet, null);
            synchronizing = true;
            gotOneRequest = false;

            toggling = false;
            UpdateVisuals();
        }

        /// <summary>
        /// Stop reacting to get requests
        /// </summary>
        private void StopSynchronization()
        {
            if (toggling || !synchronizing)
                return;
            toggling = true;
            Console.WriteLine("Stopping synchronization");
            UpdateVisuals();

            filesWatcher.EnableRaisingEvents = false;
            directoriesWatcher.EnableRaisingEvents = false;
            listener.Stop();
            synchronizing = false;

            toggling = false;
            UpdateVisuals();
        }

        private void openProjectInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(path))
                Process.Start(path);
        }

        private void sendManualUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (synchronizing)
                MakeCreationEvents(path);
        }

        private void clearRecentProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.RecentPaths.Clear();
            settings.Save();
            UpdateRecentProjectsList();
        }
    }
}
