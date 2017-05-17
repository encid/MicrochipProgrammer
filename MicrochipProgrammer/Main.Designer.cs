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
            this.SuspendLayout();
            // 
            // txtSWPartOne
            // 
            this.txtSWPartOne.Location = new System.Drawing.Point(68, 50);
            this.txtSWPartOne.Name = "txtSWPartOne";
            this.txtSWPartOne.Size = new System.Drawing.Size(78, 20);
            this.txtSWPartOne.TabIndex = 0;
            // 
            // txtSWPartTwo
            // 
            this.txtSWPartTwo.Location = new System.Drawing.Point(163, 50);
            this.txtSWPartTwo.Name = "txtSWPartTwo";
            this.txtSWPartTwo.Size = new System.Drawing.Size(42, 20);
            this.txtSWPartTwo.TabIndex = 1;
            // 
            // txtECL
            // 
            this.txtECL.Location = new System.Drawing.Point(231, 50);
            this.txtECL.Name = "txtECL";
            this.txtECL.Size = new System.Drawing.Size(38, 20);
            this.txtECL.TabIndex = 2;
            // 
            // cmdGetECL
            // 
            this.cmdGetECL.Location = new System.Drawing.Point(294, 48);
            this.cmdGetECL.Name = "cmdGetECL";
            this.cmdGetECL.Size = new System.Drawing.Size(75, 23);
            this.cmdGetECL.TabIndex = 3;
            this.cmdGetECL.Text = "Get ECL";
            this.cmdGetECL.UseVisualStyleBackColor = true;
            this.cmdGetECL.Click += new System.EventHandler(this.cmdGetECL_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 297);
            this.Controls.Add(this.cmdGetECL);
            this.Controls.Add(this.txtECL);
            this.Controls.Add(this.txtSWPartTwo);
            this.Controls.Add(this.txtSWPartOne);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSWPartOne;
        private System.Windows.Forms.TextBox txtSWPartTwo;
        private System.Windows.Forms.TextBox txtECL;
        private System.Windows.Forms.Button cmdGetECL;
    }
}

