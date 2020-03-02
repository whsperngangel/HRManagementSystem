using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HRManagementSystem
{
    class DBOperation
    {
        #region Variables
        public MySqlConnection _dbConn;
        public string Server,
                       Port,
                       Database,
                       Uid,
                       Password;
        private string _connectionString;
        #endregion



        #region Connection
        public DBOperation()
        {
            Server = "localhost";
            Port = "3306";
            Database = "employee";
            Uid = "root";
            Password = "";
            _connectionString = String.Format("server={0}; port={1}; user id={2}; password={3}; database={4};", Server, Port, Uid, Password, Database);
            _dbConn = new MySqlConnection(_connectionString);
        }

        public void DBConnect()
        {
            try
            {
                _dbConn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                _dbConn.Close();
            }
        }
        public void DBClose()
        {
            try
            {
                _dbConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
    }
}
