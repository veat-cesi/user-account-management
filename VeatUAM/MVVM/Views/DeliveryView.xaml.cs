using System;
using System.Data;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VeatUAM._Services;
using VeatUAM.MVVM.Models;
using VeatUAM.MVVM.ViewModels;

namespace VeatUAM.MVVM.Views
{
    public partial class DeliveryView : UserControl
    {
        public DeliveryView()
        {
            InitializeComponent();
            CreationMode = false;
        }

        public bool CreationMode { get; set; }

        public DeliveryModel SelectedDelivery { get; set; }

        private void DataGridRow_OnSelected(object sender, RoutedEventArgs e)
        {
            CreationMode = false;
            if (sender == null) return;
            if (DeliveryDataGrid.Items.IndexOf(DeliveryDataGrid.CurrentItem) < 0) return;
            SelectedDelivery = (DeliveryModel) DeliveryDataGrid.CurrentItem;
            InputDeliveryId.Text = SelectedDelivery.Id.ToString();
            InputDeliveryFirstName.Text = SelectedDelivery.FirstName;
            InputDeliveryLastName.Text = SelectedDelivery.LastName;
            InputDeliveryEmail.Text = SelectedDelivery.Email;
            InputDeliveryPhone.Text = SelectedDelivery.Phone;
            ActionDelivery.Text = $"Delivery {SelectedDelivery.Id.ToString()} selected";
            ActionDelivery.Visibility = Visibility.Visible;
            DeliveryDeleteButton.Visibility = Visibility.Visible;
            DeliverySubmitButton.Visibility = Visibility.Visible;
        }

        private void ClearDeliveryInputs()
        {
            InputDeliveryId.Text = "";
            InputDeliveryFirstName.Text = "";
            InputDeliveryLastName.Text = "";
            InputDeliveryEmail.Text = "";
            InputDeliveryPhone.Text = "";
            InputDeliveryPassword.Password = "";
            InputDeliveryPasswordConfirm.Password = "";
        }
        
        private void NewDelivery(object sender, EventArgs e)
        {
            if (AuthenticationService.Role.Equals("user"))
            {
                PermissionService.Permission(false);
                return;
            }
            DeliveryDataGrid.UnselectAll();
            ClearDeliveryInputs();
            CreationMode = true;
            ActionDelivery.Text = "Creating a new customer";
            ActionDelivery.Visibility = Visibility.Visible;
            DeliveryDeleteButton.Visibility = Visibility.Hidden;
            DeliverySubmitButton.Visibility = Visibility.Visible;
        }

        public void DeliverySubmit(object sender, RoutedEventArgs routedEventArgs)
        {
            if (AuthenticationService.Role.Equals("user"))
            {
                PermissionService.Permission(false);
                return;
            }
            if (CreationMode)
            {
                NewDeliverySubmit();
            }
            else
            {
                EditDeliverySubmit();
            }
        }

        public void DeliveryDelete(object sender, RoutedEventArgs routedEventArgs)
        {
            if (AuthenticationService.Role.Equals("user"))
            {
                PermissionService.Permission(false);
                return;
            }
            var viewModel = (DeliveryViewModel) DataContext;
            viewModel.DeleteDelivery(SelectedDelivery.Id);
            ActionDelivery.Text = $"Delivery {SelectedDelivery.Id} deleted";
            RefreshDataGrid();
        }
        
        private void NewDeliverySubmit()
        {
            if (string.IsNullOrWhiteSpace(InputDeliveryPassword.Password))
            {
                MessageBox.Show("Password input is empty!");
                return;
            }

            if (PasswordConfirmation() == false)
            {
                ActionDelivery.Text = "Password do not match";
                MessageBox.Show("Password do not match");
                return;
            }
            var submitedDelivery = new DeliveryModel(
                InputDeliveryFirstName.Text,
                InputDeliveryLastName.Text,
                InputDeliveryEmail.Text,
                InputDeliveryPhone.Text,
                PasswordEncoderService.Hash(InputDeliveryPassword.Password),
                DateTimeOffset.Now,
                DateTimeOffset.Now,
                false,
                null
                );
            var viewModel = (DeliveryViewModel) DataContext;
            viewModel.CreateDelivery(submitedDelivery);
            ActionDelivery.Text = "New customer submitted";
            RefreshDataGrid();
        }
        
        private void EditDeliverySubmit()
        {
            DeliveryModel submittedDelivery;
            if (!string.IsNullOrWhiteSpace(InputDeliveryPassword.Password))
            {
                if (PasswordConfirmation() == false)
                {
                    ActionDelivery.Text = "Password do not match";
                    MessageBox.Show("Password do not match");
                    return;
                }
                submittedDelivery = new DeliveryModel(
                    id: SelectedDelivery.Id,
                    firstName: InputDeliveryFirstName.Text,
                    lastName: InputDeliveryLastName.Text,
                    email: InputDeliveryEmail.Text,
                    phone: InputDeliveryPhone.Text,
                    createdAt: SelectedDelivery.CreatedAt,
                    updatedAt: DateTimeOffset.Now, 
                    password: PasswordEncoderService.Hash(InputDeliveryPassword.Password));
            }
            else
            {
                submittedDelivery = new DeliveryModel(
                    id: SelectedDelivery.Id,
                    firstName: InputDeliveryFirstName.Text,
                    lastName: InputDeliveryLastName.Text,
                    email: InputDeliveryEmail.Text,
                    phone: InputDeliveryPhone.Text,
                    createdAt: SelectedDelivery.CreatedAt,
                    updatedAt: DateTimeOffset.Now
                );
            }
            var viewModel = (DeliveryViewModel) DataContext;
            viewModel.EditDelivery(SelectedDelivery.Email, submittedDelivery);
            ActionDelivery.Text = $"Delivery {submittedDelivery.Id.ToString()} updated!";
            RefreshDataGrid();
        }

        private bool PasswordConfirmation()
        {
            return InputDeliveryPassword.Password == InputDeliveryPasswordConfirm.Password;
        }

        private void RefreshDataGrid()
        {
            var viewModel = (DeliveryViewModel) DataContext;
            DeliveryDataGrid.ItemsSource = null;
            DeliveryDataGrid.ItemsSource = viewModel.Deliveries;
        }
    }
}