
namespace CampusPortalBiometric
{
    partial class MenuScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuScreen));
            this.btnAttendance = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnUpdateDB = new System.Windows.Forms.Button();
            this.tbSchoolName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAttendance
            // 
            this.btnAttendance.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAttendance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(152)))), ((int)(((byte)(3)))));
            this.btnAttendance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAttendance.FlatAppearance.BorderSize = 0;
            this.btnAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendance.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.btnAttendance.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAttendance.Location = new System.Drawing.Point(301, 170);
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.Size = new System.Drawing.Size(380, 74);
            this.btnAttendance.TabIndex = 0;
            this.btnAttendance.Text = "Mark Attendance";
            this.btnAttendance.UseVisualStyleBackColor = false;
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(152)))), ((int)(((byte)(3)))));
            this.btnRegister.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRegister.Location = new System.Drawing.Point(301, 273);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(380, 74);
            this.btnRegister.TabIndex = 1;
            this.btnRegister.Text = "Register Fingerprint";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(152)))), ((int)(((byte)(3)))));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUpdate.Location = new System.Drawing.Point(301, 371);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(380, 74);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update Fingerprint";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnUpdateDB
            // 
            this.btnUpdateDB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUpdateDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(152)))), ((int)(((byte)(3)))));
            this.btnUpdateDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUpdateDB.FlatAppearance.BorderSize = 0;
            this.btnUpdateDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.btnUpdateDB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnUpdateDB.Location = new System.Drawing.Point(301, 470);
            this.btnUpdateDB.Name = "btnUpdateDB";
            this.btnUpdateDB.Size = new System.Drawing.Size(380, 74);
            this.btnUpdateDB.TabIndex = 3;
            this.btnUpdateDB.Text = "Update Database";
            this.btnUpdateDB.UseVisualStyleBackColor = false;
            this.btnUpdateDB.Click += new System.EventHandler(this.btnUpdateDB_Click);
            // 
            // tbSchoolName
            // 
            this.tbSchoolName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbSchoolName.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.tbSchoolName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tbSchoolName.Location = new System.Drawing.Point(183, 24);
            this.tbSchoolName.Name = "tbSchoolName";
            this.tbSchoolName.Size = new System.Drawing.Size(629, 84);
            this.tbSchoolName.TabIndex = 4;
            this.tbSchoolName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MenuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(986, 624);
            this.Controls.Add(this.tbSchoolName);
            this.Controls.Add(this.btnUpdateDB);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnAttendance);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MenuScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAttendance;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnUpdateDB;
        private System.Windows.Forms.Label tbSchoolName;
    }
}