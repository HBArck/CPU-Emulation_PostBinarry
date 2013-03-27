using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms.Design;
namespace MyControls
{
    //public interface ITest { int testAttr(); }
    public partial class CustomTextBox : Control//TextBox,ITest
    {
        private int startPosition = 0;
        private int endPosition = 0;
        private bool displayError = false;

        /*
        private String text;
        [Category("Отображение"), Description("Текст, отображаемый в элементе управления"), DisplayName("Text")]
        [DefaultValue("")]
        public String Text
        {
            get { return text; }
            set { 
                if (value.Length <= maxTextLength)
                text = value; 
            }
        }*/
        private int length;
        [Category("Отображение"), Description("Текущее количество символов"), DisplayName("TextLength")]
        [DefaultValue(0)]
        public int Length 
        {
            get { return length; }
            set { length = value; }
        }
        
        private Timer caretTimer = new Timer();
        private bool drawCaret;
        
        private int textPadding = 2;
        
        private int fontSize = 9;
        [Category("Отображение"), Description("Размер шрифта"), DisplayName("FontSize")]
        [DefaultValue(9)]
        public int FontSize
        {
            get { return fontSize; }
            set
            {
                if (value <= 15)
                    fontSize = value;
                else
                    fontSize = 15;
                if (value < 9)
                    fontSize = 9;
            }
        }

        private int maxTextLength;
        [Category("Отображение"), Description("Максимальное количество символов"), DisplayName("MaxTextLength")]
        [DefaultValue(32565)]
        public int MaxTextLength
        {
            get { return maxTextLength; }
            set {
                if (value <= Int32.MaxValue)
                    maxTextLength = value;
                else
                    maxTextLength = Int32.MaxValue;
                if (value < 0)
                    maxTextLength = 0;
            }
        }

        /*
        private Size size;
        [Category("Отображение"), Description("Размер поля"), DisplayName("Size")]
        [DefaultValue(typeof(Size),"Size(260,20)")]
        public Size Size 
        {
            get { return size; }
            set {
                if (value.Width < 260)
                    size.Width = 260;
                if (value.Height < 20)
                    size.Height = 20;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Size s = new Size(260, 20);
            if (Size.Width < s.Width)
                Size.Width = s.Width;
            if (Size.Width > 260)
                Size.Width = 260;
            base.OnSizeChanged(e);
        }*/

        private int windowH;
        [Category("Отображение"), Description("Высота выпадающего окна"), DisplayName("WindowHeight")]
        [DefaultValue(200)]
        public int WindowHeight
        {
            get { return windowH; }
            set { 
                if (value < 19)
                    windowH = value;
                if (value > 500)
                    windowH = 500;
            }
        }

        private int windowW;
        [Category("Отображение"), Description("Ширина выпадающего окна"), DisplayName("WindowWidth")]
        [DefaultValue(260)]
        public int WindowWidth
        {
            get { return windowW; }
            set
            {
                
                if (value < ClientSize.Width)
                    windowW = ClientSize.Width;
                if (value > 500)
                    windowW = 500;
            }
        }
        //private Color forecolor;

        private int currCaretPos = 0; 
        // FLAGS BEGIN
        [DefaultValue(false)]
        private bool Entered; // When control gets focus
        
        // FLAGS END

        //private String text;

        [Category("Ошибка"), Description("Отображать ошибку"), DisplayName("DisplayError")]
        [DefaultValue(false)]
        public bool DisplayError
        {
            get { return displayError; }
            set { displayError = value;}
        }

        [Category("Ошибка"), Description("Начальная позиция символов ошибки"), DisplayName("StartPosition")]
        [DefaultValue(0)]
        public int StartPosition
        {
            get { return startPosition; }
            set { 
                if ( (value >= 0) || (value < this.Length) )
                    {
                        startPosition = value;
                    }
                }
        }

        [Category("Ошибка"), Description("Конечная позиция символов ошибки"), DisplayName("EndPosition")]
        [DefaultValue(0)]
        public int EndPosition
        {
            get { return endPosition; }
            set
            {
                if ( ((value >= 0) || (value <= this.Length)) && (value > startPosition) )
                {
                    endPosition = value;
                }
            }
        }

        //[Description("Цвет текста")]
        //public Color ForeColor 
        //{
        //    get { return forecolor; }
        //    set { forecolor = value;}
        //}
        
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            Entered = true;
            Invalidate();
            caretTimer.Start();
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            Entered = false;
            caretTimer.Stop();
            drawCaret = false;
            Invalidate();
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Entered = true;
            //currCaretPosition 
            
            Focus();
            caretTimer.Start();
            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            this.Cursor = Cursors.IBeam;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Cursor = Cursors.Arrow;
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (Length + 1 >= maxTextLength)
                return;
        }
        
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

           // if (Length + 1 >= maxTextLength)
            //    e.KeyChar = '\0';
            
