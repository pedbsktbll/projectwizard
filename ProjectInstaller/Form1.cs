using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32.TaskScheduler;

namespace ProjectInstaller
{
	public class ProjInstaller
	{
		private static readonly string binsURL = "git@bitbucket.org:pedbsktbll/projectbin.git";

		public ProjInstaller()
		{
			string progFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
			string projDir = "\\VC\\vcprojects\\";
//			string wizDir = projDir + "OSBWizard\\";

			string baseDir = /*Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 3)*/progFiles + "\\OSBWizard\\";

			// Delete old shit and re-create base directory
			Console.WriteLine( "Creating OSBWizard directory in " + progFiles + "...");
			try
			{
//				Directory.Delete( baseDir + "ProjectWizardBins", true );
				DeleteDirectoryBecauseCSharpIsFuckingRetardedAndSucksAss( baseDir + "ProjectWizardBins", true );
			}
			catch( System.Exception /*e*/ ) { /*MessageBox.Show( e.Message );*/ }
			Directory.CreateDirectory( baseDir );

			// Dump Updater program:
			Console.WriteLine( "Dumping OSBWizardUpdater..." );
			Stream updaterProgStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ProjectInstaller.Resources.UpdateScript.ProjectWizardUpdater.exe");
			if( updaterProgStream != null )
			{
				Stream output = File.OpenWrite( baseDir + "ProjectWizardUpdater.exe" );
				if( output != null )
				{
					updaterProgStream.CopyTo( output );
					output.Close();
				}
				updaterProgStream.Close();
			}

			// Clone ProjectWizardBins into C:\\OSBWizard\\ProjectWizardBins\\
			Console.WriteLine( "Attempting to clone ProjectWizardBins..." );
			ProjectWizard.GitInterop git = new ProjectWizard.GitInterop(baseDir);
			git.Git_Clone(binsURL, baseDir + "ProjectWizardBins");
			bool installLocal = !Directory.Exists(baseDir + "ProjectWizardBins");

			// If  the clone failed....
			if( installLocal )
			{
				Console.WriteLine( "GIT has FAILED... resorting to internal backup copies..." );
				Directory.CreateDirectory(baseDir + "ProjectWizardBins");
				SortedDictionary<string, Stream> wizFiles = GetResources( "ProjectInstaller.Resources.ProjectWizardBins" );
				foreach( var kvp in wizFiles )
				{
					Stream output = File.OpenWrite(baseDir + "ProjectWizardBins\\" + kvp.Key);
					if( output != null )
					{
						kvp.Value.CopyTo(output);
						output.Close();
					}
					kvp.Value.Close();
				}
			}

			// FIGURED IT OUTTTTTTTT!!!!!!!!!!!!!!!!!!! From Visual Studio Command Prompt, run regasm ProjectWizard.dll / codebase C:\OSBWizard\ProjectWizard.dll
			// Additional we COULD OPTIONALLY add to GAC.. but I dont think this is necessary... I was playing around with: gacutil -i ProjectWizard.dll
			// gacutil -u ProjectWizard..... regasm ProjectWizard.dll /unregister

			Console.WriteLine( "Registering Assembly for access from Visual Studio..." );
			Assembly asm = Assembly.LoadFrom(baseDir + "ProjectWizardBins\\ProjectWizard.dll");
			RegistrationServices regAsm = new RegistrationServices();
			bool bResult = regAsm.RegisterAssembly(asm, AssemblyRegistrationFlags.SetCodeBase);

			// Supports up to VS 19.0 :)
			Console.WriteLine( "Adding VS Config files to all versions of VS from 10.0+..." );
			StringBuilder vs = new StringBuilder(progFiles + "\\Microsoft Visual Studio 10.0");
			for( char i = '0'; i <= '9'; i++ )
			{
				vs[vs.Length - 3] = i;
				if( !Directory.Exists(vs.ToString()) )
					continue;
//				Directory.CreateDirectory(vs.ToString() + wizDir);

				SortedDictionary<string, Stream> vsConfigFiles = GetResources( "ProjectInstaller.Resources.VS_Config" );
				foreach( var kvp in vsConfigFiles )
				{
					Stream output = File.OpenWrite( /*kvp.Key.StartsWith("OSBWizard") ? */vs.ToString() + projDir + kvp.Key /*: vs.ToString() + wizDir + kvp.Key*/  );
					if( output != null )
					{
						kvp.Value.CopyTo(output);
						kvp.Value.Seek(0, SeekOrigin.Begin);
						output.Close();
					}
					kvp.Value.Close();
				}
			}

//			foreach( var kvp in wizFiles )
//				kvp.Value.Close();

			if( !installLocal )
			{
				Console.WriteLine( "Creating update task..." );
				//				SchedulePersistence("\"" + git.gitPath() + "bin\\sh.exe\"", "-c \"git pull\"", baseDir + "ProjectWizardBins" );
				//				SchedulePersistence( "\"" + git.gitPath() + "bin\\sh.exe\"", "-c \"'" + git.gitPath() + "bin\\git.exe' pull\"", baseDir + "ProjectWizardBins" );
				//				SchedulePersistence( "cmd.exe", "/C \"" + baseDir + "ProjectWizardUpdater.exe -q\"", baseDir + "ProjectWizardBins" );
				SchedulePersistence( "\"" + baseDir + "ProjectWizardUpdater.exe\"", "-q", baseDir + "ProjectWizardBins" );
			}

			MessageBox.Show("Successfully loaded Wizard\n", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

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

		private bool SchedulePersistence(string path, string command, string workingDir)
		{
			// Get the service on the local machine
			using( TaskService ts = new TaskService() )
			{
				// Remove the task we just created
				//ts.RootFolder.DeleteTask( "OSB Project Wizard" );
				Task t = ts.FindTask( "OSB ProjectWizard" );
				if( t != null )
					ts.RootFolder.DeleteTask( "OSB ProjectWizard" );

				// Create a new task definition and assign properties
				TaskDefinition td = ts.NewTask();
				td.RegistrationInfo.Description = "OSB Project Wizard Updater";

				// Settings:
				td.Settings.ExecutionTimeLimit = TimeSpan.FromMinutes(5);

				// Security token: SYSTEM. We don't want command boxes popping and shit
//				td.Principal.LogonType = TaskLogonType.Group;//TaskLogonType.ServiceAccount;
				td.Principal.RunLevel = TaskRunLevel.Highest;

				// Create a trigger that will fire the task at this time every other day
//				td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });

				// Triggers every two days sometime between 0500 and 1000
				DailyTrigger dTrigger = (DailyTrigger)td.Triggers.Add(new DailyTrigger());
				dTrigger.StartBoundary = DateTime.Today + TimeSpan.FromHours(5);
				dTrigger.RandomDelay = TimeSpan.FromHours(5);
				dTrigger.DaysInterval = 2;

				// Trigger on logon
				td.Triggers.Add(new LogonTrigger());

				// Updated Project data:
				td.Actions.Add(new ExecAction(path, command, workingDir));

				// Register the task in the root folder
//				ts.RootFolder.RegisterTaskDefinition("OSB Project Wizard", td, TaskCreation.Create, "SYSTEM", null, TaskLogonType.ServiceAccount);
				ts.RootFolder.RegisterTaskDefinition( "OSB Project Wizard", td );
			}
			return true;
		}

		private static void DeleteDirectoryBecauseCSharpIsFuckingRetardedAndSucksAss(string path, bool recursive)
		{
			if( recursive )
			{
				var subDirs = Directory.GetDirectories( path );
				foreach( var s in subDirs )
					DeleteDirectoryBecauseCSharpIsFuckingRetardedAndSucksAss( s, true );
			}

			var files = Directory.GetFiles( path );
			foreach( var f in files )
			{
				var attr = File.GetAttributes( f );
				if( ( attr & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly )
					File.SetAttributes( f, attr ^ FileAttributes.ReadOnly );
				File.Delete( f );
			}
			Directory.Delete( path );
		}
	}
}
