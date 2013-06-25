using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PostBinary.Components
{
    /// <summary>
    /// Bar that shortly shows 256 tetrit number 
    /// </summary>
    public partial class CPBNumber : UserControl
    {

         /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CPBNumber
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Name = "CPBNumber";
            this.Size = new System.Drawing.Size(160, 20);
            this.ResumeLayout(false);

        }

        #endregion
    

        private const String EMPTY_NUMBER = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";

        #region Constructors
        public CPBNumber()
        {
            numberDelimiter = 1;
            number = AddDelimiter(EMPTY_NUMBER, numberDelimiter);
            valueChanged = false;


            InitializeComponent();

            this.ClientSize = new System.Drawing.Size(width, height);
            //this.ClientRectangle = new Rectangle(this.Location, this.ClientSize);

            this.AutoSize = false;
            this.Size = new System.Drawing.Size(width, height);
            this.Size = SizeFromClientSize(ClientSize);
            this.MinimumSize = new System.Drawing.Size(width, height);
        }

        public CPBNumber(String newNumber)
        {
            numberDelimiter = 1;
            if (newNumber.Length > 256)
                number = AddDelimiter(newNumber.Substring(newNumber.Length - 256), numberDelimiter);
            else
                number = AddDelimiter(newNumber, numberDelimiter);
            valueChanged = false;

            InitializeComponent();

            this.ClientSize = new System.Drawing.Size(width, height);
            //this.ClientRectangle = new Rectangle(this.Location, this.ClientSize);

            this.AutoSize = false;
            this.Size = new System.Drawing.Size(width, height);
            this.Size = SizeFromClientSize(ClientSize);
            this.MinimumSize = new System.Drawing.Size(width, height);
        }

        /// <summary>
        /// Constructor for "Compact Number" component
        /// </summary>
        /// <param name="newNumber">Number value in tetrits</param>
        /// <param name="NumberDelimiter">Number that represents position of delimiter which splits integer part and float part of number</param>
        public CPBNumber(String newNumber, uint NumberDelimiter)
        {
            if (NumberDelimiter < 2)
                numberDelimiter = 2;
            else
                numberDelimiter = NumberDelimiter;


            if (newNumber.Length > 256)
                number = AddDelimiter(newNumber.Substring(newNumber.Length - 256), numberDelimiter);
            else
                number = AddDelimiter(newNumber, numberDelimiter);

            valueChanged = false;



            InitializeComponent();

            this.ClientSize = new System.Drawing.Size(width, height);
            //this.ClientRectangle = new Rectangle(this.Location, this.ClientSize);

            this.AutoSize = false;
            this.Size = new System.Drawing.Size(width, height);
            this.Size = SizeFromClientSize(ClientSize);
            this.MinimumSize = new System.Drawing.Size(width, height);
        }
        #endregion

        #region Properties
        private int width = 266; // 522
        private int height = 14;
        TextBox tempBox;
        [DefaultValue("")]
        private String number;
        public String Number
        {
            get { return number; }
            set
            {
                if (value != "")
                {
                    value = value.ToUpper();
                    if (value.Length > 256)
                    {

                        value = value.Substring(value.Length - 256);
                    }
                    if (!value.Equals(number))
                    {
                        number = AddDelimiter(value, numberDelimiter);
                        valueChanged = true;
                        OnValueChanged(value);
                    }
                }
            }
        }

        [DefaultValue(false)]
        private bool valueChanged = false;

        /// <summary>
        /// Quantity of symbols in integer part of number
        /// </summary>
        private uint numberDelimiter;
        public uint NumberDelimiter
        {
            get { return numberDelimiter; }
            set
            {
                if (value < 2)
                {
                    numberDelimiter = 2;
                }
                else
                    numberDelimiter = value;

            }
        }

        #endregion

        #region Event
        public delegate void CPBNumberEventHandler(object sender, CPBNumberEventArgs e);

        /// <summary>Получил  новое значение</summary>
        public event CPBNumberEventHandler Changed;

        protected void OnValueChanged(String newValue)
        {
            if (Changed != null)
                Changed(this, new CPBNumberEventArgs(newValue));
            PaintEventArgs ev = new PaintEventArgs(this.CreateGraphics(), ClientRectangle);
            OnPaint(ev);
        }
        #endregion

        private String AddDelimiter(String inStr, uint Delimeter)
        {
            inStr = inStr.Insert(1, "W");
            return inStr.Insert((int)Delimeter + 2, "W");
        }

        public void ChangeValue(String newValue, uint Delimeter)
        {
            this.numberDelimiter = Delimeter;
            this.Number = newValue;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //System.Drawing.Pen borderTop;
            System.Drawing.Pen borderLRB;
            System.Drawing.Pen inner;

            System.Drawing.Pen ZERO;
            System.Drawing.Pen ONE;
            System.Drawing.Pen M;
            System.Drawing.Pen A;

            borderLRB = new System.Drawing.Pen(System.Drawing.Color.FromArgb(180, 205, 230));//225, 235, 240
            inner = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 255, 255));
            // Top  Border
            e.Graphics.DrawLine(borderLRB, 1, 0, width, 0);
            // Left Border
            e.Graphics.DrawLine(borderLRB, 0, 0, 0, height);
            // Right Border
            e.Graphics.DrawLine(borderLRB, width, 0, width, height);
            // Bottom
            e.Graphics.DrawLine(borderLRB, 0, height, width, height);

            Rectangle rect = new Rectangle(1, 1, width - 1, height - 1);
            e.Graphics.FillRectangle(Brushes.Lavender, rect);//Lavender
            borderLRB.Dispose(); //borderTop.Dispose(); 
            inner.Dispose();

            if ((valueChanged) || (Number.Length > 0))
            {
                int paddingTopBottom = 2;
                int paddingLeftRight = 0;
                // Draw Number
                ZERO = new System.Drawing.Pen(System.Drawing.Color.FromArgb(104, 128, 55));//240, 240, 240
                ONE = new System.Drawing.Pen(System.Drawing.Color.FromArgb(130, 165, 70));

                A = new System.Drawing.Pen(System.Drawing.Color.FromArgb(191, 191, 191));
                M = new System.Drawing.Pen(System.Drawing.Color.FromArgb(205, 115, 107));
                int count = 0;

                int scaling = 1; 
                for (int i = 258; i < 0 || count < Number.Length; i--, count++)
                {

                    Point StartPos1 = new Point(i * scaling + paddingLeftRight, paddingTopBottom);
                    Point EndPos1 = new Point(i * scaling + paddingLeftRight, height - paddingTopBottom);
                    Point StartPos2 = new Point((i) * scaling + 1 + paddingLeftRight, paddingTopBottom);
                    Point EndPos2 = new Point((i) * scaling + 1 + paddingLeftRight, height - paddingTopBottom);
                    switch (Number[Number.Length - count - 1])
                    {
                        case '0':
                            e.Graphics.DrawLine(ZERO, StartPos1, EndPos1);
                            e.Graphics.DrawLine(ZERO, StartPos2, EndPos2);
                            break;

                        case '1':
                            e.Graphics.DrawLine(ONE, StartPos1, EndPos1);
                            e.Graphics.DrawLine(ONE, StartPos2, EndPos2);
                            break;

                        case 'A':
                            e.Graphics.DrawLine(A, StartPos1, EndPos1);
                            e.Graphics.DrawLine(A, StartPos2, EndPos2);
                            break;

                        case 'M':
                            e.Graphics.DrawLine(M, StartPos1, EndPos1);
                            e.Graphics.DrawLine(M, StartPos2, EndPos2);
                            break;
                    }
                }

                valueChanged = false;
            }

        }

        protected override void OnMouseHover(EventArgs e)
        {
            String temp = this.number;
            temp = temp.Replace("W", " ");

            Graphics g = this.Parent.CreateGraphics();
            tempBox = new TextBox();
            tempBox.Enabled = false;
            tempBox.Location = new Point(this.Location.X + 10, this.Location.Y + 5);
            tempBox.Text = temp;
            tempBox.Width = this.Width - 20;
            tempBox.Multiline = true;
            tempBox.Font = new Font(FontFamily.GenericMonospace, 8F);
            tempBox.Height = 63;//(int)this.CreateGraphics().MeasureString(this.number, Font).Height;
            //Rectangle rect = new Rectangle(this.Location.X, this.Location.Y + 20, width + 10, height + 10);
            this.Parent.Controls.Add(tempBox);

            tempBox.BringToFront();

            //g.FillRectangle(Brushes.LawnGreen, rect);
            //((MouseEventArgs)e).X
            base.OnMouseHover(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.Parent.Controls.Remove(tempBox);
            PaintEventArgs ev = new PaintEventArgs(this.CreateGraphics(), ClientRectangle);
            this.OnPaint(ev);
            //base.OnMouseLeave(e);
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            Clipboard.SetText(this.Number);
            base.OnMouseClick(e);
        }
    }


    public class CPBNumberEventArgs
    {
        protected String newValue;
        public String Value
        {
            get { return newValue; }
            set { newValue = value; }
        }

        public CPBNumberEventArgs(String inValue)
        {
            this.newValue = inValue;

        }
    }
}
