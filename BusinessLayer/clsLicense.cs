using DataAccessLayer_DLVD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsLicense
    {
        int LicenseID;
        int ApplicationID;
        int DriverID             ;
        int LicenseClassID       ;
        DateTime IssueDate      ;
        DateTime ExpirationDate  ;
        string Notes             ;
        decimal PaidFees         ;
        byte IsActive            ;
        byte IssueReason        ;
        int CreatedByUserID;

        public clsLicense() { }

        bool _AddNew()
        {
            LicenseID = clsLicenseAccess.AddNewLicense(ApplicationID, DriverID,
                LicenseClassID, IssueDate, ExpirationDate, Notes,PaidFees,IsActive,IssueReason,CreatedByUserID);
            return (LicenseID > 0);
        }


        public bool Save()
        {
            return _AddNew();
        }

    }
}
