using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HRManagementSystem
{
    class CPosition
    {
        #region Variables
        public string EmployeeID,
                      Position,
                      Department,
                      Branch,
                      Head;

        public DateTime StartDate;

        DBOperation _dbOp = new DBOperation();
        #endregion



        #region Constructors
        public CPosition() { }

        public CPosition(string employeeID,
                         string position,
                         string department,
                         string branch,
                         string head,
                         DateTime startDate)
        {
            EmployeeID = employeeID;
            Position = position;
            Department = department;
            Branch = branch;
            Head = head;
            StartDate = startDate;
        }
        #endregion



        #region Position Methods
        public void InsertCPosition(CPosition cPosition)
        {
            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"INSERT INTO CPosition(EmployeeID,
                                                          Position,
                                                          Department,
                                                          Branch,
                                                          Head,
                                                          StartDate)

                                                    VALUE(@EmployeeID,
                                                          @Position,
                                                          @Department,
                                                          @Branch,
                                                          @Head,
                                                          @StartDate)";

                cmd.Parameters.AddWithValue("@EmployeeID", cPosition.EmployeeID);
                cmd.Parameters.AddWithValue("@Position", cPosition.Position);
                cmd.Parameters.AddWithValue("@Department", cPosition.Department);
                cmd.Parameters.AddWithValue("@Branch", cPosition.Branch);
                cmd.Parameters.AddWithValue("@Head", cPosition.Head);
                cmd.Parameters.AddWithValue("@StartDate", cPosition.StartDate);

                cmd.ExecuteNonQuery();
                MessageBox.Show("New Employee Position has been saved!");
                _dbOp.DBClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public CPosition RetrieveCPosition(string employeeID)
        {
            CPosition temp = new CPosition();

            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM CPosition " + "WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeID = (string)reader.GetValue(0);
                    Position = (string)reader.GetValue(1);
                    Department = (string)reader.GetValue(2);
                    Branch = (string)reader.GetValue(3);
                    Head = (string)reader.GetValue(4);
                    StartDate = (DateTime)reader.GetValue(5);

                    temp = new CPosition(employeeID,
                                         Position,
                                         Department,
                                         Branch,
                                         Head,
                                         StartDate);
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

        public List<CPosition> RetrieveCPositionList()
        {
            List<CPosition> cPositionList = new List<CPosition>();

            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM CPosition";
                MySqlDataReader reader = cmd.ExecuteReader();
                CPosition temp = new CPosition();

                while (reader.Read())
                {
                    EmployeeID = (string)reader.GetValue(0);
                    Position = (string)reader.GetValue(1);
                    Department = (string)reader.GetValue(2);
                    Branch = (string)reader.GetValue(3);
                    Head = (string)reader.GetValue(4);
                    StartDate = (DateTime)reader.GetValue(5);

                    temp = new CPosition(EmployeeID,
                                         Position,
                                         Department,
                                         Branch,
                                         Head,
                                         StartDate);

                    cPositionList.Add(temp);
                }
                reader.Close();
                _dbOp.DBClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return cPositionList;
        }

        public void UpdateCPosition(CPosition cPosition)
        {
            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"UPDATE CPosition SET EmployeeID = @EmployeeID,
                                                         Position = @Position,
                                                         Department = @Department,
                                                         Branch = @Branch,
                                                         Head = @Head,
                                                         StartDate = StartDate";

                cmd.Parameters.AddWithValue("@EmployeeID", cPosition.EmployeeID);
                cmd.Parameters.AddWithValue("@Position", cPosition.Position);
                cmd.Parameters.AddWithValue("@Department", cPosition.Department);
                cmd.Parameters.AddWithValue("@Branch", cPosition.Branch);
                cmd.Parameters.AddWithValue("@Head", cPosition.Head);
                cmd.Parameters.AddWithValue("@StartDate", cPosition.StartDate);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Employee Position has been saved!");
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
