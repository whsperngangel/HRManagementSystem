using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HRManagementSystem
{
    class GovernmentID
    {
        #region Variables
        public string EmployeeID,
                      TIN,
                      SSS,
                      PAGIBIG,
                      PhilHealth;

        DBOperation _dbOp = new DBOperation();
        #endregion



        #region Constructors
        public GovernmentID() { }

        public GovernmentID(string employeeID,
                            string tin,
                            string sss,
                            string pagibig,
                            string philHealth)
        {
            EmployeeID = employeeID;
            TIN = tin;
            SSS = sss;
            PAGIBIG = pagibig;
            PhilHealth = philHealth;
        }
        #endregion



        #region GovernmentID Methods
        public void InsertGovernmentID(GovernmentID governmentID)
        {
            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();

                cmd.CommandText = @"INSERT INTO GovernmentID(EmployeeID,
                                                             TIN,
                                                             SSS,
                                                             PAGIBIG,
                                                             PhilHealth)

                                                       VALUE(@EmployeeID,
                                                             @TIN,
                                                             @SSS,
                                                             @PAGIBIG,
                                                             @PhilHealth)";
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                cmd.Parameters.AddWithValue("@TIN", TIN);
                cmd.Parameters.AddWithValue("@SSS", SSS);
                cmd.Parameters.AddWithValue("@PAGIBIG", PAGIBIG);
                cmd.Parameters.AddWithValue("@PhilHealth", PhilHealth);

                cmd.ExecuteNonQuery();
                MessageBox.Show("New Employee Government ID has been saved!");
                _dbOp.DBClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public GovernmentID RetrieveGovernmentID(string employeeID)
        {
            GovernmentID temp = new GovernmentID();

            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM GovernmentID " + "WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeID = (string)reader.GetValue(0);
                    TIN = (string)reader.GetValue(1);
                    SSS = (string)reader.GetValue(2);
                    PAGIBIG = (string)reader.GetValue(3);
                    PhilHealth = (string)reader.GetValue(4);

                    temp = new GovernmentID(employeeID,
                                            TIN,
                                            SSS,
                                            PAGIBIG,
                                            PhilHealth);
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

        public void UpdateGovernmentID(GovernmentID governmentID)
        {
            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"UPDATE GovernmentID SET EmployeeID = @EmployeeID,
                                                            TIN = @TIN,
                                                            SSS = @SSS,
                                                            PAGIBIG = @PAGIBIG,
                                                            PhilHealth = @PhilHealth" + " WHERE EmployeeID = @EmployeeID";

                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                cmd.Parameters.AddWithValue("@TIN", TIN);
                cmd.Parameters.AddWithValue("@SSS", SSS);
                cmd.Parameters.AddWithValue("@PAGIBIG", PAGIBIG);
                cmd.Parameters.AddWithValue("@PhilHealth", PhilHealth);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Employee Government ID has been saved!");
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
