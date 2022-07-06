import { Button } from 'primereact/button';
import { InputText } from 'primereact/InputText';
import { InputTextarea } from 'primereact/InputTextarea';

import ReactDotNet from "./ReactDotNet";

import Xarrow from "react-xarrows";


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
    "InputTextarea": InputTextarea
};
window.Xarrow = Xarrow;



ReactDotNet.RenderComponentIn({
    fullTypeNameOfReactComponent: "QuranAnalyzer.WebUI.Pages.MainPage.View,QuranAnalyzer.WebUI",
    containerHtmlElementId: 'app'
});

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


