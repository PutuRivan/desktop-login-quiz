using System;
using System.Windows.Forms;

namespace LoginSystem
{
    public partial class LoginForm : Form
    {
        private bool canClose = false; // flag untuk membolehkan close

        public LoginForm()
        {
            InitializeComponent();

            // FULLSCREEN MODE
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

            // Hilangkan Close/Minimize/Maximize
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Blok ALT + F4 kecuali jika canClose = true
            this.FormClosing += LoginForm_FormClosing;
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true; // cegah close manual
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            this.Hide();                   // Sembunyikan LoginForm
            QuizForm quiz = new QuizForm();
            quiz.Show();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Login sebagai User berhasil!");
            canClose = true;               // izinkan form ditutup
            this.Close();                  // tutup form → aplikasi ikut keluar
        }
    }
}
