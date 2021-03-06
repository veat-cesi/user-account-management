namespace VeatUAM._Services
{
    public  static class AuthenticationService
    {
        public static string Email { get; set; }

        public static string Role { get; set; }

        public static string FirstName { get; set; }

        public static bool Connected { get; set; }

        public static void Expire()
        {
            Email = null;
            Role = null;
            FirstName = null;
            Connected = false;
        }
    }
}