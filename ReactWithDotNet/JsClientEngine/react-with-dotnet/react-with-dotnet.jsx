/**
 *  == ReactWithDotNet ==
 *  Written by Abdullah Beyaztaş
 *  Manages react ui by using react render information which incoming from server.
 */

import React from 'react';
import {createRoot} from 'react-dom/client';


const createElement = React.createElement;

const DotNetTypeOfReactComponent = '$Type';
const RootNode = '$RootNode';
const ClientTasks = '$ClientTasks';
const SyncId = '$SyncId';
const DotNetState = '$State';
const ComponentDidMountMethod = '$ComponentDidMountMethod';
const ComponentWillUnmountMethod = '$ComponentWillUnmountMethod';
const DotNetComponentUniqueIdentifier = '$DotNetComponentUniqueIdentifier';
const DotNetComponentUniqueIdentifiers = '$DotNetComponentUniqueIdentifiers';
const ON_COMPONENT_DESTROY = '$ON_COMPONENT_DESTROY';
const CUSTOM_EVENT_LISTENER_MAP = '$CUSTOM_EVENT_LISTENER_MAP';
const DotNetProperties = 'DotNetProperties';

/**
 * @typedef {Object} RemoteMethodInfo
 * @property {string} remoteMethodName
 * @property {number} HandlerComponentUniqueIdentifier
 * @property {string} FunctionNameOfGrabEventArguments
 * @property {number} StopPropagation
 * @property {string[]} KeyboardEventCallOnly
 * @property {?number} DebounceTimeout
 * @property {number} $isRemoteMethod
 * @property {Boolean?} Cacheable
 */

/**
 * @typedef {Object} JsonNode
 * @property {string} $tag
 * @property {Object} $props
 * @property {number} $isPureComponent
 * @property {string} $text
 * @property {JsonNode[]} $children
 * @property {JsonNode} $onClickPreview
 * @property {?number} $FakeChild
 * @property {string} FunctionNameOfGrabEventArguments
 * @property {number} StopPropagation
 * @property {string[]} KeyboardEventCallOnly
 * @property {?number} DebounceTimeout
 * @property {string[]} $ReactAttributeNames
 */

/**
 * @typedef {Object} ClientTask
 * @property {string} JsFunctionPath
 * @property {object[]} JsFunctionArguments
 */

/**
 * @typedef {Object} CacheableMethodInfo
 * @property {string} MethodName
 * @property {object[]} MethodArguments
 * @property {JsonNode} ElementAsJson
 */

/**
 * @typedef {Object} ComponentState
 */
/**
 * @typedef {Object} Component
 * @property {ComponentState} state
 */

/**
 * @typedef {Object} RemoteExecutionResponse
 * @property {number} SkipRender
 * @property {number} CallFunctionId
 * @property {string} ErrorMessage
 * @property {ClientTask[]} ClientTaskList
 * @property {JsonNode} ElementAsJson
 * @property {Object} NewState
 * @property {Object} NewDotNetProperties
 * @property {number} LastUsedComponentUniqueIdentifier
 * @property {Object} DynamicStyles
 */

/**
 * @typedef {Object} PropValue
 */

/**
 * @typedef {Object} BindInfo
 * @property {string} DebounceHandler
 * @property {number?} DebounceTimeout
 * @property {string} eventName
 * @property {number?} HandlerComponentUniqueIdentifier
 * @property {number?} $isBinding
 * @property {string[]} jsValueAccess
 * @property {number} sourceIsState
 * @property {string[]} sourcePath
 * @property {string} targetProp
 * @property {string} transformFunction
 */

/**
 * @typedef {Object} InnerElementInfo
 * @property {JsonNode} Element
 * @property {number?} $isElement
 */

/**
 * @typedef {Object} ItemTemplateInfo
 * @property {JsonNode} ElementAsJson
 * @property {Object} Item
 */

/**
 * @typedef {Object} ItemTemplate
 * @property {ItemTemplateInfo[]} ___ItemTemplates___
 * @property {JsonNode} ___TemplateForNull___
 */

/**
 * @typedef {Object} EventSenderInfo
 * @property {number} SenderComponentUniqueIdentifier
 * @property {string} SenderPropertyFullName
 */

function SafeExecute(fn)
{
    try
    {
        return { success: true, fail:false, value: fn() };
    }
    catch (exception)
    {
        return { success: false, fail: true, exception: exception };
    }
}

function TryRemoveItemFromArray(array, item)
{
    if (array == null || array.length === 0)
    {
        return;
    }

    const index = array.indexOf(item);

    if (index >= 0)
    {
        array.splice(index, 1);
        return true;
    }

    return false;
}

class EventBusImp
{
    constructor()
    {
        this.map = {};
    }

    subscribe(eventName, callback)
    {
        let listenerFunctions = this.map[eventName];

        if (!listenerFunctions)
        {
            this.map[eventName] = listenerFunctions = [];
        }

        listenerFunctions.push(callback);
    }

    unsubscribe(eventName, callback)
    {
        TryRemoveItemFromArray(this.map[eventName], callback);
    }

    publish(eventName, eventArgumentsAsArray)
    {
        if (eventArgumentsAsArray == null)
        {
            throw CreateNewDeveloperError("Publish event arguments should be given in array. @Example: ReactWithDotNet.DispatchEvent('MovieActorNameChanged', ['Tom Hanks']);");
        }

        const listenerFunctions = this.map[eventName];

        if (listenerFunctions == null || listenerFunctions.length === 0)
        {
            return;
        }

        // NOTE: maybe user removed some listeners so we need to protect array modification

        const functionArray = listenerFunctions.slice(0);

        for (let i = 0; i < functionArray.length; i++)
        {
            if (listenerFunctions.indexOf(functionArray[i]) >= 0)
            {
                functionArray[i].apply(null, [eventArgumentsAsArray]);
            }
        }
    }
}

const EventBus =
{
    bus: new EventBusImp(),

    On: function(event, callback)
    {
        EventBus.bus.subscribe(event, callback);
    },
    Dispatch: function (eventName, eventArgumentsAsArray)
    {
        EventBus.bus.publish(eventName, eventArgumentsAsArray);
    },
    Remove: function(event, callback)
    {
        EventBus.bus.unsubscribe(event, callback);
    }
};

const Before3rdPartyComponentAccessListeners = [];

function Before3rdPartyComponentAccess(dotNetFullClassNameOf3rdPartyComponent)
{
    for (let i = 0; i < Before3rdPartyComponentAccessListeners.length; i++)
    {
        Before3rdPartyComponentAccessListeners[i](dotNetFullClassNameOf3rdPartyComponent);
    }
}

function BeforeAnyThirdPartyComponentAccess(fn)
{
    Before3rdPartyComponentAccessListeners.push(fn);
}

const ThirdPartyComponentPropsCalculatedListeners = {};

function OnThirdPartyComponentPropsCalculated(dotNetFullNameOfThirdPartyComponent, fn)
{
    if (dotNetFullNameOfThirdPartyComponent == null)
    {
        throw CreateNewDeveloperError("Missing argument. @dotNetFullNameOfThirdPartyComponent cannot be null.");
    }

    if (fn == null)
    {
        throw CreateNewDeveloperError("Missing argument. @fn cannot be null.");
    }

    let arr = ThirdPartyComponentPropsCalculatedListeners[dotNetFullNameOfThirdPartyComponent];
    if (!arr)
    {
        arr = ThirdPartyComponentPropsCalculatedListeners[dotNetFullNameOfThirdPartyComponent] = [];
    }

    arr.push(fn);
}

