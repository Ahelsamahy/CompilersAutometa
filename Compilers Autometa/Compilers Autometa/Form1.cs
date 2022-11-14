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
                lastLocation = "c:\\";
            }
            browseDB.InitialDirectory = lastLocation;
            browseDB.Filter = "Database files (*.csv)|*.csv; ";
            browseDB.FilterIndex = 0;
            browseDB.RestoreDirectory = true;

            if (browseDB.ShowDialog() == DialogResult.OK)
            {
                lastLocation = Path.GetDirectoryName(browseDB.FileName);
                tbPath.Text = browseDB.FileName;
                //...
            }
        }
    }
}
