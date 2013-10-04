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
        private UserControlEx[] pucList;
        private int iCurIndex;
        private bool bOverrideExit;

        public fMain()
        {
            InitializeComponent();

            //Order the user controls will show up in.
            pucList = new UserControlEx[4];
            pucList[0] = ucType1;
            pucList[1] = ucSubmodules1;
            pucList[2] = ucAuthorBlock1;
            pucList[3] = ucConfirmation1;

            iCurIndex = 0;
            bOverrideExit = true;
        }

        private void PaintPanels()
        {
            switch (iCurIndex)
            {
                case 0:
                    bPrevious.Visible = false;
                    bPrevious.Enabled = false;
                    break;
                default:
                    bPrevious.Visible = true;
                    bPrevious.Enabled = true;
                    break;
            }

            for (int i = 0; i < pucList.Length; i++)
            {
                pucList[i].Visible = false;
                pucList[i].Enabled = false;
            }
            pucList[iCurIndex].Visible = true;
            pucList[iCurIndex].Enabled = true;

        }

        private bool ValidateUserControl()
        {
            return pucList[iCurIndex].ValidateData();
        }       

        private bool CheckExit()
        {
            if( DialogResult.OK == MessageBox.Show("Wait!  Are you really sure you want to exit?  You won't have an awesome project to start with!", "Wait!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                return true;
            }

            return false;
        }
              
        private void bNext_Click(object sender, EventArgs e)
        {
            WizardData wz = new WizardData();
            wz.Type = ucType1.GetData();
            if (ValidateUserControl())
            {
                iCurIndex++;
                if (iCurIndex == pucList.Length)
                {
                    bOverrideExit = false;
                    this.Close();
                }
                else
                    PaintPanels();
            }
            else
            {
                MessageBox.Show("Something is wrong!", "Oh, Snap!");
            }
        }

        private void bPrevious_Click(object sender, EventArgs e)
        {
            iCurIndex--;
            PaintPanels();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && bOverrideExit)
            {
                if (!CheckExit())
                    e.Cancel = true;
            }
        }

    }
}
