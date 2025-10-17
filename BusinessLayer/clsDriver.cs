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
        int DriverID;
        int PersonID;
        int CreatedByUserID ;
        DateTime CreatedTime ;

        public clsDriver() { }

        static public DataTable GetAllDrivers()
        {
            return clsDriverAccess.GetAllDrivers();
        }

        bool _AddNew()
        {
            DriverID = clsDriverAccess.AddNewDriver(PersonID,
                CreatedByUserID, CreatedTime);
            return (DriverID > 0);
        }

        public bool Save()
        {
            return _AddNew();
        }

    }
}
