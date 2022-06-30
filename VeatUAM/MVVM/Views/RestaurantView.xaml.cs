using System;
using System.Windows;
using System.Windows.Controls;
using VeatUAM._Services;
using VeatUAM.MVVM.Models;
using VeatUAM.MVVM.ViewModels;

namespace VeatUAM.MVVM.Views
{
    public partial class RestaurantView : UserControl
    {
        public RestaurantView()
        {
            InitializeComponent();
            CreationMode = false;
        }

        public bool CreationMode { get; set; }

        public RestaurantModel SelectedRestaurant { get; set; }

        private void DataGridRow_OnSelected(object sender, RoutedEventArgs e)
        {
            CreationMode = false;
            if (sender == null) return;
            if (RestaurantDataGrid.Items.IndexOf(RestaurantDataGrid.CurrentItem) < 0) return;
            SelectedRestaurant = (RestaurantModel) RestaurantDataGrid.CurrentItem;
            InputRestaurantId.Text = SelectedRestaurant.Id.ToString();
            InputRestaurantName.Text = SelectedRestaurant.Name;
            InputRestaurantEmail.Text = SelectedRestaurant.Email;
            InputRestaurantPhone.Text = SelectedRestaurant.Phone;
            ActionRestaurant.Text = $"Restaurant {SelectedRestaurant.Id.ToString()} selected";
            ActionRestaurant.Visibility = Visibility.Visible;
            RestaurantDeleteButton.Visibility = Visibility.Visible;
            RestaurantSubmitButton.Visibility = Visibility.Visible;
        }

        private void ClearRestaurantInputs()
        {
            InputRestaurantId.Text = "";
            InputRestaurantName.Text = "";
            InputRestaurantEmail.Text = "";
            InputRestaurantPhone.Text = "";
            InputRestaurantPassword.Password = "";
            InputRestaurantPasswordConfirm.Password = "";
        }
        
        private void NewRestaurant(object sender, EventArgs e)
        {
            if (AuthenticationService.Role.Equals("user"))
            {
                PermissionService.Permission(false);
                return;
            }
            RestaurantDataGrid.UnselectAll();
            ClearRestaurantInputs();
            CreationMode = true;
            ActionRestaurant.Text = "Creating a new customer";
            ActionRestaurant.Visibility = Visibility.Visible;
            RestaurantDeleteButton.Visibility = Visibility.Hidden;
            RestaurantSubmitButton.Visibility = Visibility.Visible;
        }

        public void RestaurantSubmit(object sender, RoutedEventArgs routedEventArgs)
        {
            if (AuthenticationService.Role.Equals("user"))
            {
                PermissionService.Permission(false);
                return;
            }
            if (CreationMode)
            {
                NewRestaurantSubmit();
            }
            else
            {
                EditRestaurantSubmit();
            }
        }

        public void RestaurantDelete(object sender, RoutedEventArgs routedEventArgs)
        {
            if (AuthenticationService.Role.Equals("user"))
            {
                PermissionService.Permission(false);
                return;
            }
            var viewModel = (RestaurantViewModel) DataContext;
            viewModel.DeleteRestaurant(SelectedRestaurant.Id);
            ActionRestaurant.Text = $"Restaurant {SelectedRestaurant.Id} deleted";
            RefreshDataGrid();
        }
        
        private void NewRestaurantSubmit()
        {
            if (string.IsNullOrWhiteSpace(InputRestaurantPassword.Password))
            {
                MessageBox.Show("Password input is empty!");
                return;
            }

            if (PasswordConfirmation() == false)
            {
                ActionRestaurant.Text = "Password do not match";
                MessageBox.Show("Password do not match");
                return;
            }
            var submitedRestaurant = new RestaurantModel(
                name:InputRestaurantName.Text,
                email:InputRestaurantEmail.Text,
                phone:InputRestaurantPhone.Text,
                password:PasswordEncoderService.Hash(InputRestaurantPassword.Password),
                createdAt:DateTimeOffset.Now,
                updatedAt:DateTimeOffset.Now,
                deleted:false,
                deletedAt:null
                );
            var viewModel = (RestaurantViewModel) DataContext;
            viewModel.CreateRestaurant(submitedRestaurant);
            ActionRestaurant.Text = "New customer submitted";
            RefreshDataGrid();
        }
        
        private void EditRestaurantSubmit()
        {
            RestaurantModel submittedRestaurant;
            if (!string.IsNullOrWhiteSpace(InputRestaurantPassword.Password))
            {
                if (PasswordConfirmation() == false)
                {
                    ActionRestaurant.Text = "Password do not match";
                    MessageBox.Show("Password do not match");
                    return;
                }
                submittedRestaurant = new RestaurantModel(
                    id: SelectedRestaurant.Id,
                    name:InputRestaurantName.Text,
                    email: InputRestaurantEmail.Text,
                    phone: InputRestaurantPhone.Text,
                    createdAt: SelectedRestaurant.CreatedAt,
                    updatedAt: DateTimeOffset.Now, 
                    password: PasswordEncoderService.Hash(InputRestaurantPassword.Password));
            }
            else
            {
                submittedRestaurant = new RestaurantModel(
                    id: SelectedRestaurant.Id,
                    name:InputRestaurantName.Text,
                    email: InputRestaurantEmail.Text,
                    phone: InputRestaurantPhone.Text,
                    createdAt: SelectedRestaurant.CreatedAt,
                    updatedAt: DateTimeOffset.Now
                );
            }
            var viewModel = (RestaurantViewModel) DataContext;
            viewModel.EditRestaurant(submittedRestaurant);
            ActionRestaurant.Text = $"Restaurant {submittedRestaurant.Id.ToString()} updated!";
            RefreshDataGrid();
        }

        private bool PasswordConfirmation()
        {
            return InputRestaurantPassword.Password == InputRestaurantPasswordConfirm.Password;
        }

        private void RefreshDataGrid()
        {
            var viewModel = (RestaurantViewModel) DataContext;
            RestaurantDataGrid.ItemsSource = null;
            RestaurantDataGrid.ItemsSource = viewModel.Restaurants;
        }
    }
}