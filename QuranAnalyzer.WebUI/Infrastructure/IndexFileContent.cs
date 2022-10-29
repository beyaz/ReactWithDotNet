using System.Text;

namespace QuranAnalyzer.WebUI;

sealed class IndexFileContent
{
    public Type Component { get; set; }

    public string[] Head { get; set; }

    public string RootFolderName { get; set; }

    public string GetFileContent()
    {
        var lines = new List<Line>
        {
            "<!DOCTYPE html>",

            "<html lang='en' xmlns='http://www.w3.org/1999/xhtml' dir='ltr'>",

            "<head>",
            "    <meta charset='utf-8' />",
            "    <meta name='viewport' content='width=device-width, initial-scale=1'>",

            "    <meta http-equiv='Cache-Control' content='no-cache, no-store, must-revalidate' />",
            "    <meta http-equiv='Pragma' content='no-cache' />",
            "    <meta http-equiv='Expires' content='0' />",

            "    <title>Quran Analyzer</title>",

            "    <!-- Font -->",
            "    <link rel='stylesheet' href='https://fonts.googleapis.com/css2?family=Google+Sans+Text:wght@400;500;700&display=swap'>",

            $"    <link rel='stylesheet' href='{RootFolderName}/index.css'>",

            Head,

            "</head>",

            "<body>",
            "    <div id='app'>",
            $"        <script src='{RootFolderName}/index.js'></script>",
            "    </div>",
            "</body>",

            "</html>",

            "<script type='text/javascript'>",
            "    ReactWithDotNet.RenderComponentIn({",
            $"        fullTypeNameOfReactComponent: '{GetFullName(Component)}',",
            "        containerHtmlElementId: 'app'",
            "    });",
            "</script>"
        };

        return lines.Aggregate(new StringBuilder(), (sb, line) => line.WriteTo(sb)).ToString();
    }

    static string GetFullName(Type type)
    {
        return $"{type.FullName},{type.Assembly.GetName().Name}";
    }

    class Line
    {
        string value;
        string[] values;

        public static implicit operator Line(string line)
        {
            return new Line { value = line };
        }

        public static implicit operator Line(string[] lines)
        {
            return new Line { values = lines };
        }

        public StringBuilder WriteTo(StringBuilder sb)
        {
            if (values?.Length > 0)
            {
                foreach (var item in values)
                {
                    sb.AppendLine(item);
                }

                return sb;
            }

            sb.AppendLine(value);

            return sb;
        }
    }
}