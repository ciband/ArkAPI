using System;
using ArkAPI;
using ArkAPI.DevNet;
using ArkAPI.Helpers;
using ArkAPI.MainNet;
using Language = ArkAPI.Helpers.Language;
using static ArkAPI.ArkBases;
using static ArkAPI.DevNet.DevNet;
using static ArkAPI.DevNet.DevNetKeyStore;
using static ArkAPI.Helpers.RijndaelHelper;

namespace ArkTestApp
{
    public static class Program
    {
        private const string Passphrase = @"bullet parade snow bacon mutual deposit brass floor staff list concert ask";
        private const string Password = "dummypassword";
        private const string Pin = "30302012";
        private static readonly string Account = GenerateDevNetPubKey(Passphrase).GetAddress();

        static void Main()
        {
            var passphra = GeneratePassphrase(Language.English);
            Console.WriteLine(passphra);
            Console.WriteLine();
            
            var pubkey = GenerateDevNetPubKey(Passphrase);
            Console.WriteLine(pubkey);
            Console.WriteLine();

            var address = GenerateDevNetPubKey(Passphrase).GetDevNetAddress();
            Console.WriteLine(address);
            Console.WriteLine();

            var tmp = EncryptBytes(Passphrase.ToUtf8Bytes(), Password, Pin);
            var roundabout = DecryptBytes(tmp, Password, Pin);
            Console.WriteLine(roundabout.FromUtf8ToString());

            SaveDevNetAccount(Passphrase,Password,Pin);
            var mAcccounts = GetDevNetAcccounts;
            foreach (var acccount in mAcccounts)
            {
                Console.WriteLine(acccount);
            }
            Console.WriteLine();

            var tmp2 = GetDevNetAccountPassphrase(Account, Password, Pin);
            Console.WriteLine(tmp2);
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
