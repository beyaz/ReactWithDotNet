namespace ReactWithDotNet.Exporting.ExporterForPrimeReact;

public class ExportInput : Exporting.ExportInput
{
    public ExportInput()
    {
        OutputFileLocation = @"\PrimeReact\";

        NamespaceName = "PrimeReact";

        PropToDotNetTypeMap = new Dictionary<string, string>
        {
            
            //{ $"{NamespaceName} > * > sx", "dynamic" },
            //{ $"{NamespaceName} > * > inputProps", "dynamic" },
            //{ $"{NamespaceName} > TextField > defaultValue", "string" },
            //{ $"{NamespaceName} > TextField > type", "string" },
            //{ $"{NamespaceName} > TextField > value", "string" },
            //{ $"{NamespaceName} > SwitchBase > type", "string" },
            //{ $"{NamespaceName} > SwitchBase > value", "string" }
        };
    }
}