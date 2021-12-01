using Bridge;

namespace ReactDotNet.PrimeReact
{
    [Enum(Emit.StringNameLowerCase)]
    public enum TooltipPositionType
    {
        top, bottom, left, right
    }

    [ObjectLiteral]
    public class TooltipOptions
    {
        public string className;
        public TooltipPositionType position;
    }


}