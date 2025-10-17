﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Driving_License_Management.GlobalClasses;

namespace Driving_License_Management.Applcations.LocalDrivingLicenseApplication
{
    public partial class frmAddUpdataLocalDrivingLicenseApplications : Form
    {
        enum enMode { AddNew = 1, Update =2}
        enMode _Mode = enMode.AddNew;

        int _LocalDrivingLicenseApplicationID;
        clsLocalDrivingLicenseApplication LDLApication;
        clsApplication Application;
        public frmAddUpdataLocalDrivingLicenseApplications()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdataLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }
        private void _FilCmboWithLicenseClassNames()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClases();

            foreach (DataRow r in dt.Rows)
            {
                cmbLicenseClass.Items.Add(r["ClassName"]);
            }
        }

        private void _ResatDefaultValues()
        {
            _FilCmboWithLicenseClassNames();
            cmbLicenseClass.SelectedIndex = 0;

            if (_Mode == enMode.AddNew)
            {
                LDLApication = new clsLocalDrivingLicenseApplication();
                lbApplicationDate.Text = DateTime.Now.ToShortDateString();
                lbApplicationFees.Text = ((int)(clsApplcationType.Find((int)clsApplication.enApplicationType.NewLocalDrivingLicenseService)).Fees).ToString();
                lbCreatedBy.Text = clsGlobal.CurrentUser.UserName;
                cmbLicenseClass.SelectedIndex = 2;
                ucPersonInfoWithFilter1.FilterFocus();
                return;
            }
            if (_Mode == enMode.Update) {

                this.Text = "Update Local Driving License Applications";
                lbHeader.Text = "Update Local Driving License Applications";
                ucPersonInfoWithFilter1.FilterEnabeled = false;

                LDLApication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            }


        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if(ucPersonInfoWithFilter1.PersonID == -1)
            {
                MessageBox.Show("You Must Choose or Add Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tabControl1.SelectedIndex < tabControl1.TabPages.Count - 1)
            {

                tabControl1.SelectedIndex++;
                btnSave.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex > 0)
            {

                tabControl1.SelectedIndex--;
                btnSave.Enabled = false;

            }
        }

        private void frmAddUpdataLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            _ResatDefaultValues();
            if (_Mode == enMode.Update) {
                _LoadData();
            }

        }

        private void _LoadApplicationsData()
        {
            Application.ApplicantPersonID = ucPersonInfoWithFilter1.PersonID;
            Application.ApplicationStatus = 1; //New;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = 1; //New License
            Application.LastStatusDate = DateTime.Now;
            Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            Application.PaidFees = clsApplcationType.Find(Application.ApplicationTypeID).Fees;
        }
        private void _LoadData()
        {
            if (LDLApication == null) {
                MessageBox.Show("No Local drivig license application with ID = " + _LocalDrivingLicenseApplicationID);
                return;
            }

            lbDLApplicationID.Text = LDLApication.LocalDrivingLicenseApplicationID.ToString();
            lbApplicationDate.Text = LDLApication.ApplicationDate.ToString();
            lbApplicationFees.Text = LDLApication.PaidFees.ToString();
            clsUser CurrentUser = clsUser.FindUser(LDLApication.CreatedByUserID);

            if (CurrentUser != null) lbCreatedBy.Text = CurrentUser.UserName; else lbCreatedBy.Text = "";

            cmbLicenseClass.SelectedIndex = cmbLicenseClass.FindString(clsLicenseClass.Find(LDLApication.LicenseClassID).ClassName);
            if(_Mode == enMode.Update)
            ucPersonInfoWithFilter1.LoadPersonInfo(LDLApication.ApplicantPersonID);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) {

                MessageBox.Show("Application already Exist or Complated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; }


            LDLApication.ApplicantPersonID = ucPersonInfoWithFilter1.PersonID;
            LDLApication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewLocalDrivingLicenseService;
            LDLApication.ApplicationDate = DateTime.Now;
            LDLApication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            LDLApication.ApplicationStatus = Convert.ToByte(clsApplication.enStatus.New);
            LDLApication.LastStatusDate = DateTime.Now;
            LDLApication.LicenseClassID = clsLicenseClass.Find(cmbLicenseClass.Text).LicenseClassID;
            LDLApication.PaidFees = Convert.ToDecimal(lbApplicationFees.Text);
            


            if (LDLApication.Save())
            {
                _LoadData();
                _Mode = enMode.Update;
                this.Text = "Update Local Driving License Applications";
                lbHeader.Text = "Update Local Driving License Applications";
                ucPersonInfoWithFilter1.FilterEnabeled = false;
                MessageBox.Show("Data Saved Successfully.", "Infotmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void cmbLicenseClass_Validating(object sender, CancelEventArgs e)
        {
            if (clsLocalDrivingLicenseApplication.IsApplicationExsistOrComplated(ucPersonInfoWithFilter1.PersonID, clsLicenseClass.Find(cmbLicenseClass.Text).LicenseClassID))
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbLicenseClass, "Application already Exist or Complated");
                return;
            }
            errorProvider1.SetError(cmbLicenseClass, null);

        }

        private void frmAddUpdataLocalDrivingLicenseApplications_Activated(object sender, EventArgs e)
        {
            ucPersonInfoWithFilter1.FilterFocus();
        }
    }
}
