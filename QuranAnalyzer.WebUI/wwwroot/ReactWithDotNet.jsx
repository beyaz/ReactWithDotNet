/**
 *  == ReactWithDotNet ==
 *  Written by Abdullah Beyaztaş
 *  Manages react ui by using react render informations which incoming from server.
 */

import React from 'react';
import {createRoot} from 'react-dom/client';

var createElement = React.createElement;

const DotNetTypeOfReactComponent = '$Type';
const FullTypeNameOfState = '$TypeOfState';
const RootNode = '$RootNode';

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
    if (IsSerializablePrimitiveJsValue(obj))
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
        throw CreateNewDeveloperError("ReactWithDotNet event queue problem occured.");
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
            throw CreateNewDeveloperError("Path is not read. Path:" + steps.join("."));
        }

        obj = obj[steps[i]];
    }

    return obj;
}

function SetValueInPath(obj, steps, value)
{
    if (obj == null)
    {
        throw CreateNewDeveloperError("SetValueInPath->" + value);
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
        throw CreateNewDeveloperError("value cannot be null.");
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
    
    PushHistory: 4,
    // free 5,
    GotoMethod: 6,
    NavigateToUrl : 7
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


const GetNextSequence = (() =>
{
    var sequence = 1;

    return () => { return sequence++; };
})();
function ConvertToReactElement(jsonNode, component, isConvertingRootNode)
{   
    if (jsonNode.$FakeChild != null)
    {
        jsonNode = component.props.$jsonNode.$RootNode.$children[jsonNode.$FakeChild];
    }
    
    // is ReactWithDotNet component
    if (jsonNode[DotNetTypeOfReactComponent])
    {
        const cmp = DefineComponent(jsonNode);

        return createElement(cmp,
        {
            key: NotNull(jsonNode.key),
            ParentReactWithDotNetManagedComponent: component,
            $jsonNode: jsonNode,
            $SyncId: GetNextSequence()
        });
    }

    let props = null;
    
    var constructorFunction = jsonNode.$tag;
    if (!constructorFunction)
    {
        throw CreateNewDeveloperError('ReactNode is not recognized');
    }
    
    if (/* is component */constructorFunction.indexOf('.') > 0)
    {
        constructorFunction = GetExternalJsObject(constructorFunction);
    }

    // calculate props
    for (let propName in jsonNode)
    {
        if (!jsonNode.hasOwnProperty(propName))
        {
            continue;
        }

        // is related information with .net
        if (propName[0] === '$')
        {
            continue;
        }

        if (props === null)
        {
            props = {};
        }
        
        const propValue = jsonNode[propName];

        if (propValue != null)
        {
            // tryProcessAsEventHandler
            if (propValue.$isRemoteMethod === true)
            {
                const remoteMethodName = propValue.remoteMethodName;

                props[propName] = function()
                {
                    PushToEventQueue(() => HandleAction({ remoteMethodName: remoteMethodName, component: component, eventArguments: Array.prototype.slice.call(arguments) }));
                }

                continue;
            }

            // tryProcessAsBinding
            /*
                "valueBind": {
                    "eventName": "onChange",
                    "$isBinding": true,
                    "jsValueAccess": ["e","target","value"],
                    "sourcePath": ["innerA","innerB","text"],
                    "targetProp": "value"
                  }
                 */
            if (propValue.$isBinding === true)
            {
                const targetProp = propValue.targetProp;
                const eventName = propValue.eventName;
                const sourcePath = propValue.sourcePath;
                const jsValueAccess = propValue.jsValueAccess;
                const defaultValue = propValue.defaultValue;

                props[targetProp] = IfNull(GetValueInPath(component.state.$state, sourcePath), defaultValue);
                props[eventName] = function(e)
                {
                    const state = Clone(component.state.$state);

                    SetValueInPath(state, sourcePath, IfNull(GetValueInPath({ e: e }, jsValueAccess)), defaultValue);

                    component.setState({ $state: state });
                }

                continue;
            }

            // tryProcessAsElement
            if (propValue.$isElement === true)
            {
                props[propName] = ConvertToReactElement(propValue.Element, component);

                continue;
            }

            // tryProcessAsItemsTemplate
            if (propValue.___ItemTemplates___)
            {
                props[propName] = function(item)
                {
                    if (item == null && propValue.___TemplateForNull___)
                    {
                        return ConvertToReactElement(propValue.___TemplateForNull___);
                    }

                    const length = propValue.___ItemTemplates___.length;
                    for (let j = 0; j < length; j++)
                    {
                        const key = propValue.___ItemTemplates___[j].Key;

                        // try find as TreeNode
                        if (key.key != null && item && item.key != null && key.key === item.key)
                        {
                            return ConvertToReactElement(propValue.___ItemTemplates___[j].Value);
                        }

                        if (JSON.stringify(key) === JSON.stringify(item))
                        {
                            return ConvertToReactElement(propValue.___ItemTemplates___[j].Value);
                        }
                    }

                    throw CreateNewDeveloperError('item template not found');
                }

                continue;
            }

            // tryTransformValue
            if (propValue.$transformValueFunction)
            {
                props[propName] = GetExternalJsObject(propValue.$transformValueFunction)(propValue.RawValue);
                continue;
            }
        }

        props[propName] = jsonNode[propName];
    }
    
    if (isConvertingRootNode === true && component.state.$RootNodeOnMouseEnter)
    {
        NotNull(props);
        
        props['onMouseEnter'] = function()
        {
            component.setState({ $onMouseEnter: true });
        }
        props['onMouseLeave'] = function()
        {
            component.setState({ $onMouseEnter: false });
        }
    }

    if (jsonNode.$text != null)
    {
        return createElement(constructorFunction, props, jsonNode.$text);
    }
    
    const children = jsonNode.$children;
    if (children)
    {
        const childrenLength = children.length;
        
        const newChildren = [];

        for (let childIndex = 0; childIndex < childrenLength; childIndex++)
        {
            newChildren.push(ConvertToReactElement(children[childIndex], component));
        }

        return createElement(constructorFunction, props, newChildren);
    }

    return createElement(constructorFunction, props);
}

/**
 * if value is string OR number OR boolean OR Date then returns TRUE
 * @param {any} value
 */
function IsSerializablePrimitiveJsValue(value)
{
    const typeofValue = typeof value;

    return typeofValue === "string" || typeofValue === "number" || typeofValue === 'boolean' || value instanceof Date;
}

function NormalizeEventArguments(eventArguments)
{
    function normalizeEventArgument(obj)
    {
        if (IsSerializablePrimitiveJsValue(obj))
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

function ProcessClientTasks(clientTasks, component)
{
    if (clientTasks == null)
    {
        return;
    }

    for (let i = 0; i < clientTasks.length; i++)
    {
        const clientTask = clientTasks[i];
                       
        if (clientTask.TaskId === ClientTaskId.GotoMethod)
        {
            NotNull(component);

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
            NotNull(component);

            EventBus.On(clientTask.EventName, (eventArgumentsAsArray)=>
            {
                HandleAction({ remoteMethodName: clientTask.RouteToMethod, component: component, eventArguments: eventArgumentsAsArray });
            });

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

        throw CreateNewDeveloperError("ClientTask not recognized.");
    }
}

function HandleAction(data)
{
    const remoteMethodName = data.remoteMethodName;
    const component = NotNull(data.component);
    
    const request =
    {
        MethodName: "HandleComponentEvent",

        EventHandlerMethodName: NotNull(remoteMethodName),
        FullName   : NotNull(component.constructor)[DotNetTypeOfReactComponent],
        CapturedStateTree: component.CaptureStateTree()
    };
    
    request.eventArgumentsAsJsonArray = NormalizeEventArguments(data.eventArguments).map(JSON.stringify);

    function onSuccess(response)
    {
        if (response.ErrorMessage != null)
        {
            throw CreateNewDeveloperError(response.ErrorMessage);
        }

        const element = response.ElementAsJson;
        
        function restoreState(onStateReady)
        {
            const newState = {
                $rootNode: NotNull(element[RootNode]),
                $state: NotNull(element.state),
                $SyncId: component.state.$SyncId + 1,
                $clientTasks: element.$ClientTasks
            };

            if (element.$RootNodeOnMouseEnter)
            {
                newState.$onMouseEnter = false;
                newState.$RootNodeOnMouseEnter = element.$RootNodeOnMouseEnter;
            }
            
            data.component.setState(newState, onStateReady);
        }

        restoreState(OnReactStateReady);
    }

    SendRequest(request, onSuccess);
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
                $state: NotNull(props.$jsonNode.state),
                $SyncId: props.$SyncId
            };

            if (props.$jsonNode.$RootNodeOnMouseEnter)
            {
                this.state.$RootNodeOnMouseEnter = props.$jsonNode.$RootNodeOnMouseEnter;
            }

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


            // calculate root props if exists
            if (isRoot)
            {
                const jsonNode = this.props.$jsonNode;
                
                let props = null;
                for (let key in jsonNode)
                {
                    if (jsonNode.hasOwnProperty(key))
                    {
                        if (key === RootNode)
                        {
                            if (jsonNode[key].$children)
                            {
                                if (jsonNode[key].$children.length > 0)
                                {
                                    if (props === null)
                                    {
                                        props = {};
                                    }
                                    props.$childrenCount = jsonNode[key].$children.length;
                                }
                            }
                        }
                        
                        if (key === RootNode ||
                            key === DotNetTypeOfReactComponent ||
                            key === FullTypeNameOfState ||
                            key === '$HasComponentDidMountMethod' ||
                            key === '$RootNodeOnMouseEnter' ||
                            key === '$ClientTasks'||
                            key === 'key' ||
                            key === 'state')
                        {
                            continue;
                        }

                        if (props === null)
                        {
                            props = {};
                        }

                        props[key] = jsonNode[key];
                    }
                }

                
                
                if (props)
                {
                    map[prefix].props = props;
                }
            }
            
            
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

            const state = this.state;
            
            if (state.$onMouseEnter === true)
            {
                return ConvertToReactElement(state.$RootNodeOnMouseEnter, this, /*isConvertingRootNode*/true);    
            }

            return ConvertToReactElement(state.$rootNode, this, /*isConvertingRootNode*/true);
        }

        componentDidMount()
        {
            if (this.props.$jsonNode.$HasComponentDidMountMethod)
            {
                HandleAction({ remoteMethodName: 'ComponentDidMount', component: this, eventArguments: [] });
                return;
            }
        }

        componentWillUnmount()
        {
            
        }        

        componentDidUpdate(nextProps)
        {
            ProcessClientTasks(this.state.$clientTasks, this);
        }

        static getDerivedStateFromProps(props, state) 
        {
            const syncIdInState = state.$SyncId;
            const syncIdInProp  = props.$SyncId;

            if (isNaN(syncIdInState) || isNaN(syncIdInProp))
            {
                return null;
            }

            if (syncIdInState > syncIdInProp)
            {
                 return null;
            }

            if (syncIdInState !== syncIdInProp)
            {
                return {
                    $rootNode: NotNull(props.$jsonNode[RootNode]),
                    $SyncId: syncIdInProp,
                    $clientTasks: props.$jsonNode.$ClientTasks
                };
            }

            return null;
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
        request.ClientWidth  = document.documentElement.clientWidth;
        request.ClientHeight = document.documentElement.clientHeight;
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

            function renderCallback(component)
            {
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

const ExternalJsObjectMap = {
    'RegExp': (x) => new RegExp(x)
};
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
    return new Error('ReactWithDotNet developer error occured.' + message);
}

var ReactWithDotNet =
{
    OnDocumentReady: OnDocumentReady,
    HandleAction: HandleAction,
    DispatchEvent: EventBus.Dispatch,
    RenderComponentIn: RenderComponentIn,
    SendRequest: function (request, callback)
    {
        Fetch("/HandleReactWithDotNetRequest", {
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
    RegisterExternalJsObject: RegisterExternalJsObject,
    GetExternalJsObject: GetExternalJsObject
};

window.ReactWithDotNet = ReactWithDotNet;

export default ReactWithDotNet;

