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
                CreatedAt = DateTime.Parse( dbCustomer["createdAt"].ToString());
                UpdatedAt = DateTime.Parse(dbCustomer["updatedAt"].ToString());
                Deleted = (bool) dbCustomer["deleted"];
            }
            catch (Exception e)
            {
                // ignored
            }
        }
        
    }
}