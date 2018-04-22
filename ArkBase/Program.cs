using System;
using ArkAPI;
using Language = ArkAPI.Language;

namespace ArkTestApp
{
    public static class Program
    {
        private const string Passphrase = @"bullet parade snow bacon mutual deposit brass floor staff list concert ask";
        private const string Password = "dummypassword";
        private const string Pin = "30302012";
        private static readonly string Account = DevNetworkApi.GeneratePubKey(Passphrase).Get_address();

        static void Main()
        {
            var passphra = DevNetworkApi.GeneratePassphrase(Language.English);
            Console.WriteLine(passphra);
            Console.WriteLine();
            
            var pubkey = DevNetworkApi.GeneratePubKey(Passphrase);
            Console.WriteLine(pubkey);
            Console.WriteLine();

            var address = DevNetworkApi.GeneratePubKey(Passphrase).Get_address();
            Console.WriteLine(address);
            Console.WriteLine();

            var tmp = RijndaelHelper.EncryptBytes(Passphrase.ToUtf8Bytes(), Password, Pin);
            var roundabout = RijndaelHelper.DecryptBytes(tmp, Password, Pin);
            Console.WriteLine(roundabout.FromUtf8ToString());

            KeyStore.SaveAccount(Passphrase,Password,Pin);
            var mAcccounts = KeyStore.GetAcccounts;
            foreach (var acccount in mAcccounts)
            {
                Console.WriteLine(acccount);
            }
            Console.WriteLine();

            var tmp2 = KeyStore.GetAccountPassphrase(Account, Password, Pin);
            Console.WriteLine(tmp2);
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
