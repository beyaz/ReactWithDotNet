using System.Collections.Generic;

namespace QuranAnalyzer;

public static class SpecifiedByRK
{
    #region Static Fields
    public static IReadOnlyDictionary<string, int> RealElifCounts = new Dictionary<string, int>
    {
        // Chapter 2
        {"2:31", 16},
        {"2:62", 21},
        {"2:65", 6},
        {"2:81", 8},
        {"2:108", 12},
        {"2:123", 10},
        {"2:145", 27},
        {"2:282", 107},

        // Chapter 3

        {"3:49", 35},
        {"3:66", 10},
        {"3:69", 9},
        {"3:87", 9},
        {"3:120", 14},
        {"3:121", 6},
        {"3:157", 5},
        {"3:158", 4},
        {"3:187", 17}
    };

    public static IReadOnlyDictionary<string, int> SS = new Dictionary<string, int>
    {
        {"7:69", 0}// bestaten: *بَسْطَةً*
    };
    public static IReadOnlyDictionary<string, int> SS_Tanzil = new Dictionary<string, int>
    {
        {"7:69", 1} // bestaten: *بَصْۜطَةً*
    };

    public static IReadOnlyDictionary<string, int> TanzilElifCounts = new Dictionary<string, int>
    {
        // Chapter 2
        {"2:31", 17},
        {"2:62", 22},
        {"2:65", 7},
        {"2:81", 7},
        {"2:108", 11},
        {"2:123", 11},
        {"2:145", 26},
        {"2:282", 108},

        // Chapter 3
        {"3:49", 33},
        {"3:66", 9},
        {"3:69", 8},
        {"3:87", 8},
        {"3:120", 13},
        {"3:121", 5},
        {"3:157", 4},
        {"3:158", 3},
        {"3:187", 16}
    };
    #endregion
}