/**
 *  == ReactWithDotNet ==
 *  Written by Abdullah Beyaztaş
 *  Manages react ui by using react render information which incoming from server.
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
const DotNetComponentUniqueIdentifier = '$DotNetComponentUniqueIdentifier';
const DotNetComponentUniqueIdentifiers = '$DotNetComponentUniqueIdentifiers';

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

const Before3rdPartyComponentAccessListeners = [];

function Before3rdPartyComponentAccess(dotNetFullClassNameOf3rdPartyComponent)
{
    for (var i = 0; i < Before3rdPartyComponentAccessListeners.length; i++)
    {
        Before3rdPartyComponentAccessListeners[i](dotNetFullClassNameOf3rdPartyComponent);
    }
}

function AttachToBefore3rdPartyComponentAccess(fn)
{
    Before3rdPartyComponentAccessListeners.push(fn);
}

function TryLoadCssByHref(href)
{
    const headElement = document.querySelector(`head`);

    if (headElement == null)
    {
        return {error: 'Head element not found in document.'};
    }

    const linkElement = headElement.querySelector("link[href*=" + '"' + href + '"' + "]");
    if (linkElement)
    {
        return { isAlreadyLoaded: true };
    }

    const newLinkElement = document.createElement(`link`);

    newLinkElement.rel = `stylesheet`;
    newLinkElement.href = href;
    newLinkElement.type = 'text/css';

    headElement.appendChild(newLinkElement);

    return { loadStarted: true };
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
        const item = FunctionExecutionQueue.shift();

        if (item.isValid === false)
        {
            EmitNextFunctionInFunctionExecutionQueue();
            return;
        }

        FunctionExecutionQueueStateIsExecuting = true;
        FunctionExecutionQueueCurrentEntry = item;
        
        item.fn(item);
    }
}

var FunctionExecutionQueueEntryUniqueIdentifier = 1;
var FunctionExecutionQueueCurrentEntry = null;

function PushToFunctionExecutionQueue(fn)
{
    const entry = { fn: fn, isValid: true, id: FunctionExecutionQueueEntryUniqueIdentifier++ };

    FunctionExecutionQueue.push(entry);

    if (!FunctionExecutionQueueStateIsExecuting)
    {
        EmitNextFunctionInFunctionExecutionQueue();
    }

    return entry;
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

        let step = steps[i];

        if (step === '[')
        {
            step = steps[i + 1];
            i++; // skip [
            i++; // skip name            
        }

        obj = obj[step];
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
            if (step === '[')
            {
                step = steps[i + 1];
                i++; // skip [
                i++; // skip name            
            }

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
            FullTypeNameOfState: NotNull(fiberNode.memoizedProps.$jsonNode[FullTypeNameOfState]),
            FullTypeNameOfComponent: fiberNode.stateNode.state[DotNetTypeOfReactComponent],
            ComponentUniqueIdentifier: fiberNode.stateNode.state[DotNetComponentUniqueIdentifier]
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

        // skip reference equal components
        {
            const isReferenceEquals = (x) => x === component;

            const existingComponent = this.linkedList.first(isReferenceEquals);
            if (existingComponent)
            {
                return;
            }
        }

        // remove twice rendered components
        // occurs when lazy components scenarios
        {
            const isTwiceRendered = (x) => x[DotNetComponentUniqueIdentifiers][0] === component[DotNetComponentUniqueIdentifiers][0];

            const existingComponent = this.linkedList.first(isTwiceRendered);
            if (existingComponent)
            {
                this.linkedList.removeFirst(isTwiceRendered);
            }    
        }
           

        this.linkedList.add(component);
    }

    FindComponentByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier)
    {
        const isMatch = (component) =>
        {
            if (component && component[DotNetComponentUniqueIdentifiers])
            {
                return component[DotNetComponentUniqueIdentifiers].indexOf(dotNetComponentUniqueIdentifier) >= 0;
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

function GetComponentByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier)
{
    const component = COMPONENT_CACHE.FindComponentByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier);
    if (component == null)
    {
        throw CreateNewDeveloperError("Component not found. dotNetComponentUniqueIdentifier: " + dotNetComponentUniqueIdentifier);
    }

    return component;
}

/**
 * @param {number} componentUniqueIdentifier
 */
