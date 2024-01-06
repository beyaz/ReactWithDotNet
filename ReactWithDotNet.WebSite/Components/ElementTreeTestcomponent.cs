namespace ReactWithDotNet.WebSite.Components;

class ElementTreeTestcomponent : PureComponent
{
    protected override Element render()
    {
        return new div(Size(300,300))
        {
            new div(Size(250,250))
            {
                new div(Size(200,200))
                {
                    new a 
                    {
                        new label
                        {
                            "abc"
                        }
                    }
                }
            }
        };
    }
}