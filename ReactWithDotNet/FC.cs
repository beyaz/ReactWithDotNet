using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public interface IFunctionalComponent : IReactComponent
{
    public Func<Task> ComponentDidMount { set; }

    public Func<Task> Constructor { set; }

    public bool DesignMode { get; }
}

partial class Mixin
{
    /// <summary>
    ///     Dispatch given <paramref name="handlerFunc" />
    ///     <br />
    ///     Sample usage:
    ///     <code> 
    ///    static FC Counter(int Count, Func&lt;int, Task&gt; OnValueChange)
    ///    {
    ///        var count = Count;
    ///       
    ///        return cmp =>
    ///        {
    ///            return new FlexColumn
    ///            {
    ///                new button(Padding(10))
    ///                {
    ///                    $"{count}",
    ///                    OnClick(OnClickHandler)
    ///                }
    ///            };
    ///       
    ///            Task OnClickHandler(MouseEvent e)
    ///            {
    ///                count++;
    ///        
    ///                cmp.DispatchEvent(OnValueChange, count);
    ///       
    ///                return Task.CompletedTask;
    ///            }
    ///        };
    ///    }
    /// </code>
    /// </summary>
    public static void DispatchEvent(this IFunctionalComponent functionalComponent, Delegate handlerFunc, [CallerArgumentExpression(nameof(handlerFunc))] string handlerFuncName = null)
    {
        if (handlerFuncName is null)
        {
            throw new ArgumentNullException(nameof(handlerFuncName));
        }

        var propertyName = handlerFuncName.Split('.').Last();

        var senderInfo = GetEventSenderInfo((FunctionalComponent)functionalComponent, propertyName);

        functionalComponent.Client.DispatchDotNetCustomEvent(senderInfo);
    }

    /// <summary>
    ///     Dispatch given <paramref name="handlerFunc" />
    ///     <br />
    ///     Sample usage:
    ///     <code> 
    ///    static FC Counter(int Count, Func&lt;int, Task&gt; OnValueChange)
    ///    {
    ///        var count = Count;
    ///       
    ///        return cmp =>
    ///        {
    ///            return new FlexColumn
    ///            {
    ///                new button(Padding(10))
    ///                {
    ///                    $"{count}",
    ///                    OnClick(OnClickHandler)
    ///                }
    ///            };
    ///       
    ///            Task OnClickHandler(MouseEvent e)
    ///            {
    ///                count++;
    ///        
    ///                cmp.DispatchEvent(OnValueChange, [count]);
    ///       
    ///                return Task.CompletedTask;
    ///            }
    ///        };
    ///    }
    /// </code>
    /// </summary>
    public static void DispatchEvent(this IFunctionalComponent functionalComponent, Delegate handlerFunc, object[] parameters, [CallerArgumentExpression(nameof(handlerFunc))] string handlerFuncName = null)
    {
        if (handlerFuncName is null)
        {
            throw new ArgumentNullException(nameof(handlerFuncName));
        }

        var propertyName = handlerFuncName.Split('.').Last();

        var senderInfo = GetEventSenderInfo((FunctionalComponent)functionalComponent, propertyName);

        functionalComponent.Client.DispatchDotNetCustomEvent(senderInfo, parameters);
    }

    public static Element FC(Func<IFunctionalComponent, Element> func, [CallerMemberName] string callerMemberName = null)
    {
        if (func == null)
        {
            return null;
        }

        if (func.Target is not null)
        {
            var targeType = func.Target.GetType();

            if (targeType.IsCompilerGenerated())
            {
                return new FunctionalComponent
                {
                    renderFuncWithScope = func,

                    RenderMethodNameWithToken = func.Method.GetAccessKey(),

                    CompilerGeneratedType = targeType,

                    Name = $"{targeType.DeclaringType?.Name}.{callerMemberName}"
                };
            }
        }

        throw InvalidUsageOfFunctionalComponent(func);
    }

