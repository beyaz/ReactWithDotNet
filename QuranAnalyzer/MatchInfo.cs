
namespace QuranAnalyzer;

[Serializable]
public class MatchInfo
{
    #region Public Properties
    public int ArabicLetterIndex { get; set; }

    public bool HasNoMatch => ArabicLetterIndex == -1;
    
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
            if (ArabicLetterIndex >= 0)
            {
                return ArabicLetter.AllArabicLetters[ArabicLetterIndex];
            }

            return Verse._text[StartIndexInVerseText].ToString();
        }

        return string.Empty;
    }
    #endregion
}


