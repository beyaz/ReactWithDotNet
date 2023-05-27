namespace ReactWithDotNet.ExporterForPrimeReact;

public class ExportInput
{
    public string ClassName { get; set; }

    public string DefinitionTsCode { get; set; }

    public IReadOnlyList<string> SkipMembers { get; set; }

    public string StartFrom { get; set; }

    public IReadOnlyList<string> ExtraProps { get; set; }

    public bool IsContainer { get; set; }

    public string ClassModifier { get; set; } = "sealed";

    public string BaseClassName { get; set; } = "ElementBase";

    public string OutputFileLocation { get; set; } = @"\PrimeReact\";

    public string NamespaceName { get; set; } = "PrimeReact";
}