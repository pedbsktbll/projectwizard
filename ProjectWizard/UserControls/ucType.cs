using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ProjectWizard
{
    public partial class ucType : UserControlEx
    {
        public ucType()
        {
            InitializeComponent();

            rbCON.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbCON.Tag).Type = "Console Executable";
            //((ProjectType_Data)rbCON.Tag).Location = "CONEXELOCATION";
			((ProjectType_Data)rbCON.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[0];
            ((ProjectType_Data)rbCON.Tag).Description = "Awesome Console EXE description should go here";
			((ProjectType_Data)rbCON.Tag).ProjectType = 0;

            rbWIN.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbWIN.Tag).Type = "Windows Executable";
            //((ProjectType_Data)rbWIN.Tag).Location = "WINEXELOCATION";
			((ProjectType_Data)rbWIN.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[1];
            ((ProjectType_Data)rbWIN.Tag).Description = "Awesome Windows EXE description should go here";
			((ProjectType_Data)rbWIN.Tag).ProjectType = 1;

            rbDLL.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbDLL.Tag).Type = "Dynamic Link Library";
//            ((ProjectType_Data)rbDLL.Tag).Location = "DLLLOCATION";
			((ProjectType_Data)rbDLL.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[2];
            ((ProjectType_Data)rbDLL.Tag).Description = "Awesome DLL description should go here";
			((ProjectType_Data)rbDLL.Tag).ProjectType = 2;

            rbLIB.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbLIB.Tag).Type = "Static Library";
//            ((ProjectType_Data)rbLIB.Tag).Location = "LIBLOCATION";
			((ProjectType_Data)rbLIB.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[3];
            ((ProjectType_Data)rbLIB.Tag).Description = "Awesome LIB description should go here";
			((ProjectType_Data)rbLIB.Tag).ProjectType = 3;

            rbSYS.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbSYS.Tag).Type = "Native Driver";
//            ((ProjectType_Data)rbSYS.Tag).Location = "SYSLOCATION";
			((ProjectType_Data)rbSYS.Tag).Location = ProjectWizard.Wiz.ProjectTypeStrings[4];
            ((ProjectType_Data)rbSYS.Tag).Description = "Awesome SYS description should go here";
			((ProjectType_Data)rbSYS.Tag).ProjectType = 4;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ProjectType_Data[]));

                StreamReader reader = new StreamReader("temp.xml");
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
            string e = Path.GetExtension(txtMain.Text);
            if (e != ".cpp")
            {
                MessageBox.Show("Error Parsing Main Project File: File must end in .cpp", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            e = Path.GetFileName(txtMain.Text);
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
            //wz.ProjectTemplate = ((ProjectType_Data)checkedButton.Tag).Location;
			wz.ProjectTemplate = ((ProjectType_Data)checkedButton.Tag).ProjectType;
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
                txtMain.Text = projName + ".cpp";
            }
            catch 
            {
                txtMain.Text = "ProjectMain.cpp";
            }
        }
    }
}
