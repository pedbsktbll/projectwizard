namespace ProjectWizard
{
    partial class fMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbLeftBanner = new System.Windows.Forms.PictureBox();
            this.pBottom = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLeftBanner
            // 
            this.pbLeftBanner.Location = new System.Drawing.Point(0, 0);
            this.pbLeftBanner.Name = "pbLeftBanner";
            this.pbLeftBanner.Size = new System.Drawing.Size(150, 555);
            this.pbLeftBanner.TabIndex = 0;
            this.pbLeftBanner.TabStop = false;
            // 
            // pBottom
            // 
            this.pBottom.Location = new System.Drawing.Point(150, 500);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(685, 55);
            this.pBottom.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(150, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 500);
            this.panel1.TabIndex = 2;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 555);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pbLeftBanner);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "fMain";
            this.Text = "Project Creation Wizard";
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLeftBanner;
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Panel panel1;
    }
}

