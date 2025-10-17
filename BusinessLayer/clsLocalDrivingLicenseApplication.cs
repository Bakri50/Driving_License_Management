﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccessLayer_DLVD;

namespace BusinessLayer
{

    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        enum enMode { AddNew = 1, Update = 2 };

        public int LocalDrivingLicenseApplicationID { get; set; }
       // public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClass LicenseClass;

        enMode Mode = enMode.AddNew;
        public clsLocalDrivingLicenseApplication()
        {
            Mode = enMode.AddNew;
        }
         clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,int LicenseClassID,
             int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
           byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;

            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            PersonInfo = clsPerson.FindPerson(ApplicantPersonID);
            CreateByUserInfo = clsUser.FindUser(CreatedByUserID);
            ApplcationType = clsApplcationType.Find(ApplicationTypeID);
            clsLicenseClass licenseClass = clsLicenseClass.Find(LicenseClassID);

            Mode = enMode.Update;

        }
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationAccess.GetAllLocalDrivigLicenseApplications();
        }

        public static bool IsApplicationExsistOrComplated(int ApplicantPersonID, int LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationAccess.IsApplicationExsistOrComplated(ApplicantPersonID, LicenseClassID);
        }
        static public clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;
            int LisenseClass = -1;



            bool IsFound = clsLocalDrivingLicenseApplicationAccess.GetLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LisenseClass);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LisenseClass,
                    Application.ApplicationID, Application.ApplicantPersonID,Application.ApplicationDate,
                    Application.ApplicationTypeID, Application.ApplicationStatus,Application.LastStatusDate,
                    Application.PaidFees,Application.CreatedByUserID
                    );
            }
            return null;
        }

        static public clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            int LisenseClass = -1;



            bool IsFound = clsLocalDrivingLicenseApplicationAccess.GetLocalDrivingLicenseApplicationByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LisenseClass);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LisenseClass,
                    Application.ApplicationID, Application.ApplicantPersonID, Application.ApplicationDate,
                    Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, Application.CreatedByUserID
                    );
            }
            return null;
        }
        private bool _AddNew()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationAccess.AddNewLocalDrivingLicenseApplication(
                this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID > 0);
        }
        private bool _Update()
        {
            int RowAffected = clsLocalDrivingLicenseApplicationAccess.UpdateLocalDrivingLicenseApplication(
                this.LocalDrivingLicenseApplicationID,this.ApplicationID,this.LicenseClassID
                );

            return (RowAffected > 0);
        }
        public bool Delete()
        {
            int IsLocalDrivingLicenseApplicationDeleted = clsLocalDrivingLicenseApplicationAccess.DeleteLocalDrivingLicenseApplication(
                this.LocalDrivingLicenseApplicationID);
            if(IsLocalDrivingLicenseApplicationDeleted > 0)
            {
                return base.Delete();
            }
            return false;
        }
        public bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save()) { 
            return false;   
            }
            switch (Mode)
            {
                case enMode.AddNew:
                    Mode = enMode.Update;
                    return _AddNew();

                case enMode.Update:
                    return _Update();
                default:
                   return false;
            }

        }
    }
}
