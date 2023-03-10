namespace QuranAnalyzer.WebUI.Pages.AllInitialLettersCombined;

class View : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Başlangıç harflerinin yan yana yazılması ile oluşan büyük sayılar"),

            new p
            {
                "Başlangıç harflerinin içinde bulunduğu sure ile ilişkili olduğunu 'Başlangıç Harfleri' sayfasında detaylı olarak incelenmişti.",
                new br(),
                "Peki bu geçiş adetlerini yanyana yazsak acaba önümüze nasıl bir rakam çıkar?"
            },

            seperation,

            "Mesela Kaf harfi iki surede olmak üzere toplamda 114 defa geçer.",
            new br(),
            "Başlangıç harflerinin ait oldukları surelerdeki toplam geçiş adetlerini yan yana yazınca oluşan büyük rakam 19'un tam katıdır.",
            new br(),
            raisePanel(new TotalCounts()),

            new br(),
            "Yukarıdaki geçiş adetlerinden herhangi birini değiştirmeyi deneyebilirsiniz. Hesaplama gerçek zamanlı olarak çalışmaktadır.",
            seperation,

            "2. olarak bu geçiş adetlerini daha detaylı olarak yazalım. ",
            "Mesela Mim harfi toplamda 17 surede olmak üzere 8659 defa geçer. ",
            "Bu 17 suredeki geçiş adetlerini de bu toplamın önüne ekleyelim ve oluşan sayıyı inceleyelim. ",
            "Aşağıda bulunan paneldeki hesapla düğmesine basarak bu işlemi hesaplayabilirsiniz.",
            raisePanel(new TotalCountsWithDetail()),
            new br(),
            "Yukarıdaki geçiş adetlerinden herhangi birini değiştirmeyi deneyebilirsiniz.",
            seperation,

            "Son olarak olayı daha da zorlaştıralım. İlaveten sure numaraları da dahil edelim. ",
            "Mesela Mim harfi toplamda 17 surede olmak üzere 8659 defa geçer. ",
            "Bu 17 suredeki ", (strong)"sure no, o suredeki toplam geçiş adeti ", " şeklinde yan yana yazalım. Aşağıda bu işlemi hesaplayabilirsiniz.",
            raisePanel(new TotalCountsWithDetail { IncludeChapterNumbers = true }),
            new br(),
            "Yukarıdaki geçiş adetlerinden herhangi birini değiştirmeyi deneyebilirsiniz. Hesaplama gerçek zamanlı olarak çalışmaktadır.",
            seperation,

            new SubTitle("Sonuç"),
            new p
            {
                "Elbetteki herhangi bir sayının 19'a bölünme ihtimali 19 da 1 dir. " ,
                "Sayının ne kadar büyük olduğununun bir önemi yoktur.",
                new br(),
                "Burada insanı hayrete düşüren olayı bir örnek ile açıklayalım." ,
                new br(),
                "Sadece ",AsLetter(ArabicLetter.Miim)," harfini ele alalım. Bu başlangıç harfi ile başlayan 17 surelerde toplamda 8659 adet geçer. " ,

                "Eğer 46. suredeki ",AsLetter(ArabicLetter.Miim)," harfi bir fazla veya eksik olsaydı  hem bu yukarıda hesaplanan  büyük rakamlar 19'un katı olmazdı. ",
                "Hem 'Başlangıç Harfleri' kısmında gözlemlediğimiz Ha-Mim ile ilgili veriler olmazdı.",
                "Sadece bir yerden değil bir çok yerden kitlenen iç içe geçmeli bir yapı gibi düşünülebilir."
            },

            new p
            {
                "Kuranda özellikle ",(strong)"bu kitabın bir benzerinin oluştutulamayacağı"," vurgulanır. ",
                "19 sistemi keşfedilinceye kadar olay sadece edebi açıdan ele alınıyordu. Edebi mucize olarak ele alınıyordu. " ,
                "Bu edebi açıdan mucize olduğu yorumu ise bazı insanları tam tatmin etmiyordu. Çünkü edebiyat görecelidir. ",
                "Kimine göre Necip Fazıl daha iyi şairdir kimine göre de Nazım Hikmet. ",
                "Ama matematik yoruma daha kapalıdır. 2 + 2 Bağcılarda da 4 eder Berlinde de 4 eder. ",
                "Tüm denizlerdeki kum tanelerinin adetini kesin olarak bilen bir yaratıcı Kuranın içine de böyle bir örüntüyü eklemiş. ",
                "Böylelikle Kuranın korunacağı ve Kuranın bir benzerinin getirilemeyeceği iddaları 19 sistemi ile daha anlamlı hale gelmiş oluyor. ",
            },

            new div{ "Neden 19?", TextAlignCenter},
            new br(),
            "Elbette herşeyin sayısını bilen Allah istese bunu 19 değil 29 rakamı ile de yapabilirdi. Eğer bu örüntü 29 üzerine olsaydı neden 29 diye sorulacaktı. " ,
            new br(),
            new ul
            {
                (li)"Kuranın kapağını açtığınızda karşınıza çıkan ilk çümle yani Besmele 19 harftir.",
                (li)"Kuranda 'Kanıt' kelimesi (Arapça: beyyine) tam 19 defa geçmektedir.",
                (li)"Kuranda bahsi geçen tüm rakamların toplamı 19'un katıdır. Mesela Nuh'un yaşı ifade edilirken 1000 yıldan 50 eksik gibi bir ifade kullanılmış."
            },
            new p
            {
                " Buna benzer daha bir çok örnek verilebilir. " ,
                " Mühim olan buradaki tasarımı görebilmektir. " ,
                " Böylelikle Allah var mı? yok mu? Kuran Allah kelamı mı değil mi ? gibi şüpheler giderilmiş olacak.",
                " Kuran'ın Allah'dan geldiğine olan inancımızı sağlam bir zemine oturmuş olacağız. " ,
                " Artık bir sonraki aşamaya yani Kuranı anlama ve hayatımıza uygulama aşamasına geçebiliriz."
            }
        };

        static Element seperation() => new FlexRowCentered(MarginTopBottom(10)) { "* * *" };

        static Element raisePanel(Element element) => new div
        {
            BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), Padding(15), BorderRadiusForPanels, MarginTopBottom(10),
            element
        };
    }
}