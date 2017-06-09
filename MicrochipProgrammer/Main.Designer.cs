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
            this.txtSWPartOne = new System.Windows.Forms.TextBox();
            this.txtSWPartTwo = new System.Windows.Forms.TextBox();
            this.txtECL = new System.Windows.Forms.TextBox();
            this.cmdGetECL = new System.Windows.Forms.Button();
            this.lblSoftwarePrefix = new System.Windows.Forms.Label();
            this.lblDash = new System.Windows.Forms.Label();
            this.lblECL = new System.Windows.Forms.Label();
            this.cmdProgram = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSWPartOne
            // 
            this.txtSWPartOne.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSWPartOne.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtSWPartOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSWPartOne.Location = new System.Drawing.Point(64, 49);
            this.txtSWPartOne.MaxLength = 5;
            this.txtSWPartOne.Name = "txtSWPartOne";
            this.txtSWPartOne.Size = new System.Drawing.Size(77, 26);
            this.txtSWPartOne.TabIndex = 0;
            // 
            // txtSWPartTwo
            // 
            this.txtSWPartTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSWPartTwo.Location = new System.Drawing.Point(166, 50);
            this.txtSWPartTwo.MaxLength = 2;
            this.txtSWPartTwo.Name = "txtSWPartTwo";
            this.txtSWPartTwo.Size = new System.Drawing.Size(42, 26);
            this.txtSWPartTwo.TabIndex = 1;
            // 
            // txtECL
            // 
            this.txtECL.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtECL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtECL.Location = new System.Drawing.Point(263, 50);
            this.txtECL.MaxLength = 2;
            this.txtECL.Name = "txtECL";
            this.txtECL.Size = new System.Drawing.Size(38, 26);
            this.txtECL.TabIndex = 2;
            // 
            // cmdGetECL
            // 
            this.cmdGetECL.Location = new System.Drawing.Point(322, 48);
            this.cmdGetECL.Name = "cmdGetECL";
            this.cmdGetECL.Size = new System.Drawing.Size(75, 23);
            this.cmdGetECL.TabIndex = 3;
            this.cmdGetECL.Text = "Get ECL";
            this.cmdGetECL.UseVisualStyleBackColor = true;
            this.cmdGetECL.Click += new System.EventHandler(this.cmdGetECL_Click);
            // 
            // lblSoftwarePrefix
            // 
            this.lblSoftwarePrefix.AutoSize = true;
            this.lblSoftwarePrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoftwarePrefix.Location = new System.Drawing.Point(14, 52);
            this.lblSoftwarePrefix.Name = "lblSoftwarePrefix";
            this.lblSoftwarePrefix.Size = new System.Drawing.Size(45, 20);
            this.lblSoftwarePrefix.TabIndex = 4;
            this.lblSoftwarePrefix.Text = "240-";
            // 
            // lblDash
            // 
            this.lblDash.AutoSize = true;
            this.lblDash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDash.Location = new System.Drawing.Point(147, 53);
            this.lblDash.Name = "lblDash";
            this.lblDash.Size = new System.Drawing.Size(15, 20);
            this.lblDash.TabIndex = 5;
            this.lblDash.Text = "-";
            // 
            // lblECL
            // 
            this.lblECL.AutoSize = true;
            this.lblECL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblECL.Location = new System.Drawing.Point(214, 52);
            this.lblECL.Name = "lblECL";
            this.lblECL.Size = new System.Drawing.Size(43, 20);
            this.lblECL.TabIndex = 6;
            this.lblECL.Text = "ECL";
            // 
            // cmdProgram
            // 
            this.cmdProgram.Location = new System.Drawing.Point(141, 130);
            this.cmdProgram.Name = "cmdProgram";
            this.cmdProgram.Size = new System.Drawing.Size(75, 23);
            this.cmdProgram.TabIndex = 7;
            this.cmdProgram.Text = "Program";
            this.cmdProgram.UseVisualStyleBackColor = true;
            this.cmdProgram.Click += new System.EventHandler(this.cmdProgram_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 211);
            this.Controls.Add(this.cmdProgram);
            this.Controls.Add(this.lblECL);
            this.Controls.Add(this.lblDash);
            this.Controls.Add(this.lblSoftwarePrefix);
            this.Controls.Add(this.cmdGetECL);
            this.Controls.Add(this.txtECL);
            this.Controls.Add(this.txtSWPartTwo);
            this.Controls.Add(this.txtSWPartOne);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
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
    }
}

