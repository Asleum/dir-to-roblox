using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirToRoblox
{
    public partial class Form : System.Windows.Forms.Form
    {
        //private bool synchronizing = false;
        private string path;

        private HttpListener listener;

        public Form()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:3260/dirtoroblox/");
            listener.Start();
            listener.BeginGetContext(HandleGet, null);
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                path = browserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Executed when a Get request is received on the local server
        /// </summary>
        /// <param name="result">The passed result</param>
        private void HandleGet(IAsyncResult result)
        {
            // Get the response and write into it
            var context = listener.EndGetContext(result);
            var response = context.Response;
            response.StatusCode = 200;
            var buffer = Encoding.UTF8.GetBytes("Hello, Roblox!");
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
            // All done, now we can wait for another request
            listener.BeginGetContext(HandleGet, null);
        }
    }
}