function OnThirdPartyComponentPropsCalculatedTryFire(dotNetFullNameOfThirdPartyComponent, props, callerComponent)
{
    if (dotNetFullNameOfThirdPartyComponent == null)
    {
        throw CreateNewDeveloperError("Missing argument. @dotNetFullNameOfThirdPartyComponent cannot be null.");
    }

    const arr = ThirdPartyComponentPropsCalculatedListeners[dotNetFullNameOfThirdPartyComponent];
    if (arr)
    {
        for (let i = 0; i < arr.length; i++)
        {
            props = arr[i](props, callerComponent);
        }
    }

    return props;
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

function OnDocumentReady(callback)
{
    const stateCheck = setInterval(function ()
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

    for (const key in obj)
    {
        if (Object.prototype.hasOwnProperty.call(obj, key))
        {
            return false;
        }
    }

    return true;
}

function InvalidateQueuedFunctionsByName(name)
{
    const operations = Operations.array;

    for (let i = 0; i < operations.length; i++)
    {
        const operation = operations[i];
        if (operation.uniqueName === name)
        {
            operation.status = OperationStatusInvalidated;
        }
    }
}

const OperationStatusInitial = 1;
const OperationStatusWaitingRemoteResponse = 2;
const OperationStatusReactStateReady = 3;
const OperationStatusInvalidated = 4;
const OperationStatusRemoteFail = 5;

/**
 * @typedef {Object} Operation
 * @property {number?} status
 * @property {number?} id
 * @property {Component} component
 * @property {string?} remoteMethodName
 * @property {Object[]} remoteMethodArguments
 * @property {number?} isComponentWillUnmount
 * @property {string?} capturedStateTreeRootNodeKey
 * @property {Object?} capturedStateTree
 * @property {Function?} onPreviewHandler
 * @property {string?} uniqueName
 * @property {Function?} onCompleted
 * @property {number?} priorityIsMore
 * @property {Boolean?} isCacheable
 * @property {Function?} onRemoteSuccess
 */

/**
 * @type {{timer: number, array: Operation[]}}
 */
const Operations = {
    array:[],
    timer:0
};

function StartOperations()
{
    if (Operations.timer)
    {
        return;
    }

    Operations.timer = setInterval(Operate, 5);
}

function StopOperations()
{
    clearInterval(Operations.timer);
    Operations.timer = 0;
}

/**
 * @param {Operation} operation
 */
function AddOperation(operation)
{
    const operations = Operations.array;
    
    if (operation.priorityIsMore)
    {
        operations.splice(0, 0, operation);
    }
    else 
    {
        operations.push(operation);
    }
    StartOperations();
}

function Operate()
{    
    const operations = Operations.array;
        
    // try clear
    for (let i = 0; i < operations.length; i++)
    {
        const operation = operations[i];

        if (operation.status === OperationStatusInvalidated ||
            operation.status === OperationStatusReactStateReady ||
            operation.status === OperationStatusRemoteFail)
        {
            if (operation.onCompleted)
            {
                operation.onCompleted();
            }
            
            operations.splice(i, 1);
            i--;
        }
    }

    if (operations.length === 0)
    {
        StopOperations();
        return;
    }
    
    for (let i = 0; i < operations.length; i++)
    {
        const operation = operations[i];

        if (operation.status === OperationStatusWaitingRemoteResponse)
        {
            return;
        }
        
        if (operation.status === OperationStatusInitial)
        {            
            CallRemote(operation);
            return;
        }
    }    
}

let OperationUniqueId = 1;

/**
 * @param {Operation} operation
 */
function StartNewOperation(operation)
{
    operation.status = OperationStatusInitial;
    operation.id = OperationUniqueId++;

    AddOperation(operation);
}

function SetState(component, partialState, stateCallback)
{
    component.setState(partialState, stateCallback);
}

function TryGetValueInPath(obj, steps)
{
    steps = typeof steps === "string" ? steps.split(".") : steps;

    const len = steps.length;

    for (let i = 0; i < len; i++)
    {
        if (obj == null)
        {
            return { success: false, error: CreateNewDeveloperError("Path is not read. Path:" + steps.join(".")) };
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

    return { success: true, value: obj };
}

function GetValueInPath(obj, steps)
{
    const response = TryGetValueInPath(obj, steps);
    if (response.success)
    {
        return response.value;
    }

    throw response.error;
}

function SetValueInPath(obj, steps, value)
{
    if (obj == null)
    {
        throw CreateNewDeveloperError("SetValueInPath->" + value);
    }

    const len = steps.length;

    for (let i = 0; i < len; i++)
    {
        let step = steps[i];

        if (len === i + 3 && steps[i] === '[' && steps[i + 2] === ']')
        {
            obj[steps[i + 1]] = value;
            return;
        }

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
    let breadcrumb = parentScope.breadcrumb;

    if (fiberNode.key !== null)
    {
        breadcrumb = breadcrumb + ',' + fiberNode.key;
    }

    const scope = {map: parentScope.map, breadcrumb: breadcrumb};

    const isFiberNodeRelatedWithDotNetComponent = fiberNode.type && fiberNode.type[DotNetTypeOfReactComponent];
    if (isFiberNodeRelatedWithDotNetComponent)
    {
        const map = parentScope.map;

        if (map[breadcrumb] !== undefined)
        {
            throw CreateNewDeveloperError('Problem when traversing nodes');
        }

        map[breadcrumb] = {
            StateAsJson: JSON.stringify(fiberNode.stateNode.state[DotNetState]),
            FullTypeNameOfComponent: fiberNode.stateNode.state[DotNetTypeOfReactComponent],
            ComponentUniqueIdentifier: fiberNode.stateNode.state[DotNetComponentUniqueIdentifier]
        };
    }

    let child = fiberNode.child;
    while (child)
    {
        VisitFiberNodeForCaptureState(scope, child);
        child = child.sibling;
    }
};

const CaptureStateTreeFromFiberNode = (rootFiberNode) =>
{
    // I haven't known what is going here :)
    if (rootFiberNode.alternate && rootFiberNode.actualStartTime < rootFiberNode.alternate.actualStartTime)
    {
        rootFiberNode = rootFiberNode.alternate;
    }

    const rootNodeKey = rootFiberNode.key;

    const map = {};

    const stateInRootNode = rootFiberNode.stateNode.state;

    map[rootNodeKey] =
    {
        StateAsJson: JSON.stringify(stateInRootNode[DotNetState])
    };

    const rootScope = {map: map, breadcrumb: rootNodeKey};

    let child = rootFiberNode.child;
    while (child)
    {
        VisitFiberNodeForCaptureState(rootScope, child);
        child = child.sibling;
    }

    map[rootNodeKey][DotNetProperties] = Object.assign({}, NotNull(stateInRootNode[DotNetProperties]));

    // calculate $LogicalChildrenCount
    {
        const logicalChildrenCountCalculation = TryGetValueInPath(rootFiberNode.stateNode, "props.$jsonNode.$LogicalChildrenCount");
        if (logicalChildrenCountCalculation.success)
        {
            map[rootNodeKey][DotNetProperties].$LogicalChildrenCount = logicalChildrenCountCalculation.value;
        }
    }

    return { stateTree: map, rootNodeKey: rootNodeKey };
};

const GetNextSequence = (() =>
{
    let sequence = 1;

    return () => { return sequence++; };
})();


let DotNetComponentInstanceId_Next_Value = 1;

function InitializeDotNetComponentInstanceId(component)
{
    component['$DotNetComponentInstanceId'] = DotNetComponentInstanceId_Next_Value++;
}


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
        const node = new LinkedListNode(data);

        /**
         * @type {LinkedListNode}
         */
        let current;

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
        let current = this.head;
        let prev = null;

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
        let current = this.head;

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
}

class ComponentCacheItem
{
    constructor()
    {
        this.instanceArray = [];
        this.freeSpace = {};
        this.freeSpace[CUSTOM_EVENT_LISTENER_MAP] = {};
        this.freeSpace[ON_COMPONENT_DESTROY] = [];
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
            const hasMatch = componentCacheItem =>
            {
                return componentCacheItem.instanceArray.indexOf(component) >= 0;
            };
            const existingItem = this.linkedList.first(hasMatch);
            if (existingItem)
            {
                return;
            }
        }

        // has same unique identifier
        {
            const hasMatch = item =>
            {
                const instanceArray = item.instanceArray;

                const componentIdArray = component[DotNetComponentUniqueIdentifiers];

                for (let i = 0; i < instanceArray.length; i++)
                {
                    const idArray = instanceArray[i][DotNetComponentUniqueIdentifiers];

                    for (let j = 0; j < componentIdArray.length; j++)
                    {
                        if (idArray.indexOf(componentIdArray[j]) >= 0)
                        {
                            return true;
                        }
                    }                    
                }

                return false;
            };
            
            const existingItem = this.linkedList.first(hasMatch);
            if (existingItem)
            {
                // merge id list
                {
                    const instanceArray = existingItem.instanceArray;
                    const instanceArrayLength = instanceArray.length;
                    
                    const targetIdArray = component[DotNetComponentUniqueIdentifiers];

                    for (let i = 0; i < instanceArrayLength; i++)
                    {
                        const sourceIdArray = instanceArray[i][DotNetComponentUniqueIdentifiers];

                        for (let j = 0; j < sourceIdArray.length; j++)
                        {
                            const id = sourceIdArray[j];
                            
                            if (targetIdArray.indexOf(id) < 0)
                            {
                                targetIdArray.push(id);
                            }
                        }
                    }
                }
                
                existingItem.instanceArray.push(component);
                
                return;
            }
        }

        // add new item
        {
            const newItem = new ComponentCacheItem();

            newItem.instanceArray.push(component);

            this.linkedList.add(newItem);
        }
       
    }

    FindComponentByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier)
    {
        const firstItem = this.FindFirstCacheItemByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier);
        if (firstItem)
        {
            return firstItem.freeSpace.ref?.current ?? firstItem.instanceArray[firstItem.instanceArray.length - 1];
        }

        return null;
    }

    GetFreeSpaceOfComponent(dotNetComponentUniqueIdentifier)
    {
        const firstItem = this.FindFirstCacheItemByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier);
        if (firstItem)
        {
            return firstItem.freeSpace;
        }

        throw CreateNewDeveloperError('AccessToFreeSpace -> ComponentNotFound. dotNetComponentUniqueIdentifier:' + dotNetComponentUniqueIdentifier);
    }

    FindFirstCacheItemByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier)
    {
        const hasMatch = item =>
        {
            const instanceArray = item.instanceArray;
            
            const instanceArrayLength = instanceArray.length;
            
            for (let i = 0; i < instanceArrayLength; i++)
            {
                const idArray = instanceArray[i][DotNetComponentUniqueIdentifiers];
                if (idArray.indexOf(dotNetComponentUniqueIdentifier) >= 0)
                {
                    return true;
                }
            }

            return false;
        };

        return this.linkedList.first(hasMatch);
    }

    Unregister(component)
    {
        this.linkedList.removeFirst(item => item.instanceArray.indexOf(component) >= 0);
    }
}

