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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CampusPortalBiometric.Utils.Entities;

namespace CampusPortalBiometric
{
    public partial class UpdateFingerprint : Form
    {
        public Login _sender;
        public UserInfo _userInfo;
        private List<Student> RegisteredStudents;
        private List<Employee> RegisteredEmployees;
        List<Fmd> preenrollmentFmds;
        private SQLStudentServices studentServices;
        private StudentMgmt studentMgmt;
        private EmployeeMgmt employeeMgmt;
        private SQLEmployeeServices employeeServices;
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
        public UpdateFingerprint()
        {
            InitializeComponent();
            RegisteredEmployees = new List<Employee>();
            RegisteredStudents = new List<Student>();
            studentServices = new SQLStudentServices();
            employeeServices = new SQLEmployeeServices();
            studentMgmt = new StudentMgmt();
            employeeMgmt = new EmployeeMgmt();
        }


        private void UpdateFingerprint_Load(object sender, EventArgs e)
        {
            RegisteredStudents = studentServices.GetRegisteredStudents();
            RegisteredEmployees = employeeServices.GetRegisteredEmployees();
            rbStudents.Checked = true;
            cbFilter.SelectedIndex = 1;
            dgStudents.Columns["Fingerprint"].Visible = false;
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
            dgStudents.DataSource = RegisteredEmployees;
        }

        private void UpgradeStudentDataGrid()
        {
            dgStudents.DataSource = RegisteredStudents;
        }

        private void dgStudents_SelectionChanged(object sender, EventArgs e)
        {
            var row = dgStudents.CurrentCell.RowIndex;
            var ID = dgStudents.Rows[row].Cells[0].Value.ToString();
            if (rbStudents.Checked)
                SelectedRow = RegisteredStudents.FindIndex(x => x.Id == ID);
            else
                SelectedRow = RegisteredEmployees.FindIndex(x => x.Id == ID);
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
                            btnUpdate.Enabled = Convert.ToBoolean(payload);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (rbStudents.Checked)
            {
                studentServices.UpdateStudentFPrint(ID, XMLPrint);
                studentMgmt.RegisterorUpdateStudentFPrint(ID, XMLPrint, _userInfo.token);
            }
            else
            {
                employeeServices.UpdateEmployeeFPrint(ID, XMLPrint);
                employeeMgmt.RegisterorUpdateEmployeeFPrint(ID, XMLPrint, _userInfo.token);
            }
            RegisteredStudents = studentServices.GetRegisteredStudents();
            RegisteredEmployees = employeeServices.GetRegisteredEmployees();
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

        private void ClearForm()
        {
            preenrollmentFmds.Clear();
            count = 0;
            ID = string.Empty;
            SName = string.Empty;
            XMLPrint = string.Empty;
        }

        private void OnCaptured(CaptureResult captureResult)
        {
            try
            {
                if (rbStudents.Checked)
                {
                    ID = RegisteredStudents[SelectedRow].Id;
                    SName = RegisteredStudents[SelectedRow].Name;
                }
                else
                {
                    ID = RegisteredEmployees[SelectedRow].Id;
                    SName = RegisteredEmployees[SelectedRow].Name;
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
                        SendMessage(Action.SendMessage, "Fingerprint is successfully created. \r\nPress Update to Save.");
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            _sender.CancelCaptureAndCloseReader(this.OnCaptured);
            this.Close();
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
                            SearchedStudents = RegisteredStudents.FindAll(x => x.Id == SearchText);
                        else
                            SearchedEmployees = RegisteredEmployees.FindAll(x => x.Id == SearchText);
                        break;
                    case 1://Name
                        if (rbStudents.Checked)
                            SearchedStudents = RegisteredStudents.FindAll(x => x.Name.ToUpper().Contains(SearchText));
                        else
                            SearchedEmployees = RegisteredEmployees.FindAll(x => x.Name.ToUpper().Contains(SearchText));
                        break;
                    case 2://Father Name
                        if (rbStudents.Checked)
                            SearchedStudents = RegisteredStudents.FindAll(x => x.Father_Name.ToUpper().Contains(SearchText));
                        else
                            SearchedEmployees = RegisteredEmployees.FindAll(x => x.Father_Name.ToUpper().Contains(SearchText));
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
                        dgStudents.DataSource = RegisteredStudents;
                    else
                        dgStudents.DataSource = RegisteredEmployees;
                }
            }
            else
            {
                if (rbStudents.Checked)
                    dgStudents.DataSource = RegisteredStudents;
                else
                    dgStudents.DataSource = RegisteredEmployees;
            }
        }
    }
}
