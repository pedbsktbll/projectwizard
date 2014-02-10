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
			SortedDictionary<string, Stream> wizFiles = GetResources("ProjectInstaller.Resources");
			string progFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
			string projDir = "\\VC\\vcprojects\\";
			string wizDir = projDir + "OSBWizard\\";

			string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 3) + "OSBWizard\\";

			// Delete old shit and re-create base directory
			Directory.Delete(baseDir, true);
			Directory.CreateDirectory(baseDir);

			// Clone ProjectWizardBins into C:\\OSBWizard\\ProjectWizardBins\\
			ProjectWizard.GitInterop git = new ProjectWizard.GitInterop(baseDir);
			git.Git_Clone(binsURL);
			bool installLocal = !Directory.Exists(baseDir + "ProjectWizardBins");

			// If  the clone failed....
			if( installLocal )
			{
				Directory.CreateDirectory(baseDir + "ProjectWizardBins");
				foreach( var kvp in wizFiles )
				{
					if( kvp.Key.StartsWith("OSBWizard") )
						continue;
					Stream output = File.OpenWrite(baseDir + "ProjectWizardBins\\" + kvp.Key);
					if( output != null )
					{
						kvp.Value.CopyTo(output);
						output.Close();
					}
				}
			}

			// FIGURED IT OUTTTTTTTT!!!!!!!!!!!!!!!!!!! From Visual Studio Command Prompt, run regasm ProjectWizard.dll / codebase C:\OSBWizard\ProjectWizard.dll
			// Additional we COULD OPTIONALLY add to GAC.. but I dont think this is necessary... I was playing around with: gacutil -i ProjectWizard.dll
			// gacutil -u ProjectWizard..... regasm ProjectWizard.dll /unregister

			Assembly asm = Assembly.LoadFrom(baseDir + "ProjectWizardBins\\ProjectWizard.dll");
			RegistrationServices regAsm = new RegistrationServices();
			bool bResult = regAsm.RegisterAssembly(asm, AssemblyRegistrationFlags.SetCodeBase);

			// Supports up to VS 19.0 :)
			StringBuilder vs = new StringBuilder(progFiles + "\\Microsoft Visual Studio 10.0");
			for( char i = '0'; i <= '9'; i++ )
			{
				vs[vs.Length - 3] = i;
				if( !Directory.Exists(vs.ToString()) )
					continue;
				Directory.CreateDirectory(vs.ToString() + wizDir);

				foreach( var kvp in wizFiles )
				{
					Stream output = File.OpenWrite( kvp.Key.StartsWith("OSBWizard") ? vs.ToString() + projDir + kvp.Key : vs.ToString() + wizDir + kvp.Key  );
					if( output != null )
					{
						kvp.Value.CopyTo(output);
						kvp.Value.Seek(0, SeekOrigin.Begin);
						output.Close();
					}
				}
			}

			foreach( var kvp in wizFiles )
				kvp.Value.Close();

			if( !installLocal )
				SchedulePersistence("\"" + git.gitPath() + "bin\\sh.exe\"", "-c \"git pull\"", baseDir + "ProjectWizardBins" );

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
	}
}
