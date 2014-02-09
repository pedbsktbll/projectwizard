using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.VCCodeModel;
using Microsoft.Win32;

namespace ProjectWizard
{
	public enum ProjectType
	{
		ConsoleApp = 0,
		WINApp = 1,
		WTLApp = 2,
		DLLApp = 3,
		LIBApp = 4,
		SYSApp = 5,
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

		public static readonly string[] ProjectTypeStrings = new string[6]
		{
			"ConsoleApp",
			"WINApp",
			"WTLApp",
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
			}
        }

		// Main function that does all the work of setting up the project....
		protected bool createProject()
		{
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

			// If .gitignore file doesn't exist, create it: ... This should probably be a function, meh...
			if( !File.Exists(solutionPath + "\\.gitignore") )
			{
				string gitResource = "ProjectWizard.Resources.base..gitignore";
				SortedDictionary<string, Stream> gitIgnore = GetResources(gitResource);
//				var/*Stream*/ gigIgnoreFile = gitIgnore.Values.Last();

				foreach( var kvp in gitIgnore )
				{
					Stream output = File.OpenWrite(solutionPath + "\\.gitignore");
					if( output != null )
					{
						kvp.Value.CopyTo(output);
						output.Close();
					}
					kvp.Value.Close();
				}
			}

			// Copy all the required property sheets into solutiondir/props
			CopyPropertySheets();

			// Copy all the libs required for Dynamic building if they don't already exist
//			CopyDynamicLibs();

			// Create project dir, stage .vcxproj and .filters
			CopyProjFiles();

			// Using EnvDTE, create the VS project...
			EnvDTE.Project project = this.dte.Solution.AddFromFile(this.projectPath + "\\" + this.projectName + ".vcxproj");

			// Copy all project items (source and header files) into project
			AddProjectItems();


			Microsoft.VisualStudio.VCProjectEngine.VCProject proj;
			Microsoft.VisualStudio.VCProjectEngine.VCCLCompilerTool compilerTool;
			Microsoft.VisualStudio.VCProjectEngine.IVCCollection toolsCollection;
			Microsoft.VisualStudio.VCProjectEngine.IVCCollection configurationsCollection;
			
			proj = (Microsoft.VisualStudio.VCProjectEngine.VCProject)project.Object;
			configurationsCollection = (Microsoft.VisualStudio.VCProjectEngine.IVCCollection)proj.Configurations;
			
			foreach( Microsoft.VisualStudio.VCProjectEngine.VCConfiguration configuration in configurationsCollection )
			{
				toolsCollection = (Microsoft.VisualStudio.VCProjectEngine.IVCCollection)configuration.Tools;
				compilerTool = toolsCollection.Item("VCCLCompilerTool");
				compilerTool.AdditionalIncludeDirectories += "$(Submodules)Dynamic_Libs/blah/bah";

				Microsoft.VisualStudio.VCProjectEngine.VCLinkerTool linkerTool = toolsCollection.Item("VCLinkerTool");
				linkerTool.AdditionalLibraryDirectories += "$(Libs)Bin/location/here";

// 				foreach( Object toolObject in toolsCollection) 
// 				{
// 					if( toolObject is Microsoft.VisualStudio.VCProjectEngine.VCCLCompilerTool )
// 					{
// 						compilerTool = (Microsoft.VisualStudio.VCProjectEngine.VCCLCompilerTool)toolObject;
// //						MessageBox.Show(configuration.Name + ": " + compilerTool.AdditionalIncludeDirectories);
// 						compilerTool.AdditionalIncludeDirectories += "$(Submodules)Dynamic_Libs/blah/bah";
// 
// 						break;
// 					}
// 					if( toolObject is Microsoft.VisualStudio.VCProjectEngine.VCCL)
// 				}
			}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////
//         Configuration config;
//         EnvDTE.Properties configProps;
//         Property prop;
//         config = project.ConfigurationManager.ActiveConfiguration;
//         configProps = config.Properties;
//         prop = configProps.Item("PlatformTarget");
//         MessageBox.Show("The platform target for this project is: " + prop.Value.ToString());
//         prop = configProps.Item("WarningLevel");
//         MessageBox.Show
// ("The warning level for this project is set to: " + prop.Value.ToString());
//         MessageBox.Show("Changing the warning level to 3...");
//         prop.Value = "3";
//         MessageBox.Show
// ("The warning level for this project is now set to: " + prop.Value.ToString());

/////////////////////////////////////////////////////////////////////////////////////////////////////////////


// 			foreach( object projConfigName in (object[])project.ConfigurationManager.ConfigurationRowNames )
// 			{
// 				Configurations projConfigs = project.ConfigurationManager.ConfigurationRow(projConfigName.ToString());
// 				foreach( Configuration config in projConfigs )
// 				{
// 					EnvDTE.Properties configProps = config.Properties;
// //					Property prop = configProps.Item("PlatformTarget");
// 					Property prop = configProps.Item("WarningLevel");
// 					prop.Value = "1";
// 				}
// 			}

			//Let's add submodules now and other git stuff!
			GitInterop git = new GitInterop(solutionPath);

			// Git exist?
			if( !git.gitExists() )
			{
				MessageBox.Show("Cannot find Git; No Git functions can be performed.", "Git Error");
				return true;
			}

			// First let's see if git already exists in the solution:
			bool gitRepo = Directory.Exists(this.solutionPath + "\\.git");

			// Only init the Repo if not already a git repo, though I guess it doesn't really matter.
			if( !gitRepo )
				git.init();

			AddGitSubmodules(git);

			// save all proj and sol settings 

			// finally commit and push to git
			// Add origin location if provided
			if( wz.Type.OriginLocation != "" )
				git.Remote_Add(wz.Type.OriginLocation);
			git.Git_Add("--all");
//			git.Git_Add("./Libs/Dynamic_Libs/* --all --force");
			git.Git_Commit(gitRepo ? "Added Project " + this.projectName : "Initial commit by Project Wizard.");

			return true;
		}

