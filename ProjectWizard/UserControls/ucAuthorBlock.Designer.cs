namespace ProjectWizard
{
    partial class ucAuthorBlock
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
            this.lblIntro = new System.Windows.Forms.Label();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.txtProjectNumber = new System.Windows.Forms.TextBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.lblProjectNumber = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.nudVersion = new System.Windows.Forms.NumericUpDown();
            this.txtOffice = new System.Windows.Forms.TextBox();
            this.lblOffice = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtEmployeeID = new System.Windows.Forms.TextBox();
            this.lblEmployeeID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIntro
            // 
            this.lblIntro.AutoSize = true;
            this.lblIntro.Location = new System.Drawing.Point(3, 3);
            this.lblIntro.Name = "lblIntro";
            this.lblIntro.Size = new System.Drawing.Size(361, 13);
            this.lblIntro.TabIndex = 0;
            this.lblIntro.Text = "Please fill out the below information for the author block:";
            // 
            // lblProjectName
            // 
            this.lblProjectName.AutoSize = true;
            this.lblProjectName.Location = new System.Drawing.Point(3, 28);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(85, 13);
            this.lblProjectName.TabIndex = 1;
            this.lblProjectName.Text = "Project Name:";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Location = new System.Drawing.Point(6, 44);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(304, 20);
            this.txtProjectName.TabIndex = 2;
            // 
            // txtProjectNumber
            // 
            this.txtProjectNumber.Location = new System.Drawing.Point(378, 44);
            this.txtProjectNumber.Name = "txtProjectNumber";
            this.txtProjectNumber.Size = new System.Drawing.Size(304, 20);
            this.txtProjectNumber.TabIndex = 4;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(375, 77);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(61, 13);
            this.lblCustomer.TabIndex = 7;
            this.lblCustomer.Text = "Customer:";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(378, 93);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(304, 20);
            this.txtCustomer.TabIndex = 8;
            this.txtCustomer.Text = "NONE";
            // 
            // lblProjectNumber
            // 
            this.lblProjectNumber.AutoSize = true;
            this.lblProjectNumber.Location = new System.Drawing.Point(375, 28);
            this.lblProjectNumber.Name = "lblProjectNumber";
            this.lblProjectNumber.Size = new System.Drawing.Size(97, 13);
            this.lblProjectNumber.TabIndex = 3;
            this.lblProjectNumber.Text = "Project Number:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(3, 77);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(55, 13);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "Version:";
            // 
            // nudVersion
            // 
            this.nudVersion.DecimalPlaces = 1;
            this.nudVersion.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudVersion.Location = new System.Drawing.Point(6, 93);
            this.nudVersion.Name = "nudVersion";
            this.nudVersion.Size = new System.Drawing.Size(120, 20);
            this.nudVersion.TabIndex = 6;
            this.nudVersion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtOffice
            // 
            this.txtOffice.Location = new System.Drawing.Point(378, 142);
            this.txtOffice.Name = "txtOffice";
            this.txtOffice.Size = new System.Drawing.Size(304, 20);
            this.txtOffice.TabIndex = 12;
            this.txtOffice.Text = "STD";
            // 
            // lblOffice
            // 
            this.lblOffice.AutoSize = true;
            this.lblOffice.Location = new System.Drawing.Point(375, 126);
            this.lblOffice.Name = "lblOffice";
            this.lblOffice.Size = new System.Drawing.Size(49, 13);
            this.lblOffice.TabIndex = 11;
            this.lblOffice.Text = "Office:";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(6, 142);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(304, 20);
            this.txtAuthor.TabIndex = 10;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(3, 126);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(241, 13);
            this.lblAuthor.TabIndex = 9;
            this.lblAuthor.Text = "Author(s) (Use comma if more than one):";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 175);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(79, 13);
            this.lblDescription.TabIndex = 13;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(6, 191);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(676, 282);
            this.txtDescription.TabIndex = 14;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(6, 502);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(304, 20);
            this.txtStatus.TabIndex = 16;
            this.txtStatus.Text = "BETA";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(3, 486);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(43, 13);
            this.lblStatus.TabIndex = 15;
            this.lblStatus.Text = "Satus:";
            // 
            // txtEmployeeID
            // 
            this.txtEmployeeID.Location = new System.Drawing.Point(378, 502);
            this.txtEmployeeID.Name = "txtEmployeeID";
            this.txtEmployeeID.Size = new System.Drawing.Size(304, 20);
            this.txtEmployeeID.TabIndex = 18;
            this.txtEmployeeID.Text = "0000000";
            // 
            // lblEmployeeID
            // 
            this.lblEmployeeID.AutoSize = true;
            this.lblEmployeeID.Location = new System.Drawing.Point(375, 486);
            this.lblEmployeeID.Name = "lblEmployeeID";
            this.lblEmployeeID.Size = new System.Drawing.Size(79, 13);
            this.lblEmployeeID.TabIndex = 17;
            this.lblEmployeeID.Text = "Employee ID:";
            // 
            // ucAuthorBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtEmployeeID);
            this.Controls.Add(this.lblEmployeeID);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.txtOffice);
            this.Controls.Add(this.lblOffice);
            this.Controls.Add(this.nudVersion);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.lblProjectNumber);
            this.Controls.Add(this.txtProjectNumber);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.txtProjectName);
            this.Controls.Add(this.lblProjectName);
            this.Controls.Add(this.lblIntro);
            this.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.Name = "ucAuthorBlock";
            this.Size = new System.Drawing.Size(685, 525);
            ((System.ComponentModel.ISupportInitialize)(this.nudVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIntro;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.TextBox txtProjectNumber;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label lblProjectNumber;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.NumericUpDown nudVersion;
        private System.Windows.Forms.TextBox txtOffice;
        private System.Windows.Forms.Label lblOffice;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtEmployeeID;
        private System.Windows.Forms.Label lblEmployeeID;

    }
}
