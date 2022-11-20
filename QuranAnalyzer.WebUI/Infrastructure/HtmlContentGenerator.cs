using System.Text;

namespace QuranAnalyzer.WebUI;

sealed class HtmlContentGenerator
{
    public string[] Stylesheets { get; set; }

    public Type TargetReactComponent { get; set; }

    public string GetHtmlContent()
    {
        const string root = "wwwroot";

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

            "<script type='text/javascript'>alert('A');</script>",
            //"<script src='//cdn.jsdelivr.net/npm/eruda'></script>",
            //"<script type='text/javascript'>eruda.init();</script>",

            //"    <!-- Font -->",
            //"    <link rel='stylesheet' href='https://fonts.googleapis.com/css2?family=Google+Sans+Text:wght@400;500;700&display=swap'>",

            //$"    <link rel='stylesheet' href='{root}/index.css'>",

            //Stylesheets,

            "</head>",

            "<body>",
            "<div>tt</div>",
            "    <div id='app'>",
            $"        <script src='{root}/index.js'></script>",
            "    </div>",
            "</body>",

            "</html>",

            //"<script type='text/javascript'>",
            //"    ReactWithDotNet.RenderComponentIn({",
            //$"        fullTypeNameOfReactComponent: '{TargetReactComponent.GetFullName()}',",
            //"        containerHtmlElementId: 'app'",
            //"    });",
            //"</script>"
        };

        return lines.Aggregate(new StringBuilder(), (sb, line) => line.WriteTo(sb)).ToString();
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