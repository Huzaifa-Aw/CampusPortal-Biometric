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
    public partial class RegisterScreen : Form
    {

        public Login _sender;
        public UserInfo _userInfo;
        private List<Student> NonRegisteredStudents;
        private List<Employee> NonRegisteredEmployees;
        List<Fmd> preenrollmentFmds;
        private SQLStudentServices studentServices;
        private SQLEmployeeServices employeeServices;
        private StudentMgmt studentMgmt;
        private EmployeeMgmt employeeMgmt;
        private int SelectedRow = -1;
        private string ID;
        private string SName;
        private string XMLPrint;
        int count;
        private List<Student> SearchedStudents;
        private List<Employee> SearchedEmployees;

        private enum Action
        {
            SendMessage, UpdateBtn, SendDialog
        }
        public RegisterScreen()
        {
            InitializeComponent();
            NonRegisteredEmployees = new List<Employee>();
            NonRegisteredStudents = new List<Student>();
            studentServices = new SQLStudentServices();
            employeeServices = new SQLEmployeeServices();
            studentMgmt = new StudentMgmt();
            employeeMgmt = new EmployeeMgmt();
        }


        private void RegisterScreen_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                NonRegisteredStudents = studentMgmt.GetNonBiometricStudents(_userInfo.token, _userInfo.SchoolID);
                NonRegisteredEmployees = employeeMgmt.GetNonBiometricStudents(_userInfo.token, _userInfo.SchoolID);
                rbStudents.Checked = true;
                cbFilter.SelectedIndex = 1;
                dgStudents.Columns["Fingerprint"].Visible = false;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                this.Close();
            }
        }

        private void rbStudents_CheckedChanged(object sender, EventArgs e)
        {
            dgStudents.DataSource = null;
            if (rbStudents.Checked)
            {
                UpgradeStudentDataGrid();
            }
        }
        private void rbEmployee_CheckedChanged(object sender, EventArgs e)
        {
            dgStudents.DataSource = null;
            if (rbEmployee.Checked)
            {
                UpgradeEmployeeDataGrid();
            }
        }

        private void UpgradeEmployeeDataGrid()
        {
            dgStudents.DataSource = NonRegisteredEmployees;
        }

        private void UpgradeStudentDataGrid()
        {
            dgStudents.DataSource = NonRegisteredStudents;
        }

        private void dgStudents_SelectionChanged(object sender, EventArgs e)
        {
            var row = dgStudents.CurrentCell.RowIndex;
            var ID = dgStudents.Rows[row].Cells[0].Value.ToString();
            if (rbStudents.Checked)
                SelectedRow = NonRegisteredStudents.FindIndex(x => x.Id == ID);
            else
                SelectedRow = NonRegisteredEmployees.FindIndex(x => x.Id == ID);
        }

        private void btnStartScan_Click(object sender, EventArgs e)
        {
            if (SelectedRow >= 0)
            {
                txtEnroll.Text = string.Empty;
                preenrollmentFmds = new List<Fmd>();
                count = 0;

                SendMessage(Action.SendMessage, "Place a finger on the reader.");
                _sender.CancelCaptureAndCloseReader(this.OnCaptured);

                if (!_sender.OpenReader())
                {
                    this.Close();
                }

                if (!_sender.StartCaptureAsync(this.OnCaptured))
                {
                    this.Close();
                }
            }
        }
        private void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                if (rbStudents.Checked)
                {
                    ID = NonRegisteredStudents[SelectedRow].Id;
                    SName = NonRegisteredStudents[SelectedRow].Name;
                }
                else
                {
                    ID = NonRegisteredEmployees[SelectedRow].Id;
                    SName = NonRegisteredEmployees[SelectedRow].Name;
                }

                // Check capture quality and throw an error if bad.
                if (!_sender.CheckCaptureResult(captureResult)) return;

                count++;
                if (count > 4)
                {
                    return;
                }
                DataResult<Fmd> resultConversion = FeatureExtraction.CreateFmdFromFid(captureResult.Data, Constants.Formats.Fmd.ANSI);

                SendMessage(Action.SendMessage, "A finger was captured.  \r\nCount:  " + (count));

                if (resultConversion.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    _sender.Reset = true;
                    throw new Exception(resultConversion.ResultCode.ToString());
                }

                preenrollmentFmds.Add(resultConversion.Data);

                if (count == 4)
                {
                    DataResult<Fmd> resultEnrollment = DPUruNet.Enrollment.CreateEnrollmentFmd(Constants.Formats.Fmd.ANSI, preenrollmentFmds);

                    if (resultEnrollment.ResultCode == Constants.ResultCode.DP_SUCCESS)
                    {
                        XMLPrint = Fmd.SerializeXml(resultEnrollment.Data);
                        SendMessage(Action.SendMessage, "Fingerprint is successfully created. \r\nPress Register to Save.");
                        SendMessage(Action.UpdateBtn, "true");
                        return;
                    }
                    else if (resultEnrollment.ResultCode == Constants.ResultCode.DP_ENROLLMENT_INVALID_SET)
                    {
                        SendMessage(Action.SendMessage, "Enrollment was unsuccessful.  Please try again.");
                        SendMessage(Action.SendMessage, "Place a finger on the reader.");
                        preenrollmentFmds.Clear();
                        count = 0;
                        return;
                    }
                }

                SendMessage(Action.SendMessage, "Now place the same finger on the reader.");
            }
            catch (Exception ex)
            {
                // Send error message, then close form
                SendMessage(Action.SendMessage, "Error:  " + ex.Message);
            }
        }
        private delegate void SendMessageCallback(Action action, string payload);
        private void SendMessage(Action action, string payload)
        {
            try
            {
                if (this.txtEnroll.InvokeRequired)
                {
                    SendMessageCallback d = new SendMessageCallback(SendMessage);
                    this.Invoke(d, new object[] { action, payload });
                }
                else
                {
                    switch (action)
                    {
                        case Action.SendDialog:
                            MessageBox.Show(payload, "Success");
                            break;
                        case Action.SendMessage:
                            txtEnroll.Text += payload + "\r\n\r\n";
                            txtEnroll.SelectionStart = txtEnroll.TextLength;
                            txtEnroll.ScrollToCaret();
                            break;
                        case Action.UpdateBtn:
                            btnRegister.Enabled = Convert.ToBoolean(payload);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }


        private void ClearForm()
        {
            preenrollmentFmds.Clear();
            count = 0;
            ID = string.Empty;
            SName = string.Empty;
            XMLPrint = string.Empty;
        }



        private void btnBack_Click(object sender, EventArgs e)
        {
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (rbStudents.Checked)
                {
                    var student = NonRegisteredStudents.Find(x => x.Id == ID);
                    studentMgmt.RegisterorUpdateStudentFPrint(ID, XMLPrint, _userInfo.token);
                    studentServices.RegisterStudent(student, XMLPrint);
                }
                else
                {
                    var employee = NonRegisteredEmployees.Find(x => x.Id == ID);
                    employeeMgmt.RegisterorUpdateEmployeeFPrint(ID, XMLPrint, _userInfo.token);
                    employeeServices.RegisterEmployee(employee, XMLPrint);
                }
                NonRegisteredStudents = studentMgmt.GetNonBiometricStudents(_userInfo.token, _userInfo.SchoolID);
                NonRegisteredEmployees = employeeMgmt.GetNonBiometricStudents(_userInfo.token, _userInfo.SchoolID);
                if (rbStudents.Checked)
                    UpgradeStudentDataGrid();
                else
                    UpgradeEmployeeDataGrid();
                SendMessage(Action.SendMessage, "Fingerprint Saved successfully for " + SName + ".");
                SendMessage(Action.SendDialog, "Fingerprint Saved successfully for " + SName + ".");
                ClearForm();
                SendMessage(Action.UpdateBtn, "false");
                _sender.CancelCaptureAndCloseReader(this.OnCaptured);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                _sender.CancelCaptureAndCloseReader(this.OnCaptured);
                MessageBox.Show(ex.Message,"Error");
                this.Close();
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text.Length > 0)
            {
                var SearchText = tbSearch.Text.ToUpper();
                SearchedStudents = new List<Student>();
                SearchedEmployees = new List<Employee>();
                switch (cbFilter.SelectedIndex)
                {
                    case 0://Id
                        if (rbStudents.Checked)
                            SearchedStudents = NonRegisteredStudents.FindAll(x => x.Id == SearchText);
                        else
                            SearchedEmployees = NonRegisteredEmployees.FindAll(x => x.Id == SearchText);
                        break;
                    case 1://Name
                        if (rbStudents.Checked)
                            SearchedStudents = NonRegisteredStudents.FindAll(x => x.Name.ToUpper().Contains(SearchText));
                        else
                            SearchedEmployees = NonRegisteredEmployees.FindAll(x => x.Name.ToUpper().Contains(SearchText));
                        break;
                    case 2://Father Name
                        if (rbStudents.Checked)
                            SearchedStudents = NonRegisteredStudents.FindAll(x => x.Father_Name.ToUpper().Contains(SearchText));
                        else
                            SearchedEmployees = NonRegisteredEmployees.FindAll(x => x.Father_Name.ToUpper().Contains(SearchText));
                        break;
                    default:
                        break;
                }
                if (SearchedEmployees.Count > 0 || SearchedStudents.Count > 0)
                {
                    if (rbStudents.Checked)
                        dgStudents.DataSource = SearchedStudents;
                    else
                        dgStudents.DataSource = SearchedEmployees;
                }
                else
                {
                    if (rbStudents.Checked)
                        dgStudents.DataSource = NonRegisteredStudents;
                    else
                        dgStudents.DataSource = NonRegisteredEmployees;
                }
            }
            else
            {
                if (rbStudents.Checked)
                    dgStudents.DataSource = NonRegisteredStudents;
                else
                    dgStudents.DataSource = NonRegisteredEmployees;
            }
        }
    }
}
