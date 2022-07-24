
import './app.css'

import ReactWithDotNet from "./ReactWithDotNet";

// primereact
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
import { AutoComplete } from 'primereact/autocomplete';
import { Tree } from 'primereact/tree';
import { InputSwitch } from 'primereact/inputswitch';
import { Panel } from 'primereact/panel';
import { Tooltip } from 'primereact/tooltip';

// react-xarrows
import Xarrow from "react-xarrows";


// react-simple-code-editor
import Editor  from 'react-simple-code-editor';
import { highlight, languages } from 'prismjs/components/prism-core';
import 'prismjs/components/prism-json';
import 'prismjs/themes/prism.css';

const GetHighlightFunction = (language) =>
{
    const lang = languages[language];
    
     return code =>
     {
         if (code == null)
         {
             return "";
         }
         return highlight(code, lang);
     };
};


RegisterScrollEvents = function()
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



function InitializeUIDesignerEvents(timeoutInMilliseconds)
{
    OnBrowserInactive(timeoutInMilliseconds, () =>
    {
        ReactWithDotNet.DispatchEvent('OnBrowserInactive',[]);
    });
}

function OnBrowserInactive(timeoutInMilliseconds, callback)
{
    var wait = setTimeout(callback, timeoutInMilliseconds);
    document.onmousemove = document.mousedown = document.mouseup = document.onkeydown = document.onkeyup = document.focus = function ()
    {
        clearTimeout(wait);
        wait = setTimeout(callback, timeoutInMilliseconds);
    };
}


if (process.env.NODE_ENV === 'production')
{
    // specify your production required components or functions
}
else
{
    // primereact
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Button", Button);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.InputText", InputText);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.InputTextarea", InputTextarea);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.BlockUI", BlockUI);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Card", Card);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.TabView", TabView);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.TabPanel", TabPanel);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.SplitterPanel", SplitterPanel);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Splitter", Splitter);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Slider", Slider);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.ListBox", ListBox);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Dropdown", Dropdown);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Column", Column);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.DataTable", DataTable);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Checkbox", Checkbox);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.InputMask", InputMask);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.AutoComplete", AutoComplete);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Tree", Tree);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.InputSwitch", InputSwitch);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Panel", Panel);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Tooltip", Tooltip);
    
    
    

    // react-xarrows
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.react_xarrows.Xarrow", Xarrow);

    // react-prism-editor
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.react_simple_code_editor.Editor", Editor);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.react_simple_code_editor.highlight", GetHighlightFunction);

    ReactWithDotNet.RegisterExternalJsObject("RegisterScrollEvents", RegisterScrollEvents);
    ReactWithDotNet.RegisterExternalJsObject("InitializeUIDesignerEvents", InitializeUIDesignerEvents);
}