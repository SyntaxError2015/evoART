using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

namespace evoART.Special
{
    public static class TextWarping
    {
        public static string EncryptMD5(string key)
        {
            var cypher = MD5.Create();

            // Calculate MD5 hash from input
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
            var hash = cypher.ComputeHash(inputBytes);

            // Convert byte array to HEX string
            var sb = new StringBuilder();

            foreach (var part in hash)
            {
                sb.Append(part.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string ExtractSpecialHashKey(object obj)
        {
            return obj == null ? null : obj.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }
    }
}