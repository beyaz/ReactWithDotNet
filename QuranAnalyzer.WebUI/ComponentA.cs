using System;
using ReactDotNet;

namespace QuranAnalyzer.WebUI;

[Serializable]
public class ComponentAModel
{
    public string A_Prop0 { get; set; }  
    public string A_Prop1 { get; set; } 
    public string A_Prop2 { get; set; }  
}

[Serializable]
public class ComponentBModel
{
    public string B_Prop0 { get; set; }  
    public string B_Prop1 { get; set; } 
    public string B_Prop2 { get; set; }  
}

[Serializable]
public class ComponentCModel
{
    public string C_Prop0 { get; set; }  
    public string C_Prop1 { get; set; } 
    public string C_Prop2 { get; set; }  
}


class ComponentCView : ReactComponent<ComponentCModel>
{
    public ComponentCView()
    {
        state = new ComponentCModel
        {
            C_Prop0 = "C_Prop0"
        };
    }

    void OnClick()
    {
        state.C_Prop1 = "C+Clicked";
    }
    public override Element render()
    {
        return new div
        {
            new div
            {
                new div
                {
                    new div{text = state.C_Prop0,onClick = OnClick , style = { border = "1px solid red", margin = "10px"}},
                    
                    new ReactDotNet.PrimeReact.InputText{ valueBind = ()=>state.C_Prop1},
                    new ReactDotNet.PrimeReact.InputText{ valueBind = ()=>state.C_Prop2}
                }
            }
        };
    }
}

class ComponentBView : ReactComponent<ComponentBModel>
{
    public ComponentBView()
    {
        state = new ComponentBModel
        {
            B_Prop0 = "B_Prop0"
        };
    }

    void OnClick()
    {
        state.B_Prop1 = "B+Clicked";
    }

    public override Element render()
    {
        return new div
        {
            new div{text = state.B_Prop0,onClick = OnClick , style = { border = "1px solid red", margin = "10px"}},
            new ReactDotNet.PrimeReact.InputText{ valueBind = () => state.B_Prop0},
            new ReactDotNet.PrimeReact.InputText{ valueBind = () => state.B_Prop1},
            new ReactDotNet.PrimeReact.InputText{ valueBind = () => state.B_Prop2},
            new ComponentCView()
        };
    }
}

class ComponentAView : ReactComponent<ComponentAModel>
{
    public ComponentAView()
    {
        state = new ComponentAModel
        {
            A_Prop0 = "A_Prop0"
        };
    }

    void OnClick()
    {
        state.A_Prop1 = "A+Clicked";
    }

    public override Element render()
    {
        return new div
        {
            new div{text = state.A_Prop0,onClick = OnClick , style = { border = "1px solid red", margin = "10px"}},
            
            new ReactDotNet.PrimeReact.InputText{ valueBind = () => state.A_Prop1},
            new ReactDotNet.PrimeReact.InputText{ valueBind = () => state.A_Prop2},
            new ComponentBView(),
        };
    }
}