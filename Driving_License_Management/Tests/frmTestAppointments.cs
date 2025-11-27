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
    public partial class frmTestAppointments : Form
    {
        int _LDLApplicationID = -1;
        clsTestType.enTestType _TestType;
        clsLocalDrivingLicenseApplication LDLApplication;
        public frmTestAppointments(int LDLApplicationID, clsTestType.enTestType TestType)
        {
            _LDLApplicationID = LDLApplicationID;
            _TestType = TestType;
            InitializeComponent();
        }

        void _LoadData()
        {
            LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LDLApplicationID);

            if (LDLApplication == null)
            {
                MessageBox.Show("Application not exist!");
                btnAddNewAppointment.Enabled = false;
                takeTestToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = false;

            }

            ucLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(LDLApplication.LocalDrivingLicenseApplicationID);
            dgv.DataSource = clsTestAppointment.GetAllTestAppointmentsPerTestType(_LDLApplicationID, (int)_TestType);
            lblRecords.Text = dgv.Rows.Count.ToString();

        }
        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            this.Height = 650;
            _LoadData();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {



            if (LDLApplication.IsThereAnActiveTestApointments(_TestType))
            {

                MessageBox.Show("This Application Already Has An Active Appointment", "Not Allow", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            clsTest LastTest = LDLApplication.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {

                frmAddUpdateTestAppointment frm1 = new frmAddUpdateTestAppointment(_LDLApplicationID, _TestType);
                frm1.ShowDialog();
                frmTestAppointment_Load(null, null);
                return;

            }

            if (LastTest.TestResult == 1)
            {
                MessageBox.Show("This Person Already Pass This Test", "Not Allow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmAddUpdateTestAppointment frm2 = new frmAddUpdateTestAppointment(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestType);
            frm2.ShowDialog();
            frmTestAppointment_Load(null, null);

        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgv.CurrentRow.Cells[0].Value;

            frmAddUpdateTestAppointment frm1 = new frmAddUpdateTestAppointment(_LDLApplicationID, _TestType,TestAppointmentID);
            frm1.ShowDialog();
            _LoadData();
            
           

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;
            //frmT frm = new frmTakeTest(ID);
            //frm.ShowDialog();
            frmTakeTest1 frm = new frmTakeTest1(ID, _TestType);
            frm.ShowDialog();
            _LoadData();
        }

        private void cmsApplications_Opening(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgv.CurrentRow.Cells[0].Value;
            
            if(clsTestAppointment.Find(TestAppointmentID).IsLocked == true)
            {
                takeTestToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = false;
            }
            else
            {
                takeTestToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = true;
            }
        }

    }
}
