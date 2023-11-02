using Application.Ports;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure;

public class SHA256HashingService : IHashingService
{
    public string Hash(string toHash)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(toHash));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
