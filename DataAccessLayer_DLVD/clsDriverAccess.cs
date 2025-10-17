﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer_DLVD
{
    public class clsDriverAccess
    {


        static public DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"select Drivers.DriverID, People.PersonID,People.NationalNo, People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'')  + ' ' +
                            + People.LastName As FullName , Licenses.IsActive As ActiveLicenses
                            From Drivers inner join People 
                            on Drivers.PersonID = People.PersonID inner join Licenses on Licenses.DriverID = Drivers.DriverID";

            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                return dt;
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        static public int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {

            int DriverID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "INSERT INTO [dbo].[Drivers] ([PersonID], [CreatedByUserID], [CreatedDate])" +
                " VALUES(@PersonID, @CreatedByUserID , @CreatedDate )select SCOPE_IDENTITY()";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);


            try
            {

                connection.Open();
                Object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    DriverID = id;
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return DriverID;
        }

    }
}
