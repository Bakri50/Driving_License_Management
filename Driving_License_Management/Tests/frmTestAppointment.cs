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
    public partial class frmTestAppointment : Form
    {
        int _LDLApplicationID = -1;
        int _TestTypeID;
        clsLocalDrivingLicenseApplication LDLApplication;
        public frmTestAppointment(int LDLApplicationID, int TestID)
        {
            _LDLApplicationID = LDLApplicationID;
            _TestTypeID = TestID;   
            InitializeComponent();
        }

        void _LoadData()
        {
            LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LDLApplicationID);
            if (LDLApplication == null)
            {
                MessageBox.Show("Application not exist!");
            }

            ucLocalDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(LDLApplication.LocalDrivingLicenseApplicationID);
            dgv.DataSource = clsTestAppointment.GetAllTestAppointmentwithLDLApplicationID(_LDLApplicationID,_TestTypeID);

        }
        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            this.Height = 650;
            _LoadData();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {

            if (clsTest.DoseApplicationPassTest(_LDLApplicationID, _TestTypeID))
            {

                MessageBox.Show("This Application Already pass The test", "Not Allow", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (clsTestAppointment.DoseApplicationHasActiveTestAppointment(_LDLApplicationID, _TestTypeID))
            {
                MessageBox.Show("This Application Already has active appointment", "Not Allow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
             }

            if(clsTestAppointment.DoseApplicationHasLockedTestAppointment(_LDLApplicationID, _TestTypeID))
            {
                frmAddUpdateTestAppointment frm1 = new frmAddUpdateTestAppointment(_LDLApplicationID, _TestTypeID, true);
                frm1.ShowDialog();
                _LoadData();
                return;
            }
            
            frmAddUpdateTestAppointment frm = new frmAddUpdateTestAppointment(_LDLApplicationID, _TestTypeID);
            frm.ShowDialog();
            _LoadData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;
            frmAddUpdateTestAppointment frm = new frmAddUpdateTestAppointment(ID);
            frm.ShowDialog();
            _LoadData();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;
            //frmT frm = new frmTakeTest(ID);
            //frm.ShowDialog();
            frmTakeTest1 frm = new frmTakeTest1(ID);
            frm.ShowDialog();
            _LoadData();
        }

        private void cmsApplications_Opening(object sender, EventArgs e)
        {
            int ID = (int)dgv.CurrentRow.Cells[0].Value;
            
            if(clsTestAppointment.Find(ID).IsLocked == 1)
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
