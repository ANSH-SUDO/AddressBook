using System.Security.Cryptography;

namespace AddressBook.Helpers
{
    public class TokenGenerator
    {
        public static string GenerateToken()
        {
            byte[] tokenBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }
            return Convert.ToBase64String(tokenBytes);
        }
    }
}
