namespace ProjectWizard
{
    partial class ucType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucType));
            this.txtWelcome = new System.Windows.Forms.TextBox();
            this.lblStandardProject = new System.Windows.Forms.Label();
            this.lblCustomProject = new System.Windows.Forms.Label();
            this.rbEXE = new System.Windows.Forms.RadioButton();
            this.rbDLL = new System.Windows.Forms.RadioButton();
            this.rbLIB = new System.Windows.Forms.RadioButton();
            this.rbSYS = new System.Windows.Forms.RadioButton();
            this.rbCUS = new System.Windows.Forms.RadioButton();
            this.lblMain = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtMain = new System.Windows.Forms.TextBox();
            this.txtRemote = new System.Windows.Forms.TextBox();
            this.lblRemote = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.clbCustomTemplates = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // txtWelcome
            // 
            this.txtWelcome.BackColor = System.Drawing.Color.White;
            this.txtWelcome.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWelcome.Enabled = false;
            this.txtWelcome.ForeColor = System.Drawing.Color.Black;
            this.txtWelcome.Location = new System.Drawing.Point(3, 3);
            this.txtWelcome.Multiline = true;
            this.txtWelcome.Name = "txtWelcome";
            this.txtWelcome.Size = new System.Drawing.Size(678, 55);
            this.txtWelcome.TabIndex = 0;
            this.txtWelcome.Text = resources.GetString("txtWelcome.Text");
            // 
            // lblStandardProject
            // 
            this.lblStandardProject.AutoSize = true;
            this.lblStandardProject.Location = new System.Drawing.Point(3, 61);
            this.lblStandardProject.Name = "lblStandardProject";
            this.lblStandardProject.Size = new System.Drawing.Size(109, 13);
            this.lblStandardProject.TabIndex = 1;
            this.lblStandardProject.Text = "Standard Project:";
            // 
            // lblCustomProject
            // 
            this.lblCustomProject.AutoSize = true;
            this.lblCustomProject.Enabled = false;
            this.lblCustomProject.Location = new System.Drawing.Point(257, 61);
            this.lblCustomProject.Name = "lblCustomProject";
            this.lblCustomProject.Size = new System.Drawing.Size(157, 13);
            this.lblCustomProject.TabIndex = 2;
            this.lblCustomProject.Text = "Custom Project Templates:";
            // 
            // rbEXE
            // 
            this.rbEXE.AutoSize = true;
            this.rbEXE.Checked = true;
            this.rbEXE.Location = new System.Drawing.Point(6, 78);
            this.rbEXE.Name = "rbEXE";
            this.rbEXE.Size = new System.Drawing.Size(121, 17);
            this.rbEXE.TabIndex = 3;
            this.rbEXE.TabStop = true;
            this.rbEXE.Text = "Executable (exe)";
            this.rbEXE.UseVisualStyleBackColor = true;
            this.rbEXE.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // rbDLL
            // 
            this.rbDLL.AutoSize = true;
            this.rbDLL.Location = new System.Drawing.Point(6, 101);
            this.rbDLL.Name = "rbDLL";
            this.rbDLL.Size = new System.Drawing.Size(151, 17);
            this.rbDLL.TabIndex = 4;
            this.rbDLL.TabStop = true;
            this.rbDLL.Text = "Dynamic Library (dll)";
            this.rbDLL.UseVisualStyleBackColor = true;
            this.rbDLL.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // rbLIB
            // 
            this.rbLIB.AutoSize = true;
            this.rbLIB.Location = new System.Drawing.Point(6, 124);
            this.rbLIB.Name = "rbLIB";
            this.rbLIB.Size = new System.Drawing.Size(145, 17);
            this.rbLIB.TabIndex = 5;
            this.rbLIB.TabStop = true;
            this.rbLIB.Text = "Static Library (lib)";
            this.rbLIB.UseVisualStyleBackColor = true;
            this.rbLIB.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // rbSYS
            // 
            this.rbSYS.AutoSize = true;
            this.rbSYS.Location = new System.Drawing.Point(6, 147);
            this.rbSYS.Name = "rbSYS";
            this.rbSYS.Size = new System.Drawing.Size(139, 17);
            this.rbSYS.TabIndex = 6;
            this.rbSYS.TabStop = true;
            this.rbSYS.Text = "Native Driver (sys)";
            this.rbSYS.UseVisualStyleBackColor = true;
            this.rbSYS.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // rbCUS
            // 
            this.rbCUS.AutoSize = true;
            this.rbCUS.Location = new System.Drawing.Point(6, 170);
            this.rbCUS.Name = "rbCUS";
            this.rbCUS.Size = new System.Drawing.Size(163, 17);
            this.rbCUS.TabIndex = 7;
            this.rbCUS.TabStop = true;
            this.rbCUS.Text = "Custom Project Template";
            this.rbCUS.UseVisualStyleBackColor = true;
            this.rbCUS.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.Location = new System.Drawing.Point(3, 435);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(163, 13);
            this.lblMain.TabIndex = 10;
            this.lblMain.Text = "Name of the main cpp file:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 200);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(181, 13);
            this.lblDescription.TabIndex = 10;
            this.lblDescription.Text = "Description of Selected Type:";
            // 
            // txtMain
            // 
            this.txtMain.Location = new System.Drawing.Point(6, 452);
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(278, 20);
            this.txtMain.TabIndex = 12;
            this.txtMain.Text = "ProjectName.cpp";
            // 
            // txtRemote
            // 
            this.txtRemote.Location = new System.Drawing.Point(6, 502);
            this.txtRemote.Name = "txtRemote";
            this.txtRemote.Size = new System.Drawing.Size(675, 20);
            this.txtRemote.TabIndex = 14;
            // 
            // lblRemote
            // 
            this.lblRemote.AutoSize = true;
            this.lblRemote.Location = new System.Drawing.Point(3, 485);
            this.lblRemote.Name = "lblRemote";
            this.lblRemote.Size = new System.Drawing.Size(295, 13);
            this.lblRemote.TabIndex = 13;
            this.lblRemote.Text = "Optional location of Origin remote (i.e. Stash):";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.Enabled = false;
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(6, 216);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(675, 206);
            this.txtDescription.TabIndex = 15;
            this.txtDescription.Text = "Description goes here.";
            // 
            // clbCustomTemplates
            // 
            this.clbCustomTemplates.Enabled = false;
            this.clbCustomTemplates.FormattingEnabled = true;
            this.clbCustomTemplates.Location = new System.Drawing.Point(260, 78);
            this.clbCustomTemplates.Name = "clbCustomTemplates";
            this.clbCustomTemplates.Size = new System.Drawing.Size(421, 109);
            this.clbCustomTemplates.TabIndex = 16;
            // 
            // ucType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.clbCustomTemplates);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtRemote);
            this.Controls.Add(this.lblStandardProject);
            this.Controls.Add(this.lblCustomProject);
            this.Controls.Add(this.lblRemote);
            this.Controls.Add(this.txtMain);
            this.Controls.Add(this.rbEXE);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.rbDLL);
            this.Controls.Add(this.txtWelcome);
            this.Controls.Add(this.rbCUS);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.rbLIB);
            this.Controls.Add(this.rbSYS);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucType";
            this.Size = new System.Drawing.Size(685, 525);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWelcome;
        private System.Windows.Forms.Label lblStandardProject;
        private System.Windows.Forms.Label lblCustomProject;
        private System.Windows.Forms.RadioButton rbEXE;
        private System.Windows.Forms.RadioButton rbDLL;
        private System.Windows.Forms.RadioButton rbLIB;
        private System.Windows.Forms.RadioButton rbSYS;
        private System.Windows.Forms.RadioButton rbCUS;
        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.TextBox txtMain;
        private System.Windows.Forms.TextBox txtRemote;
        private System.Windows.Forms.Label lblRemote;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.CheckedListBox clbCustomTemplates;



    }
}
