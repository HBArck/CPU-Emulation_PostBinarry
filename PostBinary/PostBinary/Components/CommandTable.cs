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
    public partial class CommandTable : UserControl
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
            // CommandTable
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CommandTable";
            this.Size = new System.Drawing.Size(459, 308);
            this.ResumeLayout(false);

        }

        #endregion


        private List<CommanTableItem> CommandList;

        public CommandTable()
        {
            InitializeComponent();
            CommandList = new List<CommanTableItem>();
            this.AutoScrollMinSize = new Size(250, 350);
            this.HScroll = false;
            this.VScroll = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(1, 1, Width - 2, Height - 2);
            e.Graphics.FillRectangle(Brushes.White, rect);

            Size ScrollOffset = new Size(this.AutoScrollPosition);
            if (CommandList.Count > 0)
            {
                // Find how much items could be painted
                //byte PaintNumber = 10; i < PaintNumber &&
                for (int i = 0; i <= CommandList.Count - 1; i++)
                {
                    e.Graphics.DrawString((i + 1).ToString(), Font, Brushes.Black, new PointF(2, i * 20 + ScrollOffset.Height));
                    e.Graphics.DrawString(CommandList[i].CommandName, Font, Brushes.Black, new PointF(23, i * 20 + ScrollOffset.Height));
                    //this.Controls.Add(CommandList[i].CompactNumber);
                    //CommandList[i].CompactNumber.Location(88, i*20 + ScrollOffset.Height);
                    //CommandList[i].CompactNumber.Show();
                }
            }

            #region Standart Paint
            System.Drawing.Pen borderLRB;
            System.Drawing.Pen inner;

            borderLRB = new System.Drawing.Pen(System.Drawing.Color.FromArgb(130, 125, 130));//225, 235, 240
            inner = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 255, 255));

            // Top  Border
            e.Graphics.DrawLine(borderLRB, 1, 0, Width, 0);
            // Left Border
            e.Graphics.DrawLine(borderLRB, 0, 0, 0, Height);
            // Right Border
            e.Graphics.DrawLine(borderLRB, Width - 1, 0, Width - 1, Height);
            // Bottom
            e.Graphics.DrawLine(borderLRB, 0, Height - 1, Width, Height - 1);



            // Number Right Border
            e.Graphics.DrawLine(borderLRB, 29, 1, 29, Height - 1);
            // Command Right Border
            e.Graphics.DrawLine(borderLRB, 88, 1, 88, Height - 1);

            borderLRB.Dispose();
            inner.Dispose();
            #endregion



            //base.OnPaint(e);
        }
        protected override void OnScroll(ScrollEventArgs se)
        {
            PaintEventArgs ev = new PaintEventArgs(this.CreateGraphics(), ClientRectangle);
            this.OnPaint(ev);
            base.OnScroll(se);
        }

        public void AddItem(CommanTableItem newTableItem)
        {
            CommandList.Add(newTableItem);
            AddControl(newTableItem.CompactNumber);
            PaintEventArgs ev = new PaintEventArgs(this.CreateGraphics(), ClientRectangle);
            this.OnPaint(ev);
        }

        public void AddItem(String CommandName, String Value)
        {
            CommanTableItem tempItem = new CommanTableItem(CommandName, Value);
            CommandList.Add(tempItem);
            AddControl(tempItem.CompactNumber);
            PaintEventArgs ev = new PaintEventArgs(this.CreateGraphics(), ClientRectangle);
            this.OnPaint(ev);
        }

        public void AddControl(CPBNumber newControl)
        {
            int len = Controls.Count;
            Size ScrollOffset = new Size(this.AutoScrollPosition);
            this.Controls.Add(newControl);
            this.Controls[len].Location = new Point(82, 1 + len * 20 + ScrollOffset.Height);
            this.Controls[len].Show();

        }

        public CommanTableItem GetItem(int index)
        {
            return CommandList[index];
        }
        public void ChangeItemValue(int index, String newItemValue)
        {
            CommandList[index].CompactNumber.Number = newItemValue;
            PaintEventArgs ev = new PaintEventArgs(this.CreateGraphics(), ClientRectangle);
            this.OnPaint(ev);
        }

    }

    public class CommanTableItem
    {
        public String CommandName;
        public CPBNumber CompactNumber;
        public CommanTableItem(String newName, String newNumber)
        {
            CommandName = newName;
            CompactNumber = new CPBNumber(newNumber);
        }
    }
}
