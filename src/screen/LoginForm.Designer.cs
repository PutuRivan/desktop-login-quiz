namespace LoginSystem
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // LoginForm
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "LoginForm";
            this.Text = "Login System";

            this.ResumeLayout(false);
        }
    }
}
