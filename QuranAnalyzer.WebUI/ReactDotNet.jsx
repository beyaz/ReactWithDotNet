/**
*  Written by Abdullah Beyaztaş
*  Manages react ui by using react render informations which incoming from server.
*/

import React from 'react';
import {createRoot} from 'react-dom/client';

var createElement = React.createElement;

const DotNetTypeOfReactComponent = '$Type$';
const FullTypeNameOfState = '$TypeOfState$';
const RootNode = 'RootElement';


const EventBus =
{
    On: function(event, callback)
    {
        document.addEventListener(event, (e) => callback(e.detail));
    },
    Dispatch: function(event, data)
    {
        document.dispatchEvent(new CustomEvent(event, { detail: data }));
    },
    Remove: function(event, callback)
    {
        document.removeEventListener(event, callback);
    }
};

function IterateObject(obj, fn)
{
    for (var key in obj)
    {
        if (obj.hasOwnProperty(key))
        {
            fn(key, obj[key]);
        }
    }
}

function HasId(htmlElement)
{
    return htmlElement.id !== "";
}

function GoUpwardFindFirst(htmlElement, findFunc)
{
    while (htmlElement)
    {
        if (findFunc(htmlElement))
        {
            return htmlElement;
        }

        htmlElement = htmlElement.parentElement;
    }
    
    return null;
}

function OnDocumentReady(callback)
{
    var stateCheck = setInterval(function ()
    {
        if (document.readyState === "complete")
        {
            clearInterval(stateCheck);

            setTimeout(callback, 1);
        }
    }, 10);
}

function PickPropertiesToNewObject(obj, fn_bool__CanPick__stringKey_objectValue)
{
    var returnObj = {};
    
    IterateObject(obj, function (key, value)
    {
        if (fn_bool__CanPick__stringKey_objectValue(key, value))
        {
            returnObj[key] = value;
        }
    });

    return returnObj;
}

function IsEmptyObject(obj)
{
    for (var key in obj)
    {
        if (Object.prototype.hasOwnProperty.call(obj, key))
        {
            return false;
        }
    }

    return true;
}

function IsNotEmptyObject(obj)
{
    return IsEmptyObject(obj) === false;
}

const EventQueue = [];
var IsExecutingEvent = false;
function EmitNextEvent()
{
    if (EventQueue.length > 0)
    {
        const fn = EventQueue.shift();

        IsExecutingEvent = true;
        
        fn();

        return;
    }

    IsExecutingEvent = false;
}
function PushToEventQueue(fn)
{
    EventQueue.push(fn);

    if (EventQueue.length === 1)
    {
        EmitNextEvent();
    }

    if (!IsExecutingEvent)
    {
        EmitNextEvent();
    }
}

function Pipe(value)
{
    for (var i = 1; i < arguments.length; i++)
    {
        value = arguments[i](value);
    }

    return value;
}

function GetValueInPath(obj, steps)
{
    steps = typeof steps === "string" ? steps.split(".") : steps;

    var len = steps.length;

    for (var i = 0; i < len; i++)
    {
        if (obj == null)
        {
            throw "Path is not read. Path:" + steps.join(".");
        }

        obj = obj[steps[i]];
    }

    return obj;
}

function SetValueInPath(obj, steps, value)
{
    if (obj == null)
    {
        throw Error("SetValueInPath->" + value);
    }

    var len = steps.length;

    for (var i = 0; i < len; i++)
    {
        var step = steps[i];

        if (obj[step] == null)
        {
            obj[step] = {};
        }

        if (i === len - 1)
        {
            obj[step] = value;
        }
        else
        {
            obj = obj[step];
        }
    }
}

function NotNull(value)
{
    if (value == null)
    {
        throw Error("value cannot be null.");
    }

    return value;
}

function NotFrozen(value)
{
    if (Object.isFrozen(value))
    {
        throw Error("value cannot be frozen.");
    }

    return value;
}

function NVL(a, b)
{
    if (a == null)
    {
        return b;
    }

    return a;
}    

function Clone(obj)
{
    return JSON.parse(JSON.stringify(obj));
}

var ClientTaskId =
{
    CallJsFunction: 0,
    ListenEvent: 1,
    DispatchEvent: 2,        
    ListenComponentEvent: 3,
    PushHistory: 4,
    ComebackWithLastAction: 5,
    GotoMethod: 6,
    NavigateToUrl : 7
}

