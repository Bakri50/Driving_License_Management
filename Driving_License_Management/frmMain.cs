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
using Driving_License_Management.Applcations.Applcations_Types;
using Driving_License_Management.Applcations.LocalDrivingLicenseApplication;
using Driving_License_Management.GlobalClasses;
using Driving_License_Management.Tests.TestTypes;
using Driving_License_Management.Users;
using Driving_License_Management.Drivers;

namespace Driving_License_Management
{
    public partial class frmMain : Form
    {
        int UserID = -1;

        clsUser _User = clsGlobal.CurrentUser;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            this.AutoScaleMode = AutoScaleMode.Dpi;

            if (_User != null) {
            lbPersonName.Text = _User.Person.FirstName + " " + _User.Person.SecondName;
                if (_User.Person.ImagePath != null) {
                    pBPersonPhoto.ImageLocation = _User.Person.ImagePath;
                }
            
            }

        }
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeopleList form2 = new frmPeopleList();
            form2.ShowDialog();
        }

        private void uToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frm = new frmShowUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }
       
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsersLists frm = new frmUsersLists();
            frm.ShowDialog();
        }

        private void ChangePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(_User.UserID);
            frm.ShowDialog();
        }

        private void SignOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes == MessageBox.Show("Are you sure", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1))
            {
                this.Close();
            }
        }

        private void manageApplicationsTypsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplcationsTypes frm = new frmListApplcationsTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdataLocalDrivingLicenseApplications frm = new frmAddUpdataLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void manageApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplication frm = new frmListLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();

        }
    }
}
