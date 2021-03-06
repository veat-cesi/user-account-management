using System;
using System.Data.SqlClient;
using System.Security.Authentication.ExtendedProtection;
using System.Windows;
using System.Windows.Input;
using VeatUAM._Services;
using VeatUAM.Core;

namespace VeatUAM
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            WpfSingleInstance.Make("Veat UAM - Login", uniquePerUser: false);
            InitializeComponent();
        }

        private void DragWindow(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (mouseButtonEventArgs.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }

        private void SubmitLogin(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                MySqlConnectionService.Connection.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (!MySqlConnectionService.ConnectionState()) return;
            var query = $"SELECT email, password, role, firstName FROM tech WHERE email = '{LoginEmail.Text}';";
            MySqlConnectionService.SetupQuery(query);
            MySqlConnectionService.SetupReader();
            while (MySqlConnectionService.Reader.Read())
            {
                if (!PasswordEncoderService.VerifyPassword(LoginPassword.Password,
                        MySqlConnectionService.Reader.GetString(1)))
                {
                    MessageBox.Show("Wrong Password");
                    MySqlConnectionService.Reader.Close();
                    MySqlConnectionService.Connection.Close();
                    return;
                }
                AuthenticationService.Email = LoginEmail.Text;
                AuthenticationService.Role = MySqlConnectionService.Reader.GetString(2);
                AuthenticationService.FirstName = MySqlConnectionService.Reader.GetString(3);
                AuthenticationService.Connected = true;
            }
            if (!AuthenticationService.Connected)
            {
                MySqlConnectionService.Reader.Close();
                MySqlConnectionService.Connection.Close();
                MessageBox.Show($"Unfound user with this email : {LoginEmail.Text}");
                LoggerService.NewLog("Failed to login.");
                return;
            }
            MySqlConnectionService.Reader.Close();
            var mainWindow = new MainWindow();
            LoggerService.NewLog("Logged successfully!");
            Close();
            mainWindow.ShowDialog();
        }

        private void LoginEmail_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginEmail.Text == "Email")
            {
                LoginEmail.Text = "";
            }
        }

        private void LoginPassword_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginPassword.Password == "Password")
            {
                LoginPassword.Password = "";
            }
        }
    }
}