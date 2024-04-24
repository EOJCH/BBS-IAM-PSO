namespace VVIPUser_Tool
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtfile = new System.Windows.Forms.TextBox();
            this.txtbrowse = new System.Windows.Forms.Button();
            this.txtsubmit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CWID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Firstname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lastname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VVIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btndownload = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btncheck_individual = new System.Windows.Forms.Button();
            this.txt_cwids = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtfile
            // 
            this.txtfile.Enabled = false;
            this.txtfile.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtfile.Location = new System.Drawing.Point(60, 29);
            this.txtfile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtfile.Name = "txtfile";
            this.txtfile.Size = new System.Drawing.Size(668, 27);
            this.txtfile.TabIndex = 1;
            // 
            // txtbrowse
            // 
            this.txtbrowse.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtbrowse.Location = new System.Drawing.Point(805, 28);
            this.txtbrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtbrowse.Name = "txtbrowse";
            this.txtbrowse.Size = new System.Drawing.Size(77, 28);
            this.txtbrowse.TabIndex = 2;
            this.txtbrowse.Text = "Browse";
            this.txtbrowse.UseVisualStyleBackColor = true;
            this.txtbrowse.Click += new System.EventHandler(this.txtbrowse_Click);
            // 
            // txtsubmit
            // 
            this.txtsubmit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtsubmit.Location = new System.Drawing.Point(918, 28);
            this.txtsubmit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtsubmit.Name = "txtsubmit";
            this.txtsubmit.Size = new System.Drawing.Size(134, 28);
            this.txtsubmit.TabIndex = 3;
            this.txtsubmit.Text = "Submit";
            this.txtsubmit.UseVisualStyleBackColor = true;
            this.txtsubmit.Click += new System.EventHandler(this.txtsubmit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CWID,
            this.Firstname,
            this.Lastname,
            this.Email,
            this.VVIP});
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.Location = new System.Drawing.Point(12, 220);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1082, 482);
            this.dataGridView1.TabIndex = 4;
            // 
            // CWID
            // 
            this.CWID.HeaderText = "CWID";
            this.CWID.MinimumWidth = 6;
            this.CWID.Name = "CWID";
            this.CWID.ReadOnly = true;
            this.CWID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Firstname
            // 
            this.Firstname.HeaderText = "First Name";
            this.Firstname.MinimumWidth = 6;
            this.Firstname.Name = "Firstname";
            this.Firstname.ReadOnly = true;
            // 
            // Lastname
            // 
            this.Lastname.HeaderText = "Last Name";
            this.Lastname.MinimumWidth = 6;
            this.Lastname.Name = "Lastname";
            this.Lastname.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.MinimumWidth = 6;
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // VVIP
            // 
            this.VVIP.HeaderText = "VVIP";
            this.VVIP.MinimumWidth = 6;
            this.VVIP.Name = "VVIP";
            this.VVIP.ReadOnly = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "\"Excel Files|*.xlsx";
            // 
            // btndownload
            // 
            this.btndownload.Enabled = false;
            this.btndownload.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btndownload.Location = new System.Drawing.Point(942, 179);
            this.btndownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btndownload.Name = "btndownload";
            this.btndownload.Size = new System.Drawing.Size(134, 28);
            this.btndownload.TabIndex = 7;
            this.btndownload.Text = "Download";
            this.btndownload.UseVisualStyleBackColor = true;
            this.btndownload.Click += new System.EventHandler(this.btndownload_Click);
            // 
            // btncheck_individual
            // 
            this.btncheck_individual.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btncheck_individual.Location = new System.Drawing.Point(918, 35);
            this.btncheck_individual.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btncheck_individual.Name = "btncheck_individual";
            this.btncheck_individual.Size = new System.Drawing.Size(134, 28);
            this.btncheck_individual.TabIndex = 4;
            this.btncheck_individual.Text = "Submit";
            this.btncheck_individual.UseVisualStyleBackColor = true;
            this.btncheck_individual.Click += new System.EventHandler(this.btncheck_individual_Click);
            // 
            // txt_cwids
            // 
            this.txt_cwids.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt_cwids.Location = new System.Drawing.Point(60, 37);
            this.txt_cwids.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_cwids.Name = "txt_cwids";
            this.txt_cwids.Size = new System.Drawing.Size(668, 27);
            this.txt_cwids.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtfile);
            this.groupBox1.Controls.Add(this.txtbrowse);
            this.groupBox1.Controls.Add(this.txtsubmit);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(24, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1070, 72);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Click Browse To Select VVIP File";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btncheck_individual);
            this.groupBox2.Controls.Add(this.txt_cwids);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(24, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1070, 80);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CWID / CWID\'s (Multiple with , seperation)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1123, 713);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btndownload);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "VVIP User Loader";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private TextBox txtfile;
        private Button txtbrowse;
        private Button txtsubmit;
        private DataGridView dataGridView1;
        private OpenFileDialog openFileDialog1;
        private DataGridViewTextBoxColumn CWID;
        private DataGridViewTextBoxColumn Firstname;
        private DataGridViewTextBoxColumn Lastname;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn VVIP;
        private Button btndownload;
        private SaveFileDialog saveFileDialog1;
        private Button btncheck_individual;
        private TextBox txt_cwids;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}