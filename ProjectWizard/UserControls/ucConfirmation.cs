using System.Text;

namespace ProjectWizard
{
    public partial class ucConfirmation : UserControlEx
    {
        public ucConfirmation()
        {
            InitializeComponent();
        }

        public override bool ValidateData()
        {
            //Nothing to do here.
            return true;
        }

        public void PrintResults(WizardData wd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Project Settings:\r\n");
            sb.Append("Project Type - " + wd.Type.ProjectTemplate + "\r\n");
            sb.Append("Location of Main - " + wd.Type.MainLocation + "\r\n");
            sb.Append("Location of Origin - " + wd.Type.OriginLocation + "\r\n");

            sb.Append("\r\nDesired Submodules:\r\n");
            foreach(WizData_Submodules i in wd.SubmodulesAr)
            {
                sb.Append("- " + i.Name + "\r\n");
            }

            sb.Append("\r\nAuthor Block:\r\n");
            sb.Append("Project Name - " + wd.Author.ProjectName + "\r\n");
            sb.Append("Requirement Number - " + wd.Author.RequirementNum + "\r\n");
            sb.Append("Customer - " + wd.Author.Customer + "\r\n");
            sb.Append("Office - " + wd.Author.Office + "\r\n");
            sb.Append("Author - " + wd.Author.Author + "\r\n");
            sb.Append("Version - " + wd.Author.Version + "\r\n");
            sb.Append("Description - " + wd.Author.Description + "\r\n");
            sb.Append("Status - " + wd.Author.Status + "\r\n");
            sb.Append("Employee ID - " + wd.Author.EmpId);

            this.txtConfirm.Text = sb.ToString();
        }
    }
}
