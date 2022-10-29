using System.Text;

namespace QuranAnalyzer.WebUI;

sealed class IndexFileContent
{
    public Type Component { get; set; }

    public string[] Head { get; set; }
    public string RootFolderName { get; set; }

    public string GetFileContent()
    {
        var sb = new StringBuilder();

        foreach (var item in GetLines())
        {
            foreach (var line in item)
            {
                sb.AppendLine(line);
            }
        }

        return sb.ToString();
    }

    static string GetFullName(Type type)
    {
        return $"{type.FullName},{type.Assembly.GetName().Name}";
    }

    IEnumerable<IReadOnlyList<string>> GetLines()
    {
        
        
        return new List<List<string>>
        {
            new()
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
                "",
                "    <!-- Font -->",
                "    <link rel='stylesheet' href='https://fonts.googleapis.com/css2?family=Google+Sans+Text:wght@400;500;700&display=swap'>",
                "",
                $"    <link rel='stylesheet' href='{RootFolderName}/index.css'>",
                "</head>",
            },

            new()
            {
                "<body>",
                "    <div id='app'>",
                $"        <script src='{RootFolderName}/index.js'></script>",
                "    </div>",
                "</body>",
            },

            new()
            {
                "</html>",

                "",

                "<script type='text/javascript'>",
                "",
                "    ReactWithDotNet.RenderComponentIn({",
                $"        fullTypeNameOfReactComponent: '{GetFullName(Component)}',",
                "        containerHtmlElementId: 'app'",
                "    });",
                "",
                "</script>"
            }
        };
    }
}