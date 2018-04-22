using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;

namespace ArkAPI
{
    public static class DevNetworkApi
    {
        private static Mnemonic _mnemo;
        private static PubKey _pubkey;

        public static string GeneratePassphrase(Language lng)
        {
            _mnemo = new Mnemonic(lng.FetchLanguage(), WordCount.Twelve);
            return _mnemo.ToString();
        }


        public static PubKey GeneratePubKey(string passphrase)
        {
            string mypass;

            if (passphrase != null) mypass = passphrase;
            else if (_mnemo != null) mypass = _mnemo.ToString();
            else mypass = GeneratePassphrase(Language.English);

            var passphraseBytes = Encoding.UTF8.GetBytes(mypass);
            var hash = Hashes.SHA256(passphraseBytes);
            var key = new Key(hash);
            var pubkey = key.PubKey;
            _pubkey = pubkey;
            return pubkey;
        }


        //MainNet byte : 0x17
        public static string Get_address(this PubKey publicKey)
        {
            if (publicKey == null && _pubkey == null)
                throw new NotImplementedException("Generate a pubkey or pass one as parameter");
            if (publicKey == null && _pubkey != null) publicKey = _pubkey;
            if (publicKey == null) throw new NotImplementedException("No Pubkey");

            var ripemd160 = RIPEMD160.Create();
            var keyComputeHash = ripemd160.ComputeHash(publicKey.ToBytes());

            var seed = new byte[21];
            seed[0] = 0X1E;
            for (var i = 0; i < keyComputeHash.Length; i++)
            {
                seed[i + 1] = keyComputeHash[i];
            }

            var base58 = new Base58CheckEncoder();
            var address = base58.EncodeData(seed.ToArray());
            return address;
        }

}
}
