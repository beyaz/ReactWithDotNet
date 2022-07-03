using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static QuranAnalyzer.QuranAnalyzerMixin;
using static QuranAnalyzer.ArabicCharacters;
using static QuranAnalyzer.VerseFilter;

namespace QuranAnalyzer;

[TestClass]
public class CharacterCountingTests
{
    #region Public Methods
    [TestMethod]
    public void Bakara()
    {
        CountShouldBe("2:*","م",2195);
        CountShouldBe("2:*","ل",3202);
        CountShouldBe("2:*","ا",4504);
        CountShouldBe("2:*","ا", new CountingOption { UseElifCountsSpecifiedByRK = true },4502);
    }

    [TestMethod]
    public void Chapter_10()
    {
        CountShouldBe("10:*",Ra,257);
        CountShouldBe("10:*",Lam,913);
        CountShouldBe("10:*",Elif,1323);
        CountShouldBe("10:*",Elif, new CountingOption { UseElifCountsSpecifiedByRK = true },1319);
    }

    [TestMethod]
    public void Chapter_11()
    {
        CountShouldBe("11:*",Ra,325);
        CountShouldBe("11:*",Lam,795);
        CountShouldBe("11:*",Lam, new CountingOption { Use_Lam_SpecifiedByRK = true },794);
        CountShouldBe("11:*",Elif,1373);
        CountShouldBe("11:*",Elif, new CountingOption { UseElifCountsSpecifiedByRK = true },1370);
    }

    [TestMethod]
    public void Chapter_12()
    {
        CountShouldBe("12:*",Ra,257);
        CountShouldBe("12:*",Lam,812);
        CountShouldBe("12:*",Elif,1315);
        CountShouldBe("12:*",Elif, new CountingOption { UseElifCountsSpecifiedByRK = true },1306);
    }

    [TestMethod]
    public void Chapter_13()
    {
        CountShouldBe("13:*",Ra,137);
        CountShouldBe("13:*",Mim,260);
        CountShouldBe("13:*",Lam,480);
        CountShouldBe("13:*",Elif,610);
        CountShouldBe("13:*",Elif, new CountingOption { UseElifCountsSpecifiedByRK = true },605);
    }

    [TestMethod]
    public void Chapter_14()
    {
        CountShouldBe("14:*",Ra,160);
        CountShouldBe("14:*",Lam,452);
        CountShouldBe("14:*",Elif,589);
        CountShouldBe("14:*",Elif, new CountingOption { UseElifCountsSpecifiedByRK = true },585);
    }

    [TestMethod]
    public void Chapter_15()
    {
        CountShouldBe("15:*",Ra,96);
        CountShouldBe("15:*",Lam,323);
        CountShouldBe("15:*",Elif,493);
        CountShouldBe("15:*",Elif, new CountingOption { UseElifCountsSpecifiedByRK = true },493);
    }

    [TestMethod]
    public void Chapter_19()
    {
        CountShouldBe("19:*",Kef,137);
        CountShouldBe("19:*",Ha,175);
        CountShouldBe("19:*",Ya,343);
        CountShouldBe("19:*",Ayn,117);
        CountShouldBe("19:*",Sad,26);
    }

    [TestMethod]
    public void Chapter_7()
    {
        CountShouldBe("7:*",Sad,98);
        CountShouldBe("7:*",Sad, new CountingOption { Use_Sad_in_Surah_7_Verse_69_in_word_bestaten = true },97);

        CountShouldBe("7:*",Mim,1164);
        CountShouldBe("7:*",Lam,1530);
        CountShouldBe("7:*",Elif,2521);
        CountShouldBe("7:*",Elif, new CountingOption { UseElifCountsSpecifiedByRK = true },2529);
    }

    [TestMethod]
    public void Ha_Mim()
    {
        CountShouldBe("40:*,41:*,42:*,43:*,44:*,45:*,46:*",HH,292);
        CountShouldBe("40:*,41:*,42:*,43:*,44:*,45:*,46:*",Mim,1855);
    }

    [TestMethod]
    public void İmran()
    {
        CountShouldBe("3:*","م",1249);
        CountShouldBe("3:*","ل",1892);
        CountShouldBe("3:*","ا",2511);
        CountShouldBe("3:*","ا", new CountingOption { UseElifCountsSpecifiedByRK = true },2521);
    }

    [TestMethod]
    public void Kaf_in_50_and_42()
    {
        CountShouldBe("50:*",Kaf,57);
        CountShouldBe("42:*",Kaf,57);
    }

    [TestMethod]
    public void Sad_in_38_and_19_and_98()
    {
        CountShouldBe("38:*", Sad, 29);
        CountShouldBe("19:*", Sad, 26);
        CountShouldBe("7:* ", Sad, 98);
    }

    [TestMethod]
    public void Section_37()
    {
        CountShouldBe("40:*",HH,64);
        CountShouldBe("44:*",HH,16);
        CountShouldBe("45:*",HH,31);
        CountShouldBe("46:*",HH,36);

        CountShouldBe("40:*",Mim,380);
        CountShouldBe("44:*",Mim,150);
        CountShouldBe("45:*",Mim,200);
        CountShouldBe("46:*",Mim,225);
    }

    [TestMethod]
    public void Section_38()
    {
        CountShouldBe("42:*",Ayn,98);
        CountShouldBe("42:*",Sin,54);
        CountShouldBe("42:*",Kaf,57);
    }

    [TestMethod]
    public void AnalyzeVerseTest()
    {
        DataAccess.AnalyzeVerse(new Verse { _text = "طه" }).Count.Should().Be(2);
        DataAccess.AnalyzeVerse(new Verse { _text = "طه" })[0].HarfIndex.Should().Be(15);
        DataAccess.AnalyzeVerse(new Verse { _text = "طه" })[1].HarfIndex.Should().Be(25);
    }
    [TestMethod]
    public void TH()
    {
        
        CountShouldBe("19:*",Ha,175);
        CountShouldBe("20:*",Ha,251);

        CountShouldBe("20:*",T,28);
        CountShouldBe("26:*",T,33);
        CountShouldBe("27:*",T,27);
        CountShouldBe("28:*",T,19);

        CountShouldBe("26:*",Sin,94);
        CountShouldBe("27:*",Sin,94);
        CountShouldBe("28:*",Sin,102);

        CountShouldBe("26:*",Mim,484);
        CountShouldBe("28:*",Mim,460);
    }

    [TestMethod]
    public void ye_sin()
    {
        CountShouldBe("36:*",Ya,237);
        CountShouldBe("36:*",Sin,48);
    }
    #endregion

    #region Methods
    static void CountShouldBe(string searchScript, string character, int expectedCount)
    {
        GetVerseList(searchScript).Then(verses => GetCountOfCharacter(verses, character)).ShouldBe(expectedCount);
    }

    static void CountShouldBe(string searchScript, string character, CountingOption option, int expectedCount)
    {
        GetVerseList(searchScript).Then(verses => GetCountOfCharacter(verses, character, option)).ShouldBe(expectedCount);
    }
    #endregion
}