const COMPONENT_CACHE = new ComponentCache();

function GetFreeSpaceOfComponent(component)
{
    return COMPONENT_CACHE.GetFreeSpaceOfComponent(component[DotNetComponentUniqueIdentifiers][0]);
}

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

function isEquals(a, b)
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
		return isTwoLiteralObjectEquals(a, b);
	}
}
function isTwoLiteralObjectEquals(o1, o2)
{
    let p;
    for(p in o1)
	{
        if(o1.hasOwnProperty(p))
		{
            if(!isEquals(o1[p], o2[p]))
			{
                return false;
            }
        }
    }

    for(p in o2)
	{
        if(o2.hasOwnProperty(p))
		{
            if(!isEquals(o1[p], o2[p]))
			{
                return false;
            }
        }
    }

    return true;
}

/**
 * @param {JsonNode} parentJsonNode
 * @param {RemoteMethodInfo} remoteMethodInfo
 * @returns {(function(): void)|*}
 */
function ConvertToEventHandlerFunction(parentJsonNode, remoteMethodInfo)
{
    const remoteMethodName   = remoteMethodInfo.remoteMethodName;
    const handlerComponentUniqueIdentifier = remoteMethodInfo.HandlerComponentUniqueIdentifier;
    const functionNameOfGrabEventArguments = remoteMethodInfo.FunctionNameOfGrabEventArguments;
    const stopPropagation = remoteMethodInfo.StopPropagation;
    const keyboardEventCallOnly = remoteMethodInfo.KeyboardEventCallOnly;
    const debounceTimeout = remoteMethodInfo.DebounceTimeout;

    NotNull(remoteMethodName);
    NotNull(handlerComponentUniqueIdentifier);

    const onClickPreview = parentJsonNode.$onClickPreview;
    let onPreviewHandler = null;
    if (onClickPreview)
    {
        onPreviewHandler = function ()
        {
            const cmp = GetComponentByDotNetComponentUniqueIdentifier(onClickPreview.$DotNetComponentUniqueIdentifier);

            const newState = CalculateNewStateFromJsonElement(cmp.state, onClickPreview);

            cmp.setState(newState);
        }
    }

    return function ()
    {
        if (stopPropagation)
        {
            if (arguments.length === 0)
            {
                throw CreateNewDeveloperError("There is no event argument for applying StopPropagation");
            }

            if (arguments[0] == null)
            {
                throw CreateNewDeveloperError("Trying to call StopPropagation method bu event argument is null.");
            }

            arguments[0].stopPropagation();
        }

        let eventArguments = Array.prototype.slice.call(arguments);

        if (keyboardEventCallOnly)
        {
            const key = arguments[0].key;
            if (keyboardEventCallOnly.indexOf(key) < 0)
            {
                return;
            }

            eventArguments[0].preventDefault();

            eventArguments[0] = ConvertToSyntheticKeyboardEvent(eventArguments[0]);
        }

        const targetComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);
        
        if (functionNameOfGrabEventArguments)
        {
            eventArguments = GetExternalJsObject(functionNameOfGrabEventArguments)(eventArguments);
        }
        
        if (debounceTimeout > 0)
        {
            const eventName = eventArguments[0]._reactName;

            const operationUniqueName = eventName + '-debounce-' + GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);

            InvalidateQueuedFunctionsByName(operationUniqueName);

            const timeoutKey = eventName + '-debounceTimeoutId';

            clearTimeout(targetComponent.state[timeoutKey]);

            const newState = {};
            newState[timeoutKey] = setTimeout(() =>
            {
                const operation = 
                {
                    component: targetComponent,
                    remoteMethodName: remoteMethodName,
                    remoteMethodArguments: eventArguments,
                    uniqueName: operationUniqueName
                };
                StartNewOperation(operation);

            }, debounceTimeout);

            newState[SyncId] = GetNextSequence();

            targetComponent.setState(newState);

            return;
        }

        {
            const operation = 
            {
                component: targetComponent,
                remoteMethodName: remoteMethodName,
                remoteMethodArguments: eventArguments,
                onPreviewHandler: onPreviewHandler,
                isCacheable: remoteMethodInfo.Cacheable
            };
            StartNewOperation(operation);
        }
    }
}

/**
 * @param {number} fakeChildIndex
 * @param {JsonNode} rootNodeInState
 * @param {JsonNode} jsonNodeInProps
 * @returns {*|null}
 * @constructor
 */
function FindRealNodeByFakeChild(fakeChildIndex, rootNodeInState, jsonNodeInProps)
{
    if (rootNodeInState && rootNodeInState.$FakeChild === fakeChildIndex)
    {
        return jsonNodeInProps;
    }
    const childrenInState = rootNodeInState.$children;
    const childrenInProps = jsonNodeInProps.$children;

    if (childrenInState && childrenInProps && childrenInState.length <= childrenInProps.length)
    {
        const length = childrenInState.length;

        for (let i = 0; i < length; i++)
        {
            const record = FindRealNodeByFakeChild(fakeChildIndex, childrenInState[i], childrenInProps[i]);
            if (record != null)
            {
                return record;
            }
        }
    }

    return null;
}

