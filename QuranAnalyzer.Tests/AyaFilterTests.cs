using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuranAnalyzer;

    [TestClass]
    public class AyaFilterTests
    {
        [TestMethod]
        public void All()
        {
            var records = AyaFilter.Filter("42:*").Value;

            records.Count.Should().Be(43);
        }
    }
