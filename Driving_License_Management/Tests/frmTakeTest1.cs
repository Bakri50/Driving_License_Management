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

namespace Driving_License_Management.Tests
{
    public partial class frmTakeTest1 : Form
    {

        int _TestAppointmentID;
        clsTest _Test;

        public delegate bool DataBakcEventHandler(object sender, bool IsTakeTest);

        public event DataBakcEventHandler DataBack;
        public frmTakeTest1()
        {
            InitializeComponent();
        }
        public frmTakeTest1(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            InitializeComponent();
        }

        private void frmTakeTest1_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode) {

                if (this._TestAppointmentID > 0)
                {
                    ucScheduled1.LoadInfo(_TestAppointmentID);
                    _Test = new clsTest();
                    
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.Notes = txbNotes.Text;
            _Test.TestResult = Convert.ToByte((rbPass.Checked) ? 1 : 0);
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_Test.Save())
            {
                ucScheduled1.TestID = _Test.TestID;
                MessageBox.Show("Saved Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clsTestAppointment.Lock(_TestAppointmentID);
                rbFaild.Enabled = false;
                rbPass.Enabled = false;
                txbNotes.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Don't Saved Succesfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
