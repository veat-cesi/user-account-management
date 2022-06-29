using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using MahApps.Metro.IconPacks;
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

        private void OnClickLogout(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
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
            switch (WindowState)
            {
                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    MaximizeWindowButton.Content = new PackIconMaterial()
                    {
                        Kind = PackIconMaterialKind.WindowMaximize
                    };
                    break;
                case WindowState.Normal:
                    WindowState = WindowState.Maximized;
                    MaximizeWindowButton.Content = new PackIconMaterial()
                    {
                        Kind = PackIconMaterialKind.WindowRestore
                    };
                    break;
                case WindowState.Minimized:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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