function ConvertToReactElement(jsonNode, component)
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
        // jsonNode = component.props.$jsonNode[RootNode].$children[jsonNode.$FakeChild];

        jsonNode = FindRealNodeByFakeChild(jsonNode.$FakeChild, component.state[RootNode], component.props.$jsonNode[RootNode]);
    }

    if (jsonNode.$isPureComponent)
    {
        const cmp = DefinePureComponent(jsonNode);

        const cmpKey = NotNull(jsonNode.key);

        const cmpProps =
        {
            key: cmpKey,
            $jsonNode: jsonNode,
            $styles: DynamicStyles[jsonNode[DotNetComponentUniqueIdentifier]]
        };

        if (jsonNode.$props)
        {
            Object.assign(cmpProps, jsonNode.$props);
        }

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
            $jsonNode: jsonNode,
            $styles: DynamicStyles[jsonNode[DotNetComponentUniqueIdentifier]]
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

        const currentComponent = COMPONENT_CACHE.FindComponentByDotNetComponentUniqueIdentifier(jsonNode[DotNetComponentUniqueIdentifier]);
        if (currentComponent)
        {
            const syncIdInState = ShouldBeNumber(currentComponent.state[SyncId]);
            const syncIdInProp = ShouldBeNumber(currentComponent.props[SyncId]);

            cmpProps[SyncId] = Math.max(syncIdInProp, syncIdInState);

            cmpProps.ref = currentComponent.myRef;
            
            GetFreeSpaceOfComponent(currentComponent).ref = currentComponent.myRef;
        }
        else
        {
            cmpProps[SyncId] = GetNextSequence();
        }

        if (jsonNode.$props)
        {
            Object.assign(cmpProps, jsonNode.$props);
        }

        return createElement(cmp, cmpProps);
    }

    let props = null;

    let elementType = jsonNode.$tag;
    if (!elementType)
    {
        throw CreateNewDeveloperError('ReactNode is not recognized');
    }

    let isThirdPartyComponent = false;

    if (/* is component */elementType.indexOf('.') > 0)
    {
        Before3rdPartyComponentAccess(elementType);

        elementType = GetExternalJsObject(elementType);

        isThirdPartyComponent = true;
    }

    if (elementType === 'nbsp')
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

        /**
         * @type {PropValue | RemoteMethodInfo | BindInfo | InnerElementInfo | ItemTemplate | {$transformValueFunction: string, RawValue: Object}}
         */
        const propValue = jsonNode[propName];

        if (propValue != null)
        {
            // tryProcessAsEventHandler
            if (propValue.$isRemoteMethod)
            {
                props[propName] = ConvertToEventHandlerFunction(jsonNode, propValue);

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
            if (propValue.$isBinding)
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
                        const operationUniqueName = eventName + '-debounce-' + GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);

                        InvalidateQueuedFunctionsByName(operationUniqueName);

                        const timeoutKey = eventName + '-debounceTimeoutId';

                        clearTimeout(targetComponent.state[timeoutKey]);

                        newState[timeoutKey] = setTimeout(() =>
                        {
                            const actionArguments = {
                                component: targetComponent,
                                remoteMethodName: debounceHandler,
                                remoteMethodArguments: [],
                                uniqueName: operationUniqueName
                            };
                            StartNewOperation(actionArguments);
                            

                        }, debounceTimeout);
                    }

                    newState[SyncId] = GetNextSequence();

                    targetComponent.setState(newState);

                }

                continue;
            }

            // tryProcessAsElement
            if (propValue.$isElement)
            {
                props[propName] = ConvertToReactElement(propValue.Element, component);

                continue;
            }

            // tryProcessAsItemsTemplate
            const itemTemplates = propValue.___ItemTemplates___;
            const itemTemplateForNull = propValue.___TemplateForNull___;
            if (itemTemplates)
            {
                props[propName] = function(item)
                {
                    if (React.isValidElement(item))
                    {
                        return item;
                    }

                    if (item == null && itemTemplateForNull)
                    {
                        return ConvertToReactElement(itemTemplateForNull);
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
                                return ConvertToReactElement(jsonNodeInTemplateInfo);
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
                                return ConvertToReactElement(jsonNodeInTemplateInfo);
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

    if (isThirdPartyComponent === true)
    {
        props = OnThirdPartyComponentPropsCalculatedTryFire(jsonNode.$tag, props, component);
    }

    if (jsonNode.$text != null)
    {
        return createElement(elementType, props, jsonNode.$text);
    }

    const children = jsonNode.$children;
    if (children)
    {
        const childrenLength = children.length;

        if (childrenLength === 1)
        {
            return createElement(elementType, props, ConvertToReactElement(children[0], component));
        }

        const newChildren = [];

        for (let childIndex = 0; childIndex < childrenLength; childIndex++)
        {
            newChildren.push(ConvertToReactElement(children[childIndex], component));
        }

        return createElement(elementType, props, newChildren);
    }

    return createElement(elementType, props);
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
    return {
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
        timeStamp: e.timeStamp,
        type:      e.type,

        target: ConvertToShadowHtmlElement(e.target),
        currentTarget :ConvertToShadowHtmlElement(e.currentTarget)
    };
}

function ConvertToSyntheticScrollEvent(e)
{
    return {
        currentTarget: ConvertToShadowHtmlElement(e.currentTarget),
        target: ConvertToShadowHtmlElement(e.target),
        timeStamp: e.timeStamp,
        type: e.type
    };
}

function ConvertToSyntheticKeyboardEvent(e)
{ 
    return {
        currentTarget: ConvertToShadowHtmlElement(e.currentTarget),
        target: ConvertToShadowHtmlElement(e.target),
        timeStamp: e.timeStamp,
        type: e.type,
        keyCode: e.keyCode,
        key: e.key,
        shiftKey: e.shiftKey,
        ctrlKey: e.ctrlKey,
        altKey: e.altKey,
        which: e.which
    };
}


function ConvertToSyntheticChangeEvent(e)
{
    const target = ConvertToShadowHtmlElement(e.target);

    if (e._reactName === 'onInput')
    {
        target.textContent = e.target.textContent;
    }

    return {
        bubbles:   e.bubbles,
        target:    target,
        timeStamp: e.timeStamp,
        type:      e.type
    };
}



function ConvertToShadowHtmlElement(htmlElement)
{
    if (htmlElement == null)
    {
        return null;
    }

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
        value: value,
        data: htmlElement.dataset,

        scrollHeight: htmlElement.scrollHeight,
        scrollLeft: htmlElement.scrollLeft,
        scrollTop: htmlElement.scrollTop,
        scrollWidth: htmlElement.scrollWidth
    };
}

/**
 * @param {ClientTask[]} clientTasks
 * @param {Object} callerInstance
 */
function ProcessClientTasks(clientTasks, callerInstance)
{
    if (clientTasks == null)
    {
        return;
    }

    const length = clientTasks.length;
    
    for (let i = 0; i < length; i++)
    {
        const jsFunctionPath      = clientTasks[i].JsFunctionPath;
        const jsFunctionArguments = clientTasks[i].JsFunctionArguments;

        InvokeJsFunctionInPath(jsFunctionPath, callerInstance, jsFunctionArguments);
    }
}

function IsSyntheticBaseEvent(e)
{
    try
    {
        return e.nativeEvent && typeof e.persist === 'function';
    }
    catch (exception)
    {
        return false;
    }
}

function ArrangeRemoteMethodArguments(remoteMethodArguments)
{
    if (remoteMethodArguments)
    {
        for (let i = 0; i < remoteMethodArguments.length; i++)
        {
            const prm = remoteMethodArguments[i];

            if (IsSyntheticBaseEvent(prm))
            {
                const reactName = prm._reactName;

                if (reactName)
                {
                    if (reactName.indexOf('Mouse') > 0 || reactName.indexOf('onClick') >= 0)
                    {
                        remoteMethodArguments[i] = ConvertToSyntheticMouseEvent(prm);
                    }
                    else if (reactName === 'onScroll')
                    {
                        remoteMethodArguments[i] = ConvertToSyntheticScrollEvent(prm);
                    }
                    else if (reactName === 'onChange')
                    {
                        remoteMethodArguments[i] = ConvertToSyntheticChangeEvent(prm);
                    }
                    else
                    {
                        throw new Error("FatalError: " + reactName);
                    }
                }
            }
        }
    }    
}

/**
 * @param {Operation} operation
 */
function CallRemote(operation)
{
    const remoteMethodName = operation.remoteMethodName;

    const isComponentWillUnmount = operation.isComponentWillUnmount;

    let component = NotNull(operation.component);

    if (component.ComponentWillUnmountIsCalled)
    {
        operation.status = OperationStatusReactStateReady;
        return;
    }

    component = GetComponentByDotNetComponentUniqueIdentifier(component[DotNetComponentUniqueIdentifiers][0]);

    if (component._reactInternals == null)
    {
        throw CreateNewDeveloperError('Component is not ready to send server.');
    }

    const isComponentPreview = component[DotNetTypeOfReactComponent] === 'ReactWithDotNet.UIDesigner.ReactWithDotNetDesignerComponentPreview,ReactWithDotNet' ||
                               component[DotNetTypeOfReactComponent] === "ReactWithDotNet.UIDesigner.HotReloadListener,ReactWithDotNet";

    let capturedStateTree,capturedStateTreeRootNodeKey;
    if (isComponentWillUnmount)
    {
        capturedStateTree = operation.capturedStateTree;
        capturedStateTreeRootNodeKey = operation.capturedStateTreeRootNodeKey;
    }
    else
    {
        const capturedStateTreeResponse = SafeExecute(() => CaptureStateTreeFromFiberNode(component._reactInternals));
        if (capturedStateTreeResponse.fail)
        {
            if (isComponentPreview)
            {
                location.reload();
            }

            throw capturedStateTreeResponse.exception;
        }

        capturedStateTree = capturedStateTreeResponse.value.stateTree;
        capturedStateTreeRootNodeKey = capturedStateTreeResponse.value.rootNodeKey;
    }

    const request =
    {
        MethodName: "HandleComponentEvent",

        EventHandlerMethodName: NotNull(remoteMethodName),
        FullName: NotNull(component.constructor)[DotNetTypeOfReactComponent],
        CapturedStateTree: capturedStateTree,
        CapturedStateTreeRootNodeKey: capturedStateTreeRootNodeKey,
        ComponentKey: parseInt(NotNull(component.props.$jsonNode.key)),
        LastUsedComponentUniqueIdentifier: LastUsedComponentUniqueIdentifier,
        ComponentUniqueIdentifier: NotNull(component.state[DotNetComponentUniqueIdentifier]),

        CallFunctionId: operation.id
    };

    ArrangeRemoteMethodArguments(operation.remoteMethodArguments);

    if (operation.isCacheable)
    {
        const freeSpace = GetFreeSpaceOfComponent(component);

        /**
         * @type {CacheableMethodInfo[]} 
         */
        const cachedMethods = freeSpace.$CachedMethods = freeSpace.$CachedMethods || [];

        for (let i = 0; i < cachedMethods.length; i++)
        {
            const cachedMethod = cachedMethods[i];

            if (cachedMethod.MethodName !== remoteMethodName)
            {
                continue;
            }
            if (cachedMethod.MethodArguments.length !== operation.remoteMethodArguments.length)
            {
                continue
            }

            // compare parameters
            {
                let hasSameParameters = true;

                for (let i = 0; i < operation.remoteMethodArguments.length; i++)
                {
                    if (!isEquals(operation.remoteMethodArguments[i], cachedMethod.MethodArguments[i]))
                    {
                        hasSameParameters = false;
                        break;
                    }
                }

                if (!hasSameParameters)
                {
                    continue;
                }
            }

            const newState = CalculateNewStateFromJsonElement(component.state, cachedMethod.ElementAsJson);

            operation.status = OperationStatusReactStateReady;
            
            component.setState(newState);

            return;
        }

        operation.onRemoteSuccess = function (remoteResponse)
        {
            freeSpace.$CachedMethods.push({
                MethodName: remoteMethodName,
                MethodArguments: Array.from(operation.remoteMethodArguments),
                ElementAsJson: remoteResponse.ElementAsJson
            });
        }
    }

    // prepare arguments
    {
        const remoteMethodArguments = Array.from(operation.remoteMethodArguments);

        const length = remoteMethodArguments.length;

        for (let i = 0; i < length; i++)
        {
            remoteMethodArguments[i] = JSON.stringify(remoteMethodArguments[i]);
        }

        request.EventArgumentsAsJsonArray = remoteMethodArguments;
    }


    /**
     * @param {RemoteExecutionResponse} response
     */
    function onSuccess(response)
    {
        if (operation.status === OperationStatusInvalidated)
        {
            // this function is not valid.
            // maybe expired by debounce mechanism.
            return;
        }

        if (response.ErrorMessage != null)
        {
            throw CreateNewDeveloperError(response.ErrorMessage);
        }

        if (response.LastUsedComponentUniqueIdentifier > LastUsedComponentUniqueIdentifier)
        {
            LastUsedComponentUniqueIdentifier = response.LastUsedComponentUniqueIdentifier;
        }

        ProcessDynamicCssClasses(response.DynamicStyles);

        if (response.SkipRender)
        {
            // note: setState not used here because this is special case. we don't want to trigger render.
            component.state[DotNetState] = response.NewState;
            component.state[DotNetProperties] = response.NewDotNetProperties;
            if (response.ClientTaskList)
            {
                component.state[ClientTasks] = response.ClientTaskList;

                HandleComponentClientTasks(component);
            }

            operation.status = OperationStatusReactStateReady;

            return;
        }

        if (operation.onRemoteSuccess)
        {
            operation.onRemoteSuccess(response);
        }
        
        const partialState = CalculateNewStateFromJsonElement(component.state, response.ElementAsJson);

        SetState(component, partialState, ()=>
        {
            operation.status = OperationStatusReactStateReady;
        });

        if (isComponentWillUnmount)
        {
            ProcessClientTasks(partialState[ClientTasks], component);

            operation.status = OperationStatusReactStateReady;
        }
    }

    function onFail(error)
    {
        // Maybe has network error on hot reload mode is active. We should retry. 
        if (isComponentPreview)
        {
            setTimeout(() => SendRequest(request, onSuccess, onFail), 1000);
            return;
        }

        console.error(error);

        operation.status = OperationStatusRemoteFail;
    }

    if (operation.onPreviewHandler)
    {
        operation.onPreviewHandler();
    }

    operation.status = OperationStatusWaitingRemoteResponse;

    SendRequest(request, onSuccess, onFail);
}

function CalculateNewStateFromJsonElement(componentState, jsonElement)
{
    // connect unique identifiers
    if (NotNull(componentState[DotNetComponentUniqueIdentifier]) !== NotNull(jsonElement[DotNetComponentUniqueIdentifier]))
    {
        const component = COMPONENT_CACHE.FindComponentByDotNetComponentUniqueIdentifier(componentState[DotNetComponentUniqueIdentifier]);
        if (component)
        {
            component[DotNetComponentUniqueIdentifiers].push(jsonElement[DotNetComponentUniqueIdentifier]);
        }
    }

    // new way
    jsonElement[SyncId] = GetNextSequence();

    return jsonElement;
}

const ComponentDefinitions = {};

// DESTROY UTILITY
function InvokeComponentDestroyListeners(componentInstance)
{
    const functionArray = GetFreeSpaceOfComponent(componentInstance)[ON_COMPONENT_DESTROY];

    for (let i = 0; i < functionArray.length; i++)
    {
        functionArray[i]();
    }
}

function OnComponentDestroy(component, fn)
{
    GetFreeSpaceOfComponent(component)[ON_COMPONENT_DESTROY].push(fn);
}

function DestroyDotNetComponentInstance(instance)
{
    InvokeComponentDestroyListeners(instance);

    RemoveComponentDynamicStyles(instance[DotNetComponentUniqueIdentifiers]);

    COMPONENT_CACHE.Unregister(instance);
}

function HandleComponentClientTasks(component)
{
    const clientTasks = component.state[ClientTasks];

    if (clientTasks == null || clientTasks.length === 0 || component.ComponentWillUnmountIsCalled === true)
    {
        return false;
    }

    const freeSpace = GetFreeSpaceOfComponent(component);
    if (freeSpace.lastProcessedClientTasks === clientTasks)
    {
        return false;
    }

    freeSpace.lastProcessedClientTasks = clientTasks;

    ProcessClientTasks(clientTasks, component);

    return true;
}

function DefineComponent(componentDeclaration)
{
    let cacheKeyForComponentDefinitions = componentDeclaration[DotNetTypeOfReactComponent];

    if (cacheKeyForComponentDefinitions === 'ReactWithDotNet.FunctionalComponent,ReactWithDotNet')
    {
        cacheKeyForComponentDefinitions = componentDeclaration[DotNetProperties]['RenderMethodNameWithToken'];
    }
    const component = ComponentDefinitions[cacheKeyForComponentDefinitions];
    if (component)
    {
        return component;
    }

    const dotNetTypeOfReactComponent = componentDeclaration[DotNetTypeOfReactComponent];

    class NewComponent extends React.Component
    {
        constructor(props)
        {
            super(props||{});

            this.myRef = React.createRef();
            
            const instance = this;

            // new way
            const initialState = {};
            if (props)
            {
                Object.assign(initialState, props.$jsonNode);

                initialState[SyncId] = ShouldBeNumber(props[SyncId]);
            }

            instance.state = initialState;

            initialState[DotNetTypeOfReactComponent] = instance[DotNetTypeOfReactComponent] = dotNetTypeOfReactComponent;
                        
            instance[DotNetComponentUniqueIdentifiers] = [NotNull(props.$jsonNode[DotNetComponentUniqueIdentifier])];

            InitializeDotNetComponentInstanceId(instance);

            COMPONENT_CACHE.Register(instance);
        }

        render()
        {
            const props = this.props;

            // move styles to dom if not exists
            {
                const styles = props.$styles;
                if (styles)
                {
                    const cuid = props.$jsonNode[DotNetComponentUniqueIdentifier];

                    if (DynamicStyles[cuid] !== styles)
                    {
                        DynamicStyles[cuid] = styles;

                        ApplyDynamicStylesToDom();
                    }
                }
            }            

            return ConvertToReactElement(this.state[RootNode], this);
        }

        componentDidMount()
        {
            COMPONENT_CACHE.Register(this);
            
            this.state['$didMount'] = 1;
            
            /**
             * @type {Component | NewComponent}
             */
            const component = this;

            HandleComponentClientTasks(this);

            const componentDidMountMethod = component.state[ComponentDidMountMethod];
            if (componentDidMountMethod == null)
            {
                return;
            }

            const partialState = {};

            partialState[ComponentDidMountMethod] = null;

            SetState(component, partialState, ()=>
            {
                /**
                 * @type {Operation}
                 */
                const operation =
                {
                    component: /** @type {Component}*/component,
                    remoteMethodName: componentDidMountMethod,
                    remoteMethodArguments: []
                };
                StartNewOperation(operation);
            });            
        }

        componentDidUpdate(prevProps, prevState, snapshot)
        {
            HandleComponentClientTasks(this);
        }

        componentWillUnmount()
        {
            if (ReactWithDotNet.StrictMode)
            {
                return;    
            }
            
            if (!this.state['$isUnmounting'])
            {
                this.setState({$isUnmounting: 1});

                if (!this.state['$didMount'])
                {
                    return;
                }

                if (this.ComponentWillUnmountIsCalled === true)
                {
                    throw 'componentWillUnmount -> ComponentWillUnmountIsCalled called twice';
                }

                const component = this;

                const componentWillUnmountMethod = component.state[ComponentWillUnmountMethod];
                if (componentWillUnmountMethod == null)
                {
                    // u n r e g i s t e r
                    component.ComponentWillUnmountIsCalled = true;
                    DestroyDotNetComponentInstance(component);

                    return;
                }

                {
                    // step: 1 start remote call
                    {
                        const capturedStateTreeResponse = SafeExecute(() => CaptureStateTreeFromFiberNode(component._reactInternals));
                        if (capturedStateTreeResponse.fail)
                        {
                            throw capturedStateTreeResponse.exception;
                        }

                        const capturedStateTree = capturedStateTreeResponse.value.stateTree;
                        const capturedStateTreeRootNodeKey = capturedStateTreeResponse.value.rootNodeKey;

                        /**
                         * @type {Operation}
                         */
                        const operation =
                        {
                            component: /** @type {Component}*/component,
                            remoteMethodName: componentWillUnmountMethod,
                            remoteMethodArguments: [],
                            isComponentWillUnmount: 1,

                            capturedStateTree: capturedStateTree,
                            capturedStateTreeRootNodeKey: capturedStateTreeRootNodeKey,
                            onCompleted: () =>
                            {
                                // u n r e g i s t e r
                                component.ComponentWillUnmountIsCalled = true;
                                DestroyDotNetComponentInstance(component);
                            }
                        };
                        StartNewOperation(operation);
                    }
                }

            }
        }

        static getDerivedStateFromProps(nextProps, prevState)
        {
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
                partialState[DotNetState] = NotNull(nextProps.$jsonNode[DotNetState]);

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

    ComponentDefinitions[cacheKeyForComponentDefinitions] = NewComponent;

    NewComponent.displayName = dotNetTypeOfReactComponent.split(',')[0].split('.').pop();
    if (NewComponent.displayName === 'FunctionalComponent')
    {
        NewComponent.displayName = componentDeclaration[DotNetProperties].Name;
    }

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
            const props = this.props;

            // move styles to dom if not exists
            {
                const styles = props['$styles'];
                if (styles)
                {
                    const cuid = props['$jsonNode'][DotNetComponentUniqueIdentifier];

                    if (DynamicStyles[cuid] !== styles)
                    {
                        DynamicStyles[cuid] = styles;

                        ApplyDynamicStylesToDom();
                    }
                }
            }            

            return ConvertToReactElement(props['$jsonNode'][RootNode], this);
        }
        componentWillUnmount()
        {
            const uid = NotNull(this.props['$jsonNode'][DotNetComponentUniqueIdentifier]);
            
            RemoveComponentDynamicStyles([uid]);
        }
    }

    ComponentDefinitions[dotNetTypeOfReactComponent] = NewPureComponent;

    NewPureComponent.displayName = dotNetTypeOfReactComponent.split(',')[0].split('.').pop();

    return NewPureComponent;
}

function SendRequest(request, onSuccess, onFail)
{
    request.ClientWidth  = document.documentElement.clientWidth;
    request.ClientHeight = document.documentElement.clientHeight;
    request.QueryString = window.location.search;

    const url = ReactWithDotNet.RequestHandlerPath;

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
        ReactWithDotNet.BeforeSendRequest(options);
    }

    window.fetch(url, options).then(response => response.json()).then(json => onSuccess(json)).catch(onFail);
}

