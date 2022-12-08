using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using ReactWithDotNet.Libraries.PrimeReact;
using ReactWithDotNet.Libraries.uiw.react_codemirror;
using static ReactWithDotNet.UIDesigner.Extensions;

namespace ReactWithDotNet.UIDesigner;

public class ReactWithDotNetDesigner : ReactComponent<ReactWithDotNetDesignerModel>
{
    public void Refresh()
    {
        SaveState();
    }

    protected override void componentDidMount()
    {
        Client.OnBrowserInactive(Refresh);
        Client.CallJsFunction("InitializeUIDesignerEvents", 1000);
    }

    protected override void constructor()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        state.SelectedAssemblyFilePath ??= Assembly.GetEntryAssembly()?.Location;

        if (state.SelectedMethodTreeNodeKey.HasValue())
        {
            OnElementSelected((state.SelectedMethodTreeNodeKey, state.SelectedMethodTreeFilter));
        }
    }

    protected override Element render()
    {
        const int width = 500;

        Element createJsonEditor()
        {
            Expression<Func<string>> valueBind = () => state.JsonTextForDotNetMethodParameters;

            if (state.IsInstanceEditorActive)
            {
                valueBind = () => state.JsonTextForDotNetInstanceProperties;
            }

            return new CodeMirror
            {
                extensions = { "json", "githubLight" },
                valueBind  = valueBind,
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

        var propertyPanel = new FlexColumn(PaddingLeftRight(5), Height("100%"), Width("100%"), FontSize15, PrimaryBackground)
        {
            new link { href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Mono&display=swap", rel = "stylesheet" },

            new style
            {
                Text($@"



.token.property{{ {new Style { Color("#189af6") }.ToCssWithImportant()} }}

.cm-editor{{ {new Style { Height("calc(100% - 2px)"), Color("#DE3163") }.ToCssWithImportant()} }}

.ͼ16 {{ {new Style { FontWeight600, FontFamily("'IBM Plex Mono', monospace") }.ToCssWithImportant()} }}

")
            },
            new MethodSelectionView
            {
                Filter                    = state.SelectedMethodTreeFilter,
                SelectedMethodTreeNodeKey = state.SelectedMethodTreeNodeKey,
                SelectionChanged          = OnElementSelected,
                AssemblyFilePath          = state.SelectedAssemblyFilePath,
                Width                     = width
            },

            Space(10),
            new Slider
            {
                max   = 100, min = 0, value = state.ScreenWidth, onChange = OnWidthChanged,
                style = { Margin(10), Padding(5) }
            },

            new FlexColumn(Height("100%"))
            {
                // header
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
                    }),
                },
                // content
                createJsonEditor() + Height("100%")
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
                border = "0.3px dotted #e0e0e0",
                width  = state.ScreenWidth + "%",
                height = "100%"
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

    void OnElementSelected((string value, string filter) e)
    {
        SaveState();

        state.SelectedType   = null;
        state.SelectedMethod = null;

        state.JsonTextForDotNetInstanceProperties = null;
        state.JsonTextForDotNetMethodParameters   = null;

        state.SelectedMethodTreeNodeKey = e.value;
        state.SelectedMethodTreeFilter  = e.filter;

        var fullAssemblyPath = state.SelectedAssemblyFilePath;

        var node = MethodSelectionView.FindTreeNode(fullAssemblyPath, state.SelectedMethodTreeNodeKey);
        if (node is not null)
        {
            static object getDefaultValueForJson(Type type)
            {
                if (type.IsValueType)
                {
                    return Activator.CreateInstance(type);
                }

                if (type == typeof(string))
                {
                    return "";
                }

                if (type.IsSubclassOf(typeof(IEnumerable)))
                {
                    return "[]";
                }
                return null;
            }
            if (node.IsClass)
            {
                state.SelectedType = node.TypeReference;
                state              = StateCache.TryRead(state.SelectedType) ?? state;

                // calculate json text
                {
                    var map = JsonConvert.DeserializeObject<Dictionary<string, object>>(state.JsonTextForDotNetInstanceProperties ?? "{}");
                    foreach (var propertyInfo in MetadataHelper.LoadAssembly(fullAssemblyPath).TryLoadFrom(state.SelectedType)?.GetProperties() ?? new PropertyInfo[] { })
                    {
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

                        var name         = propertyInfo.Name;
                        var propertyType = propertyInfo.PropertyType;

                        if (propertyType.BaseType == typeof(MulticastDelegate))
                        {
                            continue;
                        }

                        if (!map.ContainsKey(name))
                        {
                            map.Add(name, getDefaultValueForJson(propertyType));
                        }
                    }

                    state.JsonTextForDotNetInstanceProperties = JsonConvert.SerializeObject(map, new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Include,
                        Formatting           = Formatting.Indented
                    });
                }
            }

            if (node.IsMethod)
            {
                state.SelectedMethod = node.MethodReference;
                state                = StateCache.TryRead(state.SelectedMethod) ?? state;

                // calculate json text
                {
                    var map = JsonConvert.DeserializeObject<Dictionary<string, object>>(state.JsonTextForDotNetMethodParameters ?? "{}");
                    foreach (var parameterInfo in MetadataHelper.LoadAssembly(fullAssemblyPath).TryLoadFrom(state.SelectedMethod)?.GetParameters() ?? new ParameterInfo[] { })
                    {
                        var name = parameterInfo.Name;

                        var parameterType = parameterInfo.ParameterType;

                        if (parameterType == typeof(Element) ||
                            parameterType == typeof(ReactStatefulComponent))
                        {
                            continue;
                        }

                        if (name != null && !map.ContainsKey(name))
                        {
                            map.Add(name, getDefaultValueForJson(parameterType));
                        }
                    }

                    state.JsonTextForDotNetMethodParameters = JsonConvert.SerializeObject(map, new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Include,
                        Formatting           = Formatting.Indented
                    });
                }

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
    }

    void OnWidthChanged(SliderChangeParams e)
    {
        state.ScreenWidth = e.value;
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