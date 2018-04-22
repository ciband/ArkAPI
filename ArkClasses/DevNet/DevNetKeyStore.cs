using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using ArkAPI.Helpers;

namespace ArkAPI.DevNet
{


    public static class DevNetKeyStore
    {
        private static readonly string KeyStorePath = string.Format($"DFTRIBE\\{nameof(DevNetKeyStore)}");
        private static readonly string AccountFilesPath = KeyStorePath + "\\";

        public static void SaveDevNetAccount(string passphrase, string password, string pincode)
        {

            using (var isoStore = IsolatedStorageFile.GetUserStoreForDomain())
            {
                var filepath = AccountFilesPath + DevNet.GenerateDevNetPubKey(passphrase).GetDevNetAddress();
                if (!isoStore.DirectoryExists(KeyStorePath)) isoStore.CreateDirectory(KeyStorePath);
                
                // Open or create a writable file.
                var isoStream = isoStore.OpenFile(filepath, FileMode.OpenOrCreate, FileAccess.Write);
                var writer = new BinaryWriter(isoStream,Encoding.UTF8,false);

                //Encrypt the passphrase & write it
                var tmp = RijndaelHelper.EncryptBytes(passphrase.ToUtf8Bytes(), password, pincode);
                writer.Write(tmp);
                writer.Flush();
                // Cleanup of the above operations.
                writer.Close();
                isoStream.Close();
            }
        }

        public static IEnumerable<string> GetDevNetAcccounts
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

        public static string GetDevNetAccountPassphrase(string accountAddress, string password, string pin)
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
