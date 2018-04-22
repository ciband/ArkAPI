using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using Language = ArkAPI.Helpers.Language;

namespace ArkAPI.MainNet
{
    public static class MainNet
    {
        private static PubKey _pubkey;


        public static PubKey GeneratePubKey(string passphrase)
        {
            var mypass = passphrase ?? ArkBases.GeneratePassphrase(Language.English);

            var passphraseBytes = Encoding.UTF8.GetBytes(mypass);
            var hash = Hashes.SHA256(passphraseBytes);
            var key = new Key(hash);
            var pubkey = key.PubKey;
            _pubkey = pubkey;
            return pubkey;
        }

        public static string GetAddress(this PubKey publicKey)
        {
            if (publicKey == null && _pubkey == null)
                throw new NotImplementedException("Generate a pubkey or pass one as parameter");
            if (publicKey == null && _pubkey != null) publicKey = _pubkey;
            if (publicKey == null) throw new NotImplementedException("No Pubkey");

            var ripemd160 = RIPEMD160.Create();
            var keyComputeHash = ripemd160.ComputeHash(publicKey.ToBytes());

            var seed = new byte[21];
            seed[0] = 0X17;
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