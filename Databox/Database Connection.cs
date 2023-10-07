using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Databox
{
    public class Database_Connection
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public Database_Connection()
        {
            Initialize();
        }

        // Initialize values
        /*private void Initialize()
        {
            server = "sql12.freemysqlhosting.net";
            database = "sql12625611";
            uid = "sql12625611";
            password = "Zflm2lfhsN";
            string connectionString;
            connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};Port=3306;";

            connection = new MySqlConnection(connectionString);
        }*/
        private void Initialize()
        {
            server = "sql12.freemysqlhosting.net";
            database = "sql12627671";
            uid = "sql12627671";
            password = "ErX84Suvrd";
            string connectionString;
            connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};Port=3306;";

            connection = new MySqlConnection(connectionString);
        }
        // Open connection to the database
        public bool OpenConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Open) // check if connection is not already open
                {
                    connection.Open();
                }
                return true;
            }
            catch (MySqlException ex)
            {
                // Handle the exception appropriately (e.g. log the error)
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Close connection to the database
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // Handle the exception appropriately (e.g. log the error)
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Execute query
        public void ExecuteQuery(string query)
        {
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        // Retrieve data from database
        public MySqlDataReader GetData(string query)
        {
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                return dataReader;
            }
            return null;
        }
        public object ExecuteScalar(string query)
        {
            object result = null;
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                result = cmd.ExecuteScalar();
                CloseConnection();
            }
            return result;
        }

        public int ExecuteNonQuery(string query)
        {
            int rowsAffected = 0;
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                rowsAffected = cmd.ExecuteNonQuery();
                CloseConnection();
            }
            return rowsAffected;
        }

        public DataTable ExecuteSelectQuery(string query)
        {
            DataTable resultTable = new DataTable();
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(resultTable);
                CloseConnection();
            }
            return resultTable;
        }
    }
}
