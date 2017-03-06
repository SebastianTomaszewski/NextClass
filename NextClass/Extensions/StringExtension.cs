using System;
using System.Linq;
using static System.String;

// ReSharper disable InconsistentNaming

namespace NextClass.Extensions
{
    public static class StringExtension
    {
        #region Cryptography
        //#region Consts

        //private const string PASSWORD_HASH = "P@@Sw0rd";
        //private const string SALT_KEY = "S@LT&KEY";
        //private const string VI_KEY = "@1B2c3D4e5F6g7H8";
        //private const string ENCRYPTION_INDICATOR = "#ENC#";

        //#endregion

        //#region Public methods

        //public static string ToMd5String(this string str)
        //{
        //   if (IsNullOrEmpty(str)) return Empty;

        //    using (var md5 = MD5.Create())
        //    {
        //        return BitConverter
        //            .ToString(md5.ComputeHash(UTF8.GetBytes(str)))
        //            .Replace("-", Empty);
        //    }
        //}
        //public static bool IsEncrypted(this string text)
        //{
        //    return !IsNullOrEmpty(text) && text.StartsWith(ENCRYPTION_INDICATOR, StringComparison.InvariantCulture);
        //}
        //public static string Encrypt(this string plainText)
        //{
           
        //    if (IsEncrypted(plainText)) return plainText;

        //    var plainTextBytes = UTF8.GetBytes(plainText);

        //    var keyBytes = new Rfc2898DeriveBytes(PASSWORD_HASH, ASCII.GetBytes(SALT_KEY)).GetBytes(256 / 8);
        //    var symmetricKey = new RijndaelManaged() {Mode = CipherMode.CBC, Padding = PaddingMode.Zeros};
        //    var encryptor = symmetricKey.CreateEncryptor(keyBytes, ASCII.GetBytes(VI_KEY));


        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        //        {
        //            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        //            cryptoStream.FlushFinalBlock();
        //            return ENCRYPTION_INDICATOR + Convert.ToBase64String(memoryStream.ToArray());
        //        }
        //    }
        //}
        //public static string Decrypt(this string encryptedText)
        //{
        //    if (!IsEncrypted(encryptedText)) return encryptedText;

        //    var cipherTextBytes = Convert.FromBase64String(encryptedText.Substring(ENCRYPTION_INDICATOR.Length));
        //    var keyBytes = new Rfc2898DeriveBytes(PASSWORD_HASH, ASCII.GetBytes(SALT_KEY)).GetBytes(256 / 8);
        //    var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

        //    var decryptor = symmetricKey.CreateDecryptor(keyBytes, ASCII.GetBytes(VI_KEY));

        //    using (var memoryStream = new MemoryStream(cipherTextBytes))
        //    {
        //        var plainTextBytes = new byte[cipherTextBytes.Length];
        //        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
        //        {
        //            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        //            return UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        //        }
        //    }
        //}
        //#endregion Public methods
        #endregion Cryptography

        #region SqlStrings
        public static string ToQuote(this string stringArg,char h = '\u0027')
        {
            if (h <= 0) throw new ArgumentOutOfRangeException(nameof(h));

            switch (h)
            {
                // {s}
                case '\u007B': 
                case '\u007D': return $@"{'\u007B'}{stringArg}{'\u0029'}";
                // (s)
                case '\u0029':
                case '\u0028': return $@"{'\u0028'}{stringArg}{'\u0029'}";
                // [s]
                case '\u005B':
                case '\u005D': return $@"{'\u005B'}{stringArg}{'\u005D'}";
                // hsh
                default:
                    return $@"{h}{stringArg}{h}";                   
            }
        }
        //public static string[] ToQuote(this string[] args)
        //{
        //    if (args == null) throw new ArgumentNullException(nameof(args));
        //    if (args.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(args));

        //    return args.Select(t => t.Trim().ToQuote()).ToArray();
        //}

        public static string ToQuoteAndCommaSplitStrings(this string[] args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (args.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(args));

            return args.Aggregate(Empty, (current, t) => current + t.Trim().ToQuote().AddLastChar(',')).RemoveLastChar(',');

        }

        private static string RemoveLastChar(this string text, char h)
        {
            return IsNullOrWhiteSpace(text) ? Empty : text.Remove(text.LastIndexOf(h), 1);
        }
        private static string AddLastChar(this string text, char h)
        {
            return IsNullOrWhiteSpace(text) ? Empty : $@"{text}{h}";
        }
        #endregion
    }
}
