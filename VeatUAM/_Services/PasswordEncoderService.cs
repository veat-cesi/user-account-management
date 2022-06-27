using System;

namespace LoginTuto.Services
{
    public static class PasswordEncoderService
    {

        public static EncryptedPasswordModel EncryptedPassword(string password)
        {
            return new EncryptedPasswordModel(password);
        }

        public static string Hash(string password)
        {
            var encryptedPasswordModel = EncryptedPassword(password);
            if (VerifyPassword(password, encryptedPasswordModel.HashedPassword))
            {
                return encryptedPasswordModel.HashedPassword;
            }

            throw new Exception("Hash processus failed.");
        }

        public static bool VerifyPassword(string input, string reference)
        {
            return BCrypt.Net.BCrypt.Verify(input, reference);
        }
    }
}