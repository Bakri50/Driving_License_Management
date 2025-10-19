using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_License_Management.Licenses.LocalLicenses.Controls
{
    public partial class ucLocalDriver_sLicense : UserControl
    {
        clsLicense _License;
        public ucLocalDriver_sLicense()
        {
            InitializeComponent();
        }

        public void LoadLicenseInfoByApplicationID(int ApplicationID)
        {
           
            _License = clsLicense.FindByApplicationID(ApplicationID);
            clsPerson _Person = clsPerson.FindPerson(clsDriver.FindByDriverID(_License.DriverID).PersonID);

            if (_License == null) {
                MessageBox.Show("License Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
            }
            lblClass.Text = clsLicenseClass.Find(_License.LicenseClassID).ClassName;
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblFullName.Text = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID).FullName;
            lblGendor.Text = (_Person.Gendor == 0)? "Male": "Female";
            lblIsActive.Text = (_License.IsActive == 1) ? "Yes" : "No";
            lblIsDetained.Text = (_License.IsActive == 1) ? "No" : "Yes";
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();

            switch (_License.IssueReason)
            {
                case 1:
                    lblIssueReason.Text = "FirstTime";
                    break;
                case 2:
                    lblIssueReason.Text = "Renew";
                    break;
                case 3:
                    lblIssueReason.Text = "Replacement for Damaged";
                    break;
                case 4:
                    lblIssueReason.Text = "Replacement for Lost";
                    break;
                default:
                    break;
            }
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblNotes.Text = _License.Notes.ToString();
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            pbPersonImage.ImageLocation = _Person.ImagePath;

        }
      
    }
}
