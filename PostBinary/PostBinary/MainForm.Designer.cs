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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tSSLStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tResult = new System.Windows.Forms.TextBox();
            this.tInfelicity = new System.Windows.Forms.TextBox();
            this.lInfelicity = new System.Windows.Forms.Label();
            this.lResult = new System.Windows.Forms.Label();
            this.bStart = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.customTextBox1 = new MyControls.CustomTextBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSSLStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 677);
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
            this.toolStripMenuItem1});
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
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
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
            this.preferencesToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 665);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(892, 15);
            this.progressBar1.TabIndex = 6;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(19, 83);
            this.listBox1.Margin = new System.Windows.Forms.Padding(5);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(739, 238);
            this.listBox1.TabIndex = 8;
            // 
            // tResult
            // 
            this.tResult.Location = new System.Drawing.Point(90, 617);
            this.tResult.Name = "tResult";
            this.tResult.Size = new System.Drawing.Size(668, 20);
            this.tResult.TabIndex = 9;
            // 
            // tInfelicity
            // 
            this.tInfelicity.Location = new System.Drawing.Point(90, 565);
            this.tInfelicity.Name = "tInfelicity";
            this.tInfelicity.Size = new System.Drawing.Size(668, 20);
            this.tInfelicity.TabIndex = 10;
            // 
            // lInfelicity
            // 
            this.lInfelicity.AutoSize = true;
            this.lInfelicity.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInfelicity.Location = new System.Drawing.Point(15, 565);
            this.lInfelicity.Name = "lInfelicity";
            this.lInfelicity.Size = new System.Drawing.Size(74, 22);
            this.lInfelicity.TabIndex = 11;
            this.lInfelicity.Text = "Infelicity";
            // 
            // lResult
            // 
            this.lResult.AutoSize = true;
            this.lResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lResult.Location = new System.Drawing.Point(15, 617);
            this.lResult.Name = "lResult";
            this.lResult.Size = new System.Drawing.Size(61, 22);
            this.lResult.TabIndex = 12;
            this.lResult.Text = "Result";
            // 
            // bStart
            // 
            this.bStart.BackgroundImage = global::PostBinary.Properties.Resources.bStartGray;
            this.bStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bStart.Enabled = false;
            this.bStart.FlatAppearance.BorderSize = 0;
            this.bStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStart.Location = new System.Drawing.Point(806, 41);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(39, 39);
            this.bStart.TabIndex = 7;
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.EnabledChanged += new System.EventHandler(this.bStart_EnabledChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(784, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "validate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // customTextBox1
            // 
            this.customTextBox1.Location = new System.Drawing.Point(19, 55);
            this.customTextBox1.MaxTextLength = 0;
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.Size = new System.Drawing.Size(740, 20);
            this.customTextBox1.TabIndex = 14;
            this.customTextBox1.WindowHeight = 0;
            this.customTextBox1.WindowSize = new System.Drawing.Size(0, 0);
            this.customTextBox1.WindowWidth = 500;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(892, 699);
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lResult);
            this.Controls.Add(this.lInfelicity);
            this.Controls.Add(this.tInfelicity);
            this.Controls.Add(this.tResult);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "PostBinary";
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
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox tResult;
        private System.Windows.Forms.TextBox tInfelicity;
        private System.Windows.Forms.Label lInfelicity;
        private System.Windows.Forms.Label lResult;
        private System.Windows.Forms.Button button1;
        private MyControls.CustomTextBox customTextBox1;
    }
}

