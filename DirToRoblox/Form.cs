using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private string path = "C:\\Users\\samue\\Documents\\~Fichiers\\Dev\\Roblox\\OtherProjects\\Finished\\Solitaire\\Sources";
        private List<Dictionary<string, string>> toSend = new List<Dictionary<string, string>>();

        private HttpListener listener;

        public Form()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:3260/dirtoroblox/");

            InitializeComponent();
            UpdateVisuals();

            watcher.Changed += OnFileChanged;
            watcher.Renamed += OnFileRenamed;
            watcher.Deleted += OnFileDeleted;
            watcher.Created += OnFileCreated;
        }

        /// <summary>
        /// Converts a path to one relative to a folder
        /// </summary>
        /// <param name="file">The path to shorten</param>
        /// <param name="folder">The new root folder</param>
        /// <returns></returns>
        string GetRelativePath(string file, string folder)
        {
            Uri pathUri = new Uri(file);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
                folder += Path.DirectorySeparatorChar;
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }

        /// <summary>
        /// Wether or not a given file should be included in the synchronization
        /// </summary>
        /// <param name="fileName">The file to check</param>
        /// <returns>True if the file is going needs be synchronized</returns>
        bool IsFileRelevant(string fileName)
        {
            // We want to watch lua files and directories only
            return fileName.EndsWith(".lua") || !fileName.Contains('.');
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
                } catch (IOException) when (i < 5) {
                    Thread.Sleep(50);
                }
            }
            return null;
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            if (!IsFileRelevant(e.Name))
                return;
            // To send:
            // - Indication that a file is created
            // - Path of the created file
            // - Contents of the created file (in case it was copied or moved)
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Type", "Creation");
            data.Add("Path", GetRelativePath(e.FullPath, path));
            data.Add("Content", GetFileContents(e.FullPath));
            toSend.Add(data);
            Console.WriteLine("Created: " + e.Name);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            if (!IsFileRelevant(e.Name))
                return;
            // To send:
            // - Indication that a file is deleted
            // - Path of the file to delete
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Type", "Deletion");
            data.Add("Path", GetRelativePath(e.FullPath, path));
            toSend.Add(data);
            Console.WriteLine("Deleted: " + e.Name);
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            var oldRelevant = IsFileRelevant(e.OldFullPath);
            var newRelevant = IsFileRelevant(e.FullPath);
            if (!oldRelevant && !newRelevant)
                return;
            // To send:
            // - Indication that something is renamed
            // - Old path of the file
            // - New name of the file
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (!oldRelevant && newRelevant)
            {
                data.Add("Type", "Creation");
                data.Add("Path", GetRelativePath(e.FullPath, path));
                data.Add("Content", GetFileContents(e.FullPath));
            } else if (oldRelevant && !newRelevant)
            {
                data.Add("Type", "Deletion");
                data.Add("Path", GetRelativePath(e.OldFullPath, path));
            } else
            {
                data.Add("Type", "Renaming");
                data.Add("Path", GetRelativePath(e.OldFullPath, path));
                data.Add("NewName", e.Name);
            }
            toSend.Add(data);
            Console.WriteLine("Renamed: " + e.Name);
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            if (!IsFileRelevant(e.Name))
                return;
            // To send:
            // - Indication that something is changed
            // - Path of the file
            // - Content of the file
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Type", "Modification");
            data.Add("Path", GetRelativePath(e.FullPath, path));
            data.Add("Content", GetFileContents(e.FullPath));
            toSend.Add(data);
            Console.WriteLine("Changed: " + e.Name);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                path = browserDialog.SelectedPath;
                Console.WriteLine(path);
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
            else
                statusBox.Text = "Synchronizing:\n" + path;
            
        }

        /// <summary>
        /// Begin reacting to get requests and enable file watcher
        /// </summary>
        private void BeginSynchronization()
        {
            if (toggling || synchronizing)
                return;
            toggling = true;
            Console.WriteLine("Beginning synchronization");
            UpdateVisuals();

            watcher.Path = path;
            watcher.EnableRaisingEvents = true;
            listener.Start();
            listener.BeginGetContext(HandleGet, null);
            synchronizing = true;

            toggling = false;
            UpdateVisuals();
        }

        /// <summary>
        /// Stop reacting to get requests and disable file watcher
        /// </summary>
        private void StopSynchronization()
        {
            if (toggling || !synchronizing)
                return;
            toggling = true;
            Console.WriteLine("Stopping synchronization");
            UpdateVisuals();

            watcher.EnableRaisingEvents = false;
            listener.Stop();
            synchronizing = false;

            toggling = false;
            UpdateVisuals();
        }
    }
}
