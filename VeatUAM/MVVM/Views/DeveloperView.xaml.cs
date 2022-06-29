using System;
using System.Windows;
using System.Windows.Controls;
using VeatUAM._Services;
using VeatUAM.MVVM.Models;
using VeatUAM.MVVM.ViewModels;

namespace VeatUAM.MVVM.Views
{
    public partial class DeveloperView : UserControl
    {
        public DeveloperView()
        {
            InitializeComponent();
            CreationMode = false;
        }

        public bool CreationMode { get; set; }

        public DeveloperModel SelectedDeveloper { get; set; }

        private void DataGridRow_OnSelected(object sender, RoutedEventArgs e)
        {
            CreationMode = false;
            if (sender == null) return;
            if (DeveloperDataGrid.Items.IndexOf(DeveloperDataGrid.CurrentItem) < 0) return;
            SelectedDeveloper = (DeveloperModel) DeveloperDataGrid.CurrentItem;
            InputDeveloperId.Text = SelectedDeveloper.Id.ToString();
            InputDeveloperFirstName.Text = SelectedDeveloper.FirstName;
            InputDeveloperLastName.Text = SelectedDeveloper.LastName;
            InputDeveloperEmail.Text = SelectedDeveloper.Email;
            InputDeveloperPhone.Text = SelectedDeveloper.Phone;
            ActionDeveloper.Text = $"Developer {SelectedDeveloper.Id.ToString()} selected";
            ActionDeveloper.Visibility = Visibility.Visible;
            DeveloperDeleteButton.Visibility = Visibility.Visible;
            DeveloperSubmitButton.Visibility = Visibility.Visible;
        }

        private void ClearDeveloperInputs()
        {
            InputDeveloperId.Text = "";
            InputDeveloperFirstName.Text = "";
            InputDeveloperLastName.Text = "";
            InputDeveloperEmail.Text = "";
            InputDeveloperPhone.Text = "";
        }
        
        private void NewDeveloper(object sender, EventArgs e)
        {
            DeveloperDataGrid.UnselectAll();
            ClearDeveloperInputs();
            CreationMode = true;
            ActionDeveloper.Text = "Creating a new customer";
            ActionDeveloper.Visibility = Visibility.Visible;
            DeveloperDeleteButton.Visibility = Visibility.Hidden;
            DeveloperSubmitButton.Visibility = Visibility.Visible;
        }

        public void DeveloperSubmit(object sender, RoutedEventArgs routedEventArgs)
        {
            if (CreationMode)
            {
                NewDeveloperSubmit();
            }
            else
            {
                EditDeveloperSubmit();
            }
        }

        public void DeveloperDelete(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = (DeveloperViewModel) DataContext;
            viewModel.DeleteDeveloper(SelectedDeveloper.Id);
            ActionDeveloper.Text = $"Developer {SelectedDeveloper.Id} deleted";
            RefreshDataGrid();
        }
        
        private void NewDeveloperSubmit()
        {
            if (string.IsNullOrWhiteSpace(InputDeveloperPassword.Password))
            {
                MessageBox.Show("Password input is empty!");
                return;
            }

            if (PasswordConfirmation() == false)
            {
                ActionDeveloper.Text = "Password do not match";
                throw new Exception("Password do not match");
            }
            var submitedDeveloper = new DeveloperModel(
                firstName:InputDeveloperFirstName.Text,
                lastName:InputDeveloperLastName.Text,
                email:InputDeveloperEmail.Text,
                phone:InputDeveloperPhone.Text,
                password:PasswordEncoderService.Hash(InputDeveloperPassword.Password),
                createdAt:DateTimeOffset.Now,
                updatedAt:DateTimeOffset.Now,
                deleted:false,
                deletedAt:null
                );
            var viewModel = (DeveloperViewModel) DataContext;
            viewModel.CreateDeveloper(submitedDeveloper);
            ActionDeveloper.Text = "New customer submitted";
            RefreshDataGrid();
        }
        
        private void EditDeveloperSubmit()
        {
            DeveloperModel submittedDeveloper;
            if (!string.IsNullOrWhiteSpace(InputDeveloperPassword.Password))
            {
                if (PasswordConfirmation() == false)
                {
                    ActionDeveloper.Text = "Password do not match";
                    throw new Exception("Password do not match");
                }
                submittedDeveloper = new DeveloperModel(
                    id: SelectedDeveloper.Id,
                    firstName: InputDeveloperFirstName.Text,
                    lastName: InputDeveloperLastName.Text,
                    email: InputDeveloperEmail.Text,
                    phone: InputDeveloperPhone.Text,
                    createdAt: SelectedDeveloper.CreatedAt,
                    updatedAt: DateTimeOffset.Now, 
                    password: PasswordEncoderService.Hash(InputDeveloperPassword.Password));
            }
            else
            {
                submittedDeveloper = new DeveloperModel(
                    id: SelectedDeveloper.Id,
                    firstName: InputDeveloperFirstName.Text,
                    lastName: InputDeveloperLastName.Text,
                    email: InputDeveloperEmail.Text,
                    phone: InputDeveloperPhone.Text,
                    createdAt: SelectedDeveloper.CreatedAt,
                    updatedAt: DateTimeOffset.Now
                );
            }
            var viewModel = (DeveloperViewModel) DataContext;
            viewModel.EditDeveloper(submittedDeveloper);
            ActionDeveloper.Text = $"Developer {submittedDeveloper.Id.ToString()} updated!";
            RefreshDataGrid();
        }

        private bool PasswordConfirmation()
        {
            return InputDeveloperPassword.Password == InputDeveloperPasswordConfirm.Password;
        }

        private void RefreshDataGrid()
        {
            var viewModel = (DeveloperViewModel) DataContext;
            DeveloperDataGrid.ItemsSource = null;
            DeveloperDataGrid.ItemsSource = viewModel.Developers;
        }
    }
}