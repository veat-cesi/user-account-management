using System.Windows;
using System.Windows.Input;
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

        private void Login(object sender, RoutedEventArgs routedEventArgs)
        {
            if (true)
            {
                WindowState = WindowState.Maximized;
                var mainWindow = new MainWindow();
                //Close();
                mainWindow.ShowDialog();
            }
        }
        
    }
}