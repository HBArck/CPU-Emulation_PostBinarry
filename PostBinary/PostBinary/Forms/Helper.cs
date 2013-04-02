using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostBinary.Forms
{
    public partial class HelperForm : Form
    {
        private Classes.Helper helper;

        public HelperForm()
        {
            InitializeComponent();
        }

        private void Helper_Load(object sender, EventArgs e)
        {
            helper = new Classes.Helper();

            foreach (String currConst in helper.constants)
            {
                cBConst.Items.Add(currConst);
            }
        }
    }
}
