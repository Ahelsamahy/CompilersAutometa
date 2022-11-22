using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Compilers_Autometa
{
    public partial class Form1 : Form
    {
        string LASTLOCATION = "";
        string INPUT = "";
        Stack RULESET = new Stack();
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

            RULESET.Push("#");
            RULESET.Push("E");
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

        private void calculateStep(List<string> currentCellValue, char inputFirst, int rowIndex, int colIndex)
        {

            RULESET.Pop();
            if ((string)dgv.Rows[rowIndex].Cells[0].Value.ToString() == (string)dgv.Columns[colIndex].HeaderText)
            {
                INPUT = INPUT.Remove(0, 1);
            }
            else if (currentCellValue[0] == "")
            {

            }
            else
            {
                for (int i = currentCellValue.Count - 2; i >= 0; i--)
                {
                    RULESET.Push(currentCellValue[i]);
                }
                RULESTEPS += int.Parse(currentCellValue.Last()); // to store the step
            }

            if ((string)RULESET.Peek() == "ε")
            {
                RULESET.Pop();
            }

            string wholeStack = "";
            foreach (var item in RULESET)
            {
                wholeStack += item;
            }
            tbResult.Text += string.Format("({0}, {1}, {2}) {3}", new string(INPUT.ToArray()), wholeStack, RULESTEPS, Environment.NewLine);
        }
        private void ReadDGVCell()
        {
            RULESTEPS = "";
            int colIndex = 0;
            int rowIndex = 0;
            do
            {
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    if (char.ToString(INPUT[0]) == column.HeaderText)
                    {
                        colIndex = column.Index;
                    }
                }
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if ((string)RULESET.Peek() == row.Cells[0].Value.ToString())
                    {
                        rowIndex = row.Cells[0].RowIndex;
                    }
                }
                string selectCell = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();

                string cellValue = selectCell.ToString().Replace("(", "").Replace(")", "");
                List<string> splitted = cellValue.Split(';').ToList();

                if (splitted[0].Length > 1) // if there are more than one character
                {
                    string[] temp = splitted[0].Select(c => c.ToString()).ToArray();
                    splitted.RemoveAt(0);
                    splitted = temp.Concat(splitted).ToList();
                }
                if (splitted.Contains("`"))
                {
                    splitted[splitted.IndexOf("`") - 1] += "`";
                    splitted.RemoveAt(splitted.IndexOf("`"));
                }
                calculateStep(splitted, INPUT[0], rowIndex, colIndex);
            } while ((string)dgv.Rows[rowIndex].Cells[0].Value.ToString() != "#" && (string)dgv.Columns[colIndex].HeaderText != "#");
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
                tbConverted.Text = Regex.Replace(tbConverted.Text, "[A-Za-z]+", "i"); ;
                tbConverted.Text = Regex.Replace(tbConverted.Text, @"\s+", "") + "#";

                INPUT += tbConverted.Text;
                tbResult.Text = "";
                sysMessage("Converted text successfully", Color.Green);
                string temp = (string)RULESET.Peek();
                RULESET.Pop();
                tbResult.Text = string.Format("({0}, {1}, {2}) {3}", new string(INPUT.ToArray()), (temp + (string)RULESET.Peek()), RULESTEPS, Environment.NewLine);
                RULESET.Push(temp);
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
