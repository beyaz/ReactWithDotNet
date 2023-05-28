namespace ReactWithDotNet.Exporting.ExporterForMui;

public class ExportInput : Exporting.ExportInput
{
    public ExportInput()
    {
        OutputFileLocation = @"\MUI\Material\";

        NamespaceName = "MUI.Material";

        PropToDotNetTypeMap = new Dictionary<string, string>
        {
            { $"{NamespaceName} > * > classes", "init_only_style_map" },
            { $"{NamespaceName} > * > sx", "dynamic" },
            { $"{NamespaceName} > * > inputProps", "dynamic" },
            { $"{NamespaceName} > TextField > defaultValue", "string" },
            { $"{NamespaceName} > TextField > rows", "int?" }
        };
    }
}