using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace LoginSystem
{
    public partial class TikQuestionForm : Form
    {
        private WebView2 webView;

        public TikQuestionForm()
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
            this.ControlBox = false;
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

            string tikQuestionPath = Path.Combine(Application.StartupPath, "src", "assets", "html", "tik-question.html");
            webView.CoreWebView2.Navigate(tikQuestionPath);

            webView.CoreWebView2.WebMessageReceived += WebMessageReceived;
        }

        private void WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string message = e.TryGetWebMessageAsString();

            if (message == "yes")
            {
                // Jika YES, langsung ke quiz
                this.Hide();
                QuizForm quiz = new QuizForm();
                quiz.Show();
            }
            else if (message == "no")
            {
                // Jika NO, ke module dulu
                this.Hide();
                ModuleForm module = new ModuleForm();
                module.Show();
            }
        }
    }
}

