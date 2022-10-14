using System.IO;
using System.Reflection;
using System.Text.Json;
using Newtonsoft.Json;
using ReactWithDotNet.PrimeReact;
using ReactWithDotNet.react_simple_code_editor;
using static ReactWithDotNet.UIDesigner.Extensions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ReactWithDotNet.UIDesigner;

class UIDesignerView : ReactComponent<UIDesignerModel>
{
    //static JsClientFunctionInfo<int> InitializeUIDesignerEvents = new(nameof(InitializeUIDesignerEvents));
    //static JsClientEventInfo OnBrowserInactive = new(nameof(OnBrowserInactive));

    //public void Refresh()
    //{
    //    SaveState();
    //}

    //protected override void componentDidMount()
    //{
    //    ClientTask.ListenEvent(OnBrowserInactive, Refresh);
    //    ClientTask.CallJsFunction(InitializeUIDesignerEvents, 1000);
    //}

    protected override void constructor()
    {
        state = StateCache.ReadState() ?? new UIDesignerModel();

        var defaultAssemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + Path.DirectorySeparatorChar;

        var suggestionDirectories = new List<string>();
        if (state.SelectedFolderSuggestions is not null)
        {
            suggestionDirectories.AddRange(state.SelectedFolderSuggestions);
        }

        if (!suggestionDirectories.Contains(defaultAssemblyDirectory))
        {
            suggestionDirectories.Add(defaultAssemblyDirectory);
        }

        state.SelectedFolderSuggestions = suggestionDirectories;
    }

    protected override Element render()
    {
        return new FolderSelectionTextBox();
        var propertyPanel = new FlexColumn(Padding(5))
        {
            BuildFolderSelectionPart(),
            Space(10),
            BuildAssemblySelectionPart(),
            Space(10),
            new MethodSelectionView
            {
                SelectedMethodTreeNodeKey = state.SelectedMethodTreeNodeKey,
                OnSelectionChange = e =>
                {
                    SaveState();

                    state.SelectedMethodName = null;
                    state.MetadataToken      = null;

                    state.SelectedMethodTreeNodeKey = e.value;

                    string typeReference = null;

                    var fullAssemblyPath = Path.Combine(state.SelectedFolder, state.SelectedAssembly);

                    string fullClassName = null;

                    var node = MethodSelectionView.FindTreeNode(fullAssemblyPath, state.SelectedMethodTreeNodeKey);
                    if (node is not null)
                    {
                        if (node.IsClass)
                        {
                            fullClassName = $"{node.NamespaceName}.{node.Name}";
                        }

                        if (node.IsMethod)
                        {
                            fullClassName = $"{node.DeclaringTypeFullName}";

                            state.MetadataToken      = node.MetadataToken;
                            state.SelectedMethodName = node.Name;
                        }
                    }

                    if (fullClassName is not null)
                    {
                        typeReference = $"{fullClassName},{Path.GetFileNameWithoutExtension(state.SelectedAssembly)}";
                    }

                    state.SelectedComponentTypeReference = typeReference;

                    if (typeReference != null)
                    {
                        var json = StateCache.ReadFromCache(typeReference + state.MetadataToken);
                        if (json.HasValue())
                        {
                            state.SelectedDotNetMemberSpecification = JsonConvert.DeserializeObject<DotNetMemberSpecification>(json);
                        }
                        else
                        {
                            state.SelectedDotNetMemberSpecification = new DotNetMemberSpecification();
                        }
                    }

                    SaveState();
                },
                AssemblyFilePath = state.SelectedFolder.HasValue() && state.SelectedAssembly.HasValue() ? Path.Combine(state.SelectedFolder, state.SelectedAssembly) : null
            }.render(),
            Space(10),
            new Slider { max = 100, min = 0, value = state.ScreenWidth, onChange = OnWidthChanged, style = { margin = "10px", padding = "5px" } },

            new TabView
            {
                new TabPanel
                {
                    header = "Instance json",
                    children =
                    {
                        new Editor
                        {
                            valueBind = () => state.SelectedDotNetMemberSpecification.JsonTextForDotNetInstanceProperties,
                            highlight = "json",
                            style     = { minHeight = "200px", border = "1px dashed blue", fontSize = "16px", fontFamily = "ui-monospace,SFMono-Regular,SF Mono,Menlo,Consolas,Liberation Mono,monospace" }
                        }
                    }
                },
                new TabPanel
                {
                    header = "Parameters json",
                    children =
                    {
                        new Editor
                        {
                            valueBind = () => state.SelectedDotNetMemberSpecification.JsonTextForDotNetMethodParameters,
                            highlight = "json",
                            style     = { minHeight = "200px", border = "1px dashed blue", fontSize = "16px", fontFamily = "ui-monospace,SFMono-Regular,SF Mono,Menlo,Consolas,Liberation Mono,monospace" }
                        }
                    }
                }
            }
        };

        var outputPanel = new div
        {
            children =
            {
                createElement()
            },
            style =
            {
                border = "1px dashed #e0e0e0",
                width  = state.ScreenWidth + "%",
                height = "100%"
            }
        };

        Element createElement()
        {
            return new iframe { src = "/ReactWithDotNetDesigner.ComponentPreview", style = { width_height = "100%", border = "none" } };
        }

        var mainPanel = new Splitter
        {
            layout = SplitterLayoutType.horizontal,
            style =
            {
                width  = "100%",
                height = "100%"
            },

            children =
            {
                new SplitterPanel
                {
                    size = 25,
                    children =
                    {
                        propertyPanel
                    }
                },
                new SplitterPanel(DisplayFlex, JustifyContentCenter)
                {
                    size = 75,

                    children =
                    {
                        outputPanel
                    }
                }
            }
        };

        return new div
        {
            children =
            {
                mainPanel
            },
            style =
            {
                width = "100%", height = "100%", padding = "7px"
            }
        };
    }

