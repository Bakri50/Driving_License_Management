using DataAccessLayer_DLVD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLicense
    {
        public int LicenseID;
        public int ApplicationID;
       public  int DriverID             ;
       public int LicenseClassID       ;
       public DateTime IssueDate      ;
       public DateTime ExpirationDate  ;
       public string Notes             ;
       public decimal PaidFees         ;
       public byte IsActive            ;
       public byte IssueReason        ;
        public int CreatedByUserID;

        public clsLicense() { }

        public clsLicense(int licenseID, int applicationID, int driverID, int licenseClassID, DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees, byte isActive, byte issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            ApplicationID = applicationID;
            DriverID = driverID;
            LicenseClassID = licenseClassID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
        }

        bool _AddNew()
        {
            LicenseID = clsLicenseAccess.AddNewLicense(ApplicationID, DriverID,
                LicenseClassID, IssueDate, ExpirationDate, Notes,PaidFees,IsActive,IssueReason,CreatedByUserID);
            return (LicenseID > 0);
        }

        static public bool IsLicenseExistWithApplicationID(int ApplicationID)
        {
            return clsLicenseAccess.IsLicenseExistWithApplicationID(ApplicationID);
        }
        static public clsLicense FindByApplicationID(int ApplicationID)
        {

            int LicenseID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees =0;
            byte IsActive = 0;
            byte IssueReason = 0;
            int CreatedByUserID = 0;

            bool IsFound = clsLicenseAccess.GetLicenseInfoByApplicationID(ApplicationID, ref LicenseID, ref DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate,
             ref Notes, ref PaidFees, ref IsActive, ref IssueReason,
             ref CreatedByUserID);

            if (IsFound)
            {
                return new clsLicense(LicenseID,ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate,  Notes, PaidFees, IsActive,IssueReason,CreatedByUserID);
            }
            return null;
        }
        static public clsLicense FindByLicenseID(int LicenseID)
        {

            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClassID = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            byte IsActive = 0;
            byte IssueReason = 0;
            int CreatedByUserID = 0;

            bool IsFound = clsLicenseAccess.GetLicenseInfoByApplicationID(LicenseID, ref  ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate, ref ExpirationDate,
             ref Notes, ref PaidFees, ref IsActive, ref IssueReason,
             ref CreatedByUserID);

            if (IsFound)
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            return null;
        }
        static public DataTable GetAllLicensesWithDriverID(int DriverID)
        {
            return clsLicenseAccess.GetAllLicensesWithDriverID(DriverID);
        }
        public bool Save()
        {
            return _AddNew();
        }

    }
}