function GetFirstAssignedUniqueIdentifierValueOfComponent(componentUniqueIdentifier)
{
    return GetComponentByDotNetComponentUniqueIdentifier(componentUniqueIdentifier)[DotNetComponentUniqueIdentifiers][0];
}

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
                if (isEquivent(eventArguments[0], cachedMethodInfo.Parameter))
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
    const handlerComponentUniqueIdentifier = remoteMethodInfo.HandlerComponentUniqueIdentifier;
    const functionNameOfGrabEventArguments = remoteMethodInfo.FunctionNameOfGrabEventArguments;
    const stopPropagation = remoteMethodInfo.StopPropagation;
    

    NotNull(remoteMethodName);
    NotNull(handlerComponentUniqueIdentifier);

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

        const targetComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);

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

    if (jsonNode.$isPureComponent === 1)
    {
        const cmp = DefinePureComponent(jsonNode);

        const cmpKey = NotNull(jsonNode.key);

        const cmpProps =
        {
            key: cmpKey,
            $jsonNode: jsonNode
        };

        return createElement(cmp, cmpProps);
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

        const reactAttributeNames = jsonNode.$ReactAttributeNames;
        if (reactAttributeNames)
        {
            for (let i = 0; i < reactAttributeNames.length; i++)
            {
                const name = reactAttributeNames[i];
                cmpProps[name] = jsonNode[DotNetProperties][name];
            }
        }

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
        Before3rdPartyComponentAccess(constructorFunction);

        constructorFunction = GetExternalJsObject(constructorFunction);
    }

    if (constructorFunction === 'nbsp')
    {
        if (jsonNode && jsonNode.length)
        {
            return Array(jsonNode.length).fill('\xA0').join('');
        }

        return '\xA0';
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
                const targetProp    = propValue.targetProp;
                const eventName     = propValue.eventName;
                const sourcePath    = propValue.sourcePath;
                const sourceIsState = propValue.sourceIsState;

                const debounceTimeout = propValue.DebounceTimeout;
                const debounceHandler = propValue.DebounceHandler;
                
                const jsValueAccess = propValue.jsValueAccess;
                const transformFunction = GetExternalJsObject(IfNull(propValue.transformFunction , 'ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull'));

                const handlerComponentUniqueIdentifier = propValue.HandlerComponentUniqueIdentifier;

                let accessToSource = DotNetProperties;
                if (sourceIsState)
                {
                    accessToSource = DotNetState;
                }

                props[targetProp] = transformFunction(GetValueInPath(GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier).state[accessToSource], sourcePath));
                props[eventName] = function(e)
                {
                    const targetComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);

                    const modifiedDotNetState = Clone(targetComponent.state[accessToSource]);

                    SetValueInPath(modifiedDotNetState, sourcePath, transformFunction(GetValueInPath({ e: e }, jsValueAccess)));

                    const newState = {};
                    newState[accessToSource] = modifiedDotNetState;

                    if (debounceTimeout > 0)
                    {
                        const executionQueueItemName = eventName + '-debounce-' + GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);

                        if (FunctionExecutionQueueCurrentEntry &&
                            FunctionExecutionQueueCurrentEntry.name === executionQueueItemName)
                        {
                            FunctionExecutionQueueCurrentEntry.isValid = false;
                        }

                        clearTimeout(targetComponent.state[eventName + '-debounceTimeoutId']);

                        newState[eventName + '-debounceTimeoutId'] = setTimeout(() =>
                        {
                            const executionEntry = StartAction(debounceHandler, targetComponent, /*eventArguments*/[]);
                            executionEntry.name = executionQueueItemName;

                        }, debounceTimeout);                        
                    }

                    targetComponent.setState(newState);

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
            const itemTemplates = propValue.___ItemTemplates___;
            const itemTemplateForNull = propValue.___TemplateForNull___;
            if (itemTemplates)
            {
                props[propName] = function(item)
                {
                    if (item == null && itemTemplateForNull)
                    {
                        return ConvertToReactElement(CreateNewBuildContext(), itemTemplateForNull);
                    }

                    const length = itemTemplates.length;

                    // try to match by key
                    {
                        for (let j = 0; j < length; j++)
                        {
                            const itemInTemplateInfo     = itemTemplates[j].Item;
                            const jsonNodeInTemplateInfo = itemTemplates[j].ElementAsJson;

                            // try find as TreeNode
                            if (item && item.key != null && itemInTemplateInfo.key != null && itemInTemplateInfo.key === item.key)
                            {
                                return ConvertToReactElement(CreateNewBuildContext(), jsonNodeInTemplateInfo);
                            }
                        }
                    }
                    

                    // try to match whole data
                    {
                        for (let j = 0; j < length; j++)
                        {
                            const itemInTemplateInfo     = itemTemplates[j].Item;
                            const jsonNodeInTemplateInfo = itemTemplates[j].ElementAsJson;

                            if (JSON.stringify(itemInTemplateInfo) === JSON.stringify(item))
                            {
                                return ConvertToReactElement(CreateNewBuildContext(), jsonNodeInTemplateInfo);
                            }
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

        if (childrenLength === 1)
        {
            return createElement(constructorFunction, props, ConvertToReactElement(buildContext, children[0], component));
        }

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


function ConvertToSyntheticChangeEvent(e)
{
    const target = ConvertToShadowHtmlElement(e.target);

    return {        
        bubbles:   e.bubbles,
        target:    target,
        timeStamp: e.timeStamp,        
        type:      e.type
    };
}



function ConvertToShadowHtmlElement(htmlElement)
{
    let value = null;

    if (htmlElement.value != null)
    {
        if (typeof htmlElement.value === 'string')
        {
            value = htmlElement.value;
        }
    }

    let selectedIndex = null;
    if (htmlElement.selectedIndex != null)
    {
        if (typeof htmlElement.selectedIndex === 'number')
        {
            selectedIndex = htmlElement.selectedIndex;
        }
    }

    let selectionStart = null;
    if (htmlElement.selectionStart != null)
    {
        if (typeof htmlElement.selectionStart === 'number')
        {
            selectionStart = htmlElement.selectionStart;
        }
    }

    return {
        id: htmlElement.id,
        offsetHeight: htmlElement.offsetHeight,
        offsetLeft: htmlElement.offsetLeft,
        offsetTop: htmlElement.offsetTop,
        offsetWidth: htmlElement.offsetWidth,
        selectedIndex: selectedIndex,
        selectionStart: selectionStart,
        tagName: htmlElement.tagName,        
        value: value
    };
}

function ProcessClientTasks(clientTasks, component)
{
    if (clientTasks == null)
    {
        return;
    }

    for (let i = 0; i < clientTasks.length; i++)
    {
        const jsFunctionPath      = clientTasks[i].JsFunctionPath;
        const jsFunctionArguments = clientTasks[i].JsFunctionArguments;

        PushToFunctionExecutionQueue(() =>
        {
            InvokeJsFunctionInPath(component, jsFunctionPath, jsFunctionArguments);
            OnReactStateReady();
        });
    }
}

function StartAction(remoteMethodName, component, eventArguments)
{
    function execute(executionQueueEntry)
    {
        HandleAction({ remoteMethodName: remoteMethodName, component: component, eventArguments: eventArguments }, executionQueueEntry);
    }
    return PushToFunctionExecutionQueue(execute);
}

function HandleAction(data, executionQueueEntry)
{
    const remoteMethodName = data.remoteMethodName;
    const component = NotNull(data.component);

    const request =
    {
        MethodName: "HandleComponentEvent",

        EventHandlerMethodName: NotNull(remoteMethodName),
        FullName   : NotNull(component.constructor)[DotNetTypeOfReactComponent],
        CapturedStateTree: CaptureStateTreeFromFiberNode(component._reactInternals),
        ComponentKey: NotNull(component.props.$jsonNode.key),
        LastUsedComponentUniqueIdentifier: LastUsedComponentUniqueIdentifier,
        ComponentUniqueIdentifier: NotNull(component.state[DotNetComponentUniqueIdentifier]),

        CallFunctionId: executionQueueEntry.id
    };
    
    request.eventArgumentsAsJsonArray = data.eventArguments.map(JSON.stringify);

    function onSuccess(response)
    {
        

        IsWaitingRemoteResponse = false;

        if (response.CallFunctionId > 0 &&
            FunctionExecutionQueueCurrentEntry && 
            FunctionExecutionQueueCurrentEntry.id === response.CallFunctionId &&
            FunctionExecutionQueueCurrentEntry.isValid === false)
        {
            // this function is not valid.
            // maybe expired by debounce mechanism.

            OnReactStateReady();
            return;
        }

        if (response.ErrorMessage != null)
        {
            throw CreateNewDeveloperError(response.ErrorMessage);
        }

        LastUsedComponentUniqueIdentifier = response.LastUsedComponentUniqueIdentifier;        

        ProcessDynamicCssClasses(response.DynamicStyles);

        function stateCallback()
        {
            OnReactStateReady();
        }

        data.component.setState(CaclculateNewStateFromJsonElement(component.state, response.ElementAsJson), stateCallback);
    }

    SendRequest(request, onSuccess);
}

function CaclculateNewStateFromJsonElement(componentState, jsonElement)
{
    const newState = {};

    newState[DotNetState]     = NotNull(jsonElement[DotNetState]);
    newState[SyncId]          = ShouldBeNumber(componentState[SyncId]) + 1;
    newState[RootNode]        = jsonElement[RootNode];
    newState[ClientTasks]     = jsonElement[ClientTasks];
    newState[DotNetProperties] = jsonElement[DotNetProperties];
    newState[DotNetComponentUniqueIdentifier] = jsonElement[DotNetComponentUniqueIdentifier];


    if (NotNull(componentState[DotNetComponentUniqueIdentifier]) !== NotNull(jsonElement[DotNetComponentUniqueIdentifier]))
    {
        const component = COMPONENT_CACHE.FindComponentByDotNetComponentUniqueIdentifier(componentState[DotNetComponentUniqueIdentifier]);
        if (component)
        {
            component[DotNetComponentUniqueIdentifiers].push(jsonElement[DotNetComponentUniqueIdentifier]);
        }
                    
    } 

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
            initialState[RootNode] = props.$jsonNode[RootNode];
            initialState[DotNetProperties] = NotNull(props.$jsonNode[DotNetProperties]);
            initialState[DotNetComponentUniqueIdentifier] = NotNull(props.$jsonNode[DotNetComponentUniqueIdentifier]);

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

            this[DotNetComponentUniqueIdentifiers] = [NotNull(props.$jsonNode[DotNetComponentUniqueIdentifier])];

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

            const me = this;

            const clientTasks = this.state[ClientTasks];            
            if (clientTasks)
            {
                const partialState = {};

                partialState[ClientTasks] = null;

                this.setState(partialState, ()=> ProcessClientTasks(clientTasks, me));
            }

            const hasComponentDidMountMethod = this.state[HasComponentDidMountMethod];
            if (hasComponentDidMountMethod)
            {
                // try call from cache
                {
                    const cachedMethodInfo = tryToFindCachedMethodInfo(this, 'componentDidMount', []);
                    if (cachedMethodInfo)
                    {
                        const newState = CaclculateNewStateFromJsonElement(this.state, cachedMethodInfo.ElementAsJson);

                        newState[HasComponentDidMountMethod] = null;

                        const incomingClientTasks = newState[ClientTasks];

                        function stateCallback()
                        {
                            if (incomingClientTasks)
                            {
                                ProcessClientTasks(incomingClientTasks, me);
                            }
                        }

                        this.setState(newState, stateCallback);

                        return;
                    }
                }

                const partialState = {};

                partialState[HasComponentDidMountMethod] = null;

                this.setState(partialState, ()=>StartAction(/*remoteMethodName*/'componentDidMount', /*component*/me, /*eventArguments*/[]));
            }
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

        componentWillUnmount()
        {
            this.ComponentWillUnmountIsCalled = true;

            const length = this[ON_COMPONENT_DESTROY].length;
            for (var i = 0; i < length; i++)
            {
                this[ON_COMPONENT_DESTROY][i]();
            }

            // remove related dynamic styles
            for (let i = 0; i < DynamicStyles.length; i++)
            {
                if (this.$DotNetComponentUniqueIdentifiers.indexOf(DynamicStyles[i].componentUniqueIdentifier) >= 0)
                {
                    DynamicStyles.splice(i, 1);
                    i--;
                }
            }

            COMPONENT_CACHE.Unregister(this);

            TraceComponent(this, "componentWillUnmount");
        }

        static getDerivedStateFromProps(nextProps, prevState) 
        {
            TraceComponent(prevState[DotNetTypeOfReactComponent], "getDerivedStateFromProps", nextProps, prevState);

            const syncIdInState = ShouldBeNumber(prevState[SyncId]);
            const syncIdInProp  = ShouldBeNumber(nextProps[SyncId]);

            if (syncIdInState > syncIdInProp)
            {
                const componentActiveUniqueIdentifier = NotNull(prevState[DotNetComponentUniqueIdentifier]);
                const componentNextUniqueIdentifier   = NotNull(nextProps.$jsonNode[DotNetComponentUniqueIdentifier]);

                if (componentActiveUniqueIdentifier !== componentNextUniqueIdentifier)
                {
                    const component = GetComponentByDotNetComponentUniqueIdentifier(componentActiveUniqueIdentifier);

                    component[DotNetComponentUniqueIdentifiers].push(componentNextUniqueIdentifier);

                    const partialState = {};

                    partialState[DotNetComponentUniqueIdentifier] = componentNextUniqueIdentifier;

                    return partialState;                    
                }

                return null;
            }

            if (syncIdInState < syncIdInProp)
            {
                const partialState = {};

                partialState[SyncId] = syncIdInProp;
                partialState[RootNode] = nextProps.$jsonNode[RootNode];
                partialState[ClientTasks] = nextProps.$jsonNode[ClientTasks];
                partialState[DotNetProperties] = NotNull(nextProps.$jsonNode[DotNetProperties]);

                const componentActiveUniqueIdentifier = NotNull(prevState[DotNetComponentUniqueIdentifier]);
                const componentNextUniqueIdentifier   = NotNull(nextProps.$jsonNode[DotNetComponentUniqueIdentifier]);

                if (componentActiveUniqueIdentifier !== componentNextUniqueIdentifier)
                {
                    const component = GetComponentByDotNetComponentUniqueIdentifier(componentActiveUniqueIdentifier);
                    component[DotNetComponentUniqueIdentifiers].push(componentNextUniqueIdentifier);
                }

                partialState[DotNetComponentUniqueIdentifier] = componentNextUniqueIdentifier;

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

function DefinePureComponent(componentDeclaration)
{
    const dotNetTypeOfReactComponent = componentDeclaration[DotNetTypeOfReactComponent];

    const component = ComponentDefinitions[dotNetTypeOfReactComponent];
    if (component)
    {
        return component;
    }
    
    class NewPureComponent extends React.PureComponent
    {        
        render()
        {
            return ConvertToReactElement(CreateNewBuildContext(), this.props.$jsonNode[RootNode], this, /*isConvertingRootNode*/true);
        }
    }

    ComponentDefinitions[dotNetTypeOfReactComponent] = NewPureComponent;

    NewPureComponent.displayName = dotNetTypeOfReactComponent.split(',')[0].split('.').pop();

    return NewPureComponent;
}

var IsWaitingRemoteResponse = false;

function SendRequest(request, onSuccess)
{
    IsWaitingRemoteResponse = true;

    request.ClientWidth  = document.documentElement.clientWidth;
    request.ClientHeight = document.documentElement.clientHeight;
    request.QueryString = window.location.search;

    const url = ReactWithDotNet.RequestHandlerUrl;

    let options =
    {
        method: "POST",
        headers:
        {
            'Accept': "application/json",
            'Content-Type': "application/json"
        },
        body: JSON.stringify(request)
    };

    if (ReactWithDotNet.BeforeSendRequest)
    {
        options = ReactWithDotNet.BeforeSendRequest(options);
    }

    window.fetch(url, options).then(response => response.json()).then(json => onSuccess(json));
}

var LastUsedComponentUniqueIdentifier = 1;

function ConnectComponentFirstResponseToReactSystem(containerHtmlElementId, response)
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

    ProcessDynamicCssClasses(response.DynamicStyles);

    const element = response.ElementAsJson;

    const component = element.$isPureComponent === 1 ? DefinePureComponent(element) : DefineComponent(element);

    LastUsedComponentUniqueIdentifier = response.LastUsedComponentUniqueIdentifier;
            

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

function RenderComponentIn(input)
{
    if (input.renderInfo)
    {
        if (input.idOfContainerHtmlElement == null)
        {
            throw CreateNewDeveloperError('idOfContainerHtmlElement cannot be null.');
        }

        setTimeout(() => ConnectComponentFirstResponseToReactSystem(input.idOfContainerHtmlElement , input.renderInfo), 10);

        return;
    }

    var fullTypeNameOfReactComponent = input.fullTypeNameOfReactComponent;
    var containerHtmlElementId       = input.containerHtmlElementId;

    OnDocumentReady(function()
    {
        const request =
        {
            MethodName: "FetchComponent",
            FullName: fullTypeNameOfReactComponent,
            LastUsedComponentUniqueIdentifier: LastUsedComponentUniqueIdentifier,
            ComponentUniqueIdentifier: 1
        };

        function onSuccess(response)
        {
            IsWaitingRemoteResponse = false;

            ConnectComponentFirstResponseToReactSystem(containerHtmlElementId, response);
        }
        
        SendRequest(request, onSuccess);
    });
}

function CallJsFunctionInPath(clientTask)
{
    GetExternalJsObject(clientTask.JsFunctionPath).apply(null, clientTask.JsFunctionArguments);
}

function InvokeJsFunctionInPath(callerReactComponent, jsFunctionPath, jsFunctionArguments)
{
    GetExternalJsObject(jsFunctionPath).apply(callerReactComponent, jsFunctionArguments);
}

const ExternalJsObjectMap = {
    'React.Fragment': React.Fragment
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

function RegisterCoreFunction(name, fn)
{
    RegisterExternalJsObject("ReactWithDotNet::Core::" + name, fn);
}

ExternalJsObjectMap["ReactWithDotNet.GetExternalJsObject"] = GetExternalJsObject;

RegisterCoreFunction('RegExp', (x) => new RegExp(x));

RegisterCoreFunction('CopyToClipboard', function (text) 
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
});

RegisterCoreFunction('ReplaceNullWhenEmpty', function(value)
{
    if (IsEmptyObject(value))
    {
        return null;
    }

    return value;
});

RegisterCoreFunction('EmptyTransformFunction', function(value)
{
   return value;
});

RegisterCoreFunction('ReplaceEmptyStringWhenIsNull', function(value)
{
    if (value == null)
    {
        return '';
    }

    return value;
});

RegisterCoreFunction('ListenWindowResizeEvent', function (resizeTimeout)
{
    var timeout = null;
    window.addEventListener('resize', function () 
    {
        clearTimeout(timeout);

        timeout = setTimeout(function ()
        {
            ReactWithDotNet.DispatchEvent('ReactWithDotNet::Core::OnWindowResize', []);
        }, resizeTimeout);
    });
});

RegisterCoreFunction('ConvertDotnetSerializedStringDateToJsDate', function(dotnetDateAsJsonString)
{
    if (dotnetDateAsJsonString == null || dotnetDateAsJsonString === '')
    {
        return null;
    }
    return new Date(dotnetDateAsJsonString);
});

RegisterCoreFunction("CalculateSyntheticMouseEventArguments", (argumentsAsArray) => [ConvertToSyntheticMouseEvent(argumentsAsArray[0])]);

RegisterCoreFunction("CalculateSyntheticChangeEventArguments", (argumentsAsArray) => [ConvertToSyntheticChangeEvent(argumentsAsArray[0])]);

RegisterCoreFunction("SetCookie", function (cookieName, cookieValue, expiredays)
{
    var exdate = new Date();

    exdate.setDate(exdate.getDate() + expiredays);

    document.cookie = cookieName + "=" + escape(cookieValue) + ((expiredays == null) ? "" : "; expires=" + exdate.toUTCString());
});

RegisterCoreFunction("PushHistory", function (title, url)
{
    window.history.replaceState({}, title, url);    
});

RegisterCoreFunction("GotoMethod", function (timeout, remoteMethodName, remoteMethodArguments)
{
    const component = this;

    remoteMethodArguments = remoteMethodArguments || [];

    setTimeout(() =>
    {
        const cachedMethodInfo = tryToFindCachedMethodInfo(component, remoteMethodName, remoteMethodArguments);
        if (cachedMethodInfo)
        {
            const newState = CaclculateNewStateFromJsonElement(component.state, cachedMethodInfo.ElementAsJson);

            component.setState(newState);

            return;
        }

        StartAction(remoteMethodName, component, remoteMethodArguments);

    }, timeout);  
});

RegisterCoreFunction("DispatchEvent", function(eventName, eventArguments)
{
    EventBus.Dispatch(eventName, eventArguments); 
});

/**
 * @param {string} senderPropertyFullName
 * @param {number} senderComponentUniqueIdentifier
 */
function GetRealNameOfDotNetEvent(senderPropertyFullName, senderComponentUniqueIdentifier)
{
    return [
        'senderPropertyFullName:' + senderPropertyFullName,
        'senderComponentUniqueIdentifier:' + senderComponentUniqueIdentifier
    ].join(',');
}

RegisterCoreFunction("DispatchDotNetCustomEvent", function(eventSenderInfo, eventArguments)
{
    const senderPropertyFullName = eventSenderInfo.SenderPropertyFullName;
    const senderComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(eventSenderInfo.SenderComponentUniqueIdentifier);

    const eventName = GetRealNameOfDotNetEvent(senderPropertyFullName, senderComponentUniqueIdentifier);

    EventBus.Dispatch(eventName, eventArguments); 
});

RegisterCoreFunction("ListenEvent", function (eventName, remoteMethodName)
{
    const component = this;

    const onEventFired = (e) =>
    {
        const eventArgumentsAsArray = e.detail;

        StartAction(remoteMethodName, component, eventArgumentsAsArray);
    };

    NotNull(component[ON_COMPONENT_DESTROY]);

    component[ON_COMPONENT_DESTROY].push(() =>
    {
        EventBus.Remove(eventName, onEventFired);
    });

    EventBus.On(eventName, onEventFired); 
});

RegisterCoreFunction("ListenEventOnlyOnce", function (eventName, remoteMethodName)
{
    const component = this;

    const onEventFired = (e) =>
    {
        EventBus.Remove(eventName, onEventFired);

        const eventArgumentsAsArray = e.detail;

        StartAction(remoteMethodName, component, eventArgumentsAsArray);
    };

    NotNull(component[ON_COMPONENT_DESTROY]);

    component[ON_COMPONENT_DESTROY].push(() =>
    {
        EventBus.Remove(eventName, onEventFired);
    });

    EventBus.On(eventName, onEventFired);
});

RegisterCoreFunction("InitializeDotnetComponentEventListener", function (eventSenderInfo, remoteMethodName, handlerComponentUniqueIdentifier)
{
    const component = this;

    if (component.ComponentWillUnmountIsCalled)
    {
        return;
    }

    const senderPropertyFullName = eventSenderInfo.SenderPropertyFullName;
    const senderComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(eventSenderInfo.SenderComponentUniqueIdentifier);

    handlerComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);

    // avoid multiple attach we need to ensure attach a listener at once
    {
        const customEventListenerMapKey = [
            'senderPropertyFullName:' + senderPropertyFullName,
            'senderComponentUniqueIdentifier:' + senderComponentUniqueIdentifier,
            'handlerComponentUniqueIdentifier:' + handlerComponentUniqueIdentifier
        ].join(',');

        if (component[CUSTOM_EVENT_LISTENER_MAP][customEventListenerMapKey])
        {
            return;
        }

        component[CUSTOM_EVENT_LISTENER_MAP][customEventListenerMapKey] = 1;
    }

    const eventName = GetRealNameOfDotNetEvent(senderPropertyFullName, senderComponentUniqueIdentifier);

    const onEventFired = (e) =>
    {
        const eventArgumentsAsArray = e.detail;

        const handlerComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);

        StartAction(remoteMethodName, handlerComponent, eventArgumentsAsArray);
    };

    component[ON_COMPONENT_DESTROY].push(() =>
    {
        EventBus.Remove(eventName, onEventFired);
    });

    EventBus.On(eventName, onEventFired);
});

RegisterCoreFunction("NavigateToUrl", function (url)
{
     window.location.replace(location.origin + url);
});  

RegisterCoreFunction("OnOutsideClicked", function (idOfElement, remoteMethodName, handlerComponentUniqueIdentifier)
{
    handlerComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);

    const component = this;

    // avoid multiple attach we need to ensure attach a listener at once
    {
        const customEventListenerMapKey = 'OnOutsideClicked(IdOfElement:' + idOfElement + ', remoteMethodName:' + remoteMethodName + ', @handlerComponentUniqueIdentifier:' + handlerComponentUniqueIdentifier + ')';

        if (component[CUSTOM_EVENT_LISTENER_MAP][customEventListenerMapKey])
        {
            return;
        }

        component[CUSTOM_EVENT_LISTENER_MAP][customEventListenerMapKey] = 1;
    }

    function onDocumentClick(e)
    {
        const element = document.getElementById(idOfElement);
        if (element == null)
        {
            throw CreateNewDeveloperError("Element not found for calculating OnOutsideClicked operation. id: " + idOfElement);
        }
        const isClickedOutside = !element.contains(e.target)
        if (isClickedOutside)
        {
            const handlerComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);

            StartAction(remoteMethodName, handlerComponent, /*eventArguments*/[]);
        }
    }

    document.addEventListener('click', onDocumentClick);

    component[ON_COMPONENT_DESTROY].push(() =>
    {
        document.removeEventListener('click', onDocumentClick);
    });
});

           


function CreateNewDeveloperError(message)
{
    return new Error('ReactWithDotNet developer error occured.' + message);
}

const DynamicStyles = [];
var ReactWithDotNetDynamicCssElement = null;

function ProcessDynamicCssClasses(dynamicStyles)
{
    if (dynamicStyles == null || dynamicStyles.length === 0)
    {
        return;
    }

    let hasChange = false;

    for (var key in dynamicStyles)
    {
        if (dynamicStyles.hasOwnProperty(key)) 
        {
            const cssSelector = key;
            const cssBody = dynamicStyles[key];

            let shouldInsert = true;

            for (var i = 0; i < DynamicStyles.length; i++)
            {
                if (DynamicStyles[i].cssSelector === cssSelector)
                {
                    if (DynamicStyles[i].cssBody === cssBody)
                    {
                        shouldInsert = false;
                        break;
                    }

                    hasChange = true;
                    DynamicStyles[i].cssBody = cssBody;
                    break;
                }
            }

            if (shouldInsert)
            {
                hasChange = true;

                let startIndex = cssSelector.indexOf('._rwd_');
                let endIndex = cssSelector.indexOf('_', startIndex + 6);

                const componentUniqueIdentifier = parseInt(cssSelector.substring(startIndex + 6, endIndex));
                if (isNaN(componentUniqueIdentifier))
                {
                    throw CreateNewDeveloperError('componentUniqueIdentifier cannot calculated from ' + cssSelector);
                }

                DynamicStyles.push({cssSelector: cssSelector, cssBody: cssBody, componentUniqueIdentifier: componentUniqueIdentifier});
            }           
        }
    }

    if (hasChange)
    {
        if (ReactWithDotNetDynamicCssElement === null)
        {
            const idOfStyleElement = "ReactWithDotNetDynamicCss";

            ReactWithDotNetDynamicCssElement = document.getElementById(idOfStyleElement);

            if (ReactWithDotNetDynamicCssElement == null)
            {
                ReactWithDotNetDynamicCssElement = document.createElement('style');
                ReactWithDotNetDynamicCssElement.id = idOfStyleElement;
                document.head.appendChild(ReactWithDotNetDynamicCssElement);
            }         
        }

        const arr = [];
        for (var i = 0; i < DynamicStyles.length; i++)
        {
            const cssSelector = DynamicStyles[i].cssSelector;
            const cssBody = DynamicStyles[i].cssBody;

            arr.push("");
            arr.push(cssSelector);
            arr.push("{");
            arr.push(cssBody);
            arr.push("}");
            if (cssSelector.indexOf('@media ') === 0)
            {
                arr.push("}"); // for closing media rule bracket
            }
        }

        ReactWithDotNetDynamicCssElement.innerHTML = arr.join("\n");
    }
}

function IsMobile()
{
    return document.documentElement.clientWidth <= 767;
}

function IsTablet()
{
    return document.documentElement.clientWidth <= 1023 && IsMobile() === false;
}

function IsDesktop()
{
    return IsMobile() === false && IsTablet() === false;
}

var ReactWithDotNet =
{
    RequestHandlerUrl: '/HandleReactWithDotNetRequest',
    OnDocumentReady: OnDocumentReady,
    StartAction: StartAction,
    DispatchEvent: EventBus.Dispatch,
    RenderComponentIn: RenderComponentIn,
    BeforeSendRequest: x=>x,
    RegisterExternalJsObject: RegisterExternalJsObject,
    GetExternalJsObject: GetExternalJsObject,
    BeforeAny3rdPartyComponentAccess: AttachToBefore3rdPartyComponentAccess,
    TryLoadCssByHref: TryLoadCssByHref,

    IsMediaMobile: IsMobile,
    IsMediaTablet: IsTablet,
    IsMediaDesktop: IsDesktop
};

export default ReactWithDotNet;