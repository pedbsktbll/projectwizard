using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;
using System.Windows.Forms;

namespace ProjectWizard
{
    public class Wiz : IDTWizard
    {

        /// <summary>
        /// Execute is the main entry point for a project wizard.  It has to follow this template.
        /// </summary>
        /// <param name="Application"></param>
        /// <param name="hwndOwner"></param>
        /// <param name="contextParams"></param>
        /// <param name="customParams"></param>
        /// <param name="retval"></param>
        /// <returns></returns>
        public void Execute(object Application, int hwndOwner, ref object[] contextParams, ref object[] customParams, ref EnvDTE.wizardResult retval)
        {
            fMain f = new fMain((string)contextParams[1]);
            if (f.ShowDialog() == DialogResult.OK)
            {
                WizardData wz = f.GetWizardData();
				this.createProject(wz, true, "solution", "project", "C:\\somewhere\\");
                //Do stuff
                return;
            }
            else
            {
                return; //Handle canceling here....
            }

        }

		// Template... how much of this stuff is needed and how much is contained in WizardData? Dunno, just memory dumping right nwo...
		private void createProject(WizardData wz, bool createNewSolution, string solutionName, string projectName, string path)
		{
			switch( projectName )
			{
				// All the projects here
			}

			// Create new solution if we need to....
			if( createNewSolution )
				;

//			CopyPropertySheets();
//			AddProjectItems();
		}
    }
}
