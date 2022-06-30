using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using VeatUAM.Core;
using VeatUAM.MVVM.Models;

namespace VeatUAM.MVVM.ViewModels
{
    public class TechUserViewModel : ObservableObject
    {
        public string head = "Sales Service";

        public TechUserViewModel()
        {
            Roles = new List<string>();
            Roles.Add("user");
            Roles.Add("admin");
            Roles.Add("superadmin");
            InitTechUsers();
        }

        public IList<TechUserModel> TechUsers { get; set; }
        public IList<string> Roles { get; set; }
        
        private void InitTechUsers()
        {
            TechUsers = new List<TechUserModel>();
            const string query =
                "SELECT id, role, firstName, lastName, email, phone, createdAt, updatedAt, deleted FROM tech";
            MySqlConnectionService.SetupQuery(query);
            MySqlConnectionService.SetupReader();
            while (MySqlConnectionService.Reader.Read())
                TechUsers.Add(new TechUserModel(
                    id:MySqlConnectionService.Reader.GetInt32(0),
                    role:MySqlConnectionService.Reader.GetString(1),
                    firstName:MySqlConnectionService.Reader.GetString(2),
                    lastName:MySqlConnectionService.Reader.GetString(3),
                    email:MySqlConnectionService.Reader.GetString(4),
                    phone:MySqlConnectionService.Reader.GetString(5),
                    createdAt:MySqlConnectionService.Reader.GetDateTimeOffset(6),
                    updatedAt:MySqlConnectionService.Reader.GetDateTimeOffset(7),
                    deleted:MySqlConnectionService.Reader.GetBoolean(8)
                ));
            MySqlConnectionService.Reader.Close();
        }

        public void CreateTechUser(TechUserModel tu)
        {
            if (tu == null) return;
            if (tu.Role == null && tu.FirstName == null && tu.LastName == null && tu.Email == null && tu.Phone == null &&
                tu.Password == null && tu.Deleted)
            {
                MessageBox.Show("Invalid tech model");
                return;
            }

            if (!IsUniqueEmail(tu.Email))
            {
                MessageBox.Show("Email already in database!");
                return;
            }

            try
            {
                const string query =
                    "INSERT INTO tech (role, firstName, lastName, email, phone, password, createdAt, updatedAt, deleted, deletedAt) VALUES (@role, @first_name, @last_name, @email, @phone, @password, @created_at, @updated_at, @deleted, NULL)";
                MySqlConnectionService.SetupQuery(query);
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@role", SqlDbType.VarChar, 255))
                    .Value = tu.Role;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255))
                    .Value = tu.FirstName;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255))
                    .Value = tu.LastName;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                    .Value = tu.Email;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                    .Value = tu.Phone;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 255))
                    .Value = tu.Password;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@created_at", SqlDbType.DateTimeOffset))
                    .Value = tu.CreatedAt;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset))
                    .Value = tu.UpdatedAt;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@deleted", SqlDbType.Bit)).Value =
                    tu.Deleted;
                MySqlConnectionService.Command.ExecuteNonQuery();
                InitTechUsers();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EditTechUser(TechUserModel tu)
        {
            if (tu == null) return;
            if (tu.Role == null && tu.FirstName == null && tu.LastName == null && tu.Email == null && tu.Phone == null && tu.Deleted)
            {
                MessageBox.Show("Invalid tech user model");
                return;
            }

            if (!IsUniqueEmail(tu.Email))
            {
                MessageBox.Show("Email already in database!");
                return;
            }

            try
            {
                string query;
                if (!string.IsNullOrWhiteSpace(tu.Password))
                {
                    query = "UPDATE tech SET " +
                            "role = @role, firstName = @first_name, lastName = @last_name, email = @email, " +
                            "phone = @phone, password = @password, updatedAt = @updated_at " +
                            "WHERE id = @id;";
                    MySqlConnectionService.SetupQuery(query);
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@role", SqlDbType.VarChar, 255))
                        .Value = tu.Role;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 255))
                        .Value = tu.Password;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4)).Value =
                        tu.Id;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = tu.FirstName;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = tu.LastName;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                        .Value = tu.Email;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                        .Value = tu.Phone;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = tu.UpdatedAt;
                    MySqlConnectionService.Command.ExecuteNonQuery();
                }
                else
                {
                    query = "UPDATE tech SET " +
                            "role = @role, firstName = @first_name, lastName = @last_name, email = @email," +
                            " phone = @phone, updatedAt = @updated_at " +
                            "WHERE id = @id;";
                    MySqlConnectionService.SetupQuery(query);
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@role", SqlDbType.VarChar, 255))
                        .Value = tu.Role;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = tu.Id;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@first_name", SqlDbType.VarChar, 255)).Value = tu.FirstName;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@last_name", SqlDbType.VarChar, 255)).Value = tu.LastName;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                        .Value = tu.Email;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                        .Value = tu.Phone;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = tu.UpdatedAt;
                    MySqlConnectionService.Command.ExecuteNonQuery();
                }

                InitTechUsers();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteTechUser(int id)
        {
            const string query = "UPDATE tech SET deleted = @deleted, deletedAt = @deleted_at WHERE id = @id;";
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

            InitTechUsers();
        }

        private bool IsUniqueEmail(string email)
        {
            const string query = "SELECT COUNT(email) FROM tech WHERE email LIKE @email;";
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