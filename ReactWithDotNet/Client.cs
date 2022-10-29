using System.Reflection;

namespace ReactWithDotNet;

static partial class Mixin
{
    internal static void ConvertReactEventsToTaskForEventBus(this ReactStatefulComponent reactComponent)
    {
        foreach (var propertyInfo in reactComponent.GetType().GetProperties().Where(x => x.GetCustomAttribute<ReactCustomEventAttribute>() is not null))
        {
            var isAction        = propertyInfo.PropertyType.FullName == typeof(Action).FullName;
            var isGenericAction = propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.IsGenericAction1or2or3();

            if (isAction || isGenericAction)
            {
                convertToTask(propertyInfo);
                continue;
            }

            throw DeveloperException("ReactCustomEventAttribute can only use with Action or Action<..>");
        }

        void convertToTask(PropertyInfo propertyInfo)
        {
            var @delegate = (Delegate)propertyInfo.GetValue(reactComponent);
            if (@delegate is null)
            {
                return;
            }

            if (@delegate.Target is ReactStatefulComponent target)
            {
                propertyInfo.SetValue(reactComponent, null);

                reactComponent.Client.taskList.Add(new ClientTask
                {
                    TaskId              = (int)TaskId.InitializeDotnetComponentEventListener,
                    EventName           = GetEventKey(reactComponent, propertyInfo.Name),
                    HandlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier,
                    RouteToMethod       = @delegate.Method.Name
                });
            }
            else
            {
                throw DeveloperException("Action handler method should belong to React component");
            }
        }
    }

    internal static string GetEventKey(ReactStatefulComponent reactComponent, string propertyName)
    {
        if (reactComponent.ComponentUniqueIdentifier is null)
        {
            throw DeveloperException("ComponentUniqueIdentifier cannot be null");
        }
        return $"{{Property: '{reactComponent.GetType().FullName}::{propertyName}', ComponentUniqueIdentifier: {reactComponent.ComponentUniqueIdentifier}}}";
    }
}

public abstract class ReactComponent<TState> : ReactStatefulComponent where TState : new()
{
    #region Public Properties
    public TState state { get; protected internal set; }
    #endregion

    #region Methods
    protected override void constructor()
    {
        state = new TState();
    }
    #endregion
}

[Serializable]
public sealed class EmptyState
{
}

public sealed class Client
{
    #region Fields
    internal readonly List<ClientTask> taskList = new();
    #endregion

    #region Public Methods

   
    
  

    
    #endregion

    #region Methods
    internal ClientTask[] ToArray() => taskList.ToArray();

    public void CallJsFunction(string JsFunctionPath, params object[] JsFunctionArguments)
    {
        taskList.Add(new ClientTask { TaskId = (int)TaskId.CallJsFunction, JsFunctionPath = JsFunctionPath, JsFunctionArguments = JsFunctionArguments });
    }

   

   
    #endregion


}
enum TaskId
{
    CallJsFunction = 1,
    ListenEvent = 2,
    DispatchEvent = 3,
    
    InitializeDotnetComponentEventListener = 5,
    
    NavigateToUrl = 7,
    OnOutsideClicked =8,
    ListenEventOnlyOnce = 9
}
public class JsClientEventInfo
{
    #region Fields
    public readonly string Name;
    #endregion

    #region Constructors
    public JsClientEventInfo(string name)
    {
        Name = name;
    }
    #endregion
}

public sealed class JsClientEventInfo<EventArgument1> : JsClientEventInfo
{
    #region Constructors
    public JsClientEventInfo(string name) : base(name)
    {
    }
    #endregion
}