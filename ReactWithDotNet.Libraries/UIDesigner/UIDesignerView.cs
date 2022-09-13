using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ReactWithDotNet.PrimeReact;
using ReactWithDotNet.react_simple_code_editor;
using static ReactWithDotNet.UIDesigner.Extensions;

namespace ReactWithDotNet.UIDesigner;

class UIDesignerView : ReactComponent<UIDesignerModel>
{
    #region Static Fields
    static JsClientEventInfo OnBrowserInactive = new(nameof(OnBrowserInactive));
    static JsClientFunctionInfo<int> InitializeUIDesignerEvents = new(nameof(InitializeUIDesignerEvents));
    #endregion

    #region Public Methods
    public void Refresh()
    {
        SaveState();
    }

    protected override Element render()
    {
        var propertyPanel = new VStack
        {
            style = { margin = "5px" },
            children =
            {
                BuildFolderSelectionPart(),
                new VSpace(10),
                BuildAssemblySelectionPart(),
                new VSpace(10),
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
                            state.JsonText = StateCache.ReadFromCache(typeReference + state.MetadataToken);
                        }

                        SaveState();
                    },
                    AssemblyFilePath = state.SelectedFolder.HasValue() && state.SelectedAssembly.HasValue() ? Path.Combine(state.SelectedFolder, state.SelectedAssembly) : null
                }.render(),
                new VSpace(10),
                new Slider { max = 100, min = 0, value = state.ScreenWidth, onChange = OnWidthChanged, style = { margin = "10px", padding = "5px" } },

                new div { text = state.SelectedComponentTypeReference + (state.SelectedMethodName.HasValue() ? $"::{state.SelectedMethodName}" : null) },

                new Editor
                {
                    valueBind = () => state.JsonText,
                    highlight = "json",
                    style     = { minHeight = "200px", border = "1px dashed blue", fontSize = "16px", fontFamily = "ui-monospace,SFMono-Regular,SF Mono,Menlo,Consolas,Liberation Mono,monospace" }
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
                    size = 40,
                    children =
                    {
                        propertyPanel
                    }
                },
                new SplitterPanel
                {
                    size  = 60,
                    style = { display = "flex", justifyContent = "center", alignItems = "center" },
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
    #endregion

    #region Methods
    protected override void componentDidMount()
    {
        ClientTask.ListenEvent(OnBrowserInactive, Refresh);
        ClientTask.CallJsFunction(InitializeUIDesignerEvents, 1000);
    }

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

        var autoComplete = new AutoComplete
        {
            suggestions    = Suggestions.Where(x => x.Contains(LastQuery ?? "", StringComparison.OrdinalIgnoreCase)).ToList(),
            value          = SelectedFolder,
            onChange       = OnFolderChange,
            completeMethod = OnFolderComplete,
            itemTemplate = item => new div
            {
                style = { display = "flex", alignItems = "center" },
                children =
                {
                    new img { src  = GetSvgUrl("Folder"), width = 20, height = 20 },
                    
                    new div { text = item, style = { marginLeft = "7px" } }
                }
            }
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

    void OnFolderChange(AutoCompleteChangeParams e)
    {
        state.SelectedFolder = e.GetValue<string>();
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
            StateCache.SaveToCache(state.SelectedComponentTypeReference + state.MetadataToken, state.JsonText);
        }

        StateCache.SaveState(state);
    }
    #endregion
}