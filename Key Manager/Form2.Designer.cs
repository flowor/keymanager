namespace Key_Manager
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.textBoxKeyCodeRm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxEditEntry = new System.Windows.Forms.GroupBox();
            this.textBoxKeyCodeApt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxLname = new System.Windows.Forms.TextBox();
            this.textBoxFname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSid = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBoxEditEntry.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxKeyCodeRm
            // 
            this.textBoxKeyCodeRm.Location = new System.Drawing.Point(6, 24);
            this.textBoxKeyCodeRm.Name = "textBoxKeyCodeRm";
            this.textBoxKeyCodeRm.Size = new System.Drawing.Size(122, 20);
            this.textBoxKeyCodeRm.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Key Code (Room)";
            // 
            // groupBoxEditEntry
            // 
            this.groupBoxEditEntry.Controls.Add(this.label1);
            this.groupBoxEditEntry.Controls.Add(this.label2);
            this.groupBoxEditEntry.Controls.Add(this.textBoxKeyCodeApt);
            this.groupBoxEditEntry.Controls.Add(this.textBoxKeyCodeRm);
            this.groupBoxEditEntry.Location = new System.Drawing.Point(12, 12);
            this.groupBoxEditEntry.Name = "groupBoxEditEntry";
            this.groupBoxEditEntry.Size = new System.Drawing.Size(252, 89);
            this.groupBoxEditEntry.TabIndex = 2;
            this.groupBoxEditEntry.TabStop = false;
            this.groupBoxEditEntry.Text = "HK-3101-A";
            // 
            // textBoxKeyCodeApt
            // 
            this.textBoxKeyCodeApt.Location = new System.Drawing.Point(6, 50);
            this.textBoxKeyCodeApt.Name = "textBoxKeyCodeApt";
            this.textBoxKeyCodeApt.Size = new System.Drawing.Size(122, 20);
            this.textBoxKeyCodeApt.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key Code (APT)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Student ID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "First Name";
            // 
            // textBoxLname
            // 
            this.textBoxLname.Location = new System.Drawing.Point(142, 134);
            this.textBoxLname.Name = "textBoxLname";
            this.textBoxLname.Size = new System.Drawing.Size(122, 20);
            this.textBoxLname.TabIndex = 4;
            // 
            // textBoxFname
            // 
            this.textBoxFname.Location = new System.Drawing.Point(14, 134);
            this.textBoxFname.Name = "textBoxFname";
            this.textBoxFname.Size = new System.Drawing.Size(122, 20);
            this.textBoxFname.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Last Name";
            // 
            // textBoxSid
            // 
            this.textBoxSid.Location = new System.Drawing.Point(14, 183);
            this.textBoxSid.Name = "textBoxSid";
            this.textBoxSid.Size = new System.Drawing.Size(122, 20);
            this.textBoxSid.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(142, 180);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Edit Entry";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(207, 180);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click_1);
            // 
            // Form2
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(277, 223);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxLname);
            this.Controls.Add(this.textBoxFname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxSid);
            this.Controls.Add(this.groupBoxEditEntry);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Edit Entry";
            this.TopMost = true;
            this.groupBoxEditEntry.ResumeLayout(false);
            this.groupBoxEditEntry.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxKeyCodeRm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxEditEntry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxKeyCodeApt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxLname;
        private System.Windows.Forms.TextBox textBoxFname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}