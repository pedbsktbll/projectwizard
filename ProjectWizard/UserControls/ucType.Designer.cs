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
            this.gbProject = new System.Windows.Forms.GroupBox();
            this.lblStandardProject = new System.Windows.Forms.Label();
            this.lvCustomTemplates = new System.Windows.Forms.ListView();
            this.rbWIN = new System.Windows.Forms.RadioButton();
            this.rbCON = new System.Windows.Forms.RadioButton();
            this.rbSYS = new System.Windows.Forms.RadioButton();
            this.lblCustomProject = new System.Windows.Forms.Label();
            this.rbLIB = new System.Windows.Forms.RadioButton();
            this.rbCUS = new System.Windows.Forms.RadioButton();
            this.rbDLL = new System.Windows.Forms.RadioButton();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtRemote = new System.Windows.Forms.TextBox();
            this.lblRemote = new System.Windows.Forms.Label();
            this.txtMain = new System.Windows.Forms.TextBox();
            this.lblMain = new System.Windows.Forms.Label();
            this.txtWelcome = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.ProjectType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbProject.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbProject
            // 
            this.gbProject.Controls.Add(this.lblStandardProject);
            this.gbProject.Controls.Add(this.lvCustomTemplates);
            this.gbProject.Controls.Add(this.rbWIN);
            this.gbProject.Controls.Add(this.rbCON);
            this.gbProject.Controls.Add(this.rbSYS);
            this.gbProject.Controls.Add(this.lblCustomProject);
            this.gbProject.Controls.Add(this.rbLIB);
            this.gbProject.Controls.Add(this.rbCUS);
            this.gbProject.Controls.Add(this.rbDLL);
            this.gbProject.Location = new System.Drawing.Point(6, 55);
            this.gbProject.Name = "gbProject";
            this.gbProject.Size = new System.Drawing.Size(675, 174);
            this.gbProject.TabIndex = 1;
            this.gbProject.TabStop = false;
            this.gbProject.Text = "Project Type";
            // 
            // lblStandardProject
            // 
            this.lblStandardProject.AutoSize = true;
            this.lblStandardProject.Location = new System.Drawing.Point(6, 16);
            this.lblStandardProject.Name = "lblStandardProject";
            this.lblStandardProject.Size = new System.Drawing.Size(109, 13);
            this.lblStandardProject.TabIndex = 0;
            this.lblStandardProject.Text = "Standard Project:";
            // 
            // lvCustomTemplates
            // 
            this.lvCustomTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProjectType});
            this.lvCustomTemplates.Enabled = false;
            this.lvCustomTemplates.Location = new System.Drawing.Point(254, 31);
            this.lvCustomTemplates.Name = "lvCustomTemplates";
            this.lvCustomTemplates.Size = new System.Drawing.Size(415, 133);
            this.lvCustomTemplates.TabIndex = 8;
            this.lvCustomTemplates.UseCompatibleStateImageBehavior = false;
            this.lvCustomTemplates.View = System.Windows.Forms.View.Details;
            this.lvCustomTemplates.SelectedIndexChanged += new System.EventHandler(this.lvCustomTemplates_SelectedIndexChanged);
            this.lvCustomTemplates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            // 
            // rbWIN
            // 
            this.rbWIN.AutoSize = true;
            this.rbWIN.Location = new System.Drawing.Point(9, 55);
            this.rbWIN.Name = "rbWIN";
            this.rbWIN.Size = new System.Drawing.Size(175, 17);
            this.rbWIN.TabIndex = 2;
            this.rbWIN.Text = "Windows Application (exe)";
            this.rbWIN.UseVisualStyleBackColor = true;
            this.rbWIN.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // rbCON
            // 
            this.rbCON.AutoSize = true;
            this.rbCON.Checked = true;
            this.rbCON.Location = new System.Drawing.Point(9, 32);
            this.rbCON.Name = "rbCON";
            this.rbCON.Size = new System.Drawing.Size(175, 17);
            this.rbCON.TabIndex = 1;
            this.rbCON.TabStop = true;
            this.rbCON.Text = "Console Application (exe)";
            this.rbCON.UseVisualStyleBackColor = true;
            this.rbCON.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // rbSYS
            // 
            this.rbSYS.AutoSize = true;
            this.rbSYS.Location = new System.Drawing.Point(9, 124);
            this.rbSYS.Name = "rbSYS";
            this.rbSYS.Size = new System.Drawing.Size(139, 17);
            this.rbSYS.TabIndex = 5;
            this.rbSYS.Text = "Native Driver (sys)";
            this.rbSYS.UseVisualStyleBackColor = true;
            this.rbSYS.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // lblCustomProject
            // 
            this.lblCustomProject.AutoSize = true;
            this.lblCustomProject.Enabled = false;
            this.lblCustomProject.Location = new System.Drawing.Point(251, 16);
            this.lblCustomProject.Name = "lblCustomProject";
            this.lblCustomProject.Size = new System.Drawing.Size(157, 13);
            this.lblCustomProject.TabIndex = 7;
            this.lblCustomProject.Text = "Custom Project Templates:";
            // 
            // rbLIB
            // 
            this.rbLIB.AutoSize = true;
            this.rbLIB.Location = new System.Drawing.Point(9, 101);
            this.rbLIB.Name = "rbLIB";
            this.rbLIB.Size = new System.Drawing.Size(145, 17);
            this.rbLIB.TabIndex = 4;
            this.rbLIB.Text = "Static Library (lib)";
            this.rbLIB.UseVisualStyleBackColor = true;
            this.rbLIB.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // rbCUS
            // 
            this.rbCUS.AutoSize = true;
            this.rbCUS.Location = new System.Drawing.Point(9, 147);
            this.rbCUS.Name = "rbCUS";
            this.rbCUS.Size = new System.Drawing.Size(163, 17);
            this.rbCUS.TabIndex = 6;
            this.rbCUS.Text = "Custom Project Template";
            this.rbCUS.UseVisualStyleBackColor = true;
            this.rbCUS.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // rbDLL
            // 
            this.rbDLL.AutoSize = true;
            this.rbDLL.Location = new System.Drawing.Point(9, 78);
            this.rbDLL.Name = "rbDLL";
            this.rbDLL.Size = new System.Drawing.Size(151, 17);
            this.rbDLL.TabIndex = 3;
            this.rbDLL.Text = "Dynamic Library (dll)";
            this.rbDLL.UseVisualStyleBackColor = true;
            this.rbDLL.CheckedChanged += new System.EventHandler(this.rbChecked_Changed);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 242);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(181, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description of Selected Type:";
            // 
            // txtRemote
            // 
            this.txtRemote.Location = new System.Drawing.Point(6, 502);
            this.txtRemote.Name = "txtRemote";
            this.txtRemote.Size = new System.Drawing.Size(675, 20);
            this.txtRemote.TabIndex = 7;
            // 
            // lblRemote
            // 
            this.lblRemote.AutoSize = true;
            this.lblRemote.Location = new System.Drawing.Point(3, 485);
            this.lblRemote.Name = "lblRemote";
            this.lblRemote.Size = new System.Drawing.Size(295, 13);
            this.lblRemote.TabIndex = 6;
            this.lblRemote.Text = "Optional location of Origin remote (i.e. Stash):";
            // 
            // txtMain
            // 
            this.txtMain.Location = new System.Drawing.Point(6, 452);
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(278, 20);
            this.txtMain.TabIndex = 5;
            this.txtMain.Text = "ProjectName.cpp";
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.Location = new System.Drawing.Point(3, 435);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(163, 13);
            this.lblMain.TabIndex = 4;
            this.lblMain.Text = "Name of the main cpp file:";
            // 
            // txtWelcome
            // 
            this.txtWelcome.BackColor = System.Drawing.Color.White;
            this.txtWelcome.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWelcome.ForeColor = System.Drawing.Color.Black;
            this.txtWelcome.Location = new System.Drawing.Point(3, 3);
            this.txtWelcome.Multiline = true;
            this.txtWelcome.Name = "txtWelcome";
            this.txtWelcome.Size = new System.Drawing.Size(678, 46);
            this.txtWelcome.TabIndex = 0;
            this.txtWelcome.TabStop = false;
            this.txtWelcome.Text = resources.GetString("txtWelcome.Text");
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.Enabled = false;
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(6, 258);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(675, 162);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.Text = "Description goes here.";
            // 
            // ProjectType
            // 
            this.ProjectType.Text = "ProjectType";
            this.ProjectType.Width = 385;
            // 
            // ucType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gbProject);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtRemote);
            this.Controls.Add(this.lblRemote);
            this.Controls.Add(this.txtMain);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.txtWelcome);
            this.Controls.Add(this.txtDescription);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucType";
            this.Size = new System.Drawing.Size(685, 525);
            this.gbProject.ResumeLayout(false);
            this.gbProject.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWelcome;
        private System.Windows.Forms.Label lblStandardProject;
        private System.Windows.Forms.Label lblCustomProject;
        private System.Windows.Forms.RadioButton rbCON;
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
        private System.Windows.Forms.ListView lvCustomTemplates;
        private System.Windows.Forms.RadioButton rbWIN;
        private System.Windows.Forms.GroupBox gbProject;
        private System.Windows.Forms.ColumnHeader ProjectType;



    }
}
