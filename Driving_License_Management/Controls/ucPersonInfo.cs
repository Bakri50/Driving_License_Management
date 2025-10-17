using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Driving_License_Management.Properties;

namespace Driving_License_Management
{
    public partial class ucPersonInfo : UserControl
    {

        private clsPerson _Person;
        private int _PersonID = -1;

        public clsPerson SelectedPerson{
            get { return _Person; }
        }

        public int PersonID
        {
            get { return _PersonID; }
        }


        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.FindPerson(PersonID);

            if (_Person == null)
            {
                _RestDefualtValues();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
                   
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.FindPerson(NationalNo);

            if (_Person == null)
            {
                _RestDefualtValues();
                MessageBox.Show("No Person with NtionalNo = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();

        }

        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;
            this.lblPersonID.Text = _Person.PersonID.ToString();
            this.lblFullName.Text = $"{_Person.FirstName} {_Person.SecondName} {_Person.ThirdName} {_Person.LastName}";
            this.lblNationalNo.Text = _Person.NationalNo;
            this.lblGendor.Text = (_Person.Gendor == 0) ? "Male" : "Famle";
            
            this.lblAddress.Text = _Person.Address;
            this.lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            this.lblPhone.Text = _Person.Phone;
            this.lblCountry.Text = _Person.CountryInfo.CountryName;
            this.lblEmail.Text = _Person.Email;
            this.linkLabel1.Visible = true;

            // For Photo
            if (_Person.ImagePath != "")
            {
                if (File.Exists(_Person.ImagePath))
                {
                    this.pBox.ImageLocation = _Person.ImagePath;
                }
                else {
                    if (_Person.Gendor == 0) {
                        pBox.ImageLocation = @"D:\Projects\Storge\Icons\Icons\Male 512.png";
                    }
                    else pBox.ImageLocation = @"D:\Projects\Storge\Icons\Icons\Male 512.png";

                    MessageBox.Show("Could not find this image: = " + _Person.ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                       ;
                }
            }
            //if (_Person.Gendor == 0) pBoxGendor.Image = Resources.Man_32;
            //else pBoxGendor.Image = Resources.Woman_32;

        }

         private void _RestDefualtValues()
        {
            _PersonID = -1;
            this.lblPersonID.Text = "[???]";
            this.lblFullName.Text = "[???]";
            this.lblNationalNo.Text = "[???]";
            this.lblGendor.Text = "[???]";
            this.pBoxGendor.Image = Resources.Man_32;
            this.lblAddress.Text = "[???]";
            this.lblDateOfBirth.Text = "[???]";
            this.lblPhone.Text = "[???]";
            this.lblCountry.Text = "[???]";
            this.lblEmail.Text = "[???]";
            this.pBox.Image = Resources.Male_512;
            this.linkLabel1.Visible = false;
        }
        public ucPersonInfo()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddNewPerson(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        private void ucPersonInfo_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson(_PersonID); 
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        private void gbPersonInfo_Enter(object sender, EventArgs e)
        {

        }

        //public string lbPersonID
        //{
        //    get { return this.lblPersonID.Text; }
        //    set {  this.lblPersonID.Text = value;}
        //}

        //public string lbFullName
        //{
        //    get { return this.lblFullName.Text; }
        //    set { this.lblFullName.Text = value; }
        //}

        //public string lbNationalNo
        //{
        //    get { return this.lblNationalNo.Text; }
        //    set { this.lblNationalNo.Text = value; }
        //}

        //public string lbGendor
        //{
        //    get { return this.lblGendor.Text; }
        //    set { this.lblGendor.Text = value; }
        //}

        //public string lbEmail
        //{
        //    get { return this.lblEmail.Text; }
        //    set { this.lblEmail.Text = value; }
        //}

        //public string lbAddress
        //{
        //    get { return this.lblAddress.Text; }
        //    set { this.lblAddress.Text = value; }
        //}

        //public string lbDateOfBirth
        //{
        //    get { return this.lblDateOfBirth.Text; }
        //    set { this.lblDateOfBirth.Text = value; }
        //}

        //public string lbPhone
        //{
        //    get { return this.lblPhone.Text; }
        //    set { this.lblPhone.Text = value; }
        //}

        //public string lbCountry
        //{
        //    get { return this.lblCountry.Text; }
        //    set { this.lblCountry.Text = value; }
        //}

        //public LinkLabel linkLabel
        //{
        //    get { return this.linkLabel1 as LinkLabel; }
        //    set { this.linkLabel1 = value; }
        //}

        //public PictureBox pictureBox
        //{
        //    get { return this.pBox as PictureBox; }
        //    set { this.pBox = value; }
        //}


        //public event EventHandler LinkLabelClicked;

        //private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    if(LinkLabelClicked != null)
        //    {
        //        LinkLabelClicked(this, EventArgs.Empty);
        //    }
        //}


    }
}