function ConvertToReactElement(jsonNode, component)
{
    // is ReactDotNet component
    if (jsonNode[DotNetTypeOfReactComponent])
    {
        var cmp = DefineComponent(jsonNode);

        return createElement(cmp, {key: NotNull(jsonNode.key)});
    }

    var i;

    var reactAttributes = jsonNode.reactAttributes;

    var reactAttributesLength = 0;

    var props = null;

    if (reactAttributes)
    {
        reactAttributesLength = reactAttributes.length;
    }

    if (reactAttributesLength > 0)
    {
        props = {};
    }
    
    var constructorFunction = jsonNode.$type;
    if (!constructorFunction)
    {
        throw 'ReactNode is not recognized';
    }
    
    if (/* is component */constructorFunction.indexOf('.') > 0)
    {
        constructorFunction = GetComponentByFullName(constructorFunction);
    }

    const children = jsonNode.children;

    var childrenLength = 0;
    if (children)
    {
        childrenLength = children.length;
    }


    // collect props

    function tryTransformValue(propName)
    {
        var value = jsonNode[propName];
        if (value != null && value.$JsTransformFunctionLocation)
        {
            props[propName] = GetValueInPath(window, value.$JsTransformFunctionLocation)(value.RawValue);
            return true;
        }

        return false;
    }

    function tryProcessAsEventHandler(propName)
    {
        var value = jsonNode[propName];
        if (value != null && value.$isRemoteMethod === true)
        {
            var remoteMethodName = value.remoteMethodName;

            props[propName] = function ()
            {
                PushToEventQueue( () => HandleAction({ remoteMethodName: remoteMethodName, component: component, eventArguments: Array.prototype.slice.call(arguments) }) );
            }

            return true;
        }

        return false;
    }

    function tryProcessAsElement(propName)
    {
        var value = jsonNode[propName];
        if (value != null && value.$isElement === true)
        {
            props[propName] = ConvertToReactElement(value.Element, component);

            return true;
        }

        return false;
    }

    function IfNull(value, defaultValue)
    {
        if (defaultValue === undefined)
        {
            return value;
        }

        if (value == null)
        {
            return defaultValue;
        }

        return value;
    }

    function tryProcessAsBinding(propName)
    {
        /*
        "valueBind": {
            "eventName": "onChange",
            "$isBinding": true,
            "jsValueAccess": ["e","target","value"],
            "sourcePath": ["innerA","innerB","text"],
            "targetProp": "value"
          }
         */

        var value = jsonNode[propName];

        if (value != null && value.$isBinding === true)
        {
            var targetProp    = value.targetProp;
            var eventName     = value.eventName;
            var sourcePath    = value.sourcePath;
            var jsValueAccess = value.jsValueAccess;
            var defaultValue  = value.defaultValue;

            props[targetProp] = IfNull(GetValueInPath(component.state.$state, sourcePath), defaultValue);
            props[eventName] = function (e)
            {
                var state = component.$stateAsJsProperty;

                SetValueInPath(state, sourcePath, IfNull(GetValueInPath({ e: e }, jsValueAccess)), defaultValue);

                component.setState({ $state: Clone(state) });
            }

            return true;
        }

        return false;
    }

    for (i = 0; i < reactAttributesLength; i++)
    {
        var propName = reactAttributes[i];

        var isProcessedAsEvent = tryProcessAsEventHandler(propName);
        if (isProcessedAsEvent)
        {
            continue;
        }

        var isProcessedAsBinding = tryProcessAsBinding(propName);
        if (isProcessedAsBinding)
        {
            continue;
        }

        var processedAsTemplate = tryProcessAsElement(propName);
        if (processedAsTemplate)
        {
            continue;
        }

        if (tryTransformValue(propName))
        {
            continue;
        }

        if (name.indexOf("bind$") === 0)
        {
            continue;
        }

        props[propName] = jsonNode[propName];
    }

    if (jsonNode.innerText != null)
    {
        return createElement(constructorFunction, props, jsonNode.innerText);
    }

    if (childrenLength > 0)
    {
        var newChildren = [];

        for (i = 0; i < childrenLength; i++)
        {
            newChildren.push(ConvertToReactElement(children[i], component));
        }

        return createElement(constructorFunction, props, newChildren);
    }

    return createElement(constructorFunction, props);
}

