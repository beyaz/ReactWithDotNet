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

        // Chapter 11
        {"11:107", 13},
        {"11:74",  6},
        {"11:62",  19},
        {"11:53",  11},
        {"11:35",  9},
        {"11:29",  21},
        {"11:9",   7},


        // Chapter 12 
        {"12:109",  24},
        {"12:105",  7 },
        {"12:97",   11},
        {"12:91",   9 },
        {"12:87",   16},
        {"12:82",   11},
        {"12:80",   23},
        {"12:39",   10},
        {"12:29",   6 },
        {"12:11",   10},



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
        {"7:204", 9},
        {"7:165", 18},
        {"7:149", 12},
        {"7:134", 11},
        {"7:131", 17},
        {"7:129", 13},
        {"7:127", 17},
        {"7:101", 15},
        {"7:97", 10},
        {"7:95", 20},
        {"7:90", 10},

        // Chapter 10
        {"10:49", 22},
        {"10:41", 9},
        {"10:28", 13},
        {"10:27", 22},
        {"10:18", 18},

        // Chapter 11
        {"11:107", 14},
        {"11:74", 7},
        {"11:62", 18},
        {"11:53", 10},
        {"11:35", 10},
        {"11:29", 22},
        {"11:9", 8},

        // Chapter 12
        {"12:109",  25  },
        {"12:105",  8   },
        {"12:97",   12  },
        {"12:91",   10  },
        {"12:87",   18  },
        {"12:82",   12  },
        {"12:80",   24  },
        {"12:39",   9   },
        {"12:29",   7   },
        {"12:11",   11  },

    };


    public static IReadOnlyDictionary<string, int> Lam = new Dictionary<string, int>
    {
        {"11:70", 8}
    };
    public static IReadOnlyDictionary<string, int> Lam_Tanzil = new Dictionary<string, int>
    {
        {"11:70", 9}
    };



    #endregion
}