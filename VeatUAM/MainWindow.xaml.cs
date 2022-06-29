using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using VeatUAM._Services;
using VeatUAM.Core;

namespace VeatUAM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
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
        private void MaximizeWindow(object sender, RoutedEventArgs routedEventArgs)
        {
            WindowState = WindowState switch
            {
                WindowState.Maximized => WindowState.Normal,
                WindowState.Normal => WindowState.Maximized,
                _ => WindowState
            };
        }
        private void MinimizeWindow(object sender, RoutedEventArgs routedEventArgs)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            WindowState = WindowState.Minimized;
        }
    }
}