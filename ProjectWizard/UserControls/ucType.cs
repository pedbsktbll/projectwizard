using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Reflection;

namespace ProjectWizard
{
    public partial class ucType : UserControlEx
    {
		public string projName;

        public ucType()
        {
            InitializeComponent();

            rbCON.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbCON.Tag).Type = "Console Executable";
			((ProjectType_Data)rbCON.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[0];
            ((ProjectType_Data)rbCON.Tag).Description = "Normal Windows Console Application (.exe).\r\n\r\nLike all our " +
														"projects, there are No TCHARs and no STDAFX.";
			((ProjectType_Data)rbCON.Tag).ProjectType = 0;

            rbWIN.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbWIN.Tag).Type = "Windows Executable";
			((ProjectType_Data)rbWIN.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[1];
			((ProjectType_Data)rbWIN.Tag).Description = "Basic Windows Application (.exe) with some typical boilerplate code " +
														"for registering your class, initializing your window, and enabling " +
														"your basic WndProc for Windows messages.\r\n\r\n" +
														"Additional files include a standard header file and Windows Resources.";

			((ProjectType_Data)rbWIN.Tag).ProjectType = 1;

			rbWTL.Tag = new ProjectType_Data();
			((ProjectType_Data)rbWTL.Tag).Type = "Windows WTL Executable";
			((ProjectType_Data)rbWTL.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[2];
			((ProjectType_Data)rbWTL.Tag).Description = "Windows Template Library (WTL) GUI application (.exe) with the standard " +
														"boilerplate code for creating a basic dialog.\r\n\r\n" +
														"Additional files include MainDlg.h/.cpp and Windows Resources.\r\n\r\n" +
														"REQUIRES: WTL Submodule.";
			((ProjectType_Data)rbWTL.Tag).ProjectType = 2;

            rbDLL.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbDLL.Tag).Type = "Dynamic Link Library";
			((ProjectType_Data)rbDLL.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[3];
            ((ProjectType_Data)rbDLL.Tag).Description = "Windows Dynamic-Link Library (.dll). Very basic " + 
														"with a dllmain and Exports.def";
			((ProjectType_Data)rbDLL.Tag).ProjectType = 3;

            rbLIB.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbLIB.Tag).Type = "Static Library";
			((ProjectType_Data)rbLIB.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[4];
            ((ProjectType_Data)rbLIB.Tag).Description = "Windows Static Library (.lib) with sample .h/.cpp class";
			((ProjectType_Data)rbLIB.Tag).ProjectType = 4;

            rbSYS.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbSYS.Tag).Type = "Native Driver";
			((ProjectType_Data)rbSYS.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[5];
			((ProjectType_Data)rbSYS.Tag).Description = "Windows Driver (.sys) with boilerplate WDM implementation from VisualDDK.\r\n" +
														"This project allows you to write driver code and actually compile it directly " +
														"with Visual Studio. Our only project that starts with .h/.c files.";
			((ProjectType_Data)rbSYS.Tag).ProjectType = 5;

            try
            {
				string test = Assembly.GetExecutingAssembly().Location;
                XmlSerializer serializer = new XmlSerializer(typeof(ProjectType_Data[]));

				StreamReader reader = new StreamReader(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\temp.xml");
                ProjectType_Data[] temp = (ProjectType_Data[])serializer.Deserialize(reader);
                reader.Close();
                foreach (ProjectType_Data d in temp)
                {
                    ListViewItem item = new ListViewItem(d.Type);
                    item.Tag = d;
                    lvCustomTemplates.Items.Add(item);
                }

                if (lvCustomTemplates.Items.Count > 0)
                {

                    lvCustomTemplates.Items[0].Selected = true;
                    rbCUS.Tag = lvCustomTemplates.Items[0].Tag;
                }
                else
                {
                    rbCUS.Enabled = false;
                }
            }
            catch (SystemException e)
            {
                MessageBox.Show("There was an error loading the custom templates.  The wizard will continue, but the templates will be unavailable.\r\n\r\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rbCUS.Enabled = false;
            }
        }

        public override bool ValidateData()
        {
			if( txtMain.Text == "" )
				txtMain.Text = projName;

            string e = Path.GetFileName(txtMain.Text);
            if (e != txtMain.Text)
            {
                MessageBox.Show("Error Parsing Main Project File: Putting the main cpp file in a folder is not currently supported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Do something here with git to test remote.
            return true;
        }

        public WizData_Type GetData()
        {
            WizData_Type wz = new WizData_Type();
            var checkedButton = this.gbProject.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
			wz.ProjectTemplate = ((ProjectType_Data)checkedButton.Tag).ProjectType;
			wz.projectTemplateString = ((ProjectType_Data)checkedButton.Tag).Type;
			wz.projectName = projName;
			wz.MainLocation = txtMain.Text;
            wz.OriginLocation = txtRemote.Text;
            return wz;
        }

        private void rbChecked_Changed(object sender, EventArgs e)
        {
            var checkedButton = this.gbProject.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            txtDescription.Text = ((ProjectType_Data)checkedButton.Tag).Description;
            if (rbCUS.Checked)
            {
                lblCustomProject.Enabled = true;
                lvCustomTemplates.Enabled = true;
                lvCustomTemplates.Focus();
            }
            else
            {
                lblCustomProject.Enabled = false;
                lvCustomTemplates.Enabled = false;
            }
        }

        private void lvCustomTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvCustomTemplates.SelectedItems.Count > 0)
            {
                rbCUS.Tag = lvCustomTemplates.SelectedItems[0].Tag;
                txtDescription.Text = ((ProjectType_Data)rbCUS.Tag).Description;
            }
        }

        public void SetProjectName(string projName)
        {
            try
            {
				this.projName = projName;
                txtMain.Text = projName; //+ ".cpp";
            }
            catch 
            {
				this.projName = "Project";
                txtMain.Text = "ProjectMain";//.cpp";
            }
        }
    }
}