function NormalizeEventArguments(eventArguments)
{
    function normalizeEventArgument(obj)
    {
        if (typeof obj === "string" || typeof obj === "number")
        {
            return obj;
        }

        if (obj && obj._reactName === "onClick")
        {
            return NVL(GoUpwardFindFirst(obj.target, HasId), obj.target).id;
        }

        // inputtext: // todo take from attribute
        if (obj && obj._reactName === "onChange")
        {
            return {
                target: {
                    value: obj.target.value,
                    selectionStart: obj.target.selectionStart
                }
            };
        }

        // ReSharper disable once UnusedParameter
        function canSendToServer(key, value)
        {
            if (key === "originalEvent")
            {
                return false;
            }

            return true;
        }

        return PickPropertiesToNewObject(obj, canSendToServer);
    }

    return Pipe(eventArguments,
        arr => arr.map(normalizeEventArgument),
        arr => arr.filter(IsNotEmptyObject)
    );
}

var GetNextUniqueNumber = (function()
{
    var nextValue = 0;

    return function()
    {
        return nextValue++;
    }
})();

var StateCache = 
{

};

var unAvailableStateIdList = [];

function CollectStates(component)
{
    var map = {};

    // map["0"] = { StateAsJson: component.state.$state, FullTypeNameOfState: component.$FullTypeNameOfState };

    visitChilderen(component.$rootJsonNodeForUI, "0");

    return stringifyValuesInMap(map);

    function stringifyValuesInMap(map)
    {
        function modify(key, value)
        {
            unAvailableStateIdList.push(key);
            value.StateAsJson = JSON.stringify(value.StateAsJson);
        }
        IterateObject(map,modify);

        return map;
    }

    function isComponent(jsonUiNode)
    {
        if (jsonUiNode != null)
        {
            if (jsonUiNode[RootNode] != null)
            {
                if (jsonUiNode[DotNetTypeOfReactComponent] != null)
                {
                    if (jsonUiNode.state != null)
                    {
                        return true;
                    }
                }
            } 
        }

        return false;
    }
    
    function visitChilderen(node, path)
    {
        if (node.children == null)
        {
            return;
        }

        for (let i = 0; i < node.children.length; i++)
        {
            const child = node.children[i];

            const location = path + "," + i;

            if(isComponent(child))
            {
                NotNull(child.UniqueIdOfState);

                map[location] = { StateAsJson: StateCache[child.UniqueIdOfState], FullTypeNameOfState: StateCache[child.UniqueIdOfState + "-FullTypeNameOfState"] };
                
                visitChilderen(child[RootNode], location);
                
                continue;
            }

            visitChilderen(child, location);
        }
    }
}

