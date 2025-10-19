using DataAccessLayer_DLVD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLayer.clsApplication;

namespace BusinessLayer
{
    public class clsDriver
    {
       public int DriverID;
       public int PersonID;
       public int CreatedByUserID ;
       public DateTime CreatedTime ;

        public clsDriver() { }

        public clsDriver(int driverID, int personID, int createdByUserID, DateTime createdTime)
        {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.CreatedByUserID = createdByUserID;
            this.CreatedTime = createdTime;
        }

        static public DataTable GetAllDrivers()
        {
            return clsDriverAccess.GetAllDrivers();
        }


        static public bool IsExistByPersonID(int PersonID)
        {
            return clsDriverAccess.IsDriverExistByPersonID(PersonID);
        }

        static public clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedTime = DateTime.Now;

            bool IsFound = clsDriverAccess.GetDriverByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedTime);

            if (IsFound)
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedTime);
            }
            else return null;
        }
        bool _AddNew()
        {
            DriverID = clsDriverAccess.AddNewDriver(PersonID,
                CreatedByUserID, CreatedTime);
            return (DriverID > 0);
        }

        static public clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;
            DateTime CreatedTime = DateTime.Now;

            bool IsFound = clsDriverAccess.GetDriverByDriverID(DriverID,ref PersonID,ref CreatedByUserID, ref CreatedTime);

            if (IsFound)
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedTime);
            }
            else return null;
        }

        public bool Save()
        {
            return _AddNew();
        }

    }
}
