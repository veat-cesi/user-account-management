namespace LoginTuto.Services
{
    public class UserModel
    {
        private string _firstName;
        private string _lastName;
        private string _role;
        private string _email;
        private string _password;

        public UserModel(string firstName, string lastName, string role, string email, string password)
        {
            _firstName = firstName;
            _lastName = lastName;
            _role = role;
            _email = email;
            _password = password;
        }
    }
}