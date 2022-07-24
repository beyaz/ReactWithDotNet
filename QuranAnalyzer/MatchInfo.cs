using System;

namespace QuranAnalyzer;

[Serializable]
public class MatchInfo
{
    #region Public Properties
    public int ArabicCharacterIndex { get; set; }

    public bool HasNoMatch => ArabicCharacterIndex == -1;
    
    public int StartIndexInVerseText { get; set; }

    public Verse Verse { get; set; }
    #endregion

    #region Public Methods
    public override string ToString()
    {
        if (Verse != null)
        {
            if (ArabicCharacterIndex >= 0)
            {
                return DataAccess.harfler[ArabicCharacterIndex];
            }

            return Verse._text[StartIndexInVerseText].ToString();
        }

        return string.Empty;
    }
    #endregion
}