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

namespace Driving_License_Management.Tests
{
    public partial class frmAddUpdateTestAppointment : Form
    {
        enum enMode { AddNew = 0, Update = 1 }
        int _TestAppointmentID;
        clsTestAppointment _TestAppointment;
        int _LocalDrivingLicenseApplicationID;
        int _TestTypeID;
        bool _IsRetakeTest = false;
        enMode _Mode;
        public frmAddUpdateTestAppointment(int LocalDrivingLicenseApplicationID,
        int TestTypeID,
        bool isRatekeTest = false)
        {
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _Mode = enMode.AddNew;
            InitializeComponent();
            _IsRetakeTest = isRatekeTest;
        }

        public frmAddUpdateTestAppointment(int TestAppointmentID,bool isRatekeTest = false)
        {
            _TestAppointmentID = TestAppointmentID;
           _Mode =enMode.Update;
            _IsRetakeTest = isRatekeTest;
            InitializeComponent();

        }

        private void frmAddTestAppointment_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                if (_IsRetakeTest)
                {
                    ucSchedule1.LoadInfo(_TestAppointmentID,true);
                    return;
                }
                ucSchedule1.LoadInfo(_TestAppointmentID);

            }
            else
            {
                if (_IsRetakeTest)
                {
                    ucSchedule1.LoadInfo(_LocalDrivingLicenseApplicationID, _TestTypeID, true);

                    return;
                }

                ucSchedule1.LoadInfo(_LocalDrivingLicenseApplicationID, _TestTypeID);

            }
        }
    }
}
