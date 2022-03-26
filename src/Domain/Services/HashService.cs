using System.Security.Cryptography;
using System.Text;

namespace Domain.Services;

public interface IHashService
{
    string Generate(string value);
}

public class HashService : IHashService
{
    public string Generate(string value)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bytes);
            return string.Concat(hash.Select(x => x.ToString("x2")));
        }
    }
}