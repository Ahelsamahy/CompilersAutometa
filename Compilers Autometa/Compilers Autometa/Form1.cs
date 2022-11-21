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
        string LASTLOCATION = "";
        char[] INPUT = new char[] { };
        char[] STACK = new char[] { 'E', '#' };
        string RULESTEPS = "";
        DataTable dt = new DataTable();
        bool headerFilled = false;
        public Form1()
        {
            InitializeComponent();
            tbResult.TextAlign = HorizontalAlignment.Center;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }


        private void Initializedgv()
        {
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void sysMessage(string message, Color type)
        {
            lbResult.Text = message;
            lbResult.ForeColor = type;
        }
        void sizeDGV(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            totalHeight += dgv.Rows.Count + 4;  // a correction I need
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            dgv.ClientSize = new Size(totalWidth, totalHeight);
        }
        private void ReadCSV(String fileLocation)
        {
            dgv.Rows.Clear();
            dgv.Refresh();
            using (TextFieldParser parser = new TextFieldParser(fileLocation, Encoding.UTF8))
            {
                parser.TextFieldType = FieldType.Delimited;
                DataRow dr = dt.NewRow();
                parser.SetDelimiters(",");

                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    if (!headerFilled)
                    {
                        foreach (string field in fields)
                        {
                            dgv.Columns.Add(field, field);
                        }
                        headerFilled = true;
                    }
                    else
                    {
                        dgv.Rows.Add(fields);
                    }

                }
                Initializedgv();
                sizeDGV(dgv);
            }
        }

        private void calculateStep(string[] currentCellValue, char inputFirst)
        {
            if (true)// row == column then remove
            {

            }
            tbResult.Text += string.Format("({0}, {1}, {2})", tbConverted.Text, new string(STACK), RULESTEPS);
        }

        private void ReadDGVCell()
        {
            int colIndex;
            int rowIndex;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (char.ToString(INPUT[0]) == column.HeaderText)
                {
                    colIndex = column.Index;
                }
                //tbResult.Text += column.HeaderText;
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (char.ToString(STACK[0]) == (string) row.Cells[0].FormattedValue)
                {
                    rowIndex = row.Cells[0].RowIndex;
                }
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //(input, stack, serial number of the rules)
                    string cellValue = cell.Value.ToString().Replace("(", "").Replace(")", "");
                    string[] splitted = cellValue.Split(';');
                    if (splitted[0] == char.ToString(INPUT[0]))
                    {
                        calculateStep(splitted, INPUT[0]);
                    }

                }
            }
        }
        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (tbInput.Text.Length == 0)
            {
                sysMessage("please enter text to convert", Color.Red);
            }
            else
            {
                tbConverted.Text = Regex.Replace(tbInput.Text, "[0-9]+", "i");
                tbConverted.Text = Regex.Replace(tbConverted.Text, @"\s+", "") + "#";
                INPUT = tbConverted.Text.ToCharArray();
                tbResult.Text = "";

                sysMessage("Converted text successfully", Color.Green);
            }


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
            if (LASTLOCATION.Length == 0)
            {
                LASTLOCATION = @"D:\Work\Collage\3_Third Year\First Semester\Compilers\final\CompilersAutometa";
            }
            browseDB.InitialDirectory = LASTLOCATION;
            browseDB.Filter = "Database files (*.csv)|*.csv; ";
            browseDB.FilterIndex = 0;
            browseDB.RestoreDirectory = true;

            if (browseDB.ShowDialog() == DialogResult.OK)
            {
                LASTLOCATION = Path.GetDirectoryName(browseDB.FileName);
                tbPath.Text = browseDB.FileName;
                ReadCSV(browseDB.FileName);
            }
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int colInd = dgv.CurrentCell.ColumnIndex;
            int rowInd = dgv.CurrentCell.RowIndex;

            lbResult.Text = dgv.Rows[rowInd].Cells[0].Value.ToString();
        }
        private void dgv_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private void btnSolve_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                sysMessage("Please add a file to read from first", Color.Red);
            }
            else if (INPUT.Length == 0)
            {
                sysMessage("Please type input first ", Color.Red);
                tbInput.Focus();
            }
            else
            {
                ReadDGVCell();
            }

        }
    }
}
