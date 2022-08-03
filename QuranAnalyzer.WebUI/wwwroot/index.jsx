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

ReactWithDotNet.RegisterExternalJsObject("RegisterScrollEvents", RegisterScrollEvents);