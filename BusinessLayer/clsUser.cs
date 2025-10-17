﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccessLayer_DLVD;

namespace BusinessLayer
{
    public class clsUser
    {
        enum enMode { AddNew =0, Update = 1 }
        public int UserID {  get; set; }
        public string UserName {  get; set; }
        public string Password {  get; set; }
        public bool IsActive {  get; set; }
        public clsPerson Person;
        enMode _Mode;
        public clsUser() {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            this._Mode = enMode.AddNew;
        }
        public clsUser(int UserID, string Username, string Password, byte IsActive, int PersonID) {
        
        this.UserID = UserID;
        this.UserName = Username;
        this.Password = Password;
        this.IsActive = (IsActive == 1)? true : false;
        this.Person = clsPerson.FindPerson(PersonID);
        this._Mode = enMode.Update;
        }

        private bool AddNew()

        {
            
            UserID =  clsUserAccess.AddNew(this.Person.PersonID,UserName,Password, Convert.ToByte(IsActive));
            return (UserID != -1);
        }
        private bool Update()
        {
            int result = clsUserAccess.Update(this.UserID,this.Person.PersonID, this.UserName, this.Password, Convert.ToByte(IsActive));
            return (result > 0);
        }

       
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    _Mode = enMode.AddNew;
                    return AddNew();
                    
                case enMode.Update:
                    return Update();
                default:
                    break;
            }
            return false;
        }

        static public bool Delete(int UserID)
        {
            int result = clsUserAccess.Delete(UserID);
            return (result > 0);    
        }
        static public DataTable GetAllUsers()
        {
            return clsUserAccess.GetAllUsers();
        }
        static public clsUser FindUser(string UserName)
        {
            string Password = "";
            byte IsActive = 0;
            int UserID = -1;
            int PersonID = -1;

            bool IsFound = clsUserAccess.GetUserInfoByUserName(UserName,ref PersonID,ref UserID,ref Password,ref IsActive);

            if (IsFound)
            {
                return new clsUser(UserID, UserName, Password, IsActive, PersonID);                 
            }
            return null;
        }

        static public clsUser FindUser(int UserID)
        {
            string Password = "";
            byte IsActive = 0;
            string UserName = "";
            int PersonID = -1;

            bool IsFound = clsUserAccess.GetUserInfoByID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
            {
                return new clsUser(UserID, UserName, Password, IsActive, PersonID);
            }
            return null;
        }

        static public bool IsUserExistWithPersonID(int PersonID)
        {
            return clsUserAccess.IsExistWithPerson(PersonID);
        }

        static public bool IsExistWithUserNameAndPassword(string UserName,string Password)
        {
            return clsUserAccess.IsExistWithUserNameAndPassword(UserName,Password);
        }

    }
}
