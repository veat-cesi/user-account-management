using System;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VeatUAM.MVVM.Model;

namespace VeatUAM.MVVM.View
{
    public partial class CustomerView : UserControl
    {
        private bool _creationMode;
        
        public CustomerView()
        {
            InitializeComponent();
            _creationMode = false;
        }

        public bool CreationMode
        {
            get => _creationMode;
            set => _creationMode = value;
        }

        private void DataGridRow_OnSelected(object sender, RoutedEventArgs e)
        {
            CreationMode = false;
            if (sender == null) return;
            if (CustomerDataGrid.Items.IndexOf(CustomerDataGrid.CurrentItem) < 0) return;
            var rowModel = (CustomerModel) CustomerDataGrid.CurrentItem;
            InputCustomerId.Text = rowModel.Id.ToString();
            InputCustomerFirstName.Text = rowModel.FirstName;
            InputCustomerLastName.Text = rowModel.LastName;
            InputCustomerEmail.Text = rowModel.Email;
            InputCustomerPhone.Text = rowModel.Phone;
            ActionCustomer.Text = $"Customer {rowModel.Id.ToString()} selected";
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
    }
}