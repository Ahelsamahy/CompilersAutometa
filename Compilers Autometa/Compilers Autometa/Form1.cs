using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilers_Autometa
{
    public partial class Form1 : Form
    {
        string lastLocation = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            tbConverted.Text = Regex.Replace(tbInput.Text, "[0-9]", "i");
            tbConverted.Text = Regex.Replace(tbConverted.Text, @"\s+", "") + "#";
            lbResult.Text = "Converted text successfully";
            lbResult.ForeColor = Color.Green;

        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConvert.PerformClick();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog browseDB = new OpenFileDialog();
            if (lastLocation.Length == 0)
            {
                lastLocation = @"D:\Work\Collage\3_Third Year\First Semester\Artifical neual network\mass\dataset\archive";
            }
            browseDB.InitialDirectory = lastLocation;
            browseDB.Filter = "Database files (*.csv)|*.csv; ";
            browseDB.FilterIndex = 0;
            browseDB.RestoreDirectory = true;

            if (browseDB.ShowDialog() == DialogResult.OK)
            {
                lastLocation = Path.GetDirectoryName(browseDB.FileName);
                tbPath.Text = browseDB.FileName;
                ReadCSV(browseDB.FileName);
            }
        }
        private void ReadCSV(String fileLocation)
        {
            using (var reader = new StreamReader(fileLocation))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listA.Add(values[0]);
                    listA.Add(values[1]);
                    listA.Add(values[2]);
                    listA.Add(values[3]);
                    listA.Add(values[4]);
                    listA.Add(values[5]);
                    listA.Add(values[6]);
                }
            }
        }

    }
}
