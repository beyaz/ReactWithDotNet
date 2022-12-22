
import ReactWithDotNet from "../react-with-dotnet";

// primereact
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { InputTextarea } from 'primereact/inputtextarea';
import { BlockUI } from 'primereact/blockui';
import { Card } from 'primereact/card';
import { TabView, TabPanel } from 'primereact/tabview';
import { Splitter, SplitterPanel } from 'primereact/splitter';
import { Slider } from 'primereact/slider';
import { ListBox } from 'primereact/listbox';
import { Dropdown } from 'primereact/dropdown';
import { Column } from 'primereact/column';
import { DataTable } from 'primereact/datatable';
import { Checkbox } from 'primereact/checkbox';
import { InputMask } from 'primereact/inputmask';
import { AutoComplete } from 'primereact/autocomplete';
import { Tree } from 'primereact/tree';
import { InputSwitch } from 'primereact/inputswitch';
import { Panel } from 'primereact/panel';
import { Tooltip } from 'primereact/tooltip';
import { ScrollPanel } from 'primereact/scrollpanel';
import { Message } from 'primereact/message';
import { Dialog } from 'primereact/dialog';

function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.PrimeReact." + name, value);
}

register("Button", Button);
register("InputText", InputText);
register("InputTextarea", InputTextarea);
register("BlockUI", BlockUI);
register("Card", Card);
register("TabView", TabView);
register("TabPanel", TabPanel);
register("SplitterPanel", SplitterPanel);
register("Splitter", Splitter);
register("Slider", Slider);
register("ListBox", ListBox);
register("Dropdown", Dropdown);
register("Column", Column);
register("DataTable", DataTable);
register("Checkbox", Checkbox);
register("InputMask", InputMask);
register("AutoComplete", AutoComplete);
register("Tree", Tree);
register("InputSwitch", InputSwitch);
register("Panel", Panel);
register("Tooltip", Tooltip);
register("Message", Message);
register("ScrollPanel", ScrollPanel);
register("Dialog", Dialog);

register("Panel::GetHeaderTemplate", (key) => ReactWithDotNet.GetExternalJsObject(key));

register("GrabOnlyValueParameterFromCommonPrimeReactEvent", function (argumentsAsArray)
{
    //const originalEvent = argumentsAsArray[0].originalEvent;

    const value = argumentsAsArray[0].value;

    return [{ value: value }];
});

register("GrabWithoutOriginalEvent", function (argumentsAsArray)
{
    const newInstance = {};

    const obj = argumentsAsArray[0];

    for(var propertyName in obj)
	{
        if(obj.hasOwnProperty(propertyName))
        {
            const value = obj[propertyName];

            if (propertyName === 'originalEvent' && value && value._reactName)
            {
                continue;
            }

            newInstance[propertyName] = value;
        }
    }

    return [newInstance];
});