let LastUsedComponentUniqueIdentifier = 1;

function ConnectComponentFirstResponseToReactSystem(containerHtmlElementId, response)
{
    if (response.ErrorMessage != null)
    {
        throw response.ErrorMessage;
    }

    ProcessDynamicCssClasses(response.DynamicStyles);

    const element = response.ElementAsJson;

    const component = element.$isPureComponent ? DefinePureComponent(element) : DefineComponent(element);

    LastUsedComponentUniqueIdentifier = response.LastUsedComponentUniqueIdentifier;

    const props = { key: '0', $jsonNode: element };

    props[SyncId] = GetNextSequence();

    const reactElement = createElement(component, props);

    const root = createRoot(document.getElementById(containerHtmlElementId));

    if (ReactWithDotNet.StrictMode)
    {
        root.render(createElement(React.StrictMode, null, reactElement));
    }
    else
    {
        root.render(reactElement);
    }
    
}

/**
 * @param {{renderInfo: RemoteExecutionResponse, idOfContainerHtmlElement: String, fullTypeNameOfReactComponent: string, containerHtmlElementId: string}} input
 * @constructor
 */
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

    const fullTypeNameOfReactComponent = input.fullTypeNameOfReactComponent;
    const containerHtmlElementId = input.containerHtmlElementId;

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
            ConnectComponentFirstResponseToReactSystem(containerHtmlElementId, response);
        }

        function onFail(error)
        {
            throw error;
        }

        SendRequest(request, onSuccess, onFail);
    });
}

