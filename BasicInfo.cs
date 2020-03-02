using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HRManagementSystem
{
    class BasicInfo
    {
        #region Variables
        public string EmployeeID,
                      FirstName,
                      MiddleName,
                      LastName,
                      Gender,
                      MaritalStatus,
                      Nationality,
                      CurrentAddress,
                      PermanentAddress;
        public DateTime DOB;
        public byte Picture;

        DBOperation _dbOp = new DBOperation();
        #endregion



        #region Constructors
        public BasicInfo() { }
        public BasicInfo(string employeeID)
        { 
            EmployeeID = employeeID; 
        }
        public BasicInfo(string employeeID, 
                         string firstName,
                         string middleName,
                         string lastName)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public BasicInfo(string employeeID,
                         string firstName,
                         string middleName,
                         string lastName,
                         DateTime dob,
                         string gender,
                         string maritalStatus,
                         string nationality,
                         string currentAddress,
                         string permanentAddress,
                         byte picture)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            DOB = dob;
            Gender = gender;
            MaritalStatus = maritalStatus;
            Nationality = nationality;
            CurrentAddress = currentAddress;
            PermanentAddress = permanentAddress;
            Picture = picture;
        }
        #endregion




        #region others
        public string Name
        {
            get { return FirstName + " " + MiddleName + " " + LastName; }
        }
        public string Head
        {
            get { return FirstName + " " + LastName; }
        }
        #endregion



        #region BasicInfo Methods
        /*public byte[] imageToByte(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }*/

        public void InsertBasicInfo(BasicInfo basicInfo)
        {
            try
            {
                //var userImage = imageToByte(image);
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();

                cmd.CommandText = @"INSERT INTO BasicInfo(EmployeeID,
                                                          FirstName,
                                                          MiddleName,
                                                          LastName,
                                                          DateOfBirth,
                                                          Gender,
                                                          MaritalStatus,
                                                          Nationality,
                                                          CurrentAddress,
                                                          PermanentAddress,
                                                          Picture)

                                                   VALUES(@EmployeeID,
                                                          @FirstName,
                                                          @MiddleName,
                                                          @LastName,
                                                          @DateOfBirth,
                                                          @Gender,
                                                          @MaritalStatus,
                                                          @Nationality,
                                                          @CurrentAddress,
                                                          @PermanentAddress,
                                                          @Picture)";

                cmd.Parameters.AddWithValue("@EmployeeID", basicInfo.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", basicInfo.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", basicInfo.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", basicInfo.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", basicInfo.DOB);
                cmd.Parameters.AddWithValue("@Gender", basicInfo.Gender);
                cmd.Parameters.AddWithValue("@MaritalStatus", basicInfo.MaritalStatus);
                cmd.Parameters.AddWithValue("@Nationality", basicInfo.Nationality);
                cmd.Parameters.AddWithValue("@CurrentAddress", basicInfo.CurrentAddress);
                cmd.Parameters.AddWithValue("@PermanentAddress", basicInfo.PermanentAddress);
                cmd.Parameters.Add("@Picture", MySqlDbType.MediumBlob);
                cmd.Parameters["@Picture"].Value = Picture;
               // var paramUserImage = new MySqlParameter("@Picture", MySqlDbType.Blob, userImage.Length);
                //cmd.Parameters.AddWithValue("@Picture", basicInfo.Picture);

                cmd.ExecuteNonQuery();
                MessageBox.Show("New Basic Information has been saved!");
                _dbOp.DBClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public BasicInfo RetrieveBasicInfo(string employeeID)
        {
            BasicInfo temp = new BasicInfo();

            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM BasicInfo " + "WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeID = (string)reader.GetValue(0);
                    FirstName = (string)reader.GetValue(1);
                    MiddleName = (string)reader.GetValue(2);
                    LastName = (string)reader.GetValue(3);
                    DOB = (DateTime)reader.GetValue(4);
                    Gender = (string)reader.GetValue(5);
                    MaritalStatus = (string)reader.GetValue(6);
                    Nationality = (string)reader.GetValue(7);
                    CurrentAddress = (string)reader.GetValue(8);
                    PermanentAddress = (string)reader.GetValue(9);
                    Picture = (byte)reader.GetValue(10);

                    temp = new BasicInfo(employeeID,
                                         FirstName,
                                         MiddleName,
                                         LastName,
                                         DOB,
                                         Gender,
                                         MaritalStatus,
                                         Nationality,
                                         CurrentAddress,
                                         PermanentAddress,
                                         Picture);
                }
                reader.Close();
                _dbOp.DBClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return temp;
        }
        public BasicInfo RetrieveBasicInfoID(string employeeName)
        {
            string[] empName = employeeName.Split(',');
            string first = empName[0];
            string middle = empName[1];
            string last = empName[2];

            BasicInfo basicInfo = new BasicInfo();
            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"SELECT EmployeeID, FirstName, MiddleName, LastName FROM BasicInfo WHERE FirstName = '" + first + "' AND MiddleName = '" + middle + "' AND LastName = '" + last + "' AND Department = 'Manager'";

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    EmployeeID = (string)reader.GetValue(0);
                    FirstName = (string)reader.GetValue(1);
                    MiddleName = (string)reader.GetValue(2);
                    LastName = (string)reader.GetValue(3);

                    basicInfo = new BasicInfo(EmployeeID, FirstName, MiddleName, LastName);
                }
                reader.Close();
                _dbOp.DBClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return basicInfo;
        }

        public List<BasicInfo> RetrieveBasicInfoList()
        {
            List<BasicInfo> basicInfoList = new List<BasicInfo>();

            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM BasicInfo";
                MySqlDataReader reader = cmd.ExecuteReader();
                BasicInfo temp = new BasicInfo();

                if (reader.Read())
                {
                    EmployeeID = (string)reader.GetValue(0);
                    FirstName = (string)reader.GetValue(1);
                    MiddleName = (string)reader.GetValue(2);
                    LastName = (string)reader.GetValue(3);
                    DOB = (DateTime)reader.GetValue(4);
                    Gender = (string)reader.GetValue(5);
                    MaritalStatus = (string)reader.GetValue(6);
                    Nationality = (string)reader.GetValue(7);
                    CurrentAddress = (string)reader.GetValue(8);
                    PermanentAddress = (string)reader.GetValue(9);
                    Picture = (byte)reader.GetValue(10);

                    temp = new BasicInfo(EmployeeID,
                                         FirstName,
                                         MiddleName,
                                         LastName,
                                         DOB,
                                         Gender,
                                         MaritalStatus,
                                         Nationality,
                                         CurrentAddress,
                                         PermanentAddress,
                                         Picture);

                    basicInfoList.Add(temp);
                }
                reader.Close();
                _dbOp.DBClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return basicInfoList;
        }

        public void UpdateBasicInfo(BasicInfo basicInfo)
        {
            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"UPDATE BasicInfo SET EmployeeID = @EmployeeID,
                                                         FirstName = @FirstName,
                                                         MiddleName = @MiddleName,
                                                         LastName = @LastName,
                                                         DateOfBirth = @DateOfBirth,
                                                         Gender = @Gender,
                                                         MaritalStatus = @MaritalStatus,
                                                         Nationality = @Nationality,
                                                         CurrentAddress = @CurrentAddress,
                                                         PermanentAddress = @PermanentAddress,
                                                         Picture = @Picture" + " WHERE EmployeeID = @EmployeeID";

                cmd.Parameters.AddWithValue("@EmployeeID", basicInfo.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", basicInfo.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", basicInfo.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", basicInfo.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", basicInfo.DOB);
                cmd.Parameters.AddWithValue("@Gender", basicInfo.Gender);
                cmd.Parameters.AddWithValue("@MaritalStatus", basicInfo.MaritalStatus);
                cmd.Parameters.AddWithValue("@Nationality", basicInfo.Nationality);
                cmd.Parameters.AddWithValue("@CurrentAddress", basicInfo.CurrentAddress);
                cmd.Parameters.AddWithValue("@PermanentAddress", basicInfo.PermanentAddress);
                cmd.Parameters.Add("@Picture", MySqlDbType.MediumBlob);
                cmd.Parameters["@Picture"].Value = Picture;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Employee Information has been saved!");
                _dbOp.DBClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion



        #region Making New EmployeeID
        public string CreateEmployeeID()
        {
            EmployeeID = "TGHB" + NumberPrefix(CountEmployee()) + CountEmployee();
            return EmployeeID;
        }
        public int CountEmployee()
        {
            int count = 1;

            _dbOp.DBConnect();
            MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM BasicInfo";
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                count++;
            }
            reader.Close();
            _dbOp.DBClose();

            return count;
        }
        public string NumberPrefix(int count)
        {
            string numPrefix = "";
            if (count < 10)
            {
                numPrefix = "00000";
            }
            else if (count < 100)
            {
                numPrefix = "0000";
            }
            else if (count < 1000)
            {
                numPrefix = "000";
            }
            else if (count < 10000)
            {
                numPrefix = "00";
            }
            else if (count < 100000)
            {
                numPrefix = "0";
            }
            else
            {
                numPrefix = "";
            }
            return numPrefix;
        }
        #endregion
    }
}
