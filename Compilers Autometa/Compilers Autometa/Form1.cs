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
        DataTable dt = new DataTable();
        bool headerFilled = false;
        public Form1()
        {
            InitializeComponent();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }


        private void Initializedgv()
        {


            //dgv.Columns.Add("Column1", "Column1");
            //dgv.Columns.Add("Column2", "Column2");
            //dgv.Columns.Add("Column3", "Column3");
            //dgv.Columns.Add("Column4", "Column4");
            //dgv.Columns.Add("Column5", "Column5");
            //dgv.Columns.Add("Column6", "Column6");
            //dgv.Columns.Add("Column7", "Column7");
            dgv.RowHeadersVisible = false;
            //dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            
            using (TextFieldParser parser = new TextFieldParser(fileLocation))
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
            if (lastLocation.Length == 0)
            {
                lastLocation = @"D:\Work\Collage\3_Third Year\First Semester\Compilers\final\CompilersAutometa";
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
    }
}
