using System.Threading.Tasks;
using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

namespace ReactWithDotNet.WebSite.Showcases;


class Firer : ReactComponent
{
    public int Id { get; set; }
    
    protected override Task constructor()
    {
        Client.ListenEvent<int>("XYZ", On_XYZ_Fired);
        
        return base.constructor();
    }
    
    void On_XYZ_Fired(int id)
    {
      
    }
    
    protected override Element render()
    {
        return new FlexRowCentered(Border(Solid(1,"red")), Padding(10), OnClick(OnClickHandler))
        {
            $"XYZ - Firer:{Id}"
        };
    }

    void OnClickHandler(MouseEvent obj)
    {
        Client.DispatchEvent("XYZ", Id);
    }
}





class Container : ReactComponent
{
    public int FiredId { get; set; }

    protected override Task constructor()
    {
        Client.ListenEvent<int>("XYZ", On_XYZ_Fired);
        
        return base.constructor();
    }

   

    void On_XYZ_Fired(int id)
    {
        FiredId = id;
    }
    
    void OnClickHandler(MouseEvent obj)
    {
        FiredId++;
    }

    protected override Element render()
    {
        return new FlexColumnCentered(Border(Solid(1, "#0f6")), Padding(10))
        {
            new FlexRowCentered(Border(Solid(1, "blue")), Padding(10), OnClick(OnClickHandler))
            {
                FiredId.ToString()
            },
            When(FiredId % 2 == 0, () => new FlexColumn
            {
                new Firer { Id = 1 },
                new Firer { Id = 2 },
                new Firer { Id = 3 },
                new Firer { Id = 4 },
                new Firer { Id = 5 }
            })
            

        };
    }

    
}



public class EventBusDemo : ReactPureComponent
{
    protected override Element render()
    {
        return new div(WidthHeightMaximized)
        {
            new Container()
        };
    }
}

