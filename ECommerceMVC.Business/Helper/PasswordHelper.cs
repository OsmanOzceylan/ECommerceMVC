using System.Security.Cryptography;
using System.Text;
namespace ECommerceMVC.Business.Helper
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);           // salt eklenebilir (şifreye rastgele dize ekler)
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
