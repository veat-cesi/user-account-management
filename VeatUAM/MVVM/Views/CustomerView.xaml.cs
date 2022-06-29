using System;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VeatUAM._Services;
using VeatUAM.MVVM.Models;
using VeatUAM.MVVM.ViewModels;

namespace VeatUAM.MVVM.Views
{
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            InitializeComponent();
            CreationMode = false;
        }

        public bool CreationMode { get; set; }

        public CustomerModel SelectedCustomer { get; set; }

        private void DataGridRow_OnSelected(object sender, RoutedEventArgs e)
        {
            CreationMode = false;
            if (sender == null) return;
            if (CustomerDataGrid.Items.IndexOf(CustomerDataGrid.CurrentItem) < 0) return;
            SelectedCustomer = (CustomerModel) CustomerDataGrid.CurrentItem;
            InputCustomerId.Text = SelectedCustomer.Id.ToString();
            InputCustomerFirstName.Text = SelectedCustomer.FirstName;
            InputCustomerLastName.Text = SelectedCustomer.LastName;
            InputCustomerEmail.Text = SelectedCustomer.Email;
            InputCustomerPhone.Text = SelectedCustomer.Phone;
            ActionCustomer.Text = $"Customer {SelectedCustomer.Id.ToString()} selected";
            ActionCustomer.Visibility = Visibility.Visible;
            CustomerDeleteButton.Visibility = Visibility.Visible;
            CustomerSubmitButton.Visibility = Visibility.Visible;
        }

        private void ClearCustomerInputs()
        {
            InputCustomerId.Text = "";
            InputCustomerFirstName.Text = "";
            InputCustomerLastName.Text = "";
            InputCustomerEmail.Text = "";
            InputCustomerPhone.Text = "";
        }
        
        private void NewCustomer(object sender, EventArgs e)
        {
            CustomerDataGrid.UnselectAll();
            ClearCustomerInputs();
            CreationMode = true;
            ActionCustomer.Text = "Creating a new customer";
            ActionCustomer.Visibility = Visibility.Visible;
            CustomerDeleteButton.Visibility = Visibility.Hidden;
            CustomerSubmitButton.Visibility = Visibility.Visible;
        }

        public void CustomerSubmit(object sender, RoutedEventArgs routedEventArgs)
        {
            if (CreationMode)
            {
                NewCustomerSubmit();
            }
            else
            {
                EditCustomerSubmit();
            }
        }

        public void CustomerDelete(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = (CustomerViewModel) DataContext;
            viewModel.DeleteCustomer(SelectedCustomer.Id);
            ActionCustomer.Text = $"Customer {SelectedCustomer.Id} deleted";
            RefreshDataGrid();
        }
        
        private void NewCustomerSubmit()
        {
            if (PasswordConfirmation() == false)
            {
                ActionCustomer.Text = "Password do not match";
                throw new Exception("Password do not match");
            }
            var submitedCustomer = new CustomerModel(
                InputCustomerFirstName.Text,
                InputCustomerLastName.Text,
                InputCustomerEmail.Text,
                InputCustomerPhone.Text,
                PasswordEncoderService.Hash(InputCustomerPassword.Password),
                DateTimeOffset.Now,
                DateTimeOffset.Now,
                false,
                null
                );
            var viewModel = (CustomerViewModel) DataContext;
            viewModel.CreateCustomer(submitedCustomer);
            ActionCustomer.Text = "New customer submitted";
            RefreshDataGrid();
        }
        
        private void EditCustomerSubmit()
        {
            CustomerModel submittedCustomer;
            if (!string.IsNullOrWhiteSpace(InputCustomerPassword.Password))
            {
                if (PasswordConfirmation() == false)
                {
                    ActionCustomer.Text = "Password do not match";
                    throw new Exception("Password do not match");
                }
                submittedCustomer = new CustomerModel(
                    id: SelectedCustomer.Id,
                    firstName: InputCustomerFirstName.Text,
                    lastName: InputCustomerLastName.Text,
                    email: InputCustomerEmail.Text,
                    phone: InputCustomerPhone.Text,
                    createdAt: SelectedCustomer.CreatedAt,
                    updatedAt: DateTimeOffset.Now, 
                    password: PasswordEncoderService.Hash(InputCustomerPassword.Password));
            }
            else
            {
                submittedCustomer = new CustomerModel(
                    id: SelectedCustomer.Id,
                    firstName: InputCustomerFirstName.Text,
                    lastName: InputCustomerLastName.Text,
                    email: InputCustomerEmail.Text,
                    phone: InputCustomerPhone.Text,
                    createdAt: SelectedCustomer.CreatedAt,
                    updatedAt: DateTimeOffset.Now
                );
            }
            var viewModel = (CustomerViewModel) DataContext;
            viewModel.EditCustomer(submittedCustomer);
            ActionCustomer.Text = $"Customer {submittedCustomer.Id.ToString()} updated!";
            RefreshDataGrid();
        }

        private bool PasswordConfirmation()
        {
            return InputCustomerPassword.Password == InputCustomerPasswordConfirm.Password;
        }

        private void RefreshDataGrid()
        {
            var viewModel = (CustomerViewModel) DataContext;
            CustomerDataGrid.ItemsSource = null;
            CustomerDataGrid.ItemsSource = viewModel.Customers;
        }
    }
}