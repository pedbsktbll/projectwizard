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

namespace ProjectInstaller
{
//	public partial class Form1 : Form
	public class ProjInstaller
	{
		public ProjInstaller()
		{
//			InitializeComponent();

			SortedDictionary<string, Stream> wizFiles = GetResources("ProjectInstaller.Resources");
			string progFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
			string projDir = "\\VC\\vcprojects\\";
			string wizDir = projDir + "OSBWizard\\";

			string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 3) + "OSBWizard\\";
			Directory.CreateDirectory(baseDir);
			foreach( var kvp in wizFiles )
			{
				if( kvp.Key.StartsWith("OSBWizard") )
					continue;
				Stream output = File.OpenWrite(baseDir + kvp.Key);
				if( output != null )
				{
					kvp.Value.CopyTo(output);
					output.Close();
				}
			}

			// FIGURED IT OUTTTTTTTT!!!!!!!!!!!!!!!!!!! From Visual Studio Command Prompt, run regasm ProjectWizard.dll / codebase C:\OSBWizard\ProjectWizard.dll
			// Additional we COULD OPTIONALLY add to GAC.. but I dont think this is necessary... I was playing around with: gacutil -i ProjectWizard.dll
			// gacutil -u ProjectWizard..... regasm ProjectWizard.dll /unregister

			Assembly asm = Assembly.LoadFrom(baseDir + "ProjectWizard.dll");
			RegistrationServices regAsm = new RegistrationServices();
			bool bResult = regAsm.RegisterAssembly(asm, AssemblyRegistrationFlags.SetCodeBase);

			StringBuilder vs = new StringBuilder(progFiles + "\\Microsoft Visual Studio 10.0");
			for( char i = '0'; i < '3'; i++ )
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
						output.Close();
					}
				}
			}

			foreach( var kvp in wizFiles )
				kvp.Value.Close();

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
	}
}
