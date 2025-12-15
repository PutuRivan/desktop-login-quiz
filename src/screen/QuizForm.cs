using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace LoginSystem
{
    public partial class QuizForm : Form
    {
        private WebView2 webView;
        private bool canClose = false;

        public QuizForm()
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
            this.FormClosing += QuizForm_FormClosing;
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

            // Load halaman quiz
            string path = Path.Combine(Application.StartupPath, "src", "assets", "html", "quiz.html");
            webView.CoreWebView2.Navigate(path);

            // Event listener pesan dari JavaScript
            webView.CoreWebView2.WebMessageReceived += OnMessageReceived;

            // Reset quiz setiap kali halaman selesai dimuat
            webView.CoreWebView2.NavigationCompleted += (s, e) =>
            {
                webView.CoreWebView2.ExecuteScriptAsync("currentIndex = 0; correctCount = 0;");
            };
        }

        private void QuizForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        private void OnMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string msg = e.TryGetWebMessageAsString();

            if (msg == "open_desktop")
            {
                // Tampilkan notifikasi jika semua jawaban benar
                MessageBox.Show("Selamat Datang User", "Notifikasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                canClose = true;
                this.Hide(); // Hilangkan cepat
                this.Close();

                MainForm desktop = new MainForm();
                desktop.Show();
            }
            else if (msg == "wrong")
            {
                webView.CoreWebView2.ExecuteScriptAsync("currentIndex = 0; correctCount = 0;");

                canClose = true;
                this.Hide(); // Hindari form muncul kembali
                this.Close();

                ModuleForm module = new ModuleForm();
                module.Show();
            }
        }

    }
}
