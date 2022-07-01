using System;
using System.Data.SqlClient;
using System.Windows;

namespace VeatUAM.Core
{
    public static class MySqlConnectionService
    {
        private const string  ConnectionString =
            @"Data Source=10.133.130.206,1433;" +
            "Initial Catalog=VEAT_DEV;" +
            "Integrated Security=False;" +
            "User ID=sa;Password=Pass@word";

        static MySqlConnectionService()
        {
            try
            {
                Connection = new SqlConnection(ConnectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static SqlConnection Connection { get; }
        
        public static SqlCommand Command { get; set; }

        public static SqlDataReader Reader { get; private set; }

        public static void SetupQuery(string query)
        {
            Command = new SqlCommand(query, Connection);
        }

        public static void SetupReader()
        {
            if (Command != null)
            {
                Reader = Command.ExecuteReader();
            }
            else
            {
                throw new NullReferenceException("MySqlConnection.Command is not set");
            }
        }

        public static bool ConnectionState()
        {
            return Connection != null && Connection.State == System.Data.ConnectionState.Open;
        }
    }
}