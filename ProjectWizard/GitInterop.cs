using System.Diagnostics;
using System.IO;
using System;
using Microsoft.Win32;

namespace ProjectWizard
{
    public class GitInterop
    {
		private string gitInstallPath;
		private Process Proc;
		private ProcessStartInfo ProcInfo;
        private StreamWriter inputWriter;
        private StreamReader errorReader;
        private StreamReader outputReader;
		private string workingDirectory;
		private string plink;

        public GitInterop(string workingDirectory)
        {
			gitInstallPath = Environment.Is64BitOperatingSystem ?
			   (string)Registry.GetValue( @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Git_is1",
			   "InstallLocation", @"C:\Program Files (x86)\Git\" ) :
			   (string)Registry.GetValue( @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Git_is1",
			   "IntsllLocation", @"C:\Program Files\Git\" );

			/// AH HA!! GIT Bash and GIT CMD can be set up in three possible ways: 
			// http://stackoverflow.com/questions/8947140/git-cmd-vs-git-exe-what-is-the-difference-and-which-one-should-be-used
			// GIT Bash only (most conservative, nothing added to PATH)
			// Git from CMD only (safe with no conflicts, only adds Git to PATH)
			// Git + unix tools (override windows tools and add all unix utilities to PATH)
			//
			// The first option with nothing added to the path is what we need to handle... and we accomplish half of this by finding the git EXE ^^^^
			// HOWEVER, %HOME% is NOT set either... which means ssh tries to find the .ssh dir with our keys in /.ssh/, which doesnt make sense in windows.
			// SO, let's set %HOME% if not already:
			string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			if( Environment.GetEnvironmentVariable("HOME") == null )
				Environment.SetEnvironmentVariable("HOME", userProfile);

			// Should we also check if they're using Pageant and use those keys??
			// ENV VAR: %GIT_SSH%
			//if( !File.Exists(userProfile + "\\.ssh\\id_rsa") )
			Process[] pageant = Process.GetProcessesByName("Pageant");
			if( pageant.Length >= 1 )
			{
				plink = Path.GetDirectoryName(pageant[0].Modules[0].FileName) + "\\plink.exe";
//				if( File.Exists(plink) && Environment.GetEnvironmentVariable("GIT_SSH") == null )
//					Environment.SetEnvironmentVariable("GIT_SSH", plink);
			}

			Proc = new Process();
			ProcInfo = new ProcessStartInfo(); // Ensures the process gets the newly created environment vars we just set ^^^^
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

		private int startProc(string args, bool createWindow = false, bool usePlink = false)
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

			if( usePlink )
				myProcInfo.EnvironmentVariables["GIT_SSH"] = plink;

			myProc.Start();
			myProc.WaitForExit();
			return myProc.ExitCode;
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
// 			inputWriter.Close();		// This shit needed? -JS
// 			outputReader.Close();
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
			startProc(String.Format("-c \"'{0}' submodule add {1} {2}\"", gitInstallPath + "bin\\git.exe", submoduleAddress, submodulePath), true);

			// On first failure, let's try plink...
			if( !Directory.Exists(fullPath) && plink != null )
				startProc(String.Format("-c \"'{0}' submodule add {1} {2}\"", gitInstallPath + "bin\\git.exe", submoduleAddress, submodulePath), true, true);

			// ... Try https?
			if( !Directory.Exists(fullPath) && (submoduleAddress.StartsWith("git") || submoduleAddress.StartsWith("ssh")) )
			{
				// Attempt #1: This is the way stash handles it
				string url = "https://" + submoduleAddress.Substring( submoduleAddress.IndexOf( "@" ) + 1,
					submoduleAddress.LastIndexOf( ":" ) - submoduleAddress.IndexOf( "@" ) - 1 ) + "/scm" +
					submoduleAddress.Substring( submoduleAddress.LastIndexOf( ":" ) + 5 );
				if( !url.EndsWith( ".git" ) )
					url += ".git";

				//startProc( String.Format( "-c \"git submodule add {0} {1}\"", url, submodulePath ), true );
				startProc(String.Format("-c \"'{0}' submodule add {1} {2}\"", gitInstallPath + "bin\\git.exe", url, submodulePath), true);

				if( !Directory.Exists( fullPath ) )
				{
					// Attempt #2: This is the way bitbucket handles it
					string url2 = "https://" + submoduleAddress.Substring( submoduleAddress.IndexOf( "@" ) + 1,
						submoduleAddress.LastIndexOf( ":" ) - submoduleAddress.IndexOf( "@" ) - 1 ) + "/" +
						submoduleAddress.Substring( submoduleAddress.LastIndexOf( ":" ) + 1 );
					if( !url2.EndsWith( ".git" ) )
						url2 += ".git";

					//startProc( String.Format( "-c \"git submodule add {0} {1}\"", url, submodulePath ), true );
					startProc(String.Format("-c \"'{0}' submodule add {1} {2}\"", gitInstallPath + "bin\\git.exe", url2, submodulePath), true);
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
			inputWriter.WriteLine( "git push --all" );
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

		public bool Git_Clone(string repo, string dirPath)
		{
// 			Proc.Start();
// 			inputWriter = Proc.StandardInput;
// 			errorReader = Proc.StandardError;
// 			outputReader = Proc.StandardOutput;
// 			inputWriter.WriteLine( "git clone -q {0}", repo );
// 			inputWriter.Flush();
// 			inputWriter.WriteLine( "exit" );
// 			inputWriter.Flush();
// 			Proc.WaitForExit();
// 			return true;

			startProc( String.Format( "-c \"'{0}' clone -q {1}\"", gitInstallPath + "bin\\git.exe", repo ), true );

			// On first failure, let's try plink...
			if( !Directory.Exists( dirPath ) && plink != null )
				startProc( String.Format( "-c \"'{0}' clone -q {1}\"", gitInstallPath + "bin\\git.exe", repo ), true, true );

			// ... Try https?
			if( !Directory.Exists( dirPath ) && ( repo.StartsWith( "git" ) || repo.StartsWith( "ssh" ) ) )
			{
				// Attempt #1: This is the way stash handles it
				string url = "https://" + repo.Substring( repo.IndexOf( "@" ) + 1,
					repo.LastIndexOf( ":" ) - repo.IndexOf( "@" ) - 1 ) + "/scm" +
					repo.Substring( repo.LastIndexOf( ":" ) + 5 );
				if( !url.EndsWith( ".git" ) )
					url += ".git";

				//startProc( String.Format( "-c \"git submodule add {0} {1}\"", url, submodulePath ), true );
				startProc( String.Format( "-c \"'{0}' clone -q {1}\"", gitInstallPath + "bin\\git.exe", url ), true );

				if( !Directory.Exists( dirPath ) )
				{
					// Attempt #2: This is the way bitbucket handles it
					string url2 = "https://" + repo.Substring( repo.IndexOf( "@" ) + 1,
						repo.LastIndexOf( ":" ) - repo.IndexOf( "@" ) - 1 ) + "/" +
						repo.Substring( repo.LastIndexOf( ":" ) + 1 );
					if( !url2.EndsWith( ".git" ) )
						url2 += ".git";

					//startProc( String.Format( "-c \"git submodule add {0} {1}\"", url, submodulePath ), true );
					startProc( String.Format( "-c \"'{0}' clone -q {1}\"", gitInstallPath + "bin\\git.exe", url2 ), true );
				}
			}
			
			// Still fails? IDK, try using different keys?
			//Console.WriteLine("Alright well it looks like you fail at GIT and I can't find your ")
			// 			if( !Directory.Exists(submodulePath) && Directory.Exists(Environment.SpecialFolder.Personal + ".ssh") )
			// 			{
			// 
			// 			}

			return Directory.Exists( dirPath );
		}

		public bool Git_Pull(bool quiet = true)
		{
			//int retCode = startProc( String.Format( "-c \"'{0}' pull\"", gitInstallPath + "bin\\git.exe" ) );
			int retCode = startProc( String.Format( "-c \"'{0}' pull\"", gitInstallPath + "bin\\git.exe" ), !quiet );

			// On first failure, let's try plink...
			if( retCode != 0 && plink != null )
				retCode = startProc( String.Format( "-c \"'{0}' pull\"", gitInstallPath + "bin\\git.exe" ), !quiet, true );

			// ... Try https?
// 			if( retCode != 0 && ( repo.StartsWith( "git" ) || repo.StartsWith( "ssh" ) ) )
// 			{
// 				// Attempt #1: This is the way stash handles it
// 				string url = "https://" + repo.Substring( repo.IndexOf( "@" ) + 1,
// 					repo.LastIndexOf( ":" ) - repo.IndexOf( "@" ) - 1 ) + "/scm" +
// 					repo.Substring( repo.LastIndexOf( ":" ) + 5 );
// 				if( !url.EndsWith( ".git" ) )
// 					url += ".git";
// 
// 				//startProc( String.Format( "-c \"git submodule add {0} {1}\"", url, submodulePath ), true );
// 				startProc( String.Format( "-c \"'{0}' pull\"", gitInstallPath + "bin\\git.exe", url ) );
// 
// 				if( !Directory.Exists( dirPath ) )
// 				{
// 					// Attempt #2: This is the way bitbucket handles it
// 					string url2 = "https://" + repo.Substring( repo.IndexOf( "@" ) + 1,
// 						repo.LastIndexOf( ":" ) - repo.IndexOf( "@" ) - 1 ) + "/" +
// 						repo.Substring( repo.LastIndexOf( ":" ) + 1 );
// 					if( !url2.EndsWith( ".git" ) )
// 						url2 += ".git";
// 
// 					//startProc( String.Format( "-c \"git submodule add {0} {1}\"", url, submodulePath ), true );
// 					startProc( String.Format( "-c \"'{0}' pull\"", gitInstallPath + "bin\\git.exe", url2 ) );
// 				}
// 			}

			// Still fails? IDK, try using different keys?
			// 			if( !Directory.Exists(submodulePath) && Directory.Exists(Environment.SpecialFolder.Personal + ".ssh") )
			// 			{
			// 
			// 			}

//			return Directory.Exists( dirPath );
			return retCode == 0 ? true : false;
		}
    }
}