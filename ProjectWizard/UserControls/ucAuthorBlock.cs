using System;
using System.DirectoryServices.AccountManagement;

namespace ProjectWizard
{
    public partial class ucAuthorBlock : UserControlEx
    {
		private static int descMaxLen = 80;
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
            
            txtProjectNumber.Text = (DateTime.Now.Month >= 10 ? DateTime.Now.Year + 1 : DateTime.Now.Year) + "-XXXX";
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
            wd.EmpId = txtEmployeeID.Text;
            wd.Office = txtOffice.Text;
            wd.ProjectName = txtProjectName.Text;
            wd.RequirementNum = txtProjectNumber.Text;
            wd.Status = txtStatus.Text;
            wd.Version = nudVersion.Value.ToString();

			// Project description newline issue...
			wd.Description = txtDescription.Text;

			// If user puts in their own newlines, let's not fuck with it.. otherwise, let's add our own newlines:
			if( !wd.Description.Contains(Environment.NewLine) && wd.Description.Length > descMaxLen )
			{
				// So, assuming an 80 char max line, 79 and 80 are reserved for the \r and \n.
				for( int i = 1; i <= wd.Description.Length / descMaxLen; i++ )
				{
					int startingPos = i == 1 ? 0 : descMaxLen * (i - 1);
					// First let's make sure the line we're looking at actually has spaces...
					if( !wd.Description.Substring(startingPos, descMaxLen - 2).Contains(" ") )
					{
						// If it doesn't have spaces, then I'm just gonna throw the newline right at the exact end.
						wd.Description = wd.Description.Insert(descMaxLen * i - 2, "\r\n");
						continue;
					}
					// Otherwise we have spaces, so let's find where to split the line
					for( int j = descMaxLen * i - 2; j >= startingPos; j-- )
					{
						if( wd.Description[j] == ' ' )
						{
//							wd.Description = wd.Description.Insert(j, "\r\n");
							wd.Description = wd.Description.Substring(0, j) + "\r\n" + wd.Description.Substring(j + 2);
							break;
						}
					}
				}
			}
			if( !wd.Description.EndsWith("\r\n") )
				wd.Description += "\r\n";

            return wd;
        }

        public void SetProjectName(string projName)
        {
            try
            {
                txtProjectName.Text = projName;
            }
            catch
            {
                txtProjectName.Text = "";
            }
        }
    }
}
