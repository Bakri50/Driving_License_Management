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

namespace Driving_License_Management.Licenses
{
    public partial class frmPersonLicensesHistory : Form
    {
        int _ApplicationID;
        public frmPersonLicensesHistory(int ApplicationID)
        {
            _ApplicationID = ApplicationID;
            InitializeComponent();
        }


        private void frmPersonLicensesHistory_Load(object sender, EventArgs e) { 
        
        clsApplication Application = clsApplication.FindBaseApplication(_ApplicationID);
        ucDriverLicenses1.LoadData(Application.ApplicantPersonID);
        ucPersonInfoWithFilter1.LoadPersonInfo(Application.ApplicantPersonID);
        ucPersonInfoWithFilter1.FilterEnabeled = false;
        }
        
    }
}
