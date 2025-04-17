using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace DataAccessLayer
{
    public static class Helper
    {
        public static string ComputeHash(string input)
        {
            //SHA is Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));


                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
