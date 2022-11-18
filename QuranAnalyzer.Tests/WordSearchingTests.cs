using static QuranAnalyzer.Analyzer;

namespace QuranAnalyzer;

[TestClass]
[Ignore]
public class WordSearchingTests
{
    static void CountShouldBe(string searchWord, int expected)
    {
        var search = AnalyzeText(searchWord).Unwrap();

        VerseFilter.GetVerseList("*").Value.Sum(v => v.WordList.Count(w => w.HasValueAndSame(search))).Value.Should().Be(expected);
    }

    [TestMethod]
    public void Day_365()
    {
        const string yevm       = "يوم";
        const string ve_yevm    = "ويوم";
        const string el_yevm    = "اليوم";
        const string vel_yevm   = "واليوم";
        const string yevmen     = "يوما";
        const string li_yevm    = "ليوم";
        const string fel_yevm   = "فاليوم";
        const string bi_yevm    = "بيوم";
        const string bil_yevm   = "باليوم";
        const string vebil_yevm = "وباليوم";


        CountShouldBe(yevm,217);
        CountShouldBe(ve_yevm,44);
        CountShouldBe(el_yevm,41);
        CountShouldBe(vel_yevm,23);
        CountShouldBe(yevmen,16);
        CountShouldBe(li_yevm,8);
        CountShouldBe(fel_yevm,8);
        CountShouldBe(bi_yevm,5);
        CountShouldBe(bil_yevm,2);
        CountShouldBe(vebil_yevm,1);
    }


    [TestMethod]
    public void Adem_Isa()
    {
        // a d e m
        const string adem      = "اادم";
        const string ya_adem   = "يادم";
        const string li_adem   = "لِـَٔادَمَ";
        const string veya_adem = "و يادم";

        CountShouldBe(adem, 15);
        CountShouldBe(ya_adem, 4);
        CountShouldBe(li_adem, 5);
        CountShouldBe(veya_adem, 1);

        // i s a
        const string isa    = "عيسي";
        const string ve_isa = "وعيسي";
        const string ya_isa = "يعيسي";
        const string bi_isa = "بعيسي";

        CountShouldBe(isa, 12);
        CountShouldBe(ve_isa, 7);
        CountShouldBe(ya_isa, 4);
        CountShouldBe(bi_isa, 2);
    }
        

    [TestMethod]
    public void EndsWithNunVavNun()
    {
        var nunVavNun = AnalyzeText("نون").Unwrap();

        VerseFilter.GetVerseList("*").Value.Count(v => v.WordList.Last().EndsWith(nunVavNun)).Should().Be(133);
    }
}