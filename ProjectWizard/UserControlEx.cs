using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWizard
{
    public partial class UserControlEx : UserControl
    {
        virtual public bool ValidateData()
        {
            //Override this.
            return true;
        }
    }
}
