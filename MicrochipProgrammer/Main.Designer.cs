namespace MicrochipProgrammer
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.txtSWPartOne = new System.Windows.Forms.TextBox();
            this.txtSWPartTwo = new System.Windows.Forms.TextBox();
            this.txtECL = new System.Windows.Forms.TextBox();
            this.cmdGetECL = new System.Windows.Forms.Button();
            this.lblSoftwarePrefix = new System.Windows.Forms.Label();
            this.lblDash = new System.Windows.Forms.Label();
            this.lblECL = new System.Windows.Forms.Label();
            this.cmdProgram = new System.Windows.Forms.Button();
            this.chkPowerTargetFromDevice = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbPIC16F716 = new System.Windows.Forms.RadioButton();
            this.rbPIC32MX440F128H = new System.Windows.Forms.RadioButton();
            this.rbPIC16F1718 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rt = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSWPartOne
            // 
            this.txtSWPartOne.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSWPartOne.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtSWPartOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSWPartOne.Location = new System.Drawing.Point(56, 19);
            this.txtSWPartOne.MaxLength = 5;
            this.txtSWPartOne.Name = "txtSWPartOne";
            this.txtSWPartOne.Size = new System.Drawing.Size(77, 26);
            this.txtSWPartOne.TabIndex = 0;
            // 
            // txtSWPartTwo
            // 
            this.txtSWPartTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSWPartTwo.Location = new System.Drawing.Point(158, 20);
            this.txtSWPartTwo.MaxLength = 2;
            this.txtSWPartTwo.Name = "txtSWPartTwo";
            this.txtSWPartTwo.Size = new System.Drawing.Size(42, 26);
            this.txtSWPartTwo.TabIndex = 1;
            // 
            // txtECL
            // 
            this.txtECL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtECL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtECL.Location = new System.Drawing.Point(255, 20);
            this.txtECL.MaxLength = 2;
            this.txtECL.Name = "txtECL";
            this.txtECL.Size = new System.Drawing.Size(38, 26);
            this.txtECL.TabIndex = 2;
            // 
            // cmdGetECL
            // 
            this.cmdGetECL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetECL.Location = new System.Drawing.Point(310, 19);
            this.cmdGetECL.Name = "cmdGetECL";
            this.cmdGetECL.Size = new System.Drawing.Size(91, 27);
            this.cmdGetECL.TabIndex = 3;
            this.cmdGetECL.Text = "Get ECL";
            this.cmdGetECL.UseVisualStyleBackColor = true;
            this.cmdGetECL.Click += new System.EventHandler(this.cmdGetECL_Click);
            // 
            // lblSoftwarePrefix
            // 
            this.lblSoftwarePrefix.AutoSize = true;
            this.lblSoftwarePrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoftwarePrefix.Location = new System.Drawing.Point(6, 22);
            this.lblSoftwarePrefix.Name = "lblSoftwarePrefix";
            this.lblSoftwarePrefix.Size = new System.Drawing.Size(45, 20);
            this.lblSoftwarePrefix.TabIndex = 4;
            this.lblSoftwarePrefix.Text = "240-";
            // 
            // lblDash
            // 
            this.lblDash.AutoSize = true;
            this.lblDash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDash.Location = new System.Drawing.Point(139, 23);
            this.lblDash.Name = "lblDash";
            this.lblDash.Size = new System.Drawing.Size(15, 20);
            this.lblDash.TabIndex = 5;
            this.lblDash.Text = "-";
            // 
            // lblECL
            // 
            this.lblECL.AutoSize = true;
            this.lblECL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblECL.Location = new System.Drawing.Point(206, 22);
            this.lblECL.Name = "lblECL";
            this.lblECL.Size = new System.Drawing.Size(43, 20);
            this.lblECL.TabIndex = 6;
            this.lblECL.Text = "ECL";
            // 
            // cmdProgram
            // 
            this.cmdProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdProgram.Location = new System.Drawing.Point(463, 294);
            this.cmdProgram.Name = "cmdProgram";
            this.cmdProgram.Size = new System.Drawing.Size(127, 48);
            this.cmdProgram.TabIndex = 8;
            this.cmdProgram.Text = "Program";
            this.cmdProgram.UseVisualStyleBackColor = true;
            this.cmdProgram.Click += new System.EventHandler(this.cmdProgram_Click);
            // 
            // chkPowerTargetFromDevice
            // 
            this.chkPowerTargetFromDevice.AutoSize = true;
            this.chkPowerTargetFromDevice.Location = new System.Drawing.Point(445, 176);
            this.chkPowerTargetFromDevice.Name = "chkPowerTargetFromDevice";
            this.chkPowerTargetFromDevice.Size = new System.Drawing.Size(172, 17);
            this.chkPowerTargetFromDevice.TabIndex = 7;
            this.chkPowerTargetFromDevice.Text = "Power target using programmer";
            this.chkPowerTargetFromDevice.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSWPartOne);
            this.groupBox1.Controls.Add(this.lblSoftwarePrefix);
            this.groupBox1.Controls.Add(this.txtSWPartTwo);
            this.groupBox1.Controls.Add(this.lblDash);
            this.groupBox1.Controls.Add(this.lblECL);
            this.groupBox1.Controls.Add(this.txtECL);
            this.groupBox1.Controls.Add(this.cmdGetECL);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 57);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Software Part Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(105, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 37);
            this.label1.TabIndex = 7;
            this.label1.Text = "(FAST.)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(205, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 32);
            this.label2.TabIndex = 10;
            this.label2.Text = "PIC/Microchip Programmer";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPIC16F716);
            this.groupBox2.Controls.Add(this.rbPIC32MX440F128H);
            this.groupBox2.Controls.Add(this.rbPIC16F1718);
            this.groupBox2.Location = new System.Drawing.Point(432, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 116);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Processor (mouse over for details)";
            // 
            // rbPIC16F716
            // 
            this.rbPIC16F716.AutoSize = true;
            this.rbPIC16F716.Location = new System.Drawing.Point(17, 69);
            this.rbPIC16F716.Name = "rbPIC16F716";
            this.rbPIC16F716.Size = new System.Drawing.Size(78, 17);
            this.rbPIC16F716.TabIndex = 2;
            this.rbPIC16F716.TabStop = true;
            this.rbPIC16F716.Tag = "PIC16F716";
            this.rbPIC16F716.Text = "PIC16F716";
            this.rbPIC16F716.UseVisualStyleBackColor = true;
            // 
            // rbPIC32MX440F128H
            // 
            this.rbPIC32MX440F128H.AutoSize = true;
            this.rbPIC32MX440F128H.Location = new System.Drawing.Point(17, 45);
            this.rbPIC32MX440F128H.Name = "rbPIC32MX440F128H";
            this.rbPIC32MX440F128H.Size = new System.Drawing.Size(120, 17);
            this.rbPIC32MX440F128H.TabIndex = 1;
            this.rbPIC32MX440F128H.TabStop = true;
            this.rbPIC32MX440F128H.Tag = "PIC32MX440F128H";
            this.rbPIC32MX440F128H.Text = "PIC32MX440F128H";
            this.rbPIC32MX440F128H.UseVisualStyleBackColor = true;
            // 
            // rbPIC16F1718
            // 
            this.rbPIC16F1718.AutoSize = true;
            this.rbPIC16F1718.Location = new System.Drawing.Point(17, 22);
            this.rbPIC16F1718.Name = "rbPIC16F1718";
            this.rbPIC16F1718.Size = new System.Drawing.Size(84, 17);
            this.rbPIC16F1718.TabIndex = 0;
            this.rbPIC16F1718.TabStop = true;
            this.rbPIC16F1718.Tag = "PIC16F1718";
            this.rbPIC16F1718.Text = "PIC16F1718";
            this.rbPIC16F1718.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rt);
            this.groupBox3.Location = new System.Drawing.Point(12, 117);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(414, 225);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // rt
            // 
            this.rt.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rt.Location = new System.Drawing.Point(10, 19);
            this.rt.Name = "rt";
            this.rt.ReadOnly = true;
            this.rt.Size = new System.Drawing.Size(391, 190);
            this.rt.TabIndex = 13;
            this.rt.TabStop = false;
            this.rt.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(531, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "v1.3, 9/26/18";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::MicrochipProgrammer.Properties.Resources.questionmark;
            this.pictureBox1.InitialImage = global::MicrochipProgrammer.Properties.Resources.questionmark;
            this.pictureBox1.Location = new System.Drawing.Point(479, 199);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // Main
            // 
            this.AcceptButton = this.cmdProgram;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 354);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkPowerTargetFromDevice);
            this.Controls.Add(this.cmdProgram);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PIC/Microchip Programmer";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSWPartOne;
        private System.Windows.Forms.TextBox txtSWPartTwo;
        private System.Windows.Forms.TextBox txtECL;
        private System.Windows.Forms.Button cmdGetECL;
        private System.Windows.Forms.Label lblSoftwarePrefix;
        private System.Windows.Forms.Label lblDash;
        private System.Windows.Forms.Label lblECL;
        private System.Windows.Forms.Button cmdProgram;
        private System.Windows.Forms.CheckBox chkPowerTargetFromDevice;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbPIC32MX440F128H;
        private System.Windows.Forms.RadioButton rbPIC16F1718;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rt;
        private System.Windows.Forms.RadioButton rbPIC16F716;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

