using ReactWithDotNet.__designer__;

namespace ReactWithDotNet.WebSite.Components;

class ElementTreeTestcomponent : PureComponent
{
    protected override Element render()
    {
        return new div(Size(300,300))
        {
            Designer.GetStyle(this.GetType())[0].Modifiers[0],
            new div(Size(250,250))
            {
                new div(Size(200,200))
                {
                    new a 
                    {
                        new label
                        {
                            "abc0"
                        },
                        new label
                        {
                            "abc1"
                        },
                        
                        new label
                        {
                            "abc2"
                        }
                    }
                }
            }
        };
    }
}