using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using VeatUAM._Services;
using VeatUAM.Core;
using VeatUAM.MVVM.Models;

namespace VeatUAM.MVVM.ViewModels
{
    public class SalesUserViewModel : ObservableObject
    {
        public string head = "Sales Service";

        public SalesUserViewModel()
        {
            InitSalesUsers();
        }

        public IList<SalesUserModel> SalesUsers { get; set; }

        private void InitSalesUsers()
        {
            SalesUsers = new List<SalesUserModel>();
            const string query =
                "SELECT id, firstName, lastName, email, phone, createdAt, updatedAt, deleted FROM sales";
            MySqlConnectionService.SetupQuery(query);
            MySqlConnectionService.SetupReader();
            while (MySqlConnectionService.Reader.Read())
                SalesUsers.Add(new SalesUserModel(
                    MySqlConnectionService.Reader.GetInt32(0),
                    MySqlConnectionService.Reader.GetString(1),
                    MySqlConnectionService.Reader.GetString(2),
                    MySqlConnectionService.Reader.GetString(3),
                    MySqlConnectionService.Reader.GetString(4),
                    MySqlConnectionService.Reader.GetDateTimeOffset(5),
                    MySqlConnectionService.Reader.GetDateTimeOffset(6),
                    MySqlConnectionService.Reader.GetBoolean(7)
                ));
            MySqlConnectionService.Reader.Close();
        }

        public void CreateSalesUser(SalesUserModel su)
        {
            if (su == null) return;
            if (su.FirstName == null && su.LastName == null && su.Email == null && su.Phone == null &&
                su.Password == null && su.Deleted)
            {
                MessageBox.Show("Invalid sales model");
                return;
            }

            if (!IsUniqueEmail(su.Email))
            {
                MessageBox.Show("Email already in database!");
                return;
            }

            try
            {
                const string query =
                    "INSERT INTO sales (firstName, lastName, email, phone, password, createdAt, updatedAt, deleted, deletedAt) VALUES (@first_name, @last_name, @email, @phone, @password, @created_at, @updated_at, @deleted, NULL)";
                MySqlConnectionService.SetupQuery(query);
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255))
                    .Value = su.FirstName;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255))
                    .Value = su.LastName;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                    .Value = su.Email;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                    .Value = su.Phone;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 255))
                    .Value = su.Password;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@created_at", SqlDbType.DateTimeOffset))
                    .Value = su.CreatedAt;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset))
                    .Value = su.UpdatedAt;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@deleted", SqlDbType.Bit)).Value =
                    su.Deleted;
                MySqlConnectionService.Command.ExecuteNonQuery();
                LoggerService.NewLog("Created a new commercial user");
                InitSalesUsers();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EditSalesUser(string previousEmail, SalesUserModel su)
        {
            if (su == null) return;
            if (su.FirstName == null && su.LastName == null && su.Email == null && su.Phone == null && su.Deleted)
            {
                MessageBox.Show("Invalid sales user model");
                return;
            }

            if (!previousEmail.Equals(su.Email) && !IsUniqueEmail(su.Email))
            {
                MessageBox.Show("Email already taken!");
                return;
            }

            try
            {
                string query;
                if (!string.IsNullOrWhiteSpace(su.Password))
                {
                    query = "UPDATE sales SET " +
                            "firstName = @first_name, lastName = @last_name, email = @email, phone = @phone, " +
                            "password = @password, updatedAt = @updated_at " +
                            "WHERE id = @id;";
                    MySqlConnectionService.SetupQuery(query);
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 255))
                        .Value = su.Password;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4)).Value =
                        su.Id;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = su.FirstName;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = su.LastName;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                        .Value = su.Email;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                        .Value = su.Phone;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = su.UpdatedAt;
                    MySqlConnectionService.Command.ExecuteNonQuery();
                }
                else
                {
                    query = "UPDATE sales SET " +
                            "firstName = @first_name, lastName = @last_name, email = @email, phone = @phone, " +
                            "updatedAt = @updated_at " +
                            "WHERE id = @id;";
                    MySqlConnectionService.SetupQuery(query);
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = su.Id;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = su.FirstName;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = su.LastName;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                        .Value = su.Email;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                        .Value = su.Phone;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = su.UpdatedAt;
                    MySqlConnectionService.Command.ExecuteNonQuery();
                }
                LoggerService.NewLog($"Commercial user at ID {su.Id} updated");
                InitSalesUsers();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteSalesUser(int id)
        {
            const string query = "UPDATE sales SET deleted = @deleted, deletedAt = @deleted_at WHERE id = @id;";
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
            LoggerService.NewLog($"Commercial user at ID {id} deleted");
            InitSalesUsers();
        }

        private bool IsUniqueEmail(string email)
        {
            const string query = "SELECT COUNT(email) FROM sales WHERE email LIKE @email;";
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