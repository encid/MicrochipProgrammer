/*
 * Microchip Programmer
 * designed by: Ryan Cavallaro
 * 
 * 
 * TODO:
 * ----------------------
 * 1.  Check ARCHIVE / subfolders for ECL in ECL textbox if that ecl# is not found in the top level software dir (for custom flash)
 * 
 * 
 * 
 */

using System;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace MicrochipProgrammer
{
    public partial class Main : Form
    {
        public const string VAULT_PATH = @"\\pandora\vault\Released_Part_Information\240-xxxxx-xx_Software\240-9XXXX-XX\240-91XXX-XX";

        public Main()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            foreach (TextBox textBox in this.Controls.OfType<TextBox>())
            {
                textBox.Enter += (o, e) =>
                {
                    if (textBox.Text != string.Empty)
                        textBox.SelectAll();
                };
                textBox.KeyDown += (o, e) =>
                {
                    if (e.KeyCode == Keys.Enter && (o == txtSWPartOne || o == txtSWPartTwo))
                        cmdGetECL_Click(this, e);
                };
                textBox.TextChanged += (o, e) =>
                {
                    if (o == txtSWPartOne && txtSWPartOne.TextLength > 4)                    
                        txtSWPartTwo.Focus();                      
                };
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void cmdGetECL_Click(object sender, EventArgs e)
        {
            var softwarePartOne = txtSWPartOne.Text;
            var softwarePartTwo = txtSWPartTwo.Text;
            var softwarePartNumber = string.Format("240-{0}-{1}", softwarePartOne, softwarePartTwo);

            if (getECL(softwarePartNumber, softwarePartOne) == string.Empty)
                txtECL.Text = string.Empty;
            else
                txtECL.Text = getECL(softwarePartNumber, softwarePartOne);
        }

        private string getECL(string softwarePartNumber, string softwarePartOne)
        {
            int dash;
            string tempECLStr;

            try
            {
                var softwareDir = (from sd in Directory.GetDirectories(string.Format(@"{0}\240-{1}-XX\", VAULT_PATH, softwarePartOne))
                                  where sd.Contains(softwarePartNumber)
                                  select sd)
                                  .FirstOrDefault();
            
                var eclDir = (from ed in Directory.GetDirectories(softwareDir)
                             where ed.Contains("ECL")
                             select ed)
                             .FirstOrDefault();
                
                tempECLStr = eclDir.Substring(eclDir.Length - 6);
                dash = tempECLStr.IndexOf("-", StringComparison.CurrentCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(softwarePartNumber + " is an invalid software part number.");
                return string.Empty;
            }

            return tempECLStr.Substring(dash + 1);
        }
    }
}
