/*
 * Microchip Programmer
 * designed by: Ryan Cavallaro
 * NOTE:  Include PICKit3 directory with .exe.  Point to directory path in config file.
 * 
 * TODO:
 * ----------------------
 * 
 * 
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Configuration;

namespace MicrochipProgrammer
{
    public partial class Main : Form
    {        
        public string VAULT_PATH = ConfigurationManager.AppSettings["VAULT_PATH"];
        public string PICKIT_PATH = ConfigurationManager.AppSettings["PICKIT_PATH"];

        public Main()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            // Handle textbox events
            foreach (TextBox textBox in groupBox1.Controls.OfType<TextBox>())
            {
                // Select all text upon entering a textbox
                textBox.Enter += (o, e) =>
                {
                    if (textBox.Text != string.Empty)
                        textBox.SelectAll();
                };
                // Call GetECL button click method upon enter being pressed in SW textbox
                textBox.KeyDown += (o, e) =>
                {
                    if (e.KeyCode == Keys.Enter && (o == txtSWPartOne || o == txtSWPartTwo))
                        cmdGetECL.PerformClick();
                };
                // Move to next textbox automatically upon 5th char being entered in txtSWPartOne
                textBox.TextChanged += (o, e) =>
                {
                    if (o == txtSWPartOne && txtSWPartOne.TextLength > 4)                    
                        txtSWPartTwo.Focus();                      
                };
                // Make sure textbox stays at the most recent line(bottom most)
                rt.TextChanged += (o, e) =>
                {
                    if (rt.Visible)
                        rt.ScrollToCaret();
                };
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Give focus to software p/n textbox upon program being opened
            this.ActiveControl = txtSWPartOne;
            rbPIC16F1718.Checked = true;
            Logger.Log("Application started - ready to program.", rt);
        }

        private void cmdGetECL_Click(object sender, EventArgs e)
        {                      
            var softwarePartOne = txtSWPartOne.Text;
            var softwarePartTwo = txtSWPartTwo.Text;
            var softwarePartNumber = string.Format("240-{0}-{1}", softwarePartOne, softwarePartTwo);

            if (softwarePartOne.Length < 5 || softwarePartTwo.Length < 2)
            {
                Logger.Log($"\nError: {softwarePartNumber} is not a valid software part number.", rt);
                txtSWPartOne.Focus();
                txtSWPartOne.SelectAll();
                return;
            }

            try
            {
                if (GetLatestECL(softwarePartNumber) == string.Empty)
                {
                    Logger.Log($"\nError: {softwarePartNumber} is not a valid software part number.", rt);
                    txtECL.Text = string.Empty;
                }
                else
                    txtECL.Text = GetLatestECL(softwarePartNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmdProgram_Click(object sender, EventArgs e)
        {
            var softwarePartOne = txtSWPartOne.Text;
            var softwarePartTwo = txtSWPartTwo.Text;
            var softwarePartNumber = string.Format("240-{0}-{1}", softwarePartOne, softwarePartTwo);

            // Error checking

            if (softwarePartOne.Length < 5 || softwarePartTwo.Length < 2)
            {
                Logger.Log($"\nError: {softwarePartNumber} is an invalid software part number.", rt);
                txtSWPartOne.Focus();
                txtSWPartOne.SelectAll();
                return;
            }

            var dirs = getSoftwareDirectories(softwarePartNumber);
            var ECL = txtECL.Text;

            if (dirs == null)
            {
                Logger.Log($"\nError: {softwarePartNumber} is an invalid software part number.", rt);
                txtSWPartOne.Focus();
                txtSWPartOne.SelectAll();
                return;
            }
            if (ECL == "")
            {
                Logger.Log($"\nError: No ECL entered; please enter an ECL number.", rt);
                return;
            }            
            
            var softwareDir = (from sd in dirs
                               where sd.Contains("ECL-" + ECL)
                               select sd)
                               .FirstOrDefault();

            if (string.IsNullOrEmpty(softwareDir))
            {
                Logger.Log($"\nError: Invalid ECL entered; please enter a valid ECL or click 'Get ECL'.", rt);
                return;
            }

            // Valid directory found, now get hex file name from directory

            var files = Directory.GetFiles(softwareDir, "*.hex", System.IO.SearchOption.TopDirectoryOnly);
            if (!files.Any())
            {
                Logger.Log($"\nError: {softwarePartNumber} ECL-{ECL} does not use PIC/Microchip; can not program with this utility.", rt);
                txtSWPartOne.Focus();
                txtSWPartOne.SelectAll();
                return;
            }
            var softwareFile = files[0];

            // Check what processor and whether to use programmer to supply voltage
            string processor = "";
            double voltage = 0;

            if (rbPIC16F1718.Checked)
            {
                processor = "P16F1718";
                if (chkPowerTargetFromDevice.Checked)
                    voltage = 5.0;
            }
            if (rbPIC32MX440F128H.Checked)
            {
                processor = "P32MX440F128H";
                if (chkPowerTargetFromDevice.Checked)
                    voltage = 3.3;
            }
            if (rbPIC16F716.Checked)
            {
                processor = "P16F716";
                if (chkPowerTargetFromDevice.Checked)
                    voltage = 5.0;
            }

            Logger.Log($"\nStarting to program {softwarePartNumber} ECL-{ECL}...", rt);

            //switch (doProgram(softwareFile, processor, voltage))
            //{
            //    case 0:
            //    Logger.Log("\nProgramming successful.", rt, System.Drawing.Color.Green);
            //    break;                
            //    default:
            //    Logger.Log("\nFailed to program device.", rt, System.Drawing.Color.Red);
            //    break;
            //}
            disableUI();
            doProgram(softwareFile, processor, voltage);
            enableUI();
        }

        private int doProgram(string file, string processor, double voltage)
        {
            if (file == "")
            {
                MessageBox.Show("Error: No file selected.");
                return -1;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            //startInfo.UseShellExecute = false;
            startInfo.FileName = $@"{PICKIT_PATH}\PK3CMD.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = string.Format("/{0} /M /L /F\"{1}\"", processor, file);

            if (voltage > 0)
            {
                startInfo.Arguments += string.Format($@" /V{voltage.ToString()}");
            }

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                    switch (exeProcess.ExitCode)
                    {
                        case 0:
                        Logger.Log("Device programmed successfully!", rt, System.Drawing.Color.Green);
                        break;
                        case 1:
                        Logger.Log("Failure: HEX File not found (or invalid).", rt, System.Drawing.Color.Red);
                        break;
                        case 2:
                        Logger.Log("Failure: Communication with PICKIT3 lost.", rt, System.Drawing.Color.Red);
                        break;
                        case 5:
                        Logger.Log("Failure: PICKIT3 not detected or incorrect device connected.", rt, System.Drawing.Color.Red);
                        break;
                        case 7:
                        Logger.Log("Failure: Device is not powered; select 'Power target using programmer' or supply external power source.", rt, System.Drawing.Color.Red);
                        break;
                        case 9:
                        Logger.Log("Failure: PICKIT3 not detected or incorrect device connected.", rt, System.Drawing.Color.Red);
                        break;
                        case 36:
                        Logger.Log("Failure: HEX File not found (or invalid).", rt, System.Drawing.Color.Red);
                        break;
                        default:
                        Logger.Log("Unknown failure. Exited with code " + exeProcess.ExitCode, rt, System.Drawing.Color.Red);
                        break;
                    }
                    return exeProcess.ExitCode;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to program device. Error: " + ex.Message, rt, System.Drawing.Color.Red);
                return -1;
            }
        }

        private void disableUI()
        {
            txtSWPartOne.Enabled = false;
            txtSWPartTwo.Enabled = false;
            txtECL.Enabled = false;
            cmdGetECL.Enabled = false;
            cmdProgram.Enabled = false;
            groupBox2.Enabled = false;
            chkPowerTargetFromDevice.Enabled = false;
        }

        private void enableUI()
        {
            txtSWPartOne.Enabled = true;
            txtSWPartTwo.Enabled = true;
            txtECL.Enabled = true;
            cmdGetECL.Enabled = true;
            cmdProgram.Enabled = true;
            groupBox2.Enabled = true;
            chkPowerTargetFromDevice.Enabled = true;
        }

        private IEnumerable<string> getSoftwareDirectories(string softwarePartNumber)
        {
            var softwarePartOne = softwarePartNumber.Substring(4, 5);

            //set software vault path to appropriate number depending on what the software pn starts with
            string softwarePrePath = string.Format(@"240-{0}XXXX-XX\240-{1}XXX-XX", softwarePartOne[0], softwarePartOne.Substring(0, 2));

            var path = string.Format(@"{1}\{0}\240-{2}-XX\{3}", softwarePrePath, VAULT_PATH, softwarePartOne, softwarePartNumber);

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

            //set software vault path to appropriate number depending on what the software pn starts with
            string softwarePrePath = string.Format(@"240-{0}XXXX-XX\240-{1}XXX-XX", softwarePartOne[0], softwarePartOne.Substring(0,2));

            try
            {
                var softwareDir = (from sd in Directory.GetDirectories(string.Format(@"{1}\{0}\240-{2}-XX\", softwarePrePath, VAULT_PATH, softwarePartOne))
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
            catch (Exception)
            {
                return string.Empty;
            }

            return tempECLStr.Substring(dashIndex + 1);
        }
    }
}
