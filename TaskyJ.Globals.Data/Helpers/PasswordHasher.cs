using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace TaskyJ.Globals.Data.Helpers
{
    public sealed class JPasswordHasher
    {
        private static readonly byte[] salt = new byte[128 / 8];
        public static readonly byte[] Salt = salt;

        public static string HashJ(string plainpassword, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainpassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public static bool CheckHashJ(string plainpassword, string hashedpassword, byte[] salt)
        {
            return hashedpassword.Equals(HashJ(plainpassword, salt));
        }

        static JPasswordHasher()
        {
            // generate a 128-bit salt using a secure PRNG
            /*using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }*/
            //generate a static 128-bit salt, this should be generated once at startup and 
            //should be equal in all projects (secret file, for example)
            for (var i = 0; i < salt.Length; i++)
            {
                salt[i] = Convert.ToByte(i % 256);
            }
        }
    }
}
