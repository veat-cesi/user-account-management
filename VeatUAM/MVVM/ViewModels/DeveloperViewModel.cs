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
    public class DeveloperViewModel : ObservableObject
    {
        public string head = "Developers";

        public DeveloperViewModel()
        {
            InitDevelopers();
        }

        public IList<DeveloperModel> Developers { get; set; }

        private void InitDevelopers()
        {
            Developers = new List<DeveloperModel>();
            const string query =
                "SELECT id, firstName, lastName, email, phone, createdAt, updatedAt, deleted FROM dev";
            MySqlConnectionService.SetupQuery(query);
            MySqlConnectionService.SetupReader();
            while (MySqlConnectionService.Reader.Read())
                Developers.Add(new DeveloperModel(
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

        public void CreateDeveloper(DeveloperModel d)
        {
            if (d == null) return;
            if (d.FirstName == null && d.LastName == null && d.Email == null && d.Phone == null &&
                d.Password == null && d.Deleted)
            {
                MessageBox.Show("Invalid dev model");
                return;
            }
            
            if (!IsUniqueEmail(d.Email))
            {
                MessageBox.Show("Email already in database!");
                return;
            }
            try
            {
                const string query =
                    "INSERT INTO dev (firstName, lastName, email, phone, password, createdAt, updatedAt, deleted, deletedAt) VALUES (@first_name, @last_name, @email, @phone, @password, @created_at, @updated_at, @deleted, NULL)";
                MySqlConnectionService.SetupQuery(query);
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255))
                    .Value = d.FirstName;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255))
                    .Value = d.LastName;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                    .Value = d.Email;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                    .Value = d.Phone;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 255))
                    .Value = d.Password;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@created_at", SqlDbType.DateTimeOffset))
                    .Value = d.CreatedAt;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset))
                    .Value = d.UpdatedAt;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@deleted", SqlDbType.Bit)).Value =
                    d.Deleted;
                MySqlConnectionService.Command.ExecuteNonQuery();
                LoggerService.NewLog("Created a new developer");
                InitDevelopers();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EditDeveloper(string previousEmail, DeveloperModel d)
        {
            if (d == null) return;
            if (d.FirstName == null && d.LastName == null && d.Email == null && d.Phone == null && d.Deleted)
            {
                MessageBox.Show("Invalid dev model");
                return;
            }
            
            if (!previousEmail.Equals(d.Email) && !IsUniqueEmail(d.Email))
            {
                MessageBox.Show("Email already taken!");
                return;
            }
            
            try
            {
                string query;
                if (!string.IsNullOrWhiteSpace(d.Password))
                {
                    query = "UPDATE dev SET " +
                            "firstName = @first_name, lastName = @last_name, email = @email, phone = @phone, " +
                            "password = @password, updatedAt = @updated_at " +
                            "WHERE id = @id;";
                    MySqlConnectionService.SetupQuery(query);
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 255))
                        .Value = d.Password;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4)).Value =
                        d.Id;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = d.FirstName;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = d.LastName;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                        .Value = d.Email;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                        .Value = d.Phone;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = d.UpdatedAt;
                    MySqlConnectionService.Command.ExecuteNonQuery();
                }
                else
                {
                    query = "UPDATE dev SET " +
                            "firstName = @first_name, lastName = @last_name, email = @email, phone = @phone, " +
                            "updatedAt = @updated_at " +
                            "WHERE id = @id;";
                    MySqlConnectionService.SetupQuery(query);
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = d.Id;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = d.FirstName;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = d.LastName;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                        .Value = d.Email;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                        .Value = d.Phone;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = d.UpdatedAt;
                    MySqlConnectionService.Command.ExecuteNonQuery();
                }
                LoggerService.NewLog($"Developer ID {d.Id} updated");
                InitDevelopers();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteDeveloper(int id)
        {
            const string query = "UPDATE dev SET deleted = @deleted, deletedAt = @deleted_at WHERE id = @id;";
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
            LoggerService.NewLog($"Developer ID {id} deleted");
            InitDevelopers();
        }
        
        private bool IsUniqueEmail(string email)
        {
            const string query = "SELECT COUNT(email) FROM dev WHERE email LIKE @email;";
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