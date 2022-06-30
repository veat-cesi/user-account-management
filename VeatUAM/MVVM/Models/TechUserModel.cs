using System;

namespace VeatUAM.MVVM.Models
{
    public class TechUserModel
    {
        public TechUserModel()
        {
        }

        public TechUserModel(int id, string firstName, string lastName, string email, string phone,
            DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, string role, string password = null)
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
            Role = role;
        }

        public TechUserModel(int id, string firstName, string lastName, string email, string phone,
            DateTimeOffset createdAt, DateTimeOffset updatedAt, string role, string password = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Role = role;
        }

        public TechUserModel(string firstName, string lastName, string email, string phone, string password,
            DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, DateTimeOffset? deletedAt, string role)
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
            Role = role;
        }

        public TechUserModel(int id, string firstName, string lastName, string email, string phone, string password,
            DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, DateTimeOffset? deletedAt, string role)
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
            Role = role;
        }

        public int Id { get; set; }
        
        public string Role { get; set; }

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