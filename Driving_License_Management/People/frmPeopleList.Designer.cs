﻿namespace Driving_License_Management
{
    partial class frmPeopleList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendEmailToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.callPhoneToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.MP_lbRecords = new System.Windows.Forms.Label();
            this.MP_dgv1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.MP_cmpFilterBy = new System.Windows.Forms.ComboBox();
            this.MP_txbFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MP_btnAddNew = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MP_dgv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem1,
            this.addNewUserToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.sendEmailToolStripMenuItem1,
            this.callPhoneToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(207, 196);
            // 
            // showDetailsToolStripMenuItem1
            // 
//            this.showDetailsToolStripMenuItem1.Image = global::Driving_License_Management.Properties.Resources.PersonDetails_32;
            this.showDetailsToolStripMenuItem1.Name = "showDetailsToolStripMenuItem1";
            this.showDetailsToolStripMenuItem1.Size = new System.Drawing.Size(206, 32);
            this.showDetailsToolStripMenuItem1.Text = "Show Details";
            this.showDetailsToolStripMenuItem1.Click += new System.EventHandler(this.showDetailsToolStripMenuItem1_Click);
            // 
            // addNewUserToolStripMenuItem
            // 
//            this.addNewUserToolStripMenuItem.Image = global::Driving_License_Management.Properties.Resources.AddPerson_32;
            this.addNewUserToolStripMenuItem.Name = "addNewUserToolStripMenuItem";
            this.addNewUserToolStripMenuItem.Size = new System.Drawing.Size(206, 32);
            this.addNewUserToolStripMenuItem.Text = "Add New User";
            this.addNewUserToolStripMenuItem.Click += new System.EventHandler(this.addNewPersonToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
//            this.editToolStripMenuItem.Image = global::Driving_License_Management.Properties.Resources.edit_32;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(206, 32);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
//            this.deleteToolStripMenuItem.Image = global::Driving_License_Management.Properties.Resources.Delete_32;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(206, 32);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // sendEmailToolStripMenuItem1
            // 
//            this.sendEmailToolStripMenuItem1.Image = global::Driving_License_Management.Properties.Resources.send_email_32;
            this.sendEmailToolStripMenuItem1.Name = "sendEmailToolStripMenuItem1";
            this.sendEmailToolStripMenuItem1.Size = new System.Drawing.Size(206, 32);
            this.sendEmailToolStripMenuItem1.Text = "Send Email";
            // 
            // callPhoneToolStripMenuItem1
            // 
//            this.callPhoneToolStripMenuItem1.Image = global::Driving_License_Management.Properties.Resources.call_32;
            this.callPhoneToolStripMenuItem1.Name = "callPhoneToolStripMenuItem1";
            this.callPhoneToolStripMenuItem1.Size = new System.Drawing.Size(206, 32);
            this.callPhoneToolStripMenuItem1.Text = "Call Phone";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 663);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "#Records";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // MP_lbRecords
            // 
            this.MP_lbRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MP_lbRecords.AutoSize = true;
            this.MP_lbRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MP_lbRecords.Location = new System.Drawing.Point(98, 663);
            this.MP_lbRecords.Name = "MP_lbRecords";
            this.MP_lbRecords.Size = new System.Drawing.Size(19, 20);
            this.MP_lbRecords.TabIndex = 7;
            this.MP_lbRecords.Text = "0";
            this.MP_lbRecords.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // MP_dgv1
            // 
            this.MP_dgv1.AllowUserToAddRows = false;
            this.MP_dgv1.AllowUserToDeleteRows = false;
            this.MP_dgv1.AllowUserToOrderColumns = true;
            this.MP_dgv1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MP_dgv1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.MP_dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MP_dgv1.ContextMenuStrip = this.contextMenuStrip1;
            this.MP_dgv1.GridColor = System.Drawing.SystemColors.MenuText;
            this.MP_dgv1.Location = new System.Drawing.Point(-13, 303);
            this.MP_dgv1.Name = "MP_dgv1";
            this.MP_dgv1.ReadOnly = true;
            this.MP_dgv1.RowHeadersWidth = 62;
            this.MP_dgv1.RowTemplate.Height = 28;
            this.MP_dgv1.Size = new System.Drawing.Size(1137, 340);
            this.MP_dgv1.TabIndex = 1;
            this.MP_dgv1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MP_dgv1_CellContentClick);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(389, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(312, 46);
            this.label3.TabIndex = 9;
            this.label3.Text = "Manage People";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MP_cmpFilterBy
            // 
            this.MP_cmpFilterBy.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.MP_cmpFilterBy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MP_cmpFilterBy.CausesValidation = false;
            this.MP_cmpFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MP_cmpFilterBy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MP_cmpFilterBy.IntegralHeight = false;
            this.MP_cmpFilterBy.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No",
            "First Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Nationalty",
            "Gendor",
            "Phone",
            "Email"});
            this.MP_cmpFilterBy.Location = new System.Drawing.Point(89, 258);
            this.MP_cmpFilterBy.Name = "MP_cmpFilterBy";
            this.MP_cmpFilterBy.Size = new System.Drawing.Size(206, 28);
            this.MP_cmpFilterBy.TabIndex = 2;
            this.MP_cmpFilterBy.SelectedIndexChanged += new System.EventHandler(this.MP_cmpFilterBy_SelectedIndexChanged);
            // 
            // MP_txbFilter
            // 
            this.MP_txbFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MP_txbFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MP_txbFilter.Location = new System.Drawing.Point(325, 258);
            this.MP_txbFilter.Name = "MP_txbFilter";
            this.MP_txbFilter.Size = new System.Drawing.Size(376, 26);
            this.MP_txbFilter.TabIndex = 3;
            this.MP_txbFilter.TextChanged += new System.EventHandler(this.MP_txbFilter_TextChanged);
            this.MP_txbFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MP_txbFilter_KeyPress_1);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Filter By";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.AllowDrop = true;
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoEllipsis = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
//            this.btnClose.Image = global::Driving_License_Management.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(952, 649);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(148, 49);
            this.btnClose.TabIndex = 87;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
//            this.pictureBox1.Image = global::Driving_License_Management.Properties.Resources.People_400;
            this.pictureBox1.Location = new System.Drawing.Point(321, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(458, 155);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // MP_btnAddNew
            // 
            this.MP_btnAddNew.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.MP_btnAddNew.AutoSize = true;
//            this.MP_btnAddNew.Image = global::Driving_License_Management.Properties.Resources.Add_Person_40;
            this.MP_btnAddNew.Location = new System.Drawing.Point(1017, 223);
            this.MP_btnAddNew.Name = "MP_btnAddNew";
            this.MP_btnAddNew.Size = new System.Drawing.Size(83, 63);
            this.MP_btnAddNew.TabIndex = 5;
            this.MP_btnAddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MP_btnAddNew.UseVisualStyleBackColor = true;
            this.MP_btnAddNew.Click += new System.EventHandler(this.MP_btnAddNew_Click);
            // 
            // frmPeopleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1124, 724);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.MP_lbRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MP_btnAddNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MP_txbFilter);
            this.Controls.Add(this.MP_cmpFilterBy);
            this.Controls.Add(this.MP_dgv1);
            this.Name = "frmPeopleList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage People";
            this.Load += new System.EventHandler(this.frmPeopleList_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MP_dgv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label MP_lbRecords;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addNewUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendEmailToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem callPhoneToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.DataGridView MP_dgv1;
        private System.Windows.Forms.Button MP_btnAddNew;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.ComboBox MP_cmpFilterBy;
        private System.Windows.Forms.TextBox MP_txbFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
    }
}