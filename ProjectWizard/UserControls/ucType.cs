using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectWizard
{
    public partial class ucType : UserControlEx
    {
        public ucType()
        {
            InitializeComponent();
        }

        public override bool ValidateData()
        {
            return true;
        }

        public WizData_Type GetData()
        {
            WizData_Type wz = new WizData_Type();
            var checkedButton = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (checkedButton == rbCUS)
            {
                wz.ProjectTemplate = "Something Else";
            }
            else
            {
                wz.ProjectTemplate = checkedButton.Text;
            }
            wz.MainLocation = txtMain.Text;
            wz.OriginLocation = txtRemote.Text;
            return wz;
        }

        private void rbChecked_Changed(object sender, EventArgs e)
        {
            if (rbCUS.Checked)
            {
                lblCustomProject.Enabled = true;
                clbCustomTemplates.Enabled = true;
            }
            else
            {
                lblCustomProject.Enabled = false;
                clbCustomTemplates.Enabled = false;
            }
        }
    }
}
