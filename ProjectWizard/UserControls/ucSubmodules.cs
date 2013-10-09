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
    public partial class ucSubmodules : UserControlEx
    {
        public ucSubmodules()
        {
            InitializeComponent();

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Submodules_Data[]));

                StreamReader reader = new StreamReader("tempSubmodule.xml");
                Submodules_Data[] temp = (Submodules_Data[])serializer.Deserialize(reader);
                reader.Close();
                foreach (Submodules_Data d in temp)
                {
                    ListViewItem item = new ListViewItem(d.Name);
                    item.Tag = d;
                    pbAvailable.Items.Add(item);
                    bSelect.Enabled = true;
                    bSelectAll.Enabled = true;
                }
            }
            catch (SystemException e)
            {
                MessageBox.Show("There was an error loading the submodule list.  The wizard will continue, but the submodules will be unavailable.\r\n\r\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override bool ValidateData()
        {
            return true;
        }

        private void bSelectAll_Click(object sender, EventArgs e)
        {
            pbSelected.push(pbAvailable.popall());
        }

        private void bSelect_Click(object sender, EventArgs e)
        {
            pbSelected.push(pbAvailable.pop());
        }

        private void bRemove_Click(object sender, EventArgs e)
        {
            pbAvailable.push(pbSelected.pop());
        }

        private void bRemoveAll_Click(object sender, EventArgs e)
        {
            pbAvailable.push(pbSelected.popall());
        }

        private void pbAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pbAvailable.SelectedItems.Count == 0)
            {
                bSelect.Enabled = false;
                if (pbAvailable.Items.Count == 0)
                {
                    bSelectAll.Enabled = false;
                }
                if (pbSelected.SelectedItems.Count == 0)
                {
                    txtDescription.Text = "No SubModule Selected";
                    lnkConf.Text = "";
                    lnkJira.Text = "";
                    lnkStash.Text = "";
                }
                return;
            }
            bSelect.Enabled = true;
            bSelectAll.Enabled = true;
            txtDescription.Text = ((Submodules_Data)pbAvailable.SelectedItems[0].Tag).Description;
            lnkConf.Text = ((Submodules_Data)pbAvailable.SelectedItems[0].Tag).Confluence;
            lnkJira.Text = ((Submodules_Data)pbAvailable.SelectedItems[0].Tag).Jira;
            lnkStash.Text = ((Submodules_Data)pbAvailable.SelectedItems[0].Tag).Stash;
        }

        private void pbSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pbSelected.SelectedItems.Count == 0)
            {
                bRemove.Enabled = false;
                if (pbSelected.Items.Count == 0)
                {
                    bRemoveAll.Enabled = false;
                }
                if (pbAvailable.SelectedItems.Count == 0)
                {
                    txtDescription.Text = "No SubModule Selected";
                    lnkConf.Text = "";
                    lnkJira.Text = "";
                    lnkStash.Text = "";
                }
                return;
            }
            bRemove.Enabled = true;
            bRemoveAll.Enabled = true;
            txtDescription.Text = ((Submodules_Data)pbSelected.SelectedItems[0].Tag).Description;
            lnkConf.Text = ((Submodules_Data)pbSelected.SelectedItems[0].Tag).Confluence;
            lnkJira.Text = ((Submodules_Data)pbSelected.SelectedItems[0].Tag).Jira;
            lnkStash.Text = ((Submodules_Data)pbSelected.SelectedItems[0].Tag).Stash;
        }
    }
}
