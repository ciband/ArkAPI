using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;

namespace ArkAPI
{


    public static class KeyStore
    {
        private static readonly string KeyStorePath = string.Format($"DFTRIBE\\{nameof(KeyStore)}");
        private static readonly string AccountFilesPath = KeyStorePath + "\\";

        public static void SaveAccount(string passphrase, string password, string pincode)
        {

            using (var isoStore = IsolatedStorageFile.GetUserStoreForDomain())
            {
                var filepath = AccountFilesPath + DevNetworkApi.GeneratePubKey(passphrase).Get_address();
                if (!isoStore.DirectoryExists(KeyStorePath)) isoStore.CreateDirectory(KeyStorePath);
                // Open or create a writable file.
                var isoStream = isoStore.OpenFile(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                //var writer = new StreamWriter(isoStream);
                //var tmp = EncryptBytes(passphrase.ToUtf8Bytes(), password, pincode);
                //writer.WriteLine(tmp);               
                //writer.Flush();

                var writer = new BinaryWriter(isoStream,Encoding.UTF8,false);
                var tmp = RijndaelHelper.EncryptBytes(passphrase.ToUtf8Bytes(), password, pincode);
                writer.Write(tmp);
                writer.Flush();
                // Cleanup of the above operations.
                writer.Close();
                isoStream.Close();
            }
        }

        public static IEnumerable<string> GetAcccounts
        {
            get
            {
                var accounts = new List<string>();
                using (var isoStore = IsolatedStorageFile.GetUserStoreForDomain())
                {
                    var tmp = isoStore.GetFileNames(AccountFilesPath + "*");
                    accounts.AddRange(tmp);
                }
                return accounts;
            }
        }

        public static string GetAccountPassphrase(string accountAddress, string password, string pin)
        {
            using (var isoStore = IsolatedStorageFile.GetUserStoreForDomain())
            {
                var filepath = AccountFilesPath + accountAddress;
                var isoStream = isoStore.OpenFile(filepath, FileMode.Open, FileAccess.Read);
                var reader = new BinaryReader(isoStream, Encoding.UTF8);
                var byteLength = (int)isoStream.Length;
                var encodedBytes = reader.ReadBytes(byteLength);
                var decrypted = RijndaelHelper.DecryptBytes(encodedBytes, password, pin);
                return decrypted.FromUtf8ToString();
            }
        }
    }
}