/**
 * @param {string} jsFunctionPath
 * @param {Object} callerInstance
 * @param {Object[]} jsFunctionArguments
 * @returns {*}
 */
function InvokeJsFunctionInPath(jsFunctionPath, callerInstance, jsFunctionArguments)
{
    const fn = GetExternalJsObject(jsFunctionPath);

    return fn.apply(callerInstance, jsFunctionArguments);
}

const ExternalJsObjectMap = {
    'React.Fragment': React.Fragment
};

/**
 *  @param {string} key
 *  @param {Function|Object} value
 */
function RegisterExternalJsObject(key, value)
{
    if (ExternalJsObjectMap[key] != null)
    {
        console.log(key + ' already registered.');
    }
    return ExternalJsObjectMap[key] = value;
}
function GetExternalJsObject(key)
{
    const findResponse = TryFindExternalObject(key);
    if (findResponse != null)
    {
        if (findResponse['isCacheEnabled'] === true)
        {
            if (findResponse.value == null)
            {
                throw CreateNewDeveloperError(key + ' ==> isCacheEnabled is true but value property is null.');
            }

            RegisterExternalJsObject(key, findResponse.value);

            return findResponse.value;
        }

        return findResponse;
    }

    const value = ExternalJsObjectMap[key];
    if (value == null)
    {
        throw CreateNewDeveloperError(key + ' External js object not not found. You should register by using method: ReactWithDotNet.RegisterExternalJsObject');
    }

    return value;
}

