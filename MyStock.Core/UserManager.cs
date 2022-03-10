using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace MyStock.Core
{
    public static class UserManager
    {
        public static bool Login(string login, string password)
        {
            var user = Global.Context.Set<User>().SingleOrDefault(u => u.IsActive && u.Login.ToLower() == login.ToLower());
            if (user == null)
                return false;

            if (VerifyPassword(password, user.PasswordHash, user.Salt))
            {
                Global.CurrentUser = user;
                return true;
            }

            return false;
        }

        public static (string Hash, byte[] Salt) EncryptPassword(string password)
        {
            var salt = CreateSalt();
            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return (encryptedPassw, salt);
        }

        public static bool VerifyPassword(string enteredPassword, string storedPassword, byte[] salt)
        {
            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return encryptedPassw == storedPassword;
        }

        private static byte[] CreateSalt()
        {
            byte[] _salt = new byte[40];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(_salt);
            }
            return _salt;
        }
    }
}
