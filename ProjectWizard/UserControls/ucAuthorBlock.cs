using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;

namespace ProjectWizard
{
    public partial class ucAuthorBlock : UserControlEx
    {
        public ucAuthorBlock()
        {
            InitializeComponent();

            try
            {
                // set up domain context
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

                // find currently logged in user
                UserPrincipal user = UserPrincipal.Current;

                txtAuthor.Text = user.DisplayName;
            }
            catch
            {
                //If I can't get the active directory name, just go with the login name.
                txtAuthor.Text = Environment.UserName;
            }
            
            txtProjectNumber.Text = "XXXX-" + (DateTime.Now.Month >= 10 ? DateTime.Now.Year + 1 : DateTime.Now.Year);
        }

        public override bool ValidateData()
        {
            //Can't really think of anything to validate on this page.
            return true;
        }

        public WizData_AuthorBlock GetData()
        {
            WizData_AuthorBlock wd = new WizData_AuthorBlock();
            wd.Author = txtAuthor.Text;
            wd.Customer = txtCustomer.Text;
            wd.Description = txtDescription.Text;
            wd.EmpId = txtEmployeeID.Text;
            wd.Office = txtOffice.Text;
            wd.ProjectName = txtProjectName.Text;
            wd.RequirementNum = txtProjectNumber.Text;
            wd.Status = txtStatus.Text;
            wd.Version = nudVersion.Value.ToString();

            return wd;
        }
    }
}
