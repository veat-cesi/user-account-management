using System;
using System.ComponentModel;

namespace VeatUAM.MVVM.Models
{
    public class DeliveryModel
    {
        public DeliveryModel()
        {
        }

        public DeliveryModel(int id, string firstName, string lastName, string email, string phone,
            DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, string password = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Deleted = deleted;
        }

        public DeliveryModel(int id, string firstName, string lastName, string email, string phone,
            DateTimeOffset createdAt, DateTimeOffset updatedAt, string password = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public DeliveryModel(string firstName, string lastName, string email, string phone, string password,
            DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, DateTimeOffset? deletedAt)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Deleted = deleted;
            DeletedAt = deletedAt;
        }

        public DeliveryModel(int id, string firstName, string lastName, string email, string phone, string password,
            DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, DateTimeOffset? deletedAt)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Deleted = deleted;
            DeletedAt = deletedAt;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }
        
        public DateTimeOffset CreatedAt { get; set; }
        
        public DateTimeOffset UpdatedAt { get; set; }
        
        public bool Deleted { get; set; }
        
        public DateTimeOffset? DeletedAt { get; set; }
    }
}