using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VeatUAM.Core;

namespace VeatUAM.MVVM.Model
{
    public class CustomerModel
    {
        public CustomerModel()
        {
        }

        public CustomerModel(string firstName, string lastName, string email, string phone, string password,
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

        public CustomerModel(string firstName, string lastName, string email, string phone, DateTimeOffset createdAt,
            DateTimeOffset updatedAt, bool deleted, DateTimeOffset? deletedAt)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Deleted = deleted;
            DeletedAt = deletedAt;
        }

        public CustomerModel(int id, string firstName, string lastName, string email, string phone, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Password = null;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Deleted = deleted;
            DeletedAt = null;
        }

        public CustomerModel(int id, string firstName, string lastName, string email, string phone,
            DateTimeOffset createdAt,
            DateTimeOffset updatedAt, string password = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            UpdatedAt = updatedAt;
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