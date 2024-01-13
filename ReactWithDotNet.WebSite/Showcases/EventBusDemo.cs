namespace ReactWithDotNet.WebSite.Showcases;

public class EventBusDemo : Component
{
    protected override Element render()
    {
        return new FlexRow(WidthHeightMaximized, CursorDefault, Gap(10))
        {
            new ComponentA(), new ComponentB()
        };
    }

    class ComponentA : Component
    {
        protected override Element render()
        {
            return new div(Size(400,300), Border(Solid(1,"red")))
            {
                new div{ nameof(ComponentA)},
                new ComponentA_0()
            };
        }
    }
    
    class ComponentA_0 : Component
    {
        public string Message { get; set; } = nameof(ComponentA_0);
        
        protected override Element render()
        {
            return new div(Size(300,200), Border(Solid(1,"red")))
            {
                new div{ Message },
                new ComponentA_0_0()
            };
        }
        
        protected override Task constructor()
        {
            Client.ListenEvent("MessageForA_0", OnMessageForA_0);
            return base.constructor();
        }

        Task OnMessageForA_0(string message)
        {
            Message = message;
            Client.DispatchEvent("MessageForB","5");
            return Task.CompletedTask;
        }
    }
    
    class ComponentA_0_0 : Component
    {
        protected override Element render()
        {
            return new div(Size(200,100), Border(Solid(1,"red")))
            {
                new div{ nameof(ComponentA_0_0)},
                OnClick(OnClickHandler)
            };
        }

        [ReactStopPropagation]
        Task OnClickHandler(MouseEvent e)
        {
            Client.DispatchEvent("MessageForA_0","1");
            
            return Task.CompletedTask;
        }
    }
    
    class ComponentB : Component
    {
        public string Message { get; set; } = nameof(ComponentB);
        
        protected override Task constructor()
        {
            Client.ListenEvent("MessageForB",OnMessageForB);
            return base.constructor();
        }

        Task OnMessageForB(string message)
        {
            Message = message;
            
            Client.DispatchEvent("MessageForA_0","2");
                
            return Task.CompletedTask;
        }

        protected override Element render()
        {
            return new div(Size(400,300), Border(Solid(1,"red")))
            {
                new div{ Message }
            };
        }
    }
}

public class EventBusDemo2 : PureComponent
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

