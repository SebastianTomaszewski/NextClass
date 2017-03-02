using System;
using System.IO;
using System.Security.Cryptography;
using static System.String;
using static System.Text.Encoding;
using Convert = System.Convert;
// ReSharper disable InconsistentNaming

namespace NextClass.Extensions
{
    public static class StringExtension
    {
        #region Consts

        private const string PASSWORD_HASH = "P@@Sw0rd";
        private const string SALT_KEY = "S@LT&KEY";
        private const string VI_KEY = "@1B2c3D4e5F6g7H8";
        private const string ENCRYPTION_INDICATOR = "#ENC#";

        #endregion

        #region Public methods

        public static string ToMd5String(this string str)
        {
           if (IsNullOrEmpty(str)) return Empty;

            using (var md5 = MD5.Create())
            {
                return BitConverter
                    .ToString(md5.ComputeHash(UTF8.GetBytes(str)))
                    .Replace("-", Empty);
            }
        }

        public static bool IsEncrypted(this string text)
        {
            return !IsNullOrEmpty(text) && text.StartsWith(ENCRYPTION_INDICATOR, StringComparison.InvariantCulture);
        }

        public static string Encrypt(this string plainText)
        {
           
            if (IsEncrypted(plainText)) return plainText;

            var plainTextBytes = UTF8.GetBytes(plainText);

            var keyBytes = new Rfc2898DeriveBytes(PASSWORD_HASH, ASCII.GetBytes(SALT_KEY)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() {Mode = CipherMode.CBC, Padding = PaddingMode.Zeros};
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, ASCII.GetBytes(VI_KEY));


            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    return ENCRYPTION_INDICATOR + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public static string Decrypt(this string encryptedText)
        {
            if (!IsEncrypted(encryptedText)) return encryptedText;

            var cipherTextBytes = Convert.FromBase64String(encryptedText.Substring(ENCRYPTION_INDICATOR.Length));
            var keyBytes = new Rfc2898DeriveBytes(PASSWORD_HASH, ASCII.GetBytes(SALT_KEY)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, ASCII.GetBytes(VI_KEY));

            using (var memoryStream = new MemoryStream(cipherTextBytes))
            {
                var plainTextBytes = new byte[cipherTextBytes.Length];
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    return UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
                }
            }
        }

  
        #endregion
    }
}