            //Text = Text + e.KeyChar;
            //Length++;
            Invalidate();
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            bool flag = true;
            if ((e.KeyCode== Keys.Left) && (currCaretPos > 0))
            {
                currCaretPos--; //e.KeyChar = '\0';
                flag = false; Invalidate(); return;
            }
            if ((e.KeyCode == Keys.Right) && (currCaretPos < 30)) // count Width CORRECTLY
            {
                currCaretPos++;
                flag = false;  return;
            }
            if (flag)
            {
                String tempText = Text.Substring(0, currCaretPos);
                tempText = String.Concat(tempText, ((char)(e.KeyValue)).ToString());
                Text = tempText + Text.Substring(currCaretPos);
                //this.Text += (char)(e.KeyValue);
                currCaretPos++;
            }
            //base.OnTextChanged(e);

            Invalidate();
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
           // base.OnPaint(e);
            
            if (displayError)
            {
               // String text = Text;
                //SizeF textSize = e.Graphics.MeasureString(text, Font);
                //e.Graphics
                //System.Drawing.Font f = new System.Drawing.Font(FontFamily.GenericSansSerif, 11F);
                //e.Graphics.DrawString(Text.Substring(0, startPosition), f, Brushes.Black, ClientRectangle);

                //Rectangle temp = new Rectangle(ClientRectangle.Location.X - text.Substring(0, startPosition).Length * 11, ClientRectangle.Location.Y, ClientRectangle.Width, ClientRectangle.Height);

                //e.Graphics.DrawString(text.Substring(startPosition, endPosition), f, Brushes.Red, temp);
                //e.Graphics.DrawString(text.Substring(endPosition, Text.Length), f, Brushes.Black, ClientRectangle);
                //f.Dispose();
            }
            else
            { 
             
            }
            
            // Paint usual TextBox
            System.Drawing.Pen borderTop;
            System.Drawing.Pen borderLRB;
            System.Drawing.Pen inner;
            if (Entered)
            {
                borderTop = new System.Drawing.Pen(System.Drawing.Color.FromArgb(60, 120, 180));
                borderLRB = new System.Drawing.Pen(System.Drawing.Color.FromArgb(180, 205, 230));
                inner = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 255, 255));
            }
            else
            {
                borderTop = new System.Drawing.Pen(System.Drawing.Color.FromArgb(170, 175, 180));
                borderLRB = new System.Drawing.Pen(System.Drawing.Color.FromArgb(225, 235, 240));
                inner = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 255, 255));
            }
            //System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            e.Graphics.DrawLine(borderTop, 1, 0, ClientSize.Width - 1, 0); // Top  Border
            e.Graphics.DrawLine(borderLRB, 0, 0, 0, ClientSize.Height);// Left Border
            e.Graphics.DrawLine(borderLRB, ClientSize.Width - 1, 0, ClientSize.Width - 1, ClientSize.Height);// Right Border
            e.Graphics.DrawLine(borderLRB, 0, ClientSize.Height - 1, ClientSize.Width, ClientSize.Height - 1);// Bottom
            Rectangle rect = new Rectangle(1, 1, ClientSize.Width - 2, ClientSize.Height - 2);
            //e.Graphics.DrawRectangle(inner, rect);
            e.Graphics.FillRectangle(Brushes.White, rect);
            borderLRB.Dispose(); borderTop.Dispose(); inner.Dispose();

            //Draw Carret
            
            if (drawCaret)
            {
                System.Drawing.Pen pCaret = new Pen(System.Drawing.Color.FromArgb(0, 0, 0));
                System.Drawing.Font fnt = new System.Drawing.Font(FontFamily.GenericSansSerif, fontSize);
                if (currCaretPos > Text.Length)
                    currCaretPos = Text.Length;
                float x = e.Graphics.MeasureString(Text.Substring(0, currCaretPos), fnt).Width;
                float y = e.Graphics.MeasureString("0", fnt).Height ;
                e.Graphics.DrawLine(pCaret, 3 + x , 4, 3 + x , y); // Top  Border
                //e.Graphics.DrawLine(pCaret, new Point(4, 3), new Point(19, ClientSize.Height - 2)); // Top  Border
                pCaret.Dispose(); fnt.Dispose();
            }
            
            if (Text.Length > 0)
            { 
                System.Drawing.Font fnt = new System.Drawing.Font(FontFamily.GenericSansSerif, fontSize);
                //for (int i = 0; i< 10 || i < this.Text.Length ; i++) //(260 - textPadding * 2)/9
                {
                    //if ((this.Text[i] != '\n') || (this.Text[i] != '\b') || (this.Text[i] != '\r'))
                e.Graphics.DrawString(Text.Substring(0, Text.Length), fnt, new SolidBrush(Color.Black), 4 , 2);
                e.Graphics.DrawString(Text.Substring(0, Text.Length), fnt, new SolidBrush(Color.Red), e.Graphics.MeasureString(Text.Substring(0, Text.Length), fnt).Width, 2);
                }
                fnt.Dispose();
            }

             
        }

        private void caretTimer_Tick(System.Object sender, System.EventArgs e)
        {
            
            if (!drawCaret)
                drawCaret = true;
            else
                drawCaret = false;
            //Invalidate();
            PaintEventArgs ev = new PaintEventArgs(this.CreateGraphics(),ClientRectangle);
            OnPaint(ev);
            ev.Dispose();
        }
       
        public CustomTextBox()
        {
            SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            caretTimer.Interval = 700;
            caretTimer.Tick +=new EventHandler(caretTimer_Tick);
            maxTextLength = Text.Length;
            InitializeComponent();
        }

        

    }
}