function HandleAction(data)
{
    var remoteMethodName = data.remoteMethodName;
    var component = data.component;
    
    function getStateAsJson()
    {
        var state = component.state.$state;

        var beforePostingState = TryGetComponentAction(component, "beforePostingState");
        if (beforePostingState)
        {
            state = beforePostingState(Clone(state));
        }

        return JSON.stringify(state);
    }

    var request =
    {
        MethodName: "HandleComponentEvent",

        EventHandlerMethodName: remoteMethodName,
        FullName   : component.constructor[DotNetTypeOfReactComponent],
        StateAsJson: getStateAsJson(),
        ChildStates: CollectStates(component)
    };
    
    request.eventArgumentsAsJsonArray = NormalizeEventArguments(data.eventArguments).map(JSON.stringify);

    function onSuccess(response)
    {
        if (response.ErrorMessage != null)
        {
            throw response.ErrorMessage;
        }

        var element = JSON.parse(response.ElementAsJsonString);
        
        function restoreState(onStateReady)
        {
            for (var j = 0; j < unAvailableStateIdList.length; j++)
            {
                StateCache[unAvailableStateIdList[j]] = null;
                StateCache[unAvailableStateIdList[j] + "-FullTypeNameOfState"] = null;
            }
            unAvailableStateIdList.length = 0;

            if (!(element.UniqueIdOfState > 0))
            {
                element.UniqueIdOfState = data.component.$UniqueIdOfState;
            }

            StateCache[element.UniqueIdOfState] = element.state;

            data.component.$stateAsJsProperty = element.state;
            data.component.$rootJsonNodeForUI = element[RootNode];

            data.component.setState({
                $rootNode: Clone(data.component.$rootJsonNodeForUI),
                $state   : Clone(data.component.$stateAsJsProperty)
            }, onStateReady);
        }

        const clientTask = element.state.ClientTask;

        element.state.ClientTask = null;

        const afterSetState = processClientTask(clientTask);

        if (afterSetState)
        {
            EventQueue.push(afterSetState);
        }
        
        restoreState(EmitNextEvent);

        function processClientTask(clientTask)
        {
            if (clientTask == null)
            {
                return null;
            }

            if (clientTask.TaskId === ClientTaskId.ComebackWithLastAction)
            {
                if (clientTask.After != null)
                {
                    throw new Error("ClientTask.After can not be use after this task");
                }

                return function()
                {
                    setTimeout(function()
                    {
                        request.StateAsJson = JSON.stringify(component.state.$state);
                        SendRequest(request, onSuccess);

                    }, clientTask.Timeout);
                };
            }

            if (clientTask.TaskId === ClientTaskId.GotoMethod)
            {
                if (clientTask.After != null)
                {
                    throw new Error("ClientTask.After can not be use after this task");
                }

                return function ()
                {
                    setTimeout(function ()
                    {
                        HandleAction({ remoteMethodName: clientTask.MethodName, component: component, eventArguments: clientTask.MethodArguments || [] });

                    }, clientTask.Timeout);
                };
                
            }

            if (clientTask.TaskId === ClientTaskId.PushHistory)
            {
                window.history.replaceState({}, clientTask.Title, clientTask.Url);

                return processClientTask(clientTask.After);
            }

            if (clientTask.TaskId === ClientTaskId.DispatchEvent)
            {
                EventBus.Dispatch(clientTask.EventName, clientTask.EventArguments);

                return processClientTask(clientTask.After);
            }

            if (clientTask.TaskId === ClientTaskId.ListenEvent)
            {
                EventBus.On(clientTask.EventName, function()
                {
                    HandleAction({ remoteMethodName: clientTask.RouteToMethod, component: component, eventArguments: arguments[0] });
                });

                return processClientTask(clientTask.After);
            }

            if (clientTask.TaskId === ClientTaskId.ListenComponentEvent)
            {
                ListenComponentEvent(clientTask, component[DotNetTypeOfReactComponent]);

                return processClientTask(clientTask.After);
            }

            if (clientTask.TaskId === ClientTaskId.CallJsFunction)
            {
                if (clientTask.After != null)
                {
                    throw new Error("ClientTask.After can not be use after this task");
                }

                return function ()
                {
                    CallJsFunctionInPath(clientTask);
                };
            }

            if (clientTask.TaskId === ClientTaskId.NavigateToUrl)
            {
                window.location.replace(location.origin + clientTask.Url);

                return processClientTask(clientTask.After);
            }

            throw Error("ClientTask not recognized.");
        }

    }
    SendRequest(request, onSuccess);
}

var componentActions = {};

function GetComponentActionLocation(fullName, actionName)
{
    return fullName + "$" + actionName;
}

function RegisterActionToComponent(parameterObject)
{
    componentActions[GetComponentActionLocation(parameterObject.typeNameOfComponent, parameterObject.actionName)] = parameterObject.handlerFunction;
}

function TryGetComponentAction(component, actionName)
{
    return componentActions[GetComponentActionLocation(component[DotNetTypeOfReactComponent], actionName)];
}

function TryDispatchComponentAction(component, actionName)
{
    EventBus.Dispatch(GetComponentActionLocation(component[DotNetTypeOfReactComponent], actionName), component);
}

var ComponentDefinitions = {
};

function DefineComponent(componentDeclaration)
{
    const dotNetTypeOfReactComponent = componentDeclaration[DotNetTypeOfReactComponent];

    const component = ComponentDefinitions[dotNetTypeOfReactComponent];
    if (component)
    {
        // return component;
    }
    
    class NewComponent extends React.Component
    {
        constructor(props)
        {
            super(props||{});

            // register stateId
            NotFrozen(componentDeclaration);
            NotNull(componentDeclaration[FullTypeNameOfState]);

            const uniqueIdOfState = GetNextUniqueNumber();
            
            this.$UniqueIdOfState = componentDeclaration.UniqueIdOfState = uniqueIdOfState;

            StateCache[uniqueIdOfState] = componentDeclaration.state;
            StateCache[uniqueIdOfState + "-FullTypeNameOfState"] = componentDeclaration[FullTypeNameOfState];
            

            this.$FullTypeNameOfState = componentDeclaration[FullTypeNameOfState];
            this.$stateAsJsProperty   = componentDeclaration.state;
            this.$rootJsonNodeForUI   = componentDeclaration[RootNode];

            this.state =
            {
                $rootNode: Clone(this.$rootJsonNodeForUI),
                $state   : Clone(this.$stateAsJsProperty)
            };

            this[DotNetTypeOfReactComponent] = dotNetTypeOfReactComponent;
        }

        render()
        {
            NotFrozen(this.$rootJsonNodeForUI);

            return ConvertToReactElement(this.$rootJsonNodeForUI, this);
        }

        componentDidMount()
        {
            TryDispatchComponentAction(this, "componentDidMount");
        }

        componentWillUnmount()
        {
            TryDispatchComponentAction(this, "componentWillUnmount");
        }
    }
    
    NewComponent[DotNetTypeOfReactComponent] = dotNetTypeOfReactComponent;

    ComponentDefinitions[dotNetTypeOfReactComponent] = NewComponent;
    
    return NewComponent;
}

