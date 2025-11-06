using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_License_Management.Licenses.LocalLicenses.Controls
{
    public partial class ucDriverLicenseWithFilter : UserControl
    {
        public ucDriverLicenseWithFilter()
        {
            
            InitializeComponent();
        }

        bool _FilterEnabeld = true;

        public bool FilterEnabeld
        {
            get { return _FilterEnabeld; }
            set {  _FilterEnabeld = value;
                gbFilters.Enabled = _FilterEnabeld;
            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            ucLocalDriver_sLicense1.LoadLicenseInfoByLicenseID(Convert.ToInt32(txtLicenseID.Text));
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e) {
        
                 e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

    }
}
