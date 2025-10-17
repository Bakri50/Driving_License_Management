using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_License_Management.Tests.TestTypes
{
    public partial class frmTakeTest : Form
    {
        int _TestAppointmentID;
        public frmTakeTest()
        {
            InitializeComponent();
        }
        public frmTakeTest(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            InitializeComponent();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {

        }
    }
}
