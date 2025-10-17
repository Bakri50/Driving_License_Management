using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Driving_License_Management.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Runtime.ConstrainedExecution;
using Driving_License_Management.GlobalClasses;


namespace Driving_License_Management
{
    public partial class frmAddNewPerson : Form
    {
        enum _enMode { AddNew = 0, Update = 1 }
        int PersonID = -1;
        _enMode Mode;
        clsPerson _Person;

        public delegate void DataBakEventHandler(object sender, int PersonID);

        public event DataBakEventHandler DataBack;
        public frmAddNewPerson()
        {
            InitializeComponent();
            Mode = _enMode.AddNew;
        }
        public frmAddNewPerson(int ID)
        {
            InitializeComponent();

            PersonID = ID;
            Mode = _enMode.Update;
           
        }

        private void _FillcmbCountrey()
        {
            DataTable dt = clsCountry.GetAllCountries();

            foreach (DataRow item in dt.Rows)
            {
                cmbCountries.Items.Add(item["CountryName"]);
            }
        }


        private void _ResetDefultValues()
        {
            _FillcmbCountrey();
            BirthDateSetting();

            if (Mode == _enMode.AddNew)
            {
                cmbCountries.SelectedIndex = cmbCountries.FindString("Sudan");
                lblTitle.Text = "Add New Person";
            }

            _Person = new clsPerson();

            rbMale.Checked = true;
            pBoxPersonImage.ImageLocation = @"D:\Projects\Storge\Icons\Icons\Male 512.png";
            txbFirstN.Text = "";
            txbSecondN.Text = "";
            txbThirdN.Text = "";
            txbLastN.Text = "";
            txbNationalNo.Text = "";
            txbEmail.Text = "";
            txbPhone.Text = "";
            txbAddress.Text = ""; 



            //else
            //{
            //    clsPerson person = clsPerson.FindPerson(PersonID);
               


            //    if (person != null)
            //    {
            //        cmbCountries.SelectedIndex = person.NationalityCountryID - 1;
            //        pB.ImageLocation = person.ImagePath;
            //        lblTitle.Text = " Update Person";

            //        lblPersonID.Text = PersonID.ToString();
            //        txbFirstN.Text = person.FirstName;
            //        txbSecondN.Text = person.SecondName;
            //        txbThirdN.Text = person.ThirdName;
            //        txbLastN.Text = person.LastName;
            //        txbNationalNo.Text = person.NationalNo;
            //        if (person.Gendor == 0)
            //        {
            //            rbMale.Checked = true;
            //        }
            //        else rbFamale.Checked = true;

            //        txbEmail.Text = person.Email;
            //        txbAddress.Text = person.Address;
            //        dtpDateOfBirth.Value = person.DateOfBirth;
            //        txbPhone.Text = person.Phone;
            //        cmbCountries.SelectedValue = person.NationalityCountryID;

            //    }

            //}

            lblRemove.Visible = (pBoxPersonImage.ImageLocation != null);

            
        }
        private void _LoadData()
        {
            

                _Person = clsPerson.FindPerson(PersonID);



                if (_Person != null)
                {
                    
                    lblTitle.Text = " Update Person";

                    lblPersonID.Text = PersonID.ToString();
                    txbFirstN.Text = _Person.FirstName;
                    txbSecondN.Text = _Person.SecondName;
                    txbThirdN.Text = _Person.ThirdName;
                    txbLastN.Text = _Person.LastName;
                    txbNationalNo.Text = _Person.NationalNo;


                    if (_Person.Gendor == 0)
                    {
                        rbMale.Checked = true;
                    }
                    else rbFamale.Checked = true;

                    txbEmail.Text = _Person.Email;
                    txbAddress.Text = _Person.Address;
                    dtpDateOfBirth.Value = _Person.DateOfBirth;
                    txbPhone.Text = _Person.Phone;
                    cmbCountries.SelectedValue = _Person.NationalityCountryID;

                    cmbCountries.SelectedIndex = cmbCountries.FindString(_Person.CountryInfo.CountryName);

                if(_Person.ImagePath != null)  pBoxPersonImage.ImageLocation = _Person.ImagePath;

                lblRemove.Visible = (_Person.ImagePath != null);

            }




        }
        private void frmAddNewPerson_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                _ResetDefultValues();
                if (Mode == _enMode.Update) _LoadData();
            }
        }


        //for maxmium and minimum birth Date
        private void BirthDateSetting()
        {
            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Today.AddYears(-100);


        }

        //---------------------------


        //for set or remove Image
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Title";
            openFileDialog1.DefaultExt = "jpg";

            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                pBoxPersonImage.ImageLocation = openFileDialog1.FileName;
            }
            lblRemove.Visible = true;


        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pBoxPersonImage.ImageLocation = null;
            pBoxPersonImage.Image = null;
            lblRemove.Visible =false;   
        }

        //---------------------------
     


        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pBoxPersonImage.ImageLocation == null)
            {
                pBoxPersonImage.ImageLocation = @"D:\Projects\Storge\Icons\Icons\Male 512.png";
            }
        }

        private void rbFamale_CheckedChanged(object sender, EventArgs e)
        {
            if (pBoxPersonImage.ImageLocation == null ) {
                pBoxPersonImage.ImageLocation = @"D:\Projects\Storge\Icons\Icons\Famle 512.png";
            }
        }



        //For Inputs validate


        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox Temp = ((System.Windows.Forms.TextBox)sender);

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
            // TextBox Temp = ((TextBox)sender);
        }

        private void txbNationalNoValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbNationalNo, "This field is required");
            }


            else if (txbNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsExist(txbNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbNationalNo, "This National Number Already Exist");
            }
            else
            {
                errorProvider1.SetError(txbNationalNo, null);
            }
        }

        private void txbEmailValidating(object obj, CancelEventArgs e)
        {
            if (txbEmail.Text.Trim() == "")
                return;
            if (!clsValidation.IsValidEmail(txbEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txbEmail, null);
            };
        }



        private bool _IsValidEmail(string str)
        {
            if (str.EndsWith(".com") && str.Contains("@") && !str.StartsWith("@"))
            {
                return true;
            }
            return false;
        }

        //______________________________


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pBoxPersonImage.ImageLocation) {

                if (_Person.ImagePath != null) {

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {

                    }
                }
            
            }

            if (pBoxPersonImage.ImageLocation != null) { 
            
            
                string sourceFile = pBoxPersonImage.ImageLocation.ToString();

                if (clsUtil.CopeyFileToProjectFolder(ref sourceFile))
                {
                    pBoxPersonImage.ImageLocation = sourceFile;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
           
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Invalid Data", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandlePersonImage()) return; 


            _Person.NationalNo = txbNationalNo.Text.Trim();
            _Person.FirstName = txbFirstN.Text.Trim();
            _Person.SecondName = txbSecondN.Text.Trim();
            _Person.ThirdName = txbThirdN.Text.Trim();
            _Person.LastName = txbLastN.Text.Trim();
            _Person.Address = txbAddress.Text.Trim();
            _Person.Email = txbEmail.Text.Trim();
            _Person.Phone = txbPhone.Text.Trim();
            _Person.Gendor = (rbMale.Checked) ? Convert.ToByte(0) : Convert.ToByte(1);
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = cmbCountries.SelectedIndex + 1;
            _Person.ImagePath = pBoxPersonImage.ImageLocation; //pB = pictureBox

            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mode = _enMode.Update;
                lblTitle.Text = " Update Person";
                PersonID = _Person.PersonID;
                lblPersonID.Text = PersonID.ToString();

                DataBack?.Invoke(this, PersonID);
            }
            else
            {
                MessageBox.Show("Data Saved Faild.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //    else
            //{
            //    clsPerson newPerson = new clsPerson(PersonID, txbNationalNo.Text, txbFirstN.Text, txbSecondN.Text, txbThirdN.Text
            //        , txbLastN.Text, txbAddress.Text, txbPhone.Text, txbEmail.Text, pBoxPersonImage.ImageLocation, (rbMale.Checked) ? Convert.ToByte(0) : Convert.ToByte(1),
            //        dtpDateOfBirth.Value, cmbCountries.SelectedIndex + 1, PrevImagePath);

            //    newPerson.PersonID = PersonID;
            //    newPerson.NationalNo = txbNationalNo.Text.Trim();
            //    newPerson.FirstName = txbFirstN.Text.Trim();
            //    newPerson.SecondName = txbSecondN.Text.Trim();
            //    newPerson.ThirdName = txbThirdN.Text.Trim();
            //    newPerson.LastName = txbLastN.Text.Trim();
            //    newPerson.Address = txbAddress.Text.Trim();
            //    newPerson.Email = txbEmail.Text.Trim();
            //    newPerson.Phone = txbPhone.Text.Trim();
            //    newPerson.Gendor = (rbMale.Checked) ? Convert.ToByte(0) : Convert.ToByte(1);
            //    newPerson.DateOfBirth = dtpDateOfBirth.Value;
            //    newPerson.NationalityCountryID = clsCountry.Find(cmbCountries.Text).CountryID;
            //    if (pBoxPersonImage.ImageLocation != null)
            //        newPerson.ImagePath = pBoxPersonImage.ImageLocation; //pB = pictureBox
            //    else newPerson.ImagePath = "";

            //    if (newPerson.Save())
            //    {
            //        MessageBox.Show("Data Saved Successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Data Saved Faild.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

     
        private void txbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) { e.Handled = true; }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
