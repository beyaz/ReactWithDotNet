using System.Collections.Generic;

namespace QuranAnalyzer;

public static class SpecifiedByRK
{
    #region Static Fields
    public static IReadOnlyDictionary<string, int> RealElifCounts = new Dictionary<string, int>
    {
        {"2:31", 16},
        {"2:62", 21},
        {"2:65", 6},
        {"2:81", 8},
        {"2:108", 12},
        {"2:123", 10},
        {"2:145", 27},
        {"2:282", 107}
    };

    public static IReadOnlyDictionary<string, int> TanzilElifCounts = new Dictionary<string, int>
    {
        {"2:31", 17},
        {"2:62", 22},
        {"2:65", 7},
        {"2:81", 7},
        {"2:108", 11},
        {"2:123", 11},
        {"2:145", 26},
        {"2:282", 108}
    };
    #endregion
}