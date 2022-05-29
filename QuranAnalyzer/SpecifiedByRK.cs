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
        {"3:187", 17},

        // Chapter 7
        {"7:204",   10},
        {"7:165",   17},
        {"7:149",   13},
        {"7:134",   12},
        {"7:131",   19},
        {"7:129",   14},
        {"7:127",   16},
        {"7:101",   16},
        {"7:97",    11},
        {"7:95",    21},
        {"7:90",    11},

        // Chapter 10 
        {"10:49",   21},
        {"10:41",   7 },
        {"10:28",   14},
        {"10:27",   21},
        {"10:18",   17},


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
        {"3:187", 16},

        // Chapter 7
        {"7:204",   9},
        {"7:165",   18},
        {"7:149",   12},
        {"7:134",   11},
        {"7:131",   17},
        {"7:129",   13},
        {"7:127",   17},
        {"7:101",   15},
        {"7:97",    10},
        {"7:95",    20},
        {"7:90",    10},

        // Chapter 10
        {"10:49",       22},
        {"10:41",       9},
        {"10:28",       13},
        {"10:27",       22},
        {"10:18",       18},

    };
    #endregion
}