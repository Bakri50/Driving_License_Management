using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_License_Management.Controls
{
    public partial class ucScheduled : UserControl
    {

        clsTestAppointment _TestAppointment;
        int _TestID;

        public int TestID
        {
            get { return _TestID; }
            set
            {
                _TestID = value;
                lblTestID.Text = _TestID.ToString();
            }
        }

        public ucScheduled()
        {
            InitializeComponent();
        }


        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);
            if (_TestAppointment == null)
            {

                MessageBox.Show("No Test Appointment with TestAppointmentID = " + TestAppointmentID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            _FillInfo();

        }
        private void _FillInfo()
        {

            lblLocalDrivingLicenseAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_TestAppointment.LocalDrivingLicenseApplicationID);
            lblDrivingClass.Text = clsLicenseClass.Find(LDLApplication.LicenseClassID).ClassName;
            lblFullName.Text = LDLApplication.FullName;
            lblTrial.Text = clsTestAppointment.TotalTrialPerTest(_TestAppointment.LocalDrivingLicenseApplicationID, _TestAppointment.TestTypeID1).ToString();
            if (_TestAppointment.RetakeTestApplicationID > 0) {
                lblFees.Text = (clsTestType.Find(_TestAppointment.TestTypeID1).Fees + clsApplication.FindBaseApplication(_TestAppointment.RetakeTestApplicationID).PaidFees).ToString();
            }
            else lblFees.Text = clsTestType.Find(_TestAppointment.TestTypeID1).Fees.ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();

        }
        private void _ResetInfo()
        {
            lblLocalDrivingLicenseAppID.Text = "[????]";
            lblLocalDrivingLicenseAppID.Text = "[????]";
            lblDrivingClass.Text = "[????]";
            lblFullName.Text = "[????]";
            lblTrial.Text = "[????]";
            lblFees.Text = "[????]";
            lblDate.Text = "[??/??/????]";
        }

    }


}
