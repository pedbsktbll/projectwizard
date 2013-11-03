using System.Windows.Forms;

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
