import {
  require_react_dom
} from "./chunk-QNQS7X5M.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __commonJS,
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/react-dom/client.js
var require_client = __commonJS({
  "node_modules/react-dom/client.js"(exports) {
    "use strict";
    var m = require_react_dom();
    if (false) {
      exports.createRoot = m.createRoot;
      exports.hydrateRoot = m.hydrateRoot;
    } else {
      i = m.__SECRET_INTERNALS_DO_NOT_USE_OR_YOU_WILL_BE_FIRED;
      exports.createRoot = function(c, o) {
        i.usingClientEntryPoint = true;
        try {
          return m.createRoot(c, o);
        } finally {
          i.usingClientEntryPoint = false;
        }
      };
      exports.hydrateRoot = function(c, h, o) {
        i.usingClientEntryPoint = true;
        try {
          return m.hydrateRoot(c, h, o);
        } finally {
          i.usingClientEntryPoint = false;
        }
      };
    }
    var i;
  }
});

// react-with-dotnet/react-with-dotnet.jsx
var import_react = __toESM(require_react());
var import_client = __toESM(require_client());
var createElement = import_react.default.createElement;
var DotNetTypeOfReactComponent = "$Type";
var RootNode = "$RootNode";
var ClientTasks = "$ClientTasks";
var SyncId = "$SyncId";
var DotNetState = "$State";
var ComponentDidMountMethod = "$ComponentDidMountMethod";
var ComponentWillUnmountMethod = "$ComponentWillUnmountMethod";
var DotNetComponentUniqueIdentifier = "$DotNetComponentUniqueIdentifier";
var DotNetComponentUniqueIdentifiers = "$DotNetComponentUniqueIdentifiers";
var ON_COMPONENT_DESTROY = "$ON_COMPONENT_DESTROY";
var CUSTOM_EVENT_LISTENER_MAP = "$CUSTOM_EVENT_LISTENER_MAP";
var DotNetProperties = "DotNetProperties";
function SafeExecute(fn) {
  try {
    return { success: true, fail: false, value: fn() };
  } catch (exception) {
    return { success: false, fail: true, exception };
  }
}
function TryRemoveItemFromArray(array, item) {
  if (array == null || array.length === 0) {
    return;
  }
  const index = array.indexOf(item);
  if (index >= 0) {
    array.splice(index, 1);
    return true;
  }
  return false;
}
var EventBusImp = class {
  constructor() {
    this.map = {};
  }
  subscribe(eventName, callback) {
    let listenerFunctions = this.map[eventName];
    if (!listenerFunctions) {
      this.map[eventName] = listenerFunctions = [];
    }
    listenerFunctions.push(callback);
  }
  unsubscribe(eventName, callback) {
    TryRemoveItemFromArray(this.map[eventName], callback);
  }
  publish(eventName, eventArgumentsAsArray) {
    if (eventArgumentsAsArray == null) {
      throw CreateNewDeveloperError("Publish event arguments should be given in array. @Example: ReactWithDotNet.DispatchEvent('MovieActorNameChanged', ['Tom Hanks']);");
    }
    const listenerFunctions = this.map[eventName];
    if (listenerFunctions == null || listenerFunctions.length === 0) {
      return;
    }
    const functionArray = listenerFunctions.slice(0);
    for (let i = 0; i < functionArray.length; i++) {
      if (listenerFunctions.indexOf(functionArray[i]) >= 0) {
        functionArray[i].apply(null, [eventArgumentsAsArray]);
      }
    }
  }
};
var EventBus = {
  bus: new EventBusImp(),
  On: function(event, callback) {
    EventBus.bus.subscribe(event, callback);
  },
  Dispatch: function(eventName, eventArgumentsAsArray) {
    EventBus.bus.publish(eventName, eventArgumentsAsArray);
  },
  Remove: function(event, callback) {
    EventBus.bus.unsubscribe(event, callback);
  }
};
var Before3rdPartyComponentAccessListeners = [];
function Before3rdPartyComponentAccess(dotNetFullClassNameOf3rdPartyComponent) {
  for (let i = 0; i < Before3rdPartyComponentAccessListeners.length; i++) {
    Before3rdPartyComponentAccessListeners[i](dotNetFullClassNameOf3rdPartyComponent);
  }
}
function BeforeAnyThirdPartyComponentAccess(fn) {
  Before3rdPartyComponentAccessListeners.push(fn);
}
var ThirdPartyComponentPropsCalculatedListeners = {};
function OnThirdPartyComponentPropsCalculated(dotNetFullNameOfThirdPartyComponent, fn) {
  if (dotNetFullNameOfThirdPartyComponent == null) {
    throw CreateNewDeveloperError("Missing argument. @dotNetFullNameOfThirdPartyComponent cannot be null.");
  }
  if (fn == null) {
    throw CreateNewDeveloperError("Missing argument. @fn cannot be null.");
  }
  let arr = ThirdPartyComponentPropsCalculatedListeners[dotNetFullNameOfThirdPartyComponent];
  if (!arr) {
    arr = ThirdPartyComponentPropsCalculatedListeners[dotNetFullNameOfThirdPartyComponent] = [];
  }
  arr.push(fn);
}
function OnThirdPartyComponentPropsCalculatedTryFire(dotNetFullNameOfThirdPartyComponent, props, callerComponent) {
  if (dotNetFullNameOfThirdPartyComponent == null) {
    throw CreateNewDeveloperError("Missing argument. @dotNetFullNameOfThirdPartyComponent cannot be null.");
  }
  const arr = ThirdPartyComponentPropsCalculatedListeners[dotNetFullNameOfThirdPartyComponent];
  if (arr) {
    for (let i = 0; i < arr.length; i++) {
      props = arr[i](props, callerComponent);
    }
  }
  return props;
}
function TryLoadCssByHref(href) {
  const headElement = document.querySelector(`head`);
  if (headElement == null) {
    return { error: "Head element not found in document." };
  }
  const linkElement = headElement.querySelector('link[href*="' + href + '"]');
  if (linkElement) {
    return { isAlreadyLoaded: true };
  }
  const newLinkElement = document.createElement(`link`);
  newLinkElement.rel = `stylesheet`;
  newLinkElement.href = href;
  newLinkElement.type = "text/css";
  headElement.appendChild(newLinkElement);
  return { loadStarted: true };
}
function OnDocumentReady(callback) {
  const stateCheck = setInterval(function() {
    if (document.readyState === "complete") {
      clearInterval(stateCheck);
      setTimeout(callback, 1);
    }
  }, 10);
}
function IsEmptyObject(obj) {
  if (IsSerializablePrimitiveJsValue(obj)) {
    return false;
  }
  for (const key in obj) {
    if (Object.prototype.hasOwnProperty.call(obj, key)) {
      return false;
    }
  }
  return true;
}
function InvalidateQueuedFunctionsByName(name) {
  const operations = Operations.array;
  for (let i = 0; i < operations.length; i++) {
    const operation = operations[i];
    if (operation.uniqueName === name) {
      operation.status = OperationStatusInvalidated;
    }
  }
}
var OperationStatusInitial = 1;
var OperationStatusWaitingRemoteResponse = 2;
var OperationStatusReactStateReady = 3;
var OperationStatusInvalidated = 4;
var OperationStatusRemoteFail = 5;
var Operations = {
  array: [],
  timer: 0
};
function StartOperations() {
  if (Operations.timer) {
    return;
  }
  Operations.timer = setInterval(Operate, 5);
}
function StopOperations() {
  clearInterval(Operations.timer);
  Operations.timer = 0;
}
function AddOperation(operation) {
  const operations = Operations.array;
  if (operation.priorityIsMore) {
    operations.splice(0, 0, operation);
  } else {
    operations.push(operation);
  }
  StartOperations();
}
function Operate() {
  const operations = Operations.array;
  for (let i = 0; i < operations.length; i++) {
    const operation = operations[i];
    if (operation.status === OperationStatusInvalidated || operation.status === OperationStatusReactStateReady || operation.status === OperationStatusRemoteFail) {
      if (operation.onCompleted) {
        operation.onCompleted();
      }
      operations.splice(i, 1);
      i--;
    }
  }
  if (operations.length === 0) {
    StopOperations();
    return;
  }
  for (let i = 0; i < operations.length; i++) {
    const operation = operations[i];
    if (operation.status === OperationStatusWaitingRemoteResponse) {
      return;
    }
    if (operation.status === OperationStatusInitial) {
      CallRemote(operation);
    }
  }
}
var OperationUniqueId = 1;
function StartNewOperation(operation) {
  operation.status = OperationStatusInitial;
  operation.id = OperationUniqueId++;
  AddOperation(operation);
}
function SetState(component, partialState, stateCallback) {
  component.setState(partialState, stateCallback);
}
function TryGetValueInPath(obj, steps) {
  steps = typeof steps === "string" ? steps.split(".") : steps;
  const len = steps.length;
  for (let i = 0; i < len; i++) {
    if (obj == null) {
      return { success: false, error: CreateNewDeveloperError("Path is not read. Path:" + steps.join(".")) };
    }
    let step = steps[i];
    if (step === "[") {
      step = steps[i + 1];
      i++;
      i++;
    }
    obj = obj[step];
  }
  return { success: true, value: obj };
}
function GetValueInPath(obj, steps) {
  const response = TryGetValueInPath(obj, steps);
  if (response.success) {
    return response.value;
  }
  throw response.error;
}
function SetValueInPath(obj, steps, value) {
  if (obj == null) {
    throw CreateNewDeveloperError("SetValueInPath->" + value);
  }
  const len = steps.length;
  for (let i = 0; i < len; i++) {
    let step = steps[i];
    if (len === i + 3 && steps[i] === "[" && steps[i + 2] === "]") {
      obj[steps[i + 1]] = value;
      return;
    }
    if (obj[step] == null) {
      obj[step] = {};
    }
    if (i === len - 1) {
      obj[step] = value;
    } else {
      if (step === "[") {
        step = steps[i + 1];
        i++;
        i++;
      }
      obj = obj[step];
    }
  }
}
function NotNull(value) {
  if (value == null) {
    throw CreateNewDeveloperError("value cannot be null.");
  }
  return value;
}
function ShouldBeNumber(value) {
  if (typeof value === "number") {
    if (!isNaN(value)) {
      return value;
    }
  }
  throw CreateNewDeveloperError("value should be number.");
}
function Clone(obj) {
  return JSON.parse(JSON.stringify(obj));
}
function IfNull(value, defaultValue) {
  if (defaultValue === void 0) {
    return value;
  }
  if (value == null) {
    return defaultValue;
  }
  return value;
}
var VisitFiberNodeForCaptureState = (parentScope, fiberNode) => {
  let breadcrumb = parentScope.breadcrumb;
  if (fiberNode.key !== null) {
    breadcrumb = breadcrumb + "," + fiberNode.key;
  }
  const scope = { map: parentScope.map, breadcrumb };
  const isFiberNodeRelatedWithDotNetComponent = fiberNode.type && fiberNode.type[DotNetTypeOfReactComponent];
  if (isFiberNodeRelatedWithDotNetComponent) {
    const map = parentScope.map;
    if (map[breadcrumb] !== void 0) {
      throw CreateNewDeveloperError("Problem when traversing nodes");
    }
    map[breadcrumb] = {
      StateAsJson: JSON.stringify(fiberNode.stateNode.state[DotNetState]),
      FullTypeNameOfComponent: fiberNode.stateNode.state[DotNetTypeOfReactComponent],
      ComponentUniqueIdentifier: fiberNode.stateNode.state[DotNetComponentUniqueIdentifier]
    };
  }
  let child = fiberNode.child;
  while (child) {
    VisitFiberNodeForCaptureState(scope, child);
    child = child.sibling;
  }
};
var CaptureStateTreeFromFiberNode = (rootFiberNode) => {
  if (rootFiberNode.alternate && rootFiberNode.actualStartTime < rootFiberNode.alternate.actualStartTime) {
    rootFiberNode = rootFiberNode.alternate;
  }
  const rootNodeKey = rootFiberNode.key;
  const map = {};
  const stateInRootNode = rootFiberNode.stateNode.state;
  map[rootNodeKey] = {
    StateAsJson: JSON.stringify(stateInRootNode[DotNetState])
  };
  const rootScope = { map, breadcrumb: rootNodeKey };
  let child = rootFiberNode.child;
  while (child) {
    VisitFiberNodeForCaptureState(rootScope, child);
    child = child.sibling;
  }
  map[rootNodeKey][DotNetProperties] = Object.assign({}, NotNull(stateInRootNode[DotNetProperties]));
  {
    const logicalChildrenCountCalculation = TryGetValueInPath(rootFiberNode.stateNode, "props.$jsonNode.$LogicalChildrenCount");
    if (logicalChildrenCountCalculation.success) {
      map[rootNodeKey][DotNetProperties].$LogicalChildrenCount = logicalChildrenCountCalculation.value;
    }
  }
  return { stateTree: map, rootNodeKey };
};
var GetNextSequence = /* @__PURE__ */ (() => {
  let sequence = 1;
  return () => {
    return sequence++;
  };
})();
var DotNetComponentInstanceId_Next_Value = 1;
function InitializeDotNetComponentInstanceId(component) {
  component["$DotNetComponentInstanceId"] = DotNetComponentInstanceId_Next_Value++;
}
var LinkedListNode = class {
  constructor(data) {
    this.data = data;
    this.next = null;
  }
};
var LinkedList = class {
  constructor() {
    this.head = null;
    this.size = 0;
  }
  add(data) {
    const node = new LinkedListNode(data);
    let current;
    if (this.head == null) {
      this.head = node;
    } else {
      current = this.head;
      while (current.next) {
        current = current.next;
      }
      current.next = node;
    }
    this.size++;
  }
  removeFirst(isMatch) {
    let current = this.head;
    let prev = null;
    while (current != null) {
      if (isMatch(current.data) === true) {
        if (prev == null) {
          this.head = current.next;
        } else {
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
  first(isMatch) {
    let current = this.head;
    while (current != null) {
      if (isMatch(current.data) === true) {
        return current.data;
      }
      current = current.next;
    }
    return null;
  }
};
function MergeDotNetComponentUniqueIdentifiers(sourceIdList, targetIdList) {
  for (let i = 0; i < sourceIdList.length; i++) {
    const value = sourceIdList[i];
    if (targetIdList.indexOf(value) >= 0) {
      continue;
    }
    targetIdList.push(value);
  }
}
var ComponentCacheItem = class {
  constructor() {
    this.component = null;
    this.freeSpace = {};
    this.freeSpace[CUSTOM_EVENT_LISTENER_MAP] = {};
    this.freeSpace[ON_COMPONENT_DESTROY] = [];
  }
};
var ComponentCache = class {
  constructor() {
    this.linkedList = new LinkedList();
  }
  Register(component) {
    NotNull(component);
    {
      const isReferenceEquals = (item) => item.component === component;
      const existingItem = this.linkedList.first(isReferenceEquals);
      if (existingItem) {
        return;
      }
    }
    {
      const isTwiceRendered = (item) => item.component[DotNetComponentUniqueIdentifiers][0] === component[DotNetComponentUniqueIdentifiers][0];
      const existingItem = this.linkedList.first(isTwiceRendered);
      if (existingItem) {
        MergeDotNetComponentUniqueIdentifiers(existingItem.component[DotNetComponentUniqueIdentifiers], component[DotNetComponentUniqueIdentifiers]);
        existingItem.component = component;
        return;
      }
    }
    const newItem = new ComponentCacheItem();
    newItem.component = component;
    this.linkedList.add(newItem);
  }
  FindComponentByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier) {
    const firstItem = this.FindFirstCacheItemByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier);
    if (firstItem) {
      return firstItem.component;
    }
    return null;
  }
  GetFreeSpaceOfComponent(dotNetComponentUniqueIdentifier) {
    const firstItem = this.FindFirstCacheItemByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier);
    if (firstItem) {
      return firstItem.freeSpace;
    }
    throw CreateNewDeveloperError("AccessToFreeSpace -> ComponentNotFound. dotNetComponentUniqueIdentifier:" + dotNetComponentUniqueIdentifier);
  }
  FindFirstCacheItemByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier) {
    const hasMatch = (item) => {
      if (item.component && item.component[DotNetComponentUniqueIdentifiers]) {
        return item.component[DotNetComponentUniqueIdentifiers].indexOf(dotNetComponentUniqueIdentifier) >= 0;
      }
      return false;
    };
    return this.linkedList.first(hasMatch);
  }
  Unregister(component) {
    this.linkedList.removeFirst((item) => item.component === component);
  }
};
var COMPONENT_CACHE = new ComponentCache();
function GetFreeSpaceOfComponent(component) {
  return COMPONENT_CACHE.GetFreeSpaceOfComponent(component[DotNetComponentUniqueIdentifiers][0]);
}
function GetComponentByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier) {
  const component = COMPONENT_CACHE.FindComponentByDotNetComponentUniqueIdentifier(dotNetComponentUniqueIdentifier);
  if (component == null) {
    throw CreateNewDeveloperError("Component not found. dotNetComponentUniqueIdentifier: " + dotNetComponentUniqueIdentifier);
  }
  return component;
}
function GetFirstAssignedUniqueIdentifierValueOfComponent(componentUniqueIdentifier) {
  return GetComponentByDotNetComponentUniqueIdentifier(componentUniqueIdentifier)[DotNetComponentUniqueIdentifiers][0];
}
function isEquals(a, b) {
  if (a === b) {
    return true;
  }
  if (a instanceof Date && typeof b instanceof Date) {
    return a.valueOf() === b.valueOf();
  }
  if (typeof a === "object" && typeof b === "object") {
    return isTwoLiteralObjectEquals(a, b);
  }
}
function isTwoLiteralObjectEquals(o1, o2) {
  let p;
  for (p in o1) {
    if (o1.hasOwnProperty(p)) {
      if (!isEquals(o1[p], o2[p])) {
        return false;
      }
    }
  }
  for (p in o2) {
    if (o2.hasOwnProperty(p)) {
      if (!isEquals(o1[p], o2[p])) {
        return false;
      }
    }
  }
  return true;
}
function GetAllCachedMethodsOfComponent(component) {
  return component.state.$CachedMethods;
}
function tryToFindCachedMethodInfo(component, remoteMethodName, eventArguments) {
  const cachedMethods = GetAllCachedMethodsOfComponent(component);
  if (cachedMethods == null) {
    return null;
  }
  const length = cachedMethods.length;
  for (let i = 0; i < length; i++) {
    const cachedMethodInfo = cachedMethods[i];
    if (cachedMethodInfo.MethodName === remoteMethodName && cachedMethodInfo.IgnoreParameters) {
      return cachedMethodInfo;
    }
    if (remoteMethodName === "componentDidMount" && cachedMethodInfo.MethodName.endsWith("|componentDidMount")) {
      return cachedMethodInfo;
    }
    if (remoteMethodName === "componentWillUnmount" && cachedMethodInfo.MethodName.endsWith("|componentWillUnmount")) {
      return cachedMethodInfo;
    }
    if (cachedMethodInfo.MethodName === remoteMethodName && eventArguments.length === 1) {
      if (isEquals(eventArguments[0], cachedMethodInfo.Parameter)) {
        return cachedMethodInfo;
      }
    }
  }
  return null;
}
function ConvertToEventHandlerFunction(parentJsonNode, remoteMethodInfo) {
  const remoteMethodName = remoteMethodInfo.remoteMethodName;
  const handlerComponentUniqueIdentifier = remoteMethodInfo.HandlerComponentUniqueIdentifier;
  const functionNameOfGrabEventArguments = remoteMethodInfo.FunctionNameOfGrabEventArguments;
  const stopPropagation = remoteMethodInfo.StopPropagation;
  const keyboardEventCallOnly = remoteMethodInfo.KeyboardEventCallOnly;
  const debounceTimeout = remoteMethodInfo.DebounceTimeout;
  NotNull(remoteMethodName);
  NotNull(handlerComponentUniqueIdentifier);
  const onClickPreview = parentJsonNode.$onClickPreview;
  let onPreviewHandler = null;
  if (onClickPreview) {
    onPreviewHandler = function() {
      const cmp = GetComponentByDotNetComponentUniqueIdentifier(onClickPreview.$DotNetComponentUniqueIdentifier);
      const newState = CalculateNewStateFromJsonElement(cmp.state, onClickPreview);
      cmp.setState(newState);
    };
  }
  return function() {
    if (stopPropagation) {
      if (arguments.length === 0) {
        throw CreateNewDeveloperError("There is no event argument for applying StopPropagation");
      }
      if (arguments[0] == null) {
        throw CreateNewDeveloperError("Trying to call StopPropagation method bu event argument is null.");
      }
      arguments[0].stopPropagation();
    }
    let eventArguments = Array.prototype.slice.call(arguments);
    if (keyboardEventCallOnly) {
      const key = arguments[0].key;
      if (keyboardEventCallOnly.indexOf(key) < 0) {
        return;
      }
      eventArguments[0].preventDefault();
      eventArguments[0] = ConvertToSyntheticKeyboardEvent(eventArguments[0]);
    }
    const targetComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);
    if (functionNameOfGrabEventArguments) {
      eventArguments = GetExternalJsObject(functionNameOfGrabEventArguments)(eventArguments);
    }
    const cachedMethodInfo = tryToFindCachedMethodInfo(targetComponent, remoteMethodName, eventArguments);
    if (cachedMethodInfo) {
      const newState = CalculateNewStateFromJsonElement(targetComponent.state, cachedMethodInfo.ElementAsJson);
      targetComponent.setState(newState);
      return;
    }
    if (debounceTimeout > 0) {
      const eventName = eventArguments[0]._reactName;
      const operationUniqueName = eventName + "-debounce-" + GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);
      InvalidateQueuedFunctionsByName(operationUniqueName);
      const timeoutKey = eventName + "-debounceTimeoutId";
      clearTimeout(targetComponent.state[timeoutKey]);
      const newState = {};
      newState[timeoutKey] = setTimeout(() => {
        const operation = {
          component: targetComponent,
          remoteMethodName,
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
      const operation = {
        component: targetComponent,
        remoteMethodName,
        remoteMethodArguments: eventArguments,
        onPreviewHandler
      };
      StartNewOperation(operation);
    }
  };
}
function FindRealNodeByFakeChild(fakeChildIndex, rootNodeInState, jsonNodeInProps) {
  if (rootNodeInState && rootNodeInState.$FakeChild === fakeChildIndex) {
    return jsonNodeInProps;
  }
  const childrenInState = rootNodeInState.$children;
  const childrenInProps = jsonNodeInProps.$children;
  if (childrenInState && childrenInProps && childrenInState.length <= childrenInProps.length) {
    const length = childrenInState.length;
    for (let i = 0; i < length; i++) {
      const record = FindRealNodeByFakeChild(fakeChildIndex, childrenInState[i], childrenInProps[i]);
      if (record != null) {
        return record;
      }
    }
  }
  return null;
}
function ConvertToReactElement(jsonNode, component) {
  if (jsonNode == null) {
    return null;
  }
  if (typeof jsonNode === "string") {
    return jsonNode;
  }
  if (jsonNode.$FakeChild != null) {
    jsonNode = FindRealNodeByFakeChild(jsonNode.$FakeChild, component.state[RootNode], component.props.$jsonNode[RootNode]);
  }
  if (jsonNode.$isPureComponent) {
    const cmp = DefinePureComponent(jsonNode);
    const cmpKey = NotNull(jsonNode.key);
    const cmpProps = {
      key: cmpKey,
      $jsonNode: jsonNode,
      $styles: DynamicStyles[jsonNode[DotNetComponentUniqueIdentifier]]
    };
    if (jsonNode.$props) {
      Object.assign(cmpProps, jsonNode.$props);
    }
    return createElement(cmp, cmpProps);
  }
  if (jsonNode[DotNetTypeOfReactComponent]) {
    const cmp = DefineComponent(jsonNode);
    const cmpKey = NotNull(jsonNode.key);
    const cmpProps = {
      key: cmpKey,
      $jsonNode: jsonNode,
      $styles: DynamicStyles[jsonNode[DotNetComponentUniqueIdentifier]]
    };
    const reactAttributeNames = jsonNode.$ReactAttributeNames;
    if (reactAttributeNames) {
      for (let i = 0; i < reactAttributeNames.length; i++) {
        const name = reactAttributeNames[i];
        cmpProps[name] = jsonNode[DotNetProperties][name];
      }
    }
    const currentComponent = COMPONENT_CACHE.FindComponentByDotNetComponentUniqueIdentifier(jsonNode[DotNetComponentUniqueIdentifier]);
    if (currentComponent) {
      const syncIdInState = ShouldBeNumber(currentComponent.state[SyncId]);
      const syncIdInProp = ShouldBeNumber(currentComponent.props[SyncId]);
      cmpProps[SyncId] = Math.max(syncIdInProp, syncIdInState);
      cmpProps.ref = currentComponent.myRef;
      GetFreeSpaceOfComponent(currentComponent).ref = currentComponent.myRef;
    } else {
      cmpProps[SyncId] = GetNextSequence();
    }
    if (jsonNode.$props) {
      Object.assign(cmpProps, jsonNode.$props);
    }
    return createElement(cmp, cmpProps);
  }
  let props = null;
  let elementType = jsonNode.$tag;
  if (!elementType) {
    throw CreateNewDeveloperError("ReactNode is not recognized");
  }
  let isThirdPartyComponent = false;
  if (
    /* is component */
    elementType.indexOf(".") > 0
  ) {
    Before3rdPartyComponentAccess(elementType);
    elementType = GetExternalJsObject(elementType);
    isThirdPartyComponent = true;
  }
  if (elementType === "nbsp") {
    if (jsonNode && jsonNode.length) {
      return Array(jsonNode.length).fill("\xA0").join("");
    }
    return "\xA0";
  }
  for (let propName in jsonNode) {
    if (!jsonNode.hasOwnProperty(propName)) {
      continue;
    }
    if (propName[0] === "$") {
      continue;
    }
    if (props === null) {
      props = {};
    }
    const propValue = jsonNode[propName];
    if (propValue != null) {
      if (propValue.$isRemoteMethod) {
        props[propName] = ConvertToEventHandlerFunction(jsonNode, propValue);
        continue;
      }
      if (propValue.$isBinding) {
        const targetProp = propValue.targetProp;
        const eventName = propValue.eventName;
        const sourcePath = propValue.sourcePath;
        const sourceIsState = propValue.sourceIsState;
        const debounceTimeout = propValue.DebounceTimeout;
        const debounceHandler = propValue.DebounceHandler;
        const jsValueAccess = propValue.jsValueAccess;
        const transformFunction = GetExternalJsObject(IfNull(propValue.transformFunction, "ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull"));
        const handlerComponentUniqueIdentifier = propValue.HandlerComponentUniqueIdentifier;
        let accessToSource = DotNetProperties;
        if (sourceIsState) {
          accessToSource = DotNetState;
        }
        props[targetProp] = transformFunction(GetValueInPath(GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier).state[accessToSource], sourcePath));
        props[eventName] = function(e) {
          const targetComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);
          const modifiedDotNetState = Clone(targetComponent.state[accessToSource]);
          SetValueInPath(modifiedDotNetState, sourcePath, transformFunction(GetValueInPath({ e }, jsValueAccess)));
          const newState = {};
          newState[accessToSource] = modifiedDotNetState;
          if (debounceTimeout > 0) {
            const operationUniqueName = eventName + "-debounce-" + GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);
            InvalidateQueuedFunctionsByName(operationUniqueName);
            const timeoutKey = eventName + "-debounceTimeoutId";
            clearTimeout(targetComponent.state[timeoutKey]);
            newState[timeoutKey] = setTimeout(() => {
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
        };
        continue;
      }
      if (propValue.$isElement) {
        props[propName] = ConvertToReactElement(propValue.Element, component);
        continue;
      }
      const itemTemplates = propValue.___ItemTemplates___;
      const itemTemplateForNull = propValue.___TemplateForNull___;
      if (itemTemplates) {
        props[propName] = function(item) {
          if (import_react.default.isValidElement(item)) {
            return item;
          }
          if (item == null && itemTemplateForNull) {
            return ConvertToReactElement(itemTemplateForNull);
          }
          const length = itemTemplates.length;
          {
            for (let j = 0; j < length; j++) {
              const itemInTemplateInfo = itemTemplates[j].Item;
              const jsonNodeInTemplateInfo = itemTemplates[j].ElementAsJson;
              if (item && item.key != null && itemInTemplateInfo.key != null && itemInTemplateInfo.key === item.key) {
                return ConvertToReactElement(jsonNodeInTemplateInfo);
              }
            }
          }
          {
            for (let j = 0; j < length; j++) {
              const itemInTemplateInfo = itemTemplates[j].Item;
              const jsonNodeInTemplateInfo = itemTemplates[j].ElementAsJson;
              if (JSON.stringify(itemInTemplateInfo) === JSON.stringify(item)) {
                return ConvertToReactElement(jsonNodeInTemplateInfo);
              }
            }
          }
          throw CreateNewDeveloperError("item template not found");
        };
        continue;
      }
      if (propValue.$transformValueFunction) {
        props[propName] = GetExternalJsObject(propValue.$transformValueFunction)(propValue.RawValue);
        continue;
      }
    }
    props[propName] = jsonNode[propName];
  }
  if (isThirdPartyComponent === true) {
    props = OnThirdPartyComponentPropsCalculatedTryFire(jsonNode.$tag, props, component);
  }
  if (jsonNode.$text != null) {
    return createElement(elementType, props, jsonNode.$text);
  }
  const children = jsonNode.$children;
  if (children) {
    const childrenLength = children.length;
    if (childrenLength === 1) {
      return createElement(elementType, props, ConvertToReactElement(children[0], component));
    }
    const newChildren = [];
    for (let childIndex = 0; childIndex < childrenLength; childIndex++) {
      newChildren.push(ConvertToReactElement(children[childIndex], component));
    }
    return createElement(elementType, props, newChildren);
  }
  return createElement(elementType, props);
}
function IsSerializablePrimitiveJsValue(value) {
  const typeofValue = typeof value;
  return typeofValue === "string" || typeofValue === "number" || typeofValue === "boolean" || value instanceof Date || value instanceof Array;
}
function ConvertToSyntheticMouseEvent(e) {
  return {
    altKey: e.altKey,
    bubbles: e.bubbles,
    clientX: e.clientX,
    clientY: e.clientY,
    ctrlKey: e.ctrlKey,
    metaKey: e.metaKey,
    movementX: e.movementX,
    movementY: e.movementY,
    pageX: e.pageX,
    pageY: e.pageY,
    screenX: e.screenX,
    screenY: e.screenY,
    shiftKey: e.shiftKey,
    timeStamp: e.timeStamp,
    type: e.type,
    target: ConvertToShadowHtmlElement(e.target),
    currentTarget: ConvertToShadowHtmlElement(e.currentTarget)
  };
}
function ConvertToSyntheticScrollEvent(e) {
  return {
    currentTarget: ConvertToShadowHtmlElement(e.currentTarget),
    target: ConvertToShadowHtmlElement(e.target),
    timeStamp: e.timeStamp,
    type: e.type
  };
}
function ConvertToSyntheticKeyboardEvent(e) {
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
function ConvertToSyntheticChangeEvent(e) {
  const target = ConvertToShadowHtmlElement(e.target);
  if (e._reactName === "onInput") {
    target.textContent = e.target.textContent;
  }
  return {
    bubbles: e.bubbles,
    target,
    timeStamp: e.timeStamp,
    type: e.type
  };
}
function ConvertToShadowHtmlElement(htmlElement) {
  if (htmlElement == null) {
    return null;
  }
  let value = null;
  if (htmlElement.value != null) {
    if (typeof htmlElement.value === "string") {
      value = htmlElement.value;
    }
  }
  let selectedIndex = null;
  if (htmlElement.selectedIndex != null) {
    if (typeof htmlElement.selectedIndex === "number") {
      selectedIndex = htmlElement.selectedIndex;
    }
  }
  let selectionStart = null;
  if (htmlElement.selectionStart != null) {
    if (typeof htmlElement.selectionStart === "number") {
      selectionStart = htmlElement.selectionStart;
    }
  }
  return {
    id: htmlElement.id,
    offsetHeight: htmlElement.offsetHeight,
    offsetLeft: htmlElement.offsetLeft,
    offsetTop: htmlElement.offsetTop,
    offsetWidth: htmlElement.offsetWidth,
    selectedIndex,
    selectionStart,
    tagName: htmlElement.tagName,
    value,
    data: htmlElement.dataset,
    scrollHeight: htmlElement.scrollHeight,
    scrollLeft: htmlElement.scrollLeft,
    scrollTop: htmlElement.scrollTop,
    scrollWidth: htmlElement.scrollWidth
  };
}
function ProcessClientTasks(clientTasks, callerInstance) {
  if (clientTasks == null) {
    return;
  }
  const length = clientTasks.length;
  for (let i = 0; i < length; i++) {
    const jsFunctionPath = clientTasks[i].JsFunctionPath;
    const jsFunctionArguments = clientTasks[i].JsFunctionArguments;
    InvokeJsFunctionInPath(jsFunctionPath, callerInstance, jsFunctionArguments);
  }
}
function IsSyntheticBaseEvent(e) {
  try {
    return e.constructor.prototype.constructor.name === "SyntheticBaseEvent";
  } catch (exception) {
    return false;
  }
}
function ArrangeRemoteMethodArguments(remoteMethodArguments) {
  if (remoteMethodArguments) {
    for (let i = 0; i < remoteMethodArguments.length; i++) {
      const prm = remoteMethodArguments[i];
      if (IsSyntheticBaseEvent(prm)) {
        const reactName = prm._reactName;
        if (reactName) {
          if (reactName.indexOf("Mouse") > 0) {
            remoteMethodArguments[i] = ConvertToSyntheticMouseEvent(prm);
          } else if (reactName === "onScroll") {
            remoteMethodArguments[i] = ConvertToSyntheticScrollEvent(prm);
          }
        }
      }
    }
  }
}
function CallRemote(operation) {
  const remoteMethodName = operation.remoteMethodName;
  const isComponentWillUnmount = operation.isComponentWillUnmount;
  let component = NotNull(operation.component);
  if (component.ComponentWillUnmountIsCalled) {
    operation.status = OperationStatusReactStateReady;
    return;
  }
  component = GetComponentByDotNetComponentUniqueIdentifier(component[DotNetComponentUniqueIdentifiers][0]);
  if (component._reactInternals == null) {
    throw CreateNewDeveloperError("Component is not ready to send server.");
  }
  const isComponentPreview = component[DotNetTypeOfReactComponent] === "ReactWithDotNet.UIDesigner.ReactWithDotNetDesignerComponentPreview,ReactWithDotNet";
  let capturedStateTree, capturedStateTreeRootNodeKey;
  if (isComponentWillUnmount) {
    capturedStateTree = operation.capturedStateTree;
    capturedStateTreeRootNodeKey = operation.capturedStateTreeRootNodeKey;
  } else {
    const capturedStateTreeResponse = SafeExecute(() => CaptureStateTreeFromFiberNode(component._reactInternals));
    if (capturedStateTreeResponse.fail) {
      if (isComponentPreview) {
        location.reload();
      }
      throw capturedStateTreeResponse.exception;
    }
    capturedStateTree = capturedStateTreeResponse.value.stateTree;
    capturedStateTreeRootNodeKey = capturedStateTreeResponse.value.rootNodeKey;
  }
  const request = {
    MethodName: "HandleComponentEvent",
    EventHandlerMethodName: NotNull(remoteMethodName),
    FullName: NotNull(component.constructor)[DotNetTypeOfReactComponent],
    CapturedStateTree: capturedStateTree,
    CapturedStateTreeRootNodeKey: capturedStateTreeRootNodeKey,
    ComponentKey: parseInt(NotNull(component.props.$jsonNode.key)),
    LastUsedComponentUniqueIdentifier,
    ComponentUniqueIdentifier: NotNull(component.state[DotNetComponentUniqueIdentifier]),
    CallFunctionId: operation.id
  };
  ArrangeRemoteMethodArguments(operation.remoteMethodArguments);
  {
    const remoteMethodArguments = Array.from(operation.remoteMethodArguments);
    const length = remoteMethodArguments.length;
    for (let i = 0; i < length; i++) {
      remoteMethodArguments[i] = JSON.stringify(remoteMethodArguments[i]);
    }
    request.EventArgumentsAsJsonArray = remoteMethodArguments;
  }
  function onSuccess(response) {
    if (operation.status === OperationStatusInvalidated) {
      return;
    }
    if (response.ErrorMessage != null) {
      throw CreateNewDeveloperError(response.ErrorMessage);
    }
    if (response.LastUsedComponentUniqueIdentifier > LastUsedComponentUniqueIdentifier) {
      LastUsedComponentUniqueIdentifier = response.LastUsedComponentUniqueIdentifier;
    }
    ProcessDynamicCssClasses(response.DynamicStyles);
    if (response.SkipRender) {
      component.state[DotNetState] = response.NewState;
      component.state[DotNetProperties] = response.NewDotNetProperties;
      if (response.ClientTaskList) {
        component.state[ClientTasks] = response.ClientTaskList;
        HandleComponentClientTasks(component);
      }
      operation.status = OperationStatusReactStateReady;
      return;
    }
    const partialState = CalculateNewStateFromJsonElement(component.state, response.ElementAsJson);
    SetState(component, partialState, () => {
      operation.status = OperationStatusReactStateReady;
    });
    if (isComponentWillUnmount) {
      ProcessClientTasks(partialState[ClientTasks], component);
      operation.status = OperationStatusReactStateReady;
    }
  }
  function onFail(error) {
    if (isComponentPreview) {
      setTimeout(() => SendRequest(request, onSuccess, onFail), 1e3);
      return;
    }
    console.error(error);
    operation.status = OperationStatusRemoteFail;
  }
  if (operation.onPreviewHandler) {
    operation.onPreviewHandler();
  }
  operation.status = OperationStatusWaitingRemoteResponse;
  SendRequest(request, onSuccess, onFail);
}
function CalculateNewStateFromJsonElement(componentState, jsonElement) {
  if (NotNull(componentState[DotNetComponentUniqueIdentifier]) !== NotNull(jsonElement[DotNetComponentUniqueIdentifier])) {
    const component = COMPONENT_CACHE.FindComponentByDotNetComponentUniqueIdentifier(componentState[DotNetComponentUniqueIdentifier]);
    if (component) {
      component[DotNetComponentUniqueIdentifiers].push(jsonElement[DotNetComponentUniqueIdentifier]);
    }
  }
  jsonElement[SyncId] = GetNextSequence();
  return jsonElement;
}
var ComponentDefinitions = {};
function InvokeComponentDestroyListeners(componentInstance) {
  const functionArray = GetFreeSpaceOfComponent(componentInstance)[ON_COMPONENT_DESTROY];
  for (let i = 0; i < functionArray.length; i++) {
    functionArray[i]();
  }
}
function OnComponentDestroy(component, fn) {
  GetFreeSpaceOfComponent(component)[ON_COMPONENT_DESTROY].push(fn);
}
function DestroyDotNetComponentInstance(instance) {
  InvokeComponentDestroyListeners(instance);
  RemoveComponentDynamicStyles(instance[DotNetComponentUniqueIdentifiers]);
  COMPONENT_CACHE.Unregister(instance);
}
function HandleComponentClientTasks(component) {
  const clientTasks = component.state[ClientTasks];
  if (clientTasks == null || clientTasks.length === 0 || component.ComponentWillUnmountIsCalled === true) {
    return false;
  }
  const freeSpace = GetFreeSpaceOfComponent(component);
  if (freeSpace.lastProcessedClientTasks === clientTasks) {
    return false;
  }
  freeSpace.lastProcessedClientTasks = clientTasks;
  ProcessClientTasks(clientTasks, component);
  return true;
}
function DefineComponent(componentDeclaration) {
  let cacheKeyForComponentDefinitions = componentDeclaration[DotNetTypeOfReactComponent];
  if (cacheKeyForComponentDefinitions === "ReactWithDotNet.FunctionalComponent,ReactWithDotNet") {
    cacheKeyForComponentDefinitions = componentDeclaration[DotNetProperties]["RenderMethodNameWithToken"];
  }
  const component = ComponentDefinitions[cacheKeyForComponentDefinitions];
  if (component) {
    return component;
  }
  const dotNetTypeOfReactComponent = componentDeclaration[DotNetTypeOfReactComponent];
  class NewComponent extends import_react.default.Component {
    constructor(props) {
      super(props || {});
      this.myRef = import_react.default.createRef();
      const instance = this;
      const initialState = {};
      if (props) {
        Object.assign(initialState, props.$jsonNode);
        initialState[SyncId] = ShouldBeNumber(props[SyncId]);
      }
      instance.state = initialState;
      initialState[DotNetTypeOfReactComponent] = instance[DotNetTypeOfReactComponent] = dotNetTypeOfReactComponent;
      instance[DotNetComponentUniqueIdentifiers] = [NotNull(props.$jsonNode[DotNetComponentUniqueIdentifier])];
      InitializeDotNetComponentInstanceId(instance);
      COMPONENT_CACHE.Register(instance);
    }
    render() {
      const props = this.props;
      {
        const styles = props.$styles;
        if (styles) {
          const cuid = props.$jsonNode[DotNetComponentUniqueIdentifier];
          if (DynamicStyles[cuid] !== styles) {
            DynamicStyles[cuid] = styles;
            ApplyDynamicStylesToDom();
          }
        }
      }
      return ConvertToReactElement(this.state[RootNode], this);
    }
    componentDidMount() {
      this.state["$didMount"] = 1;
      const component2 = this;
      HandleComponentClientTasks(this);
      const componentDidMountMethod = component2.state[ComponentDidMountMethod];
      if (componentDidMountMethod == null) {
        return;
      }
      {
        const cachedMethodInfo = tryToFindCachedMethodInfo(component2, "componentDidMount", []);
        if (cachedMethodInfo) {
          const newState = CalculateNewStateFromJsonElement(component2.state, cachedMethodInfo.ElementAsJson);
          const clientTasks = newState[ClientTasks];
          newState[ComponentDidMountMethod] = null;
          newState[ClientTasks] = null;
          SetState(component2, newState, () => {
            ProcessClientTasks(clientTasks, component2);
          });
          return;
        }
      }
      const partialState = {};
      partialState[ComponentDidMountMethod] = null;
      SetState(component2, partialState, () => {
        const operation = {
          component: (
            /** @type {Component}*/
            component2
          ),
          remoteMethodName: componentDidMountMethod,
          remoteMethodArguments: []
        };
        StartNewOperation(operation);
      });
    }
    componentDidUpdate(prevProps, prevState, snapshot) {
      HandleComponentClientTasks(this);
    }
    componentWillUnmount() {
      if (!this.state["$isUnmounting"]) {
        this.setState({ $isUnmounting: 1 });
        if (!this.state["$didMount"]) {
          return;
        }
        if (this.ComponentWillUnmountIsCalled === true) {
          throw "componentWillUnmount -> ComponentWillUnmountIsCalled called twice";
        }
        const component2 = this;
        const componentWillUnmountMethod = component2.state[ComponentWillUnmountMethod];
        if (componentWillUnmountMethod == null) {
          component2.ComponentWillUnmountIsCalled = true;
          DestroyDotNetComponentInstance(component2);
          return;
        }
        {
          const cachedMethodInfo = tryToFindCachedMethodInfo(component2, "componentWillUnmount", []);
          if (cachedMethodInfo) {
            let stateCallback = function() {
              ProcessClientTasks(clientTasks, component2);
              component2.ComponentWillUnmountIsCalled = true;
              DestroyDotNetComponentInstance(component2);
            };
            const newState = CalculateNewStateFromJsonElement(component2.state, cachedMethodInfo.ElementAsJson);
            const clientTasks = newState[ClientTasks];
            newState[ComponentDidMountMethod] = null;
            newState[ClientTasks] = null;
            SetState(component2, newState, stateCallback);
            return;
          }
        }
        {
          {
            const capturedStateTreeResponse = SafeExecute(() => CaptureStateTreeFromFiberNode(component2._reactInternals));
            if (capturedStateTreeResponse.fail) {
              throw capturedStateTreeResponse.exception;
            }
            const capturedStateTree = capturedStateTreeResponse.value.stateTree;
            const capturedStateTreeRootNodeKey = capturedStateTreeResponse.value.rootNodeKey;
            const operation = {
              component: (
                /** @type {Component}*/
                component2
              ),
              remoteMethodName: componentWillUnmountMethod,
              remoteMethodArguments: [],
              isComponentWillUnmount: 1,
              capturedStateTree,
              capturedStateTreeRootNodeKey,
              onCompleted: () => {
                component2.ComponentWillUnmountIsCalled = true;
                DestroyDotNetComponentInstance(component2);
              }
            };
            StartNewOperation(operation);
          }
        }
      }
    }
    static getDerivedStateFromProps(nextProps, prevState) {
      const syncIdInState = ShouldBeNumber(prevState[SyncId]);
      const syncIdInProp = ShouldBeNumber(nextProps[SyncId]);
      if (syncIdInState > syncIdInProp) {
        const componentActiveUniqueIdentifier = NotNull(prevState[DotNetComponentUniqueIdentifier]);
        const componentNextUniqueIdentifier = NotNull(nextProps.$jsonNode[DotNetComponentUniqueIdentifier]);
        if (componentActiveUniqueIdentifier !== componentNextUniqueIdentifier) {
          const component2 = GetComponentByDotNetComponentUniqueIdentifier(componentActiveUniqueIdentifier);
          component2[DotNetComponentUniqueIdentifiers].push(componentNextUniqueIdentifier);
          const partialState = {};
          partialState[DotNetComponentUniqueIdentifier] = componentNextUniqueIdentifier;
          return partialState;
        }
        return null;
      }
      if (syncIdInState < syncIdInProp) {
        const partialState = {};
        partialState[SyncId] = syncIdInProp;
        partialState[RootNode] = nextProps.$jsonNode[RootNode];
        partialState[ClientTasks] = nextProps.$jsonNode[ClientTasks];
        partialState[DotNetProperties] = NotNull(nextProps.$jsonNode[DotNetProperties]);
        partialState[DotNetState] = NotNull(nextProps.$jsonNode[DotNetState]);
        const componentActiveUniqueIdentifier = NotNull(prevState[DotNetComponentUniqueIdentifier]);
        const componentNextUniqueIdentifier = NotNull(nextProps.$jsonNode[DotNetComponentUniqueIdentifier]);
        if (componentActiveUniqueIdentifier !== componentNextUniqueIdentifier) {
          const component2 = GetComponentByDotNetComponentUniqueIdentifier(componentActiveUniqueIdentifier);
          component2[DotNetComponentUniqueIdentifiers].push(componentNextUniqueIdentifier);
        }
        partialState[DotNetComponentUniqueIdentifier] = componentNextUniqueIdentifier;
        return partialState;
      }
      return null;
    }
  }
  NewComponent[DotNetTypeOfReactComponent] = dotNetTypeOfReactComponent;
  ComponentDefinitions[cacheKeyForComponentDefinitions] = NewComponent;
  NewComponent.displayName = dotNetTypeOfReactComponent.split(",")[0].split(".").pop();
  if (NewComponent.displayName === "FunctionalComponent") {
    NewComponent.displayName = componentDeclaration[DotNetProperties].Name;
  }
  return NewComponent;
}
function DefinePureComponent(componentDeclaration) {
  const dotNetTypeOfReactComponent = componentDeclaration[DotNetTypeOfReactComponent];
  const component = ComponentDefinitions[dotNetTypeOfReactComponent];
  if (component) {
    return component;
  }
  class NewPureComponent extends import_react.default.PureComponent {
    render() {
      const props = this.props;
      {
        const styles = props["$styles"];
        if (styles) {
          const cuid = props["$jsonNode"][DotNetComponentUniqueIdentifier];
          if (DynamicStyles[cuid] !== styles) {
            DynamicStyles[cuid] = styles;
            ApplyDynamicStylesToDom();
          }
        }
      }
      return ConvertToReactElement(props["$jsonNode"][RootNode], this);
    }
    componentWillUnmount() {
      const uid = NotNull(this.props["$jsonNode"][DotNetComponentUniqueIdentifier]);
      RemoveComponentDynamicStyles([uid]);
    }
  }
  ComponentDefinitions[dotNetTypeOfReactComponent] = NewPureComponent;
  NewPureComponent.displayName = dotNetTypeOfReactComponent.split(",")[0].split(".").pop();
  return NewPureComponent;
}
function SendRequest(request, onSuccess, onFail) {
  request.ClientWidth = document.documentElement.clientWidth;
  request.ClientHeight = document.documentElement.clientHeight;
  request.QueryString = window.location.search;
  const url = ReactWithDotNet.RequestHandlerPath;
  let options = {
    method: "POST",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(request)
  };
  if (ReactWithDotNet.BeforeSendRequest) {
    ReactWithDotNet.BeforeSendRequest(options);
  }
  window.fetch(url, options).then((response) => response.json()).then((json) => onSuccess(json)).catch(onFail);
}
var LastUsedComponentUniqueIdentifier = 1;
function ConnectComponentFirstResponseToReactSystem(containerHtmlElementId, response) {
  if (response.ErrorMessage != null) {
    throw response.ErrorMessage;
  }
  ProcessDynamicCssClasses(response.DynamicStyles);
  const element = response.ElementAsJson;
  const component = element.$isPureComponent ? DefinePureComponent(element) : DefineComponent(element);
  LastUsedComponentUniqueIdentifier = response.LastUsedComponentUniqueIdentifier;
  const props = { key: "0", $jsonNode: element };
  props[SyncId] = GetNextSequence();
  const reactElement = createElement(component, props);
  const root = (0, import_client.createRoot)(document.getElementById(containerHtmlElementId));
  if (ReactWithDotNet.StrictMode) {
    root.render(createElement(import_react.default.StrictMode, null, reactElement));
  } else {
    root.render(reactElement);
  }
}
function RenderComponentIn(input) {
  if (input.renderInfo) {
    if (input.idOfContainerHtmlElement == null) {
      throw CreateNewDeveloperError("idOfContainerHtmlElement cannot be null.");
    }
    setTimeout(() => ConnectComponentFirstResponseToReactSystem(input.idOfContainerHtmlElement, input.renderInfo), 10);
    return;
  }
  const fullTypeNameOfReactComponent = input.fullTypeNameOfReactComponent;
  const containerHtmlElementId = input.containerHtmlElementId;
  OnDocumentReady(function() {
    const request = {
      MethodName: "FetchComponent",
      FullName: fullTypeNameOfReactComponent,
      LastUsedComponentUniqueIdentifier,
      ComponentUniqueIdentifier: 1
    };
    function onSuccess(response) {
      ConnectComponentFirstResponseToReactSystem(containerHtmlElementId, response);
    }
    function onFail(error) {
      throw error;
    }
    SendRequest(request, onSuccess, onFail);
  });
}
function InvokeJsFunctionInPath(jsFunctionPath, callerInstance, jsFunctionArguments) {
  const fn = GetExternalJsObject(jsFunctionPath);
  return fn.apply(callerInstance, jsFunctionArguments);
}
var ExternalJsObjectMap = {
  "React.Fragment": import_react.default.Fragment
};
function RegisterExternalJsObject(key, value) {
  if (ExternalJsObjectMap[key] != null) {
    console.log(key + " already registered.");
  }
  return ExternalJsObjectMap[key] = value;
}
function GetExternalJsObject(key) {
  const findResponse = TryFindExternalObject(key);
  if (findResponse != null) {
    if (findResponse["isCacheEnabled"] === true) {
      if (findResponse.value == null) {
        throw CreateNewDeveloperError(key + " ==> isCacheEnabled is true but value property is null.");
      }
      RegisterExternalJsObject(key, findResponse.value);
      return findResponse.value;
    }
    return findResponse;
  }
  const value = ExternalJsObjectMap[key];
  if (value == null) {
    throw CreateNewDeveloperError(key + " External js object not not found. You should register by using method: ReactWithDotNet.RegisterExternalJsObject");
  }
  return value;
}
var FindExternalObjectFnList = [];
function OnFindExternalObject(fn) {
  FindExternalObjectFnList.push(fn);
}
function TryFindExternalObject(name) {
  const items = FindExternalObjectFnList;
  const length = items.length;
  for (let i = 0; i < length; i++) {
    const response = items[i](name);
    if (response == null) {
      continue;
    }
    return response;
  }
}
function RegisterCoreFunction(name, fn) {
  RegisterExternalJsObject("ReactWithDotNet::Core::" + name, fn);
}
ExternalJsObjectMap["ReactWithDotNet.GetExternalJsObject"] = GetExternalJsObject;
RegisterCoreFunction("RegExp", (x) => new RegExp(x));
RegisterCoreFunction("IsTwoObjectEquals", isEquals);
RegisterCoreFunction("CopyToClipboard", function(text) {
  if (navigator.clipboard && navigator.clipboard.writeText) {
    navigator.clipboard.writeText(text).then(() => {
    });
    return;
  }
  if (window.clipboardData && window.clipboardData.setData) {
    return window.clipboardData.setData("Text", text);
  }
});
RegisterCoreFunction("RunJavascript", (javascriptCode) => {
  window.eval(javascriptCode);
});
RegisterCoreFunction("ReplaceNullWhenEmpty", function(value) {
  if (IsEmptyObject(value)) {
    return null;
  }
  return value;
});
RegisterCoreFunction("EmptyTransformFunction", function(value) {
  return value;
});
RegisterCoreFunction("ReplaceEmptyStringWhenIsNull", function(value) {
  if (value == null) {
    return "";
  }
  return value;
});
RegisterCoreFunction("ListenWindowResizeEvent", function(resizeTimeout) {
  let timeout = null;
  window.addEventListener("resize", function() {
    clearTimeout(timeout);
    timeout = setTimeout(function() {
      DispatchEvent("ReactWithDotNet::Core::OnWindowResize", [], 0);
    }, resizeTimeout);
  });
});
RegisterCoreFunction("ConvertDotnetSerializedStringDateToJsDate", function(dotnetDateAsJsonString) {
  if (dotnetDateAsJsonString == null || dotnetDateAsJsonString === "") {
    return null;
  }
  return new Date(dotnetDateAsJsonString);
});
RegisterCoreFunction("CalculateSyntheticMouseEventArguments", (argumentsAsArray) => [ConvertToSyntheticMouseEvent(argumentsAsArray[0])]);
RegisterCoreFunction("CalculateSyntheticChangeEventArguments", (argumentsAsArray) => [ConvertToSyntheticChangeEvent(argumentsAsArray[0])]);
RegisterCoreFunction("CalculateSyntheticFocusEventArguments", (argumentsAsArray) => {
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
function CalculateRemoteMethodArgument(arg) {
  return arg;
}
function CalculateRemoteMethodArguments(args) {
  if (args == null) {
    return null;
  }
  const newArgs = [];
  for (let i = 0; i < args.length; i++) {
    newArgs.push(CalculateRemoteMethodArgument(args[i]));
  }
  return newArgs;
}
RegisterCoreFunction("CalculateRemoteMethodArguments", CalculateRemoteMethodArguments);
function SetCookie(cookieName_StringNotNull, cookieValue_StringNotNull, expireDays_NumberNotNull) {
  const expireDate = /* @__PURE__ */ new Date();
  expireDate.setDate(expireDate.getDate() + expireDays_NumberNotNull);
  document.cookie = [
    cookieName_StringNotNull + "=" + encodeURI(cookieValue_StringNotNull),
    "expires=" + expireDate.toUTCString(),
    "path=/"
  ].join(";");
}
RegisterCoreFunction("SetCookie", SetCookie);
RegisterCoreFunction("HistoryBack", function() {
  window.history.back();
});
RegisterCoreFunction("HistoryForward", function() {
  window.history.forward();
});
RegisterCoreFunction("HistoryGo", function(delta) {
  window.history.go(delta);
});
RegisterCoreFunction("HistoryReplaceState", function(stateObj, title, url) {
  if (stateObj == null) {
    stateObj = {};
  }
  window.history.replaceState(stateObj, title, url);
});
RegisterCoreFunction("GotoMethod", function(timeout, remoteMethodName, remoteMethodArguments) {
  const component = this;
  remoteMethodArguments = remoteMethodArguments || [];
  setTimeout(() => {
    if (component.ComponentWillUnmountIsCalled) {
      return;
    }
    const cachedMethodInfo = tryToFindCachedMethodInfo(component, remoteMethodName, remoteMethodArguments);
    if (cachedMethodInfo) {
      const newState = CalculateNewStateFromJsonElement(component.state, cachedMethodInfo.ElementAsJson);
      component.setState(newState);
      return;
    }
    const operation = {
      component,
      remoteMethodName,
      remoteMethodArguments
    };
    StartNewOperation(operation);
  }, timeout);
});
function DispatchEvent(eventName, eventArguments, timeout) {
  setTimeout(() => {
    EventBus.Dispatch(eventName, eventArguments || []);
  }, timeout);
}
RegisterCoreFunction("DispatchEvent", DispatchEvent);
function GetRealNameOfDotNetEvent(senderPropertyFullName, senderComponentUniqueIdentifier) {
  return [
    "senderPropertyFullName:" + senderPropertyFullName,
    "senderComponentUniqueIdentifier:" + senderComponentUniqueIdentifier
  ].join(",");
}
RegisterCoreFunction("DispatchDotNetCustomEvent", function(eventSenderInfo, eventArguments) {
  const senderPropertyFullName = eventSenderInfo.SenderPropertyFullName;
  const senderComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(eventSenderInfo.SenderComponentUniqueIdentifier);
  const eventName = GetRealNameOfDotNetEvent(senderPropertyFullName, senderComponentUniqueIdentifier);
  EventBus.Dispatch(eventName, eventArguments);
});
RegisterCoreFunction("ListenEvent", function(eventName, remoteMethodName) {
  const component = this;
  const onEventFired = (eventArgumentsAsArray) => {
    if (component.ComponentWillUnmountIsCalled) {
      return;
    }
    const operation = {
      component,
      remoteMethodName,
      remoteMethodArguments: eventArgumentsAsArray,
      priorityIsMore: 1
    };
    StartNewOperation(operation);
    OnComponentDestroy(component, () => {
      operation.status = OperationStatusInvalidated;
    });
  };
  OnComponentDestroy(component, () => {
    EventBus.Remove(eventName, onEventFired);
  });
  EventBus.On(eventName, onEventFired);
});
RegisterCoreFunction("ListenEventOnlyOnce", function(eventName, remoteMethodName) {
  const component = this;
  const onEventFired = (eventArgumentsAsArray) => {
    EventBus.Remove(eventName, onEventFired);
    const operation = {
      component,
      remoteMethodName,
      remoteMethodArguments: eventArgumentsAsArray,
      priorityIsMore: 1
    };
    StartNewOperation(operation);
    OnComponentDestroy(component, () => {
      operation.status = OperationStatusInvalidated;
    });
  };
  OnComponentDestroy(component, () => {
    EventBus.Remove(eventName, onEventFired);
  });
  EventBus.On(eventName, onEventFired);
});
RegisterCoreFunction("InitializeDotnetComponentEventListener", function(eventSenderInfo, remoteMethodName, handlerComponentUniqueIdentifier) {
  const component = this;
  if (component.ComponentWillUnmountIsCalled) {
    return;
  }
  const map = GetFreeSpaceOfComponent(component)[CUSTOM_EVENT_LISTENER_MAP];
  const senderPropertyFullName = eventSenderInfo.SenderPropertyFullName;
  const senderComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(eventSenderInfo.SenderComponentUniqueIdentifier);
  handlerComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);
  const onEventFired = (eventArgumentsAsArray) => {
    const handlerComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);
    const operation = {
      component: handlerComponent,
      remoteMethodName,
      remoteMethodArguments: eventArgumentsAsArray,
      priorityIsMore: 1
    };
    StartNewOperation(operation);
    OnComponentDestroy(handlerComponent, () => {
      operation.status = OperationStatusInvalidated;
    });
  };
  const key = [
    "senderPropertyFullName:" + senderPropertyFullName,
    "senderComponentUniqueIdentifier:" + senderComponentUniqueIdentifier,
    "handlerComponentUniqueIdentifier:" + handlerComponentUniqueIdentifier
  ].join(",");
  if (map[key]) {
    return;
  }
  map[key] = onEventFired;
  const eventName = GetRealNameOfDotNetEvent(senderPropertyFullName, senderComponentUniqueIdentifier);
  OnComponentDestroy(component, () => {
    EventBus.Remove(eventName, onEventFired);
    map[key] = null;
  });
  EventBus.On(eventName, onEventFired);
});
function NavigateTo(path) {
  const location2 = window.location;
  location2.assign(location2.origin + path);
}
RegisterCoreFunction("NavigateTo", NavigateTo);
function OnOutsideClicked(component, operationType, idOfElement, remoteMethodName, handlerComponentUniqueIdentifier) {
  const map = GetFreeSpaceOfComponent(component)[CUSTOM_EVENT_LISTENER_MAP];
  const isRemove = operationType === "remove";
  handlerComponentUniqueIdentifier = GetFirstAssignedUniqueIdentifierValueOfComponent(handlerComponentUniqueIdentifier);
  function onDocumentClick(e) {
    const element = document.getElementById(idOfElement);
    if (element == null) {
      return;
    }
    const isClickedOutside = !element.contains(e.target);
    if (isClickedOutside) {
      const handlerComponent = GetComponentByDotNetComponentUniqueIdentifier(handlerComponentUniqueIdentifier);
      const operation = {
        component: handlerComponent,
        remoteMethodName,
        remoteMethodArguments: [ConvertToSyntheticMouseEvent(e)]
      };
      StartNewOperation(operation);
    }
  }
  const key = "OnOutsideClicked(IdOfElement:" + idOfElement + ", remoteMethodName:" + remoteMethodName + ", @handlerComponentUniqueIdentifier:" + handlerComponentUniqueIdentifier + ")";
  if (map[key]) {
    if (isRemove) {
      document.removeEventListener("click", map[key]);
      map[key] = null;
    }
    return;
  }
  if (isRemove) {
    return;
  }
  map[key] = onDocumentClick;
  document.addEventListener("click", onDocumentClick);
  OnComponentDestroy(component, () => {
    document.removeEventListener("click", onDocumentClick);
    map[key] = null;
  });
}
RegisterCoreFunction("AddEventListener", function(idOfElement, eventName, remoteMethodName, handlerComponentUniqueIdentifier) {
  if (eventName === "OutsideClick") {
    OnOutsideClicked(this, "add", idOfElement, remoteMethodName, handlerComponentUniqueIdentifier);
  } else {
    throw CreateNewDeveloperError("InvalidUsageOfAddEventListener");
  }
});
RegisterCoreFunction("RemoveEventListener", function(idOfElement, eventName, remoteMethodName, handlerComponentUniqueIdentifier) {
  if (eventName === "OutsideClick") {
    OnOutsideClicked(this, "remove", idOfElement, remoteMethodName, handlerComponentUniqueIdentifier);
  } else {
    throw CreateNewDeveloperError("InvalidUsageOfRemoveEventListener");
  }
});
function CreateNewDeveloperError(message) {
  return new Error("\nReactWithDotNet developer error occured.\n" + message);
}
var DynamicStyles = {};
var ReactWithDotNetDynamicCssElement = null;
function ProcessDynamicCssClasses(dynamicStyles) {
  if (dynamicStyles == null) {
    return;
  }
  for (let cuid in dynamicStyles) {
    if (dynamicStyles.hasOwnProperty(cuid)) {
      DynamicStyles[cuid] = dynamicStyles[cuid];
    }
  }
  ApplyDynamicStylesToDom();
}
function ApplyDynamicStylesToDom() {
  if (ReactWithDotNetDynamicCssElement === null) {
    const idOfStyleElement = "ReactWithDotNetDynamicCss";
    ReactWithDotNetDynamicCssElement = document.getElementById(idOfStyleElement);
    if (ReactWithDotNetDynamicCssElement == null) {
      ReactWithDotNetDynamicCssElement = document.createElement("style");
      ReactWithDotNetDynamicCssElement.id = idOfStyleElement;
      document.head.appendChild(ReactWithDotNetDynamicCssElement);
    }
  }
  const arr = [];
  for (let cuid in DynamicStyles) {
    if (DynamicStyles.hasOwnProperty(cuid)) {
      const values = DynamicStyles[cuid];
      if (values == null) {
        continue;
      }
      const length = values.length;
      for (let i = 0; i < length; i++) {
        const items = values[i];
        const name = items[0];
        const body = items[1];
        const mediaQueries = items[2];
        const pseudos = items[3];
        arr.push("." + name);
        arr.push("{");
        arr.push(body);
        arr.push("}");
        if (mediaQueries) {
          const count = mediaQueries.length;
          for (let j = 0; j < count; j++) {
            const mediaRule = mediaQueries[j][0];
            const cssBody = mediaQueries[j][1];
            arr.push("@media " + mediaRule + " {");
            arr.push("  ." + name + " {");
            arr.push(cssBody);
            arr.push("  }");
            arr.push("}");
          }
        }
        if (pseudos) {
          const count = pseudos.length;
          for (let j = 0; j < count; j++) {
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
  const newStyle = arr.join("\n");
  if (!IsTwoStringHasValueAndSame(ReactWithDotNetDynamicCssElement.innerHTML, newStyle)) {
    ReactWithDotNetDynamicCssElement.innerHTML = newStyle;
  }
}
function CloneObjectWithoutNullValues(obj) {
  const clone = {};
  for (let key in obj) {
    if (obj.hasOwnProperty(key)) {
      const value = obj[key];
      if (value != null) {
        clone[key] = obj[key];
      }
    }
  }
  return clone;
}
setInterval(() => {
  DynamicStyles = CloneObjectWithoutNullValues(DynamicStyles);
}, 3e3);
function RemoveComponentDynamicStyles(componentUniqueIdentifiers) {
  let hasChange = false;
  const length = componentUniqueIdentifiers.length;
  for (let i = 0; i < length; i++) {
    const cuid = componentUniqueIdentifiers[i];
    if (DynamicStyles[cuid] != null) {
      hasChange = true;
      DynamicStyles[cuid] = null;
    }
  }
  if (hasChange) {
    ApplyDynamicStylesToDom();
  }
}
function IsTwoStringHasValueAndSame(a, b) {
  if (a == null || b == null) {
    return false;
  }
  const anyWhiteSpaceRegex = /\s/g;
  a = a.replace(anyWhiteSpaceRegex, "");
  b = b.replace(anyWhiteSpaceRegex, "");
  return a === b;
}
function IsMobile() {
  return document.documentElement.clientWidth <= 767;
}
function IsTablet() {
  return document.documentElement.clientWidth <= 1023 && IsMobile() === false;
}
function IsDesktop() {
  return IsMobile() === false && IsTablet() === false;
}
var ReactWithDotNet = {
  StrictMode: false,
  RequestHandlerPath: "DeveloperError: missing RequestHandlerPath",
  "OnDocumentReady": OnDocumentReady,
  "StartAction": StartNewOperation,
  DispatchEvent,
  "RenderComponentIn": RenderComponentIn,
  BeforeSendRequest: (x) => x,
  RegisterExternalJsObject,
  GetExternalJsObject,
  BeforeAnyThirdPartyComponentAccess,
  TryLoadCssByHref,
  OnThirdPartyComponentPropsCalculated,
  OnFindExternalObject,
  IsMediaMobile: IsMobile,
  IsMediaTablet: IsTablet,
  IsMediaDesktop: IsDesktop,
  Call: InvokeJsFunctionInPath,
  Util: {
    SetCookie
  }
};
window.ReactWithDotNet = ReactWithDotNet;
var react_with_dotnet_default = ReactWithDotNet;

export {
  react_with_dotnet_default
};
