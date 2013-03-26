using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostBinary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bStart_EnabledChanged(object sender, EventArgs e)
        {
            if (bStart.Enabled)
            {
                bStart.BackgroundImage = Properties.Resources.bStartGray;
            }
            else
            {
                bStart.BackgroundImage = Properties.Resources.bStart;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PostBinary.Classes.TestValidator test = new Classes.TestValidator();
          
        }
    }
}
