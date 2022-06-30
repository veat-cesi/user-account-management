using System;
using System.Windows;
using System.Windows.Controls;
using VeatUAM._Services;
using VeatUAM.MVVM.Models;
using VeatUAM.MVVM.ViewModels;

namespace VeatUAM.MVVM.Views
{
    public partial class SalesUserView : UserControl
    {
        public SalesUserView()
        {
            InitializeComponent();
            CreationMode = false;
        }

        public bool CreationMode { get; set; }

        public SalesUserModel SelectedSalesUser { get; set; }

        private void DataGridRow_OnSelected(object sender, RoutedEventArgs e)
        {
            CreationMode = false;
            if (sender == null) return;
            if (SalesUserDataGrid.Items.IndexOf(SalesUserDataGrid.CurrentItem) < 0) return;
            SelectedSalesUser = (SalesUserModel) SalesUserDataGrid.CurrentItem;
            InputSalesUserId.Text = SelectedSalesUser.Id.ToString();
            InputSalesUserFirstName.Text = SelectedSalesUser.FirstName;
            InputSalesUserLastName.Text = SelectedSalesUser.LastName;
            InputSalesUserEmail.Text = SelectedSalesUser.Email;
            InputSalesUserPhone.Text = SelectedSalesUser.Phone;
            ActionSalesUser.Text = $"SalesUser {SelectedSalesUser.Id.ToString()} selected";
            ActionSalesUser.Visibility = Visibility.Visible;
            SalesUserDeleteButton.Visibility = Visibility.Visible;
            SalesUserSubmitButton.Visibility = Visibility.Visible;
        }

        private void ClearSalesUserInputs()
        {
            InputSalesUserId.Text = "";
            InputSalesUserFirstName.Text = "";
            InputSalesUserLastName.Text = "";
            InputSalesUserEmail.Text = "";
            InputSalesUserPhone.Text = "";
        }
        
        private void NewSalesUser(object sender, EventArgs e)
        {
            if (AuthenticationService.Role.Equals("user"))
            {
                PermissionService.Permission(false);
                return;
            }
            SalesUserDataGrid.UnselectAll();
            ClearSalesUserInputs();
            CreationMode = true;
            ActionSalesUser.Text = "Creating a new customer";
            ActionSalesUser.Visibility = Visibility.Visible;
            SalesUserDeleteButton.Visibility = Visibility.Hidden;
            SalesUserSubmitButton.Visibility = Visibility.Visible;
        }

        public void SalesUserSubmit(object sender, RoutedEventArgs routedEventArgs)
        {
            if (AuthenticationService.Role.Equals("user"))
            {
                PermissionService.Permission(false);
                return;
            }
            if (CreationMode)
            {
                NewSalesUserSubmit();
            }
            else
            {
                EditSalesUserSubmit();
            }
        }

        public void SalesUserDelete(object sender, RoutedEventArgs routedEventArgs)
        {
            if (AuthenticationService.Role.Equals("user"))
            {
                PermissionService.Permission(false);
                return;
            }
            var viewModel = (SalesUserViewModel) DataContext;
            viewModel.DeleteSalesUser(SelectedSalesUser.Id);
            ActionSalesUser.Text = $"SalesUser {SelectedSalesUser.Id} deleted";
            RefreshDataGrid();
        }
        
        private void NewSalesUserSubmit()
        {
            if (string.IsNullOrWhiteSpace(InputSalesUserPassword.Password))
            {
                MessageBox.Show("Password input is empty!");
                return;
            }

            if (PasswordConfirmation() == false)
            {
                ActionSalesUser.Text = "Password do not match";
                MessageBox.Show("Password do not match");
                return;
            }
            var submittedSalesUser = new SalesUserModel(
                firstName:InputSalesUserFirstName.Text,
                lastName:InputSalesUserLastName.Text,
                email:InputSalesUserEmail.Text,
                phone:InputSalesUserPhone.Text,
                password:PasswordEncoderService.Hash(InputSalesUserPassword.Password),
                createdAt:DateTimeOffset.Now,
                updatedAt:DateTimeOffset.Now,
                deleted:false,
                deletedAt:null
                );
            var viewModel = (SalesUserViewModel) DataContext;
            viewModel.CreateSalesUser(submittedSalesUser);
            ActionSalesUser.Text = "New customer submitted";
            RefreshDataGrid();
        }
        
        private void EditSalesUserSubmit()
        {
            SalesUserModel submittedSalesUser;
            if (!string.IsNullOrWhiteSpace(InputSalesUserPassword.Password))
            {
                if (PasswordConfirmation() == false)
                {
                    ActionSalesUser.Text = "Password do not match";
                    MessageBox.Show("Password do not match");
                    return;
                }
                submittedSalesUser = new SalesUserModel(
                    id: SelectedSalesUser.Id,
                    firstName: InputSalesUserFirstName.Text,
                    lastName: InputSalesUserLastName.Text,
                    email: InputSalesUserEmail.Text,
                    phone: InputSalesUserPhone.Text,
                    createdAt: SelectedSalesUser.CreatedAt,
                    updatedAt: DateTimeOffset.Now, 
                    password: PasswordEncoderService.Hash(InputSalesUserPassword.Password));
            }
            else
            {
                submittedSalesUser = new SalesUserModel(
                    id: SelectedSalesUser.Id,
                    firstName: InputSalesUserFirstName.Text,
                    lastName: InputSalesUserLastName.Text,
                    email: InputSalesUserEmail.Text,
                    phone: InputSalesUserPhone.Text,
                    createdAt: SelectedSalesUser.CreatedAt,
                    updatedAt: DateTimeOffset.Now
                );
            }
            var viewModel = (SalesUserViewModel) DataContext;
            viewModel.EditSalesUser(submittedSalesUser);
            ActionSalesUser.Text = $"SalesUser {submittedSalesUser.Id.ToString()} updated!";
            RefreshDataGrid();
        }

        private bool PasswordConfirmation()
        {
            return InputSalesUserPassword.Password == InputSalesUserPasswordConfirm.Password;
        }

        private void RefreshDataGrid()
        {
            var viewModel = (SalesUserViewModel) DataContext;
            SalesUserDataGrid.ItemsSource = null;
            SalesUserDataGrid.ItemsSource = viewModel.SalesUsers;
        }
    }
}