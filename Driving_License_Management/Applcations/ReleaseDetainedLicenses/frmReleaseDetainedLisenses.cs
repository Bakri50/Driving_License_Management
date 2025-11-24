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
using Driving_License_Management.Licenses;
using Driving_License_Management.Licenses.LocalLicenses;

namespace Driving_License_Management.Applcations.ReleaseDetainedLicenses
{
    public partial class frmReleaseDetainedLisenses : Form
    {
        int _LicenseID;
        clsDetainedLicense DetainedLicense;
        clsApplicationType ApplicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense);

        public frmReleaseDetainedLisenses()
        {
            InitializeComponent();
        }
        public void LoadLicenseInfo(int LicenseID)
        {
            ucDriverLicenseWithFilter1.LoadLicenseInfo(LicenseID);
            ucDriverLicenseWithFilter1.FilterEnabeld = false;
        }
        private void frmReleaseDetainedLisenses_Load(object sender, EventArgs e)
        {
            this.Height = 515;
            lblApplicationFees.Text = ((float)clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees).ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ucDriverLicenseWithFilter1_OnLicenseSelected(int obj)
        {
           _LicenseID = obj;
            DetainedLicense = clsDetainedLicense.FindByLicenseID(_LicenseID);
            lblLicenseID.Text = _LicenseID.ToString();
            llShowLicenseHistory.Enabled = (_LicenseID != -1);
            
            if(DetainedLicense == null)
            {
                MessageBox.Show("This License is not detained", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (DetainedLicense.IsReleased)
            {
                MessageBox.Show("This License already released", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblFineFees.Text = DetainedLicense.FineFess.ToString();
            lblDetainID.Text = DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = DetainedLicense.DetainDate.ToShortDateString();
            lblTotalFees.Text = ((float)ApplicationType.Fees + DetainedLicense.FineFess).ToString();

            btnRelease.Enabled = true;
            llShowLicenseInfo.Enabled = true;


        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = ucDriverLicenseWithFilter1.SelectedLicense.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationStatus = 3; //Complated
            Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            Application.ApplicationTypeID = ApplicationType.ApplicationTypeID;
            Application.PaidFees = ApplicationType.Fees;
            Application.LastStatusDate = DateTime.Now;

            if (!Application.Save()) {

                MessageBox.Show("An error Occurrd", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

         

            DetainedLicense.ReleaseApplicationID = Application.ApplicationID;
            DetainedLicense.ReleaseDate = DateTime.Now;
            DetainedLicense.ReleasedByUserID = clsGlobal.CurrentUser.UserID;

           
            if (!DetainedLicense.ReleaseLicense()) {


                MessageBox.Show("An error Occurrd", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("The License Released Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblApplicationID.Text = Application.ApplicationID.ToString();

            btnRelease.Enabled = false;
            ucDriverLicenseWithFilter1.FilterEnabeld = false;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseInfo frm = new frmDriverLicenseInfo(_LicenseID);

            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonLicensesHistory frm = new frmPersonLicensesHistory(ucDriverLicenseWithFilter1.SelectedLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
