using System;
using System.Collections.Generic;
using static QuranAnalyzer.QuranAnalyzerMixin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static QuranAnalyzer.ArabicCharacters;
using static QuranAnalyzer.VerseFilter;
using static QuranAnalyzer.FpExtensions;

namespace QuranAnalyzer;

[TestClass]
public class CharacterCountingTests
{

    
    static void CountShouldBe(string searchScript, string character,  int expectedCount)
    {
        GetVerseList(searchScript).Then(verses => GetCountOfCharacter(verses, character)).ShouldBe(expectedCount);
    }
    static void CountShouldBe(string searchScript, string character, CountingOption option, int expectedCount)
    {
        GetVerseList(searchScript).Then(verses => GetCountOfCharacter(verses, character, option)).ShouldBe(expectedCount);
    }

    [TestMethod]
    public void Sad_in_38_and_19_and_98()
    {
        CountShouldBe("38:*",Sad,29);
        CountShouldBe("19:*",Sad,26);
        CountShouldBe("7:* ",Sad,98);

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

    [TestMethod]
    public void Chapter_10()
    {
        Pipe(GetVerseList("10:*"), verses => GetCountOfCharacter(verses, Ra)).ShouldBe(257);
        Pipe(GetVerseList("10:*"), verses => GetCountOfCharacter(verses, Lam)).ShouldBe(913);
        Pipe(GetVerseList("10:*"), verses => GetCountOfCharacter(verses, Elif)).ShouldBe(1323);
        Pipe(GetVerseList("10:*"), verses => GetCountOfCharacter(verses, Elif, new CountingOption { UseElifCountsSpecifiedByRK = true })).ShouldBe(1319);
    }

    [TestMethod]
    public void Chapter_11()
    {
        Pipe(GetVerseList("11:*"), verses => GetCountOfCharacter(verses, Ra)).ShouldBe(325);
        Pipe(GetVerseList("11:*"), verses => GetCountOfCharacter(verses, Lam)).ShouldBe(795);
        Pipe(GetVerseList("11:*"), verses => GetCountOfCharacter(verses, Lam, new CountingOption {Use_Lam_SpecifiedByRK = true})).ShouldBe(794);
        Pipe(GetVerseList("11:*"), verses => GetCountOfCharacter(verses, Elif)).ShouldBe(1373);
        Pipe(GetVerseList("11:*"), verses => GetCountOfCharacter(verses, Elif, new CountingOption { UseElifCountsSpecifiedByRK = true })).ShouldBe(1370);
    }


    [TestMethod]
    public void Chapter_12()
    {
        Pipe(GetVerseList("12:*"), verses => GetCountOfCharacter(verses, Ra)).ShouldBe(257);
        Pipe(GetVerseList("12:*"), verses => GetCountOfCharacter(verses, Lam)).ShouldBe(812);
        Pipe(GetVerseList("12:*"), verses => GetCountOfCharacter(verses, Elif)).ShouldBe(1315);
        Pipe(GetVerseList("12:*"), verses => GetCountOfCharacter(verses, Elif, new CountingOption { UseElifCountsSpecifiedByRK = true })).ShouldBe(1306);
    }

    [TestMethod]
    public void Chapter_13()
    {
        Pipe(GetVerseList("13:*"), verses => GetCountOfCharacter(verses, Ra)).ShouldBe(137);
        Pipe(GetVerseList("13:*"), verses => GetCountOfCharacter(verses, Mim)).ShouldBe(260);
        Pipe(GetVerseList("13:*"), verses => GetCountOfCharacter(verses, Lam)).ShouldBe(480);
        Pipe(GetVerseList("13:*"), verses => GetCountOfCharacter(verses, Elif)).ShouldBe(610);
        Pipe(GetVerseList("13:*"), verses => GetCountOfCharacter(verses, Elif, new CountingOption { UseElifCountsSpecifiedByRK = true })).ShouldBe(605);
    }


    [TestMethod]
    public void Chapter_14()
    {
        Pipe(GetVerseList("14:*"), verses => GetCountOfCharacter(verses, Ra)).ShouldBe(160);
        Pipe(GetVerseList("14:*"), verses => GetCountOfCharacter(verses, Lam)).ShouldBe(452);
        Pipe(GetVerseList("14:*"), verses => GetCountOfCharacter(verses, Elif)).ShouldBe(589);
        Pipe(GetVerseList("14:*"), verses => GetCountOfCharacter(verses, Elif, new CountingOption { UseElifCountsSpecifiedByRK = true })).ShouldBe(585);
    }

    [TestMethod]
    public void Chapter_15()
    {
        Pipe(GetVerseList("15:*"), verses => GetCountOfCharacter(verses, Ra)).ShouldBe(96);
        Pipe(GetVerseList("15:*"), verses => GetCountOfCharacter(verses, Lam)).ShouldBe(323);
        Pipe(GetVerseList("15:*"), verses => GetCountOfCharacter(verses, Elif)).ShouldBe(493);
        Pipe(GetVerseList("15:*"), verses => GetCountOfCharacter(verses, Elif, new CountingOption { UseElifCountsSpecifiedByRK = true })).ShouldBe(493);
    }


    [TestMethod]
    public void Chapter_19()
    {
        Pipe(GetVerseList("19:*"), verses => GetCountOfCharacter(verses, Kef)).ShouldBe(137);
        Pipe(GetVerseList("19:*"), verses => GetCountOfCharacter(verses, Ha)).ShouldBe(175);
        Pipe(GetVerseList("19:*"), verses => GetCountOfCharacter(verses, Ya)).ShouldBe(343);
        Pipe(GetVerseList("19:*"), verses => GetCountOfCharacter(verses, Ayn)).ShouldBe(117);
        Pipe(GetVerseList("19:*"), verses => GetCountOfCharacter(verses, Sad)).ShouldBe(26);
    }

    [TestMethod]
    public void Section_37()
    {
        Pipe(GetVerseList("40:*"), verses => GetCountOfCharacter(verses, HH)).ShouldBe(64);
        Pipe(GetVerseList("44:*"), verses => GetCountOfCharacter(verses, HH)).ShouldBe(16);
        Pipe(GetVerseList("45:*"), verses => GetCountOfCharacter(verses, HH)).ShouldBe(31);
        Pipe(GetVerseList("46:*"), verses => GetCountOfCharacter(verses, HH)).ShouldBe(36);


        Pipe(GetVerseList("40:*"), verses => GetCountOfCharacter(verses, Mim)).ShouldBe(380);
        Pipe(GetVerseList("44:*"), verses => GetCountOfCharacter(verses, Mim)).ShouldBe(150);
        Pipe(GetVerseList("45:*"), verses => GetCountOfCharacter(verses, Mim)).ShouldBe(200);
        Pipe(GetVerseList("46:*"), verses => GetCountOfCharacter(verses, Mim)).ShouldBe(225);
    }

    [TestMethod]
    public void Section_38()
    {
        Pipe(GetVerseList("42:*"), verses => GetCountOfCharacter(verses, Ayn)).ShouldBe(98);
        Pipe(GetVerseList("42:*"), verses => GetCountOfCharacter(verses, Sin)).ShouldBe(54);
        Pipe(GetVerseList("42:*"), verses => GetCountOfCharacter(verses, Kaf)).ShouldBe(57);
    }

    [TestMethod]
    public void Ha_Mim()
    {
        Pipe(GetVerseList("40:*,41:*,42:*,43:*,44:*,45:*,46:*"), verses => GetCountOfCharacter(verses, HH)).ShouldBe(292);
        Pipe(GetVerseList("40:*,41:*,42:*,43:*,44:*,45:*,46:*"), verses => GetCountOfCharacter(verses, Mim)).ShouldBe(1855);
    }

    [TestMethod]
    public void TH()
    {
        Pipe(GetVerseList("19:*"), verses => GetCountOfCharacter(verses, Ha)).ShouldBe(175);
        Pipe(GetVerseList("20:*"), verses => GetCountOfCharacter(verses, Ha)).ShouldBe(250); // todo: 251 olmalı

        Pipe(GetVerseList("20:*"), verses => GetCountOfCharacter(verses, T)).ShouldBe(28);
        Pipe(GetVerseList("26:*"), verses => GetCountOfCharacter(verses, T)).ShouldBe(33);
        Pipe(GetVerseList("27:*"), verses => GetCountOfCharacter(verses, T)).ShouldBe(27);
        Pipe(GetVerseList("28:*"), verses => GetCountOfCharacter(verses, T)).ShouldBe(19);

        Pipe(GetVerseList("26:*"), verses => GetCountOfCharacter(verses, Sin)).ShouldBe(94);
        Pipe(GetVerseList("27:*"), verses => GetCountOfCharacter(verses, Sin)).ShouldBe(94);
        Pipe(GetVerseList("28:*"), verses => GetCountOfCharacter(verses, Sin)).ShouldBe(102);

        Pipe(GetVerseList("26:*"), verses => GetCountOfCharacter(verses, Mim)).ShouldBe(484);
        Pipe(GetVerseList("28:*"), verses => GetCountOfCharacter(verses, Mim)).ShouldBe(460);
    }
}