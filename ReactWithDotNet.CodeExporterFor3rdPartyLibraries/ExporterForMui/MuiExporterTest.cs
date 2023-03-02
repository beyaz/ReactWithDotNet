using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

[TestClass]
public class MuiExporterTest
{
    [TestMethod]
    public void Paper()
    {
        MuiExporter.ExportToCSharpFile(new MuiExportInput
        {
            rawUrlInGithub = "https://raw.githubusercontent.com/mui/material-ui/master/packages/mui-material/src/Paper/Paper.d.ts",
            StartFrom      = "props: P & {",
            ClassName      = "Paper",
            SkipMembers    = new[] { "children" }
        });
    }
}