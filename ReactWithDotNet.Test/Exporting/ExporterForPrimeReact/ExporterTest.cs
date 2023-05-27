using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Exporting.ExporterForPrimeReact;

[TestClass]
public class ExporterTest
{
    static string GetTsCode(string className)
    {
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
            ClassModifier = " ",
            SkipMembers          = new[] { "children", "onResizeEnd" },
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
            ClassModifier    = " ",
            SkipMembers      = new[] { "children", "style" },
        });
    }


}