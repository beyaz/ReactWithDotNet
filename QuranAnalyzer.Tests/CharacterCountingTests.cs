using static QuranAnalyzer.QuranAnalyzerMixin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static QuranAnalyzer.ArabicCharacters;
using static QuranAnalyzer.VerseFilter;
using static QuranAnalyzer.FpExtensions;

namespace QuranAnalyzer;

[TestClass]
public class CharacterCountingTests
{

    [TestMethod]
    public void Sad_in_38_and_19_and_98()
    {
        Pipe(GetVerseList("38:*"), verses => GetCountOfCharacter(verses, Sad)).ShouldBe(29);
        Pipe(GetVerseList("19:*"), verses => GetCountOfCharacter(verses, Sad)).ShouldBe(26);
        Pipe(GetVerseList("7:*"), verses => GetCountOfCharacter(verses, Sad)).ShouldBe(98);

    }

    [TestMethod]
    public void Kaf_in_50_and_42()
    {
        Pipe(GetVerseList("50:*"), verses => GetCountOfCharacter(verses, Kaf)).ShouldBe(57);
        Pipe(GetVerseList("42:*"), verses => GetCountOfCharacter(verses, Kaf)).ShouldBe(57);
    }

        

    [TestMethod]
    public void ye_sin()
    {
        Pipe(GetVerseList("36:*"), verses => GetCountOfCharacter(verses, Ya)).ShouldBe(237);
        Pipe(GetVerseList("36:*"), verses => GetCountOfCharacter(verses, Sin)).ShouldBe(48);
    }

    [TestMethod]
    public void Bakara()
    {
        Pipe(GetVerseList("2:*"), verses => GetCountOfCharacter(verses, "م")).ShouldBe(2195);
        Pipe(GetVerseList("2:*"), verses => GetCountOfCharacter(verses, "ل")).ShouldBe(3202);
        Pipe(GetVerseList("2:*"), verses => GetCountOfCharacter(verses, "ا")).ShouldBe(4504);
        Pipe(GetVerseList("2:*"), verses => GetCountOfCharacter(verses, "ا", new CountingOption{UseElifCountsSpecifiedByRK = true})).ShouldBe(4502);
    }

    [TestMethod]
    public void İmran()
    {
        Pipe(GetVerseList("3:*"), verses => GetCountOfCharacter(verses, "م")).ShouldBe(1249);
        Pipe(GetVerseList("3:*"), verses => GetCountOfCharacter(verses, "ل")).ShouldBe(1892);
        Pipe(GetVerseList("3:*"), verses => GetCountOfCharacter(verses, "ا")).ShouldBe(2511);
        Pipe(GetVerseList("3:*"), verses => GetCountOfCharacter(verses, "ا", new CountingOption { UseElifCountsSpecifiedByRK = true })).ShouldBe(2521);
    }


    [TestMethod]
    public void Chapter_7()
    {
        Pipe(GetVerseList("7:*"), verses => GetCountOfCharacter(verses, Sad)).ShouldBe(98);
        Pipe(GetVerseList("7:*"), verses => GetCountOfCharacter(verses, Sad, new CountingOption{Use_Sad_in_Surah_7_Verse_69_in_word_bestaten = true})).ShouldBe(97);

        Pipe(GetVerseList("7:*"), verses => GetCountOfCharacter(verses, Mim)).ShouldBe(1164);
        Pipe(GetVerseList("7:*"), verses => GetCountOfCharacter(verses, Lam)).ShouldBe(1530);
        Pipe(GetVerseList("7:*"), verses => GetCountOfCharacter(verses, Elif)).ShouldBe(2521);
        Pipe(GetVerseList("7:*"), verses => GetCountOfCharacter(verses, Elif, new CountingOption { UseElifCountsSpecifiedByRK = true })).ShouldBe(2529);
    }
}