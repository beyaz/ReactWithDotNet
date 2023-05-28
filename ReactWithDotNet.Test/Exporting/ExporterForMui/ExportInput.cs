namespace ReactWithDotNet.Exporting.ExporterForMui;

public class ExportInput : Exporting.ExportInput
{
    public ExportInput()
    {
        OutputFileLocation = @"\MUI\Material\";

        NamespaceName = "MUI.Material";
    }

    public new readonly IReadOnlyDictionary<string, string> PropToDotNetTypeMap = new Dictionary<string, string>
    {
        { "TextField > defaultValue", "string" }
    };
}

