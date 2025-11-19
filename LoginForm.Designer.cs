namespace LoginSystem
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnAdmin;
        private Button btnUser;

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
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // btnAdmin
            this.btnAdmin.Location = new System.Drawing.Point(50, 30);
            this.btnAdmin.Size = new System.Drawing.Size(200, 40);
            this.btnAdmin.Text = "Login sebagai Admin";
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);

            // btnUser
            this.btnUser.Location = new System.Drawing.Point(50, 90);
            this.btnUser.Size = new System.Drawing.Size(200, 40);
            this.btnUser.Text = "Login sebagai User";
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);

            // LoginForm
            this.ClientSize = new System.Drawing.Size(300, 170);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnUser);
            this.Text = "Login System";

            this.ResumeLayout(false);
        }
    }
}
