using BusinessLayer;
using Driving_License_Management.GlobalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BusinessLayer.clsApplication;

namespace Driving_License_Management.Controls
{
    public partial class ucSchedule : UserControl
    {
        enum enMode { AddNew = 0, Update = 1 }
        public ucSchedule()
        {
            InitializeComponent();
        }

        clsTestAppointment _TestAppointment;
        clsLocalDrivingLicenseApplication LDLApplication;
        int _TestTypeID;
        enMode _Mode = enMode.AddNew;
        bool _RetakeTestInfoEnabled = false;

       public bool RetakeTestInfoEnabled
        {
            get { return _RetakeTestInfoEnabled; }
            set {
                _RetakeTestInfoEnabled = value;
                gbRetakeTestInfo.Enabled = _RetakeTestInfoEnabled;
            }
        }




        public void LoadInfo(int LocalDrivingLicenseApplicationID, int TestTypeID,bool IsRetakeTest = false)
        {

            
            LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            _TestTypeID = TestTypeID;

            if (LDLApplication == null)
            {
                _ResetInfo();


                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Mode = enMode.AddNew;
            _TestAppointment = new clsTestAppointment();
            _TestAppointment.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointment.TestTypeID = TestTypeID;
            _FillInfo(IsRetakeTest);
        }

        public void LoadInfo(int TestAppointmentID, bool IsRetakeTest = false)
        {
            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);
            _TestTypeID = _TestAppointment.TestTypeID;
            if (_TestAppointment == null) {

                MessageBox.Show("No Test Appointment with TestAppointmentID = " + TestAppointmentID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_TestAppointment.LocalDrivingLicenseApplicationID);
            if (LDLApplication == null)
            {
                _ResetInfo();


                MessageBox.Show("No Application with ApplicationID = " + _TestAppointment.LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Mode = enMode.Update;
            dtpTestDate.Value = _TestAppointment.AppointmentDate;
            _FillInfo(IsRetakeTest);

        }

        private void _FillInfo(bool IsRetakeTest)
        {

            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = clsLicenseClass.Find(LDLApplication.LicenseClassID).ClassName;
            lblFullName.Text = LDLApplication.FullName;
            lblTrial.Text = clsTestAppointment.TotalTrialPerTest(_TestAppointment.LocalDrivingLicenseApplicationID, _TestAppointment.TestTypeID).ToString();  
            lblFees.Text = clsTestType.FindTestType(_TestAppointment.TestTypeID).Fees.ToString();
            dtpTestDate.MinDate = DateTime.Now;
            if (IsRetakeTest) {
            
                clsApplication Application = new clsApplication();

                Application.ApplicantPersonID = LDLApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = Convert.ToByte(clsApplication.enStatus.New);
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplcationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save()) {
                    MessageBox.Show("An Error Occurred!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                decimal TotalFees = clsApplcationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees +
                  clsTestType.FindTestType(_TestTypeID).Fees  ;
                lblRetakeTestAppID.Text = Application.ApplicationID.ToString();
                lblRetakeAppFees.Text = clsApplcationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                lblTotalFees.Text = TotalFees.ToString();
                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
                gbRetakeTestInfo.Enabled = true;
                
            
            }
           

        }
        private void _ResetInfo()
        {
            lblLocalDrivingLicenseAppID.Text = "[????]";
            lblLocalDrivingLicenseAppID.Text = "[????]";
            lblDrivingClass.Text = "[????]";
            lblFullName.Text = "[????]";
            lblTrial.Text = "[????]";
            lblFees.Text = "[????]";
            dtpTestDate.MinDate = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

           
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.CreatedByUserID=clsGlobal.CurrentUser.UserID;

            if (gbRetakeTestInfo.Enabled)
            {
                _TestAppointment.PaidFees = Convert.ToDecimal(lblTotalFees.Text); 
            }
            else
            {
                _TestAppointment.PaidFees = Convert.ToDecimal(lblFees.Text);

            }
            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
    }

