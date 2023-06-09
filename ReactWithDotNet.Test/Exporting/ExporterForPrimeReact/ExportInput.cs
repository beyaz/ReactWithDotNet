namespace ReactWithDotNet.Exporting.ExporterForPrimeReact;

public class ExportInput : Exporting.ExportInput
{
    public ExportInput()
    {
        OutputFileLocation = @"\PrimeReact\";

        NamespaceName = "PrimeReact";

        PropToDotNetTypeMap = new Dictionary<string, string>
        {
            { $"{NamespaceName} > TabPanel > leftIcon", "string" },
            { $"{NamespaceName} > TabPanel > rightIcon", "string" },
            { $"{NamespaceName} > TabPanel > prevButton", "string" },
            { $"{NamespaceName} > TabPanel > nextButton", "string" },
            { $"{NamespaceName} > TabPanel > closeIcon", "string" }
        };
    }
}