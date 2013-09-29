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
            this.pBottom = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pbLeftBanner = new System.Windows.Forms.PictureBox();
            this.pBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // pBottom
            // 
            this.pBottom.Controls.Add(this.button3);
            this.pBottom.Controls.Add(this.button2);
            this.pBottom.Controls.Add(this.bCancel);
            this.pBottom.Controls.Add(this.button1);
            this.pBottom.Location = new System.Drawing.Point(150, 525);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(685, 30);
            this.pBottom.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(437, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "&Back";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(518, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "&Next";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(599, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 0;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(518, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "C&reate";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pbLeftBanner
            // 
            this.pbLeftBanner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLeftBanner.Image = global::ProjectWizard.Properties.Resources.temp;
            this.pbLeftBanner.Location = new System.Drawing.Point(0, 0);
            this.pbLeftBanner.Name = "pbLeftBanner";
            this.pbLeftBanner.Size = new System.Drawing.Size(150, 555);
            this.pbLeftBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLeftBanner.TabIndex = 0;
            this.pbLeftBanner.TabStop = false;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(836, 555);
            this.ControlBox = false;
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pbLeftBanner);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "fMain";
            this.Text = "Project Creation Wizard";
            this.pBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLeftBanner;
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button button1;
    }
}

