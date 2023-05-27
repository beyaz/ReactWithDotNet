namespace ReactWithDotNet.ExporterForPrimeReact;

public class ExportInput : ReactWithDotNet.ExportInput
{
    public ExportInput()
    {
        OutputFileLocation = @"\PrimeReact\";

        NamespaceName = "PrimeReact";
    }
}