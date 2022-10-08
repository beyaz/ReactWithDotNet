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
const ClientTasks = '$ClientTasks';
const SyncId = '$SyncId';
const DotNetState = '$State';
const HasComponentDidMountMethod = '$HasComponentDidMountMethod';
const ComponentRefKey = '$Key';
const ON_COMPONENT_DESTROY = '$ON_COMPONENT_DESTROY';
const CUSTOM_EVENT_LISTENER_MAP = '$CUSTOM_EVENT_LISTENER_MAP';
const DotNetProperties = 'DotNetProperties';

const EventBus =
{
    On: function(event, callback)
    {
        document.addEventListener(event, callback);
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

const FunctionExecutionQueue = [];

var FunctionExecutionQueueStateIsExecuting = false;

function OnReactStateReady()
{
    FunctionExecutionQueueStateIsExecuting = false;

    EmitNextFunctionInFunctionExecutionQueue();
}

function EmitNextFunctionInFunctionExecutionQueue()
{
    if (FunctionExecutionQueueStateIsExecuting)
    {
        throw CreateNewDeveloperError("ReactWithDotNet event queue problem occured.");
    }
    
    if (FunctionExecutionQueue.length > 0)
    {
        const fn = FunctionExecutionQueue.shift();

        FunctionExecutionQueueStateIsExecuting = true;
        
        fn();
    }
}

function PushToFunctionExecutionQueue(fn)
{
    FunctionExecutionQueue.push(fn);

    if (!FunctionExecutionQueueStateIsExecuting)
    {
        EmitNextFunctionInFunctionExecutionQueue();
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

function ShouldBeNumber(value)
{
    if (typeof value === 'number')
    {
        if (!isNaN(value))
        {
            return value;
        }
    }

    throw CreateNewDeveloperError("value should be number.");
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
    InitializeDotnetComponentEventListener: 5,
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

const VisitFiberNodeForCaptureState = (parentScope, fiberNode) =>
{
    var scope = parentScope;

    var isFiberNodeRelatedWithDotNetComponent = fiberNode.type && fiberNode.type[DotNetTypeOfReactComponent];
    if (isFiberNodeRelatedWithDotNetComponent)
    {
        var map = parentScope.map;

        var breadcrumb = parentScope.breadcrumb + ',' + parentScope.index;

        parentScope.index++;

        if (map[breadcrumb] !== undefined)
        {
            throw CreateNewDeveloperError('Problem when traversing nodes');
        }
        map[breadcrumb] =
        {
            StateAsJson: JSON.stringify(fiberNode.stateNode.state[DotNetState]),
            FullTypeNameOfState: NotNull(fiberNode.memoizedProps.$jsonNode[FullTypeNameOfState])
        };

        scope = { map: map, index: 0, breadcrumb: breadcrumb };
    }

    var child = fiberNode.child;
    while (child)
    {
        VisitFiberNodeForCaptureState(scope, child);
        child = child.sibling;
    }
};

const CaptureStateTreeFromFiberNode = (rootFiberNode) =>
{
    // I'dont know what is going here :)
    if (rootFiberNode.alternate && rootFiberNode.actualStartTime < rootFiberNode.alternate.actualStartTime)
    {
        rootFiberNode = rootFiberNode.alternate;
    }

    var map = {};

    map['0'] =
    {
        StateAsJson: JSON.stringify(rootFiberNode.stateNode.state[DotNetState]),
        FullTypeNameOfState: NotNull(rootFiberNode.memoizedProps.$jsonNode[FullTypeNameOfState])
    };

    var rootScope = { map: map, index: 0, breadcrumb: '0' };

    var child = rootFiberNode.child;
    while (child)
    {
        VisitFiberNodeForCaptureState(rootScope, child);
        child = child.sibling;
    }
    
    map['0'][DotNetProperties] = Object.assign({}, NotNull(rootFiberNode.stateNode.state[DotNetProperties]));

    // calculate $childrenCount
    {
        const rootNode = rootFiberNode.stateNode.state[RootNode];
        if (rootNode && rootNode.$children && rootNode.$children.length > 0)
        {
            map['0'][DotNetProperties].$childrenCount = rootNode.$children.length;
        }
    }

    return map;
};

const GetNextSequence = (() =>
{
    var sequence = 1;

    return () => { return sequence++; };
})();


class LinkedListNode 
{
    constructor(data) 
    {
        this.data = data
        this.next = null                
    }
}
class LinkedList 
{
    constructor()
    {
        this.head = null;
        this.size = 0;
    }

    add(data)
    {
	    var node = new LinkedListNode(data);

	    var current;

	    if (this.head == null)
        {
            this.head = node;
        }
        else 
        {
		    current = this.head;

		    // iterate to the end of the list
            while (current.next) 
            {
			    current = current.next;
		    }

		    // add node
		    current.next = node;
	    }
	    this.size++;
    }

    removeFirst(isMatch)
    {
	    var current = this.head;
	    var prev = null;

        while (current != null)
        {
            if (isMatch(current.data) === true)
            {
                if (prev == null) 
                {
				    this.head = current.next;
                }
                else
                {
				    prev.next = current.next;
			    }
			    this.size--;
			    return current.data;
		    }
		    prev = current;
		    current = current.next;
	    }
	    return -1;
    }

    first(isMatch)
    {
	    var current = this.head;

	    // iterate over the list
        while (current != null) 
        {
            if (isMatch(current.data) === true)
            {
                return current.data;
            }
		    current = current.next;
	    }

	    return null;
    }

    visitAll(action)
    {
        if (this.head == null)
        {
            return;
        }

	    let current = this.head;

        while (current.next) 
        {
            action(current.data);
			current = current.next;
		}
    }
}

class ComponentCache 
{
    constructor()
    {
        this.linkedList = new LinkedList();
    }

    Register(component)
    {
        NotNull(component);

        var existingComponent = this.linkedList.first(x => x === component);
        if (existingComponent)
        {
            return;
        }

        this.linkedList.add(component);
    }

    FindComponentByKey(key)
    {
        const isMatch = (component) =>
        {
            if (component && component.state)
            {
                if (component.state[ComponentRefKey] === key)
                {
                    return true;
                }
            }

            return false;
        };

        return this.linkedList.first(isMatch);
    }

    Unregister(component)
    {
        this.linkedList.removeFirst(x => x === component);
    }

    PrintAll()
    {
        this.linkedList.visitAll(console.log);
    }
}

const COMPONENT_CACHE = new ComponentCache();


function isEquivent(a, b)
{
	if(a === b)
	{
		return true;
	}
		
	if(a instanceof Date && typeof b instanceof Date)
	{
		return a.valueOf() === b.valueOf();
	}
		
	if(typeof a === 'object' && typeof b === 'object')
	{
		return isTwoLiteralObjectEquivent(a, b);
	}
}
function isTwoLiteralObjectEquivent(o1, o2)
{	
    for(var p in o1)
	{
        if(o1.hasOwnProperty(p))
		{
            if(!isEquivent(o1[p], o2[p]))
			{
                return false;
            }
        }
    }
	
    for(var p in o2)
	{
        if(o2.hasOwnProperty(p))
		{
            if(!isEquivent(o1[p], o2[p]))
			{
                return false;
            }
        }
    }
	
    return true;
};

function tryToFindCachedMethodInfo(targetComponent, remoteMethodName, eventArguments)
{
    if (targetComponent.props &&
        targetComponent.props.$jsonNode &&
        targetComponent.props.$jsonNode.$CachedMethods)
    {
        for (var i = 0; i < targetComponent.props.$jsonNode.$CachedMethods.length; i++)
        {
            const cachedMethodInfo = targetComponent.props.$jsonNode.$CachedMethods[i];

            if (cachedMethodInfo.MethodName === remoteMethodName && cachedMethodInfo.IgnoreParameters)
            {
                return cachedMethodInfo;
            }

            if (cachedMethodInfo.MethodName === remoteMethodName && eventArguments.length === 1)
            {
                if (isTwoLiteralObjectEquivent(eventArguments[0], cachedMethodInfo.Parameter))
                {
                    return cachedMethodInfo;
                }                       
            }
        }
    }

    return null;
}

function ConvertToEventHandlerFunction(remoteMethodInfo)
{
    const remoteMethodName   = remoteMethodInfo.remoteMethodName;
    const targetComponentKey = remoteMethodInfo.TargetKey;
    const functionNameOfGrabEventArguments = remoteMethodInfo.FunctionNameOfGrabEventArguments;
    const stopPropagation = remoteMethodInfo.StopPropagation;
    

    NotNull(remoteMethodName);
    NotNull(targetComponentKey);

    return function ()
    {
        if (stopPropagation)
        {
            if (arguments.length === 0)
            {
                throw CreateNewDeveloperError("There is no event argument for applying StopPropagation");
            }

            if (arguments[0] == null || arguments[0].constructor.prototype.stopPropagation == null)
            {
                throw CreateNewDeveloperError("Event argument not support StopPropagation");
            }

            arguments[0].stopPropagation();
        }

        const targetComponent = COMPONENT_CACHE.FindComponentByKey(targetComponentKey);
        if (targetComponent === null)
        {
            throw CreateNewDeveloperError('Target component not found. Target component key is ' + targetComponentKey);
        }

        let eventArguments = Array.prototype.slice.call(arguments);

        if (functionNameOfGrabEventArguments)
        {
            eventArguments = GetExternalJsObject(functionNameOfGrabEventArguments)(eventArguments);
        }

        const cachedMethodInfo = tryToFindCachedMethodInfo(targetComponent, remoteMethodName, eventArguments);
        if (cachedMethodInfo)
        {
            const newState = CaclculateNewStateFromJsonElement(targetComponent.state, cachedMethodInfo.ElementAsJson);

            targetComponent.setState(newState);

            return;                
        }

        if (IsWaitingRemoteResponse === true)
        {
            StartAction(/*remoteMethodName*/remoteMethodName, /*component*/targetComponent, /*eventArguments*/eventArguments);
            return;
        }

        // TODO: check 
        if (FunctionExecutionQueueStateIsExecuting === true)
        {            
            FunctionExecutionQueueStateIsExecuting = false;
        }        

        StartAction(/*remoteMethodName*/remoteMethodName, /*component*/targetComponent, /*eventArguments*/eventArguments);
    }
}

const CreateNewBuildContext = () =>
{
    const context = {};

    return context;
};

function ConvertToReactElement(buildContext, jsonNode, component, isConvertingRootNode)
{
    if (jsonNode == null)
    {
        return null;
    }

    if (typeof jsonNode === 'string')
    {
        return jsonNode;
    }

    if (jsonNode.$FakeChild != null)
    {
        jsonNode = component.props.$jsonNode[RootNode].$children[jsonNode.$FakeChild];
    }
    
    // is ReactWithDotNet component
    if (jsonNode[DotNetTypeOfReactComponent])
    {
        const cmp = DefineComponent(jsonNode);

        const cmpKey = NotNull(jsonNode.key);

        const cmpProps =
        {
            key: cmpKey,
            $jsonNode: jsonNode
        };

        cmpProps[SyncId] = GetNextSequence();

        return createElement(cmp, cmpProps);
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
                props[propName] = ConvertToEventHandlerFunction(propValue);

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

                props[targetProp] = IfNull(GetValueInPath(component.state[DotNetState], sourcePath), defaultValue);
                props[eventName] = function(e)
                {
                    const modifiedDotNetState = Clone(component.state[DotNetState]);

                    SetValueInPath(modifiedDotNetState, sourcePath, IfNull(GetValueInPath({ e: e }, jsValueAccess)), defaultValue);

                    const newState = {};
                    newState[DotNetState] = modifiedDotNetState;

                    component.setState(newState);
                }

                continue;
            }

            // tryProcessAsElement
            if (propValue.$isElement === true)
            {
                props[propName] = ConvertToReactElement(buildContext, propValue.Element, component);

                continue;
            }

            // tryProcessAsItemsTemplate
            if (propValue.___ItemTemplates___)
            {
                props[propName] = function(item)
                {
                    if (item == null && propValue.___TemplateForNull___)
                    {
                        return ConvertToReactElement(CreateNewBuildContext(), propValue.___TemplateForNull___);
                    }

                    const length = propValue.___ItemTemplates___.length;
                    for (let j = 0; j < length; j++)
                    {
                        const key = propValue.___ItemTemplates___[j].Key;

                        // try find as TreeNode
                        if (key.key != null && item && item.key != null && key.key === item.key)
                        {
                            return ConvertToReactElement(CreateNewBuildContext(), propValue.___ItemTemplates___[j].Value);
                        }

                        if (JSON.stringify(key) === JSON.stringify(item))
                        {
                            return ConvertToReactElement(CreateNewBuildContext(), propValue.___ItemTemplates___[j].Value);
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
            newChildren.push(ConvertToReactElement(buildContext, children[childIndex], component));
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

    return typeofValue === "string" || typeofValue === "number" || typeofValue === 'boolean' || value instanceof Date || value instanceof Array;
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
            return ConvertToSyntheticMouseEvent(obj);
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

function ConvertToSyntheticMouseEvent(e)
{
    let firstNotEmptyId = NVL(GoUpwardFindFirst(e.target, HasId), e.target).id;
    if (firstNotEmptyId === '')
    {
        firstNotEmptyId = null;
    }

    const target = ConvertToShadowHtmlElement(e.target);

    return {
        FirstNotEmptyId: firstNotEmptyId,
        
        altKey:    e.altKey,
        bubbles:   e.bubbles,
        clientX:   e.clientX,
        clientY:   e.clientY,
        ctrlKey:   e.ctrlKey,
        metaKey:   e.metaKey,
        movementX: e.movementX,
        movementY: e.movementY,
        pageX:     e.pageX,
        pageY:     e.pageY,        
        screenX:   e.screenX,
        screenY:   e.screenY,
        shiftKey:  e.shiftKey,
        target:    target,
        timeStamp: e.timeStamp,        
        type:      e.type
    };
}

function ConvertToShadowHtmlElement(htmlElement)
{
    return {
        tagName: htmlElement.tagName,
        id: htmlElement.id
    };
}

const EnableTraceOfClientTask = false;
function TraceClientTask(component, actionName, actionValue)
{
    if (!EnableTraceOfClientTask)
    {
        return;
    }

    const fullTypeName = component[DotNetTypeOfReactComponent];

    console.log(fullTypeName + " -> " + actionName + " -> " + actionValue);
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

            TraceClientTask(component, 'GotoMethod', clientTask.MethodName);
            
            PushToFunctionExecutionQueue(() =>
            {
                setTimeout(() =>
                {
                    StartAction(/*remoteMethodName*/clientTask.MethodName, /*component*/component, /*eventArguments*/clientTask.MethodArguments || []);

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
            TraceClientTask(component, 'DispatchEvent', clientTask.EventName);

            EventBus.Dispatch(clientTask.EventName, clientTask.EventArguments);

            continue;
        }

        if (clientTask.TaskId === ClientTaskId.ListenEvent)
        {
            NotNull(component);

            TraceClientTask(component, 'ListenEvent', clientTask.EventName);

            const onEventFired = (e) =>
            {
                const eventArgumentsAsArray = e.detail;

                StartAction(/*remoteMethodName*/clientTask.RouteToMethod, /*component*/component, /*eventArguments*/eventArgumentsAsArray);
            };

            NotNull(component[ON_COMPONENT_DESTROY]);

            component[ON_COMPONENT_DESTROY].push(() =>
            {
                TraceClientTask(component, 'UNDO-ListenEvent', clientTask.EventName);
                EventBus.Remove(clientTask.EventName, onEventFired);
            });

            EventBus.On(clientTask.EventName, onEventFired);

            continue;
        }

        if (clientTask.TaskId === ClientTaskId.InitializeDotnetComponentEventListener)
        {
            NotNull(component);

            TraceClientTask(component, 'InitializeDotnetComponentEventListener', clientTask.EventName);

            const handlerComponentKey = clientTask.HandlerComponentKey;

            // avoid multiple attach we need to ensure attach an listener at once
            {
                const customEventListenerMapKey = clientTask.EventName + ', RemoteMethodName: ' + clientTask.RouteToMethod + ' handlerComponentKey: ' + handlerComponentKey;

                if (component[CUSTOM_EVENT_LISTENER_MAP][customEventListenerMapKey])
                {
                    continue;
                }

                component[CUSTOM_EVENT_LISTENER_MAP][customEventListenerMapKey] = 1;
            }

            const onEventFired = (e) =>
            {
                const eventArgumentsAsArray = e.detail;

                const handlerComponent = COMPONENT_CACHE.FindComponentByKey(handlerComponentKey);
                if (handlerComponent === null)
                {
                    throw CreateNewDeveloperError('Handler component not found. Handler component key is ' + handlerComponentKey);
                }

                StartAction(/*remoteMethodName*/clientTask.RouteToMethod, /*component*/handlerComponent, /*eventArguments*/eventArgumentsAsArray);
            };

            NotNull(component[ON_COMPONENT_DESTROY]);

            component[ON_COMPONENT_DESTROY].push(() =>
            {
                TraceClientTask(component, 'UNDO-ListenEvent', clientTask.EventName);
                EventBus.Remove(clientTask.EventName, onEventFired);
            });

            EventBus.On(clientTask.EventName, onEventFired);

            continue;
        }        
                
        if (clientTask.TaskId === ClientTaskId.CallJsFunction)
        {
            TraceClientTask(component, 'CallJsFunction', clientTask.JsFunctionPath);

            PushToFunctionExecutionQueue(() =>
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

function StartAction(remoteMethodName, component, eventArguments)
{
    PushToFunctionExecutionQueue(() => HandleAction({ remoteMethodName: remoteMethodName, component: component, eventArguments: eventArguments }));
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
        CapturedStateTree: CaptureStateTreeFromFiberNode(component._reactInternals),
        ComponentRefId: NotNull(component.props.$jsonNode.key),
        NextAvailableKey: NextAvailableKey
    };
    
    request.eventArgumentsAsJsonArray = NormalizeEventArguments(data.eventArguments).map(JSON.stringify);

    function onSuccess(response)
    {
        IsWaitingRemoteResponse = false;

        if (response.ErrorMessage != null)
        {
            throw CreateNewDeveloperError(response.ErrorMessage);
        }

        NextAvailableKey = response.NextAvailableKey;

        ProcessDynamicCssClasses(response.DynamicStyles);

        data.component.setState(CaclculateNewStateFromJsonElement(component.state, response.ElementAsJson), OnReactStateReady);
    }

    SendRequest(request, onSuccess);
}

function CaclculateNewStateFromJsonElement(componentState, jsonElement)
{
    const newState = {};

    newState[DotNetState]     = NotNull(jsonElement[DotNetState]);
    newState[SyncId]          = ShouldBeNumber(componentState[SyncId]) + 1;
    newState[RootNode]        = NotNull(jsonElement[RootNode]);
    newState[ClientTasks]     = jsonElement[ClientTasks];
    newState[ComponentRefKey] = jsonElement.key;
    newState[DotNetProperties] = jsonElement[DotNetProperties];

    return newState;
}

const EnableTraceOfComponent = false;
function TraceComponent(component, methodName, methodArgument1, methodArgument2)
{
    if (!EnableTraceOfComponent)
    {
        return;
    }

    let fullTypeName = null;

    if (typeof (component) === 'string')
    {
        fullTypeName = component;
    }
    else
    {
        fullTypeName = component.constructor[DotNetTypeOfReactComponent];
    }    
    
    if (fullTypeName !== 'QuranAnalyzer.WebUI.Components.FixedTopPanelContainer,QuranAnalyzer.WebUI')
    {
        return;
    }

    console.log(fullTypeName + '::' + methodName);

    if (methodArgument1 !== undefined)
    {
        console.log(methodArgument1);
    }

    if (methodArgument2 !== undefined)
    {
        console.log(methodArgument2);
    }
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

            TraceComponent(this, "constructor", props);

            const initialState = {};

            initialState[DotNetState] = NotNull(props.$jsonNode[DotNetState]);
            initialState[SyncId] = ShouldBeNumber(props[SyncId]);
            initialState[RootNode] = NotNull(props.$jsonNode[RootNode]);
            initialState[ComponentRefKey] = NotNull(props.$jsonNode.key);
            initialState[DotNetProperties] = NotNull(props.$jsonNode[DotNetProperties]);

            if (props.$jsonNode[HasComponentDidMountMethod])
            {
                initialState[HasComponentDidMountMethod] = props.$jsonNode[HasComponentDidMountMethod];
            }

            if (props.$jsonNode[ClientTasks])
            {
                initialState[ClientTasks] = props.$jsonNode[ClientTasks];
            }

            initialState[DotNetTypeOfReactComponent] = dotNetTypeOfReactComponent;

            this.state = initialState;

            this[DotNetTypeOfReactComponent] = dotNetTypeOfReactComponent;

            this[ON_COMPONENT_DESTROY] = [];

            this[CUSTOM_EVENT_LISTENER_MAP] = {};

            COMPONENT_CACHE.Register(this);
            
        }
        
        render()
        {
            TraceComponent(this, "render");

            return ConvertToReactElement(CreateNewBuildContext(), this.state[RootNode], this, /*isConvertingRootNode*/true);
        }

        componentDidMount()
        {
            TraceComponent(this, "componentDidMount");

            const clientTasks = this.state[ClientTasks];            
            if (clientTasks)
            {
                const partialState = {};

                partialState[ClientTasks] = null;

                this.setState(partialState, ()=> ProcessClientTasks(clientTasks, this));
            }

            const hasComponentDidMountMethod = this.state[HasComponentDidMountMethod];
            if (hasComponentDidMountMethod)
            {
                const partialState = {};

                partialState[HasComponentDidMountMethod] = null;

                this.setState(partialState, ()=>StartAction(/*remoteMethodName*/'componentDidMount', /*component*/this, /*eventArguments*/[]));
            }
        }

        componentWillUnmount()
        {            
            const length = this[ON_COMPONENT_DESTROY].length;
            for (var i = 0; i < length; i++)
            {
                this[ON_COMPONENT_DESTROY][i]();
            }            

            COMPONENT_CACHE.Unregister(this);

            TraceComponent(this, "componentWillUnmount");
        }

        componentDidUpdate(previousProps, previousState)
        {
            TraceComponent(this, "componentDidUpdate");

            const clientTasks = this.state[ClientTasks];
            if (clientTasks)
            {
                const partialState = {};

                partialState[ClientTasks] = null;

                this.setState(partialState, ()=> ProcessClientTasks(clientTasks, this));
            }
            
        }

        static getDerivedStateFromProps(nextProps, prevState) 
        {
            TraceComponent(prevState[DotNetTypeOfReactComponent], "getDerivedStateFromProps", nextProps, prevState);

            const syncIdInState = ShouldBeNumber(prevState[SyncId]);
            const syncIdInProp  = ShouldBeNumber(nextProps[SyncId]);

            if (syncIdInState > syncIdInProp)
            {
                 return null;
            }

            if (syncIdInState !== syncIdInProp)
            {
                const partialState = {};

                partialState[SyncId] = syncIdInProp;
                partialState[RootNode] = NotNull(nextProps.$jsonNode[RootNode]);
                partialState[ClientTasks] = nextProps.$jsonNode[ClientTasks];
                partialState[ComponentRefKey] = NotNull(nextProps.$jsonNode.key);
                partialState[DotNetProperties] = NotNull(nextProps.$jsonNode[DotNetProperties]);

                if (prevState && prevState[ComponentRefKey] && prevState[ComponentRefKey] !== nextProps.$jsonNode.key)
                {
                    throw CreateNewDeveloperError("Component Key Changed");
                }

                return partialState;
            }

            return null;
        }
    }
    
    NewComponent[DotNetTypeOfReactComponent] = dotNetTypeOfReactComponent;

    ComponentDefinitions[dotNetTypeOfReactComponent] = NewComponent;

    NewComponent.displayName = dotNetTypeOfReactComponent.split(',')[0].split('.').pop();

    return NewComponent;
}

var IsWaitingRemoteResponse = false;

function SendRequest(request, onSuccess)
{
    IsWaitingRemoteResponse = true;

    BeforeSendRequest(request);

    ReactWithDotNet.SendRequest(request, onSuccess);

    function BeforeSendRequest(request)
    {
        request.ClientWidth  = document.documentElement.clientWidth;
        request.ClientHeight = document.documentElement.clientHeight;
        request.SearchPartOfUrl = window.location.search;
    }
}

var NextAvailableKey = 1;

function RenderComponentIn(obj)
{
    var fullTypeNameOfReactComponent = obj.fullTypeNameOfReactComponent;
    var containerHtmlElementId       = obj.containerHtmlElementId;

    OnDocumentReady(function()
    {
        const request =
        {
            MethodName: "FetchComponent",
            FullName: fullTypeNameOfReactComponent,
            ComponentRefId: "1"
        };

        function onSuccess(response)
        {
            IsWaitingRemoteResponse = false;

            if (response.NavigateToUrl)
            {
                window.location.replace(location.origin + response.NavigateToUrl);
                return;
            }

            if (response.ErrorMessage != null)
            {
                throw response.ErrorMessage;
            }

            ProcessDynamicCssClasses(response.DynamicStyles);

            const element = response.ElementAsJson;

            const component = DefineComponent(element);

            NextAvailableKey = response.NextAvailableKey;

            function renderCallback(component)
            {
                if (component)
                {
                    OnReactStateReady();
                }
            }

            const props = { key: '0', $jsonNode: element, ref: renderCallback };

            props[SyncId] = GetNextSequence();

            const reactElement = React.createElement(component, props);
            
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

function CopyToClipboard(text) 
{
    if (navigator.clipboard && navigator.clipboard.writeText)
    {
        navigator.clipboard.writeText(text);
        return;
    }

    if (window.clipboardData && window.clipboardData.setData) 
    {
        // IE specific code path to prevent textarea being shown while dialog is visible.
        return clipboardData.setData("Text", text); 

    }

    if (document.queryCommandSupported && document.queryCommandSupported("copy"))
    {
        var textarea = document.createElement("textarea");
        textarea.textContent = text;
        textarea.style.position = "fixed";  // Prevent scrolling to bottom of page in MS Edge.
        document.body.appendChild(textarea);
        textarea.select();
        try 
        {
            return document.execCommand("copy");  // Security exception may be thrown by some browsers.
        }
        catch (ex)
        {
            console.warn("Copy to clipboard failed.", ex);
            return false;
        }
        finally
        {
            document.body.removeChild(textarea);
        }
    }
}

function ListenWindowResizeEvent(resizeTimeout)
{
    var timeout = null;
    window.addEventListener('resize', function () 
    {
        clearTimeout(timeout);

        timeout = setTimeout(function ()
        {
            ReactWithDotNet.DispatchEvent('WindowResize', []);
        }, resizeTimeout);
    });
}

const ExternalJsObjectMap = {
    'RegExp': (x) => new RegExp(x),
    'CopyToClipboard': CopyToClipboard,
    'ReplaceNullWhenEmpty': function (value)
    {
        if (IsEmptyObject(value))
        {
            return null;
        }

        return value;
    },
    'ListenWindowResizeEvent': ListenWindowResizeEvent,
    'ReactWithDotNet::Core::ConvertDotnetSerializedStringDateToJsDate': function (dotnetDateAsJsonString)
    {
        if (dotnetDateAsJsonString == null || dotnetDateAsJsonString === '')
        {
            return null;
        }
        return new Date(dotnetDateAsJsonString);
    }
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

ExternalJsObjectMap["ReactWithDotNet.GetExternalJsObject"] = GetExternalJsObject;

function CreateNewDeveloperError(message)
{
    return new Error('ReactWithDotNet developer error occured.' + message);
}

const DynamicCssClassList = [];
var ReactWithDotNetDynamicCssElement = null;

function ProcessDynamicCssClasses(arrayOfDynamicCssClasses)
{
    if (arrayOfDynamicCssClasses == null || arrayOfDynamicCssClasses.length === 0)
    {
        return;
    }

    let hasChange = false;

    for (var i = 0; i < arrayOfDynamicCssClasses.length; i++)
    {
        var cssClass = arrayOfDynamicCssClasses[i];

        if (DynamicCssClassList.indexOf(cssClass) >= 0)
        {
            continue;
        }

        hasChange = true;

        DynamicCssClassList.push(cssClass);
    }

    if (hasChange)
    {
        if (ReactWithDotNetDynamicCssElement === null)
        {
            ReactWithDotNetDynamicCssElement = document.createElement('style');
            ReactWithDotNetDynamicCssElement.id = "ReactWithDotNetDynamicCss";
            document.head.appendChild(ReactWithDotNetDynamicCssElement);
        }

        ReactWithDotNetDynamicCssElement.innerHTML = DynamicCssClassList.join("\n");
    }
}

var ReactWithDotNet =
{
    RequestHandlerUrl: '/HandleReactWithDotNetRequest',
    OnDocumentReady: OnDocumentReady,
    StartAction: StartAction,
    DispatchEvent: EventBus.Dispatch,
    RenderComponentIn: RenderComponentIn,
    SendRequest: function (request, callback)
    {
        Fetch(ReactWithDotNet.RequestHandlerUrl, {
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