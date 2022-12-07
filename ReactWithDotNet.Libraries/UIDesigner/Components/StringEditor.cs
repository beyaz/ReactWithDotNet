using ReactWithDotNet.Libraries.PrimeReact;

namespace ReactWithDotNet.Libraries.UIDesigner.Components;

public sealed class ValueInfo
{
    public string Label { get; set; }
    public string Value { get; set; }
}

class ValueInfoStringEditor : ReactComponent
{
    public ValueInfo Model { get; set; } = new();
    public int Index { get; set; }

    public Expression<Func<string>> valueBind;

    protected override Element render()
    {
        return new FlexRow(WidthMaximized)
        {
            Model.Label,new input{type = "text", valueBind = valueBind}
        };
    }

    ////[ReactDelay(400)]
    //void OnChange(ChangeEvent e)
    //{
    //    Model.Value = e.target.value;
        
    //    DispatchEvent(()=>ValueChanged,Model,Index);
    //}
    
    //[ReactCustomEvent]
    //public Action<ValueInfo,int> ValueChanged { get; set; }
}


//class ValueInfoListEditor : ReactComponent
//{
//    public IReadOnlyCollection<ValueInfo> ValueInfoList { get; set; } = new List<ValueInfo>();

//    protected override Element render()
//    {
//        return new FlexColumn
//        {
//            ValueInfoList.Select((x,i)=>new ValueInfoStringEditor{ Model = x,Index=i, ValueChanged = ValueChanged})


            
//        };
//    }

//    void ValueChanged(ValueInfo arg1, int arg2)
//    {
        
//    }
//}
