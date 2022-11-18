using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages;

public class WhoIsReshadKhalifePage : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new VSpace(10),
            new LargeTitle("Reşad Halife Kimdir? Ne söylüyor?"),
            new VSpace(15),

            new li
            {
                "Ey İslam dünyası! Sizler Muhammed peygamberi putlaştırıyorsunuz. Yüceler Yücesi Olan'ın yanında sürekli Muhammed peygamberimizi ekliyorsunuz. ",
                "Tek Olan'ı tek olarak anamıyorsunuz sürekli yanına Muhammed kelimesini yerleştiriyorsunuz. Hatta Kuranda mescitlerde En Kudretli Olanın adı tek olarak anılsın diye açık emir varken siz Allah yazılarının yanına sürekli Muhammed yazıları ekliyorsunuz. " ,
                "En Merhametli Olan'dan diler gibi Muhammed peygamberden şefaat dileniyorsunuz. ",
                "Tüm bunları yaparak Kurandaki şu ilkeleri çiğnemiş oluyorsunuz. ",
                new FlexColumn(MarginLeft(30))
                {
                    new FlexRow
                    {
                        Text("- Mescitlerde Allah'ın adı tek olarak anılmalıdır."),
                    },
                    new FlexRow
                    {
                        Text("- Peygamberler arasında ayrım yapılmamalıdır."),
                    }
                },
                
                "Özetle Şeytan sizi tamda sevdiğiniz damarınızdan siz farkettirmeden yakalamış durumda"
            },

          
            
            new div(MarginTop(50))
            {
                (b)"Zekatınızı aylık maaşınızı aldığınızda vermelisiz. ",
                new FlexColumn(MarginLeft(30))
                {
                    new li
                    {
                        Text("Mescitlerde Allah'ın adı tek olarak anılmalıdır."),
                    },
                    new li
                    {
                        Text("Peygamberler arasında ayrım yapılmamalıdır."),
                    }
                },

                "Özetle Şeytan sizi tamda sevdiğiniz damarınızdan siz farkettirmeden yakalamış durumda"
            },

            new VSpace(15)
        };
    }
}