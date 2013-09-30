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
            this.bPrevious = new System.Windows.Forms.Button();
            this.bNext = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pbLeftBanner = new System.Windows.Forms.PictureBox();
            this.ucType1 = new ProjectWizard.ucType();
            this.ucConfirmation1 = new ProjectWizard.ucConfirmation();
            this.ucAuthorBlock1 = new ProjectWizard.ucAuthorBlock();
            this.ucSubmodules1 = new ProjectWizard.ucSubmodules();
            this.pBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // pBottom
            // 
            this.pBottom.Controls.Add(this.bPrevious);
            this.pBottom.Controls.Add(this.bNext);
            this.pBottom.Controls.Add(this.button1);
            this.pBottom.Controls.Add(this.bCancel);
            this.pBottom.Location = new System.Drawing.Point(150, 525);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(685, 30);
            this.pBottom.TabIndex = 1;
            // 
            // bPrevious
            // 
            this.bPrevious.Enabled = false;
            this.bPrevious.Location = new System.Drawing.Point(437, 3);
            this.bPrevious.Name = "bPrevious";
            this.bPrevious.Size = new System.Drawing.Size(75, 23);
            this.bPrevious.TabIndex = 2;
            this.bPrevious.Text = "&Previous";
            this.bPrevious.UseVisualStyleBackColor = true;
            this.bPrevious.Visible = false;
            this.bPrevious.Click += new System.EventHandler(this.bPrevious_Click);
            // 
            // bNext
            // 
            this.bNext.Location = new System.Drawing.Point(518, 3);
            this.bNext.Name = "bNext";
            this.bNext.Size = new System.Drawing.Size(75, 23);
            this.bNext.TabIndex = 1;
            this.bNext.Text = "&Next";
            this.bNext.UseVisualStyleBackColor = true;
            this.bNext.Click += new System.EventHandler(this.bNext_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(599, 3);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 0;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
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
            // ucType1
            // 
            this.ucType1.BackColor = System.Drawing.Color.White;
            this.ucType1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucType1.Location = new System.Drawing.Point(150, 0);
            this.ucType1.Name = "ucType1";
            this.ucType1.Size = new System.Drawing.Size(685, 525);
            this.ucType1.TabIndex = 2;
            // 
            // ucConfirmation1
            // 
            this.ucConfirmation1.BackColor = System.Drawing.Color.White;
            this.ucConfirmation1.Enabled = false;
            this.ucConfirmation1.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.ucConfirmation1.Location = new System.Drawing.Point(150, 0);
            this.ucConfirmation1.Name = "ucConfirmation1";
            this.ucConfirmation1.Size = new System.Drawing.Size(685, 525);
            this.ucConfirmation1.TabIndex = 5;
            this.ucConfirmation1.Visible = false;
            // 
            // ucAuthorBlock1
            // 
            this.ucAuthorBlock1.BackColor = System.Drawing.Color.White;
            this.ucAuthorBlock1.Enabled = false;
            this.ucAuthorBlock1.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.ucAuthorBlock1.Location = new System.Drawing.Point(150, 0);
            this.ucAuthorBlock1.Name = "ucAuthorBlock1";
            this.ucAuthorBlock1.Size = new System.Drawing.Size(685, 525);
            this.ucAuthorBlock1.TabIndex = 4;
            this.ucAuthorBlock1.Visible = false;
            // 
            // ucSubmodules1
            // 
            this.ucSubmodules1.BackColor = System.Drawing.Color.White;
            this.ucSubmodules1.Enabled = false;
            this.ucSubmodules1.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.ucSubmodules1.Location = new System.Drawing.Point(150, 0);
            this.ucSubmodules1.Name = "ucSubmodules1";
            this.ucSubmodules1.Size = new System.Drawing.Size(685, 525);
            this.ucSubmodules1.TabIndex = 3;
            this.ucSubmodules1.Visible = false;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(836, 555);
            this.Controls.Add(this.pBottom);
            this.Controls.Add(this.pbLeftBanner);
            this.Controls.Add(this.ucConfirmation1);
            this.Controls.Add(this.ucAuthorBlock1);
            this.Controls.Add(this.ucSubmodules1);
            this.Controls.Add(this.ucType1);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fMain";
            this.Text = "Project Creation Wizard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.pBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLeftBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLeftBanner;
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Button bPrevious;
        private System.Windows.Forms.Button bNext;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button button1;
        private ucType ucType1;
        private ucSubmodules ucSubmodules1;
        private ucAuthorBlock ucAuthorBlock1;
        private ucConfirmation ucConfirmation1;

    }
}

