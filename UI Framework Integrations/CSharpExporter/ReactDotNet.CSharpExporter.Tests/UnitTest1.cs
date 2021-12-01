using System.Linq;
using FluentAssertions;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactDotNet.CSharpExporter.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {


            var url = "https://primefaces.org/primereact/showcase/#/button";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var t = doc.DocumentNode.SelectNodes(@"//div[contains(@class, 'doc-tablewrapper')]");

            var nodes =
                doc.DocumentNode.Descendants("div")
                   .Where(n => n.HasClass("doc-tablewrapper")).ToList();


        }

    }
}
