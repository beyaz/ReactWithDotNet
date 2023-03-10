namespace QuranAnalyzer.WebUI;

public abstract class ReactComponent : ReactWithDotNet.ReactComponent
{
    protected IEnumerable<Element> AsLetter(string arabicLetter)
    {
        var pronunciation = GetPronunciationOfArabicLetter(arabicLetter);

        return new Element[] { (strong)pronunciation, "(", (strong)arabicLetter, ")" };
    }

    protected string GetPronunciationOfArabicLetter(string arabicLetter)
    {
        return GetTurkishPronunciationOfArabicLetter(arabicLetter);
    }

    protected (string reading, string trMean)? GetPronunciationOfArabicWord(string arabicWord)
    {
        return GetTurkishPronunciationOfArabicWord(arabicWord);
    }
}

public abstract class ReactPureComponent : ReactWithDotNet.ReactPureComponent
{
    protected IEnumerable<Element> AsLetter(string arabicLetter)
    {
        var pronunciation = GetPronunciationOfArabicLetter(arabicLetter);

        return new Element[] { (strong)pronunciation, "(", (strong)arabicLetter, ")" };
    }

    protected string GetPronunciationOfArabicLetter(string arabicLetter)
    {
        return GetTurkishPronunciationOfArabicLetter(arabicLetter);
    }

    protected (string reading, string trMean)? GetPronunciationOfArabicWord(string arabicWord)
    {
        return GetTurkishPronunciationOfArabicWord(arabicWord);
    }
}

public abstract class ReactComponent<TState> : ReactWithDotNet.ReactComponent<TState> where TState : new()
{
    protected string GetPronunciationOfArabicLetter(string arabicLetter)
    {
        return GetTurkishPronunciationOfArabicLetter(arabicLetter);
    }
}