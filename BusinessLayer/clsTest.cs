using DataAccessLayer_DLVD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsTest
    {
        public enum enTestType{ Vision = 1,Written = 2,Street = 3}
        enum enMode { AddNew = 0, Update = 1 }

       public  int TestID;
       public int TestAppointmentID;
       public byte TestResult;
       public string Notes;
       public int CreatedByUserID;
       enMode _Mode;

        public clsTest() { 
        _Mode = enMode.AddNew;
        }

       public static int NumberOfTestsPassed(int LocalDrivingApplicationID)
        {
             return clsTestAccess.NumberOfTestsPassed(LocalDrivingApplicationID);
        }

        bool _AddNew()
        {
            TestID = clsTestAccess.TakeTest(TestAppointmentID,
                TestResult, Notes, CreatedByUserID);
            return (TestID > 0);
        }
        public bool Save()
        {

            switch (_Mode)
            {
                case enMode.AddNew:
                    _Mode = enMode.Update;
                    return _AddNew();
                case enMode.Update:
                    return false;
                default:
                    return false;

            }

        }

        public static bool DoseApplicationPassTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAccess.DoseApplicationPassTest(LocalDrivingLicenseApplicationID, TestTypeID);
        }


    }
}

