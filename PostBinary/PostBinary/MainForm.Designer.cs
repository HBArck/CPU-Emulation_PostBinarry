namespace PostBinary
{
    partial class MainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tSSLStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.varListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tetraMathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pBNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tResult = new System.Windows.Forms.TextBox();
            this.tInfelicity = new System.Windows.Forms.TextBox();
            this.bStart = new System.Windows.Forms.Button();
            this.VarList = new System.Windows.Forms.ListBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.rTBInput = new System.Windows.Forms.RichTextBox();
            this.rTBLog = new System.Windows.Forms.RichTextBox();
            this.validationTimer = new System.Windows.Forms.Timer(this.components);
            this.commandTable1 = new PostBinary.Components.CommandTable();
            this.srzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSSLStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 676);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(892, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tSSLStatus
            // 
            this.tSSLStatus.Name = "tSSLStatus";
            this.tSSLStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.debugToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(892, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem,
            this.helperToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.WindowText;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // helperToolStripMenuItem
            // 
            this.helperToolStripMenuItem.Name = "helperToolStripMenuItem";
            this.helperToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.helperToolStripMenuItem.Text = "Helper";
            this.helperToolStripMenuItem.Click += new System.EventHandler(this.helperToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.validatorToolStripMenuItem,
            this.varListToolStripMenuItem,
            this.stepListToolStripMenuItem,
            this.tetraMathToolStripMenuItem,
            this.pBNumberToolStripMenuItem,
            this.srzToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // validatorToolStripMenuItem
            // 
            this.validatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.validateToolStripMenuItem,
            this.variableToolStripMenuItem});
            this.validatorToolStripMenuItem.Name = "validatorToolStripMenuItem";
            this.validatorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.validatorToolStripMenuItem.Text = "Validator";
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.validateToolStripMenuItem.Text = "Validate";
            this.validateToolStripMenuItem.Click += new System.EventHandler(this.validateToolStripMenuItem_Click);
            // 
            // variableToolStripMenuItem
            // 
            this.variableToolStripMenuItem.Name = "variableToolStripMenuItem";
            this.variableToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.variableToolStripMenuItem.Text = "Variable";
            this.variableToolStripMenuItem.Click += new System.EventHandler(this.variableToolStripMenuItem_Click);
            // 
            // varListToolStripMenuItem
            // 
            this.varListToolStripMenuItem.Name = "varListToolStripMenuItem";
            this.varListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.varListToolStripMenuItem.Text = "VarList";
            this.varListToolStripMenuItem.Click += new System.EventHandler(this.varListToolStripMenuItem_Click);
            // 
            // stepListToolStripMenuItem
            // 
            this.stepListToolStripMenuItem.Name = "stepListToolStripMenuItem";
            this.stepListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stepListToolStripMenuItem.Text = "StepList";
            this.stepListToolStripMenuItem.Click += new System.EventHandler(this.stepListToolStripMenuItem_Click);
            // 
            // tetraMathToolStripMenuItem
            // 
            this.tetraMathToolStripMenuItem.Name = "tetraMathToolStripMenuItem";
            this.tetraMathToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tetraMathToolStripMenuItem.Text = "Tetra_Math";
            this.tetraMathToolStripMenuItem.Click += new System.EventHandler(this.tetraMathToolStripMenuItem_Click);
            // 
            // pBNumberToolStripMenuItem
            // 
            this.pBNumberToolStripMenuItem.Name = "pBNumberToolStripMenuItem";
            this.pBNumberToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pBNumberToolStripMenuItem.Text = "PBNumber";
            this.pBNumberToolStripMenuItem.Click += new System.EventHandler(this.pBNumberToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTreeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // showTreeToolStripMenuItem
            // 
            this.showTreeToolStripMenuItem.Name = "showTreeToolStripMenuItem";
            this.showTreeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showTreeToolStripMenuItem.Text = "Show Tree";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 665);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(892, 15);
            this.progressBar1.TabIndex = 6;
            // 
            // tResult
            // 
            this.tResult.Location = new System.Drawing.Point(101, 589);
            this.tResult.Name = "tResult";
            this.tResult.Size = new System.Drawing.Size(541, 20);
            this.tResult.TabIndex = 9;
            // 
            // tInfelicity
            // 
            this.tInfelicity.Location = new System.Drawing.Point(101, 563);
            this.tInfelicity.Name = "tInfelicity";
            this.tInfelicity.Size = new System.Drawing.Size(541, 20);
            this.tInfelicity.TabIndex = 10;
            // 
            // bStart
            // 
            this.bStart.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bStart.BackgroundImage = global::PostBinary.Properties.Resources.bStartGray;
            this.bStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bStart.Enabled = false;
            this.bStart.FlatAppearance.BorderSize = 0;
            this.bStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStart.Location = new System.Drawing.Point(651, 27);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(51, 48);
            this.bStart.TabIndex = 2;
            this.bStart.UseVisualStyleBackColor = false;
            this.bStart.EnabledChanged += new System.EventHandler(this.bStart_EnabledChanged);
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // VarList
            // 
            this.VarList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.VarList.ItemHeight = 16;
            this.VarList.Location = new System.Drawing.Point(709, 97);
            this.VarList.Name = "VarList";
            this.VarList.Size = new System.Drawing.Size(171, 356);
            this.VarList.TabIndex = 15;
            this.VarList.DoubleClick += new System.EventHandler(this.VarList_DoubleClick);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(138, 78);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(564, 20);
            this.textBox3.TabIndex = 22;
            this.textBox3.Text = "Value";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(49, 78);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(93, 20);
            this.textBox2.TabIndex = 21;
            this.textBox2.Text = "Operation";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(20, 78);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(30, 20);
            this.textBox4.TabIndex = 20;
            this.textBox4.Text = "№";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(709, 78);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(61, 20);
            this.textBox1.TabIndex = 23;
            this.textBox1.Text = "Variable";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(767, 78);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(113, 20);
            this.textBox5.TabIndex = 24;
            this.textBox5.Text = "Value";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox7.Location = new System.Drawing.Point(19, 561);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(626, 24);
            this.textBox7.TabIndex = 26;
            this.textBox7.Text = "Infelicity";
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox8.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox8.Location = new System.Drawing.Point(19, 587);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(626, 24);
            this.textBox8.TabIndex = 27;
            this.textBox8.Text = "Result";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(678, 563);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(200, 20);
            this.textBox9.TabIndex = 28;
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox10.Font = new System.Drawing.Font("Meiryo UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox10.Location = new System.Drawing.Point(651, 561);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(229, 24);
            this.textBox10.TabIndex = 29;
            this.textBox10.Text = "%";
            // 
            // rTBInput
            // 
            this.rTBInput.Location = new System.Drawing.Point(20, 42);
            this.rTBInput.Name = "rTBInput";
            this.rTBInput.Size = new System.Drawing.Size(626, 20);
            this.rTBInput.TabIndex = 1;
            this.rTBInput.Text = "";
            this.rTBInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPress);
            // 
            // rTBLog
            // 
            this.rTBLog.Location = new System.Drawing.Point(20, 459);
            this.rTBLog.Name = "rTBLog";
            this.rTBLog.Size = new System.Drawing.Size(626, 96);
            this.rTBLog.TabIndex = 31;
            this.rTBLog.Text = "";
            // 
            // validationTimer
            // 
            this.validationTimer.Interval = 2000;
            this.validationTimer.Tick += new System.EventHandler(this.validationTimer_Tick);
            // 
            // commandTable1
            // 
            this.commandTable1.AutoScroll = true;
            this.commandTable1.AutoScrollMinSize = new System.Drawing.Size(250, 350);
            this.commandTable1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commandTable1.Location = new System.Drawing.Point(20, 97);
            this.commandTable1.Margin = new System.Windows.Forms.Padding(4);
            this.commandTable1.Name = "commandTable1";
            this.commandTable1.Size = new System.Drawing.Size(682, 355);
            this.commandTable1.TabIndex = 32;
            // 
            // srzToolStripMenuItem
            // 
            this.srzToolStripMenuItem.Name = "srzToolStripMenuItem";
            this.srzToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.srzToolStripMenuItem.Text = "srz";
            this.srzToolStripMenuItem.Click += new System.EventHandler(this.srzToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(892, 698);
            this.Controls.Add(this.rTBLog);
            this.Controls.Add(this.rTBInput);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.VarList);
            this.Controls.Add(this.tInfelicity);
            this.Controls.Add(this.tResult);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox4);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(908, 737);
            this.MinimumSize = new System.Drawing.Size(908, 737);
            this.Name = "MainForm";
            this.Text = "PostBinary Calculator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel tSSLStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.TextBox tResult;
        private System.Windows.Forms.TextBox tInfelicity;
        private System.Windows.Forms.ToolStripMenuItem helperToolStripMenuItem;
        private System.Windows.Forms.ListBox VarList;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem variableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem varListToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rTBInput;
        private System.Windows.Forms.RichTextBox rTBLog;
        private Components.CommandTable commandTable1;
        private System.Windows.Forms.Timer validationTimer;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tetraMathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pBNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem srzToolStripMenuItem;
    }
}

