using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectWizard
{
    class PopBox : ListView
    {
        public ListViewItem pop()
        {
            //Maybe I should allow for more then 1 item to be returned, but I'm lazy.
            if (this.SelectedItems.Count != 1)
                return null;

            ListViewItem poppedItem = (ListViewItem)this.SelectedItems[0];
            int selIndex = this.SelectedItems[0].Index;
            this.Items.RemoveAt(selIndex);

            //No other items in the index, we are done.
            if(this.Items.Count == 0)
                return poppedItem;

            if (selIndex == this.Items.Count)
                this.Items[selIndex - 1].Selected = true;
            else
                this.Items[selIndex].Selected = true;

            this.Select();

            return poppedItem;
        }

        public ListViewItem[] popall()
        {
            try
            {
                ListViewItem[] poppedItems = new ListViewItem[this.Items.Count];
                int i = 0;
                foreach (ListViewItem item in this.Items)
                {
                    poppedItems[i] = item;
                    this.Items.Remove(item);
                }
                return poppedItems;
            }
            catch
            {
                return null;
            }
        }

        public bool push(ListViewItem toPush)
        {
            if (toPush == null)
                return false;

            try
            {
                this.Items.Add(toPush);
                this.Sort();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool push(ListViewItem[] toPush)
        {
            if (toPush == null)
                return false;

            try
            {
                foreach (ListViewItem i in toPush)
                {
                    this.Items.Add(i);
                }
                this.Sort();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
