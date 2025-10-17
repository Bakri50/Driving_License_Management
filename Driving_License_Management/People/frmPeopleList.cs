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

namespace Driving_License_Management
{
    public partial class frmPeopleList : Form
    {
        public frmPeopleList()
        {
            InitializeComponent();
        }
        DataTable FilteredData;
        private void _RefreshPepoleData()
        {
            DataTable AllData = clsPerson.GetAllPeople();

            FilteredData = AllData.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "Gendor", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");
            MP_dgv1.DataSource = FilteredData;

            MP_lbRecords.Text = MP_dgv1.RowCount.ToString();

        }
        private void frmPeopleList_Load(object sender, EventArgs e)
        {
            _RefreshPepoleData();
            MP_cmpFilterBy.SelectedIndex = 0;
            MP_txbFilter.Visible = false;

            if (MP_dgv1.Rows.Count > 0) {

                MP_dgv1.Columns[0].HeaderText = "Person ID";
                MP_dgv1.Columns[0].Width = 80;

                MP_dgv1.Columns[1].HeaderText = "National No.";
                MP_dgv1.Columns[1].Width = 120;


                MP_dgv1.Columns[2].HeaderText = "First Name";
                MP_dgv1.Columns[2].Width = 120;

                MP_dgv1.Columns[3].HeaderText = "Second Name";
                MP_dgv1.Columns[3].Width = 140;


                MP_dgv1.Columns[4].HeaderText = "Third Name";
                MP_dgv1.Columns[4].Width = 120;

                MP_dgv1.Columns[5].HeaderText = "Last Name";
                MP_dgv1.Columns[5].Width = 120;

                MP_dgv1.Columns[6].HeaderText = "Gendor";
                MP_dgv1.Columns[6].Width = 50;

                MP_dgv1.Columns[7].HeaderText = "Date Of Birth";
                MP_dgv1.Columns[7].Width = 140;

                MP_dgv1.Columns[8].HeaderText = "Nationality";
                MP_dgv1.Columns[8].Width = 120;


                MP_dgv1.Columns[9].HeaderText = "Phone";
                MP_dgv1.Columns[9].Width = 120;


                MP_dgv1.Columns[10].HeaderText = "Email";
                MP_dgv1.Columns[10].Width = 170;
             

            }

        }

        private void MP_cmpFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MP_cmpFilterBy.SelectedIndex == 0) { 
            MP_txbFilter.Visible = false;
            }
            else
            {
                MP_txbFilter.Visible = true;
            }
        }

        private void MP_txbFilter_TextChanged(object sender, EventArgs e)
        {
            /*
             *Filter from data base
            if (MP_txbFilter.Text == string.Empty)
            {
                _RefreshPepoleData();
            }
            else
            {
                switch (MP_cmpFilterBy.SelectedIndex)
                {
                    case 1:// Person ID
                        MP_dgv1.DataSource = clsPerson.FilterByID((Convert.ToInt32(MP_txbFilter.Text)));
                        break;

                    case 2:// National No
                        MP_dgv1.DataSource = clsPerson.FilterBy("NationalNo", MP_txbFilter.Text.ToString());
                        break;

                    case 3:// First Name
                        MP_dgv1.DataSource = clsPerson.FilterBy("FirstName", MP_txbFilter.Text.ToString());
                        break;

                    case 4:// SecondName
                        MP_dgv1.DataSource = clsPerson.FilterBy("SecondName", MP_txbFilter.Text.ToString());
                        break;

                    case 5:// Third Name
                        MP_dgv1.DataSource = clsPerson.FilterBy("ThirdName", MP_txbFilter.Text.ToString());
                        break;

                    case 6:// Last Name
                        MP_dgv1.DataSource = clsPerson.FilterBy("LastName", MP_txbFilter.Text.ToString());
                        break;
                    case 7:// Nationalty 
                        MP_dgv1.DataSource = clsPerson.FilterBy("CountryName", MP_txbFilter.Text.ToString());
                        break;
                    case 8:// Gendor
                        MP_dgv1.DataSource = clsPerson.FilterBy("Gendor", MP_txbFilter.Text.ToString());
                        break;
                    case 9:// Phone
                        MP_dgv1.DataSource = clsPerson.FilterBy("Phone", MP_txbFilter.Text.ToString());
                        break;
                    case 10:// Email
                        MP_dgv1.DataSource = clsPerson.FilterBy("Email", MP_txbFilter.Text.ToString());
                        break;

                    default:
                        break;
                }

            }
            */

            string FilterColoumn = "";

                switch (MP_cmpFilterBy.SelectedIndex)
                {
                    case 0:// Person ID
                        FilterColoumn = "None";
                        break;
                    case 1:// Person ID
                        FilterColoumn = "PersonID";
                        break;

                    case 2:// National No
                        FilterColoumn = "NationalNo";
                        break;

                    case 3:// First Name
                        FilterColoumn = "FirstName";
                        break;

                    case 4:// SecondName
                        FilterColoumn = "SecondName";
                        break;

                    case 5:// Third Name
                        FilterColoumn = "ThirdName";
                        break;

                    case 6:// Last Name
                        FilterColoumn = "LastName";
                        break;
                    case 7:// Nationalty 
                        FilterColoumn = "Nationalty";
                        break;
                    case 8:// Gendor
                        FilterColoumn = "Gendor";
                        break;
                    case 9:// Phone
                        FilterColoumn = "Phone";
                        break;
                    case 10:// Email
                        FilterColoumn = "Email";
                        break;

                    default:
                        break;
                }

                if(MP_txbFilter.Text.Trim() == "" || FilterColoumn == "None")
                {
                    FilteredData.DefaultView.RowFilter = "";
                    MP_lbRecords.Text =  MP_dgv1.Rows.Count.ToString();
                    return;
                }

               else if (FilterColoumn == "PersonID")
                {

                    FilteredData.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColoumn, MP_txbFilter.Text.Trim());

                }

                else {
                    FilteredData.DefaultView.RowFilter = string.Format("[{0}] LIKE  '{1}%'", FilterColoumn, MP_txbFilter.Text.Trim());

                }

                MP_lbRecords.Text = MP_dgv1.RowCount.ToString();

            

        }

        private void MP_btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson();
            frm.ShowDialog();
            _RefreshPepoleData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)MP_dgv1.CurrentRow.Cells[0].Value;
            frmAddNewPerson frm = new frmAddNewPerson(PersonID);
            frm.ShowDialog();
            _RefreshPepoleData();
        }

        private void showDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmPersonDetails((int)MP_dgv1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPepoleData();

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson(-1);
            frm.ShowDialog();
            _RefreshPepoleData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)MP_dgv1.CurrentRow.Cells[0].Value;
            int CurrentIndex = (int)MP_dgv1.CurrentRow.Index;
            int CurrentCellIndex = (int)MP_dgv1.CurrentCell.ColumnIndex;
            if ((MessageBox.Show($"Are you sure you want delete this Person[{PersonID}]", "Conferm", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes))
            {

                if (clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show("Deleted Successfuly", "Delete",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    _RefreshPepoleData();
                    if(CurrentIndex -1 >= 0)
                    {
                        MP_dgv1.CurrentCell = MP_dgv1.Rows[CurrentIndex - 1].Cells[CurrentCellIndex];
                    }

                }
                else
                {
                    MessageBox.Show("Deleted Faild, You can not delete this Person Because it has Data likned to it", "Delete",MessageBoxButtons.OK,MessageBoxIcon.Error);

                } 
            }
        }

        private void MP_txbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MP_dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MP_txbFilter_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if(MP_cmpFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
