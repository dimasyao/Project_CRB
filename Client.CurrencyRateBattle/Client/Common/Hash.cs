using System.Security.Cryptography;
using System.Text;

namespace Client.Common;

public class Hash
{

    /// <returns>string hashed by SHA256 algorythm</returns>
    public static string GetHash(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password.ToCharArray()));
        var hash = new StringBuilder(bytes.Length);

        foreach (var c in bytes)
        {
            _ = hash.Append(c);
        }

        return hash.ToString();
    }
}
