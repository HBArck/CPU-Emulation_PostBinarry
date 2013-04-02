namespace PostBinary.Forms
{
    partial class HelperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cBConst = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cBLimit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lConstant = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBSwitcher = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lPolynom = new System.Windows.Forms.Label();
            this.cBPolynom = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cBConst
            // 
            this.cBConst.FormattingEnabled = true;
            this.cBConst.Location = new System.Drawing.Point(6, 91);
            this.cBConst.Name = "cBConst";
            this.cBConst.Size = new System.Drawing.Size(232, 21);
            this.cBConst.TabIndex = 0;
            this.cBConst.SelectedIndexChanged += new System.EventHandler(this.cBConst_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose constant";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(250, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Copy to ClipBoard";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cBLimit
            // 
            this.cBLimit.FormattingEnabled = true;
            this.cBLimit.Items.AddRange(new object[] {
            "6",
            "14",
            "31",
            "65"});
            this.cBLimit.Location = new System.Drawing.Point(6, 40);
            this.cBLimit.Name = "cBLimit";
            this.cBLimit.Size = new System.Drawing.Size(83, 21);
            this.cBLimit.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Number of digits";
            // 
            // lConstant
            // 
            this.lConstant.AutoSize = true;
            this.lConstant.Location = new System.Drawing.Point(6, 115);
            this.lConstant.MaximumSize = new System.Drawing.Size(232, 21);
            this.lConstant.MinimumSize = new System.Drawing.Size(232, 21);
            this.lConstant.Name = "lConstant";
            this.lConstant.Size = new System.Drawing.Size(232, 21);
            this.lConstant.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lConstant);
            this.groupBox1.Controls.Add(this.cBConst);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cBLimit);
            this.groupBox1.Location = new System.Drawing.Point(15, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 147);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Constants";
            // 
            // cBSwitcher
            // 
            this.cBSwitcher.FormattingEnabled = true;
            this.cBSwitcher.Items.AddRange(new object[] {
            "Constants",
            "Polynoms"});
            this.cBSwitcher.Location = new System.Drawing.Point(21, 12);
            this.cBSwitcher.Name = "cBSwitcher";
            this.cBSwitcher.Size = new System.Drawing.Size(223, 21);
            this.cBSwitcher.TabIndex = 7;
            this.cBSwitcher.SelectedIndexChanged += new System.EventHandler(this.cBSwitcher_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lPolynom);
            this.groupBox2.Controls.Add(this.cBPolynom);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(15, 194);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 128);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Expression";
            // 
            // lPolynom
            // 
            this.lPolynom.AutoSize = true;
            this.lPolynom.Location = new System.Drawing.Point(6, 65);
            this.lPolynom.MinimumSize = new System.Drawing.Size(232, 21);
            this.lPolynom.Name = "lPolynom";
            this.lPolynom.Size = new System.Drawing.Size(232, 21);
            this.lPolynom.TabIndex = 7;
            // 
            // cBPolynom
            // 
            this.cBPolynom.FormattingEnabled = true;
            this.cBPolynom.Location = new System.Drawing.Point(6, 41);
            this.cBPolynom.Name = "cBPolynom";
            this.cBPolynom.Size = new System.Drawing.Size(232, 21);
            this.cBPolynom.TabIndex = 6;
            this.cBPolynom.SelectedIndexChanged += new System.EventHandler(this.cBPolynom_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Expression name";
            // 
            // HelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cBSwitcher);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "HelperForm";
            this.Text = "Helper";
            this.Load += new System.EventHandler(this.Helper_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cBConst;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cBLimit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lConstant;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cBSwitcher;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBPolynom;
        private System.Windows.Forms.Label lPolynom;
    }
}