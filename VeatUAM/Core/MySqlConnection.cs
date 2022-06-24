using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace VeatUAM.Core
{
    public class MySqlConnection
    {
        private const string  ConnectionString =
            @"Data Source=localhost;" +
            "Initial Catalog=VEAT_DEV;" +
            "Integrated Security=False;" +
            "User ID=sa;Password=Pass@word";

        private SqlCommand _command;

        public MySqlConnection()
        {
            Connection = new SqlConnection(ConnectionString);
        }

        public SqlConnection Connection { get; }

        public SqlCommand Command { get; }

        public SqlDataReader Reader { get; private set; }

        public void SetupQuery(string query)
        {
            _command = new SqlCommand(query, Connection);
        }

        public void SetupReader()
        {
            if (_command != null)
            {
                Reader = _command.ExecuteReader();
            }
            else
            {
                throw new NullReferenceException("MySqlConnection.Command is not set");
            }
        }

        public bool ConnectionState()
        {
            return Connection != null && Connection.State == System.Data.ConnectionState.Open;
        }
    }
}