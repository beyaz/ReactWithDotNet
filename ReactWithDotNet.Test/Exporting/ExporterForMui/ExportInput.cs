namespace ReactWithDotNet.Exporting.ExporterForMui;

public class ExportInput : Exporting.ExportInput
{
    public ExportInput()
    {
        OutputFileLocation = @"\MUI\Material\";

        NamespaceName = "MUI.Material";
    }
}