
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

// react-xarrows
import Xarrow from "react-xarrows";


// react-simple-code-editor

    

import Editor  from 'react-simple-code-editor';
import { highlight, languages } from 'prismjs/components/prism-core';
import 'prismjs/components/prism-json';
import 'prismjs/themes/prism.css'; //Example style, you can use another

const GetHighlightFunction = (language) => code => highlight(code, languages[language]);









function GetComponentMap()
{
    // ReSharper disable once UseOfImplicitGlobalInFunctionScope
    if (process.env.NODE_ENV === 'production')
    {
        // specify your production required components
        return {
            // primereact
            "ReactWithDotNet.PrimeReact.Button": Button,
            "ReactWithDotNet.PrimeReact.InputText": InputText,
            "ReactWithDotNet.PrimeReact.InputTextarea": InputTextarea,
            "ReactWithDotNet.PrimeReact.BlockUI": BlockUI,
            "ReactWithDotNet.PrimeReact.Card": Card,
            "ReactWithDotNet.PrimeReact.TabView": TabView,
            "ReactWithDotNet.PrimeReact.TabPanel": TabPanel,
            "ReactWithDotNet.PrimeReact.Dropdown": Dropdown,
            "ReactWithDotNet.PrimeReact.Column": Column,
            "ReactWithDotNet.PrimeReact.DataTable": DataTable,
            "ReactWithDotNet.PrimeReact.Checkbox": Checkbox,
            "ReactWithDotNet.PrimeReact.InputMask": InputMask,

            // react-xarrows
            "ReactWithDotNet.react_xarrows.Xarrow": Xarrow,

            // react-prism-editor
            "ReactWithDotNet.react_simple_code_editor.Editor": Editor,
            "ReactWithDotNet.react_simple_code_editor.highlight": GetHighlightFunction
        };
    }

    // All components
    return {
        // primereact
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
        "ReactWithDotNet.PrimeReact.AutoComplete": AutoComplete,
        "ReactWithDotNet.PrimeReact.Tree": Tree,

        // react-xarrows
        "ReactWithDotNet.react_xarrows.Xarrow": Xarrow,

        // react-prism-editor
        "ReactWithDotNet.react_simple_code_editor.Editor": Editor,
        "ReactWithDotNet.react_simple_code_editor.highlight": GetHighlightFunction
    };
}

const componentMap = GetComponentMap();

ReactWithDotNet.FindComponentByFullName = function (componentFullName)
{
    return componentMap[componentFullName];
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


