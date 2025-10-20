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
using Driving_License_Management.Tests;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Driving_License_Management.Licenses.LocalLicenses;
using Driving_License_Management.Licenses;

namespace Driving_License_Management.Applcations.LocalDrivingLicenseApplication
{
    public partial class frmListLocalDrivingLicenseApplication : Form
    {
        DataTable _dt;

        public frmListLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void _RfreshData()
        {
            _dt = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgv.DataSource = _dt;
            cmbFilterBy.SelectedIndex = 0;
            txbFilter.Visible = false;
        }
        private void frmListLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {

            _RfreshData();
            lbRecords.Text = dgv.Rows.Count.ToString();

            if (dgv.Rows.Count > 0)
            {

                dgv.Columns[0].HeaderText = "L.D.L.AppID";
                dgv.Columns[0].Width = 80;

                dgv.Columns[1].HeaderText = "Driving Class";
                dgv.Columns[1].Width = 200;

                dgv.Columns[2].HeaderText = "National No.";
                dgv .Columns[2].Width = 100;

                dgv.Columns[3].HeaderText = "Full Name";
                dgv.Columns[3].Width = 350;

                dgv.Columns[4].HeaderText = "Application Date";
                dgv.Columns[4].Width = 170;

                dgv.Columns[5].HeaderText = "Passed Tests";
                dgv.Columns[5].Width = 50;
            }
            cmbFilterBy.SelectedIndex = 0;

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdataLocalDrivingLicenseApplications frm = new frmAddUpdataLocalDrivingLicenseApplications();
            frm.ShowDialog();
            _RfreshData();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.SelectedIndex == 0) { 
               txbFilter.Visible = false;
            }
            else txbFilter.Visible = true;

        }

        private void txbFilter_TextChanged(object sender, EventArgs e)
        {
         
            string CurrentFilter = "";
            switch (cmbFilterBy.Text)
            {
                    case "L.D.L.AppID":
                    CurrentFilter = "LocalDrivingLicenseApplicationID";
                    break;
                    case "National No.":
                    CurrentFilter = "NationalNo";
                    break;
                    case "Full Name":
                    CurrentFilter = "FullName";
                    break;
                    case "Status":
                    CurrentFilter = "Status";
                    break;

                    default:
                    CurrentFilter = "None";
                    break;
            }

            if(CurrentFilter == "None" || CurrentFilter == "" || string.IsNullOrWhiteSpace(txbFilter.Text))
            {
                _dt.DefaultView.RowFilter = "";
                lbRecords.Text = dgv.Rows.Count.ToString();
                return;
            }

            if(CurrentFilter == "LocalDrivingLicenseApplicationID")
            {
                try
                {
                    _dt.DefaultView.RowFilter = string.Format("[{0}] = {1}", CurrentFilter, txbFilter.Text.Trim());
                }
                catch (Exception)
                {
                    _dt.DefaultView.RowFilter = "1=0";
                }
            }
            else
            {
                try
                {
                    _dt.DefaultView.RowFilter = string.Format("[{0}]  LIKE '{1}%'", CurrentFilter, txbFilter.Text.Trim());

                }
                catch (Exception)
                {
                    _dt.DefaultView.RowFilter = "1=0";
                }

            }
            lbRecords.Text = dgv.Rows.Count.ToString();

        }

