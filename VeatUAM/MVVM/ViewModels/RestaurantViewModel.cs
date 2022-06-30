using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using VeatUAM.Core;
using VeatUAM.MVVM.Models;

namespace VeatUAM.MVVM.ViewModels
{
    public class RestaurantViewModel
    {
        public string head = "Restaurants";

        public RestaurantViewModel()
        {
            InitRestaurants();
        }

        public IList<RestaurantModel> Restaurants { get; set; }

        private void InitRestaurants()
        {
            Restaurants = new List<RestaurantModel>();
            const string query =
                "SELECT id, name, email, phone, createdAt, updatedAt, deleted FROM restaurant";
            MySqlConnectionService.SetupQuery(query);
            MySqlConnectionService.SetupReader();
            while (MySqlConnectionService.Reader.Read())
                Restaurants.Add(new RestaurantModel(
                    id:MySqlConnectionService.Reader.GetInt32(0),
                    name:MySqlConnectionService.Reader.GetString(1),
                    email:MySqlConnectionService.Reader.GetString(2),
                    phone:MySqlConnectionService.Reader.GetString(3),
                    createdAt:MySqlConnectionService.Reader.GetDateTimeOffset(4),
                    updatedAt:MySqlConnectionService.Reader.GetDateTimeOffset(5),
                    deleted:MySqlConnectionService.Reader.GetBoolean(6)
                ));
            MySqlConnectionService.Reader.Close();
        }

        public void CreateRestaurant(RestaurantModel r)
        {
            if (r == null) return;
            if (r.Name == null && r.Email == null && r.Phone == null &&
                r.Password == null && r.Deleted)
            {
                MessageBox.Show("Invalid restaurant model");
                return;
            }
            
            if (!IsUniqueEmail(r.Email))
            {
                MessageBox.Show("Email already in database!");
                return;
            }
            try
            {
                const string query =
                    "INSERT INTO restaurant (name, email, phone, password, createdAt, updatedAt, deleted, deletedAt) VALUES (@name, @email, @phone, @password, @created_at, @updated_at, @deleted, NULL)";
                MySqlConnectionService.SetupQuery(query);
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar, 255))
                    .Value = r.Name;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                    .Value = r.Email;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                    .Value = r.Phone;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 255))
                    .Value = r.Password;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@created_at", SqlDbType.DateTimeOffset))
                    .Value = r.CreatedAt;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset))
                    .Value = r.UpdatedAt;
                MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@deleted", SqlDbType.Bit)).Value =
                    r.Deleted;
                MySqlConnectionService.Command.ExecuteNonQuery();
                InitRestaurants();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EditRestaurant(RestaurantModel r)
        {
            if (r == null) return;
            if (r.Name == null && r.Email == null && r.Phone == null && r.Deleted)
            {
                MessageBox.Show("Invalid restaurant model");
                return;
            }
            
            if (!IsUniqueEmail(r.Email))
            {
                MessageBox.Show("Email already in database!");
                return;
            }
            try
            {
                string query;
                if (!string.IsNullOrWhiteSpace(r.Password))
                {
                    query = "UPDATE restaurant SET " +
                            "name = @name, email = @email, phone = @phone, " +
                            "password = @password, updatedAt = @updated_at " +
                            "WHERE id = @id;";
                    MySqlConnectionService.SetupQuery(query);
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 255))
                        .Value = r.Password;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4)).Value =
                        r.Id;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@name", SqlDbType.VarChar, 255)).Value = r.Name;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                        .Value = r.Email;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                        .Value = r.Phone;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = r.UpdatedAt;
                    MySqlConnectionService.Command.ExecuteNonQuery();
                }
                else
                {
                    query = "UPDATE restaurant SET " +
                            "name = @name, email = @email, phone = @phone, " +
                            "updatedAt = @updated_at " +
                            "WHERE id = @id;";
                    MySqlConnectionService.SetupQuery(query);
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = r.Id;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@name", SqlDbType.VarChar, 255)).Value = r.Name;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar, 255))
                        .Value = r.Email;
                    MySqlConnectionService.Command.Parameters.Add(new SqlParameter("@phone", SqlDbType.VarChar, 255))
                        .Value = r.Phone;
                    MySqlConnectionService.Command.Parameters
                        .Add(new SqlParameter("@updated_at", SqlDbType.DateTimeOffset)).Value = r.UpdatedAt;
                    MySqlConnectionService.Command.ExecuteNonQuery();
                }

                InitRestaurants();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteRestaurant(int id)
        {
            const string query = "UPDATE restaurant SET deleted = @deleted, deletedAt = @deleted_at WHERE id = @id;";
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

            InitRestaurants();
        }
        
        private bool IsUniqueEmail(string email)
        {
            const string query = "SELECT COUNT(email) FROM restaurant WHERE email LIKE @email;";
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