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
            return true;
        }
    }
}
