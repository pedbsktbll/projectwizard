using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;
using System.Windows.Forms;
using System.IO;

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
		//TODO: Most or all of these may be within WizardData. Haven't looked much into what all info you grab
		protected _DTE dte = null;
		protected WizardData wz = null;
		protected string solutionName = null;
		protected string projectName = null;
		protected string path = null;
		protected string solutionPath = null;
		protected string projectPath = null;
		protected ProjectType projectType;
		protected bool createNewSolution = false;

        // Execute is the main entry point for a project wizard.  It has to follow this template.
		// contextParams:
		// 0: some GUID
		// 1: Project Name
		// 2: Project Path
		// 3: location of visual studio exe
		// 4: Create New Solution : true..... Add to existing solution: false
		// 5: Solution Name--- Will be empty string if not selected to create solution
		// 6: false
		// 7: "4.0"
        public void Execute(object Application, int hwndOwner, ref object[] contextParams, ref object[] customParams, ref EnvDTE.wizardResult retval)
        {
			try
			{
				fMain f = new fMain((string)contextParams[1]);
				if( f.ShowDialog() == DialogResult.OK )
				{
					// Set all our member variables based on input:
					//TODO: Organize and validate input
					this.dte = (_DTE)Application;
					this.wz = f.GetWizardData();
					this.solutionName = (string) contextParams[5];
					this.projectName = (string) contextParams[1];
					this.path = (string) contextParams[2];
					this.projectType = ProjectType.ConsoleApp;
					this.createNewSolution = (bool)contextParams[4];

					// Parse project path and solution path from "path"
					//TODO: Can "path" be null/empty?
					this.projectPath = this.path;
					if( this.createNewSolution )
						this.solutionPath = path.Substring(0, this.path.Length - this.projectName.Length - 1);
					else
						this.solutionPath = Path.GetDirectoryName(this.dte.Solution.FullName);

					// Create a Project based on all our input:
					retval = createProject() ? wizardResult.wizardResultSuccess : wizardResult.wizardResultFailure;
				}
				else
				{
					retval = wizardResult.wizardResultCancel;
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("Exception: " + ex.Message, "Error");
				retval = wizardResult.wizardResultBackOut;
//				Environment.Exit(-1);
			}
        }

		// Main function that does all the work of setting up the project....
		// Template... how much of this stuff is needed and how much is contained in WizardData? Dunno, just memory dumping right nwo...
		private bool createProject()
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
			if( this.createNewSolution )
				this.dte.Solution.Create(this.solutionPath, this.solutionName);

			// Create project dir, stage .vcxproj and .filters
			CopyProjFiles();

			// Using EnvDTE, create the VS project...
			EnvDTE.Project project = this.dte.Solution.AddFromFile(this.path);

			// Copy all the required property sheets into solutiondir/props
			CopyPropertySheets();

			// Copy all project items (source and header files) into project
			AddProjectItems();

			return true;
		}

		//TODO: STUB
		private bool CopyProjFiles()
		{
			// Copy everything from ProjectWizard.Resources.proj into new project at $(ProjectDir).
			return true;
		}

		//TODO: STUB
		private bool CopyPropertySheets()
		{
			// We somehow need to copy everything from ProjectWizard.Resources.props into the new solution at $(SolutionDir)props.
			// What if already exists? Should we overwrite? This may be more difficult than it appears...
			return true;
		}

		//TODO: STUB
		private bool AddProjectItems()
		{
			// Copy everything from ProjectWizard.Resources.base into new project at $(ProjectDir).
			return true;
		}
    }
}
