using CampusPortalBiometric.SQLiteServices;
using CampusPortalBiometric.WebServices;
using DPUruNet;
using Newtonsoft.Json;
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
    public partial class AttendanceScreen : Form
    {
        public Login _sender;
        public UserInfo _userInfo;
        private const int PROBABILITY_ONE = 0x7fffffff;
        private SQLStudentServices studentServices;
        private SQLEmployeeServices employeeServices;
        private EmployeeMgmt employeeMgmt;
        private StudentMgmt studentMgmt;
        private Fmd CapturedFingerprint;
        private List<Student> AllStudents;
        private List<Employee> AllEmployees;
        private Timer clockTimer;
        private enum Action
        {
            SendBitmap,
            SetStudentData,
            SetEmployeeData,
            SetStatus,
            ClearData
        }
        private enum Attendance
        {
            Unsuccessful,
            Successful
        }
        public AttendanceScreen()
        {
            InitializeComponent();
        }

        private void AttendanceScreen_Load(object sender, EventArgs e)
        {
            pbFingerprint.Image = null;
            clockTimer = new Timer();
            clockTimer.Enabled = true;
            clockTimer.Interval = 1000;
            clockTimer.Tick += ClockTimer_Tick;
            studentServices = new SQLStudentServices();
            employeeServices = new SQLEmployeeServices();
            employeeMgmt = new EmployeeMgmt();
            studentMgmt = new StudentMgmt();
            AllStudents = studentServices.GetRegisteredStudents();
            AllEmployees = employeeServices.GetRegisteredEmployees();
            if (!_sender.OpenReader())
            {
                this.Close();
            }

            if (!_sender.StartCaptureAsync(this.OnCaptured))
            {
                this.Close();
            }

        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            tbDateTime.Text = "Date and Time :        "+DateTime.Now.ToString();
        }

        public void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                SendMessage(Action.ClearData, null);
                CapturedFingerprint = null;
                if (!_sender.CheckCaptureResult(captureResult)) return;
                
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);
                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    _sender.Reset = true;
                    throw new Exception(resultConversion.ResultCode.ToString());
                }

                CapturedFingerprint = resultConversion.Data;
                //fin2 = XMLData;
                foreach (Fid.Fiv fiv in captureResult.Data.Views)
                {
                    var payload = _sender.CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    SendMessage(Action.SendBitmap, payload);
                }
                if (CapturedFingerprint != null)
                {
                    if (rbStudent.Checked)
                        VerifyStudent();
                    else
                        VerifyEmployee();
                }
                else
                {
                    SendMessage(Action.SetStatus, Attendance.Unsuccessful.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error"); // Send error message, then close form
                SendMessage(Action.SetStatus, Attendance.Unsuccessful.ToString());
                //SendMessage(Action.SendMessage, "Error:  " + ex.Message);
            }
        }

        private void VerifyStudent()
        {
            Student matchedStudent = null;
            string Time = string.Empty;

            foreach (var item in AllStudents)
            {
                if (item.Fingerprint != null)
                    {
                    var XMLPrint = item.Fingerprint.Replace("\\\"", "\""); 
                    var StData = Fmd.DeserializeXml(XMLPrint);
                    CompareResult compareResult = Comparison.Compare(StData, 0, CapturedFingerprint, 0);
                    if (compareResult.Score < (PROBABILITY_ONE / 100000))
                    {
                        matchedStudent = item;
                        Time = DateTime.Now.ToString();
                        break;
                    }
                }

            }
            string Answer = string.Empty;
            if (matchedStudent != null)
            {

                SendMessage(Action.SetStudentData, matchedStudent);
            }
            else
            {
                SendMessage(Action.SetStatus, Attendance.Unsuccessful.ToString());

            }
        }
        private void VerifyEmployee()
        {
            Employee matchedEmployee = null;
            string Time = string.Empty;

            foreach (var item in AllEmployees)
            {
                if (item.Fingerprint != null)
                {
                    var XMLPrint = item.Fingerprint.Replace("\\\"", "\""); 
                    var StData = Fmd.DeserializeXml(XMLPrint);
                    CompareResult compareResult = Comparison.Compare(StData, 0, CapturedFingerprint, 0);
                    if (compareResult.Score < (PROBABILITY_ONE / 100000))
                    {
                        matchedEmployee = item;
                        Time = DateTime.Now.ToString();
                        break;
                    }
                }

            }
            string Answer = string.Empty;
            if (matchedEmployee != null)
            {

                SendMessage(Action.SetEmployeeData, matchedEmployee);

                //Answer = "Matched with Id: " + Id + ", Name: " + Name + "At: " + Time;
            }
            else
            {
                //Answer = "Not Matched";
                SendMessage(Action.SetStatus, Attendance.Unsuccessful.ToString());

            }
        }

        private delegate void SendMessageCallback(Action action, object payload);
        private void SendMessage(Action action, object payload)
        {
            try
            {
                if (this.pbFingerprint.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    switch (action)
                    {
                        case Action.ClearData:
                            ClearData();
                            break;
                        case Action.SetStatus:
                            tbAttendance.Text = payload.ToString();
                            ClearData();
                            break;
                        case Action.SetStudentData:
                            var student = (Student)payload;
                            studentMgmt.MarkAttendance(student.Id,_userInfo.attendance_id,_userInfo.token,(rbIN.Checked)?"1":"2");
                            PopulateStudentData(student);
                            break;
                        case Action.SetEmployeeData:
                            var employee = (Employee)payload;
                            employeeMgmt.MarkAttendance(employee.Id, _userInfo.attendance_id, _userInfo.token, (rbIN.Checked) ? "1" : "2");
                            PopulateEmployeeData(employee);
                            break;
                        case Action.SendBitmap:
                            pbFingerprint.Image = (Bitmap)payload;
                            pbFingerprint.Refresh();
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void PopulateStudentData(Student student)
        {
            try
            {
                tbStudentID.Text = student.Id;
                tbStudentName.Text = student.Name;
                tbFatherName.Text = student.Father_Name;
                tbClass.Text = student.Class;
                tbAttendance.Text = Attendance.Successful.ToString();

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void PopulateEmployeeData(Employee employee)
        {
            try
            {
                tbStudentID.Text = employee.Id;
                tbStudentName.Text = employee.Name;
                tbFatherName.Text = employee.Father_Name;
                tbClass.Text = employee.Designation;
                tbAttendance.Text = Attendance.Successful.ToString();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ClearData()
        {
            try
            {
                tbStudentID.Text = string.Empty;
                tbStudentName.Text = string.Empty;
                tbFatherName.Text = string.Empty;
                tbClass.Text = string.Empty;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tbAttendance_TextChanged(object sender, EventArgs e)
        {
            if (tbAttendance.Text.Equals(Attendance.Successful.ToString()))
            {
                tbAttendance.BackColor = Color.LimeGreen;
            }
            else
            {
                tbAttendance.BackColor = Color.OrangeRed;

            }
        }

        private void AttendanceScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
        }

        private void rbStudent_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStudent.Checked)
            {
                ClearData();
                lblClass.Text = "Class";
            }
        }

        private void rbEmployee_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEmployee.Checked)
            {
                ClearData();
                lblClass.Text = "Designation";

            }
        }
    }
}
