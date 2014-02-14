using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ProjectWizard;
using System.Threading;

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
				Thread.Sleep( 5000 );
			Environment.Exit( 0 );
		}
	}
}
