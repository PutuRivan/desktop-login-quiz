using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace LoginSystem
{
    public partial class ModuleForm : Form
    {
        private WebView2 webView;
        private bool canClose = false;

        public ModuleForm()
        {
            InitializeComponent();
            SetupForm();
            SetupWebView();
        }

        private void SetupForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

            this.FormClosing += ModuleForm_FormClosing;
        }

        private async void SetupWebView()
        {
            webView = new WebView2
            {
                Dock = DockStyle.Fill
            };

            this.Controls.Add(webView);

            // Create a user data folder in a location with proper permissions
            string userDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "LoginQuizApp",
                "WebView2");

            Directory.CreateDirectory(userDataFolder);

            var environment = await CoreWebView2Environment.CreateAsync(
                browserExecutableFolder: null,
                userDataFolder: userDataFolder);

            await webView.EnsureCoreWebView2Async(environment);

            string modulePath = Path.Combine(Application.StartupPath, "src", "assets", "html", "module.html");
            webView.Source = new Uri(modulePath);

            webView.CoreWebView2.WebMessageReceived += WebMessageReceived;
        }

        private void WebMessageReceived(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string msg = e.TryGetWebMessageAsString();

            if (msg == "finish")
            {
                this.Hide();
                new QuizForm().Show();
            }

            if (msg == "open-pdf")
            {
                string pdfPath = Path.Combine(
                    Application.StartupPath,
                    "src", "assets", "pdf", "MODULE INFORMATIKA.pdf"
                );

                if (!File.Exists(pdfPath))
                {
                    MessageBox.Show("PDF tidak ditemukan!");
                    return;
                }

                // Convert Windows path → valid file URL
                string pdfUrl = new Uri(pdfPath).AbsoluteUri;

                webView.CoreWebView2.PostWebMessageAsString(pdfUrl);
            }
        }

        private void ModuleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
                e.Cancel = true;
        }
    }
}
