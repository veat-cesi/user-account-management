using System;
using System.Windows;
using System.Windows.Controls;
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

        private void CustomerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CreationMode = false;
            CustomerDataGrid = (DataGrid) sender;
            CustomerModel row = (CustomerModel)CustomerDataGrid.SelectedItems[0];
            if (row == null) return;
            InputCustomerId.Text = row.Id.ToString();
            InputCustomerFirstName.Text = row.FirstName;
            InputCustomerLastName.Text = row.LastName;
            InputCustomerEmail.Text = row.Email;
            InputCustomerPhone.Text = row.Phone;
            ActionCustomer.Text = $"Customer {row.Id.ToString()} selected";
            ActionCustomer.Visibility = Visibility.Visible;
            CustomerDeleteButton.Visibility = Visibility.Visible;
        }

        private void ClearCustomerInputs()
        {
            InputCustomerId.Text = "";
            InputCustomerFirstName.Text = "";
            InputCustomerLastName.Text = "";
            InputCustomerEmail.Text = "";
            InputCustomerPhone.Text = "";
            CustomerDeleteButton.Visibility = Visibility.Hidden;
        }
        
        private void NewCustomer(object sender, EventArgs e)
        {
            ClearCustomerInputs();
            CreationMode = true;
            ActionCustomer.Text = "Creating a new customer";
            ActionCustomer.Visibility = Visibility.Visible;
        }
    }
}