    public static Element FC(Func<IFunctionalComponent, Task<Element>> func, [CallerMemberName] string callerMemberName = null)
    {
        if (func == null)
        {
            return null;
        }

        if (func.Target is not null)
        {
            var targeType = func.Target.GetType();

            if (targeType.IsCompilerGenerated())
            {
                return new FunctionalComponent
                {
                    IsRenderAsync = true,

                    IsAsyncFC = true,

                    asyncRenderFunc = func,

                    RenderMethodNameWithToken = func.Method.GetAccessKey(),

                    CompilerGeneratedType = targeType,

                    Name = $"{targeType.DeclaringType?.Name}.{callerMemberName}"
                };
            }
        }

        throw InvalidUsageOfFunctionalComponent(func);
    }

    internal static bool IsFunctionalComponent(this Type type)
    {
        return type == typeof(IFunctionalComponent);
    }

    static Exception InvalidUsageOfFunctionalComponent(Delegate @delegate)
    {
        var target = @delegate.Target;
        if (target is null)
        {
            return DeveloperException($"Invalid usage of Functional component.{@delegate.Method} target cannot be null.");
        }

        return DeveloperException($"Invalid usage of Functional component. {target.GetType().FullName}  should be compiler generated type.");
    }
}

sealed class FunctionalComponent : Component<FunctionalComponent.State>, IFunctionalComponent
{
    internal object _target;

    internal Func<IFunctionalComponent, Task<Element>> asyncRenderFunc;

    internal Func<Task<Func<IFunctionalComponent, Element>>> renderFuncAsyncWithScope;

    internal Func<IFunctionalComponent, Element> renderFuncWithScope;

    public Type CompilerGeneratedType { get; init; }

    public Func<Task> ComponentDidMount { internal get; set; }

    public Func<Task> Constructor { internal get; set; }

    [JsonIgnore]
    public new bool DesignMode => base.DesignMode;

    public bool IsAsyncFC { get; init; }

    public bool IsRenderAsync { get; init; }

    public required string Name { get; init; }

    public string RenderMethodNameWithToken { get; init; }

    public void InitializeTarget()
    {
        _target ??= SerializationHelperForCompilerGeneratedClasss.Deserialize(state.CompilerGeneratedType, state.Scope);
    }

    internal void CalculateScopeFromTarget(ElementSerializerContext context)
    {
        var target = _target;

        if (target is null && renderFuncWithScope is not null)
        {
            target = renderFuncWithScope.Target;
        }
        else if (target is null && renderFuncAsyncWithScope is not null)
        {
            target = renderFuncAsyncWithScope.Target;
        }
        else if (target is null && asyncRenderFunc is not null)
        {
            target = asyncRenderFunc.Target;
        }

        if (target is null)
        {
            throw DeveloperException("Invalid usage of useState. target not calculated.");
        }

        state.Scope = SerializationHelperForCompilerGeneratedClasss.Serialize(target, this, context);
    }

    protected override Task constructor()
    {
        state = new()
        {
            IsAsyncFC = IsAsyncFC,

            IsRenderAsync = IsRenderAsync,

            CompilerGeneratedType = CompilerGeneratedType,

            RenderMethodNameWithToken = RenderMethodNameWithToken
        };

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        if (state.IsRenderAsync)
        {
            return NoneOfRender.Value;
        }

        if (renderFuncWithScope is not null && state.Scope is null)
        {
            return renderFuncWithScope(this);
        }

        if (_target is null && state.Scope is not null)
        {
            InitializeTarget();

            TryUpdateProps(renderFuncWithScope?.Target, _target);
        }

        MethodInfo methodInfo = null;
        if (MethodAccess.TryResolveMethodInfo(state.RenderMethodNameWithToken, ref methodInfo))
        {
            object[] invocationParameters = null;
            if (methodInfo.GetParameters().Length == 1)
            {
                invocationParameters = [this];
            }

            var renderResult = (Element)methodInfo.Invoke(_target, invocationParameters);

            // protect for calling twice
            Constructor = null;

            return renderResult;
        }

        throw DeveloperException("Invalid usage of useState.");
    }

