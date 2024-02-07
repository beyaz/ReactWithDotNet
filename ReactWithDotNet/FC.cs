using System.Reflection;

namespace ReactWithDotNet;

/// <summary>
///     Functional component.
/// </summary>
public delegate Element FC(Scope cmp);

public interface Scope
{
    public Client Client { get; }

    public Func<Task> ComponentDidMount { set; }

    public Func<Task> Constructor { set; }

    public ReactContext Context { get; }
}

sealed class FunctionalComponent : Component<FunctionalComponent.State>, Scope
{
    internal object _target;

    internal Func<Task<FC>> renderFuncAsyncWithScope;

    internal FC renderFuncWithScope;

    public Type CompilerGeneratedType { get; init; }

    public Func<Task> ComponentDidMount { internal get; set; }

    public Func<Task> Constructor { internal get; set; }

    public bool IsRenderAsync { get; init; }

    public string RenderMethodNameWithToken { get; init; }

    public void InitializeTarget()
    {
        _target ??= SerializationHelperForCompilerGeneratedClasss.Deserialize(state.CompilerGeneratedType, state.Scope);
    }

    internal void CalculateScopeFromTarget()
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

        if (target is null)
        {
            throw DeveloperException("Invalid usage of useState. target not calculated.");
        }

        state.Scope = SerializationHelperForCompilerGeneratedClasss.Serialize(target);
    }

    protected override Task constructor()
    {
        state = new()
        {
            IsRenderAsync = IsRenderAsync,

            CompilerGeneratedType = CompilerGeneratedType,

            RenderMethodNameWithToken = RenderMethodNameWithToken
        };

        return Task.CompletedTask;
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

            TryUpdateProps(renderFuncWithScope?.Target,_target);
        }

        MethodInfo methodInfo = null;
        if (TryResolveMethodInfo(state.RenderMethodNameWithToken, ref methodInfo))
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
        if (renderFuncAsyncWithScope is not null && state.Scope is null)
        {
            var fc = await renderFuncAsyncWithScope();

            return fc?.Invoke(this);
        }

        if (_target is null && state.Scope is not null)
        {
            InitializeTarget();

            TryUpdateProps(renderFuncAsyncWithScope?.Target,_target);
        }

        MethodInfo methodInfo = null;
        if (TryResolveMethodInfo(state.RenderMethodNameWithToken, ref methodInfo))
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

    internal class State
    {
        public Type CompilerGeneratedType { get; init; }

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
           return new div(WidthMaximized, HeightMaximized)
           {
               new FunctionalComponentMethodParamUpdateTest(),
               
               //new TraceAndBindingContainerComponent(),

               //new ChildRemoveTest(),

               //new TriggerParentTest()
           };
       }

       static FC BindingSample(int start)
       {
           var count = start;

           var text = "label: " + count;

           return _ =>
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
           };
       }

       static FC TraceFunctionalComponent(int start)
       {
           var count = start;

           var constructorCallCount = 0;

           var didMountCount = 0;

           Task OnClickHandler(MouseEvent e)
           {
               count++;
               return Task.CompletedTask;
           }

           return cmp =>
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
           };
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

           static FC Counter(int Start)
           {
               var start = Start;

               return _ =>
               {
                   return new button { $"Count:{start}", OnClick(Plus) };

                   Task Plus(MouseEvent e)
                   {
                       start++;
                       return Task.CompletedTask;
                   }
               };
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
               return new FlexColumn(WidthMaximized, HeightMaximized)
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

           static FC TriggerFunctionalComponent(int start)
           {
               var count = start;

               var constructorCallCount = 0;

               var didMountCount = 0;



               return cmp =>
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
               };
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
           
           static FC Counter(int CountAsMethodParameter)
           {
               var count = CountAsMethodParameter;

               return cmp =>
               {
                   return new button(Size(100))
                   {
                       "count:" +count + " / CountAsMethodParameter:" + CountAsMethodParameter,
                       OnClick(OnClickHandler)
                   };

                   Task OnClickHandler(MouseEvent e)
                   {
                       count++;

                       return Task.CompletedTask;
                   }
               };
           }
       }
   }
   
   
   




*/