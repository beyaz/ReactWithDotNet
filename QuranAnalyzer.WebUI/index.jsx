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

// designer

import { AutoComplete } from 'primereact/autocomplete';
import { Tree } from 'primereact/tree';

        


import ReactWithDotNet from "./ReactWithDotNet";



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
    "ReactWithDotNet.PrimeReact.Button": Button,
    "ReactWithDotNet.PrimeReact.InputText": InputText,
    "ReactWithDotNet.PrimeReact.InputTextarea": InputTextarea,
    "ReactWithDotNet.PrimeReact.BlockUI": BlockUI,
    "ReactWithDotNet.PrimeReact.Card": Card,
    "ReactWithDotNet.PrimeReact.TabView": TabView,
    "ReactWithDotNet.PrimeReact.TabPanel": TabPanel,
    "ReactWithDotNet.PrimeReact.SplitterPanel": SplitterPanel,
    "ReactWithDotNet.PrimeReact.Splitter": Splitter,
    "ReactWithDotNet.PrimeReact.Slider": Slider,
    "ReactWithDotNet.PrimeReact.ListBox": ListBox,
    "ReactWithDotNet.PrimeReact.Dropdown": Dropdown,
    "ReactWithDotNet.PrimeReact.Column": Column,
    "ReactWithDotNet.PrimeReact.DataTable": DataTable,
    "ReactWithDotNet.PrimeReact.Checkbox": Checkbox,
    "ReactWithDotNet.PrimeReact.InputMask": InputMask,
    "ReactWithDotNet.react_xarrows.Xarrow": Xarrow,

    // designer
    "ReactWithDotNet.PrimeReact.AutoComplete": AutoComplete,
    "ReactWithDotNet.PrimeReact.Tree": Tree
    
};

ReactWithDotNet.FindComponentByFullName = function (componentFullName)
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

            ReactWithDotNet.DispatchEvent('MainContentDivScrollChanged', [scrollY]);
        }
        else
        {
            currentScrollY = scrollY;
        }
    }

    ReactWithDotNet.OnDocumentReady(function()
    {
        document.getElementById("main").addEventListener('scroll', handleScroll);
    });
}


