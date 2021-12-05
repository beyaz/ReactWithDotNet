using System.Linq;
using System.Text;

namespace ReactDotNet
{
    class PropInfo
    {
        public string Name, Type, Default, Description;
    }

    static class PropScrapper
    {
        public static PropInfo ParseProp(string line)
        {
            var prop = new PropInfo();

            var arr = line.Split(" ".ToCharArray());

            prop.Name        = arr[0];
            prop.Type        = arr[1];
            prop.Default     = arr[2];
            prop.Description = string.Join(" ", arr.Skip(3));

           


            return prop;
        }

        public static string ToCSharpProperty(string line)
        {
            var prop = ParseProp(line);

            var dotNetTypeName = prop.Type;
            if (dotNetTypeName == "boolean")
            {
                dotNetTypeName = "bool";
            }

            if (dotNetTypeName == "any")
            {
                dotNetTypeName = "string";
            }

            var sb = new StringBuilder();

            sb.AppendLine($"/// <summary>");
            sb.AppendLine($"///     {prop.Description}");
            sb.AppendLine($"///     <para>default: {prop.Default}</para>");
            sb.AppendLine($"/// </summary>");
            sb.AppendLine($"[React]");
            sb.AppendLine($"public {dotNetTypeName} {prop.Name} {{ get; set; }}");


           


            return sb.ToString();
        }
    }
}