		//TODO: We probably need try/catch around all the stream shit in the following functions just in case it isnt writable...
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

// 		protected bool CopyDynamicLibs()
// 		{
// 			string libResource = "ProjectWizard.Resources.libs.Dynamic_Libs";
// 			string destination = this.solutionPath + "\\Libs\\Dynamic_Libs\\";
// 
// 			// Create Dynamic Libs directory hierarchy:
// 			Directory.CreateDirectory(destination);
// 			Directory.CreateDirectory(destination + "amd64");
// 			Directory.CreateDirectory(destination + "i386");
// 
// 			SortedDictionary<string, Stream> dynLibs = GetResources(libResource);
// 			foreach( var kvp in dynLibs )
// 			{
// 				StringBuilder lib = new StringBuilder(kvp.Key);
// 				lib[lib.ToString().IndexOf('.')] = '\\';
// 
// 				// Don't overwrite existing libs... just cuz they're big and in case the user has replaced them or something
// 				if( File.Exists(destination + lib.ToString()) )
// 				{
// 					kvp.Value.Close();
// 					continue;
// 				}
// 
// 				Stream output = File.OpenWrite(destination + lib.ToString());
// 				if( output != null )
// 				{
// 					kvp.Value.CopyTo(output);
// 					output.Close();
// 				}
// 				kvp.Value.Close();
// 			}
// 			return true;
// 		}

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

		protected bool AddProjectItems()
		{
			// Copy everything from ProjectWizard.Resources.proj into new project at $(ProjectDir).
			string srcResource = "ProjectWizard.Resources.base." + ProjectTypeStrings[(int)this.projectType];
			string destination = this.projectPath;

			SortedDictionary<string, Stream> srcFiles = GetResources(srcResource);
			foreach( var kvp in srcFiles )
			{
				Stream output = File.OpenWrite(destination + "\\" + ParseData(kvp.Key));
				if( output != null )
				{
					// Yeahhh... sooo binary files like the .ico bitmaps dont like to be treated as strings...
					if( !kvp.Key.EndsWith(".ico") )
					{
						StreamReader reader = new StreamReader(kvp.Value);
						string srcFile = ParseData(reader.ReadToEnd());
						reader.Close();

						StreamWriter writer = new StreamWriter(output);
						writer.Write(srcFile);
						writer.Close();
					}
					else
						kvp.Value.CopyTo(output);
					output.Close();
				}
				kvp.Value.Close();
			}
			return true;
		}

		private bool AddGitSubmodules(GitInterop git)
		{
			try
			{
				StringBuilder incHeader = new StringBuilder();
				foreach( var item in wz.SubmodulesAr )
				{
					string path = @"./Submodules/" + item.Repo_Name;
					if( !Directory.Exists(solutionPath + "\\Submodules\\" + item.Repo_Name) )
					{
						// may be cool to add some progress bars or something here...
						if( !git.Submodule_Add(item.Location, path) )
							MessageBox.Show("Submodule " + item.Name + " failed to clone", "Error Adding Submodule");
					}

//                 foreach (var str in item.IncludeStrAr)
//                 {
//                     incHeader.Append(str + "\r\n");
//                 }
				}
			}
			catch( System.Exception ex )
			{
				MessageBox.Show("Error adding files to Git: " + ex.Message, "Git Error");
				return false;
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
					dict.Add(name.Substring(filter.Length < name.Length ? filter.Length + 1 : 0), stream);
			}
			return dict;
		}

		// This function will parse a given string and replace all modifiable data with user-input
		// from the wizard.
		private string ParseData(string dataFile)
		{
			string retVal = dataFile.Replace("_____FILENAME_____", this.projectName);//wz.Author.ProjectName);
			retVal = retVal.Replace("_____USER_____", wz.Author.Author);
			retVal = retVal.Replace("_____DATE_____", DateTime.Now.ToString("M/d/yyyy"));
			retVal = retVal.Replace("_____DESCRIPTION_____", wz.Author.Description.Replace("\r\n", "\r\n * "));
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
