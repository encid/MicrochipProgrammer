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
using System.ComponentModel;
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
        BackgroundWorker bw;

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

                bw = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw.DoWork += bw_DoWork;
                bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            }

            ToolTip toolTip = new ToolTip
            {
                // Set up the delays for the ToolTip.
                InitialDelay = 200,
                ReshowDelay = 200,
                // Force the ToolTip text to be displayed whether or not the form is active.
                ShowAlways = true
            };

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip.SetToolTip(rbPIC16F1718, "Grindmaster");
            toolTip.SetToolTip(rbPIC32MX440F128H, "Fast Transfer");
            toolTip.SetToolTip(rbPIC16F716, "Modularm");
            toolTip.SetToolTip(rbPIC12F1840, "80266 Reset Generator");
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

        private void ExecuteSecure(Action a)
        // Usage example: ExecuteSecure(() => this.someLabel.Text = "foo");
        {
            Invoke((MethodInvoker)delegate { a(); });
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
                    Logger.Log($"\nError: {softwarePartNumber} is not a valid software part number, or was not found in vault.", rt);
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

            var dirs = GetSoftwareDirectories(softwarePartNumber);
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
            string voltage = "";

            if (rbPIC16F1718.Checked)
            {
                processor = "P16F1718";
                if (chkPowerTargetFromDevice.Checked)
                    voltage = "5.0";
            }
            if (rbPIC32MX440F128H.Checked)
            {
                processor = "P32MX440F128H";
                if (chkPowerTargetFromDevice.Checked)
                    voltage = "3.3";
            }
            if (rbPIC16F716.Checked)
            {
                processor = "P16F716";
                if (chkPowerTargetFromDevice.Checked)
                    voltage = "5.0";
            }
            if (rbPIC12F1840.Checked)
            {
                processor = "P12F1840";
                if (chkPowerTargetFromDevice.Checked)
                    voltage = "5.0";
            }
            DisableUI();
            Logger.Log($"\nStarting to program {softwareFile} - {softwarePartNumber} ECL-{ECL}...", rt);
            
            string[] args = new string[3];
            args[0] = softwareFile;
            args[1] = processor;
            args[2] = voltage;
            bw.RunWorkerAsync(args);
        }

        private int DoProgram(string file, string processor, string voltage)
        {
            if (file == "")
            {
                MessageBox.Show("Error: No file selected.");
                return -1;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,                
                FileName = $@"{PICKIT_PATH}\PK3CMD.exe",
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = $"/{processor} /M /L /F\"{file}\""                
            };

            if (voltage != "")
            {
                startInfo.Arguments += $@" /V{voltage}";
            }

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process p = Process.Start(startInfo))
                {
                    var reader = p.StandardOutput;
                    while (!reader.EndOfStream)
                    {
                        var nextLine = reader.ReadLine();                        
                        ExecuteSecure(() => Logger.Log(nextLine, rt));
                        if (nextLine.Contains("locate JRE"))
                        {
                            ExecuteSecure(() => Logger.Log("Failure: MPLab software is not installed -- Contact Test Engineering.\n", rt, System.Drawing.Color.Red));
                            pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                            p.Kill();
                            return -1;
                        }
                    }
                    p.WaitForExit();
                    switch (p.ExitCode)
                    {
                        case 0:
                            ExecuteSecure(() => Logger.Log("Device programmed successfully!", rt, System.Drawing.Color.Green));
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.check;
                        break;
                        case 1:
                            ExecuteSecure(() => Logger.Log("Failure: HEX File not found (or invalid).", rt, System.Drawing.Color.Red));
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        case 2:
                            ExecuteSecure(() => Logger.Log("Failure: Communication with PICKIT3 lost.", rt, System.Drawing.Color.Red));
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        case 5:
                            ExecuteSecure(() => Logger.Log("Failure: PICKIT3 not detected or incorrect device connected.", rt, System.Drawing.Color.Red));
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        case 7:
                            ExecuteSecure(() => Logger.Log("Failure: Device is not powered; select 'Power target using programmer' or supply external power source.", rt, System.Drawing.Color.Red));
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        case 9:
                            ExecuteSecure(() => Logger.Log("Failure: PICKIT3 not detected or incorrect device connected.", rt, System.Drawing.Color.Red));
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        case 36:
                            ExecuteSecure(() => Logger.Log("Failure: HEX File not found (or invalid).", rt, System.Drawing.Color.Red));
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                        default:
                            ExecuteSecure(() => Logger.Log("Unknown failure. Exited with code " + p.ExitCode, rt, System.Drawing.Color.Red));
                        pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                        break;
                    }
                    return p.ExitCode;
                }
            }
            catch (Exception ex)
            {
                ExecuteSecure(() => Logger.Log("Failure: " + ex.Message, rt, System.Drawing.Color.Red));
                pictureBox1.Image = MicrochipProgrammer.Properties.Resources.red_x;
                return -1;
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] args = (string[])e.Argument;
            string softwareFile = args[0];
            string processor = args[1];
            string voltage = args[2];
            DoProgram(softwareFile, processor, voltage);
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableUI();
        }

        private void DisableUI()
        {
            txtSWPartOne.Enabled = false;
            txtSWPartTwo.Enabled = false;
            txtECL.Enabled = false;
            cmdGetECL.Enabled = false;
            cmdProgram.Enabled = false;
            groupBox2.Enabled = false;
            chkPowerTargetFromDevice.Enabled = false;
        }

        private void EnableUI()
        {
            txtSWPartOne.Enabled = true;
            txtSWPartTwo.Enabled = true;
            txtECL.Enabled = true;
            cmdGetECL.Enabled = true;
            cmdProgram.Enabled = true;
            groupBox2.Enabled = true;
            chkPowerTargetFromDevice.Enabled = true;
        }

        private IEnumerable<string> GetSoftwareDirectories(string softwarePartNumber)
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
            catch (Exception e)
            {
                Logger.Log(e.Message, rt, System.Drawing.Color.Red);
                return string.Empty;
            }

            return tempECLStr.Substring(dashIndex + 1);
        }
    }
}