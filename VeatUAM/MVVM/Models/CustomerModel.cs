using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VeatUAM.Core;

namespace VeatUAM.MVVM.Model
{
    public class CustomerModel
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _password;
        private DateTimeOffset _createdAt;
        private DateTimeOffset _updatedAt;
        private bool _deleted;
        private DateTimeOffset? _deletedAt;

        public CustomerModel()
        {
        }

        public CustomerModel(string firstName, string lastName, string email, string phone, string password,
            DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted, DateTimeOffset? deletedAt)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phone = phone;
            _password = password;
            _createdAt = createdAt;
            _updatedAt = updatedAt;
            _deleted = deleted;
            _deletedAt = deletedAt;
        }

        public CustomerModel(string firstName, string lastName, string email, string phone, DateTimeOffset createdAt,
            DateTimeOffset updatedAt, bool deleted, DateTimeOffset? deletedAt)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phone = phone;
            _createdAt = createdAt;
            _updatedAt = updatedAt;
            _deleted = deleted;
            _deletedAt = deletedAt;
        }

        public CustomerModel(int id, string firstName, string lastName, string email, string phone, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phone = phone;
            _password = null;
            _createdAt = createdAt;
            _updatedAt = updatedAt;
            _deleted = deleted;
            _deletedAt = null;
        }

        public CustomerModel(int id, string firstName, string lastName, string email, string phone,
            DateTimeOffset createdAt,
            DateTimeOffset updatedAt, string password = null)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phone = phone;
            _updatedAt = updatedAt;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public DateTimeOffset CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }

        public DateTimeOffset UpdatedAt
        {
            get => _updatedAt;
            set => _updatedAt = value;
        }

        public bool Deleted
        {
            get => _deleted;
            set => _deleted = value;
        }

        public DateTimeOffset? DeletedAt
        {
            get => _deletedAt;
            set => _deletedAt = value;
        }

        public void SetCustomer(CustomerModel c)
        {
            FirstName = c.FirstName;
            LastName = c.LastName;
            Email = c.Email;
            Phone = c.Phone;
            CreatedAt = c.CreatedAt;
            UpdatedAt = c.UpdatedAt;
            Deleted = c.Deleted;
        }
    }
}