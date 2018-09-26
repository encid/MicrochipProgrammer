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

            ToolTip toolTip = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip.SetToolTip(rbPIC16F1718, "Grindmaster");
            toolTip.SetToolTip(rbPIC32MX440F128H, "Fast Transfer");
            toolTip.SetToolTip(rbPIC16F716, "Modularm");            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Give focus to software p/n textbox upon program being opened
            this.ActiveControl = txtSWPartOne;
            rbPIC16F1718.Checked = true;
            Logger.Log("Application started - ready to program.", rt);

            // Make sure config file points to PK3CMD exe
            if (!File.Exists(PICKIT_PATH + "pk3cmd.exe"))
            {
                MessageBox.Show($"Warning: PK3CMD.exe not found at {PICKIT_PATH}.\nCheck the configuration file before proceeding.", "PIC/Microchip Programmer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmdGetECL_Click(object sender, EventArgs e)
        {                      
            var softwarePartOne = txtSWPartOne.Text;
            var softwarePartTwo = txtSWPartTwo.Text;
            var softwarePartNumber = $"240-{softwarePartOne}-{softwarePartTwo}";

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
            var softwarePartNumber = $"240-{softwarePartOne}-{softwarePartTwo}";
            pictureBox1.Image = Properties.Resources.questionmark;

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

            var softwareDir = dirs.FirstOrDefault(p => p.Contains($"ECL-{ECL}"));

            if (string.IsNullOrEmpty(softwareDir))
            {
                Logger.Log($"\nError: Invalid ECL entered; please enter a valid ECL or click 'Get ECL'.", rt);
                return;
            }

            // Valid directory found, now get hex file name from directory

            var files = Directory.GetFiles(softwareDir, "*.hex", SearchOption.TopDirectoryOnly);
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
            startInfo.UseShellExecute = false;
            startInfo.FileName = $@"{PICKIT_PATH}\PK3CMD.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = $"/{processor} /M /L /F\"{file}\"";

            if (voltage > 0)
            {
                startInfo.Arguments += $@" /V{voltage.ToString()}";
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
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.check;
                        break;
                        case 1:
                        Logger.Log("Failure: HEX File not found (or invalid).", rt, System.Drawing.Color.Red);
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        case 2:
                        Logger.Log("Failure: Communication with PICKIT3 lost.", rt, System.Drawing.Color.Red);
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        case 5:
                        Logger.Log("Failure: PICKIT3 not detected or incorrect device connected.", rt, System.Drawing.Color.Red);
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        case 7:
                        Logger.Log("Failure: Device is not powered; select 'Power target using programmer' or supply external power source.", rt, System.Drawing.Color.Red);
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        case 9:
                        Logger.Log("Failure: PICKIT3 not detected or incorrect device connected.", rt, System.Drawing.Color.Red);
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        case 36:
                        Logger.Log("Failure: HEX File not found (or invalid).", rt, System.Drawing.Color.Red);
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        default:
                        Logger.Log("Unknown failure. Exited with code " + exeProcess.ExitCode, rt, System.Drawing.Color.Red);
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                    }
                    return exeProcess.ExitCode;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Failed to program device. Error: " + ex.Message, rt, System.Drawing.Color.Red);
                pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
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
            string softwarePrePath = $@"240-{softwarePartOne[0]}XXXX-XX\240-{softwarePartOne.Substring(0, 2)}XXX-XX";

            var path = $@"{VAULT_PATH}\{softwarePrePath}\240-{softwarePartOne}-XX\{softwarePartNumber}";

            if (!Directory.Exists(path))
                return null;
                        
            var dirs = Directory.EnumerateDirectories(path, "*", SearchOption.AllDirectories).ToList();

            return dirs.Where(d => d.Contains("ECL"));
        }

        private string GetLatestECL(string softwarePartNumber)
        {
            var softwarePartOne = softwarePartNumber.Substring(4, 5);
            int dashIndex;
            string tempECLStr;

            //set software vault path to appropriate number depending on what the software pn starts with
            string softwarePrePath = $@"240-{softwarePartOne[0]}XXXX-XX\240-{softwarePartOne.Substring(0, 2)}XXX-XX";

            try
            {
                var softwareDir = Directory.GetDirectories($@"{VAULT_PATH}\{softwarePrePath}\240-{softwarePartOne}-XX\")
                                           .FirstOrDefault(dir => dir.Contains(softwarePartNumber));

                var eclDir = Directory.GetDirectories(softwareDir).FirstOrDefault(dir => dir.Contains("ECL"));

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
