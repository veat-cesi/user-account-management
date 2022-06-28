using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using VeatUAM.Core;
using VeatUAM.MVVM.Model;

namespace VeatUAM.MVVM.ViewModel
{
    public class CustomerViewModel
    {
        public string head = "Customers";

        public CustomerViewModel()
        {
            Customers = new List<CustomerModel>();
            var mySqlConnection = new MySqlConnection();
            mySqlConnection.Connection.Open();
            if (mySqlConnection.ConnectionState())
            {
                const string query =
                    "SELECT id, firstName, lastName, email, phone, createdAt, updatedAt, deleted FROM customer";
                mySqlConnection.SetupQuery(query);
                mySqlConnection.SetupReader();
                while (mySqlConnection.Reader.Read())
                    Customers.Add(new CustomerModel(
                        mySqlConnection.Reader.GetInt32(0),
                        mySqlConnection.Reader.GetString(1),
                        mySqlConnection.Reader.GetString(2),
                        mySqlConnection.Reader.GetString(3),
                        mySqlConnection.Reader.GetString(4),
                        mySqlConnection.Reader.GetDateTimeOffset(5),
                        mySqlConnection.Reader.GetDateTimeOffset(6),
                        mySqlConnection.Reader.GetBoolean(7)
                    ));
                mySqlConnection.Connection.Close();
            }
            else
            {
                MessageBox.Show("Connection failed");
            }
        }

        public IList<CustomerModel> Customers { get; set; }
    }
}