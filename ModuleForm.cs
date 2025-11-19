using System;
using System.Windows.Forms;

namespace LoginSystem
{
    public partial class ModuleForm : Form
    {
        private bool canClose = false; // flag agar form bisa ditutup

        public ModuleForm()
        {
            InitializeComponent();

            // Fullscreen
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

            // Tidak bisa close manual
            this.FormClosing += ModuleForm_FormClosing;

            // Buat button untuk kembali ke QuizForm
            Button btnBackToQuiz = new Button();
            btnBackToQuiz.Text = "Kembali ke Quiz";
            btnBackToQuiz.Width = 150;
            btnBackToQuiz.Height = 40;
            btnBackToQuiz.Top = 50;
            btnBackToQuiz.Left = (this.ClientSize.Width - btnBackToQuiz.Width) / 2;
            btnBackToQuiz.Click += BtnBackToQuiz_Click;

            this.Controls.Add(btnBackToQuiz);
        }

        private void ModuleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true; // cegah close manual
            }
        }

        private void BtnBackToQuiz_Click(object sender, EventArgs e)
        {
            // Izinkan form ditutup
            canClose = true;
            this.Close();

            // Buka QuizForm lagi
            QuizForm quiz = new QuizForm();
            quiz.Show();
        }
    }
}
