using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Windows.Forms;
using BusinessLayer;

namespace Driving_License_Management.GlobalClasses
{
    internal static class clsGlobal
    {

        static public clsUser CurrentUser;

        static public bool RememberMeUsernameAndPassword(string Username, string Password)
        {
            try
            {
                string thisFolderPath = System.IO.Directory.GetCurrentDirectory();
                string FilePath = thisFolderPath + "\\data";

                if (Username == "" && File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    return true;
                }



                string DataToSave = Username + "//##//" + Password;

                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    writer.WriteLine(DataToSave);
                    return true;
                }


            }
            catch (Exception ex)
            {
              MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
             


        }

        static public bool GetStoredCredintial(ref string Username,ref string Password) {

            try
            {
                string thisFolderPath = System.IO.Directory.GetCurrentDirectory();
                string FilePath = thisFolderPath + "\\data";
                if (File.Exists(FilePath))
                {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        string Line;
                        while ((Line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(Line);

                            string[] result = Line.Split(new string[] { "//##//" }, StringSplitOptions.None);
                            
                                Username = result[0];
                                Password = result[1];
                            
                        }
                        return true;
                    }

                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        
        }
    }
}
