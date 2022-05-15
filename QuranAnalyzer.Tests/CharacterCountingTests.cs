using System.Linq;
using FluentAssertions;
using static QuranAnalyzer.Mixin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static QuranAnalyzer.FpExtensions;

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

            
        }

        

        [TestMethod]
        public void ye_sin()
        {
            Pipe(AyaFilter.Filter("36:*"), verses => GetCountOfCharacter(verses, ArabicCharacters.Ya)).Value.Should().Be(237);
            Pipe(AyaFilter.Filter("36:*"), verses => GetCountOfCharacter(verses, ArabicCharacters.Sin)).Value.Should().Be(48);
        }

        [TestMethod]
        public void Bakara()
        {
            Pipe(AyaFilter.Filter("2:*"), verses => GetCountOfCharacter(verses, "م")).Value.Should().Be(2195);
            Pipe(AyaFilter.Filter("2:*"), verses => GetCountOfCharacter(verses, "ل")).Value.Should().Be(3202);
            Pipe(AyaFilter.Filter("2:*"), verses => GetCountOfCharacter(verses, "ا")).Value.Should().Be(4504);
            Pipe(AyaFilter.Filter("2:*"), verses => GetCountOfCharacter(verses, "ا", new CountingOption{UseElifCountsSpecifiedByRK = true})).Value.Should().Be(4502);
        }
    }
}