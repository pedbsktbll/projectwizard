using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectWizard
{
    public partial class fMain : Form
    {
        //Order the user controls will show up in.
        private UserControlEx[] pucList;

        public fMain()
        {
            InitializeComponent();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CheckExit()
        {
            if( DialogResult.OK == MessageBox.Show("Wait!  Are you really sure you want to exit?  You won't have an awesome project to start with!", "Wait!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                return true;
            }

            return false;
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ( e.CloseReason == CloseReason.UserClosing )
            {
                if(!CheckExit()) 
                    e.Cancel = true;
            }
        }
    }
}
