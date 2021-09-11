using CampusPortalBiometric.SQLiteServices;
using CampusPortalBiometric.WebServices;
using DPUruNet;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CampusPortalBiometric.Utils.Entities;

namespace CampusPortalBiometric
{
    public partial class Login : Form
    {
        private UserInfo _userInfo;
        private LoginMgmt loginMgmt;
        public bool Reset
        {
            get { return reset; }
            set { reset = value; }
        }
        private bool reset;
        private Reader currentReader;
        static readonly object locker = new object();

        public Reader CurrentReader
        {
            get { return currentReader; }
            set
            {
                currentReader = value;
            }
        }
        public Login()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Open a device and check result for errors.
        /// </summary>
        /// <returns>Returns true if successful; false if unsuccessful</returns>
        public bool OpenReader()
        {
            using (Tracer tracer = new Tracer("Form1::OpenReader"))
            {
                reset = false;
                Constants.ResultCode result = Constants.ResultCode.DP_DEVICE_FAILURE;

                // Open reader
                result = currentReader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

                if (result != Constants.ResultCode.DP_SUCCESS)
                {
                    MessageBox.Show("Error:  " + result);
                    reset = true;
                    return false;
                }

                return true;
            }
        }

        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }


        /// <summary>
        /// Check quality of the resulting capture.
        /// </summary>
        public bool CheckCaptureResult(CaptureResult captureResult)
        {
            using (Tracer tracer = new Tracer("Form1::CheckCaptureResult"))
            {
                if (captureResult.Data == null || captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                {
                    if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        reset = true;
                        throw new Exception(captureResult.ResultCode.ToString());
                    }

                    // Send message if quality shows fake finger
                    if ((captureResult.Quality != Constants.CaptureQuality.DP_QUALITY_CANCELED))
                    {
                        throw new Exception("Quality - " + captureResult.Quality);
                    }
                    return false;
                }

                return true;
            }
        }



        /// <summary>
        /// Hookup capture handler and start capture.
        /// </summary>
        /// <param name="OnCaptured">Delegate to hookup as handler of the On_Captured event</param>
        /// <returns>Returns true if successful; false if unsuccessful</returns>
        public bool StartCaptureAsync(Reader.CaptureCallback OnCaptured)
        {
            using (Tracer tracer = new Tracer("Form1::StartCaptureAsync"))
            {
                // Activate capture handler
                currentReader.On_Captured += new Reader.CaptureCallback(OnCaptured);

                // Call capture
                if (!CaptureFingerAsync())
                {
                    return false;
                }

                return true;
            }
        }
        public void CancelCaptureAndCloseReader(Reader.CaptureCallback OnCaptured)
        {
            using (Tracer tracer = new Tracer("Form_Main::CancelCaptureAndCloseReader"))
            {
                if (currentReader != null)
                {
                    currentReader.CancelCapture();
                    currentReader.Dispose();
                    CurrentReader = null;
                    SelectReader();
                    // Dispose of reader handle and unhook reader events.

                    //if (reset)
                    //{
                    //    CurrentReader = null;
                    //}
                }
            }
        }
        public bool CaptureFingerAsync()
        {
            using (Tracer tracer = new Tracer("Form1::CaptureFingerAsync"))
            {
                try
                {
                    GetStatus();

                    Constants.ResultCode captureResult = currentReader.CaptureAsync(Constants.Formats.Fid.ANSI, Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT, currentReader.Capabilities.Resolutions[0]);
                    if (captureResult != Constants.ResultCode.DP_SUCCESS)
                    {
                        reset = true;
                        throw new Exception("" + captureResult);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:  " + ex.Message);
                    return false;
                }
            }
        }

        public void GetStatus()
        {
            using (Tracer tracer = new Tracer("Form1::GetStatus"))
            {
                Constants.ResultCode result = currentReader.GetStatus();

                if ((result != Constants.ResultCode.DP_SUCCESS))
                {
                    reset = true;
                    throw new Exception("" + result);
                }

                if (currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_BUSY)
                {
                    Thread.Sleep(50);
                }
                else if ((currentReader.Status.Status == Constants.ReaderStatuses.DP_STATUS_NEED_CALIBRATION))
                {
                    currentReader.Calibrate();
                }
                else if ((currentReader.Status.Status != Constants.ReaderStatuses.DP_STATUS_READY))
                {
                    throw new Exception("Reader Status - " + currentReader.Status.Status);
                }
            }
        }


        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                bool isLoggedin = false;
                string errorMsg = string.Empty;
                Cursor.Current = Cursors.WaitCursor;
                if (tbUserId.Text.Length == 0||tbUserId.Text.Equals("Enter Username"))
                    throw new Exception("Username cannot be empty.");
                if (tbPassword.Text.Length == 0 || tbPassword.Text.Equals("Enter Password"))
                    throw new Exception("Password cannot be empty."); 
                isLoggedin = loginMgmt.Login(tbUserId.Text, tbPassword.Text, ref _userInfo, ref errorMsg);
                Cursor.Current = Cursors.Default;

                if (errorMsg.Length > 0)
                {
                    MessageBox.Show(errorMsg, "Error");
                    return;
                }

                if (isLoggedin)
                {
                    //MainScreen frmMainScreen = new MainScreen(_userInfo);
                    //CaptureTest frmMainScreen = new CaptureTest();


                    MenuScreen frmMainScreen = new MenuScreen();
                    frmMainScreen._sender = this;
                    frmMainScreen._userinfo = _userInfo;
                    this.Hide();
                    frmMainScreen.FormClosed += (s, args) => this.Close();
                    frmMainScreen.Show();

                }
                else
                {
                    MessageBox.Show("Login Unsuccessful", "Error");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.loginMgmt = new LoginMgmt();
            SelectReader();
            tbUserId.Text = "Enter Username";
            tbPassword.Text = "Enter Password";
            tbPassword.UseSystemPasswordChar = false;
            //tbUserId.Text = "principle@admin";
            //tbPassword.Text = "123321";

        }
        public void RemoveUPass(object sender, EventArgs e)
        {
            if (tbPassword.Text.Equals("Enter Password"))
            {
            tbPassword.Text = "";
            }
            tbPassword.UseSystemPasswordChar = true;
        }

        public void AddUPass(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbPassword.Text))
            {
                tbPassword.Text = "Enter Password";
                tbPassword.UseSystemPasswordChar = false;
            }
        }
        public void RemoveUID(object sender, EventArgs e)
        {
            if (tbUserId.Text.Equals("Enter Username"))
                tbUserId.Text = "";
        }

        public void AddUID(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(tbUserId.Text))
                tbUserId.Text = "Enter Username";
        }
        private void SelectReader()
        {
            var allReaderd = ReaderCollection.GetReaders();
            if (allReaderd.Count > 0)
            {
                CurrentReader = allReaderd[0];
            }
            else
            {
                var confirm = MessageBox.Show("Error:  " + "Please connect the biometric device first.");
                if (confirm == DialogResult.OK)
                {
                    Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            tbUserId.Focus();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            tbPassword.Focus();
        }

    }

}