    protected override async Task<Element> renderAsync()
    {
        if (state.IsAsyncFC)
        {
            if (state.Scope is null)
            {
                if (asyncRenderFunc is not null)
                {
                    return await asyncRenderFunc(this);
                }
            }
        }

        if (renderFuncAsyncWithScope is not null && state.Scope is null)
        {
            var fc = await renderFuncAsyncWithScope();

            return fc?.Invoke(this);
        }

        if (_target is null && state.Scope is not null)
        {
            InitializeTarget();

            TryUpdateProps(renderFuncAsyncWithScope?.Target, _target);
        }

        MethodInfo methodInfo = null;
        if (MethodAccess.TryResolveMethodInfo(state.RenderMethodNameWithToken, ref methodInfo))
        {
            object[] invocationParameters = null;
            if (methodInfo.GetParameters().Length == 1)
            {
                invocationParameters = [this];
            }

            var renderResult = (Task<Element>)methodInfo.Invoke(_target, invocationParameters);

            // protect for calling twice
            Constructor = null;

            return renderResult;
        }

        throw DeveloperException("Invalid usage of useState.");
    }

    static void TryUpdateProps(object targetInMethod, object target)
    {
        if (targetInMethod is not null)
        {
            foreach (var fieldInfo in targetInMethod.GetType().GetFields())
            {
                if (char.IsUpper(fieldInfo.Name[0]))
                {
                    var propValue = fieldInfo.GetValue(targetInMethod);

                    fieldInfo.SetValue(target, propValue);
                }
            }
        }
    }

    internal class State
    {
        public Type CompilerGeneratedType { get; init; }

        public bool IsAsyncFC { get; set; }

        public bool IsRenderAsync { get; init; }

        public string RenderMethodNameWithToken { get; init; }

        /// <summary>
        ///     Scope means value of auto generated fields of CompilerGeneratedType instance.
        /// </summary>
        public IReadOnlyDictionary<string, object> Scope { get; set; }
    }
}

