using Microsoft.Web.WebView2.WinForms;
using System;
using System.IO;
using System.Windows.Forms;

namespace LoginSystem
{
    public partial class AdminPassword : Form
    {
        private WebView2 webView;
        private readonly string correctPassword = "admin123";
        private bool allowClose = false;

        public AdminPassword()
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
        }

        private async void SetupWebView()
        {
            webView = new WebView2 { Dock = DockStyle.Fill };
            this.Controls.Add(webView);

            await webView.EnsureCoreWebView2Async();

            string filePath = Path.Combine(Application.StartupPath, "src", "assets", "html", "admin-password.html");
            webView.Source = new Uri(filePath);

            webView.CoreWebView2.WebMessageReceived += OnWebMessageReceived;
        }

        private void OnWebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string message = e.TryGetWebMessageAsString();

            if (message.StartsWith("submitPassword|"))
            {
                string userPassword = message.Replace("submitPassword|", "");

                if (userPassword == correctPassword)
                {
                    allowClose = true; // <--- IZINKAN FORM DITUTUP
                    MessageBox.Show("Login Admin berhasil!");
                    this.Close(); // atau Application.Exit();
                }
                else
                {
                    webView.CoreWebView2.PostWebMessageAsString("wrong");
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowClose)
                e.Cancel = true;  // tetap block jika belum login
        }
    }
}
