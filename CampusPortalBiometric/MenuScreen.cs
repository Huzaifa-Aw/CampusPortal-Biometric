using CampusPortalBiometric.SQLiteServices;
using CampusPortalBiometric.Utils;
using CampusPortalBiometric.WebServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CampusPortalBiometric.Utils.Entities;

namespace CampusPortalBiometric
{
    public partial class MenuScreen : Form
    {
        public Login _sender;
        internal UserInfo _userinfo;
        private StudentMgmt studentMgmt;
        private EmployeeMgmt employeeMgmt;
        private SQLStudentServices sqlStudentServices;
        private SQLEmployeeServices sqlEmployeeServices;
        private enum ActionType { 
        DeleteAll,Delete
        }
        public MenuScreen()
        {
            InitializeComponent();
            
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            AttendanceScreen frmMainScreen = new AttendanceScreen();
            frmMainScreen._sender = this._sender;
            frmMainScreen._userInfo = this._userinfo;
            frmMainScreen.FormClosed += (s, args) => this.Show();
            this.Hide();
            frmMainScreen.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateFingerprint frmMainScreen = new UpdateFingerprint();
            frmMainScreen._sender = this._sender;
            frmMainScreen._userInfo = this._userinfo;
            frmMainScreen.FormClosed += (s, args) => this.Show();
            this.Hide();
            frmMainScreen.Show();
        }

        private void btnUpdateDB_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                GetStudents();
                GetEmployees();
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Database Updated Successfuly!", "Info");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void GetStudents()
        {
            sqlStudentServices.ClearStudents();
            GetRegisteredStudents();
        }
        private void GetEmployees()
        {
            sqlEmployeeServices.ClearEmployees();
            GetRegisteredEmployees();
        }
        private void GetRegisteredStudents()
        {
            var RegisteredStudents = studentMgmt.GetBiometricStudents(_userinfo.token,_userinfo.SchoolID);
            if (RegisteredStudents.Count > 0)
            {
                sqlStudentServices.SaveRegisteredStudents(RegisteredStudents);
            }
        }
        private void GetRegisteredEmployees()
        {
            var RegisteredEmployees = employeeMgmt.GetBiometricStudents(_userinfo.token, _userinfo.SchoolID);
            if (RegisteredEmployees.Count > 0)
            {
                sqlEmployeeServices.SaveRegisteredEmployees(RegisteredEmployees);
            }
        }
        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterScreen frmMainScreen = new RegisterScreen();
            frmMainScreen._sender = this._sender;
            frmMainScreen._userInfo = this._userinfo;
            frmMainScreen.FormClosed += (s, args) => this.Show();
            this.Hide();
            frmMainScreen.Show();
        }

        private void MenuScreen_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            sqlStudentServices = new SQLStudentServices();
            sqlEmployeeServices = new SQLEmployeeServices();
            studentMgmt = new StudentMgmt();
            employeeMgmt = new EmployeeMgmt();
            tbSchoolName.Text = _userinfo.SchoolName;
            pictureBox1.Load(URLManager.GetImageURL(_userinfo.SchoolLogo));
            GetStudents();
            GetEmployees();
            Cursor.Current = Cursors.Default;
        }
    }
}
