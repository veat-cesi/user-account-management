using System;
using System.Security.Authentication.ExtendedProtection;
using System.Windows;
using System.Windows.Input;
using LoginTuto.Services;
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
            UserToken = new AuthenticationService();
        }

        public static AuthenticationService UserToken { get; set; }

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

        private void Login(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                var mySqlConnection = new MySqlConnection();
                mySqlConnection.Connection.Open();
                if (!mySqlConnection.ConnectionState()) return;
                var query = $"SELECT email, password, role, firstName FROM tech WHERE email = '{LoginEmail.Text}';";
                mySqlConnection.SetupQuery(query);
                mySqlConnection.SetupReader();
                while (mySqlConnection.Reader.Read())
                {
                    if (!PasswordEncoderService.VerifyPassword(LoginPassword.Password, mySqlConnection.Reader.GetString(1))) continue;
                    UserToken.Email = LoginEmail.Text;
                    UserToken.Role = mySqlConnection.Reader.GetString(2);
                    UserToken.FirstName = mySqlConnection.Reader.GetString(3);
                    UserToken.Connected = true;
                    var mainWindow = new MainWindow();
                    Close();
                    mainWindow.ShowDialog();
                }
            }
            catch (Exception e)
            {
                throw new Exception("No user found with this email/password", e);
            }
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