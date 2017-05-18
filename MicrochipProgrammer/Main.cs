/*
 * Microchip Programmer
 * designed by: Ryan Cavallaro
 * 
 * 
 * TODO:
 * ----------------------
 * 1.  Move 'Get ECL' into its own method
 * 2.  Check ARCHIVE / subfolders for ECL in ECL textbox if that ecl# is not found in the top level software dir (for custom flash)
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
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void cmdGetECL_Click(object sender, EventArgs e)
        {
            var softwarePartOne = txtSWPartOne.Text;
            var softwarePartTwo = txtSWPartTwo.Text;
            var softwarePartNumber = string.Format("240-{0}-{1}", softwarePartOne, softwarePartTwo);
            var softwareDir = "";
            var eclDir = "";
            try
            {                
                softwareDir =
                    (from sd in Directory.GetDirectories(string.Format(@"{0}\240-{1}-XX\", VAULT_PATH, softwarePartOne))
                     where sd.Contains(softwarePartNumber)
                     select sd)
                    .FirstOrDefault();
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(softwarePartNumber + " is an invalid software part number.");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error finding software directory: " + ex.Message);
                return;
            }
            
            if (Directory.Exists(softwareDir))
            {
                eclDir =
                    (from ed in Directory.GetDirectories(softwareDir)
                    where ed.Contains("ECL")
                    select ed)
                    .FirstOrDefault(); 
            }
            else
            {
                MessageBox.Show(softwarePartNumber + " is an invalid software part number.");
                return;
            }

            var tempECLStr = eclDir.Substring(eclDir.Length - 6);
            int dash = tempECLStr.IndexOf("-", StringComparison.CurrentCulture);
            var softwareECL = tempECLStr.Substring(dash + 1);
            txtECL.Text = softwareECL;
        }
    }
}
