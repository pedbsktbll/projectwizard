using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
