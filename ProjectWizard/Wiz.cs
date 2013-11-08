using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;
using System.Windows.Forms;

namespace ProjectWizard
{
    public class Wiz : IDTWizard
    {

        /// <summary>
        /// Execute is the main entry point for a project wizard.  It has to follow this template.
        /// </summary>
        /// <param name="Application"></param>
        /// <param name="hwndOwner"></param>
        /// <param name="contextParams"></param>
        /// <param name="customParams"></param>
        /// <param name="retval"></param>
        /// <returns></returns>
        public void Execute(object Application, int hwndOwner, ref object[] contextParams, ref object[] customParams, ref EnvDTE.wizardResult retval)
        {
            MessageBox.Show("The wizard is now running.");
        }
//     //{
//         /// <summary>
//         /// The main entry point for the application.
//         /// </summary>
//        // [STAThread]
//         static void Main()
//         {
//             Application.EnableVisualStyles();
//             Application.SetCompatibleTextRenderingDefault(false);
//             Application.Run(new fMain());
//         }
    }
}
