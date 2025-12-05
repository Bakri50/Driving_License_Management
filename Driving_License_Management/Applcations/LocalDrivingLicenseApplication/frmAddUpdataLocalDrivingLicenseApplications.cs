using System;
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
        clsLocalDrivingLicenseApplication LDLApplication;
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
                LDLApplication = new clsLocalDrivingLicenseApplication();
                lbApplicationDate.Text = DateTime.Now.ToShortDateString();
                lbApplicationFees.Text = ((int)(clsApplicationType.Find((int)clsApplication.enApplicationType.NewLocalDrivingLicenseService)).Fees).ToString();
                lbCreatedBy.Text = clsGlobal.CurrentUser.UserName;
                cmbLicenseClass.SelectedIndex = 2;
                ucPersonInfoWithFilter1.FilterFocus();
                return;
            }
            if (_Mode == enMode.Update) {

                this.Text = "Update Local Driving License Applications";
                lbHeader.Text = "Update Local Driving License Applications";
                ucPersonInfoWithFilter1.FilterEnabeled = false;

                LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
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

        
        private void _LoadData()
        {
            if (LDLApplication == null) {
                MessageBox.Show("No Local drivig license application with ID = " + _LocalDrivingLicenseApplicationID);
                return;
            }

            lbDLApplicationID.Text = LDLApplication.LocalDrivingLicenseApplicationID.ToString();
            lbApplicationDate.Text = LDLApplication.ApplicationDate.ToString();
            lbApplicationFees.Text = LDLApplication.PaidFees.ToString();
            clsUser CreatedByUser = clsUser.Find(LDLApplication.CreatedByUserID);
            lbCreatedBy.Text = CreatedByUser.UserName;

            cmbLicenseClass.SelectedIndex = cmbLicenseClass.FindString(clsLicenseClass.Find(LDLApplication.LicenseClassID).ClassName);
            if(_Mode == enMode.Update)
            ucPersonInfoWithFilter1.LoadPersonInfo(LDLApplication.ApplicantPersonID);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) {

                MessageBox.Show("Application already Exist or Complated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; }


            LDLApplication.ApplicantPersonID = ucPersonInfoWithFilter1.PersonID;
            LDLApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.NewLocalDrivingLicenseService;
            LDLApplication.ApplicationDate = DateTime.Now;
            LDLApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            LDLApplication.ApplicationStatus = Convert.ToByte(clsApplication.enStatus.New);
            LDLApplication.LastStatusDate = DateTime.Now;
            LDLApplication.LicenseClassID = clsLicenseClass.Find(cmbLicenseClass.Text).LicenseClassID;
            LDLApplication.PaidFees = Convert.ToDecimal(lbApplicationFees.Text);
            


            if (LDLApplication.Save())
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
