using System;
using System.Windows.Forms;

namespace LoginSystem
{
    public partial class QuizForm : Form
    {
        private bool canClose = false;
        public QuizForm()
        {
            InitializeComponent();

            this.Load += QuizForm_Load;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

            this.FormClosing += QuizForm_FormClosing;

            // Fix textbox
            txtAnswer.Enabled = false;
            txtAnswer.Enabled = true;

            txtAnswer.BackColor = System.Drawing.Color.White;
            txtAnswer.ForeColor = System.Drawing.Color.Black;
            txtAnswer.BorderStyle = BorderStyle.Fixed3D;
        }

        private void QuizForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true; // cegah form ditutup
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string answer = txtAnswer.Text.Trim().ToLower();

            if (answer == "r")
            {
                MessageBox.Show("Jawaban benar! Login Admin berhasil.");
                canClose = true;       // izinkan form ditutup
                this.Close();          // tutup form
            }
            else
            {
                MessageBox.Show("Jawaban salah! Anda harus membaca modul dulu.");

                ModuleForm module = new ModuleForm();
                module.TopMost = true;

                this.TopMost = false;
                this.Hide();

                module.Show();
            }
        }

        private void QuizForm_Load(object sender, EventArgs e)
        {
            // Posisi tengah layar
            lblQuestion.Left = (this.ClientSize.Width - lblQuestion.Width) / 2;
            lblQuestion.Top = 50;

            txtAnswer.Left = (this.ClientSize.Width - txtAnswer.Width) / 2;
            txtAnswer.Top = lblQuestion.Bottom + 20;

            btnSubmit.Left = (this.ClientSize.Width - btnSubmit.Width) / 2;
            btnSubmit.Top = txtAnswer.Bottom + 20;
        }

    }
}
