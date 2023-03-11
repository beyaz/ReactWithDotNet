namespace QuranAnalyzer.WebUI.Pages;

public class PageWhyFamousPeopleAreSilent : ReactPureComponent
{
    protected override Element render()
    {
        return new Article
        {
            new LargeTitle("Neden Susuyorlar"),

            new p
            {
                @"Madem bu 19 meselesi bu kadar müthiş veriler içeriyor peki neden medyadaki bir çok hoca bu meseleden bahsetmiyor?",
                
            },
            new p
            {
                @"Gayet haklı bir soru.",
                new br(),
                "Tarih boyu en karlı sömürülerden biri de din üzerinden yapılan sömürüdür. " ,
                "Tüm medyatik din adamlarının tek amacı insanları sömürmektir gibi saçma bir cümle elbet kurmayacağım. ",
                "Medyadaki din adamları insanlara onların istedikleri şeyleri anlatıyorlar. " ,
                "Halk nazarında ise ayet, hadis, rivayet, Arap kültürü ve kendi örf adeti birbirine girdiği için kendi merak ettiği soruları soruyor.",
                new br(),
                new br(),
                "Sakız orucu bozar mı?",
                new br(),
                "Midye yemek günah mı?",
                new br(),
                "Böyle soruları mutlaka duymuşsunuzdur. Çoğunluk böyle sorular ile meşgul ise cevap verenler ne yapsın.",
                " Burada şunun özellikle altını çiziyorum. Halkı - çoğunluğu küçümsemek gibi bir niyetim yok." ,
                " Ben de bu halkın içinde yaşıyorum. Şunu kabul edelim. Kuran evlerimizde duvarların en güzel yerinde asılıdır.",
                " Eline alıp okuyan insanımız ise 'Arapçasından okumazsam sevap olmaz' diyerek anlamadığı bir dil ile okuyor sonrasında katpatıp yerine koyuyor.",
                " Yaradan bu kitapta ne diyor deyip de bir tercüme okuyan sayısı oldukça az. Burada çuvaldızı kendime batırmaktan da gocunmuyorum." ,
                " Ben de sözde bu ülkede üniversite mezunu mürekkep yalamış tayfayım benim dahi Kuran ile tanışmam 35 li yaşlarımda oldu."
            },
            new p
            {
                @"Madalyonun diğer yüzünde ise şu var.",
                new br(),
                "Bu 19 meselesinin sonu Reşad Halifenin elçiliğine çıkma ihtimali var." ,
                " Rivayetleri bırakıp Yalnız Kuran demek maalesef bir çoğunun işine gelmez.",
                " Haliyle hiç bir hoca bu meseleyi açıp da kariyerinden, işinden gücünden olmak istemez.",
                " Hadi olayın fikir boyutunu geçtim matematik kısmını bile açmazlar. 2+2 bana göre 4 ederken bir başkasına göre 5 etmiyor.",
                " YaSin suresindeki Y-S harfleri 1400 yıldır oradalar."
                
            },

              new p
              {
                  @"Peki medyatik olmayan Youtube vesaire bir çok ortamdaki araştıran insanlar ne durumdalar?",
                  new br(),
                  "Açıkçası burada biraz yelpaze geniş. Kimi insanlar evet Kuranda 19 vardır ama Reşad elçi değildir derler.",
                  " Kimisi bir yerde yapılan yanlış bir sayımı tüm sisteme mal edip her şeyi çöpe atıyor.",
                  " Kimisi gerçek 19 sistemi o değil benim bulduğumdur.",
                  " Özetle maalesef burada her türlü açılım var. Zaten bu siteyi kurma amacım da buydu."

              }

        };
    }
}