using Microsoft.Web.WebView2.WinForms;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LoginSystem
{
    public partial class LoginForm : Form
    {
        // Helper untuk cek CTRL (khusus jika ingin dipakai di tambahkan, tidak wajib)
        private static bool IsCtrlPressed() => (GetKeyState(0x11) & 0x8000) != 0;

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        private WebView2 webView;

        public LoginForm()
        {
            InitializeComponent();
            SetupForm();
            SetupWebView();

            // Aktifkan blokir keyboard
            KeyboardBlocker.BlockKeys();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Cegah menutup aplikasi dengan ALT+F4 / X
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }

            // Saat aplikasi exit normal → lepas hook
            KeyboardBlocker.UnblockKeys();
            base.OnFormClosing(e);
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
            await webView.EnsureCoreWebView2Async();

            string loginPath = Path.Combine(Application.StartupPath, "src", "assets", "html", "index.html");
            webView.CoreWebView2.Navigate(loginPath);

            webView.CoreWebView2.WebMessageReceived += WebMessageReceived;
        }

        private void WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string message = e.TryGetWebMessageAsString();

            if (message == "user")
            {
                this.Hide();
                TikQuestionForm tikQuestion = new TikQuestionForm();
                tikQuestion.Show();
            }
            else if (message == "admin")
            {
                this.Hide();
                AdminPassword adminForm = new AdminPassword();
                adminForm.Show();
            }
        }
    }
}
