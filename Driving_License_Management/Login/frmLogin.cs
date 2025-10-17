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
using Driving_License_Management.GlobalClasses;

namespace Driving_License_Management.Login
{
    public partial class frmLogin : Form
    {

        private clsUser _User;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string Username = "";
            string Password = "";

            if (clsGlobal.GetStoredCredintial(ref Username, ref Password)) {

                txtUserName.Text = Username;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }
        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            _User = clsUser.FindUser(txtUserName.Text);


            if (_User == null) {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username is Not Fount");
                return;
            }
            errorProvider1.SetError(txtUserName, null);

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            _User = clsUser.FindUser(txtUserName.Text);
            if (_User == null)
            {
                return;
            }
            if (_User.Password != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password is invalid");

            }
            else errorProvider1.SetError(txtPassword, null);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) {
                MessageBox.Show("Invalid Data");

                return;
            }

            if ((!_User.IsActive))
            {

                txtUserName.Focus();
                MessageBox.Show("Your account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (chkRememberMe.Checked) { 
            clsGlobal.RememberMeUsernameAndPassword(txtUserName.Text, txtPassword.Text);
            
            }
            else
            {
                clsGlobal.RememberMeUsernameAndPassword("", "");

            }

            clsGlobal.CurrentUser = _User;
            this.Hide();
            frmMain Main = new frmMain();
            Main.ShowDialog();
            if (!chkRememberMe.Checked)
            {
                this.Close();
            }
            this.ShowDialog();
            


        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
