using System.Text;

namespace AlyaVillas.VillaSearch;

class VillaSearchViewModel
{
    public string CommonTitle { get; set; } = "Dubleks Özel Havuzlu Villa";
    public int YatakSayisi { get; set; } = 2;
    public int BanyoSayisi { get; set; } = 1;
    public int ToplamdaKacKişilik { get; set; } = 7;
    public decimal ToplamFiyat { get; set; } = 7.500M;
}

class VillaSearchView : ReactComponent
{
    public VillaSearchViewModel Model { get; set; }
    
    protected override Element render()
    {
        var model = Model ?? new VillaSearchViewModel();
        
        
        return new div
        {
            style = { border = "1px solid #DBDBDB", display = "flex", flexDirection = "column" , paddingLeftRight = "10px"},
            children =
            {
                new VSpace(24),
                new div
                {
                    style = { boxShadow = "0px 4px 8px rgba(16, 24, 64, 0.08)", padding = "16px"},
                    children =
                    {
                        BuildGrisCıkışTarih()
                    }
                },
                new VSpace(24),
                new div
                {
                    text = "Aile için Villalar",
                    Style = Style.ParseCss(@"

/* Display/D-700 */

font-family: 'Marcellus';
font-style: normal;
font-weight: 400;
font-size: 32px;
line-height: 40px;

/* Neutral/N900 */

color: #4A4A49;
")
                },
                new div
                {
                    children=
                    {
                        new VillaSearchResultCardView
                        {
                            Model = new VillaSearchResultCardModel
                            {
                                BanyoSayisi = 3, CommonTitle = "Villa 1", ToplamFiyat = 7.600M,ToplamdaKacKişilik = 6, YatakSayisi = 4
                            }
                        },
                        new VSpace(16),
                        new VillaSearchResultCardView
                        {
                            Model = new VillaSearchResultCardModel
                            {
                                BanyoSayisi = 3, CommonTitle = "Villa 1", ToplamFiyat = 7.600M,ToplamdaKacKişilik = 6, YatakSayisi = 4
                            }
                        },
                        new VSpace(16),
                        new VillaSearchResultCardView
                        {
                            Model = new VillaSearchResultCardModel
                            {
                                BanyoSayisi = 3, CommonTitle = "Villa 1", ToplamFiyat = 7.600M,ToplamdaKacKişilik = 6, YatakSayisi = 4
                            }
                        }
                    }
                }
                
            }
        };

        
    }

    Element BuildGrisCıkışTarih()
    {
        return new VStack
        {
            new div("Girş - Çıkış Tarih")
            {
                Style = Style.ParseCss(@"/* Headline / H-400 */

font-family: 'Open Sans';
font-style: normal;
font-weight: 600;
font-size: 14px;
line-height: 24px;





color: #4A4A49;")
            },
            
            new div("Seçiniz")
            {
                Style = Style.ParseCss(@"


font-family: 'Open Sans';
font-style: normal;
font-weight: 400;
font-size: 14px;
line-height: 20px;
/* identical to box height, or 143% */


/* Neutral/N400 */

color: #98999D;
")
            }
        };

    }
}