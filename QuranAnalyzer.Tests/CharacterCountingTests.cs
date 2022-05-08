using System.Linq;
using FluentAssertions;
using static QuranAnalyzer.Mixin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuranAnalyzer
{
    [TestClass]
    public class CharacterCountingTests
    {

        [TestMethod]
        public void Sad()
        {
            var sad = 13;

            GetCountOfCharacter(sad, new[] { 38 }).Should().Be(29);
            GetCountOfCharacter(sad, new[] { 19 }).Should().Be(26);
            GetCountOfCharacter(sad, new[] { 7 }).Should().Be(98);

        }

        [TestMethod]
        public void Kaf()
        {
            var kaf = 20;

            GetCountOfCharacter(kaf, new[] { 50 }).Should().Be(57);
            GetCountOfCharacter(kaf, new[] { 42 }).Should().Be(57);

            

            var count = 0;
            foreach (var sura in DataAccess.AllSura)
            {
                foreach (var aya in sura.aya)
                {
                    count += DataAccess.Analyze(aya).Count(x => x.HarfIndex == 26);
                }
            }

            count.Should().Be(25676);
        }

        

        [TestMethod]
        public void ye_sin()
        {
            var chapterNumber = 36;

            var ye  = 27;
            var sin = 11;

            GetCountOfCharacter(ye, new[] { chapterNumber }).Should().Be(237);
            GetCountOfCharacter(sin, new[] { chapterNumber }).Should().Be(48);
        }
        
    }
}