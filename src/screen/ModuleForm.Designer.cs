namespace LoginSystem
{
    partial class ModuleForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Text = "Quiz";
            this.ClientSize = new System.Drawing.Size(800, 450);
        }
    }
}
