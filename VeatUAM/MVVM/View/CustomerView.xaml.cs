using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using VeatUAM.MVVM.Model;

namespace VeatUAM.MVVM.View
{
    public partial class CustomerView : UserControl
    {
        public CustomerView()
        {
            InitializeComponent();
        }
        
        private void CustomerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomerDataGrid = (DataGrid) sender;
            CustomerModel row = (CustomerModel)CustomerDataGrid.SelectedItems[0];
            if (row == null) return;
            InputCustomerId.Text = row.Id.ToString();
            InputCustomerFirstName.Text = row.FirstName;
            InputCustomerLastName.Text = row.LastName;
            InputCustomerEmail.Text = row.Email;
            InputCustomerPhone.Text = row.Phone;
            CustomerDeleteButton.Visibility = Visibility.Visible;
        }
    }
}