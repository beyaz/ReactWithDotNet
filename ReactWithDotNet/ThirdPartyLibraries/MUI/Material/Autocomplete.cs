// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

partial class Autocomplete 
{
    /// <summary>
    ///     Props applied to the [`Chip`](/material-ui/api/chip/) element.
    /// </summary>
    
    /// <summary>
    ///     The icon to display in place of the default clear icon.
    ///     <br/>
    ///     @default &lt;ClearIcon fontSize="small" /&gt;
    /// </summary>
    [ReactProp]
    public Element clearIcon { get; set; }
    
    /// <summary>
    ///     Override the default text for the *clear* icon button.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     For localization purposes, you can use the provided [translations](/material-ui/guides/localization/).
    ///     <br/>
    ///     @default 'Clear'
    /// </summary>
    [ReactProp]
    public string clearText { get; set; }
    
    /// <summary>
    ///     Override the default text for the *close popup* icon button.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     For localization purposes, you can use the provided [translations](/material-ui/guides/localization/).
    ///     <br/>
    ///     @default 'Close'
    /// </summary>
    [ReactProp]
    public string closeText { get; set; }
    
    /// <summary>
    ///     The props used for each slot inside.
    ///     <br/>
    ///     @default {}
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic componentsProps { get; } = new ExpandoObject();
    
    /// <summary>
    ///     If `true`, the component is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disabled { get; set; }
    
    /// <summary>
    ///     If `true`, the `Popper` content will be under the DOM hierarchy of the parent component.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disablePortal { get; set; }
    
    /// <summary>
    ///     Force the visibility display of the popup icon.
    ///     <br/>
    ///     @default 'auto'
    /// </summary>
    [ReactProp]
    public object forcePopupIcon { get; set; }
    
    /// <summary>
    ///     If `true`, the input will take up the full width of its container.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? fullWidth { get; set; }
    
    /// <summary>
    ///     The label to display when the tags are truncated (`limitTags`).
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {number} more The number of truncated tags.
    ///     <br/>
    ///     @returns {ReactNode}
    ///     <br/>
    ///     @default (more) =&gt; `+${more}`
    /// </summary>
    [ReactProp]
    public Element getLimitTagsText { get; set; }
    
    /// <summary>
    ///     The component used to render the listbox.
    ///     <br/>
    ///     @default 'ul'
    /// </summary>
    
    /// <summary>
    ///     Props applied to the Listbox element.
    /// </summary>
    
    /// <summary>
    ///     If `true`, the component is in a loading state.
    ///     <br/>
    ///     This shows the `loadingText` in place of suggestions (only if there are no suggestions to show, e.g. `options` are empty).
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? loading { get; set; }
    
    /// <summary>
    ///     Text to display when in a loading state.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     For localization purposes, you can use the provided [translations](/material-ui/guides/localization/).
    ///     <br/>
    ///     @default 'Loading…'
    /// </summary>
    [ReactProp]
    public Element loadingText { get; set; }
    
    /// <summary>
    ///     The maximum number of tags that will be visible when not focused.
    ///     <br/>
    ///     Set `-1` to disable the limit.
    ///     <br/>
    ///     @default -1
    /// </summary>
    [ReactProp]
    public double? limitTags { get; set; }
    
    /// <summary>
    ///     Text to display when there are no options.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     For localization purposes, you can use the provided [translations](/material-ui/guides/localization/).
    ///     <br/>
    ///     @default 'No options'
    /// </summary>
    [ReactProp]
    public Element noOptionsText { get; set; }
    
    /// <summary>
    ///     Override the default text for the *open popup* icon button.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     For localization purposes, you can use the provided [translations](/material-ui/guides/localization/).
    ///     <br/>
    ///     @default 'Open'
    /// </summary>
    [ReactProp]
    public string openText { get; set; }
    
    /// <summary>
    ///     The component used to render the body of the popup.
    ///     <br/>
    ///     @default Paper
    /// </summary>
    
    /// <summary>
    ///     The component used to position the popup.
    ///     <br/>
    ///     @default Popper
    /// </summary>
    
    /// <summary>
    ///     The icon to display in place of the default popup icon.
    ///     <br/>
    ///     @default &lt;ArrowDropDownIcon /&gt;
    /// </summary>
    [ReactProp]
    public Element popupIcon { get; set; }
    
    /// <summary>
    ///     If `true`, the component becomes readonly. It is also supported for multiple tags where the tag cannot be deleted.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? readOnly { get; set; }
    
    /// <summary>
    ///     Render the group.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {AutocompleteRenderGroupParams} params The group to render.
    ///     <br/>
    ///     @returns {ReactNode}
    /// </summary>
    [ReactProp]
    public Element renderGroup { get; set; }
    
    /// <summary>
    ///     Render the input.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {object} params
    ///     <br/>
    ///     @returns {ReactNode}
    /// </summary>
    [ReactProp]
    public Element renderInput { get; set; }
    
    /// <summary>
    ///     Render the option, use `getOptionLabel` by default.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {object} props The props to apply on the li element.
    ///     <br/>
    ///     @param {T} option The option to render.
    ///     <br/>
    ///     @param {object} state The state of each option.
    ///     <br/>
    ///     @param {object} ownerState The state of the Autocomplete component.
    ///     <br/>
    ///     @returns {ReactNode}
    /// </summary>
    [ReactProp]
    public Element renderOption { get; set; }
    
    /// <summary>
    ///     Render the selected value.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {T[]} value The `value` provided to the component.
    ///     <br/>
    ///     @param {function} getTagProps A tag props getter.
    ///     <br/>
    ///     @param {object} ownerState The state of the Autocomplete component.
    ///     <br/>
    ///     @returns {ReactNode}
    /// </summary>
    [ReactProp]
    public Element renderTags { get; set; }
    
    /// <summary>
    ///     The size of the component.
    ///     <br/>
    ///     @default 'medium'
    /// </summary>
    [ReactProp]
    public string size { get; set; }
    
    /// <summary>
    ///     The props used for each slot inside.
    ///     <br/>
    ///     @default {}
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic slotProps { get; } = new ExpandoObject();
}
