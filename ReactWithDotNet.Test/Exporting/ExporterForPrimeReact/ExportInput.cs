namespace ReactWithDotNet.Exporting.ExporterForPrimeReact;

public class ExportInput : Exporting.ExportInput
{
    public ExportInput()
    {
        OutputFileLocation = @"\PrimeReact\";

        NamespaceName = "PrimeReact";
    }
}