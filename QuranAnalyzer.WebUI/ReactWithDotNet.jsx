/**
*  Written by Abdullah Beyaztaş
*  Manages react ui by using react render informations which incoming from server.
*/

import React from 'react';
import {createRoot} from 'react-dom/client';

var createElement = React.createElement;

const DotNetTypeOfReactComponent = '___Type___';
const FullTypeNameOfState = '___TypeOfState___';
const RootNode = '___RootNode___';

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

function PickupPropertiesToNewObject(obj, dotSeperatedPropertyNames)
{
    const returnObj = {};

    for (let key in obj)
    {
        if (obj.hasOwnProperty(key))
        {
            if (dotSeperatedPropertyNames.indexOf('.' + key + '.') >= 0)
            {
                returnObj[key] = obj[key];
            }
        }
    }

    return returnObj;
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
    const typeofValue = typeof obj;

    if (typeofValue === 'number' || typeofValue === 'string' )
    {
        return false;
    }
    
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

function OnReactStateReady()
{
    IsExecutingEvent = false;

    EmitNextEvent();
}

function EmitNextEvent()
{
    if (IsExecutingEvent)
    {
        throw "ReactWithDotNet event queue problem occured.";
    }
    
    if (EventQueue.length > 0)
    {
        const fn = EventQueue.shift();

        IsExecutingEvent = true;
        
        fn();
    }
}

function PushToEventQueue(fn)
{
    EventQueue.push(fn);

    if (!IsExecutingEvent)
    {
        EmitNextEvent();
    }
}

function Pipe(value)
{
    for (let i = 1; i < arguments.length; i++)
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
    // is ReactWithDotNet component
    if (jsonNode[DotNetTypeOfReactComponent])
    {
        const cmp = DefineComponent(jsonNode);

        return createElement(cmp,
            {
                key: NotNull(jsonNode.key),
                ParentReactWithDotNetManagedComponent: component,
                $jsonNode: jsonNode
            });
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
        constructorFunction = GetExternalJsObject(constructorFunction);
    }

    const children = jsonNode.$children;

    var childrenLength = 0;
    if (children)
    {
        childrenLength = children.length;
    }


    // collect props

    function tryTransformValue(propName)
    {
        const value = jsonNode[propName];
        if (value != null && value.$JsTransformFunctionLocation)
        {
            props[propName] = GetValueInPath(window, value.$JsTransformFunctionLocation)(value.RawValue);
            return true;
        }

        if (value != null && value.$transformValueFunction)
        {
            var fn = ExternalJsObjectMap[value.$transformValueFunction];
            
            props[propName] = fn(value.RawValue);
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

    function tryProcessAsItemsTemplate(propName)
    {
        const value = jsonNode[propName];
        if (value != null && value.___ItemTemplates___)
        {
            props[propName] = function (item)
            {
                if (item == null && value.___TemplateForNull___)
                {
                    return ConvertToReactElement(value.___TemplateForNull___);
                }
                
                const length = value.___ItemTemplates___.length;
                for (let j = 0; j < length; j++)
                {
                    const key = value.___ItemTemplates___[j].Key;

                    // try find as TreeNode
                    if (key.key != null && item && item.key != null && key.key === item.key)
                    {
                        return ConvertToReactElement(value.___ItemTemplates___[j].Value);
                    }

                    if (JSON.stringify(key) === JSON.stringify(item))
                    {
                        return ConvertToReactElement(value.___ItemTemplates___[j].Value);
                    }
                }

                throw 'item template not found';
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

        const value = jsonNode[propName];

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
                const state = Clone(component.state.$state);

                SetValueInPath(state, sourcePath, IfNull(GetValueInPath({ e: e }, jsValueAccess)), defaultValue);

                component.setState({ $state: state });
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

        if (tryProcessAsItemsTemplate(propName))
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

        if (obj && (obj._reactName === "onMouseLeave" || obj._reactName === "onMouseEnter"))
        {
            return PickupPropertiesToNewObject(obj, '.clientX.clientY.pageX.pageY.screenX.screenY.timeStamp.type.');
        }

        // ReSharper disable once UnusedParameter
        function canSendToServer(key, value)
        {
            if (key === "originalEvent")
            {
                return false;
            }

            if (key === "target")
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

function HandleAction(data)
{
    const remoteMethodName = data.remoteMethodName;
    var component = data.component;
    
    var request =
    {
        MethodName: "HandleComponentEvent",

        EventHandlerMethodName: NotNull(remoteMethodName),
        FullName   : component.constructor[DotNetTypeOfReactComponent],
        CapturedStateTree: component.CaptureStateTree()
    };
    
    request.eventArgumentsAsJsonArray = NormalizeEventArguments(data.eventArguments).map(JSON.stringify);

    function onSuccess(response)
    {
        if (response.ErrorMessage != null)
        {
            throw response.ErrorMessage;
        }

        const element = response.ElementAsJson;
        
        function restoreState(onStateReady)
        {
            data.component.setState({
                $rootNode: element[RootNode],
                $state   : element.state
            }, onStateReady);
        }

        const clientTasks = response.ClientTasks;
        
        if (clientTasks != null)
        {
            for (let i = 0; i < clientTasks.length; i++)
            {
                const clientTask = clientTasks[i];

                if (clientTask.TaskId === ClientTaskId.ComebackWithLastAction)
                {
                    EventQueue.push(() =>
                    {
                        setTimeout(() =>
                        {
                            request.CapturedStateTree = component.CaptureStateTree();
                            SendRequest(request, onSuccess);

                        }, clientTask.Timeout);

                        OnReactStateReady();
                    });

                    continue;
                }

                if (clientTask.TaskId === ClientTaskId.GotoMethod)
                {
                    EventQueue.push(() =>
                    {
                        setTimeout(() =>
                        {
                            HandleAction({ remoteMethodName: clientTask.MethodName, component: component, eventArguments: clientTask.MethodArguments || [] });

                        }, clientTask.Timeout);

                        OnReactStateReady();
                    });

                    continue;
                }

                if (clientTask.TaskId === ClientTaskId.PushHistory)
                {
                    window.history.replaceState({}, clientTask.Title, clientTask.Url);

                    continue;
                }

                if (clientTask.TaskId === ClientTaskId.DispatchEvent)
                {
                    EventBus.Dispatch(clientTask.EventName, clientTask.EventArguments);

                    continue;
                }

                if (clientTask.TaskId === ClientTaskId.ListenEvent)
                {
                    EventBus.On(clientTask.EventName, (eventArgumentsAsArray)=>
                    {
                        HandleAction({ remoteMethodName: clientTask.RouteToMethod, component: component, eventArguments: eventArgumentsAsArray });
                    });

                    continue;
                }

                if (clientTask.TaskId === ClientTaskId.ListenComponentEvent)
                {
                    ListenComponentEvent(clientTask, component[DotNetTypeOfReactComponent]);

                    continue;
                }

                if (clientTask.TaskId === ClientTaskId.CallJsFunction)
                {
                    EventQueue.push(() =>
                    {
                        CallJsFunctionInPath(clientTask);
                        OnReactStateReady();
                    });

                    continue;
                }

                if (clientTask.TaskId === ClientTaskId.NavigateToUrl)
                {
                    window.location.replace(location.origin + clientTask.Url);

                    continue;
                }

                throw Error("ClientTask not recognized.");
            }    
        }
        
        restoreState(OnReactStateReady);
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

// todo: fix me
function TryDispatchComponentAction(component, actionName)
{
    EventBus.Dispatch(GetComponentActionLocation(component[DotNetTypeOfReactComponent], actionName), component);
}

const ComponentDefinitions = {};

function DefineComponent(componentDeclaration)
{
    const dotNetTypeOfReactComponent = componentDeclaration[DotNetTypeOfReactComponent];

    const component = ComponentDefinitions[dotNetTypeOfReactComponent];
    if (component)
    {
        return component;
    }
    
    class NewComponent extends React.Component
    {
        constructor(props)
        {
            super(props||{});

            this.state =
            {
                $rootNode: NotNull(props.$jsonNode[RootNode]),
                $state   : NotNull(props.$jsonNode.state)
            };

            this[DotNetTypeOfReactComponent] = dotNetTypeOfReactComponent;

            if (props.ParentReactWithDotNetManagedComponent)
            {
                if (props.ParentReactWithDotNetManagedComponent.ReactWithDotNetManagedChildComponents)
                {
                    props.ParentReactWithDotNetManagedComponent.ReactWithDotNetManagedChildComponents.push(this);
                }
            }

            this.ReactWithDotNetManagedChildComponents = [];
        }
        
        CaptureStateTree(map, prefix)
        {
            const isRoot = map == null;
            if (isRoot)
            {
                map = {};
                prefix = "0";
            }
            
            map[prefix] = { StateAsJson: JSON.stringify(this.state.$state), FullTypeNameOfState: NotNull(this.props.$jsonNode[FullTypeNameOfState]) };
            
            if (this.ReactWithDotNetManagedChildComponents)
            {
                for (let i = 0; i < this.ReactWithDotNetManagedChildComponents.length; i++)
                {
                    const child = this.ReactWithDotNetManagedChildComponents[i];

                    if (child.CaptureStateTree)
                    {
                        child.CaptureStateTree(map, prefix + "," + i);
                    }
                }
            }

            if (isRoot)
            {
                return map;
            }

            return null;
        }
        
        render()
        {
            this.ReactWithDotNetManagedChildComponents = [];
            
            return ConvertToReactElement(this.state.$rootNode, this);
        }

        componentDidMount()
        {
            if (this.props.$jsonNode.___HasComponentDidMountMethod___)
            {
                HandleAction({ remoteMethodName: 'ComponentDidMount', component: this, eventArguments: [] });
                return;
            }
        }

        componentWillUnmount()
        {
            
        }
    }
    
    NewComponent[DotNetTypeOfReactComponent] = dotNetTypeOfReactComponent;

    ComponentDefinitions[dotNetTypeOfReactComponent] = NewComponent;
    
    return NewComponent;
}

function SendRequest(request, onSuccess)
{
    BeforeSendRequest(request);

    ReactWithDotNet.SendRequest(request, onSuccess);

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
        const request =
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

            const element = response.ElementAsJson;

            const component = DefineComponent(element);
            
            const clientTasks = response.ClientTasks;

            function renderCallback(component)
            {
                if (clientTasks != null)
                {
                    for (let i = 0; i < clientTasks.length; i++)
                    {
                        const clientTask = clientTasks[i];

                        if (clientTask.TaskId === ClientTaskId.ListenEvent)
                        {
                            EventBus.On(clientTask.EventName, (eventArgumentsAsArray)=>
                            {
                                HandleAction({ remoteMethodName: clientTask.RouteToMethod, component: component, eventArguments: eventArgumentsAsArray });
                            });

                            continue;
                        }
                    
                        if (clientTask.TaskId === ClientTaskId.ListenComponentEvent)
                        {
                            ListenComponentEvent(clientTask, fullTypeNameOfReactComponent);

                            continue;
                        }

                        if (clientTask.TaskId === ClientTaskId.CallJsFunction)
                        {
                            CallJsFunctionInPath(clientTask);

                            continue;
                        }

                        throw new Error("Client Task not recognized");
                    }    
                }
                
                OnReactStateReady();
            }

            const reactElement = React.createElement(component, { key: '0', $jsonNode: element, ref: renderCallback });
            
            createRoot(document.getElementById(containerHtmlElementId)).render(reactElement);
        }
        
        SendRequest(request, onSuccess);
    });
}

function CallJsFunctionInPath(clientTask)
{
    GetExternalJsObject(clientTask.JsFunctionPath).apply(null, clientTask.JsFunctionArguments);
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

const ExternalJsObjectMap = {};
function RegisterExternalJsObject(key/*string*/, value/* componentFullName | functionName */)
{
    if (ExternalJsObjectMap[key] != null)
    {
        console.log(key + ' already registered.');
    }
    return ExternalJsObjectMap[key] = value;
}
function GetExternalJsObject(key)
{
    const value = ExternalJsObjectMap[key];
    if (value == null)
    {
        throw CreateNewDeveloperError(key + ' External js object not not found. You should register by using method: ReactWithDotNet.RegisterExternalJsObject');
    }

    return value;
}

function CreateNewDeveloperError(message)
{
    throw new Error('ReactWithDotNet developer error occured.' + message);
}

var ReactWithDotNet =
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
    RegisterExternalJsObject : RegisterExternalJsObject
};

window.ReactWithDotNet = ReactWithDotNet;

export default ReactWithDotNet;

