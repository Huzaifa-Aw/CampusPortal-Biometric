
namespace CampusPortalBiometric
{
    partial class UpdateFingerprint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateFingerprint));
            this.txtEnroll = new System.Windows.Forms.TextBox();
            this.dgStudents = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.rbEmployee = new System.Windows.Forms.RadioButton();
            this.rbStudents = new System.Windows.Forms.RadioButton();
            this.btnStartScan = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgStudents)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEnroll
            // 
            this.txtEnroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnroll.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtEnroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtEnroll.Location = new System.Drawing.Point(598, 12);
            this.txtEnroll.Multiline = true;
            this.txtEnroll.Name = "txtEnroll";
            this.txtEnroll.ReadOnly = true;
            this.txtEnroll.Size = new System.Drawing.Size(365, 375);
            this.txtEnroll.TabIndex = 6;
            // 
            // dgStudents
            // 
            this.dgStudents.AllowUserToAddRows = false;
            this.dgStudents.AllowUserToDeleteRows = false;
            this.dgStudents.AllowUserToResizeRows = false;
            this.dgStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgStudents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgStudents.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgStudents.Location = new System.Drawing.Point(17, 46);
            this.dgStudents.MultiSelect = false;
            this.dgStudents.Name = "dgStudents";
            this.dgStudents.ReadOnly = true;
            this.dgStudents.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgStudents.RowTemplate.Height = 40;
            this.dgStudents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgStudents.Size = new System.Drawing.Size(575, 341);
            this.dgStudents.TabIndex = 7;
            this.dgStudents.SelectionChanged += new System.EventHandler(this.dgStudents_SelectionChanged);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(152)))), ((int)(((byte)(3)))));
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBack.Location = new System.Drawing.Point(863, 393);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 40);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(152)))), ((int)(((byte)(3)))));
            this.btnUpdate.Enabled = false;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUpdate.Location = new System.Drawing.Point(708, 393);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 40);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // rbEmployee
            // 
            this.rbEmployee.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbEmployee.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rbEmployee.Location = new System.Drawing.Point(126, 10);
            this.rbEmployee.Name = "rbEmployee";
            this.rbEmployee.Size = new System.Drawing.Size(100, 24);
            this.rbEmployee.TabIndex = 1;
            this.rbEmployee.TabStop = true;
            this.rbEmployee.Text = "Employee";
            this.rbEmployee.UseVisualStyleBackColor = true;
            this.rbEmployee.CheckedChanged += new System.EventHandler(this.rbEmployee_CheckedChanged);
            // 
            // rbStudents
            // 
            this.rbStudents.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.rbStudents.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rbStudents.Location = new System.Drawing.Point(3, 10);
            this.rbStudents.Name = "rbStudents";
            this.rbStudents.Size = new System.Drawing.Size(100, 24);
            this.rbStudents.TabIndex = 0;
            this.rbStudents.TabStop = true;
            this.rbStudents.Text = "Students";
            this.rbStudents.UseVisualStyleBackColor = true;
            this.rbStudents.CheckedChanged += new System.EventHandler(this.rbStudents_CheckedChanged);
            // 
            // btnStartScan
            // 
            this.btnStartScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(152)))), ((int)(((byte)(3)))));
            this.btnStartScan.FlatAppearance.BorderSize = 0;
            this.btnStartScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnStartScan.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnStartScan.Location = new System.Drawing.Point(599, 393);
            this.btnStartScan.Name = "btnStartScan";
            this.btnStartScan.Size = new System.Drawing.Size(100, 40);
            this.btnStartScan.TabIndex = 11;
            this.btnStartScan.Text = "Scan";
            this.btnStartScan.UseVisualStyleBackColor = false;
            this.btnStartScan.Click += new System.EventHandler(this.btnStartScan_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.rbStudents);
            this.panel1.Controls.Add(this.rbEmployee);
            this.panel1.Location = new System.Drawing.Point(17, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 40);
            this.panel1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 27);
            this.label1.TabIndex = 14;
            this.label1.Text = "Search By :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbFilter
            // 
            this.cbFilter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "ID",
            "Name",
            "Father_Name"});
            this.cbFilter.Location = new System.Drawing.Point(108, 12);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(120, 28);
            this.cbFilter.TabIndex = 15;
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tbSearch.Location = new System.Drawing.Point(234, 12);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(358, 26);
            this.tbSearch.TabIndex = 16;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // UpdateFingerprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(975, 456);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnStartScan);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgStudents);
            this.Controls.Add(this.txtEnroll);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateFingerprint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Fingerprint";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.UpdateFingerprint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgStudents)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtEnroll;
        private System.Windows.Forms.DataGridView dgStudents;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.RadioButton rbStudents;
        private System.Windows.Forms.RadioButton rbEmployee;
        private System.Windows.Forms.Button btnStartScan;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.TextBox tbSearch;
    }
}