using System.Diagnostics;
using System.IO;
using System;
using Microsoft.Win32;

namespace ProjectWizard
{
	public class GitInterop
	{
		private string gitInstallPath;
		private Process Proc = new Process();
		private ProcessStartInfo ProcInfo = new ProcessStartInfo();
		private StreamWriter inputWriter;
		private StreamReader errorReader;
		private StreamReader outputReader;

		public GitInterop(string workingDirectory)
		{
			gitInstallPath = Environment.Is64BitOperatingSystem ?
			   (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Git_is1",
			   "InstallLocation", @"C:\Program Files (x86)\Git\bin\sh.exe") :
			   (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Git_is1",
			   "IntsllLocation", @"C:\Program Files\Git\bin\sh.exe");

			ProcInfo.FileName = gitInstallPath + "bin\\sh.exe";
			ProcInfo.Arguments = "--login -i";
			ProcInfo.RedirectStandardInput = true;
			ProcInfo.RedirectStandardError = true;
			ProcInfo.RedirectStandardOutput = true;
			ProcInfo.UseShellExecute = false;
			ProcInfo.CreateNoWindow = true;
			ProcInfo.WorkingDirectory = workingDirectory;
			Proc.StartInfo = ProcInfo;
		}

		~GitInterop()
		{
			Proc.Close();
		}

		public bool gitExists()
		{
			return File.Exists(ProcInfo.FileName);
		}

		public string gitPath()
		{
			return ProcInfo.FileName;
		}

		public bool init()
		{
			Proc.Start();
			inputWriter = Proc.StandardInput;
			errorReader = Proc.StandardError;
			outputReader = Proc.StandardOutput;
			inputWriter.WriteLine("git init");
			inputWriter.Flush();
			inputWriter.WriteLine("exit");
			inputWriter.Flush();
			Proc.WaitForExit();
			return true;
		}

		public bool Remote_Add(string remotePath, string remoteName = "origin")
		{
			Proc.Start();
			inputWriter = Proc.StandardInput;
			errorReader = Proc.StandardError;
			outputReader = Proc.StandardOutput;
			inputWriter.WriteLine("git remote add {0} {1}", remoteName, remotePath);
			inputWriter.Flush();
			inputWriter.WriteLine("exit");
			inputWriter.Flush();
			Proc.WaitForExit();
			return true;
		}

		public bool Submodule_Add(string submoduleAddress, string submodulePath)
		{
			Proc.Start();
			inputWriter = Proc.StandardInput;
			errorReader = Proc.StandardError;
			outputReader = Proc.StandardOutput;
			inputWriter.WriteLine("git submodule add {0} {1}", submoduleAddress, submodulePath);
			inputWriter.Flush();
			inputWriter.WriteLine("exit");
			inputWriter.Flush();
			Proc.WaitForExit();

			// If failed? ... Try https?
			if( !Directory.Exists(submodulePath) && submoduleAddress.StartsWith("git@") )
			{
				string url = "https://" + submoduleAddress.Substring(submoduleAddress.IndexOf("@") + 1,
					submoduleAddress.IndexOf(":") - submoduleAddress.IndexOf("@") - 1) + "/" +
					submoduleAddress.Substring(submoduleAddress.IndexOf(":") + 1);
				if( !url.EndsWith(".git") )
					url += ".git";

				Proc.Start();
				inputWriter = Proc.StandardInput;
				errorReader = Proc.StandardError;
				outputReader = Proc.StandardOutput;
				inputWriter.WriteLine("git submodule add {0} {1}", url, submodulePath);
				inputWriter.Flush();
				inputWriter.WriteLine("exit");
				inputWriter.Flush();
				Proc.WaitForExit();

			}

			// Still fails? IDK, try using different keys?
			// 			if( !Directory.Exists(submodulePath) && Directory.Exists(Environment.SpecialFolder.Personal + ".ssh") )
			// 			{
			// 
			// 			}

			return Directory.Exists(submodulePath);
		}

		public bool Git_Add(string args)
		{
			Proc.Start();
			inputWriter = Proc.StandardInput;
			errorReader = Proc.StandardError;
			outputReader = Proc.StandardOutput;
			inputWriter.WriteLine("git add {0}", args);
			inputWriter.Flush();
			inputWriter.WriteLine("exit");
			inputWriter.Flush();
			Proc.WaitForExit();
			return true;
		}

		public bool Git_Commit(string message)
		{
			Proc.Start();
			inputWriter = Proc.StandardInput;
			errorReader = Proc.StandardError;
			outputReader = Proc.StandardOutput;
			inputWriter.WriteLine("git commit -m \"{0}\"", message);
			inputWriter.Flush();
			inputWriter.WriteLine("exit");
			inputWriter.Flush();
			Proc.WaitForExit();
			return true;
		}

		public bool Git_Clone(string repo)
		{
			Proc.Start();
			inputWriter = Proc.StandardInput;
			errorReader = Proc.StandardError;
			outputReader = Proc.StandardOutput;
			inputWriter.WriteLine("git clone -q {0}", repo);
			inputWriter.Flush();
			inputWriter.WriteLine("exit");
			inputWriter.Flush();
			Proc.WaitForExit();
			return true;
		}

// 		public bool Git_Pull()
// 		{
// 			Proc.Start();
// 			inputWriter = Proc.StandardInput;
// 			errorReader = Proc.StandardError;
// 			outputReader = Proc.StandardOutput;
// 			inputWriter.WriteLine("git commit -m \"{0}\"", message);
// 			inputWriter.Flush();
// 			inputWriter.WriteLine("exit");
// 			inputWriter.Flush();
// 			Proc.WaitForExit();
// 			return true;
// 		}
	}
}