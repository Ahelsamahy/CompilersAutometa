
namespace Compilers_Autometa
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbInput = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lbConverted = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lbPath = new System.Windows.Forms.Label();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.tbConverted = new System.Windows.Forms.TextBox();
            this.btnSolve = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.lbResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbInput
            // 
            this.lbInput.AutoSize = true;
            this.lbInput.Location = new System.Drawing.Point(13, 64);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(72, 20);
            this.lbInput.TabIndex = 0;
            this.lbInput.Text = "Input text";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(430, 60);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(73, 29);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(430, 161);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(73, 29);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lbConverted
            // 
            this.lbConverted.AutoSize = true;
            this.lbConverted.Location = new System.Drawing.Point(12, 114);
            this.lbConverted.Name = "lbConverted";
            this.lbConverted.Size = new System.Drawing.Size(106, 20);
            this.lbConverted.TabIndex = 3;
            this.lbConverted.Text = "Converted text";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 212);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(490, 339);
            this.dataGridView1.TabIndex = 5;
            // 
            // lbPath
            // 
            this.lbPath.AutoSize = true;
            this.lbPath.Location = new System.Drawing.Point(13, 165);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(69, 20);
            this.lbPath.TabIndex = 6;
            this.lbPath.Text = "CSV path";
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(125, 60);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(286, 27);
            this.tbInput.TabIndex = 1;
            this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
            // 
            // tbConverted
            // 
            this.tbConverted.Location = new System.Drawing.Point(125, 111);
            this.tbConverted.Name = "tbConverted";
            this.tbConverted.Size = new System.Drawing.Size(286, 27);
            this.tbConverted.TabIndex = 8;
            // 
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(515, 42);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(273, 29);
            this.btnSolve.TabIndex = 9;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = true;
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(125, 162);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(286, 27);
            this.tbPath.TabIndex = 10;
            // 
            // lbResult
            // 
            this.lbResult.AutoSize = true;
            this.lbResult.Location = new System.Drawing.Point(164, 9);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(0, 20);
            this.lbResult.TabIndex = 11;
            this.lbResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 563);
            this.Controls.Add(this.lbResult);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.tbConverted);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.lbPath);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbConverted);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.lbInput);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInput;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lbConverted;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.TextBox tbConverted;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label lbResult;
    }
}

