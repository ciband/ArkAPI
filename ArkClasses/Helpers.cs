using System.Text;
using NBitcoin;

namespace ArkAPI
{
    public static class Helpers
    {
        internal static Wordlist FetchLanguage(this Language lng)
        {
            switch (lng)
            {
                case Language.English:
                    return Wordlist.English;
                case Language.ChineseSimplified:
                    return Wordlist.ChineseSimplified;
                case Language.ChineseTraditional:
                    return Wordlist.ChineseTraditional;
                case Language.Japanese:
                    return Wordlist.Japanese;
                case Language.Spanish:
                    return Wordlist.Spanish;
                case Language.French:
                    return Wordlist.French;
                case Language.PortugueseBrazil:
                    return Wordlist.PortugueseBrazil;
                case Language.Unknown:
                    return Wordlist.English;
                default:
                    return Wordlist.English;
            }
        }

        public static string FromUtf8ToString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] ToUtf8Bytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }

/* unused method
        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(enumerationValue));
            }

            //Tries to find a DescriptionAttribute for a potential friendly name for the enum
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length <= 0) return enumerationValue.ToString();
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            //If we have no description attribute, just return the ToString of the enum
            return attrs.Length > 0 ? ((DescriptionAttribute) attrs[0]).Description : enumerationValue.ToString();  
        }
*/

/*   Unused Function
        public static bool ExploreFile(string filePath) {
            if (!File.Exists(filePath)) {
                return false;
            }
            //Clean up file path so it can be navigated OK
            filePath = Path.GetFullPath(filePath);
            System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filePath));
            return true;
        }
*/
    }
}
