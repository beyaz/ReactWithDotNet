const isProduction = process.env.NODE_ENV === 'production';

// import core library
import ReactWithDotNet from "./ReactWithDotNet";

// import helper tool for design your component in hotreload mode
import "./integration/ReactWithDotNet-UIDesigner";


// import libraries which you use in your porject
import "./integration/primereact";
import "./integration/react-xarrows";


// your app specific imports and codes
import './app.css'


var currentScrollY = 0;

function OnMainDivScrollChanged(e)
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

ReactWithDotNet.RegisterExternalJsObject("OnMainDivScrollChanged", OnMainDivScrollChanged);
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.GetExternalJsObject", ReactWithDotNet.GetExternalJsObject);

import React from 'react';
import { Ripple } from 'primereact/ripple';
const QuranAnalyzer_WebUI_PanelHeaderTemplate = (options) => 
{
    const toggleIcon = options.collapsed ? 'pi pi-chevron-down' : 'pi pi-chevron-up';

    return (
        <div className={options.className} style={{justifyContent:'start', background:'#fefeff'}}>
            <button className={options.togglerClassName} onClick={options.onTogglerClick}>
                <span className={toggleIcon}></span>
                <Ripple/>
            </button>
            <span className={`${options.titleClassName} pl-1`}>
                {options.props.header}
            </span>
        </div>
    );
}

ReactWithDotNet.RegisterExternalJsObject("QuranAnalyzer_WebUI_PanelHeaderTemplate", QuranAnalyzer_WebUI_PanelHeaderTemplate);
