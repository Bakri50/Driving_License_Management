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
using Driving_License_Management.Licenses.LocalLicenses;

namespace Driving_License_Management.Licenses.Controls
{
    public partial class ucDriverLicenses : UserControl
    {
        int _ApplicationID;
        DataTable _dt;
        public ucDriverLicenses()
        {
            InitializeComponent();
        }

        public void LoadData(int PersonID)
        {
            
            clsDriver Driver = clsDriver.FindByPersonID(PersonID);
            if (Driver == null)
            {
                MessageBox.Show("No Driver with Person ID = " + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            _dt = clsLicense.GetAllLicensesWithDriverID(Driver.DriverID);
            dgvLocalLicensesHistory.DataSource = _dt;
            lblLocalLicensesRecords.Text = _dt.Rows.Count.ToString();
        }


        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e) {

            int ID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[1].Value;
            frmDriverLicenseInfo frm = new frmDriverLicenseInfo(ID);
            frm.ShowDialog();
            
        }
    }
}
