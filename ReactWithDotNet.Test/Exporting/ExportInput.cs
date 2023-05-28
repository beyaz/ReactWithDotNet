namespace ReactWithDotNet.Exporting;

public class ExportInput
{
    public string ClassName { get; init; }

    public string DefinitionTsCode { get; init; }

    public IReadOnlyList<string> SkipMembers { get; init; }

    public string StartFrom { get; init; }

    public IReadOnlyList<string> ExtraProps { get; init; }

    public bool IsContainer { get; init; }

    public string ClassModifier { get; init; } = "sealed";

    public string BaseClassName { get; init; } = "ElementBase";

    public string OutputFileLocation { get; init; } = @"\____?___\";

    public string NamespaceName { get; init; } = "__?__";

    public IReadOnlyDictionary<string, string> PropToDotNetTypeMap { get; init; }
}