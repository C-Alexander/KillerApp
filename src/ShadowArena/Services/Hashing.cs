using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Shadow_Arena.Services
{
    public class Hashing : IHashing
    {
        private byte[] salt = System.Text.Encoding.UTF8.GetBytes("The byte strikes back"); //change this = break all old passwords. Please add versioning if you do so.
        KeyDerivationPrf algorithm = KeyDerivationPrf.HMACSHA512;
        private int iterationCount = 10000;
        private int byteCount = 256/8;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>

        public string GetHashedPassword(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password,
                salt,
                algorithm,
                iterationCount,
                byteCount));
        }
    }
}
