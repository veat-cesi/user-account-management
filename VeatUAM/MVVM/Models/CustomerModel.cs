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
        private DateTimeOffset _createdAt;
        private DateTimeOffset _updatedAt;
        private bool _deleted;

        public CustomerModel()
        {
        }

        public CustomerModel(int id, string firstName, string lastName, string email, string phone, DateTimeOffset createdAt, DateTimeOffset updatedAt, bool deleted)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _phone = phone;
            _createdAt = createdAt;
            _updatedAt = updatedAt;
            _deleted = deleted;
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

        public void ToCustomerModel(Dictionary<string, object> dbCustomer)
        {
            try
            {
                Id = (int) dbCustomer["id"];
                FirstName = dbCustomer["firstName"].ToString();
                LastName = dbCustomer["lastName"].ToString();
                Email = dbCustomer["email"].ToString();
                Phone = dbCustomer["phone"].ToString();
                CreatedAt = DateTimeOffset.Parse(dbCustomer["createdAt"].ToString());
                UpdatedAt = DateTimeOffset.Parse(dbCustomer["updatedAt"].ToString());
                Deleted = (bool) dbCustomer["deleted"];
            }
            catch (Exception e)
            {
                // ignored
            }
        }
        
    }
}