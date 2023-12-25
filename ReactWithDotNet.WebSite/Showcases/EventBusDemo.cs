namespace ReactWithDotNet.WebSite.Showcases;


public class EventBusDemo : PureComponent
{
    protected override Element render()
    {
        return new div(WidthHeightMaximized, CursorDefault)
        {
            new Container()
        };
    }
    
    
    
    class Container : Component
    {
        public int FiredId { get; set; }

        protected override Task constructor()
        {
            Client.ListenEvent<int>("XYZ", On_XYZ_Fired);
        
            return base.constructor();
        }

   

        Task On_XYZ_Fired(int id)
        {
            FiredId = id;
            return Task.CompletedTask;
        }
    
        Task OnClickHandler(MouseEvent obj)
        {
            FiredId++;
        
            return Task.CompletedTask;
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

    
        class Firer : Component
        {
            public int Id { get; set; }
    
            protected override Task constructor()
            {
                Client.ListenEvent<int>("XYZ", On_XYZ_Fired);
        
                return base.constructor();
            }
    
            Task On_XYZ_Fired(int id)
            {
                return Task.CompletedTask;
            }
    
            protected override Element render()
            {
                return new FlexRowCentered(Border(Solid(1,"red")), Padding(10), OnClick(OnClickHandler))
                {
                    $"XYZ - Firer:{Id}"
                };
            }

            Task OnClickHandler(MouseEvent obj)
            {
                Client.DispatchEvent("XYZ", Id);
        
                return Task.CompletedTask;
            }
        }
    
    }
}

