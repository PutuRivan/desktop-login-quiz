namespace LoginSystem
{
    partial class QuizForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblQuestion;
        private TextBox txtAnswer;
        private Button btnSubmit;

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
            this.lblQuestion = new System.Windows.Forms.Label();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Location = new System.Drawing.Point(20, 20);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(182, 16);
            this.lblQuestion.Text = "Apa kepanjangan dari CPU?";
            // 
            // txtAnswer
            // 
            this.txtAnswer.Location = new System.Drawing.Point(23, 55);
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(250, 22);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(23, 95);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(250, 35);
            this.btnSubmit.Text = "Kirim Jawaban";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // QuizForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 160);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.btnSubmit);
            this.Name = "QuizForm";
            this.Text = "Admin Quiz";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
