using System.Text;
using System.Text.RegularExpressions;

namespace QuranAnalyzer.WebUI.Pages.WordSearchingPage;

class SearchScript
{
    public IReadOnlyList<(string ChapterFilter, IReadOnlyList<LetterInfo> Letters)> Lines { get; private init; }

    public static Response<SearchScript> ParseScript(string value)
    {
        if (value.HasNoValue())
        {
            return new SearchScript
            {
                Lines = new List<(string ChapterFilter, IReadOnlyList<LetterInfo> SearchLetters)>()
            };
        }

        var lines = parseToLines(value).AsListOf(parseLine);

        if (lines.IsFail)
        {
            return lines.Errors.ToArray();
        }

        return new SearchScript
        {
            Lines = lines.Value
        };

        static IEnumerable<string> parseToLines(string value)
        {
            value = value.Replace('\n', ';');

            return value.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(x=>x.Trim());
        }

        static Response<(string ChapterFilter, IReadOnlyList<LetterInfo> Letters)> parseLine(string line)
        {
            var arr = line.Split(new[] { '|', '~' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length != 2)
            {
                return "Arama komutunda yanlışlık var. Örnek: 3. suredeki Mim(م) harfini aratmak için şöyle yazabilirsiniz. 3:*|م";
            }

            var letterInfoList = Analyzer.AnalyzeText(clearText(arr[1]));
           

            var letters = letterInfoList.Where(Analyzer.IsArabicLetter).ToList();
            if (letters.Count == 0)
            {
                return "Arama komutunda yanlışlık var. En az bir harf girmelisiniz. Örnek: 3. suredeki Mim(م) harfini aratmak için şöyle yazabilirsiniz. 3:*|م";
            }

            return (arr[0].Trim(), letters);
        }

        static string clearText(string str) => Regex.Replace(str, @"\s+", string.Empty);
    }

    public string AsReadibleString()
    {
        var sb = new StringBuilder();

        foreach (var line in Lines)
        {
            sb.AppendLine(line.ChapterFilter + " | " + string.Join("", line.Letters));
        }

        return sb.ToString();
    }

    public string AsString()
    {
        return string.Join(";", Lines.Select(line => line.ChapterFilter + "~" + string.Join("", line.Letters)));
    }
}