using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HRManagementSystem
{
    class Education
    {
        #region Variables
        public string EmployeeID,
                      EduAttainment,
                      College,
                      SeniorHigh,
                      JuniorHigh,
                      Elementary;

        DBOperation _dbOp = new DBOperation();
        #endregion



        #region Constructors
        public Education() { }

        public Education(string employeeID,
                         string eduAttainment,
                         string college,
                         string seniorHigh,
                         string juniorHigh,
                         string elementary)
        {
            EmployeeID = employeeID;
            EduAttainment = eduAttainment;
            College = college;
            SeniorHigh = seniorHigh;
            JuniorHigh = juniorHigh;
            Elementary = elementary;
        }
        #endregion



        #region Education Methods
        public void InsertEducation(Education education)
        {
            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();

                cmd.CommandText = @"INSERT INTO Education(EmployeeID,
                                                          EduAttainment,
                                                          College,
                                                          SeniorHigh,
                                                          JuniorHigh,
                                                          Elementary)

                                                   VALUES(@EmployeeID,
                                                          @EduAttainment,
                                                          @College,
                                                          @SeniorHigh,
                                                          @JuniorHigh,
                                                          @Elementary)";

                cmd.Parameters.AddWithValue("@EmployeeID", education.EmployeeID);
                cmd.Parameters.AddWithValue("@EduAttainment", education.EduAttainment);
                cmd.Parameters.AddWithValue("@College", education.College);
                cmd.Parameters.AddWithValue("@SeniorHigh", education.SeniorHigh);
                cmd.Parameters.AddWithValue("@JuniorHigh", education.JuniorHigh);
                cmd.Parameters.AddWithValue("@Elementary", education.Elementary);

                cmd.ExecuteNonQuery();
                MessageBox.Show("New Employee Education has been saved!");
                _dbOp.DBClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public Education RetrieveEducation(string education)
        {
            Education temp = new Education();

            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Education " + "WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeID = (string)reader.GetValue(0);
                    EduAttainment = (string)reader.GetValue(1);
                    College = (string)reader.GetValue(2);
                    SeniorHigh = (string)reader.GetValue(3);
                    JuniorHigh = (string)reader.GetValue(4);
                    Elementary = (string)reader.GetValue(5);

                    temp = new Education(EmployeeID,
                                         EduAttainment,
                                         College,
                                         SeniorHigh,
                                         JuniorHigh,
                                         Elementary);
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

        public void UpdateEducation(Education education)
        {
            try
            {
                _dbOp.DBConnect();
                MySqlCommand cmd = _dbOp._dbConn.CreateCommand();
                cmd.CommandText = @"UPDATE Education SET EmployeeID = @EmployeeID,
                                                         EduAttainment = @EduAttainment,
                                                         College = @College,
                                                         SeniorHigh = @SeniorHigh,
                                                         JuniorHigh = @JuniorHigh,
                                                         Elementary = @Elementary" + " WHERE EmployeeID = @EmployeeID";

                cmd.Parameters.AddWithValue("@EmployeeID", education.EmployeeID);
                cmd.Parameters.AddWithValue("@EduAttainment", education.EduAttainment);
                cmd.Parameters.AddWithValue("@College", education.College);
                cmd.Parameters.AddWithValue("@SeniorHigh", education.SeniorHigh);
                cmd.Parameters.AddWithValue("@JuniorHigh", education.JuniorHigh);
                cmd.Parameters.AddWithValue("@Elementary", education.Elementary);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Employee Education has been saved!");
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
