using Microsoft.Web.WebView2.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace RemoteSupportHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async();
            webView21.CoreWebView2.DownloadStarting += CoreWebView2_DownloadStarting;
            webView21.Source = new Uri("http:\\YourScreenConnect.url");
        }

        private void CoreWebView2_DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            var downloadOp = e.DownloadOperation;
            string fileName = Path.GetFileName(downloadOp.ResultFilePath);
            string ext = Path.GetExtension(fileName).ToLower();

            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string savePath = Path.Combine(exeDir, fileName);

            e.ResultFilePath = savePath;
            e.Handled = false;

            downloadOp.StateChanged += (s2, e2) =>
            {
                if (downloadOp.State == CoreWebView2DownloadState.Completed)
                {
                    HandleDownloadedFile(savePath, ext);
                }
            };
        }

        private void HandleDownloadedFile(string path, string ext)
        {
            if (ext == ".zip")
            {
                string extractPath = Path.Combine(Path.GetTempPath(), "ExtractedZip_" + Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(extractPath);

                try
                {
                    ZipFile.ExtractToDirectory(path, extractPath);
                    var exePath = Directory.GetFiles(extractPath, "*.exe", SearchOption.AllDirectories).FirstOrDefault();

                    if (exePath != null)
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path); // Delete the original .zip
                        }

                        StartAndExit(exePath);
                    }
                }
                catch
                {
                    // Optional: silently ignore
                }
            }
            else if (ext == ".exe" || ext == ".msi")
            {
                StartAndExit(path);
            }
        }

        private void StartAndExit(string executablePath)
        {
            try
            {
                string workingDir = Path.GetDirectoryName(executablePath);

                Process.Start(new ProcessStartInfo
                {
                    FileName = executablePath,
                    WorkingDirectory = workingDir,
                    UseShellExecute = true
                });

                Application.Exit();
            }
            catch
            {
                // Optional: silently ignore or log
            }
        }
    }
}
