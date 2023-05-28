namespace ReactWithDotNet.Exporting.ExporterForMui;

public class ExportInput : Exporting.ExportInput
{
    public ExportInput()
    {
        OutputFileLocation = @"\MUI\Material\";

        NamespaceName = "MUI.Material";

        PropToDotNetTypeMap = new Dictionary<string, string>
        {
            { "TextField > defaultValue", "string" }
        };
    }
}