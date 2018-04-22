using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ArkAPI.Helpers
{
    public static class RijndaelHelper
    {
        // Example usage: EncryptBytes(someFileBytes, "SensitivePhrase", "SodiumChloride");
        public static byte[] EncryptBytes(byte[] inputBytes, string passPhrase, string saltValue)
        {
            var rijndaelCipher = new RijndaelManaged {Mode = CipherMode.CBC};

            var salt = Encoding.ASCII.GetBytes(saltValue);
            var password = new PasswordDeriveBytes(passPhrase, salt, "SHA256", 5);


            var encryptor = rijndaelCipher.CreateEncryptor(password.GetBytes(32), password.GetBytes(16));

            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            return cipherBytes;
        }

        // Example usage: DecryptBytes(encryptedBytes, "SensitivePhrase", "SodiumChloride");
        public static byte[] DecryptBytes(byte[] encryptedBytes, string passPhrase, string saltValue)
        {
            var rijndaelCipher = new RijndaelManaged {Mode = CipherMode.CBC};

            var salt = Encoding.ASCII.GetBytes(saltValue);
            var password = new PasswordDeriveBytes(passPhrase, salt, "SHA256", 5);

            var decryptor = rijndaelCipher.CreateDecryptor(password.GetBytes(32), password.GetBytes(16));

            var memoryStream = new MemoryStream(encryptedBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainBytes = new byte[encryptedBytes.Length];

            cryptoStream.Read(plainBytes, 0, plainBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            return plainBytes;
        }
    }
}