    Element BuildAssemblySelectionPart()
    {
        var SelectedFolder   = state.SelectedFolder;
        var SelectedAssembly = state.SelectedAssembly;
        var LastQuery        = state.SelectedAssemblyLastQuery;

        var suggestions = new List<string>();

        if (!string.IsNullOrWhiteSpace(SelectedFolder))
        {
            try
            {
                suggestions = Directory.EnumerateFiles(SelectedFolder).Select(Path.GetFileName).Where(x => x.Contains(LastQuery ?? "", StringComparison.OrdinalIgnoreCase)).Take(10).ToList();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        return new div
        {
            style = { display = "flex", flexDirection = "column" },
            children =
            {
                new label { text = "Search Assembly", style = { marginBottom = "5px", fontWeight = "bold" } },

                new span
                {
                    className = "p-fluid",
                    children =
                    {
                        new AutoComplete
                        {
                            suggestions = suggestions,

                            value          = SelectedAssembly,
                            onChange       = OnAssemblyChanged,
                            completeMethod = OnAssemblyCompleted
                        }
                    }
                }
            }
        };
    }

    Element BuildFolderSelectionPart()
    {
        var SelectedFolder = state.SelectedFolder;
        var LastQuery      = state.SelectedFolderLastQuery;
        var Suggestions    = state.SelectedFolderSuggestions;

        var autoComplete = new AutoComplete<Pair>
        {
            field = "Key",
            dropdown = true,
            forceSelection = true,
            autoFocus = true,
            delay = 1000,
            suggestions    = Suggestions.Where(x => x.Contains(LastQuery ?? "", StringComparison.OrdinalIgnoreCase)).ToList().Select(x=>new Pair{Key = x,Value = x}),
            value          = SelectedFolder,
            onChange       = OnFolderChange,
            completeMethod = OnFolderComplete,
            //itemTemplate = item => new FlexRow(AlignItemsCenter)
            //{
            //    new img { src = GetSvgUrl("Folder"), width = 20, height = 20 },

            //    new div { text = item, style = { marginLeft = "7px" } }
            //}
        };

        return new div
        {
            style = { display = "flex", flexDirection = "column", alignItems = "stretch" },
            children =
            {
                new label { text = "Search Folder", style = { marginBottom = "5px", fontWeight = "bold" } },

                new span
                {
                    className = "p-fluid",
                    children =
                    {
                        autoComplete
                    }
                }
            }
        };
    }

    void OnAssemblyChanged(AutoCompleteChangeParams e)
    {
        state.SelectedAssembly = e.GetValue<string>();
    }

    void OnAssemblyCompleted(AutoCompleteCompleteMethodParams e)
    {
        state.SelectedAssemblyLastQuery = e.query;
    }

    //void OnFolderChange(AutoCompleteChangeParams<string> e)
    //{
    //    state.SelectedFolder = e.value;
    //}
    void OnFolderChange(AutoCompleteChangeParams<Pair> e)
    {
        state.SelectedFolder = e.value?.Key;
    }
    void OnFolderComplete(AutoCompleteCompleteMethodParams e)
    {
        state.SelectedFolderLastQuery = e.query;
    }

    void OnWidthChanged(SliderChangeParams e)
    {
        state.ScreenWidth = e.value;
    }

    void SaveState()
    {
        if (state.SelectedComponentTypeReference.HasValue())
        {
            var selectedDotNetMemberSpecificationAsJson = JsonSerializer.Serialize(state.SelectedDotNetMemberSpecification, new JsonSerializerOptions { WriteIndented = true, IgnoreNullValues = true });

            StateCache.SaveToCache(state.SelectedComponentTypeReference + state.MetadataToken, selectedDotNetMemberSpecificationAsJson);
        }

        StateCache.SaveState(state);
    }
}

class Pair
{
    public string Key { get; set; }
    public string Value { get; set; }
}


class FolderSelectionTextBox_ : ReactComponent
{
    public string selectedCountry1 { get; set; }

    public string LastQuery { get; set; }

    IReadOnlyList<string> Suggestions { get; set; } = new[]
    {
        // Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + Path.DirectorySeparatorChar
        "a",
        "abc",
        "abcde"
    };

    public string SelectedFolder { get; set; }
    
    protected override Element render()
    {
        //  <AutoComplete value={selectedCountry1} suggestions={filteredCountries} completeMethod={searchCountry} field="name" onChange={(e) => setSelectedCountry1(e.value)} aria-label="Countries" dropdownAriaLabel="Select Country" />


        var suggestions =   Suggestions.Where(x => x.Contains(LastQuery ?? "", StringComparison.OrdinalIgnoreCase)).ToList().Select(x => new Pair { Key = x, Value = x });
        
        return new AutoComplete<Pair,string>
        {
            field          = "Key",
            dropdown       = true,
            forceSelection = true,
            autoFocus      = true,
            delay          = 1000,
            suggestions    = suggestions,
            value          = SelectedFolder,
            onChange       = OnFolderChange,
            completeMethod = OnFolderComplete,
            itemTemplate = item => new FlexRow(AlignItemsCenter, Width(100), Height(50))
            {
                new img { src = GetSvgUrl("Folder"), width = 20, height = 20 },

                new div { text = item.Key, style = { marginLeft = "7px" } }
            }
        };
    }

    void OnFolderChange(AutoCompleteChangeParams<string> e)
    {
        SelectedFolder = e.value;
    }
    void OnFolderComplete(AutoCompleteCompleteMethodParams e)
    {
        LastQuery = e.query;
    }
}


class FolderSelectionTextBox : ReactComponent
{
    public string Value { get; set; }


    IReadOnlyList<string> Suggestions { get; set; } = new[]
    {
        // Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + Path.DirectorySeparatorChar
        "a",
        "abc",
        "abcde"
    };

    public string SelectedFolder { get; set; }

    protected override Element render()
    {
        var suggestions = Suggestions.Where(x => x.Contains(Value ?? "", StringComparison.OrdinalIgnoreCase)).ToList();

        return new ReactWithDotNet.Libraries.ReactSuite.AutoComplete
        {
            data = suggestions,
            value          = SelectedFolder,
            onChange       = OnChange
        };
    }

    IEnumerable<string> AAA => new List<string> { "b", "bc" };

    [CacheThisMethodByTheseParameters(nameof(AAA))]
    void OnChange(string value)
    {
        Value = value;
    }
}