/* TEST SCENARIO













   namespace ReactWithDotNet.WebSite;



   public class MainWindow : PureComponent
   {
       protected override Element render()
       {
           return new div(WidthFull, HeightFull)
           {
               new ErrorViewTest(),

               // new ParentChildEventTest(),

               //CountComponentAsync(7),
               //CountComponentAsyncTask(8),

               //new FunctionalComponentWithDelegateParameter(),

               //new FunctionalComponentMethodParamUpdateTest(),

               //new TraceAndBindingContainerComponent(),

               //new ChildRemoveTest(),

               //new TriggerParentTest()
           };
       }

       class ErrorViewTest : Component<ErrorViewTest.State>
       {
           internal class State
           {
               public int Count { get; set; }
           }

           protected override Element render()
           {
               return new FlexColumn(Size(300), Border(1, solid, "red"))
               {
                   new button { $"container: {state.Count}", OnClick(OnClickHandler) },
                   SpaceY(50),
                   IsOdd(state.Count)
               };
           }

           static Element IsOdd(int Number)
           {
               return FC(_ =>
               {
                   var isOdd = Number % 2 == 1;

                   return new FlexColumn
                   {
                       new div
                       {
                           Number + (isOdd ? "odd" : "even")
                       }
                   };
               });
           }

           Task OnClickHandler(MouseEvent e)
           {
               state.Count++;

               return Task.CompletedTask;
           }
       }

       static Element BindingSample(int start)
       {
           var count = start;

           var text = "label: " + count;

           return FC(_ =>
           {
               return new FlexColumn
               {
                   new input
                   {
                       valueBind = () => count
                   },
                   new label { text },
                   new button(OnClick(OnClickHandler)) { "Update Count" }
               };

               Task OnClickHandler(MouseEvent e)
               {
                   text = "label: " + count;

                   return Task.CompletedTask;
               }
           });
       }

       static Element TraceFunctionalComponent(int start)
       {
           var count = start;

           var constructorCallCount = 0;

           var didMountCount = 0;

           Task OnClickHandler(MouseEvent e)
           {
               count++;
               return Task.CompletedTask;
           }

           return FC(cmp =>
           {
               cmp.Constructor = () =>
               {
                   constructorCallCount++;

                   return Task.CompletedTask;
               };

               cmp.ComponentDidMount = () =>
               {
                   didMountCount++;

                   return Task.CompletedTask;
               };

               return new FlexRowCentered
               {
                   "count:" + count + " / constructorCallCount:" + constructorCallCount + " /  didMountCount:" + didMountCount,
                   OnClick(OnClickHandler)
               };
           });
       }

       class ChildRemoveTest : Component<ChildRemoveTest.State>
       {
           protected override Element render()
           {
               return new FlexColumn(Gap(16))
               {
                   Counter(6),
                   Counter(34),
                   Counter(57),

                   new button { $"Count:{state.Count}", OnClick(Plus) }
               };
           }

           static Element Counter(int Start)
           {
               var start = Start;

               return FC(_ =>
               {
                   return new button { $"Count:{start}", OnClick(Plus) };

                   Task Plus(MouseEvent e)
                   {
                       start++;
                       return Task.CompletedTask;
                   }
               });
           }

           Task Plus(MouseEvent e)
           {
               state.Count++;

               return Task.CompletedTask;
           }

           internal class State
           {
               public int Count { get; set; }
           }
       }

       class TraceAndBindingContainerComponent : Component
       {
           public int count { get; set; }

           protected override Element render()
           {
               return new FlexColumn(WidthFull, HeightFull)
               {
                   BindingSample(7),

                   TraceFunctionalComponent(6),
                   TraceFunctionalComponent(7),
                   TraceFunctionalComponent(8),

                   new div { Size(50), Background("green"), OnClick(OnClickHandler), "count:" + count }
               };
           }

           Task OnClickHandler(MouseEvent e)
           {
               count++;
               return Task.CompletedTask;
           }
       }




       class TriggerParentTest : Component<TriggerParentTest.State>
       {
           delegate Task CountUpdated();

           internal class State
           {
               public int CountInRoot { get; set; }
           }


           protected override Task constructor()
           {
               state = new()
               {
                   CountInRoot = 4
               };

               Client.ListenEvent<CountUpdated>(OnCountUpdated);

               return Task.CompletedTask;
           }

           Task OnCountUpdated()
           {
               state.CountInRoot++;

               return Task.CompletedTask;
           }

           protected override Element render()
           {
               return new FlexColumn
               {
                   new button { Size(40), OnClick(OnPlusClicked), $"root:{state.CountInRoot}" },

                   //TriggerFunctionalComponent(10),
                   //state.CountInRoot % 10 == 0 ? null : TriggerFunctionalComponent(100),
                   //TriggerFunctionalComponent(1000),


                   //new TriggerClassComponent{BeginCount = 10},
                   //state.CountInRoot % 10 == 0 ? null : new TriggerClassComponent{BeginCount = 100},
                   //new TriggerClassComponent{BeginCount = 1000}

                   new div
                   {
                       new TriggerClassComponent { BeginCount = 10 }
                   },
                   new div
                   {
                       state.CountInRoot % 10 == 0 ? null : new TriggerClassComponent { BeginCount = 100 },
                   },
                   new div
                   {
                       new TriggerClassComponent { BeginCount = 1000 }
                   }
               };
           }

           Task OnPlusClicked(MouseEvent e)
           {
               state.CountInRoot++;

               return Task.CompletedTask;
           }

           static Element TriggerFunctionalComponent(int start)
           {
               var count = start;

               var constructorCallCount = 0;

               var didMountCount = 0;



               return FC(cmp =>
               {
                   cmp.Constructor = () =>
                   {
                       constructorCallCount++;

                       return Task.CompletedTask;
                   };

                   cmp.ComponentDidMount = () =>
                   {
                       didMountCount++;

                       return Task.CompletedTask;
                   };

                   return new FlexRowCentered
                   {
                       "count:" + count + " / constructorCallCount:" + constructorCallCount + " /  didMountCount:" + didMountCount,
                       OnClick(OnClickHandler)
                   };

                   Task OnClickHandler(MouseEvent e)
                   {
                       count++;

                       cmp.Client.DispatchEvent<CountUpdated>();

                       return Task.CompletedTask;
                   }
               });
           }

           class TriggerClassComponent : Component<TriggerClassComponent.TriggerClassComponentState>
           {
               public required int BeginCount { get; init; }

               internal class TriggerClassComponentState
               {
                   public int count { get; set; }
                   public int constructorCallCount { get; set; }
                   public int didMountCount { get; set; }
               }

               protected override Task constructor()
               {
                   state = new()
                   {
                       count                = BeginCount,
                       constructorCallCount = 1
                   };

                   return Task.CompletedTask;
               }

               protected override Task componentDidMount()
               {
                   state.didMountCount++;

                   return Task.CompletedTask;
               }

               protected override Element render()
               {
                   return new FlexRowCentered
                   {
                       "count:" + state.count + " / constructorCallCount:" + state.constructorCallCount + " /  didMountCount:" + state.didMountCount,
                       OnClick(OnClickHandler)
                   };
               }

               Task OnClickHandler(MouseEvent e)
               {
                   state.count++;

                   Client.DispatchEvent<CountUpdated>();

                   return Task.CompletedTask;
               }
           }
       }


       class FunctionalComponentMethodParamUpdateTest : Component<FunctionalComponentMethodParamUpdateTest.State>
       {
           internal class State
           {
               public int CountInParent { get; set; }
           }

           protected override Task constructor()
           {
               state = new()
               {
                   CountInParent = 4
               };

               return Task.CompletedTask;
           }

           protected override Element render()
           {
               return new FlexColumn
               {
                   SpaceY(30),

                   Counter(state.CountInParent),

                   new button(Size(100))
                   {
                       OnClick(OnClickHandler),
                       $"CountInParent: {state.CountInParent}"
                   }
               };

           }

           Task OnClickHandler(MouseEvent e)
           {
               state.CountInParent++;

               return Task.CompletedTask;
           }

           static Element Counter(int CountAsMethodParameter)
           {
               var count = CountAsMethodParameter;

               return FC(cmp =>
               {
                   return new button(Size(100))
                   {
                       "count:" + count + " / CountAsMethodParameter:" + CountAsMethodParameter,
                       OnClick(OnClickHandler)
                   };

                   Task OnClickHandler(MouseEvent e)
                   {
                       count++;

                       return Task.CompletedTask;
                   }
               });
           }
       }



       class FunctionalComponentWithDelegateParameter : Component<FunctionalComponentWithDelegateParameter.State>
       {
           internal class State
           {
               public int CountInParent { get; set; }
               public int CountInChild { get; set; }
           }

           protected override Task constructor()
           {
               state = new()
               {
                   CountInParent = 4,
                   CountInChild  = 1
               };

               return Task.CompletedTask;
           }

           protected override Element render()
           {
               return new FlexColumn
               {
                   Counter(state.CountInParent, UpdateCountInChild),

                   SpaceY(50),

                   new button(Size(200))
                   {
                       OnClick(OnClickHandler),
                       $"CountInParent: {state.CountInParent} / CountInChild: {state.CountInChild}"
                   }
               };

           }

           Task UpdateCountInChild(int newValue)
           {
               state.CountInChild = newValue;

               return Task.CompletedTask;
           }

           Task OnClickHandler(MouseEvent e)
           {
               state.CountInParent++;

               return Task.CompletedTask;
           }

           static Element Counter(int CountAsMethodParameter, Func<int, Task> OnValueChange)
           {
               var count = CountAsMethodParameter;

               return FC(cmp =>
               {
                   return new FlexColumn
                   {
                       new button(Size(100))
                       {
                           "count:" + count + " / CountAsMethodParameter:" + CountAsMethodParameter,
                           OnClick(OnClickHandler)
                       },

                       Counter_nested(9, OnNestedCountChanged)
                   };

                   Task OnClickHandler(MouseEvent e)
                   {
                       count++;

                       cmp.DispatchEvent(OnValueChange, [count]);

                       return Task.CompletedTask;
                   }

                   Task OnNestedCountChanged(int newValue)
                   {
                       count++;

                       cmp.DispatchEvent(OnValueChange, [count]);

                       return Task.CompletedTask;
                   }
               });
           }

           static Element Counter_nested(int Count, Func<int, Task> OnValueChange)
           {
               var count = Count;

               return FC(cmp =>
               {
                   return new FlexColumn(Gap(20), Border(Solid(1, Gray200)), BorderRadius(5), JustifyContentSpaceAround)
                   {
                       new button(Size(100), Background("yellow"))
                       {
                           "Counter_nested: " + count + "/" + Count,
                           OnClick(OnClickHandler)
                       },

                       Counter_nested_nested(count, OnNestedClicked)
                   };

                   Task OnClickHandler(MouseEvent e)
                   {
                       count++;

                       cmp.DispatchEvent(OnValueChange, [count]);

                       return Task.CompletedTask;
                   }

                   Task OnNestedClicked(int newValue)
                   {
                       count++;

                       return Task.CompletedTask;
                   }
               });
           }

           static Element Counter_nested_nested(int Count, Func<int, Task> OnValueChange)
           {
               var count = Count;

               return FC(cmp =>
               {
                   return new button(Size(200), BorderColor(Gray300), BorderRadius(5))
                   {
                       "Counter_nested_nested: " + count + "/" + Count,
                       OnClick(OnClickHandler)
                   };

                   Task OnClickHandler(MouseEvent e)
                   {
                       count++;

                       cmp.DispatchEvent(OnValueChange, [count]);

                       return Task.CompletedTask;
                   }
               });
           }
       }


       static Element CountComponentAsync(int Count)
       {
           var count = Count;

           return FC(async cmp =>
           {
               await Task.Delay(1000);

               return new FlexRowCentered
               {
                   $"count: {count}",
                   OnClick(OnClicked)
               };

               Task OnClicked(MouseEvent e)
               {
                   count++;

                   return Task.CompletedTask;
               }
           });
       }

       static async Task<Element> CountComponentAsyncTask(int Count)
       {
           await Task.Delay(1000);

           var count = Count;

           return FC(async cmp =>
           {
               await Task.Delay(3000);

               return new FlexRowCentered
               {
                   $"count: {count}",
                   OnClick(OnClicked)
               };

               Task OnClicked(MouseEvent e)
               {
                   count++;

                   return Task.CompletedTask;
               }
           });
       }

       class ParentChildEventTest : Component<ParentChildEventTest.State>
       {
           internal class State
           {
               public int Count { get; set; }
           }

           protected override Element render()
           {
               return new div
               {
                   $"current:{state.Count}",
                   br,
                   Counter_A(state.Count, OnValueChangeRootComponent)
               };
           }

           Task OnValueChangeRootComponent(int newValue)
           {
               state.Count = newValue;

               return Task.CompletedTask;
           }

           static Element Counter_A(int Count, Func<int, Task> OnValueChangeA)
           {
               var count = Count;

               return FC(cmp =>
               {
                   return new div(Size(200), BorderColor(Gray300), BorderRadius(5))
                   {
                       "Counter_A: " + count + "/" + Count,
                       OnClick(OnClickHandler),
                       Counter_B(Count,OnValueChangeA)
                   };

                   [ReactStopPropagation]
                   Task OnClickHandler(MouseEvent e)
                   {
                       count++;

                       cmp.DispatchEvent(OnValueChangeA, [count]);

                       return Task.CompletedTask;
                   }
               });
           }

           static Element Counter_B(int Count, Func<int, Task> OnValueChangeB)
           {
               var count = Count;

               return FC(cmp =>
               {
                   return new div(Size(200), BorderColor(Gray300), BorderRadius(5))
                   {
                       "Counter_B: " + count + "/" + Count,
                       OnClick(OnClickHandler),

                       Counter_C(Count,OnValueChangeB)
                   };

                   [ReactStopPropagation]
                   Task OnClickHandler(MouseEvent e)
                   {
                       count++;

                       cmp.DispatchEvent(OnValueChangeB, [count-2]);

                       return Task.CompletedTask;
                   }
               });
           }

           static Element Counter_C(int Count, Func<int, Task> OnValueChangeC)
           {
               var count = Count;

               return FC(cmp =>
               {
                   return new div(Size(200), BorderColor(Gray300), BorderRadius(5))
                   {
                       "Counter_C: " + count + "/" + Count,
                       OnClick(OnClickHandler)
                   };

                   [ReactStopPropagation]
                   Task OnClickHandler(MouseEvent e)
                   {
                       count++;

                       cmp.DispatchEvent(OnValueChangeC, [count]);

                       return Task.CompletedTask;
                   }
               });
           }
       }

   }











*/