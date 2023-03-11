namespace QuranAnalyzer;

[Serializable]
public class LetterInfoInVerse
{
   
    public int ArabicLetterIndex { get; init; }
    
    public string MatchedLetter { get; init; }

    public int StartIndexInVerseText { get; init; }

    public Verse Verse { get; init; }
   

   
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

            return Verse.Text[StartIndexInVerseText].ToString();
        }

        return string.Empty;
    }
    
}