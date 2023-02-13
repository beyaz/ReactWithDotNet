

// import core library
import ReactWithDotNet from "./react-with-dotnet/react-with-dotnet";

// you can comment these imports according to your project dependency
import "./react-with-dotnet/libraries/mui-core/all";
import "./react-with-dotnet/libraries/google-map-react/all";
import "./react-with-dotnet/libraries/primereact/all";
import "./react-with-dotnet/libraries/react-awesome-reveal/all";
import "./react-with-dotnet/libraries/react-datepicker/all";
import "./react-with-dotnet/libraries/react-free-scrollbar/all";
import "./react-with-dotnet/libraries/react-xarrows/all";
import "./react-with-dotnet/libraries/rsuite/all";
import "./react-with-dotnet/libraries/swiper/all";
import "./react-with-dotnet/libraries/uiw-react-codemirror/all";
import "./react-with-dotnet/libraries/framer-motion/all";


var currentScrollY = 0;

document.addEventListener('scroll', () => 
{
    var scrollY = window.scrollY;

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

        ReactWithDotNet.DispatchEvent('MainContentDivScrollChangedOverZero', [scrollY]);
    }
    else
    {
        currentScrollY = scrollY;
    }
});


export { ReactWithDotNet };