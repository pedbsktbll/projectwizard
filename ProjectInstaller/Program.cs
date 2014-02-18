using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjectInstaller
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
//		[STAThread]
		static void Main()
		{
//			Application.EnableVisualStyles();
//			Application.SetCompatibleTextRenderingDefault(false);
//			Application.Run(new Form1());
			try
			{
				ProjInstaller p = new ProjInstaller();
				p.InstallProjectWizard();
			}
			catch( System.Exception ex )
			{
				Console.WriteLine("\nWhoops! The installer broke :(\nWHAT DID YOU DO??!?!!\nYou now have an undefined installation state of the ProjectWizard\n" +
					"Please speak to a ProjectWizard POC to help us discern the issue.. Thanks!\n\nError: " + ex.Message + "\nStackTrace: " + ex.StackTrace);
				Console.WriteLine("\n\nRequires user input to terminate for assurance that error message has been received.");
				Console.ReadLine();
			}
		}
	}
}
