using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static QuranAnalyzer.QuranAnalyzerMixin;
using static QuranAnalyzer.ArabicLetter;
using static QuranAnalyzer.VerseFilter;

namespace QuranAnalyzer;

[TestClass]
public class CharacterCountingTests
{
    #region Public Methods
    
    
    [TestMethod]
    public void AnalyzeVerseTest()
    {
        CountShouldBe("*", Daad, 1686);

        Analyzer.AnalyzeVerse(new Verse { _text = "طه" }).Count.Should().Be(2);
        Analyzer.AnalyzeVerse(new Verse { _text = "طه" })[0].ArabicLetterIndex.Should().Be(15);
        Analyzer.AnalyzeVerse(new Verse { _text = "طه" })[1].ArabicLetterIndex.Should().Be(25);
    }

    [TestMethod]
    public void Chapter_10()
    {
        CountShouldBe("10:*", Raa, 257);
        CountShouldBe("10:*", Laam, 913);
        CountShouldBe("10:*", Alif, 1323);
        CountShouldBe("10:*", Alif, new MushafOptions { UseElifCountsSpecifiedByRK = true }, 1319);
    }

    [TestMethod]
    public void Chapter_11()
    {
        CountShouldBe("11:*", Raa, 325);
        CountShouldBe("11:*", Laam, 795);
        CountShouldBe("11:*", Laam, new MushafOptions { Use_Lam_SpecifiedByRK = true }, 794);
        CountShouldBe("11:*", Alif, 1373);
        CountShouldBe("11:*", Alif, new MushafOptions { UseElifCountsSpecifiedByRK = true }, 1370);
    }

    [TestMethod]
    public void Chapter_12()
    {
        CountShouldBe("12:*", Raa, 257);
        CountShouldBe("12:*", Laam, 812);
        CountShouldBe("12:*", Alif, 1315);
        CountShouldBe("12:*", Alif, new MushafOptions { UseElifCountsSpecifiedByRK = true }, 1306);
    }

    [TestMethod]
    public void Chapter_13()
    {
        CountShouldBe("13:*", Raa, 137);
        CountShouldBe("13:*", Miim, 260);
        CountShouldBe("13:*", Laam, 480);
        CountShouldBe("13:*", Alif, 610);
        CountShouldBe("13:*", Alif, new MushafOptions { UseElifCountsSpecifiedByRK = true }, 605);
    }

    [TestMethod]
    public void Chapter_14()
    {
        CountShouldBe("14:*", Raa, 160);
        CountShouldBe("14:*", Laam, 452);
        CountShouldBe("14:*", Alif, 589);
        CountShouldBe("14:*", Alif, new MushafOptions { UseElifCountsSpecifiedByRK = true }, 585);
    }

    [TestMethod]
    public void Chapter_15()
    {
        CountShouldBe("15:*", Raa, 96);
        CountShouldBe("15:*", Laam, 323);
        CountShouldBe("15:*", Alif, 493);
        CountShouldBe("15:*", Alif, new MushafOptions { UseElifCountsSpecifiedByRK = true }, 493);
    }

    [TestMethod]
    public void Chapter_19()
    {
        CountShouldBe("19:*", Kaaf, 137);
        CountShouldBe("19:*", Haa_, 175);
        CountShouldBe("19:*", Yaa, 343);
        CountShouldBe("19:*", Ayn, 117);
        CountShouldBe("19:*", Saad, 26);
    }

    [TestMethod]
    public void Chapter_2()
    {
        CountShouldBe("2:*", "م", 2195);
        CountShouldBe("2:*", "ل", 3202);
        CountShouldBe("2:*", "ا", 4504);
        CountShouldBe("2:*", "ا", new MushafOptions { UseElifCountsSpecifiedByRK = true }, 4502);
    }

    [TestMethod]
    public void Chapter_20_26_27_28()
    {
        CountShouldBe("19:*", Haa_, 175);
        CountShouldBe("20:*", Haa_, 251);

        CountShouldBe("20:*", Taa_, 28);
        CountShouldBe("26:*", Taa_, 33);
        CountShouldBe("27:*", Taa_, 27);
        CountShouldBe("28:*", Taa_, 19);

        CountShouldBe("26:*", Siin, 94);
        CountShouldBe("27:*", Siin, 94);
        CountShouldBe("28:*", Siin, 102);

        CountShouldBe("26:*", Miim, 484);
        CountShouldBe("28:*", Miim, 460);
    }

    [TestMethod]
    public void Chapter_3()
    {
        CountShouldBe("3:*", "م", 1249);
        CountShouldBe("3:*", "ل", 1892);
        CountShouldBe("3:*", "ا", 2511);
        CountShouldBe("3:*", "ا", new MushafOptions { UseElifCountsSpecifiedByRK = true }, 2521);
    }

    [TestMethod]
    public void Chapter_36()
    {
        CountShouldBe("36:*", Yaa, 237);
        CountShouldBe("36:*", Siin, 48);
    }

    [TestMethod]
    public void Chapter_40_41_42_43_44_45_46_Ha_Mim()
    {
        CountShouldBe("40:*,41:*,42:*,43:*,44:*,45:*,46:*", Haa, 292);
        CountShouldBe("40:*,41:*,42:*,43:*,44:*,45:*,46:*", Miim, 1855);
    }

    [TestMethod]
    public void Chapter_42()
    {
        CountShouldBe("42:*", Qaaf, 57);
    }

    [TestMethod]
    public void Chapter_50()
    {
        CountShouldBe("50:*", Qaaf, 57);
    }

    [TestMethod]
    public void Chapter_7()
    {
        CountShouldBe("7:*", Saad, 98);
        CountShouldBe("7:*", Saad, new MushafOptions { Use_Sad_in_Surah_7_Verse_69_in_word_bestaten = true }, 97);

        CountShouldBe("7:*", Miim, 1164);
        CountShouldBe("7:*", Laam, 1530);
        CountShouldBe("7:*", Alif, 2521);
        CountShouldBe("7:*", Alif, new MushafOptions { UseElifCountsSpecifiedByRK = true }, 2529);
    }

    [TestMethod]
    public void Sad_in_38_and_19_and_98()
    {
        CountShouldBe("38:*", Saad, 29);
        CountShouldBe("19:*", Saad, 26);
        CountShouldBe("7:* ", Saad, 98);
    }

    [TestMethod]
    public void Section_37()
    {
        CountShouldBe("40:*", Haa, 64);
        CountShouldBe("44:*", Haa, 16);
        CountShouldBe("45:*", Haa, 31);
        CountShouldBe("46:*", Haa, 36);

        CountShouldBe("40:*", Miim, 380);
        CountShouldBe("44:*", Miim, 150);
        CountShouldBe("45:*", Miim, 200);
        CountShouldBe("46:*", Miim, 225);
    }

    [TestMethod]
    public void Section_38()
    {
        CountShouldBe("42:*", Ayn, 98);
        CountShouldBe("42:*", Siin, 54);
        CountShouldBe("42:*", Qaaf, 57);
    }
    #endregion

    #region Methods
    static void CountShouldBe(string searchScript, string character, int expectedCount)
    {
        GetVerseList(searchScript).Then(verses => GetCountOfCharacter(verses, character)).ShouldBe(expectedCount);
    }

    static void CountShouldBe(string searchScript, string character, MushafOptions option, int expectedCount)
    {
        GetVerseList(searchScript).Then(verses => GetCountOfCharacter(verses, character, option)).ShouldBe(expectedCount);
    }
    #endregion
}