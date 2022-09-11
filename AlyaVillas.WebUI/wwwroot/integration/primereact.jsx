
import ReactWithDotNet from "../ReactWithDotNet.jsx";

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
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.PrimeReact.Panel::GetHeaderTemplate", (key)=> ReactWithDotNet.GetExternalJsObject(key) );