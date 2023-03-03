using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

[TestClass]
public class MuiExporterTest
{
    static string GetTsCode(string className)
    {
        var rawUrlInGithub = $"https://raw.githubusercontent.com/mui/material-ui/master/packages/mui-material/src/{className}/{className}.d.ts";
            
        return new HttpClient().GetStringAsync(rawUrlInGithub).GetAwaiter().GetResult();
    }
    [TestMethod]
    public void Paper()
    {
        MuiExporter.ExportToCSharpFile(new MuiExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Paper)),
            StartFrom      = "props: P & {",
            ClassName      = "Paper",
            ExportAsPartialClass = true,
            SkipMembers    = new[] { "children" }
        });
    }

    [TestMethod]
    public void Card()
    {
        MuiExporter.ExportToCSharpFile(new MuiExportInput
        {
            DefinitionTsCode     = GetTsCode(nameof(Card)),
            StartFrom            = "DistributiveOmit<PaperProps, 'classes'> & {",
            ClassName            = "Card",
            ExportAsPartialClass = true,
            SkipMembers          = new[] { "children" }
        });
    }

    [TestMethod]
    public void Tooltip()
    {
        MuiExporter.ExportToCSharpFile(new MuiExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Tooltip)),
            StartFrom        = "'title'> {",
            ClassName        = "Tooltip",
            SkipMembers      = new[] { "children", "TransitionComponent", "TransitionProps", "PopperComponent" }
        });
    }
}