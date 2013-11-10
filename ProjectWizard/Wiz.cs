using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace ProjectWizard
{
	public enum ProjectType
	{
		ConsoleApp = 0,
		GUIApp = 1,
		DLLApp = 2,
		LIBApp = 3,
		SYSApp = 4,
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

		public static readonly string[] ProjectTypeStrings = new string[5]
		{
			"ConsoleApp",
			"GUIApp",
			"DLLApp",
			"LIBApp",
			"SYSApp",
		};

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
					this.projectType = (ProjectType) wz.Type.ProjectTemplate;
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
		protected bool createProject()
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
			{
                Directory.CreateDirectory(solutionPath);
				this.dte.Solution.Create(this.solutionPath, this.solutionName);
                this.dte.Solution.SaveAs(this.solutionName);
			}

			// Create custom directories:
			Directory.CreateDirectory(solutionPath + "\\BIN");
			Directory.CreateDirectory(solutionPath + "\\Shared");
			Directory.CreateDirectory(solutionPath + "\\Libs");
			Directory.CreateDirectory(solutionPath + "\\Submodules");

			// Copy all the required property sheets into solutiondir/props
			CopyPropertySheets();

			// Create project dir, stage .vcxproj and .filters
			CopyProjFiles();

			// Using EnvDTE, create the VS project...
			EnvDTE.Project project = this.dte.Solution.AddFromFile(this.projectPath + "\\" + this.projectName + ".vcxproj");

			// Copy all project items (source and header files) into project
			AddProjectItems();

			return true;
		}

		//TODO: Make props hidden?
		protected bool CopyPropertySheets()
		{
			string propResource = "ProjectWizard.Resources.props";
			string destination = this.solutionPath + "\\props\\";

			// Create property sheet directory hierarchy:
			Directory.CreateDirectory(destination + "\\internal");

			SortedDictionary<string, Stream> propSheets = GetResources(propResource);
			foreach( var kvp in propSheets )
			{
				StringBuilder propSheet = new StringBuilder(kvp.Key);
				if( propSheet.ToString().StartsWith("internal") )
					propSheet[propSheet.ToString().IndexOf('.')] = '\\';

				Stream output = File.OpenWrite(destination + propSheet.ToString());
				if( output != null )
				{
					kvp.Value.CopyTo(output);
					output.Close();
				}
				kvp.Value.Close();

			}
			return true;
		}

		//TODO: Customize project settings based on user input as well as creating the unique GUIDs and other customizations
		protected bool CopyProjFiles()
		{
			// Copy everything from ProjectWizard.Resources.proj into new project at $(ProjectDir).
			string projResource = "ProjectWizard.Resources.proj." + ProjectTypeStrings[(int)this.projectType];
			string destination = this.projectPath;

			// Create VS Project directory hierarchy:
			Directory.CreateDirectory(this.projectPath);

			SortedDictionary<string, Stream> projFiles = GetResources(projResource);
			foreach( var kvp in projFiles )
			{
				Stream output = File.OpenWrite(destination + "\\" + this.projectName + kvp.Key.Substring(kvp.Key.IndexOf('.')));
				if( output != null )
				{
					StreamReader reader = new StreamReader(kvp.Value);
					string projFile = ParseData(reader.ReadToEnd());
					reader.Close();

					StreamWriter writer = new StreamWriter(output);
					writer.Write(projFile);
					writer.Close();

					output.Close();
				}
				kvp.Value.Close();
			}
			return true;
		}

		//TODO: Customize...
		protected bool AddProjectItems()
		{
			// Copy everything from ProjectWizard.Resources.proj into new project at $(ProjectDir).
			string srcResource = "ProjectWizard.Resources.base." + ProjectTypeStrings[(int)this.projectType];
			string destination = this.projectPath;

			SortedDictionary<string, Stream> srcFiles = GetResources(srcResource);
			foreach( var kvp in srcFiles )
			{
//				Stream output = File.OpenWrite(destination + "\\" + kvp.Key);//this.projectName + kvp.Key.Substring(kvp.Key.IndexOf('.')));
				Stream output = File.OpenWrite(destination + "\\" + ParseData(kvp.Key));
				if( output != null )
				{
					StreamReader reader = new StreamReader(kvp.Value);
					string srcFile = ParseData(reader.ReadToEnd());
					reader.Close();

					StreamWriter writer = new StreamWriter(output);
					writer.Write(srcFile);
					writer.Close();

					output.Close();
				}
				kvp.Value.Close();
			}
			return true;
		}

		// Helper functions:

		// This function will return a filtered list of embedded resources to do work on.
		// The SortedDictionary is to match the relative name to the associated resource data stream.
		private SortedDictionary<string, Stream> GetResources(string filter)
		{
			SortedDictionary<string, Stream> dict = new SortedDictionary<string, Stream>();

			Assembly assembly = Assembly.GetExecutingAssembly();
			foreach( string name in assembly.GetManifestResourceNames() )
			{
				if( !name.StartsWith(filter) )
					continue;

				Stream stream = assembly.GetManifestResourceStream(name);
				if( stream != null )
					dict.Add(name.Substring(filter.Length + 1), stream);
			}
			return dict;
		}

		// This function will parse a given string and replace all modifiable data with user-input
		// from the wizard.
		private string ParseData(string dataFile)
		{
			string retVal = dataFile.Replace("_____FILENAME_____", wz.Author.ProjectName);
			retVal = retVal.Replace("_____USER_____", wz.Author.Author);
			retVal = retVal.Replace("_____DATE_____", DateTime.Now.ToString("M/d/yyyy"));
			retVal = retVal.Replace("_____DESCRIPTION_____", wz.Author.Description);
			retVal = retVal.Replace("_____VERSION_____", wz.Author.Version);

			// vcxproj specific stuff:
			string guid = "<ProjectGuid>";
			string guidEnd = "</ProjectGuid>";
			string rName = "<RootNamespace>";
			string rNameEnd = "</RootNamespace>";
			if( retVal.Contains(guid) )
				retVal = retVal.Replace(retVal.Substring(retVal.IndexOf(guid) + guid.Length, retVal.IndexOf(guidEnd) - retVal.IndexOf(guid) - guid.Length), Guid.NewGuid().ToString().ToUpper());
			if( retVal.Contains(rName) )
				retVal = retVal.Replace(retVal.Substring(retVal.IndexOf(rName) + rName.Length, retVal.IndexOf(rNameEnd) - retVal.IndexOf(rName) - rName.Length), wz.Author.ProjectName);
			return retVal;
		}
    }
}
