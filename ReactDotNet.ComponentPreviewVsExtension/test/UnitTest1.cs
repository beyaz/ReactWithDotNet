using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactDotNet.ComponentPreviewVsExtension
{
    [TestClass]
    public class FileSearchTest
    {
        #region Public Methods
        [TestMethod]
        public void GetConfigurationFilePathByComponentFilePath()
        {
            var cSharpFilePath = @"D:\work\git\ReactDotNet\ReactDotNet.Demo\bin\Debug\bridge.js";

            var expected = @"D:\work\git\ReactDotNet\ReactDotNet.Demo\ReactDotNet.ComponentPreview.json";

            FileSearch.GetConfigurationFilePathByComponentFilePath(cSharpFilePath).Should().Be(expected);
        }
        #endregion
    }
}