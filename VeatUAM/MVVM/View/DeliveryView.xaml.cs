using System;
using System.Data;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VeatUAM.MVVM.Model;

namespace VeatUAM.MVVM.View
{
    public partial class DeliveryView : UserControl
    {
        public DeliveryView()
        {
            InitializeComponent();
        }

        private void DeliveryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeliveryDataGrid = (DataGrid) sender;
            DeliveryModel row = (DeliveryModel)DeliveryDataGrid.SelectedItems[0];
            if (row == null) return;
            InputDeliveryId.Text = row.Id.ToString();
            InputDeliveryFirstName.Text = row.FirstName;
            InputDeliveryLastName.Text = row.LastName;
            InputDeliveryEmail.Text = row.Email;
            InputDeliveryPhone.Text = row.Phone;
            DeliveryDeleteButton.Visibility = Visibility.Visible;
        }
    }
}