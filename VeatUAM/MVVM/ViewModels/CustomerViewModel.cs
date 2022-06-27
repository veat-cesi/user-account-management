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
            MySqlConnection mySqlConnection = new MySqlConnection();
            mySqlConnection.Connection.Open();
            if (mySqlConnection.ConnectionState())
            {
                const string query = "SELECT * FROM customer";
                mySqlConnection.SetupQuery(query);
                mySqlConnection.SetupReader();
                while (mySqlConnection.Reader.Read())
                {
                    
                    /*
                    DO NOT USE DICTIONNARY
                    RATHER USE DIRECTLY READER.GETINT32/STRING/DATETIMEOFFSET/BOOLEAN
                    DO NOT MIND BOXING
                     */
                    
                    Trace.WriteLine(mySqlConnection.Reader.GetInt32(0));
                    Trace.WriteLine(mySqlConnection.Reader.GetDateTimeOffset(6));
                    var rowModel = new Dictionary<string, object>();
                    for (var i = 0; i < mySqlConnection.Reader.FieldCount; i++)
                    {
                        rowModel.Add(mySqlConnection.Reader.GetName(i),
                            mySqlConnection.Reader.GetValue(i));
                    }
                    Trace.WriteLine(rowModel.ElementAt(1).Value);
                    var pendingCustomer = new CustomerModel();
                    pendingCustomer.ToCustomerModel(rowModel);
                    Customers.Add(pendingCustomer);
                }
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