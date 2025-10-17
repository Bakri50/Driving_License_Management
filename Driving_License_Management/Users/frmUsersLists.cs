using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Driving_License_Management.Login;

namespace Driving_License_Management.Users
{
    public partial class frmUsersLists : Form
    {

        DataTable UsersData;
        public frmUsersLists()
        {
            InitializeComponent();
        }


        private void _RefreshData()
        {
            UsersData = clsUser.GetAllUsers();
            dgv.DataSource = UsersData;
            lbRecords.Text = dgv.Rows.Count.ToString();

        }

        private void frmUsersLists_Load(object sender, EventArgs e)
        {

            _RefreshData();
            cmpFilterBy.SelectedIndex = 0;
            cmpIsActive.SelectedIndex = 0;
            txbFilter.Visible = false;
            cmpIsActive.Visible = false;
            cmpIsActive.SelectedIndex = 0;

            if (dgv.Rows.Count > 0) {

                dgv.Columns[0].HeaderText = "User ID";
                dgv.Columns[0].Width = 70;

                dgv.Columns[1].HeaderText = "Person ID";
                dgv.Columns[1].Width = 70;


                dgv.Columns[2].HeaderText = "Full Name";
                dgv.Columns[2].Width = 180;

                dgv.Columns[3].HeaderText = "User Name";
                dgv.Columns[3].Width = 100;


                dgv.Columns[4].HeaderText = "Is Active";
                dgv.Columns[4].Width = 70;

            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAdd_UpdateUser frm = new frmAdd_UpdateUser();
            frm.ShowDialog();
            _RefreshData();

        }

        private void showDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frm = new frmShowUserInfo((int)dgv.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdd_UpdateUser frm = new frmAdd_UpdateUser();
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgv.CurrentRow.Cells[0].Value;

            frmAdd_UpdateUser frm = new frmAdd_UpdateUser(UserID);
            frm.ShowDialog();
            _RefreshData();
          
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgv.CurrentRow.Cells[0].Value;
            frmChangePassword frm = new frmChangePassword(UserID);
            frm.ShowDialog();
        }


        private void cmpFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmpFilterBy.Text == "None")
            {
                txbFilter.Visible = false;
                cmpIsActive.Visible = false;
            }

            else if(cmpFilterBy.Text == "Is Active") {
                cmpIsActive.Visible = true;
                txbFilter.Visible = false;
            }
            else
            {
                cmpIsActive.Visible = false;
                txbFilter.Visible = true;
            }
        }

        private void txbFilter_TextChanged(object sender, EventArgs e)
        {
            string CurrentFilter = "";
            switch (cmpFilterBy.Text)
            {
                case "None":
                    CurrentFilter = "None";
                    break;
                case "User ID":
                    CurrentFilter = "UserID";
                    break;
                case "Person ID":
                    CurrentFilter = "PersonID";
                    break;
                case "Full Name":
                    CurrentFilter = "FullName";
                    break;
                case "User Name":
                    CurrentFilter = "UserName";
                    break;

                default:
                    break;
            }

            if(CurrentFilter == "" || CurrentFilter == "None")
            {
                UsersData.DefaultView.RowFilter = "";
                lbRecords.Text = dgv.Rows.Count.ToString();
                return;
            }

            if(CurrentFilter == "FullName" || CurrentFilter == "UserName")
            {
                if (string.IsNullOrWhiteSpace(txbFilter.Text))
                {
                    UsersData.DefaultView.RowFilter = "";
                }
                else
                {
                    try
                    {
                        UsersData.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", CurrentFilter, txbFilter.Text.Trim());

                    }
                    catch (Exception)
                    {
                        UsersData.DefaultView.RowFilter = "1=0";
                    }
                }
            }
            
            else
            {
                if (string.IsNullOrWhiteSpace(txbFilter.Text))
                {
                    UsersData.DefaultView.RowFilter = "";
                }
                else
                {
                    try
                    {
                        UsersData.DefaultView.RowFilter = string.Format("[{0}] = {1}", CurrentFilter, txbFilter.Text.Trim());
                    }
                    catch (Exception)
                    {
                        UsersData.DefaultView.RowFilter = "1=0";
                    }
                }
            }
            lbRecords.Text = dgv.Rows.Count.ToString();

        }

        private void cmpIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ColoumnName = "IsActive";
            string CurrentFilter = "";
            switch (cmpIsActive.Text)
            {
                case "All":
                    CurrentFilter = "All";
                    break;
                case "Yes":
                    CurrentFilter = "1";
                    break;
                case "No":
                    CurrentFilter = "0";
                    break;

                default:
                    break;
            }

            if (CurrentFilter == "" || CurrentFilter == "All")
            {
                UsersData.DefaultView.RowFilter = "";
                lbRecords.Text = dgv.Rows.Count.ToString();
                return;
            }
         
            else
            {
                UsersData.DefaultView.RowFilter = string.Format("{0} = {1}", ColoumnName, CurrentFilter);

            }
            lbRecords.Text = dgv.Rows.Count.ToString();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this user","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Warning))
            {
                if (clsUser.Delete((int)dgv.CurrentRow.Cells[0].Value)){
                    MessageBox.Show("Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshData();
                }
                else
                {
                    MessageBox.Show("You can't Delete This User", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cmpFilterBy.Text == "User ID" || cmpFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && ! char.IsControl(e.KeyChar);
            }
        }
    }
}
