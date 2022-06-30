using System;

namespace VeatUAM.MVVM.Models
{
    public class RestaurantModel
    {
        public RestaurantModel()
        {
        }

        public RestaurantModel(int id, string name, string email, string phone, string password,
            DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, DateTimeOffset? deletedAt)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Deleted = deleted;
            DeletedAt = deletedAt;
        }

        public RestaurantModel(string name, string email, string phone, string password, DateTimeOffset createdAt,
            DateTimeOffset updatedAt, bool deleted, DateTimeOffset? deletedAt)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Deleted = deleted;
            DeletedAt = deletedAt;
        }

        public RestaurantModel(int id, string name, string email, string phone, DateTimeOffset createdAt,
            DateTimeOffset updatedAt, string password = null, bool deleted = false)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Deleted = deleted;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }
        
        public DateTimeOffset CreatedAt { get; set; }
        
        public DateTimeOffset UpdatedAt { get; set; }
        
        public bool Deleted { get; set; }
        
        public DateTimeOffset? DeletedAt { get; set; }
    }
}