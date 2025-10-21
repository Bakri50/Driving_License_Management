﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccessLayer_DLVD
{
    public class clsLicenseAccess
    {
        static public int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, DateTime ExpirationDate,
             string Notes, decimal PaidFees, byte IsActive, byte IssueReason,
             int CreatedByUserID)
        {

            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "INSERT INTO [dbo].[Licenses]  ([ApplicationID] , [DriverID]," +
                " [LicenseClass] , [IssueDate] , [ExpirationDate] , [Notes] , [PaidFees] , [IsActive], [IssueReason],  [CreatedByUserID])" +
                " VALUES (@ApplicationID, @DriverID , @LicenseClass ," +
                " @IssueDate , @ExpirationDate , @Notes , @PaidFees, @IsActive, @IssueReason , @CreatedByUserID) select SCOPE_IDENTITY()";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);
            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            cmd.Parameters.AddWithValue("@Notes", Notes);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.Parameters.AddWithValue("@IssueReason", IssueReason);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);



            try
            {

                connection.Open();
                Object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int id))
                {
                    LicenseID = id;
                }

            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }

            return LicenseID;
        }


        static public bool GetLicenseInfoByApplicationID(int ApplicationID,ref int LicenseID, ref int DriverID,ref  int LicenseClassID,ref  DateTime IssueDate, ref DateTime ExpirationDate,
             ref string Notes, ref decimal PaidFees,ref  byte IsActive, ref byte IssueReason,
             ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "Select *From Licenses Where ApplicationID = @ApplicationID";


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);


            try
            {

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    ;
                    IsActive = Convert.ToByte(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = string.Empty;
                    }
                    else Notes = reader["Notes"].ToString();


                }

                reader.Close();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {

                connection.Close();
            }

            return IsFound;
        }

        static public bool GetLicenseInfoByLicenseID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate, ref DateTime ExpirationDate,
             ref string Notes, ref decimal PaidFees, ref byte IsActive, ref byte IssueReason,
             ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "Select *From Licenses Where LicenseID = @LicenseID";


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    ;
                    IsActive = Convert.ToByte(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = string.Empty;
                    }
                    else Notes = reader["Notes"].ToString();


                }

                reader.Close();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {

                connection.Close();
            }

            return IsFound;
        }

        static public bool IsLicenseExistWithApplicationID(int ApplicationID)
        {


            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = "SELECT Found = 1 FROM Licenses where ApplicationID = @ApplicationID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;



        }

        static public DataTable GetAllLicensesWithDriverID(int DriverID)
        {


            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.connectionString);
            string query = @"select LicenseID as 'Licenses ID' ,ApplicationID as 'App ID', LicenseClass As 'Class Name',IssueDate As 'Issue Date',
                                    ExpirationDate As 'Expiration Date', IsActive As 'Is Active'
                                    from Licenses 
                                    where DriverID = @DriverID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@DriverID", DriverID);

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


    }
}
