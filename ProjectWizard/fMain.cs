using System;
using System.Windows.Forms;
using System.Drawing;

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
			// Set "previous" button to visible IFF previous exists
			bPrevious.Visible = bPrevious.Enabled = iCurIndex == 0 ? false : true;

			// Set all controls to invisible
            for( int i = 0; i < pucList.Length; i++ )
            {
                pucList[i].Visible = false;
                pucList[i].Enabled = false;
            }

			// if current index is the first uctypes control, let's reset the submodule picks
			if( iCurIndex == 0 )
			{
				( (ucSubmodules)pucList[1] ).resetSubs();
			}

			// if current index is Submodules:
			if( iCurIndex == 1 )
			{
				((ucSubmodules)pucList[1]).setRequiredSubmodule( "Dynamic Libraries" );
				if( ((ucType)pucList[0]).GetData().ProjectTemplate == ProjectType.WTLApp )
					((ucSubmodules)pucList[1]).setRequiredSubmodule("Windows Template Library (WTL)");
				if( ((ucType)pucList[0]).GetData().ProjectTemplate == ProjectType.SYSApp )
					((ucSubmodules)pucList[1]).setRequiredSubmodule("Kernel Libraries");
			}

            if( iCurIndex == 3 )
            {
                WizardData wz = new WizardData();
                wz.Type = ((ucType)pucList[0]).GetData();
                wz.SubmodulesAr = ((ucSubmodules)pucList[1]).GetData();
                wz.Author = ((ucAuthorBlock)pucList[2]).GetData();
                ((ucConfirmation)pucList[iCurIndex]).PrintResults(wz);
            }

			if( iCurIndex != 3 )
				pucList[iCurIndex].Parent.BackColor = Color.White;
            pucList[iCurIndex].Visible = true;
            pucList[iCurIndex].Enabled = true;

        }

        private bool ValidateUserControl()
        {
            return pucList[iCurIndex].ValidateData();
        }       

        private bool CheckExit()
        {
			return (MessageBox.Show("Wait!  Are you really sure you want to exit?  You won't have an awesome project to start with!", "Wait!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)  == DialogResult.OK ? true : false);
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
				MessageBox.Show( "Something is wrong!", "Oh, Snap!", MessageBoxButtons.OK, MessageBoxIcon.Stop );
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
