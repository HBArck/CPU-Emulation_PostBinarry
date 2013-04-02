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
            // Filling ComboBoxes
            foreach (String currConst in helper.constNames)
            {
                cBConst.Items.Add(currConst);
            }

            foreach (String currPolynom in helper.polynomNames)
            {
                cBPolynom.Items.Add(currPolynom);
            }

            // Init ComboBox
            cBLimit.SelectedIndex = 0;
            cBPolynom.SelectedIndex = 0;
            cBConst.SelectedIndex = 0;
            cBSwitcher.SelectedIndex = 0;
            
          
        }

        private void cBConst_SelectedIndexChanged(object sender, EventArgs e)
        {
            lConstant.Text = helper.constants[cBConst.SelectedIndex];
        }
        private void cBPolynom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBPolynom.SelectedIndex == 0)
                lPolynom.Text = "";
            else
                lPolynom.Text = "y = " + helper.polynoms[cBPolynom.SelectedIndex];
        }
        private void cBSwitcher_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;

            switch (cBSwitcher.SelectedIndex)
            { 
                case 0:
                    groupBox1.Enabled = true;
                    break;
                case 1:
                    groupBox2.Enabled = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String retString="";
            switch (cBSwitcher.SelectedIndex)
            {
                case 0:
                    retString = helper.constants[cBConst.SelectedIndex];
                    break;
                case 1:
                    retString = helper.polynoms[cBPolynom.SelectedIndex];
                    break;
                default :
                    retString = "";
                    break;
            }
            if (retString != "")
            {
                Clipboard.SetText(retString);
                MessageBox.Show("Data was successfully copied to your clipboard.\r\n" + "Data=[" + retString + "]", "Info", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("You are trying to copy empty data. \r\n Please select data to copy", "Info",MessageBoxButtons.OK);
            }
        }

      
    }
}
