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
    public partial class ucSubmodules : UserControlEx
    {
        public ucSubmodules()
        {
            InitializeComponent();
        }

        public override bool ValidateData()
        {
            return checkBox1.Checked;
        }
    }
}
