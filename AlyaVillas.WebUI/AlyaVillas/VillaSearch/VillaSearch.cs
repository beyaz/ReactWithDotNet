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
                        new div("arama")
                    }
                },
                new VSpace(24),
                new div
                {
                    text = "Aile için Villalar",
                    
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
}