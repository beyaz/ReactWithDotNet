using System.Linq;

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
    }
}