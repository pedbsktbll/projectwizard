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
		// Basic class variables for object Wizard
		protected _DTE dte = null;
		protected WizardData wz = null;
		protected string solutionName = null;
		protected string projectName = null;
		protected string path = null;
		protected ProjectType projectType;

        // Execute is the main entry point for a project wizard.  It has to follow this template.
        public void Execute(object Application, int hwndOwner, ref object[] contextParams, ref object[] customParams, ref EnvDTE.wizardResult retval)
        {
			try
			{
				fMain f = new fMain((string)contextParams[1]);
				if( f.ShowDialog() == DialogResult.OK )
				{
					// Set all our member variables based on input:
					this.dte = (_DTE)Application;
					this.wz = f.GetWizardData();
					this.solutionName = "solutionName";
					this.projectName = "projectName";
					this.path = "C:\\somewhere\\";
					this.projectType = ProjectType.ConsoleApp;

					// Create a Project based on all our input:
					retval = createProject(true) ? wizardResult.wizardResultSuccess : wizardResult.wizardResultFailure;
				}
				else
				{
					retval = wizardResult.wizardResultCancel;
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("Error", "Exception: " + ex.Message);
				retval = wizardResult.wizardResultBackOut;
				Environment.Exit(-1);
			}
        }

		// Main function that does all the work of setting up the project....
		// Template... how much of this stuff is needed and how much is contained in WizardData? Dunno, just memory dumping right nwo...
		private bool createProject(bool createNewSolution)
		{
			switch( this.projectType )
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
				this.dte.Solution.Create(this.path, this.solutionName);

			EnvDTE.Project project = this.dte.Solution.AddFromFile(this.path);

			CopyPropertySheets();
			AddProjectItems();
			return true;
		}

		private bool CopyPropertySheets()
		{
			return true;
		}

		private bool AddProjectItems()
		{
			return true;
		}
    }
}
