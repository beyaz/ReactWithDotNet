using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Exporting.ExporterForPrimeReact;

[TestClass]
public class ExporterTest
{
    static string GetTsCode(string className)
    {
        // https://github.com/primefaces/primereact/tree/master/components/lib
        
        var rawUrlInGithub = $"https://raw.githubusercontent.com/primefaces/primereact/master/components/lib/{className}/{className}.d.ts";

        return new HttpClient().GetStringAsync(rawUrlInGithub).GetAwaiter().GetResult();
    }
    
    
    [TestMethod]
    public void Splitter()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode     = GetTsCode("splitter"),
            StartFrom            = "'ref'> {",
            ClassName            = "Splitter",
            SkipMembers          = new[] { "children", "onResizeEnd","pt" },
            IsContainer = true
        });
    }

    [TestMethod]
    public void SplitterPanel()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode("splitter"),
            StartFrom        = "interface SplitterPanelProps {",
            ClassName        = "SplitterPanel",
            SkipMembers      = new[] { "children", "style","pt" },
            IsContainer      = true
        });
    }

    [TestMethod]
    public void TabPanel()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode("tabview"),
            StartFrom        = "interface TabPanelProps {",
            ClassName        = "TabPanel",
            SkipMembers      = new[] { "children", "style", "pt" },
            IsContainer      = true
        });
    }

    [TestMethod]
    public void TabView()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode("tabview"),
            StartFrom        = "'ref'> {",
            ClassName        = "TabView",
            SkipMembers      = new[] { "children", "pt", "onBeforeTabChange", "onBeforeTabClose" },
            IsContainer      = true,
            ClassModifier = "partial"
        });
    }



    [TestMethod]
    public void Avatar()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode("avatar"),
            StartFrom        = "'ref'> {",
            ClassName        = "Avatar",
            SkipMembers      = new[] { "children", "pt", "onImageError" }
        });
    }


}