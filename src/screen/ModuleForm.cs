using Microsoft.Web.WebView2.WinForms;

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
            await webView.EnsureCoreWebView2Async();

            string modulePath = Path.Combine(Application.StartupPath, "src", "assets", "html", "module.html");
            webView.Source = new Uri(modulePath);

            webView.CoreWebView2.WebMessageReceived += WebMessageReceived;
        }

        private void WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string msg = e.TryGetWebMessageAsString();

            if (msg == "finish")
            {
                this.Hide();
                QuizForm quiz = new QuizForm();
                quiz.Show();
            }
            if (msg == "open-pdf")
            {
                string pdfPath = Path.Combine(
                    Application.StartupPath,
                    "src", "assets", "pdf", "modul1.pdf"
                );

                if (File.Exists(pdfPath))
                {
                    webView.CoreWebView2.PostWebMessageAsString(pdfPath);
                }
                else
                {
                    MessageBox.Show("PDF tidak ditemukan!");
                }
            }

        }

        private void ModuleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
                e.Cancel = true;
        }
    }
}
