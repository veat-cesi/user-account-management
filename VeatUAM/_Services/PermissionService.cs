using System.Windows;

namespace VeatUAM._Services
{
    public static class PermissionService
    {
        public static bool Permission(bool allowed)
        {
            if (!AuthenticationService.Connected)
            {
                MessageBox.Show("Not connected!");
                return false;
            }
            
            switch (AuthenticationService.Role)
            {
                case "user":
                    MessageBox.Show("Not allowed : user can only read data");
                    return false;
                case "admin":
                    if (allowed) return true;
                    MessageBox.Show("Not allowed : admin cannot edit super admin features");
                    return false;
                case "superadmin":
                    if (allowed) return true;
                    MessageBox.Show("Not allowed : super admin cannot edit / delete super admin features");
                    return false;
            }

            return false;
        }
    }
}