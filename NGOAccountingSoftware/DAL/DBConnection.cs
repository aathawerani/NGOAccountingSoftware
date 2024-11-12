using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using TrustApplication.Exceptions;

namespace TrustApplication
{
    class DBConnection
    {

        private DBConnection()
        {
        }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public void Connect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                {
                    throw new DBConnectionException("Database name is null");
                }
                string connstring = string.Format("Server=localhost; database={0}; UID=aaht14; password=aliALI@786110;CharSet=utf8;", databaseName);
                connection = new MySqlConnection(connstring);
            }
            if(connection == null)
                throw new DBConnectionException("Couldn't connect database");
        }

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
