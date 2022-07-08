import { Button } from 'primereact/button';
import { InputText } from 'primereact/InputText';
import { InputTextarea } from 'primereact/InputTextarea';
import { BlockUI } from 'primereact/BlockUI';
import { Card } from 'primereact/Card';
import { TabView, TabPanel } from 'primereact/tabview';
import { Splitter, SplitterPanel } from 'primereact/splitter';
import { Slider } from 'primereact/Slider';
import { ListBox } from 'primereact/ListBox';
import { Dropdown } from 'primereact/Dropdown';
import { Column } from 'primereact/Column';
import { DataTable } from 'primereact/DataTable';
import { Checkbox } from 'primereact/Checkbox';
import { InputMask } from 'primereact/InputMask';
import Xarrow from "react-xarrows";

        


import ReactDotNet from "./ReactDotNet";



//const boxStyle = {border: "grey solid 2px", borderRadius: "10px", padding: "5px"};

//function SimpleExample() {

//    return (
//        <div>
//            <div style={{display: "flex", justifyContent: "space-evenly", width: "100%"}}>
//                        <div id="One" style={boxStyle}>hey</div>
//                        <p id="elem2" style={boxStyle}>hey25</p>

//                        <div id="two" style={{border: "grey solid 2px", borderRadius: "10px", padding: "5px", marginTop:"41px"}}>hey</div>
//                        <div id="three" style={{border: "grey solid 2px", borderRadius: "10px", padding: "5px", marginTop:"231px"}}>hey</div>
                        
//                        <Button label="Prime button"/>
//                    </div>

//                    <Xarrow start="One" //can be react ref
//                            end="elem2" //or an id
//                            labels = "Aloha"
//                            color="red" />

//                    <Xarrow start="two" //can be react ref
//                            end="elem2" //or an id
//                            labels = "Aloha"
//                            color="red" />

//<Xarrow start="three"  curveness={0.2}
//                            end="elem2" //or an id
//                            labels = "Aloha"
//                            color="red" />
//        </div>
       
//    );
//}

window.primereact =
{
    "Button": Button,
    "InputText": InputText,
    "InputTextarea": InputTextarea,
    "BlockUI": BlockUI
};
window.Xarrow = Xarrow;

var componentMap = {
    "ReactDotNet.PrimeReact.Button": Button,
    "ReactDotNet.PrimeReact.InputText": InputText,
    "ReactDotNet.PrimeReact.InputTextarea": InputTextarea,
    "ReactDotNet.PrimeReact.BlockUI": BlockUI,
    "ReactDotNet.PrimeReact.Card": Card,
    "ReactDotNet.PrimeReact.TabView": TabView,
    "ReactDotNet.PrimeReact.TabPanel": TabPanel,
    "ReactDotNet.PrimeReact.SplitterPanel": SplitterPanel,
    "ReactDotNet.PrimeReact.Splitter": Splitter,
    "ReactDotNet.PrimeReact.Slider": Slider,
    "ReactDotNet.PrimeReact.ListBox": ListBox,
    "ReactDotNet.PrimeReact.Dropdown": Dropdown,
    "ReactDotNet.PrimeReact.Column": Column,
    "ReactDotNet.PrimeReact.DataTable": DataTable,
    "ReactDotNet.PrimeReact.Checkbox": Checkbox,
    "ReactDotNet.PrimeReact.InputMask": InputMask,
    "ReactDotNet.react_xarrows.Xarrow": Xarrow
};

ReactDotNet.FindComponentByFullName = function (componentFullName)
{
    var component = componentMap[componentFullName];
    if (component !== undefined)
    {
        return component;
    }

    return null;
}

window.RegisterScrollEvents = function()
{
    var currentScrollY = 0;

    function handleScroll(e)
    {
        var scrollY = e.target.scrollTop;

        function canFireAction()
        {
            if (scrollY > 0)
            {
                return currentScrollY === 0;
            }

            if (currentScrollY > 0)
            {
                return true;   
            }

            return false;
        }

        if (canFireAction())
        {
            currentScrollY = scrollY;

            ReactDotNet.DispatchEvent('MainContentDivScrollChanged', [scrollY]);
        }
        else
        {
            currentScrollY = scrollY;
        }
    }

    ReactDotNet.OnDocumentReady(function()
    {
        document.getElementById("main").addEventListener('scroll', handleScroll);
    });
}


