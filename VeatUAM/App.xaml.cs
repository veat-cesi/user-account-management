using System.Windows;
using VeatUAM._Services;
using VeatUAM.Core;

namespace VeatUAM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AuthenticationService.Expire();
            MySqlConnectionService.Connection.Close();
        }
    }
}