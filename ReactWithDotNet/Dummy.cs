using System.Text;

namespace ReactWithDotNet;

partial class Mixin
{
    static readonly string[] LoremWords =
    [
        "lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit", "sed", "do",
        "eiusmod", "tempor", "incididunt", "ut", "labore", "et", "dolore", "magna", "aliqua", "ut",
        "enim", "ad", "minim", "veniam", "quis", "nostrud", "exercitation", "ullamco", "laboris",
        "nisi", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "aute", "irure",
        "dolor", "in", "reprehenderit", "in", "voluptate", "velit", "esse", "cillum", "dolore",
        "eu", "fugiat", "nulla", "pariatur", "excepteur", "sint", "occaecat", "cupidatat", "non",
        "proident", "sunt", "in", "culpa", "qui", "officia", "deserunt", "mollit", "anim", "id",
        "est", "laborum"
    ];

    public static string DummySentence(int wordCount)
    {
        var random = new Random();
        var result = new StringBuilder();

        for (var i = 0; i < wordCount; i++)
        {
            if (i > 0)
            {
                result.Append(" ");
            }

            result.Append(LoremWords[random.Next(LoremWords.Length)]);
        }

        result.Append(".");
        return result.ToString();
    }

    public static string DummySrc(int size)
    {
        return $"https://picsum.photos/{size}";
    }
}