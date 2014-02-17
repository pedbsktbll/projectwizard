namespace ProjectWizard
{
    partial class ucConfirmation
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.txtConfirm = new System.Windows.Forms.RichTextBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.lblConfirm = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtConfirm
			// 
			this.txtConfirm.BackColor = System.Drawing.Color.White;
			this.txtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtConfirm.ForeColor = System.Drawing.Color.Black;
			this.txtConfirm.Location = new System.Drawing.Point(9, 29);
			this.txtConfirm.Name = "txtConfirm";
			this.txtConfirm.Size = new System.Drawing.Size(1012, 732);
			this.txtConfirm.TabIndex = 1;
			this.txtConfirm.Text = "";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(661, 113);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(8, 8);
			this.richTextBox1.TabIndex = 2;
			this.richTextBox1.Text = "";
			// 
			// lblConfirm
			// 
			this.lblConfirm.AutoSize = true;
			this.lblConfirm.Location = new System.Drawing.Point(4, 4);
			this.lblConfirm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblConfirm.Name = "lblConfirm";
			this.lblConfirm.Size = new System.Drawing.Size(504, 19);
			this.lblConfirm.TabIndex = 0;
			this.lblConfirm.Text = "Please confirm the below information before proceeding:";
			// 
			// ucConfirmation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.txtConfirm);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.lblConfirm);
			this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ucConfirmation";
			this.Size = new System.Drawing.Size(685, 525);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Label lblConfirm;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.RichTextBox txtConfirm;

    }
}
