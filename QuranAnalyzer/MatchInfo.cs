
namespace QuranAnalyzer;

[Serializable]
public class MatchInfo
{
    #region Public Properties
    public int ArabicCharacterIndex { get; set; }

    public bool HasNoMatch => ArabicCharacterIndex == -1;
    
    public int StartIndexInVerseText { get; set; }

    public Verse Verse { get; set; }
    
    public string MatchedLetter { get; set; }
    #endregion

    #region Public Methods
    public override string ToString()
    {
        if (MatchedLetter is not null)
        {
            return MatchedLetter;
        }
        
        if (Verse != null)
        {
            if (ArabicCharacterIndex >= 0)
            {
                return ArabicLetter.AllArabicLetters[ArabicCharacterIndex];
            }

            return Verse._text[StartIndexInVerseText].ToString();
        }

        return string.Empty;
    }
    #endregion
}


