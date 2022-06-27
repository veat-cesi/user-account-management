namespace LoginTuto.Services
{
    public class EncryptedPasswordModel
    {
        public EncryptedPasswordModel(string rawPassword)
        {
            RawPassword = rawPassword;
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(RawPassword);
        }

        public string RawPassword { get; set; }

        public string HashedPassword { get; set; }
        
    }
}