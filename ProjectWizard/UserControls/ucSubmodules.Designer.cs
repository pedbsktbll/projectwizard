namespace ProjectWizard
{
    partial class ucSubmodules
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
            this.lblSubmodules = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bSelect = new System.Windows.Forms.Button();
            this.bRemoveAll = new System.Windows.Forms.Button();
            this.bRemove = new System.Windows.Forms.Button();
            this.bSelectAll = new System.Windows.Forms.Button();
            this.gbAdditionalInfo = new System.Windows.Forms.GroupBox();
            this.lnkConf = new System.Windows.Forms.LinkLabel();
            this.lnkJira = new System.Windows.Forms.LinkLabel();
            this.lnkStash = new System.Windows.Forms.LinkLabel();
            this.lblStash = new System.Windows.Forms.Label();
            this.lblJira = new System.Windows.Forms.Label();
            this.lblConf = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.pbAvailable = new ProjectWizard.PopBox();
            this.pbSelected = new ProjectWizard.PopBox();
            this.gbAdditionalInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSubmodules
            // 
            this.lblSubmodules.AutoSize = true;
            this.lblSubmodules.Location = new System.Drawing.Point(3, 3);
            this.lblSubmodules.Name = "lblSubmodules";
            this.lblSubmodules.Size = new System.Drawing.Size(229, 13);
            this.lblSubmodules.TabIndex = 0;
            this.lblSubmodules.Text = "Please select the desired submodules:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Available Submodules:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(383, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Selected Submodules:";
            // 
            // bSelect
            // 
            this.bSelect.Image = global::ProjectWizard.Properties.Resources.Arrow_right_icon;
            this.bSelect.Location = new System.Drawing.Point(320, 77);
            this.bSelect.Name = "bSelect";
            this.bSelect.Size = new System.Drawing.Size(40, 23);
            this.bSelect.TabIndex = 8;
            this.bSelect.UseVisualStyleBackColor = true;
            // 
            // bRemoveAll
            // 
            this.bRemoveAll.Image = global::ProjectWizard.Properties.Resources.Arrow_double_left_icon;
            this.bRemoveAll.Location = new System.Drawing.Point(320, 159);
            this.bRemoveAll.Name = "bRemoveAll";
            this.bRemoveAll.Size = new System.Drawing.Size(40, 23);
            this.bRemoveAll.TabIndex = 7;
            this.bRemoveAll.UseVisualStyleBackColor = true;
            // 
            // bRemove
            // 
            this.bRemove.Image = global::ProjectWizard.Properties.Resources.Arrow_left_icon;
            this.bRemove.Location = new System.Drawing.Point(320, 130);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(40, 23);
            this.bRemove.TabIndex = 6;
            this.bRemove.UseVisualStyleBackColor = true;
            // 
            // bSelectAll
            // 
            this.bSelectAll.Image = global::ProjectWizard.Properties.Resources.Arrow_double_right_icon1;
            this.bSelectAll.Location = new System.Drawing.Point(320, 48);
            this.bSelectAll.Name = "bSelectAll";
            this.bSelectAll.Size = new System.Drawing.Size(40, 23);
            this.bSelectAll.TabIndex = 5;
            this.bSelectAll.UseVisualStyleBackColor = true;
            // 
            // gbAdditionalInfo
            // 
            this.gbAdditionalInfo.Controls.Add(this.lnkConf);
            this.gbAdditionalInfo.Controls.Add(this.lnkJira);
            this.gbAdditionalInfo.Controls.Add(this.lnkStash);
            this.gbAdditionalInfo.Controls.Add(this.lblStash);
            this.gbAdditionalInfo.Controls.Add(this.lblJira);
            this.gbAdditionalInfo.Controls.Add(this.lblConf);
            this.gbAdditionalInfo.Controls.Add(this.lblDescription);
            this.gbAdditionalInfo.Controls.Add(this.txtDescription);
            this.gbAdditionalInfo.Location = new System.Drawing.Point(6, 198);
            this.gbAdditionalInfo.Name = "gbAdditionalInfo";
            this.gbAdditionalInfo.Size = new System.Drawing.Size(671, 324);
            this.gbAdditionalInfo.TabIndex = 9;
            this.gbAdditionalInfo.TabStop = false;
            this.gbAdditionalInfo.Text = "Additional Information on Highlighted Submodule:";
            // 
            // lnkConf
            // 
            this.lnkConf.AutoSize = true;
            this.lnkConf.Location = new System.Drawing.Point(55, 303);
            this.lnkConf.Name = "lnkConf";
            this.lnkConf.Size = new System.Drawing.Size(211, 13);
            this.lnkConf.TabIndex = 23;
            this.lnkConf.TabStop = true;
            this.lnkConf.Text = "www.confluence.com/stuff/morestuff";
            // 
            // lnkJira
            // 
            this.lnkJira.AutoSize = true;
            this.lnkJira.Location = new System.Drawing.Point(55, 285);
            this.lnkJira.Name = "lnkJira";
            this.lnkJira.Size = new System.Drawing.Size(175, 13);
            this.lnkJira.TabIndex = 22;
            this.lnkJira.TabStop = true;
            this.lnkJira.Text = "www.jira.com/stuff/morestuff";
            // 
            // lnkStash
            // 
            this.lnkStash.AutoSize = true;
            this.lnkStash.Location = new System.Drawing.Point(55, 267);
            this.lnkStash.Name = "lnkStash";
            this.lnkStash.Size = new System.Drawing.Size(181, 13);
            this.lnkStash.TabIndex = 21;
            this.lnkStash.TabStop = true;
            this.lnkStash.Text = "www.stash.com/stuff/morestuff";
            // 
            // lblStash
            // 
            this.lblStash.AutoSize = true;
            this.lblStash.Location = new System.Drawing.Point(6, 267);
            this.lblStash.Name = "lblStash";
            this.lblStash.Size = new System.Drawing.Size(43, 13);
            this.lblStash.TabIndex = 20;
            this.lblStash.Text = "Stash:";
            // 
            // lblJira
            // 
            this.lblJira.AutoSize = true;
            this.lblJira.Location = new System.Drawing.Point(6, 285);
            this.lblJira.Name = "lblJira";
            this.lblJira.Size = new System.Drawing.Size(37, 13);
            this.lblJira.TabIndex = 19;
            this.lblJira.Text = "Jira:";
            // 
            // lblConf
            // 
            this.lblConf.AutoSize = true;
            this.lblConf.Location = new System.Drawing.Point(6, 303);
            this.lblConf.Name = "lblConf";
            this.lblConf.Size = new System.Drawing.Size(37, 13);
            this.lblConf.TabIndex = 18;
            this.lblConf.Text = "Conf:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(6, 15);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(79, 13);
            this.lblDescription.TabIndex = 16;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.Enabled = false;
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(6, 31);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(659, 228);
            this.txtDescription.TabIndex = 17;
            this.txtDescription.Text = "Description goes here.";
            // 
            // pbAvailable
            // 
            this.pbAvailable.FormattingEnabled = true;
            this.pbAvailable.Location = new System.Drawing.Point(6, 48);
            this.pbAvailable.Name = "pbAvailable";
            this.pbAvailable.Size = new System.Drawing.Size(291, 134);
            this.pbAvailable.TabIndex = 10;
            // 
            // pbSelected
            // 
            this.pbSelected.FormattingEnabled = true;
            this.pbSelected.Location = new System.Drawing.Point(386, 48);
            this.pbSelected.Name = "pbSelected";
            this.pbSelected.Size = new System.Drawing.Size(291, 134);
            this.pbSelected.TabIndex = 11;
            // 
            // ucSubmodules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pbSelected);
            this.Controls.Add(this.pbAvailable);
            this.Controls.Add(this.gbAdditionalInfo);
            this.Controls.Add(this.bSelect);
            this.Controls.Add(this.bRemoveAll);
            this.Controls.Add(this.bRemove);
            this.Controls.Add(this.bSelectAll);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSubmodules);
            this.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.Name = "ucSubmodules";
            this.Size = new System.Drawing.Size(685, 525);
            this.gbAdditionalInfo.ResumeLayout(false);
            this.gbAdditionalInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSubmodules;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bSelectAll;
        private System.Windows.Forms.Button bRemove;
        private System.Windows.Forms.Button bRemoveAll;
        private System.Windows.Forms.Button bSelect;
        private System.Windows.Forms.GroupBox gbAdditionalInfo;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblStash;
        private System.Windows.Forms.Label lblJira;
        private System.Windows.Forms.Label lblConf;
        private System.Windows.Forms.LinkLabel lnkStash;
        private System.Windows.Forms.LinkLabel lnkConf;
        private System.Windows.Forms.LinkLabel lnkJira;
        private PopBox pbAvailable;
        private PopBox pbSelected;

    }
}
