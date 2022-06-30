using System;
using System.Windows;
using System.Windows.Controls;
using VeatUAM._Services;
using VeatUAM.MVVM.Models;
using VeatUAM.MVVM.ViewModels;

namespace VeatUAM.MVVM.Views
{
    public partial class TechUserView : UserControl
    {
        public TechUserView()
        {
            InitializeComponent();
            CreationMode = false;
        }

        public bool CreationMode { get; set; }

        public TechUserModel SelectedTechUser { get; set; }

        private void DataGridRow_OnSelected(object sender, RoutedEventArgs e)
        {
            CreationMode = false;
            if (sender == null) return;
            if (TechUserDataGrid.Items.IndexOf(TechUserDataGrid.CurrentItem) < 0) return;
            SelectedTechUser = (TechUserModel) TechUserDataGrid.CurrentItem;
            InputTechUserId.Text = SelectedTechUser.Id.ToString();
            InputTechUserFirstName.Text = SelectedTechUser.FirstName;
            InputTechUserLastName.Text = SelectedTechUser.LastName;
            InputTechUserEmail.Text = SelectedTechUser.Email;
            InputTechUserPhone.Text = SelectedTechUser.Phone;
            InputTechUserRole.SelectedItem = SelectedTechUser.Role;
            ActionTechUser.Text = $"TechUser {SelectedTechUser.Id.ToString()} selected";
            ActionTechUser.Visibility = Visibility.Visible;
            TechUserDeleteButton.Visibility = Visibility.Visible;
            TechUserSubmitButton.Visibility = Visibility.Visible;
        }

        private void ClearTechUserInputs()
        {
            InputTechUserId.Text = "";
            InputTechUserFirstName.Text = "";
            InputTechUserLastName.Text = "";
            InputTechUserEmail.Text = "";
            InputTechUserPhone.Text = "";
            InputTechUserPassword.Password = "";
            InputTechUserPasswordConfirm.Password = "";
            InputTechUserRole.Text = "user";
        }
        
        private void NewTechUser(object sender, EventArgs e)
        {
            TechUserDataGrid.UnselectAll();
            ClearTechUserInputs();
            CreationMode = true;
            ActionTechUser.Text = "Creating a new customer";
            ActionTechUser.Visibility = Visibility.Visible;
            TechUserDeleteButton.Visibility = Visibility.Hidden;
            TechUserSubmitButton.Visibility = Visibility.Visible;
        }

        public void TechUserSubmit(object sender, RoutedEventArgs routedEventArgs)
        {
            if (CreationMode)
            {
                NewTechUserSubmit();
            }
            else
            {
                EditTechUserSubmit();
            }
        }

        public void TechUserDelete(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = (TechUserViewModel) DataContext;
            viewModel.DeleteTechUser(SelectedTechUser.Id);
            ActionTechUser.Text = $"TechUser {SelectedTechUser.Id} deleted";
            RefreshDataGrid();
        }
        
        private void NewTechUserSubmit()
        {
            if (string.IsNullOrWhiteSpace(InputTechUserPassword.Password))
            {
                MessageBox.Show("Password input is empty!");
                return;
            }

            if (PasswordConfirmation() == false)
            {
                ActionTechUser.Text = "Password do not match";
                MessageBox.Show("Password do not match");
                return;
            }
            var submittedTechUser = new TechUserModel(
                role:InputTechUserRole.Text,
                firstName:InputTechUserFirstName.Text,
                lastName:InputTechUserLastName.Text,
                email:InputTechUserEmail.Text,
                phone:InputTechUserPhone.Text,
                password:PasswordEncoderService.Hash(InputTechUserPassword.Password),
                createdAt:DateTimeOffset.Now,
                updatedAt:DateTimeOffset.Now,
                deleted:false,
                deletedAt:null
                );
            var viewModel = (TechUserViewModel) DataContext;
            viewModel.CreateTechUser(submittedTechUser);
            ActionTechUser.Text = "New customer submitted";
            RefreshDataGrid();
        }
        
        private void EditTechUserSubmit()
        {
            TechUserModel submittedTechUser;
            if (!string.IsNullOrWhiteSpace(InputTechUserPassword.Password))
            {
                if (PasswordConfirmation() == false)
                {
                    ActionTechUser.Text = "Password do not match";
                    MessageBox.Show("Password do not match");
                    return;
                }
                submittedTechUser = new TechUserModel(
                    id: SelectedTechUser.Id,
                    role: InputTechUserRole.Text,
                    firstName: InputTechUserFirstName.Text,
                    lastName: InputTechUserLastName.Text,
                    email: InputTechUserEmail.Text,
                    phone: InputTechUserPhone.Text,
                    createdAt: SelectedTechUser.CreatedAt,
                    updatedAt: DateTimeOffset.Now, 
                    password: PasswordEncoderService.Hash(InputTechUserPassword.Password));
            }
            else
            {
                submittedTechUser = new TechUserModel(
                    id: SelectedTechUser.Id,
                    role: InputTechUserRole.Text,
                    firstName: InputTechUserFirstName.Text,
                    lastName: InputTechUserLastName.Text,
                    email: InputTechUserEmail.Text,
                    phone: InputTechUserPhone.Text,
                    createdAt: SelectedTechUser.CreatedAt,
                    updatedAt: DateTimeOffset.Now
                );
            }
            var viewModel = (TechUserViewModel) DataContext;
            viewModel.EditTechUser(submittedTechUser);
            ActionTechUser.Text = $"TechUser {submittedTechUser.Id.ToString()} updated!";
            RefreshDataGrid();
        }

        private bool PasswordConfirmation()
        {
            return InputTechUserPassword.Password == InputTechUserPasswordConfirm.Password;
        }

        private void RefreshDataGrid()
        {
            var viewModel = (TechUserViewModel) DataContext;
            TechUserDataGrid.ItemsSource = null;
            TechUserDataGrid.ItemsSource = viewModel.TechUsers;
        }
    }
}