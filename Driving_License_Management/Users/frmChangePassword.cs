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

namespace Driving_License_Management.Users
{
    public partial class frmChangePassword : Form
    {
        clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            _User = clsUser.Find(UserID);
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            // Design Setting
            this.Size = new System.Drawing.Size(690, 510);
            //----------

            if (_User != null) {
                ucUserInformation1.LoadUserInfo(_User.UserID);
                txbCurrentPassword.Focus();
            }


        }

        private void txbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txbCurrentPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbCurrentPassword, "This field requaierd!");
                return;
            }

            if (txbCurrentPassword.Text != _User.Password)
            {
                e.Cancel = false;
                errorProvider1.SetError(txbCurrentPassword, "This Password Not Matched");
                return;
            }
            errorProvider1.SetError(txbCurrentPassword, null);

        }

        private void txbNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbNewPassword, "This field requaierd!");
                return;
            }
            errorProvider1.SetError(txbNewPassword, null);
        }

        private void txbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txbConfirmPassword.Text != txbNewPassword.Text) {
                e.Cancel = true;
                errorProvider1.SetError(txbConfirmPassword, "Not Matched");
                return;
            }
            if (string.IsNullOrWhiteSpace(txbConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbConfirmPassword, "This field requaierd!");
                return;
            }
            errorProvider1.SetError(txbConfirmPassword, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) {
                return;
            }
            if (_User == null) {

                MessageBox.Show("An error Occurred!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            _User.Password = txbNewPassword.Text;
            if (_User.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
