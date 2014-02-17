using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Reflection;

namespace ProjectWizard
{
    public partial class ucSubmodules : UserControlEx
    {
        //Hack, problem is we can't just watch for the selectedindexchanged event for the pop boxes
        //because two change whenever a button was clicked and I didn't like how the descriptions were
        //coming out after a click.  This fixes that by making sure only the first is updated.
        bool bClick = true;
        public ucSubmodules()
        {
            InitializeComponent();
            pbAvailable.Tag = pbSelected;
            pbSelected.Tag = pbAvailable;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Submodules_Data[]));

				StreamReader reader = new StreamReader(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\tempSubmodule.xml");
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

                if(pbAvailable.Items.Count > 0)
                {
                    pbAvailable.Items[0].Selected = true;
                    pbAvailable.Select();
                    pbAvailable.Focus();
                }
            }
            catch (SystemException e)
            {
                MessageBox.Show("There was an error loading the submodule list.  The wizard will continue, but the submodules will be unavailable.\r\n\r\n" + e.Message, "Error Loading Submodules", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

		public void setRequiredSubmodule(string subName)
		{
			foreach( ListViewItem item in pbAvailable.Items )
			{
				if( ((Submodules_Data)item.Tag).Name.Equals(subName) )
				{
					((Submodules_Data)item.Tag).required = true;
					pbAvailable.Items.Remove(item);
					pbSelected.push(item);
					return;
				}
			}

			// WHAT? Still haven't found it?? You're trying to trick me, aint ya?!!!
			foreach( ListViewItem item in pbSelected.Items )
			{
				if( ( (Submodules_Data)item.Tag ).Name.Equals( subName ) )
				{
					( (Submodules_Data)item.Tag ).required = true;
					return;
				}
			}
		}

		public void resetSubs()
		{
			foreach( ListViewItem item in pbSelected.Items )
			{
				if( ( (Submodules_Data)item.Tag ).required == true )
				{
					( (Submodules_Data)item.Tag ).required = false;
					pbSelected.Items.Remove( item );
					pbAvailable.push( item );
				}
			}
		}

        public override bool ValidateData()
        {
            //Nothing for the user to screw up...
            return true;
        }

        public WizData_Submodules[] GetData()
        {
            WizData_Submodules[] wz = new WizData_Submodules[pbSelected.Items.Count];
            int i = 0;
            foreach (ListViewItem item in pbSelected.Items)
            {
                wz[i] = new WizData_Submodules();
                wz[i].Name = ((Submodules_Data)item.Tag).Name;
				wz[i].Repo_Name = ( (Submodules_Data)item.Tag ).Repo_Name;
                wz[i].Location = ((Submodules_Data)item.Tag).OriginLocation;
				wz[i].AddToSolution = ( (Submodules_Data)item.Tag ).AddToSolution;
                wz[i].IncludeStrAr = ((Submodules_Data)item.Tag).IncludeStrAr;
				wz[i].AddlIncludeDirs = ( (Submodules_Data)item.Tag ).AddlIncludeDirs;
				wz[i].AddlLibDirs = ( (Submodules_Data)item.Tag ).AddlLibDirs;
                i++;
            }
            return wz;
        }

        private void bSelectAll_Click(object sender, EventArgs e)
        {
            bClick = true;
            pbSelected.push(pbAvailable.popall());
        }

        private void bSelect_Click(object sender, EventArgs e)
        {
            bClick = true;
            pbSelected.push(pbAvailable.pop());
        }

        private void bRemove_Click(object sender, EventArgs e)
        {
            bClick = true;
            pbAvailable.push(pbSelected.pop());
        }

        private void bRemoveAll_Click(object sender, EventArgs e)
        {
            bClick = true;
            pbAvailable.push(pbSelected.popall());
        }

        private void PopBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopBox pb = (PopBox)sender;
            PopBox other = (PopBox)pb.Tag;
            
            if (pbAvailable.Items.Count == 0)
                bSelectAll.Enabled = false;
            else
                bSelectAll.Enabled = true;

            if (pbAvailable.SelectedItems.Count == 0)
                bSelect.Enabled = false;
            else
                bSelect.Enabled = true;

            if (pbSelected.Items.Count == 0)
                bRemoveAll.Enabled = false;
            else
                bRemoveAll.Enabled = true;

            if (pbSelected.SelectedItems.Count == 0)
                bRemove.Enabled = false;
            else
                bRemove.Enabled = true;
            
//            if (pb.SelectedItems.Count == 0)
//                return;

			// If any selected item is required, you dont get to deselect it, buddy
			foreach( ListViewItem item in pbSelected.SelectedItems )
			{
				if( ((Submodules_Data)item.Tag).required )
				{
					bRemoveAll.Enabled = false;
					bRemove.Enabled = false;
					break;
				}
			}

			// Ah! You don't get to deselect it from "Deselect All" either...
			foreach( ListViewItem item in pbSelected.Items )
			{
				if( ((Submodules_Data)item.Tag).required )
				{
					bRemoveAll.Enabled = false;
					break;
				}
			}
            
            if (bClick)
            {
                txtDescription.Text = ((Submodules_Data)pb.SelectedItems[0].Tag).Description;
                lnkConf.Text = ((Submodules_Data)pb.SelectedItems[0].Tag).Confluence;
                lnkJira.Text = ((Submodules_Data)pb.SelectedItems[0].Tag).Jira;
                lnkStash.Text = ((Submodules_Data)pb.SelectedItems[0].Tag).Stash;
                bClick = false;
            }            
        }

        //I probably don't need half of these, but meh, I'm tired and want to go to bed.
        private void popHack_Click(object sender, EventArgs e)
        {
            bClick = true;
        }

        private void popHack_KeyPress(object sender, KeyPressEventArgs e)
        {
            bClick = true;
        }

        private void popHack_KeyPress(object sender, KeyEventArgs e)
        {
            bClick = true;
        }

        private void pbAvailable_MouseClick(object sender, MouseEventArgs e)
        {
            bClick = true;
        }

        private void pbAvailable_MouseDown(object sender, MouseEventArgs e)
        {
            bClick = true;
        }
    }
}
