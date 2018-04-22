using System.ComponentModel;

namespace ArkAPI
{
    public enum Language
    {
        [Description("English")] English,
        [Description("Japanese")] Japanese,
        [Description("Spanish")] Spanish,
        [Description("Simplified Chineese")] ChineseSimplified,
        [Description("Traditional Chineese")] ChineseTraditional,
        [Description("French")] French,
        [Description("Brazil Portugese")] PortugueseBrazil,
        [Description("Unknown")] Unknown
    }
}