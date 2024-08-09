namespace ReactWithDotNet.WebSite.Pages;

sealed class PageMilestones : PureComponent
{
    protected override Element render()
    {
        return new FlexColumn(Gap(32))
        {
            new div
            {
                new h4{ "Milestone 1: Core Concept" },
                new Progressbar{ Value = 95},
                new p
                {
                    "Core react conceps like Functional components, Class components, PureComponents and communication react js client and .net core server "
                }
            },
            new div
            {
                new h4{ "Milestone 2: Creating wrapper classes of popular react libraries like MUI, AntDesign" },
                new Progressbar{ Value = 5},
                new p
                {
                    "When this milestone completed, you can use MUI, or other famous components in your project. This insfrastructure is ready to use. We are plan to generate wrapper classes automatically."
                },
            },
            new div
            {
                new h4{ "Milestone 3: Use Client option" },
                new Progressbar{ Value = 3},
                new p
                {
                    "After this part finished, you can write js code in c# language. I'm planning mini .net runtime in js language. My plan is not to build a new c# to js compiler. My plan is creating a new MSIL code runner in js language. Not getting all .net core assemblies. Only required part of MSIL codes will be downloaded into client side."
                }
            },
            
            new div
            {
                new h4{ "Milestone 4: React Native Integration" },
                new Progressbar{ Value = 0},
                new p
                {
                    "React native user interfaces will be build by server driven ui"
                }
            }
        };

    }

    class Progressbar : PureComponent
    {
        public int Value { get; init; } = 10;
        
        protected override Element render()
        {
            return new div(SizeFull, BorderRadius(4), Border(1, solid, Gray300), Size(300,25))
            {
                new FlexRowCentered(Width(Value, 100), Background(linear_gradient(90,Gray100, Gray300)), HeightFull, FontSize11)
                {
                    
                },
                
                PositionRelative,
                new FlexRowCentered(PositionAbsolute,Inset0, FontWeight500)
                {
                    "%"+Value
                }
            };
        }
    }
}