        private void canceledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgv.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LDLApplicationID);


            if (clsApplication.FindApplication(LDLApplication.ApplicationID).Cancel())
            {
                MessageBox.Show("Successfuly", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RfreshData();
                return;

            }

            MessageBox.Show("Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;
            frmAddUpdataLocalDrivingLicenseApplications frm = new frmAddUpdataLocalDrivingLicenseApplications(ID);
            frm.ShowDialog();
            _RfreshData();
        }

        private void showDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;
            frmShowLocaldrivingLicenseApplicationInfo frm = new frmShowLocaldrivingLicenseApplicationInfo(ID);
            frm.ShowDialog();
            _RfreshData();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(ID);

            if (LDLApplication.Delete()) {


                MessageBox.Show("Successfuly", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RfreshData();
                return;

            }

            MessageBox.Show("Faild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(ID);

            if (LDLApplication.ApplicationStatus == (int)clsLocalDrivingLicenseApplication.enStatus.Cancelled ||
               LDLApplication.ApplicationStatus == (int)clsLocalDrivingLicenseApplication.enStatus.Completed)
            {
                editToolStripMenuItem.Enabled = false;
                CancelToolStripMenuItem.Enabled = false;
                scheduleTestToolStripMenuItem1.Enabled = false;
                issueToolStripMenuItem1.Enabled = false;

                if (LDLApplication.ApplicationStatus == (int)clsLocalDrivingLicenseApplication.enStatus.Cancelled) 
                    return;
                deleteToolStripMenuItem.Enabled = false;
           
            }
            else
            {
                editToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                CancelToolStripMenuItem.Enabled = true;
                scheduleTestToolStripMenuItem1.Enabled = true;
            }


            if (_DoseApplicationPassAllTests(ID) 
                && LDLApplication.ApplicationStatus != (int)clsLocalDrivingLicenseApplication.enStatus.Cancelled
                && !clsLicense.IsLicenseExistWithApplicationID(LDLApplication.ApplicationID))
            {
                
                issueToolStripMenuItem1.Enabled = true;
                scheduleTestToolStripMenuItem1.Enabled = false;
            }
            else {
                if (!clsLicense.IsLicenseExistWithApplicationID(LDLApplication.ApplicationID){
                }
                scheduleTestToolStripMenuItem1.Enabled = true;
                issueToolStripMenuItem1.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;

            }

            if (clsLicense.IsLicenseExistWithApplicationID(LDLApplication.ApplicationID))
            {

                showLicenseToolStripMenuItem.Enabled = true;

            }
            else
            {

                showLicenseToolStripMenuItem.Enabled = false;
            }



        }

        private bool _DoseApplicationPassAllTests(int ID)
        {
            if(clsTest.DoseApplicationPassTest(ID,1) && clsTest.DoseApplicationPassTest(ID, 2) && clsTest.DoseApplicationPassTest(ID, 3)){
                return true;
            }
            return false;
        }
        private void scheduleTestToolStripMenuItem1_DropDownOpening(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;

            scheduleVisionTestToolStripMenuItem.Enabled = !clsTest.DoseApplicationPassTest(ID,(int)clsTest.enTestType.Vision);
            scheduleWrittenTestToolStripMenuItem1.Enabled = !clsTest.DoseApplicationPassTest(ID,(int)clsTest.enTestType.Written) 
                && clsTest.DoseApplicationPassTest(ID, (int)clsTest.enTestType.Vision);

            scheduleStreetTestToolStripMenuItem2.Enabled = !clsTest.DoseApplicationPassTest(ID, (int)clsTest.enTestType.Street)
                && clsTest.DoseApplicationPassTest(ID, (int)clsTest.enTestType.Written);
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;

            frmTestAppointment frm = new frmTestAppointment(ID,(int)clsTest.enTestType.Vision) ;
           
            frm.ShowDialog() ;
            _RfreshData();

        }
        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;

            frmTestAppointment frm = new frmTestAppointment(ID, (int)clsTest.enTestType.Written);

            frm.ShowDialog();
            _RfreshData();


        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;

            frmTestAppointment frm = new frmTestAppointment(ID, (int)clsTest.enTestType.Street);

            frm.ShowDialog();
            _RfreshData();


        }

        private void issueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;
            frmIssueDrivingLicenceFirstTime frm = new frmIssueDrivingLicenceFirstTime(ID);
            frm.ShowDialog();
            _RfreshData();

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(ID);

            frmDriverLicenseInfo frm = new frmDriverLicenseInfo(LDLApplication.ApplicationID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(ID);

            frmPersonLicensesHistory frm = new frmPersonLicensesHistory(LDLApplication.ApplicationID);
            frm.ShowDialog();
        }
    }
}