const FindExternalObjectFnList = [];
function OnFindExternalObject(fn)
{
    FindExternalObjectFnList.push(fn);
}

function TryFindExternalObject(name)
{
    const items = FindExternalObjectFnList;

    const length = items.length;

    for (let i = 0; i < length; i++)
    {
        const response = items[i](name);
        if (response == null)
        {
            continue;
        }

        return response;
    }
}


function RegisterCoreFunction(name, fn)
{
    RegisterExternalJsObject("ReactWithDotNet::Core::" + name, fn);
}

ExternalJsObjectMap["ReactWithDotNet.GetExternalJsObject"] = GetExternalJsObject;

RegisterCoreFunction('RegExp', (x) => new RegExp(x));
RegisterCoreFunction('IsTwoObjectEquals', isEquals);

RegisterCoreFunction('CopyToClipboard', function (text)
{
    if (navigator.clipboard && navigator.clipboard.writeText)
    {
        navigator.clipboard.writeText(text).then(() => {});
        return;
    }

    if (window.clipboardData && window.clipboardData.setData)
    {
        // IE specific code path to prevent textarea being shown while dialog is visible.
        return window.clipboardData.setData("Text", text);
    }
});

RegisterCoreFunction('RunJavascript', (javascriptCode) => {

    window.eval(javascriptCode)
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

RegisterCoreFunction('ListenWindowResizeEvent', function (resizeTimeout, thresholdLength)
{
    let previousWidth = window.innerWidth;
    let previousHeight = window.innerHeight;
    
    let timeout = null;
    window.addEventListener('resize', function ()
    {
        const currentWidth = window.innerWidth;
        const currentHeight = window.innerHeight;
        
        if (Math.abs(previousWidth - currentWidth) > thresholdLength || Math.abs(previousHeight - currentHeight) > thresholdLength) 
        {
            clearTimeout(timeout);

            timeout = setTimeout(function ()
            {
                DispatchEvent('ReactWithDotNet::Core::OnWindowResize', [], 0);
            }, resizeTimeout);
        }

        previousWidth = currentWidth;
        previousHeight = currentHeight;       
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

RegisterCoreFunction("CalculateSyntheticFocusEventArguments", (argumentsAsArray) =>
{
    const e = argumentsAsArray[0];

    return [
        {
            bubbles: e.bubbles,
            cancelable: e.cancelable,
            currentTarget: ConvertToShadowHtmlElement(e.currentTarget),
            defaultPrevented: e.defaultPrevented,
            detail: e.detail,
            eventPhase: e.eventPhase,
            isTrusted: e.isTrusted,
            target: ConvertToShadowHtmlElement(e.target),
            relatedTarget: ConvertToShadowHtmlElement(e.relatedTarget),
            timeStamp: e.timeStamp,
            type: e.type
        }
    ];

});


function CalculateRemoteMethodArgument(arg)
{
    return arg;
}

function CalculateRemoteMethodArguments(args)
{
    if (args == null)
    {
        return null;
    }

    const newArgs = [];

    for (let i = 0; i < args.length; i++)
    {
        newArgs.push(CalculateRemoteMethodArgument(args[i]));
    }

    return newArgs;
}

RegisterCoreFunction('CalculateRemoteMethodArguments', CalculateRemoteMethodArguments);

function SetCookie(cookieName_StringNotNull, cookieValue_StringNotNull, expireDays_NumberNotNull)
{
    const expireDate = new Date();

    expireDate.setDate(expireDate.getDate() + expireDays_NumberNotNull);

    document.cookie = [
        cookieName_StringNotNull + "=" + encodeURI(cookieValue_StringNotNull),
        "expires" + "=" + expireDate.toUTCString(),
        "path=/"
    ].join(";");
}

RegisterCoreFunction("SetCookie", SetCookie);

RegisterCoreFunction("HistoryBack", function ()
{
    window.history.back();
});
RegisterCoreFunction("HistoryForward", function ()
{
    window.history.forward();
});
RegisterCoreFunction("HistoryGo", function (delta)
{
    window.history.go(delta);
});
RegisterCoreFunction("HistoryReplaceState", function (stateObj, title, url)
{
    if (stateObj == null)
    {
        stateObj = {};
    }
    window.history.replaceState(stateObj, title, url);
});

RegisterCoreFunction("GotoMethod", function (timeout, remoteMethodName, remoteMethodArguments)
{
    const component = this;

    remoteMethodArguments = remoteMethodArguments || [];

    setTimeout(() =>
    {
        if (component.ComponentWillUnmountIsCalled)
        {
            return;
        }

        const operation = 
        {
            component: component,
            remoteMethodName: remoteMethodName,
            remoteMethodArguments: remoteMethodArguments
        };
        StartNewOperation(operation);

    }, timeout);
});

function DispatchEvent(eventName, eventArguments, timeout)
{
    setTimeout(() =>
    {
        EventBus.Dispatch(eventName, eventArguments || []);

    }, timeout)
   
}
RegisterCoreFunction("DispatchEvent", DispatchEvent);

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

RegisterCoreFunction("DispatchDotNetCustomEvent", function(/*{EventSenderInfo}*/eventSenderInfo, eventArguments)
{
    const senderPropertyFullName = eventSenderInfo.SenderPropertyFullName;
    const senderComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(eventSenderInfo.SenderComponentUniqueIdentifier);

    const eventName = GetRealNameOfDotNetEvent(senderPropertyFullName, senderComponentUniqueIdentifier);

    EventBus.Dispatch(eventName, eventArguments);
});

RegisterCoreFunction("ListenEvent", function (eventName, remoteMethodName)
{
    const component = this;

    const onEventFired = (eventArgumentsAsArray) =>
    {
        if (component.ComponentWillUnmountIsCalled)
        {
            return;
        }

        /**
         * @type {Operation}
         */
        const operation = 
        {
            component: component,
            remoteMethodName: remoteMethodName,
            remoteMethodArguments: eventArgumentsAsArray,
            priorityIsMore: 1
        };
        StartNewOperation(operation);

        // guard for removed node before send to server
        OnComponentDestroy(component, () =>
        {
            operation.status = OperationStatusInvalidated;
        });
    };

    OnComponentDestroy(component, () =>
    {
        EventBus.Remove(eventName, onEventFired);
    });

    EventBus.On(eventName, onEventFired);
});

RegisterCoreFunction("ListenEventOnlyOnce", function (eventName, remoteMethodName)
{
    const component = this;

    const onEventFired = (eventArgumentsAsArray) =>
    {
        EventBus.Remove(eventName, onEventFired);

        /**
         * @type {Operation}
         */
        const operation = 
        {
            component: component,
            remoteMethodName: remoteMethodName,
            remoteMethodArguments: eventArgumentsAsArray,
            priorityIsMore: 1
        };
        StartNewOperation(operation);

        // guard for removed node before send to server
        OnComponentDestroy(component, () =>
        {
            operation.status = OperationStatusInvalidated;
        });
    };
    
    OnComponentDestroy(component, () =>
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

    const map = GetFreeSpaceOfComponent(component)[CUSTOM_EVENT_LISTENER_MAP];

    const senderPropertyFullName = eventSenderInfo.SenderPropertyFullName;
    const senderComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(eventSenderInfo.SenderComponentUniqueIdentifier);

    handlerComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);

    const onEventFired = (eventArgumentsAsArray) =>
    {
        const handlerComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);

        /**
         * @type {Operation}
         */
        const operation =
        {
            component: handlerComponent,
            remoteMethodName: remoteMethodName,
            remoteMethodArguments: eventArgumentsAsArray,
            priorityIsMore: 1
        };
        StartNewOperation(operation);

        // guard for removed node before send to server
        OnComponentDestroy(handlerComponent, () =>
        {
            operation.status = OperationStatusInvalidated;
        });
    };

    // avoid multiple attach we need to ensure attach a listener at once
    const key = [
        'senderPropertyFullName:' + senderPropertyFullName,
        'senderComponentUniqueIdentifier:' + senderComponentUniqueIdentifier,
        'handlerComponentUniqueIdentifier:' + handlerComponentUniqueIdentifier
    ].join(',');

    if (map[key])
    {
        return;
    }

    map[key] = onEventFired;

    const eventName = GetRealNameOfDotNetEvent(senderPropertyFullName, senderComponentUniqueIdentifier);

    OnComponentDestroy(component, () =>
    {
        EventBus.Remove(eventName, onEventFired);

        map[key] = null;
    });

    EventBus.On(eventName, onEventFired);
});

function NavigateTo(path)
{
    const location = window.location;

    location.assign(location.origin + path);
}

RegisterCoreFunction("NavigateTo", NavigateTo);

function OnOutsideClicked(component, operationType, idOfElement, remoteMethodName, handlerComponentUniqueIdentifier)
{
    const map = GetFreeSpaceOfComponent(component)[CUSTOM_EVENT_LISTENER_MAP];

    const isRemove = operationType === "remove";

    handlerComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);

    function onDocumentClick(e)
    {
        const element = document.getElementById(idOfElement);
        if (element == null)
        {
            return;
        }
        const isClickedOutside = !element.contains(e.target)
        if (isClickedOutside)
        {
            const handlerComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);

            /**
             * @type {Operation}
             */
            const operation =
            {
                component: handlerComponent,
                remoteMethodName: remoteMethodName,
                remoteMethodArguments: [ConvertToSyntheticMouseEvent(e)]
            };
            StartNewOperation(operation);
        }
    }

    // avoid multiple attach we need to ensure attach a listener at once
    
    const key = 'OnOutsideClicked(IdOfElement:' + idOfElement + ', remoteMethodName:' + remoteMethodName + ', @handlerComponentUniqueIdentifier:' + handlerComponentUniqueIdentifier + ')';

    if (map[key])
    {
        if (isRemove)
        {
            document.removeEventListener('click', map[key]);

            map[key] = null;
        }

        return;
    }

    if (isRemove)
    {
        return;
    }

    map[key] = onDocumentClick;    

    document.addEventListener('click', onDocumentClick);

    OnComponentDestroy(component, () =>
    {
        document.removeEventListener('click', onDocumentClick);

        map[key] = null;
    });
}

RegisterCoreFunction("AddEventListener", function (idOfElement, eventName, remoteMethodName, handlerComponentUniqueIdentifier)
{
    if (eventName === "OutsideClick")
    {
        OnOutsideClicked(this, "add", idOfElement, remoteMethodName, handlerComponentUniqueIdentifier)
    }
    else
    {
        throw CreateNewDeveloperError("InvalidUsageOfAddEventListener");
    }
});
RegisterCoreFunction("RemoveEventListener", function (idOfElement, eventName, remoteMethodName, handlerComponentUniqueIdentifier)
{
    if (eventName === "OutsideClick")
    {
        OnOutsideClicked(this, "remove", idOfElement, remoteMethodName, handlerComponentUniqueIdentifier)
    }
    else
    {
        throw CreateNewDeveloperError("InvalidUsageOfRemoveEventListener");
    }
});

function CreateNewDeveloperError(message)
{
    return new Error('\nReactWithDotNet developer error occured.\n' + message);
}

/*
  {  "cuid": [[cssClassInfo]]], ...  }
*/
let DynamicStyles = {};
let ReactWithDotNetDynamicCssElement = null;



function ProcessDynamicCssClasses(dynamicStyles)
{
    if (dynamicStyles == null)
    {
        return;
    }

    // import    
    for (let cuid in dynamicStyles)
    {
        if (dynamicStyles.hasOwnProperty(cuid))
        {
            DynamicStyles[cuid] = dynamicStyles[cuid];
        }
    }

    ApplyDynamicStylesToDom();
}

function ApplyDynamicStylesToDom()
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

    for (let cuid in DynamicStyles)
    {
        if (DynamicStyles.hasOwnProperty(cuid))
        {
            const values = DynamicStyles[cuid];
            if (values == null)
            {
                continue;
            }

            const length = values.length;
            for (let i = 0; i < length; i++)
            {
                const items = values[i];

                const name = items[0];
                const body = items[1];
                const mediaQueries = items[2];
                const pseudos = items[3];

                arr.push("." + name);
                arr.push("{");
                arr.push(body);
                arr.push("}");

                if (mediaQueries)
                {
                    const count = mediaQueries.length;

                    for (let j = 0; j < count; j++)
                    {
                        const mediaRule = mediaQueries[j][0];
                        const cssBody = mediaQueries[j][1];

                        arr.push("@media " + mediaRule + " {")
                        arr.push("  ." + name + " {");
                        arr.push(cssBody);
                        arr.push("  }");
                        arr.push("}");
                    }
                }

                if (pseudos)
                {
                    const count = pseudos.length;

                    for (let j = 0; j < count; j++)
                    {
                        const pseudoName = pseudos[j][0];
                        const pseudoBody = pseudos[j][1];

                        arr.push("." + name + ":" + pseudoName + "{");
                        arr.push(pseudoBody);
                        arr.push("}");
                    }
                }
            }
        }
    }

    // try update if it has changed
    const newStyle = arr.join("\n");
    if (!IsTwoStringHasValueAndSame(ReactWithDotNetDynamicCssElement.innerHTML, newStyle))
    {
        ReactWithDotNetDynamicCssElement.innerHTML = newStyle;
    }
}

function CloneObjectWithoutNullValues(obj)
{
    const clone = {};

    for (let key in obj) 
    {
        if (obj.hasOwnProperty(key))
        {
            const value = obj[key];
            if ( value != null)
            {
                clone[key] = obj[key];
            }
        }
    }

    return clone;
}

// protect for too many empty keys
setInterval(() =>
{
    DynamicStyles = CloneObjectWithoutNullValues(DynamicStyles);
}, 3000);

/**
 * @param {number[]} componentUniqueIdentifiers
 */
function RemoveComponentDynamicStyles(componentUniqueIdentifiers)
{
    let hasChange = false;

    const length = componentUniqueIdentifiers.length;

    for (let i = 0; i < length; i++)
    {
        const cuid = componentUniqueIdentifiers[i];

        if (DynamicStyles[cuid] != null)
        {
            hasChange = true;

            DynamicStyles[cuid] = null;
        }
    }

    if (hasChange)
    {
        ApplyDynamicStylesToDom();
    }
}

/**
 * @param {string} a
 * @param {string} b
 */
function IsTwoStringHasValueAndSame(a, b)
{
    if (a == null || b == null)
    {
        return false;
    }

    const anyWhiteSpaceRegex = /\s/g;

    a = a.replace(anyWhiteSpaceRegex, '');
    b = b.replace(anyWhiteSpaceRegex, '');

    return a === b;
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

const ReactWithDotNet =
{
    StrictMode: false,
    RequestHandlerPath: 'DeveloperError: missing RequestHandlerPath',
    "OnDocumentReady": OnDocumentReady,
    "StartAction": StartNewOperation,
    DispatchEvent: DispatchEvent,
    "RenderComponentIn": RenderComponentIn,
    BeforeSendRequest: x=>x,
    RegisterExternalJsObject: RegisterExternalJsObject,
    GetExternalJsObject: GetExternalJsObject,
    BeforeAnyThirdPartyComponentAccess: BeforeAnyThirdPartyComponentAccess,
    TryLoadCssByHref: TryLoadCssByHref,
    OnThirdPartyComponentPropsCalculated: OnThirdPartyComponentPropsCalculated,
    OnFindExternalObject: OnFindExternalObject,

    IsMediaMobile: IsMobile,
    IsMediaTablet: IsTablet,
    IsMediaDesktop: IsDesktop,

    Call: InvokeJsFunctionInPath,
    Util:
    {
        SetCookie: SetCookie
    }
};

window.ReactWithDotNet = ReactWithDotNet;

export default ReactWithDotNet;