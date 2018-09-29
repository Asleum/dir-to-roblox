using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DirToRoblox
{
    public class Synchronizer
    {
        private bool synchronizing = false;
        private bool toggling = false;
        private bool gotOneRequest = false;
        private string path;

        private FileSystemWatcher filesWatcher;
        private FileSystemWatcher directoriesWatcher;
        private Form form;
        private HttpListener listener = new HttpListener();
        private List<Dictionary<string, string>> toSend = new List<Dictionary<string, string>>();
        private Properties.Settings settings = Properties.Settings.Default;

        public Synchronizer(FileSystemWatcher filesWatcher, FileSystemWatcher directoriesWatcher, Form form)
        {
            // Assignments
            this.filesWatcher = filesWatcher;
            this.directoriesWatcher = directoriesWatcher;
            this.form = form;

            // Load the latest project, if applicable
            if (settings.RecentPaths.Count > 0)
                path = settings.RecentPaths[settings.RecentPaths.Count - 1];

            // Connect events for the files watcher
            filesWatcher.Changed += OnFileChanged;
            filesWatcher.Renamed += OnRenamed;
            filesWatcher.Deleted += OnFileDeleted;
            filesWatcher.Created += OnCreated;

            // Connect events for the directories watcher
            directoriesWatcher.Renamed += OnRenamed;
            directoriesWatcher.Deleted += OnDirectoryDeleted;
            directoriesWatcher.Created += OnCreated;
        }

        #region Public methods
        /// <summary>
        /// Safely update the path of the synchronized directory
        /// </summary>
        /// <param name="path">New path to synchronize</param>
        public void SetPath(string path)
        {
            if (!synchronizing && !toggling)
                this.path = path;
        }

        /// <summary>
        /// Getter for the path variable
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The currently synchronized (or to synchronize) path</returns>
        public string GetPath()
        {
            return path;
        }

        /// <summary>
        /// Returns wether or not we are currently synchronizing
        /// </summary>
        /// <returns>true if synchronizing</returns>
        public bool IsSynchronizing()
        {
            return synchronizing;
        }

        /// <summary>
        /// Returns wether or not the Roblox plugin communicated with the application at least once
        /// since synchronization started
        /// </summary>
        /// <returns>true if we got one request since synchronization start</returns>
        public bool GotOneRequest()
        {
            return gotOneRequest;
        }

        /// <summary>
        /// Update the port we are listeniong on based on the settings
        /// </summary>
        /// <param name="port"></param>
        public void UpdatePort()
        {
            listener.Prefixes.Clear();
            listener.Prefixes.Add("http://localhost:" + settings.Port + "/dirtoroblox/");
        }

        /// <summary>
        /// Register creation events for everything in the synchronized directory
        /// <param name="path">The directory to send</param>
        /// </summary>
        public void MakeCreationEvents(string parentDirectory)
        {
            toSend.Clear();
            foreach (string directory in Directory.GetDirectories(parentDirectory, "*", SearchOption.AllDirectories))
            {
                MakeModificationEvent(directory, "Folder");
            }
            foreach (string file in Directory.GetFiles(parentDirectory, "*.lua", SearchOption.AllDirectories))
            {
                MakeModificationEvent(file, GetClass(file));
            }
            foreach (string file in Directory.GetFiles(parentDirectory, "*.moon", SearchOption.AllDirectories))
            {
                MakeModificationEvent(file, GetClass(file));
            }
        }

        /// <summary>
        /// Begin reacting to get requests
        /// </summary>
        public void BeginSynchronization()
        {
            if (toggling || synchronizing || !Directory.Exists(path))
                return;
            toggling = true;
            Console.WriteLine("Beginning synchronization");

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
        }

        /// <summary>
        /// Stop reacting to get requests
        /// </summary>
        public void StopSynchronization()
        {
            if (toggling || !synchronizing)
                return;
            toggling = true;
            Console.WriteLine("Stopping synchronization");

            filesWatcher.EnableRaisingEvents = false;
            directoriesWatcher.EnableRaisingEvents = false;
            listener.Stop();
            synchronizing = false;
            toggling = false;
        }
        #endregion

        #region Auxiliary methods
        /// <summary>
        /// Converts a path to one relative to a folder. Also strips the file extension as needed.
        /// </summary>
        /// <param name="file">The path to shorten</param>
        /// <param name="folder">The new root folder</param>
        /// <returns></returns>
        string GetRelativePath(string file, string folder)
        {
            if (settings.TruncateLocalScript)
                file = TruncateFullExtension(file);
            if (settings.TruncateLua)
                file = System.IO.Path.ChangeExtension(file, null);
            Uri pathUri = new Uri(file);
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
            foreach (string extension in settings.LocalExtensions)
            {
                if (path.EndsWith(extension))
                    return "LocalScript";
            }
            foreach (string extension in settings.ScriptExtensions)
            {
                if (path.EndsWith(extension))
                    return "Script";
            }
            if (path.EndsWith(".lua") || path.EndsWith(".moon"))
                return "ModuleScript";
            return null;
        }

        /// <summary>
        /// Truncate the script/localscript extension of a path
        /// </summary>
        /// <param name="file">the path to convert</param>
        /// <returns>the truncated path</returns>
        string TruncateFullExtension(string file)
        {
            foreach (string extension in settings.LocalExtensions)
            {
                if (file.EndsWith(extension))
                    return file.Substring(0, file.Length - extension.Length);
            }
            foreach (string extension in settings.ScriptExtensions)
            {
                if (file.EndsWith(extension))
                    return file.Substring(0, file.Length - extension.Length);
            }
            return file;
        }

        /// <summary>
        /// Read the contents of a file as a string, and compile it if it's moonscript
        /// </summary>
        /// <param name="path">The file to read</param>
        /// <returns>The contents of the given file</returns>
        string GetFileContents(string path)
        {
            string content = null;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    content = File.ReadAllText(path);
                    break;
                }
                catch (IOException) when (i < 5)
                { // Fired when the file is already being read
                    Thread.Sleep(30);
                }
            }
            if (path.EndsWith(".moon")) // MOONSCRIPT ALERT
            {
                // Initialize the compilation process
                var compilationProcess = new Process();
                var info = compilationProcess.StartInfo;
                info.CreateNoWindow = true;
                info.FileName = "cmd.exe";
                // /C is a parameter for cmd.exe, it indicates that we want to just execute the given command, then close
                // The -- parameters indicate that we want to read from stdin and output the compiled moonscript in stdout
                info.Arguments = "/C moonc --";
                info.UseShellExecute = false;
                info.RedirectStandardOutput = true;
                info.RedirectStandardInput = true;

                // Now let's run this bad boy
                compilationProcess.Start();
                compilationProcess.StandardInput.WriteLine(content);
                compilationProcess.StandardInput.Close();
                var compiled = compilationProcess.StandardOutput.ReadToEnd();
                compilationProcess.WaitForExit();
                return compiled;
            }
            return content;
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
                // Update visuals to indicate that the Roblox plugin communicated successfully
                if (!gotOneRequest)
                {
                    gotOneRequest = true;
                    // Not using that delegate thingy leads C# to complain about threads being wrong
                    form.Invoke(new MethodInvoker(delegate
                    {
                        form.UpdateVisuals();
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
            }
            catch (HttpListenerException) { }
        }
        #endregion

        #region File watchers events

        private void MakeModificationEvent(string file, string robloxClass)
        {
            // To send:
            // - Indication that something is changed
            // - Path of the file
            // - Content of the file (if not a folder)
            // - Roblox class associated to the element
            var data = new Dictionary<string, string>();
            data.Add("Type", "Modification");
            data.Add("Path", GetRelativePath(file, path));
            data.Add("Class", robloxClass);
            if (File.Exists(file))
                data.Add("Content", GetFileContents(file));
            toSend.Add(data);
        }

        private void MakeDeletionEvent(string file, string robloxClass)
        {
            // To send:
            // - Indication that a file is deleted
            // - Path of the file to delete
            // - Roblox class associated to the element
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("Type", "Deletion");
            data.Add("Path", GetRelativePath(file, path));
            data.Add("Class", robloxClass);
            toSend.Add(data);
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            var robloxClass = GetClass(e.FullPath);
            if (robloxClass == null)
                return;
            // Even though we're dealing with a creation, we're sending a modification because those are handled exactly the same way
            MakeModificationEvent(e.FullPath, robloxClass);

            // Manually make creation event for a directory's child
            if (Directory.Exists(e.FullPath))
                MakeCreationEvents(e.FullPath);
            Console.WriteLine("Created: " + e.Name);
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            var robloxClass = GetClass(e.FullPath);
            if (robloxClass == null)
                return;
            MakeModificationEvent(e.FullPath, robloxClass);
            Console.WriteLine("Changed: " + e.Name);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            var robloxClass = GetClass(e.FullPath);
            if (robloxClass == null)
                return;
            MakeDeletionEvent(e.FullPath, robloxClass);
            Console.WriteLine("Deleted: " + e.Name);
        }

        private void OnDirectoryDeleted(object sender, FileSystemEventArgs e)
        {
            MakeDeletionEvent(e.FullPath, "Folder");
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
            }
            else
            {
                // Send a deletion for the old item, followed by a creation for the new one
                if (oldClass != null)
                    MakeDeletionEvent(e.OldFullPath, oldClass);
                if (newClass != null)
                    MakeModificationEvent(e.FullPath, newClass);
            }
            Console.WriteLine("Renamed: " + e.Name);
        }

        #endregion
    }
}
