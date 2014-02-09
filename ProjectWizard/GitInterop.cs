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
		private string workingDirectory;

        public GitInterop(string workingDirectory)
        {
			gitInstallPath = Environment.Is64BitOperatingSystem ?
			   (string)Registry.GetValue( @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Git_is1",
			   "InstallLocation", @"C:\Program Files (x86)\Git\" ) :
			   (string)Registry.GetValue( @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Git_is1",
			   "IntsllLocation", @"C:\Program Files\Git\" );

			ProcInfo.FileName = gitInstallPath + "bin\\sh.exe";
            ProcInfo.Arguments = "--login -i";
            ProcInfo.RedirectStandardInput = true;
            ProcInfo.RedirectStandardError = true;
            ProcInfo.RedirectStandardOutput = true;
            ProcInfo.UseShellExecute = false;
            ProcInfo.CreateNoWindow = true;
            ProcInfo.WorkingDirectory = this.workingDirectory = workingDirectory;
            Proc.StartInfo = ProcInfo;
        }

        ~GitInterop()
        {
            Proc.Close();
        }

		public bool gitExists()
		{
			return File.Exists(gitInstallPath + "bin\\sh.exe");
		}

		public string gitPath()
		{
			return gitInstallPath;
		}

		private void startProc(string args, bool createWindow = false)
		{
			Process myProc = new Process();
			ProcessStartInfo myProcInfo = new ProcessStartInfo();

			myProcInfo.FileName = gitInstallPath + "bin\\sh.exe";
			myProcInfo.Arguments = args;
			myProcInfo.RedirectStandardInput = !createWindow;
			myProcInfo.RedirectStandardError = !createWindow;
			myProcInfo.RedirectStandardOutput = !createWindow;
			myProcInfo.UseShellExecute = false;
			myProcInfo.CreateNoWindow = !createWindow;
			myProcInfo.WorkingDirectory = workingDirectory;
			myProc.StartInfo = myProcInfo;

			myProc.Start();
			myProc.WaitForExit();
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

        public bool Submodule_Add(string submoduleAddress, string submodulePath, string fullPath)
        {
			startProc( String.Format( "-c \"git submodule add {0} {1}\"", submoduleAddress, submodulePath), true );

			// If failed? ... Try https?
			if( !Directory.Exists(fullPath) && (submoduleAddress.StartsWith("git") || submoduleAddress.StartsWith("ssh")) )
			{
				// Attempt #1:
				string url = "https://" + submoduleAddress.Substring( submoduleAddress.IndexOf( "@" ) + 1,
					submoduleAddress.LastIndexOf( ":" ) - submoduleAddress.IndexOf( "@" ) - 1 ) + "/scm" +
					submoduleAddress.Substring( submoduleAddress.LastIndexOf( ":" ) + 5 );
				if( !url.EndsWith( ".git" ) )
					url += ".git";

				startProc( String.Format( "-c \"git submodule add {0} {1}\"", url, submodulePath ), true );

				if( !Directory.Exists( fullPath ) )
				{
					// Attempt #2:
					string url2 = "https://" + submoduleAddress.Substring( submoduleAddress.IndexOf( "@" ) + 1,
						submoduleAddress.LastIndexOf( ":" ) - submoduleAddress.IndexOf( "@" ) - 1 ) + "/" +
						submoduleAddress.Substring( submoduleAddress.LastIndexOf( ":" ) + 1 );
					if( !url2.EndsWith( ".git" ) )
						url2 += ".git";

					startProc( String.Format( "-c \"git submodule add {0} {1}\"", url, submodulePath ), true );
				}
			}

			// Still fails? IDK, try using different keys?
// 			if( !Directory.Exists(submodulePath) && Directory.Exists(Environment.SpecialFolder.Personal + ".ssh") )
// 			{
// 
// 			}

			return Directory.Exists( fullPath );
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

		public bool Git_Push()
		{
			Proc.Start();
			inputWriter = Proc.StandardInput;
			errorReader = Proc.StandardError;
			outputReader = Proc.StandardOutput;
			inputWriter.WriteLine( "git push origin" );
			inputWriter.Flush();
			inputWriter.WriteLine( "exit" );
			inputWriter.Flush();
			Proc.WaitForExit();
			return true;
		}

		public bool Git_CheckoutDevelop()
		{
			Proc.Start();
			inputWriter = Proc.StandardInput;
			errorReader = Proc.StandardError;
			outputReader = Proc.StandardOutput;
			inputWriter.WriteLine( "git branch develop; git checkout develop" );
			inputWriter.Flush();
			inputWriter.WriteLine( "exit" );
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
			inputWriter.WriteLine( "git clone -q {0}", repo );
			inputWriter.Flush();
			inputWriter.WriteLine( "exit" );
			inputWriter.Flush();
			Proc.WaitForExit();
			return true;
		}
    }
}