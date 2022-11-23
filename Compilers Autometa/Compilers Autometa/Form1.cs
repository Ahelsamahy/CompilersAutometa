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
        string LAST_LOCATION = "";
        string INPUT = "";
        Stack RULE_SET = new Stack();
        string RULE_STEPS = "";
        DataTable dt = new DataTable();
        bool HEADER_FILLED = false;

        public Form1()
        {
            InitializeComponent();
            initComp();
        }
        private void initComp()
        {
            tbResult.TextAlign = HorizontalAlignment.Center;
            tbMessage.TextAlign = HorizontalAlignment.Center;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            RULE_SET.Push("#");
            RULE_SET.Push("E");


        }
        private void sysMessage(string message, Color type)
        {
            tbMessage.Text = message;
            tbMessage.ForeColor = type;
        }

        #region Syntex tree related
        public void calSyntexTree(List<string> currentCellValue, char inputFirst, int rowIndex, int colIndex)
        {
            //first is to romove the first element in the stack 

            RULE_SET.Pop();
            if ((string)dgv.Rows[rowIndex].Cells[0].Value.ToString() == (string)dgv.Columns[colIndex].HeaderText)//for the pop
            {
                INPUT = INPUT.Remove(0, 1);
            }
            else if (currentCellValue[0] == "")//not to through error when there is nothing in the cell
            {

            }
            else
            {
                for (int i = currentCellValue.Count - 2; i >= 0; i--)//if there are more than one letter stored in the ruleSet
                {
                    RULE_SET.Push(currentCellValue[i]);
                }
                RULE_STEPS += int.Parse(currentCellValue.Last()); // to store the step
            }

            if ((string)RULE_SET.Peek() == "ε")
            {
                RULE_SET.Pop();
            }

            printSyntexTree();
        }
        public void printSyntexTree()
        {
            string wholeStack = "";
            foreach (var item in RULE_SET)
            {
                wholeStack += item;
            }
            tbResult.Text += string.Format("({0}, {1}, {2}) {3}", new string(INPUT.ToArray()), wholeStack, RULE_STEPS, Environment.NewLine);
        }
        public void getIndex(ref int colIndex, ref int rowIndex)
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
                if ((string)RULE_SET.Peek() == row.Cells[0].Value.ToString())
                {
                    rowIndex = row.Cells[0].RowIndex;
                }
            }
        }
        public List<string> foramtFoundedCell(ref List<string> splitted)
        {
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
            return splitted;
        }
        public void readDGVCell()
        {
            RULE_STEPS = "";
            int colIndex = 0;
            int rowIndex = 0;
            while ((string)RULE_SET.Peek() != (string)"#")
            {
                getIndex(ref colIndex, ref rowIndex);

                string selectCell = dgv.Rows[rowIndex].Cells[colIndex].Value.ToString();
                string cellValue = selectCell.ToString().Replace("(", "").Replace(")", "");
                List<string> splitted = cellValue.Split(';').ToList();

                foramtFoundedCell(ref splitted);

                calSyntexTree(splitted, INPUT[0], rowIndex, colIndex);
            }
        }
        #endregion
        #region DataGridView related
        public void createDGV_Cells(string[] fields)
        {
            if (!HEADER_FILLED)
            {
                foreach (string field in fields)
                {
                    dgv.Columns.Add(field, field);
                }
                HEADER_FILLED = true;
            }
            else
            {
                dgv.Rows.Add(fields);
            }
        }
        public void ReadCSV(String fileLocation)
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
                    createDGV_Cells(fields);
                }
            }
        }
        #endregion 

        private void formatConvertedText()
        {
            tbConverted.Text = Regex.Replace(tbInput.Text, "[0-9]+", "i");
            tbConverted.Text = Regex.Replace(tbConverted.Text, "[A-Za-z]+", "i"); ;
            tbConverted.Text = Regex.Replace(tbConverted.Text, @"\s+", "") + "#";
        }
        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (tbInput.Text.Length == 0)
            {
                sysMessage("please enter text to convert", Color.Red);
            }
            else
            {
                formatConvertedText();

                INPUT += tbConverted.Text;
                sysMessage("Converted text successfully", Color.Green);

                string temp = (string)RULE_SET.Peek();
                RULE_SET.Pop();
                tbResult.Text = string.Format("({0}, {1}, {2}) {3}", new string(INPUT.ToArray()), (temp + (string)RULE_SET.Peek()), RULE_STEPS, Environment.NewLine);
                RULE_SET.Push(temp);
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
            resetDGV();
            tbPath.Text = "";
            OpenFileDialog browseDB = new OpenFileDialog();
            if (LAST_LOCATION.Length == 0)
            {
                LAST_LOCATION = @"D:\Work\Collage\3_Third Year\First Semester\Compilers\final\CompilersAutometa";
            }
            browseDB.InitialDirectory = LAST_LOCATION;
            browseDB.Filter = "Database files (*.csv)|*.csv; ";
            browseDB.FilterIndex = 0;
            browseDB.RestoreDirectory = true;

            if (browseDB.ShowDialog() == DialogResult.OK)
            {
                LAST_LOCATION = Path.GetDirectoryName(browseDB.FileName);
                tbPath.Text = browseDB.FileName;
                ReadCSV(browseDB.FileName);
            }
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
                readDGVCell();
            }

        }
        private void btnExport_Click(object sender, EventArgs e)
        {

            if (dgv.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Output.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dgv.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dgv.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dgv.Columns[i].HeaderText.ToString();
                                if (i < columnCount - 1)
                                {
                                    columnNames += ",";
                                }
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; (i - 1) < dgv.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += dgv.Rows[i - 1].Cells[j].Value.ToString();
                                    if (j < columnCount - 1)
                                    {
                                        outputCsv[i] += ",";
                                    }
                                }
                            }


                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }

        }
        private void reset()
        {
            tbConverted.Text = tbInput.Text = tbPath.Text = tbResult.Text = tbMessage.Text = "";
            tbInput.Focus();
            RULE_STEPS = "";
            INPUT = "";
            RULE_SET.Push("#");
            RULE_SET.Push("E");
            resetDGV();
        }
        private void resetDGV()
        {
            HEADER_FILLED = false;
            dgv.Columns.Clear();
            dgv.Rows.Clear();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.HeaderText = "";
            }

            dgv.Refresh();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    reset();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
