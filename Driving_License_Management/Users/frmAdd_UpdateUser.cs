﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace Driving_License_Management.Login
{
    public partial class frmAdd_UpdateUser : Form
    {
        enum enMode { AddNew = 0, Update = 1 }

        clsUser _User;
        enMode _Mode;
        public frmAdd_UpdateUser()
        {
            InitializeComponent();

            _User = new clsUser();
            _Mode = enMode.AddNew;


        }

        public frmAdd_UpdateUser(int ID)
        {
            InitializeComponent();
            _User = clsUser.FindUser(ID);
            _Mode = enMode.Update;

        }
        private void _ReastDefualtValues()
        {
            if (_Mode == enMode.AddNew) {
                _Mode = enMode.AddNew;
                lbHeader.Text = "Add New Person";
                this.Text = lbHeader.Text;
                ucPersonInfoWithFilter1.FilterFocus();
                
            }

            else if (_Mode == enMode.Update) {
                _Mode = enMode.Update;
                lbHeader.Text = "   Update User";
                this.Text = lbHeader.Text;
                ucPersonInfoWithFilter1.FilterFocus();
                ucPersonInfoWithFilter1.FilterEnabeled = false;
                _LoadData();
            }
            btnSave.Enabled = false;


        }


        private void _LoadData()
        {
            if (_User == null) { return; }
            ucPersonInfoWithFilter1.LoadPersonInfo(_User.Person.PersonID);
            lbUserID.Text = _User.UserID.ToString();
            txbUsername.Text = _User.UserName;
            txbPassword.Text = _User.Password;
            txbConfirmPassword.Text = _User.Password;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew) { 

            if (ucPersonInfoWithFilter1.PersonID == -1)
            {

                MessageBox.Show("You Must Choose or Add Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            if (clsUser.IsUserExistWithPersonID(ucPersonInfoWithFilter1.PersonID))
            {
                MessageBox.Show("The person you selected is already a user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            }
            
            if (tabControl1.SelectedIndex < tabControl1.TabPages.Count - 1) {

                tabControl1.SelectedIndex++;
                btnSave.Enabled = true;
            }
        }

        private void frmAdd_UpdateUser_Load(object sender, EventArgs e)
        {
            _ReastDefualtValues();

            // For Form Design
            this.Size = new System.Drawing.Size(880, 560);
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            //-----------------------

            

        
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex > 0)
            {

                tabControl1.SelectedIndex--;
                btnSave.Enabled = false;

            }
        }

        private void txbUsername_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txbUsername.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbUsername, "This field requaierd!");
                return;
            }

            if (txbUsername.Text.Contains(" "))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbUsername, "Username must not contain space!");
                return;
            }

            if (clsUser.FindUser(txbUsername.Text) != null && txbUsername.Text != _User.UserName)
            {
                e.Cancel = true;
                errorProvider1.SetError(txbUsername, "Username Already Exist!");
                return;
            }

            errorProvider1.SetError(txbUsername, null);
        }

        private void txbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbPassword, "This field requaierd!");
                return;
            }
            errorProvider1.SetError(txbPassword, null);


        }

        private void txbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txbPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbConfirmPassword, "This field requaierd!");
                return;
            }


            if (txbConfirmPassword.Text != txbPassword.Text) {

                e.Cancel = true;
                errorProvider1.SetError(txbConfirmPassword, "Not Matching!");
                return;
            }

            errorProvider1.SetError(txbConfirmPassword, null);


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                return;
            }

            _User.UserName = txbUsername.Text.ToString();
            _User.Password = txbPassword.Text;
            _User.IsActive = chkIsActive.Checked;
            _User.Person = clsPerson.FindPerson(ucPersonInfoWithFilter1.PersonID);

            if (_User.Save())
            {
                _Mode = enMode.Update;
                lbHeader.Text = "   Update User";
                lbUserID.Text = _User.UserID.ToString();

                MessageBox.Show("Data Saved Successfuly","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("An error, Occurred");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
