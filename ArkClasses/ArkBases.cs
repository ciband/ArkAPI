using ArkAPI.Helpers;
using NBitcoin;
using Language = ArkAPI.Helpers.Language;

namespace ArkAPI
{
    public static class ArkBases
    {
        private static Mnemonic _mnemo;

        public static string GeneratePassphrase(Language lng)
        {
            _mnemo = new Mnemonic(lng.FetchLanguage(), WordCount.Twelve);
            return _mnemo.ToString();
        }
    }
}
