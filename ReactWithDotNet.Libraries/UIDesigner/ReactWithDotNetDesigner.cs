using System.Reflection;
using Newtonsoft.Json;
using ReactWithDotNet.Libraries.PrimeReact;
using ReactWithDotNet.Libraries.uiw.react_codemirror;
using static ReactWithDotNet.UIDesigner.Extensions;

namespace ReactWithDotNet.UIDesigner;

public class ReactWithDotNetDesigner : ReactComponent<ReactWithDotNetDesignerModel>
{
    protected override void constructor()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        state.SelectedAssemblyFilePath ??= Assembly.GetEntryAssembly()?.Location;
    }

    protected override Element render()
    {
        const int width = 450;

        Element createJsonEditor()
        {
            Expression<Func<string>> valueBind = () => state.JsonTextForDotNetMethodParameters;

            if (state.IsInstanceEditorActive)
            {
                valueBind = () => state.JsonTextForDotNetInstanceProperties;
            }

            return new CodeMirror
            {
                extensions               = { "json", "githubLight" },
                valueBind                = valueBind,
                valueBindDebounceTimeout = 700,
                valueBindDebounceHandler = OnKeypressFinished,
                basicSetup =
                {
                    highlightActiveLine       = false,
                    highlightActiveLineGutter = false,
                },
                style =
                {
                    BorderRadius(3),
                    Border("1px solid #d9d9d9"),
                    FontSize11,
                    MaxWidth(width)
                }
            };
        }

        Element createLabel(string text)
        {
            return new small(Text(text), Color("rgb(73 86 193)"), FontWeight600);
        }

        var propertyPanel = new FlexColumn(PaddingLeftRight(5), Height("100%"), Width("100%"), FontSize15, PrimaryBackground)
        {
            new link { href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Mono&display=swap", rel = "stylesheet" },

            new FlexColumn(MarginLeftRight(3))
            {
                createLabel("Filter by class name"),

                new InputText
                {
                    valueBind                = () => state.ClassFilter,
                    valueBindDebounceTimeout = 700,
                    valueBindDebounceHandler = OnFilterChanged,
                    style                    = { FontSize12 }
                }
            },
            new FlexColumn(MarginLeftRight(3), MarginTopBottom(3))
            {
                createLabel("Filter by method name"),

                new InputText
                {
                    valueBind                = () => state.MethodFilter,
                    valueBindDebounceTimeout = 700,
                    valueBindDebounceHandler = OnFilterChanged,
                    style                    = { FontSize12 }
                }
            },

            new style
            {
                Text($@"



.token.property{{ {new Style { Color("#189af6") }.ToCssWithImportant()} }}

.cm-editor{{ {new Style { Height("calc(100% - 2px)"), Color("#DE3163") }.ToCssWithImportant()} }}

.ͼ16 {{ {new Style { FontWeight600, FontFamily("'IBM Plex Mono', monospace") }.ToCssWithImportant()} }}

")
            },
            Space(5),
            new MethodSelectionView
            {
                ClassFilter               = state.ClassFilter,
                MethodFilter              = state.MethodFilter,
                SelectedMethodTreeNodeKey = state.SelectedMethodTreeNodeKey,
                SelectionChanged          = OnElementSelected,
                AssemblyFilePath          = state.SelectedAssemblyFilePath,
                Width                     = width
            },

            Space(5),

            new fieldset
            {
                Border("1px solid #d9d9d9"),
                BorderRadius(4),
                new legend
                {
                    createLabel("Media Size")
                },
                new FlexRow(JustifyContentSpaceAround)
                {
                    MediaSizeButton(320),
                    MediaSizeButton(480),
                    MediaSizeButton(600),
                    MediaSizeButton(768),
                    MediaSizeButton(900),
                    MediaSizeButton(1024),
                    MediaSizeButton(1200)
                }
            },

            new FlexColumn(HeightMaximized)
            {
                // h e a d e r
                new FlexRow(Color("#6c757d"), CursorPointer, TextAlignCenter)
                {
                    When(canShowInstanceEditor(), new div(Text("Instance json"))
                    {
                        OnClick(_ => state.IsInstanceEditorActive = true),
                        When(state.IsInstanceEditorActive, BorderBottom("2px solid #2196f3"), Color("#2196f3"), FontWeight600),
                        Padding(10),
                        FlexGrow(1),
                        FontSize13
                    }),

                    When(canShowParametersEditor(), new div(Text("Parameters json"))
                    {
                        OnClick(_ => state.IsInstanceEditorActive = false),
                        When(!state.IsInstanceEditorActive, BorderBottom("2px solid #2196f3"), Color("#2196f3"), FontWeight600),
                        Padding(10),
                        FlexGrow(1),
                        FontSize13
                    })
                },

                // c o n t e n t
                createJsonEditor() + HeightMaximized
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
                Border("0.3px dashed #e0e0e0"),
                Width(state.ScreenWidth <= 100 ? state.ScreenWidth + "%" : state.ScreenWidth + "px"),
                HeightMaximized
            }
        };

        Element createElement()
        {
            return new iframe { src = "/ReactWithDotNetDesignerComponentPreview", style = { Border("none"), WidthMaximized, HeightMaximized } };
        }

        return new FlexRow(WidthHeight("100%"))
        {
            new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/themes/saga-blue/theme.css" },
            new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/primereact.min.css" },
            new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primeicons@5.0.0/primeicons.css" },

            new div(BorderRight("1px dotted #d9d9d9"), Width(width))
            {
                propertyPanel
            },
            new div(DisplayFlex, JustifyContentCenter, FlexGrow(1), Padding(7))
            {
                outputPanel
            }
        };
    }

    bool canShowInstanceEditor()
    {
        if (state.SelectedMethod?.IsStatic == true)
        {
            return false;
        }

        return true;
    }

    bool canShowParametersEditor()
    {
        if (state.SelectedMethod?.Parameters.Count > 0)
        {
            return true;
        }

        return false;
    }

    Element MediaSizeButton(int width)
    {
        const string BluePrimary = "#1976d2";

        var isSelected = width == state.ScreenWidth;

        return new FlexRowCentered
        {
            Id(width),
            $"{width}px",
            Color(BluePrimary),
            Border($"1px solid {BluePrimary}"),
            Background(isSelected ? "#dbdbe7" : "transparent"),
            BorderRadius(5),
            CursorPointer,
            OnClick(MediaSizeButtonClicked),
            Height(25),
            Width(50),
            FontSize12
        };
    }

    void MediaSizeButtonClicked(MouseEvent e)
    {
        state.ScreenWidth = int.Parse(e.FirstNotEmptyId);
    }

    void OnElementSelected(string keyOfSelectedTreeNode)
    {
        state.SelectedType   = null;
        state.SelectedMethod = null;

        state.JsonTextForDotNetInstanceProperties = null;
        state.JsonTextForDotNetMethodParameters   = null;

        state.SelectedMethodTreeNodeKey = keyOfSelectedTreeNode;

        var fullAssemblyPath = state.SelectedAssemblyFilePath;

        var node = MethodSelectionView.FindTreeNode(fullAssemblyPath, state.SelectedMethodTreeNodeKey, state.ClassFilter, state.MethodFilter);
        if (node is not null)
        {
            if (node.IsClass)
            {
                state.SelectedType = node.TypeReference;

                state = StateCache.TryRead(state.SelectedType) ?? state;
            }

            if (node.IsMethod)
            {
                state.SelectedMethod = node.MethodReference;

                state = StateCache.TryRead(state.SelectedMethod) ?? state;
            }
        }

        if (canShowInstanceEditor() && canShowParametersEditor() == false)
        {
            state.IsInstanceEditorActive = true;
        }

        if (canShowParametersEditor() && canShowInstanceEditor() == false)
        {
            state.IsInstanceEditorActive = false;
        }

        if (canShowInstanceEditor())
        {
            initializeInstanceJson();
        }

        if (canShowParametersEditor())
        {
            initializeParametersJson();
        }

        SaveState();
        
        void initializeInstanceJson()
        {
            var typeOfInstance = state.SelectedType;
            if (typeOfInstance == null)
            {
                typeOfInstance = state.SelectedMethod?.DeclaringType;
            }

            if (typeOfInstance == null)
            {
                return;
            }

            var map = JsonConvert.DeserializeObject<Dictionary<string, object>>(state.JsonTextForDotNetInstanceProperties ?? string.Empty);
            if (map == null)
            {
                map = new Dictionary<string, object>();
            }

            foreach (var propertyInfo in MetadataHelper.LoadAssembly(fullAssemblyPath).TryLoadFrom(typeOfInstance)?.GetProperties(BindingFlags.Instance | BindingFlags.Public) ?? new PropertyInfo[] { })
            {
                var name         = propertyInfo.Name;
                var propertyType = propertyInfo.PropertyType;

                if (name is "state")
                {
                    if (propertyType == typeof(EmptyState))
                    {
                        continue;
                    }

                    if (!map.ContainsKey(name))
                    {
                        map.Add(name, ReflectionHelper.CreateDefaultValue(propertyType));
                        continue;
                    }
                }

                if (propertyInfo.DeclaringType == typeof(Element) ||
                    propertyInfo.DeclaringType == typeof(ReactStatefulComponent))
                {
                    continue;
                }

                if (propertyInfo.DeclaringType?.IsGenericType == true &&
                    propertyInfo.DeclaringType.GetGenericTypeDefinition() == typeof(ReactComponent<>))
                {
                    continue;
                }

                if (propertyType.BaseType == typeof(MulticastDelegate))
                {
                    continue;
                }

                if (!map.ContainsKey(name))
                {
                    map.Add(name, ReflectionHelper.CreateDefaultValue(propertyType));
                }
            }

            state.JsonTextForDotNetInstanceProperties = JsonConvert.SerializeObject(map, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                Formatting           = Formatting.Indented
            });
        }

        void initializeParametersJson()
        {
            var map = JsonConvert.DeserializeObject<Dictionary<string, object>>(state.JsonTextForDotNetMethodParameters ?? string.Empty);
            if (map == null)
            {
                map = new Dictionary<string, object>();
            }

            foreach (var parameterInfo in MetadataHelper.LoadAssembly(fullAssemblyPath).TryLoadFrom(state.SelectedMethod)?.GetParameters() ?? new ParameterInfo[] { })
            {
                var name = parameterInfo.Name;
                if (name == null || map.ContainsKey(name))
                {
                    continue;
                }

                map.Add(name, ReflectionHelper.CreateDefaultValue(parameterInfo.ParameterType));
            }

            state.JsonTextForDotNetMethodParameters = JsonConvert.SerializeObject(map, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                Formatting           = Formatting.Indented
            });
        }
    }

    void OnKeypressFinished()
    {
        SaveState();
    }

    void OnFilterChanged()
    {
    }

    void SaveState()
    {
        if (state.SelectedMethod is not null)
        {
            StateCache.Save(state.SelectedMethod, state);
        }

        if (state.SelectedType is not null)
        {
            StateCache.Save(state.SelectedType, state);
        }

        StateCache.Save(state);
    }
}