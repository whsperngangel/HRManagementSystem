using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HRManagementSystem
{
    class ContactInfo
    {
        #region Variables
        public string EmployeeID,
                      Email, 
                      Globe,
                      Smart;

        DBOperation _dbOp = new DBOperation();
        #endregion


        #region Constructors
        public ContactInfo() { }

        public ContactInfo(string employeeID,
                           string globe,
                           string smart,
                           string email)
        {
            EmployeeID = employeeID;
            Globe = globe;
            Smart = smart;
            Email = email;
        }
        #endregion



        #region Contact Information Methods
        public void InsertContactInfo(ContactInfo contactInfo)
        {
            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();

                cmd.CommandText = @"INSERT INTO ContactInfo(EmployeeID,
                                                            Globe,
                                                            Smart,
                                                            Email)

                                                      VALUE(@EmployeeID
                                                            @Globe,
                                                            @Smart,
                                                            @Email)";

                cmd.Parameters.AddWithValue("@EmployeeID", contactInfo.EmployeeID);
                cmd.Parameters.AddWithValue("@Globe", contactInfo.Globe);
                cmd.Parameters.AddWithValue("@Smart", contactInfo.Smart);
                cmd.Parameters.AddWithValue("@Email", contactInfo.Email);

                cmd.ExecuteNonQuery();
                MessageBox.Show("New Employee Contact Information has been saved!");
                _dbOp.DBClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public ContactInfo RetrieveContactInfo(string employeeID)
        {
            ContactInfo temp = new ContactInfo();

            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM ContactInfo " + "WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeID = (string)reader.GetValue(0);
                    Globe = (string)reader.GetValue(1);
                    Smart = (string)reader.GetValue(2);
                    Email = (string)reader.GetValue(3);

                    temp = new ContactInfo(employeeID,
                                           Globe,
                                           Smart,
                                           Email);
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

        public void UpdateContactInfo(ContactInfo contactInfo)
        {
            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"Update ContactInfo SET EmployeeID = @EmployeeID,
                                                           Globe = @Globe,
                                                           Smart = @Smart,
                                                           Email = @Email" + " WHERE EmployeeID = @EmployeeID";

                cmd.Parameters.AddWithValue("@EmployeeID", contactInfo.EmployeeID);
                cmd.Parameters.AddWithValue("@Globe", contactInfo.Globe);
                cmd.Parameters.AddWithValue("@Smart", contactInfo.Smart);
                cmd.Parameters.AddWithValue("@Email", contactInfo.Email);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Employee Contact Information has been saved!");
                _dbOp.DBClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
    }
}
