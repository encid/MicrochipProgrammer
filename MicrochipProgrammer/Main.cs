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
        public const string VAULT_PATH = @"\\pandora\vault\Released_Part_Information\240-xxxxx-xx_Software\240-9XXXX-XX\240-91XXX-XX\";

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void cmdGetECL_Click(object sender, EventArgs e)
        {
            Regex rgx = new Regex(@"/ECL-\w+$/g");

            var softwarePartOne = txtSWPartOne.Text;
            var softwarePartNumber = string.Format("240-{0}-{1}", txtSWPartOne.Text, txtSWPartTwo.Text);

            var dirList = Directory.GetDirectories(string.Format(@"{0}\240-{1}-XX\{2}", VAULT_PATH, softwarePartOne, softwarePartNumber));

            //foreach (var dir in dirList)
            //{
            //    MessageBox.Show(dir);
            //}

            //var dirECL = dirList.Where(p => p.Contains("ECL")).ToString();
            //var dirz = from dir in dirList
            //                where dir.Contains("ECL")
            //                select dir;
            string dirECL = "";
            //foreach (var d in dirz)
            //{
            //    dirECL = d;
            //}
            foreach (var dir in dirList)
            {
                if (dir.Contains("ECL"))
                    dirECL = dir;
            }

            //MessageBox.Show(dirECL);
            var tempECLStr = dirECL.Substring(dirECL.Length - 6);MessageBox.Show(tempECLStr);
            int dash = tempECLStr.IndexOf("-");
            var softwareECL = tempECLStr.Substring(dash + 1);
            txtECL.Text = softwareECL;
        }

        
    }
}
