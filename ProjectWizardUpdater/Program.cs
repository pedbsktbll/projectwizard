using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ProjectWizard;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ProjectWizardUpdater
{
	class Program
	{
		// Params:
		// ./ProjectWizardUpdater (-q) PATH
		static void Main(string[] args)
		{
//			Thread.Sleep( 1000 * 15 );
			bool quiet = false;
			string location = Environment.GetFolderPath( Environment.SpecialFolder.ProgramFilesX86 ) + "\\OSBWizard\\ProjectWizardBins";

			switch( args.Length )
			{
				case 0:
					break; // using defaults
				case 1:		   // Supplying ONE argument
				{
					if( args[0].Equals( "-q" ) )
						quiet = true;
					else
						location = args[0];
				}
				break;
				case 2:		   // Supplies both
				{
					quiet = true;
					location = args[1];
				}
				break;
				default:
				{
					Console.WriteLine( "Syntax Error: ./ProjectWizardUpdater (-q) (PATH)" );
					Console.WriteLine( "Params: -q: Quiet." );
					Environment.Exit( -1 );
				} break;
			}

			if( !quiet )
				DoEvil();

			// Check path
			if( !Directory.Exists( location ) )
			{
				Console.WriteLine( "Error: ProjectWizard does not exist!" );
				Environment.Exit( -2 );
			}

			GitInterop git = new GitInterop(location);
			Console.WriteLine( "Updatine repo..." );
			bool retVal = git.Git_Pull( quiet );
			Console.WriteLine( "Operation: " + (retVal ? "Succeeded" : "Failed") );
			if( !quiet )
				StopEvil();
			Environment.Exit( 0 );
		}

		// ROFL, this is tight....
		// SEE: http://stackoverflow.com/questions/472282/show-console-in-windows-application
		[DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32", SetLastError = true)]
        static extern bool AttachConsole(int dwProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		static void DoEvil()
		{
//			//TODO: better handling of command args, (handle help (--help /?) etc.)
//            string mode = args.Length > 0 ? args[0] : "gui"; //default to gui
//
//            if (mode == "gui")
//            {
//                MessageBox.Show("Welcome to GUI mode");
//
//                Application.EnableVisualStyles();
//
//                Application.SetCompatibleTextRenderingDefault(false);
//
//                Application.Run(new Form1());
//            }
//            else if (mode == "console")
//            {

                //Get a pointer to the forground window.  The idea here is that
                //IF the user is starting our application from an existing console
                //shell, that shell will be the uppermost window.  We'll get it
                //and attach to it
                IntPtr ptr = GetForegroundWindow();

                int  u;

                GetWindowThreadProcessId(ptr, out u);

                Process process = Process.GetProcessById(u);

                if (process.ProcessName == "cmd" )    //Is the uppermost window a cmd process?
                {
                    AttachConsole(process.Id);
//
//                    //we have a console to attach to ..
//                    Console.WriteLine("hello. It looks like you started me from an existing console.");
				}
				else
				{
//                     //no console AND we're in console mode ... create a new console.
// 
                     AllocConsole();
// 
//                     Console.WriteLine(@"hello. It looks like you double clicked me to start
//                    AND you want console mode.  Here's a new console.");
//                     Console.WriteLine("press any key to continue ...");
//                     Console.ReadLine();       
				}
//
//                FreeConsole();
		}

		static void StopEvil()
		{
			Thread.Sleep(1000 * 4);
			FreeConsole();
		}
	}
}
