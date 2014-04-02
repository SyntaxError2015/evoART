using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using evoART.Models.DbModels;

namespace evoART.Special
{
    public static class TokenGenerator
    {
        public static string EncryptMD5(string key, string type)
        {
            var cypher = MD5.Create();

            // Calculate MD5 hash from input
            var inputBytes = Encoding.ASCII.GetBytes(key);
            var hash = cypher.ComputeHash(inputBytes);

            // Convert byte array to HEX string
            var sb = new StringBuilder();

            foreach (var part in hash)
            {
                sb.Append(part.ToString(type));
            }

            return sb.ToString();
        }

        public static string EncryptMD5(string key)
        {
            return EncryptMD5(key, "X2");
        }

        public static string ExtractSpecialHashKey(object obj)
        {
            return obj == null ? null : obj.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }

        public static string GenerateValidationToken(AccountModels.AccountValidation validation)
        {
            return EncryptMD5(ExtractSpecialHashKey(validation), "X");
        }
    }
}