/*
 * Microchip Programmer
 * designed by: Ryan Cavallaro
 * 
 * 
 * TODO:
 * ----------------------
 * 
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
            // Handle textbox events
            foreach (TextBox textBox in this.Controls.OfType<TextBox>())
            {
                // Select all text upon entering a textbox
                textBox.Enter += (o, e) =>
                {
                    if (textBox.Text != string.Empty)
                        textBox.SelectAll();
                };
                // Call GetLatestECL method upon enter being pressed in textbox
                textBox.KeyDown += (o, e) =>
                {
                    if (e.KeyCode == Keys.Enter && (o == txtSWPartOne || o == txtSWPartTwo))
                        cmdGetECL_Click(this, e);                    
                };
                // Move to next textbox automatically upon 5th char being entered in txtSWPartOne
                textBox.TextChanged += (o, e) =>
                {
                    if (o == txtSWPartOne && txtSWPartOne.TextLength > 4)                    
                        txtSWPartTwo.Focus();                      
                };
            }
        }

        private void cmdGetECL_Click(object sender, EventArgs e)
        {                      
            var softwarePartOne = txtSWPartOne.Text;
            var softwarePartTwo = txtSWPartTwo.Text;
            var softwarePartNumber = string.Format("240-{0}-{1}", softwarePartOne, softwarePartTwo);

            if (softwarePartOne.Length < 5 || softwarePartTwo.Length < 2)
            {
                MessageBox.Show(softwarePartNumber + " is an invalid software part number.");
                return;
            }

            if (GetLatestECL(softwarePartNumber) == string.Empty)
                txtECL.Text = string.Empty;
            else
                txtECL.Text = GetLatestECL(softwarePartNumber);
        }

        private void cmdProgram_Click(object sender, EventArgs e)
        {
            var softwarePartOne = txtSWPartOne.Text;
            var softwarePartTwo = txtSWPartTwo.Text;
            var softwarePartNumber = string.Format("240-{0}-{1}", softwarePartOne, softwarePartTwo);

            if (softwarePartOne.Length < 5 || softwarePartTwo.Length < 2)
            {
                MessageBox.Show(softwarePartNumber + " is an invalid software part number.");
                return;
            }

            var dirs = getSoftwareDirectories(softwarePartNumber);
            var ECL = txtECL.Text;

            if (dirs == null)
            {
                MessageBox.Show(softwarePartNumber + " is an invalid software part number.");
                return;
            }
            if (ECL == "")
            {
                MessageBox.Show("Enter a valid ECL, or click 'Get ECL' button.");
                return;
            }            
            
            var softwareDir = (from sd in dirs
                               where sd.Contains("ECL-" + ECL)
                               select sd)
                               .FirstOrDefault();

            if (string.IsNullOrEmpty(softwareDir))
            {
                MessageBox.Show("Enter a valid ECL, or click 'Get ECL' button.");
                return;
            }

        }

        private IEnumerable<string> getSoftwareDirectories(string softwarePartNumber)
        {
            var softwarePartOne = softwarePartNumber.Substring(4, 5);
            var path = string.Format(@"{0}\240-{1}-XX\{2}", VAULT_PATH, softwarePartOne, softwarePartNumber);

            if (!Directory.Exists(path))
                return null;
                        
            var dirs = Directory.EnumerateDirectories(path, "*", System.IO.SearchOption.AllDirectories).ToList();

            return dirs.Where(d => d.Contains("ECL"));
        }

        private string GetLatestECL(string softwarePartNumber)
        {
            var softwarePartOne = softwarePartNumber.Substring(4, 5);
            int dashIndex;
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
                dashIndex = tempECLStr.IndexOf("-", StringComparison.CurrentCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(softwarePartNumber + " is an invalid software part number.");
                return string.Empty;
            }

            return tempECLStr.Substring(dashIndex + 1);
        }

    }
}
