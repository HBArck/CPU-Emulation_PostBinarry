using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;

using PostBinary.Classes;
using PostBinary.Classes.Utils.Parser;
using PostBinary.Classes.PostBinary;
namespace PostBinary
{
    public partial class MainForm : Form
    {
        #region Variables

            #region Form Members
                //Parser _validationParser = new Parser();
                MathExpParser _validationParser = new MathExpParser();
                Parser _parser = new Parser();
            #endregion

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
                //Parser muParser;
            #endregion

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void bStart_EnabledChanged(object sender, EventArgs e)
        {
            if (!bStart.Enabled)
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

        private void VarList_DoubleClick(object sender, EventArgs e)
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
            String NameforParser;
            if (dynamicTextBox.Modified)
            {
                if (dynamicTextBox.Text.Length == 0)
                    dynamicTextBox.Text = "0";

                // Validate Test First
                Classes.Responce tmpResp = ProgCore.ValidatorTool.validateNumber(dynamicTextBox.Text);
                if (!tmpResp.Error)
                {
                    NameforParser = varName.Substring(0, varName.Length - 2).Trim();
                    try
                    {
                        _parser.RemoveVariable(NameforParser);
                    }
                    catch (Exception ex) { }
                    // copy value to selected item
                    VarList.Items[selectedListIndex] = varName + " " + tmpResp.Result;//dynamicTextBox.Text;
                    _parser.AddVariable(NameforParser, tmpResp.Result.ToString());
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

            private void stepListToolStripMenuItem_Click(object sender, EventArgs e)
            {
                this.rTBInput.Text = "123-9/334+(222/999)-(90-9)*c-a";
                this.richTextBox1_KeyPress(sender, new KeyPressEventArgs('e'));
            }

            private void tetraMathToolStripMenuItem_Click(object sender, EventArgs e)
            {
                rTBLog.Text += "\n\r _________TETRA MATH TESTING BEGIN";
                String testNumber = "-10000000,1379999e-1055";
                PBNumber pbNumber1 = new PBNumber(testNumber, IPBNumber.NumberCapacity.PB256, IPBNumber.RoundingType.POST_BINARY);
                PBNumber pbNumber2 = new PBNumber(testNumber, IPBNumber.NumberCapacity.PB256, IPBNumber.RoundingType.ZERO);
                PBNumber pbNumber3 = new PBNumber(testNumber, IPBNumber.NumberCapacity.PB256, IPBNumber.RoundingType.NEGATIVE_INFINITY);
                PBNumber pbNumber4 = new PBNumber(testNumber, IPBNumber.NumberCapacity.PB256, IPBNumber.RoundingType.POSITIVE_INFINITY);
                PBNumber pbNumber5 = new PBNumber(testNumber, IPBNumber.NumberCapacity.PB256, IPBNumber.RoundingType.NEAR_INTEGER);

                PBConvertion pbc = new PBConvertion();
                PBMath pbmath = new PBMath();
                pbNumber3 = pbmath.pADD(pbNumber1, pbNumber2);

                String test1 = pbmath.pCMP(pbNumber1, pbNumber1).ToString();
                String test2 = pbmath.pCMP(pbNumber1, pbNumber2).ToString();
                String test3 = pbmath.pCMP(pbNumber2, pbNumber1).ToString();

                rTBLog.Text += "\r\n" + test1;
                rTBLog.Text += "\r\n" + test2;
                rTBLog.Text += "\r\n" + test3;
                rTBLog.Text += "\n\r _________TETRA MATH TESTING END";
            }

            private void pBNumberToolStripMenuItem_Click(object sender, EventArgs e)
            {
                rTBLog.Text += "\n\r _________PBNumer TESTING BEGIN";
                String testNumber = "-10000000,1379999e-1055";
                PBNumber pbNumber1 = new PBNumber(testNumber, IPBNumber.NumberCapacity.PB256, IPBNumber.RoundingType.POST_BINARY);
                PBNumber pbNumber2 = new PBNumber(testNumber, IPBNumber.NumberCapacity.PB256, IPBNumber.RoundingType.ZERO);
                PBNumber pbNumber3 = new PBNumber(testNumber, IPBNumber.NumberCapacity.PB256, IPBNumber.RoundingType.NEGATIVE_INFINITY);
                PBNumber pbNumber4 = new PBNumber(testNumber, IPBNumber.NumberCapacity.PB256, IPBNumber.RoundingType.POSITIVE_INFINITY);
                PBNumber pbNumber5 = new PBNumber(testNumber, IPBNumber.NumberCapacity.PB256, IPBNumber.RoundingType.NEAR_INTEGER);

                String test1 = pbNumber1.toDigit(30, true);
                rTBLog.Text += "PB " + test1 + "\r\n";

                test1 = pbNumber2.toDigit(30, true);
                rTBLog.Text += "ZERO " + test1 + "\r\n";

                test1 = pbNumber3.toDigit(30, true);
                rTBLog.Text += "NInf " + test1 + "\r\n";

                test1 = pbNumber4.toDigit(30, true);
                rTBLog.Text += "PInf " + test1 + "\r\n";

                test1 = pbNumber5.toDigit(30, true);
                rTBLog.Text += "NInt " + test1 + "\r\n";
                rTBLog.Text += "\n\r _________PBNumer TESTING END";
            }
        #endregion

          private void bStart_Click(object sender, EventArgs e)
          {
              MathExpParser mpar = new MathExpParser(this.rTBInput.Text);
              Stack<String> tempStack;
              String varName;
              if (mpar.compile(this.rTBInput.Text) == 0)
              {
                  rTBLog.Text = "";                  
                  try
                  {                      
                      _parser.Simplify(this.rTBInput.Text);
                  }
                  catch (Exception ex)
                  {
                      // Highlight input string
                      rTBLog.Text += "Done";
                      return;
                  }

                  try
                  {
                      Stack _Stack = _parser.GetStack();
                      Stack<Command> pbStack;
                      pbStack = _Stack.populateStack();
                      
                      String temp = "";
                      commandTable1.clearTable();
                      if (pbStack.Count > 0)
                      {
                          foreach (var currElem in pbStack)
                          {
                              if (currElem.leftOperand != null)
                              {
                                  commandTable1.AddItem(Command.commNames[currElem.Code] + " " + currElem.leftOperand.InitValue, currElem.leftOperand.Sign + currElem.leftOperand.Exponent + currElem.leftOperand.Mantissa);
                              }
                              else
                              {
                                  if (currElem.rightOperand == null)
                                  {
                                      if (currElem.Code != (int)CommandBase.commVals.Mem)
                                      {
                                          temp = string.Format(Command.commNames[currElem.Code] + " " + Command.commNames[(int)CommandBase.commVals.Mem].Substring(4), currElem.MemoryCellNeeded);
                                      }
                                      else
                                      {
                                          temp = string.Format(Command.commNames[currElem.Code], currElem.MemoryCellNeededExtra);
                                      }
                                  }
                                  else
                                  {
                                      if (currElem.Code != (int)CommandBase.commVals.Mem)
                                          temp = string.Format(Command.commNames[currElem.Code] + " " + Command.commNames[(int)CommandBase.commVals.Mem].Substring(4), currElem.MemoryCellNeeded);
                                      else
                                          temp = string.Format(Command.commNames[currElem.Code], currElem.MemoryCellNeeded);
                                  }
                                  commandTable1.AddItem(temp, "0" + PBNumber.EmptyExponent[2] + PBNumber.EmptyMantissa[2]);
                              }
                          }
                          rTBLog.Text += "Stack filling Done!";
                      }
                      else
                      {
                          this.rTBLog.Text += "Operation Require!";
                      }
                  }
                  catch (Exception ex)
                  {
                      this.rTBLog.Text += "Stack Filling failed!";
                      return;
                  }
              }
                            
          }

          private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
          {
              if (validationTimer.Enabled)
              {
                  validationTimer.Stop();
              }
              bStart.Enabled = false;
              validationTimer.Start();
          }

          private void validationTimer_Tick(object sender, EventArgs e)
          {
              bool _error = false;
              Stack<String> tempStack;
              String varName;
              try{
                  if (_validationParser.compile(this.rTBInput.Text) == -1)
                      _error = true;
                  else
                  {
                      _parser.RemoveAllVariables();
                      VarList.Items.Clear();
                      tempStack = _validationParser.getVars();

                      if (tempStack != null)
                      {
                          while (tempStack.Count > 0)
                          {
                              varName = tempStack.Pop();

                              _parser.AddVariable(varName, "1");
                              addVariable(varName, "1");
                          }
                      }
                  }
              }
              catch(Exception ex)
              {
                  this.rTBInput.ForeColor = Color.Red;
                  this.rTBLog.Text += "Validation Failed! \n\r";
                  _error = true;
                  bStart.Enabled = false;
              }
              finally
              {
                  validationTimer.Stop();
              }
              if (!_error)
              {
                  this.rTBLog.Text += "Validation Success. \n\r";
                  this.rTBInput.ForeColor = Color.Black;
                  bStart.Enabled = true;
              }
              else
              {
                  this.rTBInput.ForeColor = Color.Red;
                  this.rTBLog.Text += "Validation Failed! \n\r";
                  _error = true;
                  bStart.Enabled = false;
              }
              validationTimer.Stop();
          }

    }
}
