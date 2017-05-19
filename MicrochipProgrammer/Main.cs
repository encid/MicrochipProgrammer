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
            txtSWPartOne.Enter += (o, e) =>
            {
                if (txtSWPartOne.Text != string.Empty)
                    txtSWPartOne.SelectAll();
            };
            txtSWPartTwo.Enter += (o, e) =>
            {
                if (txtSWPartTwo.Text != string.Empty)
                    txtSWPartTwo.SelectAll();
            };

            foreach (TextBox textBox in this.Controls.OfType<TextBox>())
            {
                textBox.KeyDown += (o, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (o == txtSWPartOne || o == txtSWPartTwo)
                            cmdGetECL_Click(this, e);
                    }
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

            if (getECL(softwarePartNumber, softwarePartOne) != string.Empty)
            {
                txtECL.Text = getECL(softwarePartNumber, softwarePartOne);
            }                               
        }

        private string getECL(string softwarePartNumber, string softwarePartOne)
        {
            string softwareDir;            
            try
            {
                softwareDir = (from sd in Directory.GetDirectories(string.Format(@"{0}\240-{1}-XX\", VAULT_PATH, softwarePartOne))
                              where sd.Contains(softwarePartNumber)
                              select sd)
                              .FirstOrDefault();
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show(softwarePartNumber + " is an invalid software part number.");
                return string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error finding software directory: " + ex.Message);
                return string.Empty;
            }

            // Make sure the dir exists before attempting to get ECL subdirs
            if (!Directory.Exists(softwareDir))
            {
                MessageBox.Show(softwarePartNumber + " is an invalid software part number.");
                return string.Empty;
            }
            
            var eclDir = (from ed in Directory.GetDirectories(softwareDir)
                     where ed.Contains("ECL")
                     select ed)
                     .FirstOrDefault();

            // Make sure software dir has valid ECL subdirs
            if (eclDir == null || !eclDir.Contains("ECL"))
            {
                MessageBox.Show(softwarePartNumber + " has no valid ECLs available.");
                return string.Empty;
            }

            var tempECLStr = eclDir.Substring(eclDir.Length - 6);
            int dash = tempECLStr.IndexOf("-", StringComparison.CurrentCulture);
            return tempECLStr.Substring(dash + 1);
        }

    }
}
