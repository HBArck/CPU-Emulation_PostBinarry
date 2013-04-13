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
    public partial class MainForm : Form
    {
        // VarList Global Variables for control interconnection
        int index;
        int pos;
        TextBox tmpBox;
        String varName;
        bool fCommaEntered;
        // VarList END

        //Main Core
        Classes.ProgramCore ProgCore;

        public MainForm()
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

        private void helperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.HelperForm helper = new Forms.HelperForm();
            helper.ShowDialog();
        }


        public void addVariable(String varname, String varValue)
        {
            VarList.Items.Add(varname+"   ="+" "+varValue);
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            // Create input on item position
            index = VarList.SelectedIndex;
            pos = VarList.Items[index].ToString().IndexOf('=');
            int ItemHeight = VarList.ItemHeight;
            int ItemWidth = VarList.Width;
            
            varName = VarList.Items[index].ToString().Substring(0, pos + 1);
            tmpBox = new TextBox();
            tmpBox.Location = new Point(VarList.Location.X, VarList.Location.Y + index * ItemHeight);
            tmpBox.Height = ItemHeight;
            tmpBox.Width = ItemWidth;
            // copy only value part of item
            tmpBox.Text = VarList.Items[index].ToString().Substring(pos + 1);
            // Add it to form
            this.Controls.Add(this.tmpBox);
            tmpBox.BringToFront();

            //events 
            tmpBox.KeyPress += new KeyPressEventHandler(tmpBox_KeyPress);
            tmpBox.Show();
        }
        private void tmpBox_KeyPress(Object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == '\r') || (e.KeyChar == '\b') || (e.KeyChar == ','))
            {
                switch (e.KeyChar)
                {
                    case '\r':
                        // If text is Empty Fill it 
                        if (tmpBox.Text.Length == 0)
                            tmpBox.Text= "0";
                        // Validate Test First
                        if (ProgCore.ValidatorTool)
                        {
                            // copy value to selected item
                            VarList.Items[index] = varName + tmpBox.Text;
                            fCommaEntered = false;
                            tmpBox.Hide();
                            tmpBox.Dispose();
                        }
                        break;

                    case ',':
                        if ( (e.KeyChar == ',') && (tmpBox.Text.IndexOf(',') == -1) )
                        {
                            fCommaEntered = true;
                        }
                        else
                            e.KeyChar = '\0';
                        break;

                    case '\b':
                        
                        break;
                }
                   
            }
            else
                e.KeyChar = '\0';
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //
            ProgCore = new Classes.ProgramCore();

            
            // DEBUG 
            String[] vars = { "a", "b", "c", "d", "e" };
            String[] varVals = { "123", "234", "345", "456", "567" };
            for (int i = 0; i < 5; i++)
            {
                addVariable(vars[i], varVals[i]);
            }
        }
    }
}
