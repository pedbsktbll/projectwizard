using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;
using System.Windows.Forms;

namespace ProjectWizard
{
	public enum ProjectType
	{
		ConsoleApp,
		DLLApp,
		GUIApp,
		LIBApp,
		SYSApp,
	}

    public class Wiz : IDTWizard
    {
		private _DTE dte = null;
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
			try
			{
				this.dte = (_DTE)Application;
				fMain f = new fMain((string)contextParams[1]);
				if( f.ShowDialog() == DialogResult.OK )
				{
					WizardData wz = f.GetWizardData();
					this.createProject(wz, true, "solution", "project", "C:\\somewhere\\", ProjectType.ConsoleApp);
					return;
				}
				else
				{
					return; //Handle canceling here....
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("Error", "Exception: " + ex.Message);
			}
        }

		// Main function that does all the work of setting up the project....
		// Template... how much of this stuff is needed and how much is contained in WizardData? Dunno, just memory dumping right nwo...
		private void createProject(WizardData wz, bool createNewSolution, string solutionName, string projectName, string path, ProjectType projectType)
		{
			switch( projectType )
			{
				// All the projects here
				case ProjectType.ConsoleApp: break;
				case ProjectType.DLLApp: break;
				case ProjectType.GUIApp: break;
				case ProjectType.LIBApp: break;
				case ProjectType.SYSApp: break;
			}

			// Create new solution if we need to....
			if( createNewSolution )
				this.dte.Solution.Create(path, solutionName);

			EnvDTE.Project project = this.dte.Solution.AddFromFile(path);

			CopyPropertySheets(path);
			AddProjectItems(project, path, projectType );
		}

		private void CopyPropertySheets(string path)
		{
		}

		private void AddProjectItems(EnvDTE.Project project, string path, ProjectType projectType)
		{
		}
    }
}
