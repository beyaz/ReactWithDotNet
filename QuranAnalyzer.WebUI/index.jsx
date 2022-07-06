import React from 'react';
import ReactDOM from 'react-dom';
import { Button } from 'primereact/button';
import {createRoot} from 'react-dom/client';
import ReactDotNet from "./ReactDotNet";

import Xarrow from "react-xarrows";


const boxStyle = {border: "grey solid 2px", borderRadius: "10px", padding: "5px"};

function SimpleExample() {

    return (
        <div>
            <div style={{display: "flex", justifyContent: "space-evenly", width: "100%"}}>
                        <div id="One" style={boxStyle}>hey</div>
                        <p id="elem2" style={boxStyle}>hey25</p>

                        <div id="two" style={{border: "grey solid 2px", borderRadius: "10px", padding: "5px", marginTop:"41px"}}>hey</div>
                        <div id="three" style={{border: "grey solid 2px", borderRadius: "10px", padding: "5px", marginTop:"231px"}}>hey</div>
                        
                        <Button label="Prime button"/>
                    </div>

                    <Xarrow start="One" //can be react ref
                            end="elem2" //or an id
                            labels = "Aloha"
                            color="red" />

                    <Xarrow start="two" //can be react ref
                            end="elem2" //or an id
                            labels = "Aloha"
                            color="red" />

<Xarrow start="three"  curveness={0.2}
                            end="elem2" //or an id
                            labels = "Aloha"
                            color="red" />
        </div>
       
    );
}

createRoot(document.getElementById('app')).render(<SimpleExample />);

window.primereact = { "Button": Button};
window.ReactDotNet = ReactDotNet;


