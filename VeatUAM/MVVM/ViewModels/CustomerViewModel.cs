using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using VeatUAM.Core;
using VeatUAM.MVVM.Models;

namespace VeatUAM.MVVM.ViewModels
{
    public class CustomerViewModel : ObservableObject
    {
        public string head = "Customers";

        public CustomerViewModel()
        {
            InitCustomers();
        }

        public IList<CustomerModel> Customers { get; set; }

        private void InitCustomers()
        {
            Customers = new List<CustomerModel>();
            const string query =
                "SELECT id, firstName, lastName, email, phone, createdAt, updatedAt, deleted FROM customer";
            MySqlConnectionService.SetupQuery(query);
            MySqlConnectionService.SetupReader();
            while (MySqlConnectionService.Reader.Read())
            {
                Customers.Add(new CustomerModel(
                    MySqlConnectionService.Reader.GetInt32(0),
                    MySqlConnectionService.Reader.GetString(1),
                    MySqlConnectionService.Reader.GetString(2),
                    MySqlConnectionService.Reader.GetString(3),
                    MySqlConnectionService.Reader.GetString(4),
                    MySqlConnectionService.Reader.GetDateTimeOffset(5),
                    MySqlConnectionService.Reader.GetDateTimeOffset(6),
                    MySqlConnectionService.Reader.GetBoolean(7)
                ));
            }
            MySqlConnectionService.Reader.Close();
        }

        public void CreateCustomer(CustomerModel c)
        {
            if (c == null) return;
            if (c.FirstName == null && c.LastName == null && c.Email == null && c.Phone == null &&
                c.Password == null && c.Deleted)
            {
                MessageBox.Show("Invalid customer model");
                return;
            }

            if (!IsUniqueEmail(c.Email))
            {
                MessageBox.Show("Email already in database!");
                return;
            }
            
            try
            {
                const string query =
                    "INSERT INTO customer " +
                    "(firstName, lastName, email, phone, password, createdAt, updatedAt, deleted, deletedAt) " +
                    "VALUES (@first_name, @last_name, @email, @phone, @password, @created_at, @updated_at, @deleted, NULL)";
                MySqlConnectionService.SetupQuery(query);
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = c.FirstName;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = c.LastName;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255)).Value = c.Email;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255)).Value = c.Phone;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 255)).Value = c.Password;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@created_at", SqlDbType.DateTimeOffset)).Value = c.CreatedAt;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = c.UpdatedAt;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@deleted", SqlDbType.Bit)).Value = c.Deleted;
                MySqlConnectionService.Command.ExecuteNonQuery();
                InitCustomers();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void EditCustomer(CustomerModel c)
        {
            if (c == null) return;
            if (c.FirstName == null && c.LastName == null && c.Email == null && c.Phone == null && c.Deleted)
            {
                MessageBox.Show("Invalid customer model");
                return;
            }
            if (!IsUniqueEmail(c.Email))
            {
                MessageBox.Show("Email already in database!");
                return;
            }
            try
            {
                string query;
                if (!string.IsNullOrWhiteSpace(c.Password))
                {
                    query = "UPDATE customer SET " +
                            "firstName = @first_name, lastName = @last_name, email = @email, phone = @phone, " +
                            "password = @password, updatedAt = @updated_at " +
                            "WHERE id = @id;";
                    MySqlConnectionService.SetupQuery(query);
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 255)).Value = c.Password;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4)).Value = c.Id;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = c.FirstName;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = c.LastName;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255)).Value = c.Email;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255)).Value = c.Phone;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = c.UpdatedAt;
                    MySqlConnectionService.Command.ExecuteNonQuery();
                }
                else
                {
                    query = "UPDATE customer SET " +
                            "firstName = @first_name, lastName = @last_name, email = @email, phone = @phone, " +
                            "updatedAt = @updated_at " +
                            "WHERE id = @id;";
                    MySqlConnectionService.SetupQuery(query);
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = c.Id;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = c.FirstName;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = c.LastName;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255)).Value = c.Email;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255)).Value = c.Phone;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = c.UpdatedAt;
                    MySqlConnectionService.Command.ExecuteNonQuery();
                }
                InitCustomers();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteCustomer(int id)
        {
            
            const string query = "UPDATE customer SET deleted = @deleted, deletedAt = @deleted_at WHERE id = @id;";
            MySqlConnectionService.SetupQuery(query);
            MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int))
                .Value = id;
            MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@deleted", SqlDbType.Bit))
                .Value = true;
            MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@deleted_at", SqlDbType.DateTimeOffset))
                .Value = DateTimeOffset.Now;
            try
            {
                MySqlConnectionService.Command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            InitCustomers();
        }

        private bool IsUniqueEmail(string email)
        {
            const string query = "SELECT COUNT(email) FROM customer WHERE email LIKE @email;";
            MySqlConnectionService.SetupQuery(query);
            MySqlConnectionService.Command.Parameters.Add("@email", SqlDbType.VarChar, 255).Value = email;
            MySqlConnectionService.SetupReader();
            while (MySqlConnectionService.Reader.Read())
            {
                if (MySqlConnectionService.Reader.GetInt32(0) == 0)
                {
                    MySqlConnectionService.Reader.Close();
                    return true;
                }
                else if (MySqlConnectionService.Reader.GetInt32(0) > 0)
                {
                    MySqlConnectionService.Reader.Close();
                    return false;
                }
            }
            MySqlConnectionService.Reader.Close();
            throw new Exception("Error occured when searching for matching emails count");
        }
        
    }
}