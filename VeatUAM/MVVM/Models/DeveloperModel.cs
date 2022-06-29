using System;

namespace VeatUAM.MVVM.Models
{
    public class DeveloperModel
    {
        public DeveloperModel()
        {
        }

        public DeveloperModel(int id, string firstName, string lastName, string email, string phone, string password,
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

        public DeveloperModel(string firstName, string lastName, string email, string phone, 
            DateTimeOffset createdAt, DateTimeOffset updatedAt, string password = null,
            bool deleted = false, DateTimeOffset? deletedAt = null)
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

        public DeveloperModel(int id, string firstName, string lastName, string email, string phone,
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

        public DeveloperModel(int id, string firstName, string lastName, string email, string phone, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Deleted = deleted;
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