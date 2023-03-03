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
            DefinitionTsCode     = GetTsCode(nameof(Paper)),
            StartFrom            = "props: P & {",
            ClassName            = "Paper",
            ExportAsPartialClass = true,
            SkipMembers          = new[] { "children" },
            ExtraProps           = new[] { "string component" }
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
            SkipMembers      = new[] { "children", "TransitionComponent", "TransitionProps", "PopperComponent" },
            IsContainer = true
        });
    }

    [TestMethod]
    public void CardMedia()
    {
        MuiExporter.ExportToCSharpFile(new MuiExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(CardMedia)),
            StartFrom        = "props: P & {",
            ClassName        = "CardMedia",
            SkipMembers      = new[] { "children" },
            ExtraProps = new[]{ "string title" }
        });
    }


    [TestMethod]
    public void Divider()
    {
        MuiExporter.ExportToCSharpFile(new MuiExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Divider)),
            StartFrom        = "props: P & {",
            ClassName        = "Divider",
            SkipMembers      = new[] { "children" }
        });
    }

    [TestMethod]
    public void Typography()
    {
        MuiExporter.ExportToCSharpFile(new MuiExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Typography)),
            StartFrom        = "SystemProps<Theme> & {",
            ClassName        = "Typography",
            SkipMembers      = new[] { "children" },
            ExtraProps       = new[] { "string color", "string component" }
        });
    }

    [TestMethod]
    public void CardContent()
    {
        MuiExporter.ExportToCSharpFile(new MuiExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(CardContent)),
            StartFrom        = "props: P & {",
            ClassName        = "CardContent",
            SkipMembers      = new[] { "children" }
        });
    }

    [TestMethod]
    public void CardActions()
    {
        MuiExporter.ExportToCSharpFile(new MuiExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(CardActions)),
            StartFrom        = "<HTMLDivElement>> {",
            ClassName        = "CardActions",
            SkipMembers      = new[] { "children" }
        });
    }


    [TestMethod]
    public void TextField()
    {
        MuiExporter.ExportToCSharpFile(new MuiExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(TextField)),
            StartFrom        = "> {",
            ClassName        = "TextField",
            SkipMembers      = new[] { "children", "inputRef" },
            ExportAsPartialClass = true
        });
    }
}