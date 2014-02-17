using System.Text;
using System.Drawing;

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
			Font titleFont = new Font( "Consolas", 15, FontStyle.Bold );
			Font normalFont = new Font( "Consolas", 13, FontStyle.Regular );
			Color titleColor = Color.FromArgb( 131, 208, 226 );  //Color.FromArgb( 53, 110, 137 );
			Color normalColor = titleColor;//Color.Black;

			txtConfirm.Clear();
			lblConfirm.ForeColor = normalColor;
			txtConfirm.BackColor = this.Parent.BackColor = Color.FromArgb( 28, 49, 66 );//Color.FromArgb( 48, 99, 130 );
			this.BackColor = Color.Transparent;

			txtConfirm.SelectionFont = titleFont;
			txtConfirm.SelectionColor = titleColor;
			txtConfirm.AppendText( "Project Settings:\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
 			txtConfirm.AppendText( "Project Type - \t\t" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Type.projectTemplateString + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
 			txtConfirm.AppendText( "Project Name - \t\t" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Type.projectName + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Main cpp file - \t" );
			
			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Type.MainLocation + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Remote Repository - \t" );
			
			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Type.OriginLocation + "\r\n" );

			txtConfirm.SelectionFont = titleFont;
			txtConfirm.SelectionColor = titleColor;
 			txtConfirm.AppendText( "\r\nDesired Submodules: NOTE: MAY TAKE SEVERAL MINUTES TO CLONE\r\n" );

			bool bHasSubmodule = false;
			foreach( WizData_Submodules i in wd.SubmodulesAr )
			{
				bHasSubmodule = true;
				txtConfirm.SelectionFont = normalFont;
				txtConfirm.SelectionColor = Color.White;
				txtConfirm.AppendText( "- " + i.Name + "\r\n" );
			}

			if( !bHasSubmodule )
			{
				txtConfirm.SelectionFont = normalFont;
				txtConfirm.SelectionColor = Color.White;
				txtConfirm.AppendText( "NONE\r\n" );
			}

			txtConfirm.SelectionFont = titleFont;
			txtConfirm.SelectionColor = titleColor;
			txtConfirm.AppendText( "\r\nAuthor Block:\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Project Name - \t\t" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Author.ProjectName + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Version - \t\t\t" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Author.Version + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Author - \t\t\t" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Author.Author + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Requirement Number - \t" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Author.RequirementNum + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Customer - \t\t" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Author.Customer + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Office - \t\t\t" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Author.Office + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Description - \r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Author.Description + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Status - \t\t\t" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Author.Status + "\r\n" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = normalColor;
			txtConfirm.AppendText( "Employee ID - \t\t" );

			txtConfirm.SelectionFont = normalFont;
			txtConfirm.SelectionColor = Color.White;
			txtConfirm.AppendText( wd.Author.EmpId );
        }
    }
}
