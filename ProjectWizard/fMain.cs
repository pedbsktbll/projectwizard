using System;
using System.Windows.Forms;

namespace ProjectWizard
{
    public partial class fMain : Form
    {
        private UserControlEx[] pucList;
        private int iCurIndex;
        private bool bOverrideExit;

        public fMain(string ProjectName)
        {
            InitializeComponent();
            ucType1.SetProjectName(ProjectName);
            ucAuthorBlock1.SetProjectName(ProjectName);

            //Order the user controls will show up in.
            pucList = new UserControlEx[4];
            pucList[0] = ucType1;
            pucList[1] = ucSubmodules1;
            pucList[2] = ucAuthorBlock1;
            pucList[3] = ucConfirmation1;        

            iCurIndex = 0;
            bOverrideExit = true;
            ucType1.Enabled = true;
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

            for( int i = 0; i < pucList.Length; i++ )
            {
                pucList[i].Visible = false;
                pucList[i].Enabled = false;
            }

			if( iCurIndex == 1 )
				((ucSubmodules)pucList[1]).setRequiredSubmodule("Dynamic Libraries");

			if( iCurIndex == 1 && ((ucType)pucList[0]).GetData().ProjectTemplate == 2 )
				((ucSubmodules)pucList[1]).setRequiredSubmodule("Windows Template Library (WTL)");

            if( iCurIndex == 3 )
            {
                WizardData wz = new WizardData();
                wz.Type = ((ucType)pucList[0]).GetData();
                wz.SubmodulesAr = ((ucSubmodules)pucList[1]).GetData();
                wz.Author = ((ucAuthorBlock)pucList[2]).GetData();
                ((ucConfirmation)pucList[iCurIndex]).PrintResults(wz);
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
            if (ValidateUserControl())
            {
                iCurIndex++;
                if (iCurIndex == pucList.Length)
                {
                    bOverrideExit = false;
                    this.DialogResult = DialogResult.OK;
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
                else
                    this.DialogResult = DialogResult.Cancel;
            }
        }

        public WizardData GetWizardData()
        {
            WizardData wz = new WizardData();
            wz.Type = ucType1.GetData();
            wz.SubmodulesAr = ucSubmodules1.GetData();
            wz.Author = ucAuthorBlock1.GetData();

            return wz;
        }
    }
}
