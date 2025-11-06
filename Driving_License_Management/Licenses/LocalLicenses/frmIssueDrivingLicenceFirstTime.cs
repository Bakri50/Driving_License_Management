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
using Driving_License_Management.GlobalClasses;

namespace Driving_License_Management.Licenses.LocalLicenses
{
    public partial class frmIssueDrivingLicenceFirstTime : Form
    {
        clsLicense _License = new clsLicense();
        clsDriver _Driver;
        int _LocalDrivingLicenseApplication;
        clsLocalDrivingLicenseApplication _LDLApplication;
        public frmIssueDrivingLicenceFirstTime()
        {
            InitializeComponent();
        }
        public frmIssueDrivingLicenceFirstTime(int LocalDrivingLicenseApplication)
        {
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication;
            InitializeComponent();

        }

        private void _RefreshData()
        {
            _LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplication);
            if (_LDLApplication == null) { 
            
              MessageBox.Show("Application Not Found", "",MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueLicense.Enabled = false;
                return;
            }
            btnIssueLicense.Enabled = true;

            ucLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplication);



        }
        private void frmIssueDrivingLicenceFirstTime_Load(object sender, EventArgs e)
        {
            _RefreshData();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
          
           _License.LicenseClassID = _LDLApplication.LicenseClassID;
            _License.PaidFees = clsLicenseClass.Find(_License.LicenseClassID).ClassFees;
            _License.Notes = txtNotes.Text;
            _License.IssueDate = DateTime.Now;
            _License.IsActive = 1;
            _License.ApplicationID = _LDLApplication.ApplicationID;
            _License.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _License.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.Find(_LDLApplication.LicenseClassID).DefaultValidityLength);
            _License.IssueReason = 1;
            if (clsDriver.IsExistByPersonID(_LDLApplication.ApplicantPersonID))
            {
                _Driver = clsDriver.FindByPersonID(_LDLApplication.ApplicantPersonID);
                if (_Driver == null)
                {
                    MessageBox.Show("Driver Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                _Driver = new clsDriver();
                _Driver.PersonID = _LDLApplication.ApplicantPersonID;
                _Driver.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                _Driver.CreatedTime = DateTime.Now;

                if (!_Driver.Save())
                {
                    MessageBox.Show("Driver Data not Saved Succesfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            _License.DriverID = _Driver.DriverID;
            if (!_License.Save()) {
                MessageBox.Show("License Data not Saved Succesfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LDLApplication.Complete();
            MessageBox.Show("Saved Succesfuly with License ID = " + _License.LicenseID, "Inforamtion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnClose.Enabled = false;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
