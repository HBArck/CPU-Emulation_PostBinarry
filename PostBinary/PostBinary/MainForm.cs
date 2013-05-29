﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PostBinary.Classes;
namespace PostBinary
{
    public partial class MainForm : Form
    {
        #region Variables
       
            #region VarList Variables
            // VarList Global Variables for control interconnection
            int selectedListIndex;        
            /// <summary>
            /// Dynamic moving TextBox component for change selected item text of VarList
            /// </summary>
            TextBox dynamicTextBox; // this TextBox Should appear 
            /// <summary>
            /// Name of variable in VarList component
            /// </summary>
            String varName;

            /// <summary>
            /// Selected item Text in VarList component before changing
            /// </summary>
            String prevText; 
        
            #endregion

            #region MainCore Variables
            //Main Core
            Classes.ProgramCore ProgCore;
            Classes.Validator Validator;
            #endregion

        #endregion

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


        #region VarList Funcs
        public void addVariable(String varname, String varValue)
        {
            VarList.Items.Add("      " + varname + "         = " + varValue);
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            // before create new textBox make sure last closed 
            if (dynamicTextBox.Modified)
            {
                if (dynamicTextBox.Text.Length == 0)
                    dynamicTextBox.Text = "0";


                // Validate Test First
                Classes.Responce tmpResp = ProgCore.ValidatorTool.validateNumber(dynamicTextBox.Text);
                if (!tmpResp.Error)
                {
                    // copy value to selected item
                    VarList.Items[selectedListIndex] = varName + " " + tmpResp.Result;//dynamicTextBox.Text;
                    dynamicTextBox.Hide();
                }
                dynamicTextBox.Modified = false;
            }
            // Create input on item position
            selectedListIndex = VarList.SelectedIndex;

            // Save previous text
            prevText = VarList.Items[selectedListIndex].ToString();

            /// <summary>
            /// Position of "=" symbol to copy correctly name and value of selected item
            /// </summary>
            int pos = VarList.Items[selectedListIndex].ToString().IndexOf('=');

            // Size of item
            int ItemHeight = VarList.ItemHeight;
            int ItemWidth = VarList.Width;

            // Copied variable name  
            varName = VarList.Items[selectedListIndex].ToString().Substring(0, pos + 1);
           
            dynamicTextBox.Location = new Point(VarList.Location.X, VarList.Location.Y + selectedListIndex * ItemHeight);
            dynamicTextBox.Height = ItemHeight;
            dynamicTextBox.Width = ItemWidth;

            // copy only value part of item
            dynamicTextBox.Text = VarList.Items[selectedListIndex].ToString().Substring(pos + 2);
            dynamicTextBox.BringToFront();
            dynamicTextBox.Show();
            dynamicTextBox.Focus();
        }
        private void dynamicTextBox_KeyPress(Object sender, KeyPressEventArgs e)
        {

            if (Char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == '.' || e.KeyChar == '-' || e.KeyChar == '\r' || e.KeyChar == '\b' 
                || e.KeyChar == 27)
            {
                switch (e.KeyChar)
                {
                    case '\r':
                        // If text is Empty Fill it 
                        if (dynamicTextBox.Text.Length == 0)
                            dynamicTextBox.Text = "0";

                        // Validate Test First
                        Classes.Responce tmpResp = ProgCore.ValidatorTool.validateNumber(dynamicTextBox.Text);
                        if (!tmpResp.Error)
                        {
                            // copy value to selected item
                            VarList.Items[selectedListIndex] = varName + " " + tmpResp.Result;//dynamicTextBox.Text;
                            dynamicTextBox.Hide();
                        }
                        break;

                    case '.':
                        if (dynamicTextBox.Text.IndexOf(',') != -1)
                            e.KeyChar = '\0';
                        else
                        {
                            if (dynamicTextBox.Text.Length == 0)
                            {
                                dynamicTextBox.Text += "0,";
                                e.KeyChar = '\0';
                            }
                            else
                                e.KeyChar = ',';
                        }
                        break;
                    case ',':
                        if (dynamicTextBox.Text.IndexOf(',') != -1)
                            e.KeyChar = '\0';
                        else 
                        {
                            if (dynamicTextBox.Text.Length == 0)
                            {
                                dynamicTextBox.Text += "0,";
                                e.KeyChar = '\0';
                            }
                        }
                        break;

                    case '\b':
                        
                        break;

                    case '-':
                        if (dynamicTextBox.Text.Length==0)
                        {
                            dynamicTextBox.Text = "-" + dynamicTextBox.Text;
                            e.KeyChar = '\0';
                        }
                        if (dynamicTextBox.Text[0] != '-')
                        {
                            dynamicTextBox.Text = "-" + dynamicTextBox.Text;
                            e.KeyChar = '\0';
                        }
                        if (dynamicTextBox.Text.IndexOf('-') != -1)
                            e.KeyChar = '\0';    
                        break;
                    case (char)27: // Esc
                        VarList.Items[selectedListIndex] = prevText;
                        dynamicTextBox.Hide();
                        VarList.Focus();
                        break;
                    

                }
                   
            }
            else
                e.KeyChar = '\0';
        }
        private void dynamicTextBox_LostFocus(object sender, EventArgs e)
        {
            if (dynamicTextBox.Modified)
            {
                if (dynamicTextBox.Text.Length == 0)
                    dynamicTextBox.Text = "0";

                // Validate Test First
                Classes.Responce tmpResp = ProgCore.ValidatorTool.validateNumber(dynamicTextBox.Text);
                if (!tmpResp.Error)
                {
                    // copy value to selected item
                    VarList.Items[selectedListIndex] = varName + " " + tmpResp.Result;//dynamicTextBox.Text;
                    dynamicTextBox.Hide();
                }
                dynamicTextBox.Modified = false;
                dynamicTextBox.Hide();
            }
            else
            {
                VarList.Items[selectedListIndex] = prevText;
                dynamicTextBox.Hide();
                VarList.Focus();
            }
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            #region Init UI
            

            //Create textBox for VarList Component
            dynamicTextBox = new TextBox();

            // Add it to form
            this.Controls.Add(this.dynamicTextBox);

            //events 
            dynamicTextBox.KeyPress += new KeyPressEventHandler(dynamicTextBox_KeyPress);
            dynamicTextBox.LostFocus += new EventHandler(dynamicTextBox_LostFocus);
            #endregion
            #region Init Logic
            // 
            ProgCore = new Classes.ProgramCore();
            Validator = new Classes.Validator();
            #endregion
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        #region Debug Testers
            #region Validator Testers
            private void validateToolStripMenuItem_Click(object sender, EventArgs e)
            {
                TestValidator test = new TestValidator();
            }

            private void variableToolStripMenuItem_Click(object sender, EventArgs e)
            {
                TestVariableValidator test = new TestVariableValidator();
            }
            #endregion

            #region VarList Testers
            private void varListToolStripMenuItem_Click(object sender, EventArgs e)
            {
                // DEBUG 
                String[] vars = { "a", "b", "c", "d", "e" };
                String[] varVals = { "123", "234", "345", "456", "567" };
                for (int i = 0; i < 5; i++)
                {
                    addVariable(vars[i], varVals[i]);
                }
            }
            #endregion
        #endregion

          private void richTextBox1_TextChanged(object sender, EventArgs e)
          {
              ValidationResponce currentresponce;
              currentresponce = Validator.validate(this.richTextBox1.Text);
              if (!currentresponce.Error)
              {
                  bStart.Enabled = true;
              }
          }
    }
}
