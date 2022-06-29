using System;
using System.Data.SqlClient;

namespace VeatUAM.Core
{
    public static class MySqlConnectionService
    {
        private const string  ConnectionString =
            @"Data Source=localhost;" +
            "Initial Catalog=VEAT_DEV;" +
            "Integrated Security=False;" +
            "User ID=sa;Password=Pass@word";

        static MySqlConnectionService()
        {
            Connection = new SqlConnection(ConnectionString);
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