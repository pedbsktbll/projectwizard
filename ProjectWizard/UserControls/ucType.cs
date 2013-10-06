using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace ProjectWizard
{
    public partial class ucType : UserControlEx
    {
        public ucType()
        {
            InitializeComponent();

            rbCON.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbCON.Tag).Type = "Console Executable";
            ((ProjectType_Data)rbCON.Tag).Location = "CONEXELOCATION";
            ((ProjectType_Data)rbCON.Tag).Description = "Awesome Console EXE description should go here";

            rbWIN.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbWIN.Tag).Type = "Windows Executable";
            ((ProjectType_Data)rbWIN.Tag).Location = "WINEXELOCATION";
            ((ProjectType_Data)rbWIN.Tag).Description = "Awesome Windows EXE description should go here";

            rbDLL.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbDLL.Tag).Type = "Dynamic Link Library";
            ((ProjectType_Data)rbDLL.Tag).Location = "DLLLOCATION";
            ((ProjectType_Data)rbDLL.Tag).Description = "Awesome DLL description should go here";

            rbLIB.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbLIB.Tag).Type = "Static Library";
            ((ProjectType_Data)rbLIB.Tag).Location = "LIBLOCATION";
            ((ProjectType_Data)rbLIB.Tag).Description = "Awesome LIB description should go here";

            rbSYS.Tag = new ProjectType_Data();
            ((ProjectType_Data)rbSYS.Tag).Type = "Native Driver";
            ((ProjectType_Data)rbSYS.Tag).Location = "SYSLOCATION";
            ((ProjectType_Data)rbSYS.Tag).Description = "Awesome SYS description should go here";

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
            return true;
        }

        public WizData_Type GetData()
        {
            WizData_Type wz = new WizData_Type();
            var checkedButton = this.gbProject.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            wz.ProjectTemplate = ((ProjectType_Data)checkedButton.Tag).Location;
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
    }
}