function SendRequest(request, onSuccess)
{
    BeforeSendRequest(request);

    ReactDotNet.SendRequest(request, onSuccess);

    function BeforeSendRequest(request)
    {
        request.AvailableWidth  = document.documentElement.clientWidth;
        request.AvailableHeight = document.documentElement.clientHeight;
        request.SearchPartOfUrl = window.location.search;
    }
}

function RenderComponentIn(obj)
{
    var fullTypeNameOfReactComponent = obj.fullTypeNameOfReactComponent;
    var containerHtmlElementId       = obj.containerHtmlElementId;

    OnDocumentReady(function()
    {
        var request =
        {
            MethodName: "FetchComponent",
            FullName: fullTypeNameOfReactComponent
        };

        function onSuccess(response)
        {
            if (response.NavigateToUrl)
            {
                window.location.replace(location.origin + response.NavigateToUrl);
                return;
            }

            if (response.ErrorMessage != null)
            {
                throw response.ErrorMessage;
            }

            const element = JSON.parse(response.ElementAsJsonString);

            const component = DefineComponent(element);

            const clientTask = element.state.ClientTask;
            
            element.state.ClientTask = null;
            
            const reactElement = React.createElement(component);

            function processClientTask(clientTask)
            {
                if (clientTask == null)
                {
                    return null;
                }

                if (clientTask.TaskId === ClientTaskId.ListenComponentEvent)
                {
                    ListenComponentEvent(clientTask, fullTypeNameOfReactComponent);

                    return processClientTask(clientTask.After);
                }

                if (clientTask.TaskId === ClientTaskId.CallJsFunction)
                {
                    CallJsFunctionInPath(clientTask);

                    return processClientTask(clientTask.After);
                }

                throw new Error("Client Task not recognized");
            }

            processClientTask(clientTask);

            createRoot(document.getElementById(containerHtmlElementId)).render(reactElement);
        }
        
        SendRequest(request, onSuccess);
    });
}

function CallJsFunctionInPath(clientTask)
{
    var fn = GetValueInPath(window, clientTask.JsFunctionPath.split("."));
    if (fn == null)
    {
        throw Error("Function not found. Function is " + clientTask.JsFunctionPath);
    }

    fn.apply(null, clientTask.JsFunctionArguments);
}

function ListenComponentEvent(clientTask, fullTypeNameOfReactComponent)
{
    EventBus.On(GetComponentActionLocation(fullTypeNameOfReactComponent, clientTask.EventName), function (cmp)
    {
        HandleAction({ remoteMethodName: clientTask.RouteToMethod, component: cmp, eventArguments: [] });
    });
}

function Fetch(url, options, processResponse, callback)
{
    window.fetch(url, options).then(response => processResponse(response)).then(json => callback(json));

    // if you want to use your custom api then override here
    // jquery example
    //var jqueryOptions =
    //{
    //    url: url,
    //    method: options.method,
    //    contentType: 'application/json',
    //    data: options.body
    //};
    //$.ajax(jqueryOptions, 'json').done(function (response) {
    //    callback(response);
    //});
}

function GetComponentByFullName(componentFullName)
{
    var component = ReactDotNet.FindComponentByFullName(componentFullName);
    if (component == null)
    {
        throw 'Component not found. Component name is ' + componentFullName;
    }

    return component;
}

var ReactDotNet =
{
    OnDocumentReady: OnDocumentReady,
    RegisterActionToComponent: RegisterActionToComponent,
    HandleAction: HandleAction,
    DispatchEvent: EventBus.Dispatch,
    RenderComponentIn: RenderComponentIn,
    SendRequest: function (request, callback)
    {
        Fetch("/component/HandleRequest", {
                method: "POST",
                headers:
                {
                    'Accept': "application/json",
                    'Content-Type': "application/json"
                },
                body: JSON.stringify(request)
            },
            response => response.json(),
            json => callback(json)
        );
    },
    FindComponentByFullName: function()
    {
        throw 'Override this method.';
    }
};

window.ReactDotNet = ReactDotNet;

export default ReactDotNet;

