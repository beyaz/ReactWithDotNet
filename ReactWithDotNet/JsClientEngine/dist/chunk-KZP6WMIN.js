import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __commonJS,
  __export,
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/invariant/browser.js
var require_browser = __commonJS({
  "node_modules/invariant/browser.js"(exports, module) {
    "use strict";
    var invariant2 = function(condition, format, a, b, c, d, e, f) {
      if (true) {
        if (format === void 0) {
          throw new Error("invariant requires an error message argument");
        }
      }
      if (!condition) {
        var error;
        if (format === void 0) {
          error = new Error(
            "Minified exception occurred; use the non-minified dev environment for the full error message and additional helpful warnings."
          );
        } else {
          var args = [a, b, c, d, e, f];
          var argIndex = 0;
          error = new Error(
            format.replace(/%s/g, function() {
              return args[argIndex++];
            })
          );
          error.name = "Invariant Violation";
        }
        error.framesToPop = 1;
        throw error;
      }
    };
    module.exports = invariant2;
  }
});

// node_modules/@rpldy/shared/lib/esm/utils/isFunction.js
var require_isFunction = __commonJS({
  "node_modules/@rpldy/shared/lib/esm/utils/isFunction.js"(exports, module) {
    function isFunction2(f) {
      return typeof f === "function";
    }
    module.exports = isFunction2;
  }
});

// node_modules/@rpldy/shared/lib/esm/utils/isProduction.js
var require_isProduction = __commonJS({
  "node_modules/@rpldy/shared/lib/esm/utils/isProduction.js"(exports, module) {
    function isProduction3() {
      return false;
    }
    module.exports = isProduction3;
  }
});

// node_modules/@rpldy/shared/lib/esm/index.js
var import_invariant = __toESM(require_browser());

// node_modules/just-throttle/index.mjs
var functionThrottle = throttle;
function throttle(fn, interval, options) {
  var timeoutId = null;
  var throttledFn = null;
  var leading = options && options.leading;
  var trailing = options && options.trailing;
  if (leading == null) {
    leading = true;
  }
  if (trailing == null) {
    trailing = !leading;
  }
  if (leading == true) {
    trailing = false;
  }
  var cancel = function() {
    if (timeoutId) {
      clearTimeout(timeoutId);
      timeoutId = null;
    }
  };
  var flush = function() {
    var call = throttledFn;
    cancel();
    if (call) {
      call();
    }
  };
  var throttleWrapper = function() {
    var callNow = leading && !timeoutId;
    var context = this;
    var args = arguments;
    throttledFn = function() {
      return fn.apply(context, args);
    };
    if (!timeoutId) {
      timeoutId = setTimeout(function() {
        timeoutId = null;
        if (trailing) {
          return throttledFn();
        }
      }, interval);
    }
    if (callNow) {
      callNow = false;
      return throttledFn();
    }
  };
  throttleWrapper.cancel = cancel;
  throttleWrapper.flush = flush;
  return throttleWrapper;
}

// node_modules/@rpldy/shared/lib/esm/consts.js
var DEBUG_LOG_KEY = "__rpldy-logger-debug__";
var BATCH_STATES = {
  PENDING: "pending",
  ADDED: "added",
  PROCESSING: "processing",
  UPLOADING: "uploading",
  CANCELLED: "cancelled",
  FINISHED: "finished",
  ABORTED: "aborted",
  ERROR: "error"
};
var FILE_STATES = {
  PENDING: "pending",
  ADDED: "added",
  UPLOADING: "uploading",
  CANCELLED: "cancelled",
  FINISHED: "finished",
  ERROR: "error",
  ABORTED: "aborted"
};

// node_modules/@rpldy/shared/lib/esm/logger.js
var logger_exports = {};
__export(logger_exports, {
  debugLog: () => debugLog,
  isDebugOn: () => isDebugOn,
  setDebug: () => setDebug
});

// node_modules/@rpldy/shared/lib/esm/utils/hasWindow.js
var hasWindow = () => {
  return typeof window === "object" && !!window.document;
};
var hasWindow_default = hasWindow;

// node_modules/@rpldy/shared/lib/esm/logger.js
var isDebug = null;
var isDebugOn = () => {
  if (typeof isDebug !== "boolean") {
    isDebug = hasWindow_default() && ("location" in window && !!~window.location.search.indexOf("rpldy_debug=true") || window[DEBUG_LOG_KEY] === true);
  }
  return !!isDebug;
};
var setDebug = (debugOn) => {
  if (hasWindow_default()) {
    window[DEBUG_LOG_KEY] = debugOn;
  }
  isDebug = debugOn ? true : null;
};
var debugLog = (...args) => {
  if (isDebugOn()) {
    console.log(...args);
  }
};

// node_modules/@rpldy/shared/lib/esm/triggerCancellable.js
var triggerCancellable = (trigger2, event, ...args) => {
  const doTrigger = (event2, ...args2) => new Promise((resolve, reject) => {
    const results = trigger2(event2, ...args2);
    if (results && results.length) {
      Promise.all(results).catch(reject).then((resolvedResults) => resolvedResults && resolve(!!~resolvedResults.findIndex((r) => r === false)));
    } else {
      resolve(false);
    }
  });
  return event ? doTrigger(event, ...args) : doTrigger;
};
var triggerCancellable_default = triggerCancellable;

// node_modules/@rpldy/shared/lib/esm/utils/index.js
var import_isFunction = __toESM(require_isFunction());

// node_modules/@rpldy/shared/lib/esm/utils/isSamePropInArrays.js
var getPropsExtractor = (prop) => {
  const props = [].concat(prop);
  return (arr) => arr.map((i) => props.map((p) => i[p]).join());
};
var isSamePropInArrays = (arr1, arr2, prop) => {
  let diff = true;
  const propsExtractor = getPropsExtractor(prop);
  if (arr1 && arr2 && arr1.length === arr2.length) {
    const props1 = propsExtractor(arr1), props2 = propsExtractor(arr2);
    diff = !!props1.find((p, i) => p !== props2[i]);
  }
  return !diff;
};
var isSamePropInArrays_default = isSamePropInArrays;

// node_modules/@rpldy/shared/lib/esm/utils/devFreeze.js
var import_isProduction = __toESM(require_isProduction());
var devFreeze = (obj) => (0, import_isProduction.default)() ? obj : Object.freeze(obj);
var devFreeze_default = devFreeze;

// node_modules/@rpldy/shared/lib/esm/utils/isPlainObject.js
var isPlainObject = (obj) => !!obj && typeof obj === "object" && (Object.getPrototypeOf(obj)?.constructor.name === "Object" || Object.getPrototypeOf(obj) === null);
var isPlainObject_default = isPlainObject;

// node_modules/@rpldy/shared/lib/esm/utils/merge.js
var isMergeObj = (obj) => isPlainObject_default(obj) || Array.isArray(obj);
var getKeys = (obj, options) => {
  const keys = Object.keys(obj);
  return options.withSymbols ? keys.concat(Object.getOwnPropertySymbols(obj)) : keys;
};
var getMerge = (options = {}) => {
  const merge = (target, ...sources) => {
    if (target && sources.length) {
      sources.forEach((source) => {
        if (source) {
          getKeys(source, options).forEach((key) => {
            const prop = source[key];
            if (!options.predicate || options.predicate(key, prop)) {
              if (typeof prop !== "undefined" || options.undefinedOverwrites) {
                if (isMergeObj(prop)) {
                  if (typeof target[key] === "undefined" || !isPlainObject_default(target[key])) {
                    target[key] = Array.isArray(prop) ? [] : {};
                  }
                  merge(target[key], prop);
                } else {
                  target[key] = prop;
                }
              }
            }
          });
        }
      });
    }
    return target;
  };
  return merge;
};
var merge_default = getMerge();

// node_modules/@rpldy/shared/lib/esm/utils/clone.js
var clone = (obj, mergeFn = merge_default) => isMergeObj(obj) ? mergeFn(Array.isArray(obj) ? [] : {}, obj) : obj;
var clone_default = clone;

// node_modules/@rpldy/shared/lib/esm/utils/pick.js
var pick = (obj, props) => obj && Object.keys(obj).reduce((res, key) => {
  if (~props.indexOf(key)) {
    res[key] = obj[key];
  }
  return res;
}, {});
var pick_default = pick;

// node_modules/@rpldy/shared/lib/esm/utils/index.js
var import_isProduction2 = __toESM(require_isProduction());

// node_modules/@rpldy/shared/lib/esm/utils/isPromise.js
function isPromise(obj) {
  return !!obj && typeof obj === "object" && typeof obj.then === "function";
}
var isPromise_default = isPromise;

// node_modules/@rpldy/shared/lib/esm/utils/scheduleIdleWork.js
var supportsIdle = hasWindow_default() && window.requestIdleCallback;
var scheduler = supportsIdle ? window.requestIdleCallback : setTimeout;
var clear = supportsIdle ? window.cancelIdleCallback : clearTimeout;
var scheduleIdleWork = (fn, timeout = 0) => {
  const handler = scheduler(fn, supportsIdle ? {
    timeout
  } : timeout);
  return () => clear(handler);
};
var scheduleIdleWork_default = scheduleIdleWork;

// node_modules/@rpldy/shared/lib/esm/utils/isEmpty.js
function isEmpty(val) {
  return val === null || val === void 0;
}
var isEmpty_default = isEmpty;

// node_modules/@rpldy/shared/lib/esm/triggerUpdater.js
var triggerUpdater = (trigger2, event, ...args) => {
  const doTrigger = (event2, ...args2) => new Promise((resolve, reject) => {
    const results = trigger2(event2, ...args2);
    if (results && results.length) {
      Promise.all(results).catch(reject).then((resolvedResults) => {
        let result;
        if (resolvedResults) {
          while (isEmpty_default(result) && resolvedResults.length) {
            result = resolvedResults.pop();
          }
        }
        resolve(isEmpty_default(result) ? void 0 : result);
      });
    } else {
      resolve();
    }
  });
  return event ? doTrigger(event, ...args) : doTrigger;
};
var triggerUpdater_default = triggerUpdater;

// node_modules/@rpldy/shared/lib/esm/batchItem.js
var BISYM = Symbol.for("__rpldy-bi__");
var iCounter = 0;
var getBatchItemWithUrl = (batchItem, url) => {
  batchItem.url = url;
  return batchItem;
};
var getBatchItemWithFile = (batchItem, file) => {
  batchItem.file = file;
  return batchItem;
};
var isLikeFile = (f) => f && (f instanceof File || f instanceof Blob || !!(typeof f === "object" && f.name && f.type));
var getIsBatchItem = (obj) => {
  return !!(typeof obj === "object" && obj.id && obj.batchId && obj[BISYM] === true);
};
var createBatchItem = (f, batchId, isPending = false) => {
  const isAlreadyBatchItem = getIsBatchItem(f);
  iCounter += isAlreadyBatchItem ? 0 : 1;
  const id = isAlreadyBatchItem && f.id && typeof f.id === "string" ? f.id : `${batchId}.item-${iCounter}`, state = isPending ? FILE_STATES.PENDING : FILE_STATES.ADDED;
  let batchItem = {
    id,
    batchId,
    state,
    uploadStatus: 0,
    total: 0,
    completed: 0,
    loaded: 0,
    recycled: isAlreadyBatchItem,
    previousBatch: isAlreadyBatchItem ? f.batchId : null
  };
  Object.defineProperty(batchItem, BISYM, {
    value: true,
    writable: true
  });
  const fileData = isAlreadyBatchItem ? f.file || f.url : f;
  if (typeof fileData === "string") {
    batchItem = getBatchItemWithUrl(batchItem, fileData);
  } else if (isLikeFile(fileData)) {
    batchItem = getBatchItemWithFile(batchItem, fileData);
  } else {
    throw new Error(`Unknown type of file added: ${typeof fileData}`);
  }
  return batchItem;
};
var batchItem_default = createBatchItem;

// node_modules/@rpldy/shared/lib/esm/request/XhrPromise.js
var XhrPromise = class extends Promise {
  constructor(fn, req) {
    super(fn);
    this.xhr = req;
  }
};
var XhrPromise_default = XhrPromise;

// node_modules/@rpldy/shared/lib/esm/request/request.js
var setHeaders = (req, headers) => {
  if (headers) {
    Object.keys(headers).forEach((name) => {
      if (headers[name] !== void 0) {
        req.setRequestHeader(name, headers[name]);
      }
    });
  }
};
var request = (url, data, options = {}) => {
  const req = new XMLHttpRequest();
  return new XhrPromise_default((resolve, reject) => {
    req.onerror = () => reject(req);
    req.ontimeout = () => reject(req);
    req.onabort = () => reject(req);
    req.onload = () => resolve(req);
    req.open(options?.method || "GET", url);
    setHeaders(req, options?.headers);
    req.withCredentials = !!options?.withCredentials;
    options?.preSend?.(req);
    req.send(data);
  }, req);
};
var request_default = request;

// node_modules/@rpldy/shared/lib/esm/request/parseResponseHeaders.js
var parseResponseHeaders = (xhr) => {
  let resHeaders;
  try {
    resHeaders = xhr.getAllResponseHeaders().trim().split(/[\r\n]+/).reduce((res, line) => {
      const [key, val] = line.split(": ");
      res[key] = val;
      return res;
    }, {});
  } catch (ex) {
    debugLog("uploady.request: failed to read response headers", xhr);
  }
  return resHeaders;
};
var parseResponseHeaders_default = parseResponseHeaders;

// node_modules/@rpldy/shared/lib/esm/request/index.js
var request_default2 = request_default;

// node_modules/@rpldy/life-events/lib/esm/defaults.js
var defaults = devFreeze_default({
  allowRegisterNonExistent: true,
  canAddEvents: true,
  canRemoveEvents: true,
  collectStats: false
});
var defaults_default = defaults;

// node_modules/@rpldy/life-events/lib/esm/utils.js
var validateFunction = (f, name) => {
  if (!(0, import_isFunction.default)(f)) {
    throw new Error(`'${name}' is not a valid function`);
  }
};
var isUndefined = (val) => typeof val === "undefined";

// node_modules/@rpldy/life-events/lib/esm/consts.js
var LESYM = Symbol.for("__le__");
var LE_PACK_SYM = Symbol.for("__le__pack__");

// node_modules/@rpldy/life-events/lib/esm/lifeEvents.js
var getLE = (obj) => obj ? obj[LESYM] : null;
var getValidLE = (obj) => {
  const le = getLE(obj);
  if (!le) {
    throw new Error("Didnt find LE internal object. Something very bad happened!");
  }
  return le;
};
var isLE = (obj) => !!getLE(obj);
var addRegistration = (obj, name, cb, once = false) => {
  validateFunction(cb, "cb");
  const le = getValidLE(obj);
  if (!le.options.allowRegisterNonExistent && !~le.events.indexOf(name)) {
    throw new Error(`Cannot register for event ${name.toString()} that wasn't already defined (allowRegisterNonExistent = false)`);
  }
  const namedRegistry = le.registry[name] || [];
  if (!namedRegistry.find((r) => r.cb === cb)) {
    namedRegistry.push({
      name,
      cb,
      once
    });
    le.registry[name] = namedRegistry;
  }
  return () => unregister.call(obj, name, cb);
};
var findRegistrations = (obj, name) => {
  const registry = getValidLE(obj).registry;
  return name ? registry[name] ? registry[name].slice() : [] : Object.values(registry).flat();
};
var publicMethods = {
  "on": register,
  "once": registerOnce,
  "off": unregister,
  "getEvents": getEvents
};
var getPublicMethods = () => Object.entries(publicMethods).reduce((res, [key, m]) => {
  res[key] = {
    value: m
  };
  return res;
}, {});
var apiMethods = {
  "trigger": trigger,
  "addEvent": addEvent,
  "removeEvent": removeEvent,
  "hasEvent": hasEvent,
  "hasEventRegistrations": hasEventRegistrations,
  "assign": assign
};
var createApi = (target) => Object.entries(apiMethods).reduce((res, [name, fn]) => {
  res[name] = fn.bind(target);
  return res;
}, {
  ...apiMethods,
  target
});
var cleanRegistryForName = (obj, name, force = false) => {
  const registry = getValidLE(obj).registry;
  if (registry[name] && (force || !registry[name].length)) {
    delete registry[name];
  }
};
var removeRegItem = (obj, name, cb) => {
  const registry = getValidLE(obj).registry;
  if (registry[name]) {
    if (!cb) {
      cleanRegistryForName(obj, name, true);
    } else {
      registry[name] = registry[name].filter((reg) => reg.cb !== cb);
      cleanRegistryForName(obj, name);
    }
  }
};
function register(name, cb) {
  return addRegistration(this, name, cb);
}
function registerOnce(name, cb) {
  return addRegistration(this, name, cb, true);
}
function unregister(name, cb) {
  removeRegItem(this, name, cb);
}
function getEvents() {
  return getValidLE(this).events.slice();
}
function trigger(name, ...args) {
  const regs = findRegistrations(this, name);
  let results;
  if (regs.length) {
    let packValue;
    if (args.length === 1 && args[0]?.[LE_PACK_SYM] === true) {
      packValue = args[0].resolve();
    }
    results = regs.map((r) => {
      let result;
      if (r.once) {
        removeRegItem(this, name, r.cb);
      }
      if (packValue) {
        result = r.cb(...packValue);
      } else if (!args.length) {
        result = r.cb();
      } else if (args.length === 1) {
        result = r.cb(args[0]);
      } else if (args.length === 2) {
        result = r.cb(args[0], args[1]);
      } else if (args.length === 3) {
        result = r.cb(args[0], args[1], args[2]);
      } else {
        result = r.cb(...args);
      }
      return result;
    }).filter((result) => !isUndefined(result)).map((result) => isPromise_default(result) ? result : Promise.resolve(result));
  }
  return results && (results.length ? results : void 0);
}
function assign(toObj) {
  const le = getValidLE(this);
  defineLifeData(toObj, le.options, le.events, le.registry, le.stats);
  return createApi(toObj);
}
function addEvent(name) {
  const le = getValidLE(this);
  if (le.options.canAddEvents) {
    const index = le.events.indexOf(name);
    if (!~index) {
      le.events.push(name);
    } else {
      throw new Error(`Event '${name}' already defined`);
    }
  } else {
    throw new Error("Cannot add new events (canAddEvents = false)");
  }
}
function removeEvent(name) {
  const le = getValidLE(this);
  if (le.options.canRemoveEvents) {
    const index = le.events.indexOf(name);
    le.events.splice(index, 1);
  } else {
    throw new Error("Cannot remove events (canRemoveEvents = false)");
  }
}
function hasEvent(name) {
  const le = getValidLE(this);
  return !!~le.events.indexOf(name);
}
function hasEventRegistrations(name) {
  return !!findRegistrations(this, name).length;
}
var defineLifeData = (target, options, events = [], registry = {}, stats = {}) => {
  Object.defineProperties(target, {
    [LESYM]: {
      value: Object.seal({
        registry,
        events,
        options,
        stats
      })
    },
    ...getPublicMethods()
  });
};
var addLife = (target, events = [], options) => {
  const useTarget = target || {};
  const usedOptions = {
    ...defaults_default,
    ...options
  };
  if (!isLE(useTarget)) {
    defineLifeData(useTarget, usedOptions, events);
  }
  return createApi(useTarget);
};
var lifeEvents_default = addLife;

// node_modules/@rpldy/life-events/lib/esm/lifePack.js
var createLifePack = (creator) => {
  const lp = {
    resolve: () => [].concat(creator())
  };
  Object.defineProperty(lp, LE_PACK_SYM, {
    value: true,
    configurable: false
  });
  return lp;
};
var lifePack_default = createLifePack;

// node_modules/@rpldy/abort/lib/esm/fastAbort.js
var fastAbortBatch = (batch, aborts) => {
  batch.items.forEach(({
    id
  }) => aborts[id]?.());
};
var fastAbortAll = (aborts) => {
  Object.values(aborts).forEach((fn) => fn());
};

// node_modules/@rpldy/abort/lib/esm/abort.js
var abortNonUploadingItem = (item, aborts, finalizeItem2) => {
  logger_exports.debugLog(`abort: aborting ${item.state} item  - `, item);
  finalizeItem2(item.id, {
    status: 0,
    state: FILE_STATES.ABORTED,
    response: "aborted"
  });
  return true;
};
var ITEM_STATE_ABORTS = {
  [FILE_STATES.UPLOADING]: (item, aborts) => {
    logger_exports.debugLog(`abort: aborting uploading item  - `, item);
    return aborts[item.id]();
  },
  [FILE_STATES.ADDED]: abortNonUploadingItem,
  [FILE_STATES.PENDING]: abortNonUploadingItem
};
var callAbortOnItem = (item, aborts, finalizeItem2) => {
  const itemState = item?.state;
  const method = !!itemState && ITEM_STATE_ABORTS[itemState];
  return method ? method(item, aborts, finalizeItem2) : false;
};
var abortItem = (id, items, aborts, finalizeItem2) => callAbortOnItem(items[id], aborts, finalizeItem2);
var getIsFastAbortNeeded = (count, threshold) => {
  let result = false;
  if (threshold !== 0 && threshold) {
    result = count >= threshold;
  }
  return result;
};
var abortAll = (items, aborts, queue, finalizeItem2, options) => {
  const itemIds = Object.values(queue).flat();
  const isFastAbort = getIsFastAbortNeeded(itemIds.length, options.fastAbortThreshold);
  logger_exports.debugLog(`abort: doing abort-all (${isFastAbort ? "fast" : "normal"} abort)`);
  if (isFastAbort) {
    fastAbortAll(aborts);
  } else {
    itemIds.forEach((id) => abortItem(id, items, aborts, finalizeItem2));
  }
  return {
    isFast: isFastAbort
  };
};
var abortBatch = (batch, batchOptions, aborts, queue, finalizeItem2, options) => {
  const threshold = batchOptions.fastAbortThreshold === 0 ? 0 : batchOptions.fastAbortThreshold || options.fastAbortThreshold;
  const isFastAbort = getIsFastAbortNeeded(queue[batch.id].length, threshold);
  logger_exports.debugLog(`abort: doing abort-batch on: ${batch.id} (${isFastAbort ? "fast" : "normal"} abort)`);
  if (isFastAbort) {
    fastAbortBatch(batch, aborts);
  } else {
    batch.items.forEach((bi) => callAbortOnItem(bi, aborts, finalizeItem2));
  }
  return {
    isFast: isFastAbort
  };
};

// node_modules/@rpldy/abort/lib/esm/getAbortEnhancer.js
var getAbortEnhancer = () => {
  return (uploader) => {
    uploader.update({
      abortAll,
      abortBatch,
      abortItem
    });
    return uploader;
  };
};
var getAbortEnhancer_default = getAbortEnhancer;

// node_modules/@rpldy/abort/lib/esm/index.js
var esm_default = getAbortEnhancer_default;

// node_modules/@rpldy/uploader/lib/esm/consts.js
var UPLOADER_EVENTS = devFreeze_default({
  BATCH_ADD: "BATCH-ADD",
  BATCH_START: "BATCH-START",
  BATCH_PROGRESS: "BATCH_PROGRESS",
  BATCH_FINISH: "BATCH-FINISH",
  BATCH_ABORT: "BATCH-ABORT",
  BATCH_CANCEL: "BATCH-CANCEL",
  BATCH_ERROR: "BATCH-ERROR",
  BATCH_FINALIZE: "BATCH-FINALIZE",
  ITEM_START: "FILE-START",
  ITEM_CANCEL: "FILE-CANCEL",
  ITEM_PROGRESS: "FILE-PROGRESS",
  ITEM_FINISH: "FILE-FINISH",
  ITEM_ABORT: "FILE-ABORT",
  ITEM_ERROR: "FILE-ERROR",
  ITEM_FINALIZE: "FILE-FINALIZE",
  REQUEST_PRE_SEND: "REQUEST_PRE_SEND",
  ALL_ABORT: "ALL_ABORT"
});
var PROGRESS_DELAY = 50;
var SENDER_EVENTS = devFreeze_default({
  ITEM_PROGRESS: "ITEM_PROGRESS",
  BATCH_PROGRESS: "BATCH_PROGRESS"
});
var ITEM_FINALIZE_STATES = [FILE_STATES.FINISHED, FILE_STATES.ERROR, FILE_STATES.CANCELLED, FILE_STATES.ABORTED];

// node_modules/@rpldy/simple-state/lib/esm/consts.js
var PROXY_SYM = Symbol.for("__rpldy-sstt-proxy__");
var STATE_SYM = Symbol.for("__rpldy-sstt-state__");

// node_modules/@rpldy/simple-state/lib/esm/utils.js
var isProxy = (obj) => !(0, import_isProduction2.default)() && !!obj && !!~Object.getOwnPropertySymbols(obj).indexOf(PROXY_SYM);
var isNativeFile = (obj) => hasWindow_default() && obj instanceof File || obj.name && obj.size && obj.uri;
var isProxiable = (obj) => Array.isArray(obj) || isPlainObject_default(obj) && !isNativeFile(obj);

// node_modules/@rpldy/simple-state/lib/esm/createState.js
var mergeWithSymbols = getMerge({
  withSymbols: true,
  predicate: (key) => key !== PROXY_SYM && key !== STATE_SYM
});
var getIsUpdateable = (proxy) => (0, import_isProduction2.default)() ? true : proxy[STATE_SYM].isUpdateable;
var setIsUpdateable = (proxy, value) => {
  if (!(0, import_isProduction2.default)()) {
    proxy[STATE_SYM].isUpdateable = value;
  }
};
var deepProxy = (obj, traps) => {
  let proxy;
  if (isProxiable(obj)) {
    if (!isProxy(obj)) {
      obj[PROXY_SYM] = true;
      proxy = new Proxy(obj, traps);
    }
    Object.keys(obj).forEach((key) => {
      obj[key] = deepProxy(obj[key], traps);
    });
  }
  return proxy || obj;
};
var unwrapProxy = (proxy) => isProxy(proxy) ? clone_default(proxy, mergeWithSymbols) : proxy;
var createState = (obj) => {
  const traps = {
    set: (obj2, key, value) => {
      if (getIsUpdateable(proxy)) {
        obj2[key] = deepProxy(value, traps);
      }
      return true;
    },
    get: (obj2, key) => {
      return key === PROXY_SYM ? unwrapProxy(obj2) : obj2[key];
    },
    defineProperty: () => {
      throw new Error("Simple State doesnt support defining property");
    },
    setPrototypeOf: () => {
      throw new Error("Simple State doesnt support setting prototype");
    },
    deleteProperty: (obj2, key) => {
      if (getIsUpdateable(proxy)) {
        delete obj2[key];
      }
      return true;
    }
  };
  if (!(0, import_isProduction2.default)() && !isProxy(obj)) {
    Object.defineProperty(obj, STATE_SYM, {
      value: {
        isUpdateable: false
      },
      configurable: true
    });
  }
  const proxy = !(0, import_isProduction2.default)() ? deepProxy(obj, traps) : obj;
  return {
    state: proxy,
    update: (fn) => {
      if (!(0, import_isProduction2.default)() && getIsUpdateable(proxy)) {
        throw new Error("Can't call update on State already being updated!");
      }
      try {
        setIsUpdateable(proxy, true);
        fn(proxy);
      } finally {
        setIsUpdateable(proxy, false);
      }
      return proxy;
    },
    unwrap: (entry) => entry ? unwrapProxy(entry) : isProxy(proxy) ? unwrapProxy(proxy) : proxy
  };
};
var createState_default = createState;

// node_modules/@rpldy/uploader/lib/esm/queue/itemHelpers.js
var finalizeItem = (queue, id, delItem = false) => {
  queue.updateState((state) => {
    const {
      batchId
    } = state.items[id] || {
      batchId: null
    };
    if (delItem) {
      delete state.items[id];
    }
    const index = batchId ? state.itemQueue[batchId].indexOf(id) : -1;
    if (~index && batchId) {
      state.itemQueue[batchId].splice(index, 1);
    }
    const activeIndex = state.activeIds.indexOf(id);
    if (~activeIndex) {
      state.activeIds.splice(activeIndex, 1);
    }
    if (batchId && state.batches[batchId].itemBatchOptions[id]) {
      delete state.batches[batchId].itemBatchOptions[id];
    }
  });
};
var getIsItemExists = (queue, itemId) => !!queue.getState().items[itemId];
var getIsItemFinalized = (item) => ITEM_FINALIZE_STATES.includes(item.state);

// node_modules/@rpldy/uploader/lib/esm/queue/preSendPrepare.js
var mergeWithUndefined = getMerge({
  undefinedOverwrites: true
});
var processPrepareResponse = (eventType, items, options, updated) => {
  let usedOptions = options, usedItems = items;
  if (updated && typeof updated !== "boolean") {
    logger_exports.debugLog(`uploader.queue: REQUEST_PRE_SEND(${eventType}) event returned updated items/options`, updated);
    if (updated.items) {
      if (updated.items.length !== items.length || !isSamePropInArrays_default(updated.items, items, ["id", "batchId", "recycled"])) {
        throw new Error(`REQUEST_PRE_SEND(${eventType}) event handlers must return same items with same ids`);
      }
      usedItems = updated.items;
    }
    if (updated.options) {
      usedOptions = mergeWithUndefined({}, options, updated.options);
    }
  }
  return {
    items: usedItems,
    options: usedOptions,
    cancelled: updated === false
  };
};
var triggerItemsPrepareEvent = (queue, eventSubject, items, options, eventType, validateResponse) => triggerUpdater_default(queue.trigger, eventType, eventSubject, options).then((updated) => {
  validateResponse?.(updated);
  return processPrepareResponse(eventType, items, options, updated);
});
var persistPrepareResponse = (queue, prepared) => {
  const batchId = prepared.items[0].batchId;
  if (prepared.items[0] && queue.getState().batches[batchId]) {
    queue.updateState((state) => {
      prepared.items.forEach((i) => {
        if (!getIsItemFinalized(state.items[i.id])) {
          state.items[i.id] = i;
        }
      });
      const batchData2 = state.batches[batchId];
      prepared.items.forEach(({
        id
      }) => {
        batchData2.itemBatchOptions[id] = prepared.options;
      });
    });
    const updatedState = queue.getState();
    prepared.items = prepared.items.map((item) => updatedState.items[item.id]);
    const batchData = updatedState.batches[batchId];
    prepared.options = batchData.itemBatchOptions[prepared.items[0].id] || batchData.batchOptions;
  }
};
var prepareItems = (queue, subject, retrieveItemsFromSubject, createEventSubject, validateResponse, eventType) => {
  const items = retrieveItemsFromSubject(subject);
  const batchOptions = queue.getState().batches[items[0].batchId].batchOptions;
  const eventSubject = createEventSubject?.(subject, batchOptions) || subject;
  return triggerItemsPrepareEvent(queue, eventSubject, items, batchOptions, eventType, validateResponse).then((prepared) => {
    if (!prepared.cancelled) {
      persistPrepareResponse(queue, prepared);
    }
    return prepared;
  });
};
var getItemsPrepareUpdater = (eventType, retrieveItemsFromSubject, createEventSubject = null, validateResponse = null) => (queue, subject) => prepareItems(queue, subject, retrieveItemsFromSubject, createEventSubject, validateResponse, eventType);

// node_modules/@rpldy/uploader/lib/esm/queue/batchHelpers.js
var prepareBatchStartItems = getItemsPrepareUpdater(UPLOADER_EVENTS.BATCH_START, (batch) => batch.items, null, ({
  batch
} = {
  batch: false
}) => {
  if (batch) {
    throw new Error(`BATCH_START event handlers cannot update batch data. Only items & options`);
  }
});
var BATCH_READY_STATES = [BATCH_STATES.ADDED, BATCH_STATES.PROCESSING, BATCH_STATES.UPLOADING];
var BATCH_FINISHED_STATES = [BATCH_STATES.ABORTED, BATCH_STATES.CANCELLED, BATCH_STATES.FINISHED, BATCH_STATES.ERROR];
var getBatchFromState = (state, id) => state.batches[id].batch;
var getBatch = (queue, id) => {
  return getBatchFromState(queue.getState(), id);
};
var getBatchDataFromItemId = (queue, itemId) => {
  const state = queue.getState();
  const item = state.items[itemId];
  return state.batches[item.batchId];
};
var getBatchFromItemId = (queue, itemId) => {
  return getBatchDataFromItemId(queue, itemId).batch;
};
var removeBatchItems = (queue, batchId) => {
  const batch = getBatch(queue, batchId);
  batch.items.forEach(({
    id
  }) => finalizeItem(queue, id, true));
};
var removeBatch = (queue, batchId) => {
  queue.updateState((state) => {
    delete state.batches[batchId];
    delete state.itemQueue[batchId];
    const batchQueueIndex = state.batchQueue.indexOf(batchId);
    if (~batchQueueIndex) {
      state.batchQueue.splice(batchQueueIndex, 1);
    }
    const pendingFlagIndex = state.batchesStartPending.indexOf(batchId);
    if (~pendingFlagIndex) {
      state.batchesStartPending.splice(pendingFlagIndex, 1);
    }
  });
};
var finalizeBatch = (queue, batchId, eventType, finalState = BATCH_STATES.FINISHED, additionalInfo) => {
  queue.updateState((state) => {
    const batch = getBatchFromState(state, batchId);
    batch.state = finalState;
    if (additionalInfo) {
      batch.additionalInfo = additionalInfo;
    }
  });
  triggerUploaderBatchEvent(queue, batchId, eventType);
  triggerUploaderBatchEvent(queue, batchId, UPLOADER_EVENTS.BATCH_FINALIZE);
};
var cancelBatchWithId = (queue, batchId) => {
  logger_exports.debugLog("uploady.uploader.batchHelpers: cancelling batch: ", batchId);
  finalizeBatch(queue, batchId, UPLOADER_EVENTS.BATCH_CANCEL, BATCH_STATES.CANCELLED);
  removeBatchItems(queue, batchId);
  removeBatch(queue, batchId);
};
var cancelBatchForItem = (queue, itemId) => {
  if (getIsItemExists(queue, itemId)) {
    const data = getBatchDataFromItemId(queue, itemId), batchId = data?.batch.id;
    if (batchId) {
      cancelBatchWithId(queue, batchId);
    } else {
      logger_exports.debugLog(`uploady.uploader.batchHelpers: cancel batch called for batch already removed (item id = ${itemId})`);
    }
  }
};
var failBatchForItem = (queue, itemId, err) => {
  const batch = getBatchFromItemId(queue, itemId), batchId = batch.id;
  logger_exports.debugLog("uploady.uploader.batchHelpers: failing batch: ", {
    batch
  });
  finalizeBatch(queue, batchId, UPLOADER_EVENTS.BATCH_ERROR, BATCH_STATES.ERROR, err.message);
  removeBatchItems(queue, batchId);
  removeBatch(queue, batchId);
};
var isItemBatchStartPending = (queue, itemId) => {
  const batch = getBatchFromItemId(queue, itemId);
  return queue.getState().batchesStartPending.includes(batch.id);
};
var isNewBatchStarting = (queue, itemId) => {
  const batch = getBatchFromItemId(queue, itemId);
  return queue.getState().currentBatch !== batch.id;
};
var loadNewBatchForItem = (queue, itemId) => {
  const batch = getBatchFromItemId(queue, itemId);
  queue.updateState((state) => {
    state.batchesStartPending.push(batch.id);
  });
  return prepareBatchStartItems(queue, batch).then(({
    cancelled
  }) => {
    let alreadyFinished = false;
    queue.updateState((state) => {
      const pendingFlagIndex = state.batchesStartPending.indexOf(batch.id);
      state.batchesStartPending.splice(pendingFlagIndex, 1);
    });
    if (!cancelled) {
      alreadyFinished = !getIsItemExists(queue, itemId);
      if (!alreadyFinished) {
        queue.updateState((state) => {
          state.currentBatch = batch.id;
        });
      }
    }
    return !cancelled && !alreadyFinished;
  });
};
var cleanUpFinishedBatches = (queue) => {
  scheduleIdleWork_default(() => {
    const state = queue.getState();
    Object.keys(state.batches).forEach((batchId) => {
      const {
        batch,
        finishedCounter
      } = state.batches[batchId];
      const {
        orgItemCount
      } = batch;
      const alreadyFinalized = getIsBatchFinalized(batch);
      if (orgItemCount === finishedCounter) {
        if (!alreadyFinalized && batch.completed !== 100) {
          queue.updateState((state2) => {
            const batch2 = getBatchFromState(state2, batchId);
            batch2.completed = 100;
            batch2.loaded = batch2.items.reduce((res, {
              loaded
            }) => res + loaded, 0);
          });
          triggerUploaderBatchEvent(queue, batchId, UPLOADER_EVENTS.BATCH_PROGRESS);
        }
        queue.updateState((state2) => {
          if (state2.currentBatch === batchId) {
            state2.currentBatch = null;
          }
        });
        logger_exports.debugLog(`uploady.uploader.batchHelpers: cleaning up batch: ${batch.id}`);
        if (!alreadyFinalized) {
          finalizeBatch(queue, batchId, UPLOADER_EVENTS.BATCH_FINISH);
        }
        removeBatchItems(queue, batchId);
        removeBatch(queue, batchId);
      }
    });
  });
};
var triggerUploaderBatchEvent = (queue, batchId, event) => {
  const state = queue.getState(), {
    batch,
    batchOptions
  } = state.batches[batchId], stateItems = state.items;
  const eventBatch = {
    ...unwrapProxy(batch),
    items: batch.items.map(({
      id
    }) => unwrapProxy(stateItems[id]))
  };
  queue.trigger(event, eventBatch, unwrapProxy(batchOptions));
};
var getIsBatchReady = (queue, batchId) => {
  const batch = getBatchFromState(queue.getState(), batchId);
  return BATCH_READY_STATES.includes(batch.state);
};
var detachRecycledFromPreviousBatch = (queue, item) => {
  const {
    previousBatch
  } = item;
  if (item.recycled && previousBatch && queue.getState().batches[previousBatch]) {
    const {
      id: batchId
    } = getBatchFromItemId(queue, item.id);
    if (batchId === previousBatch) {
      queue.updateState((state) => {
        const batch = getBatchFromState(state, batchId);
        const index = batch.items.findIndex(({
          id
        }) => id === item.id);
        if (~index) {
          batch.items.splice(index, 1);
        }
        if (state.batches[batchId].itemBatchOptions[item.id]) {
          delete state.batches[batchId].itemBatchOptions[item.id];
        }
      });
    }
  }
};
var preparePendingForUpload = (queue, uploadOptions) => {
  queue.updateState((state) => {
    Object.keys(state.batches).forEach((batchId) => {
      const batchData = state.batches[batchId];
      const {
        batch,
        batchOptions
      } = batchData;
      if (batch.state === BATCH_STATES.PENDING) {
        batch.items.forEach((item) => {
          item.state = FILE_STATES.ADDED;
        });
        batch.state = BATCH_STATES.ADDED;
        batchData.batchOptions = merge_default({}, batchOptions, uploadOptions);
      }
    });
  });
};
var removePendingBatches = (queue) => {
  const batches = queue.getState().batches;
  Object.keys(batches).filter((batchId) => batches[batchId].batch.state === BATCH_STATES.PENDING).forEach((batchId) => {
    removeBatchItems(queue, batchId);
    removeBatch(queue, batchId);
  });
};
var incrementBatchFinishedCounter = (queue, batchId) => {
  queue.updateState((state) => {
    state.batches[batchId].finishedCounter += 1;
  });
};
var getIsBatchFinalized = (batch) => BATCH_FINISHED_STATES.includes(batch.state);
var clearBatchData = (queue, batchId) => {
  queue.updateState((state) => {
    const {
      items
    } = getBatchFromState(state, batchId);
    delete state.batches[batchId];
    delete state.itemQueue[batchId];
    const indx = state.batchQueue.indexOf(batchId);
    if (~indx) {
      state.batchQueue.splice(indx, 1);
    }
    if (state.currentBatch === batchId) {
      state.currentBatch = null;
    }
    items.forEach(({
      id
    }) => {
      delete state.items[id];
      const activeIndex = state.activeIds.indexOf(id);
      if (~activeIndex) {
        state.activeIds.splice(activeIndex, 1);
      }
    });
  });
};

// node_modules/@rpldy/uploader/lib/esm/queue/processFinishedRequest.js
var FILE_STATE_TO_EVENT_MAP = {
  [FILE_STATES.PENDING]: null,
  [FILE_STATES.ADDED]: UPLOADER_EVENTS.ITEM_START,
  [FILE_STATES.FINISHED]: UPLOADER_EVENTS.ITEM_FINISH,
  [FILE_STATES.ERROR]: UPLOADER_EVENTS.ITEM_ERROR,
  [FILE_STATES.CANCELLED]: UPLOADER_EVENTS.ITEM_CANCEL,
  [FILE_STATES.ABORTED]: UPLOADER_EVENTS.ITEM_ABORT,
  [FILE_STATES.UPLOADING]: UPLOADER_EVENTS.ITEM_PROGRESS
};
var getIsFinalized = (item) => !!~ITEM_FINALIZE_STATES.indexOf(item.state);
var processFinishedRequest = (queue, finishedData, next) => {
  finishedData.forEach((itemData) => {
    const state = queue.getState();
    const {
      id,
      info
    } = itemData;
    logger_exports.debugLog("uploader.processor.queue: request finished for item - ", {
      id,
      info
    });
    if (state.items[id]) {
      queue.updateState((state2) => {
        const item2 = state2.items[id];
        item2.state = info.state;
        item2.uploadResponse = info.response;
        item2.uploadStatus = info.status;
        if (getIsFinalized(item2)) {
          delete state2.aborts[id];
        }
      });
      const item = queue.getState().items[id];
      if (info.state === FILE_STATES.FINISHED && item.completed < 100) {
        const size = item.file?.size || 0;
        queue.handleItemProgress(item, 100, size, size);
      }
      const {
        batchOptions
      } = getBatchDataFromItemId(queue, id);
      if (FILE_STATE_TO_EVENT_MAP[item.state]) {
        queue.trigger(FILE_STATE_TO_EVENT_MAP[item.state], item, batchOptions);
      }
      if (getIsFinalized(item)) {
        incrementBatchFinishedCounter(queue, item.batchId);
        queue.trigger(UPLOADER_EVENTS.ITEM_FINALIZE, item, batchOptions);
      }
    }
    finalizeItem(queue, id);
  });
  cleanUpFinishedBatches(queue);
  next(queue);
};
var processFinishedRequest_default = processFinishedRequest;

// node_modules/@rpldy/uploader/lib/esm/queue/processBatchItems.js
var preparePreRequestItems = getItemsPrepareUpdater(UPLOADER_EVENTS.REQUEST_PRE_SEND, (items) => items, (items, options) => ({
  items,
  options
}));
var updateUploadingState = (queue, items, sendResult) => {
  queue.updateState((state) => {
    items.forEach((bi) => {
      const item = state.items[bi.id];
      item.state = FILE_STATES.UPLOADING;
      state.aborts[bi.id] = sendResult.abort;
    });
  });
};
var sendAllowedItems = (queue, itemsSendData, next) => {
  const {
    items,
    options
  } = itemsSendData;
  const batch = queue.getState().batches[items[0].batchId]?.batch;
  if (batch) {
    let sendResult;
    try {
      sendResult = queue.sender.send(items, batch, options);
    } catch (ex) {
      logger_exports.debugLog(`uploader.queue: sender failed with unexpected error`, ex);
      sendResult = {
        request: Promise.resolve({
          status: 0,
          state: FILE_STATES.ERROR,
          response: ex.message
        }),
        abort: () => false,
        senderType: "exception-handler"
      };
    }
    const {
      request: request2
    } = sendResult;
    updateUploadingState(queue, items, sendResult);
    request2.then((requestInfo) => {
      const finishedData = items.map((item) => ({
        id: item.id,
        info: requestInfo
      }));
      processFinishedRequest_default(queue, finishedData, next);
    });
  }
};
var reportCancelledItems = (queue, items, cancelledResults, next) => {
  const cancelledItemsIds = cancelledResults.map((isCancelled, index) => isCancelled ? items[index].id : null).filter(Boolean);
  if (cancelledItemsIds.length) {
    const finishedData = cancelledItemsIds.map((id) => ({
      id,
      info: {
        status: 0,
        state: FILE_STATES.CANCELLED,
        response: "cancel"
      }
    }));
    processFinishedRequest_default(queue, finishedData, next);
  }
  return !!cancelledItemsIds.length;
};
var reportPreparedError = (error, queue, items, next) => {
  const finishedData = items.map(({
    id
  }) => ({
    id,
    info: {
      status: 0,
      state: FILE_STATES.ERROR,
      response: error
    }
  }));
  processFinishedRequest_default(queue, finishedData, next);
};
var getAllowedItem = (id, queue) => {
  const item = queue.getState().items[id];
  return item && !getIsItemFinalized(item) ? item : void 0;
};
var processAllowedItems = ({
  allowedItems,
  cancelledResults,
  queue,
  items,
  ids,
  next
}) => {
  const afterPreparePromise = allowedItems.length ? preparePreRequestItems(queue, allowedItems) : Promise.resolve();
  let finalCancelledResults = cancelledResults;
  return afterPreparePromise.catch((err) => {
    logger_exports.debugLog("uploader.queue: encountered error while preparing items for request", err);
    reportPreparedError(err, queue, items, next);
  }).then((itemsSendData) => {
    let nextP;
    if (itemsSendData) {
      if (itemsSendData.cancelled) {
        finalCancelledResults = ids.map(() => true);
      } else {
        const hasAborted = itemsSendData.items.some((item) => getIsItemFinalized(item));
        if (!hasAborted) {
          sendAllowedItems(queue, {
            items: itemsSendData.items,
            options: itemsSendData.options
          }, next);
        } else {
          logger_exports.debugLog("uploader.queue: send data contains aborted items - not sending");
        }
      }
    }
    if (!reportCancelledItems(queue, items, finalCancelledResults, next)) {
      nextP = next(queue);
    }
    return nextP;
  });
};
var processBatchItems = (queue, ids, next) => {
  const state = queue.getState();
  let items = Object.values(state.items);
  items = items.filter((item) => ids.includes(item.id) && !getIsItemFinalized(item));
  return Promise.all(items.map((i) => {
    const {
      batchOptions
    } = getBatchDataFromItemId(queue, i.id);
    return queue.runCancellable(UPLOADER_EVENTS.ITEM_START, i, batchOptions);
  })).then((cancelledResults) => {
    let allowedItems = cancelledResults.map((isCancelled, index) => isCancelled ? null : getAllowedItem(items[index].id, queue)).filter(Boolean);
    return {
      allowedItems,
      cancelledResults,
      queue,
      items,
      ids,
      next
    };
  }).then(processAllowedItems);
};
var processBatchItems_default = processBatchItems;

// node_modules/@rpldy/uploader/lib/esm/queue/processQueueNext.js
var getIsItemInActiveRequest = (queue, itemId) => {
  return queue.getState().activeIds.flat().includes(itemId);
};
var getIsItemReady = (item) => item.state === FILE_STATES.ADDED;
var findNextItemIndex = (queue) => {
  const state = queue.getState(), itemQueue = state.itemQueue, items = state.items;
  let nextItemId = null, batchIndex = 0, itemIndex = 0, batchId = state.batchQueue[batchIndex];
  while (batchId && !nextItemId) {
    if (getIsBatchReady(queue, batchId)) {
      nextItemId = itemQueue[batchId][itemIndex];
      while (nextItemId && (getIsItemInActiveRequest(queue, nextItemId) || !getIsItemReady(items[nextItemId]))) {
        itemIndex += 1;
        nextItemId = itemQueue[batchId][itemIndex];
      }
    }
    if (!nextItemId) {
      batchIndex += 1;
      batchId = state.batchQueue[batchIndex];
      itemIndex = 0;
    }
  }
  return nextItemId ? [batchId, itemIndex] : null;
};
var getNextIdGroup = (queue) => {
  const state = queue.getState(), itemQueue = state.itemQueue, [nextBatchId, nextItemIndex] = findNextItemIndex(queue) || [];
  let nextId = nextBatchId && ~nextItemIndex ? itemQueue[nextBatchId][nextItemIndex] : null, nextGroup;
  if (nextId) {
    const {
      batchOptions
    } = state.batches[nextBatchId], groupMax = batchOptions.maxGroupSize || 0;
    if (batchOptions.grouped && groupMax > 1) {
      const batchItems = state.itemQueue[nextBatchId];
      nextGroup = batchItems.slice(nextItemIndex, nextItemIndex + groupMax);
    } else {
      nextGroup = [nextId];
    }
  }
  return nextGroup;
};
var updateItemsAsActive = (queue, ids) => {
  queue.updateState((state) => {
    state.activeIds = state.activeIds.concat(ids);
  });
};
var processNextWithBatch = (queue, ids) => {
  let newBatchP;
  if (!isItemBatchStartPending(queue, ids[0])) {
    updateItemsAsActive(queue, ids);
    if (isNewBatchStarting(queue, ids[0])) {
      newBatchP = loadNewBatchForItem(queue, ids[0]).then((allowBatch) => {
        let cancelled = !allowBatch;
        if (cancelled) {
          cancelBatchForItem(queue, ids[0]);
          processNext(queue);
        }
        return cancelled;
      }).catch((err) => {
        logger_exports.debugLog("uploader.processor: encountered error while preparing batch for request", err);
        failBatchForItem(queue, ids[0], err);
        processNext(queue);
        return true;
      });
    } else {
      newBatchP = Promise.resolve(false);
    }
  } else {
    newBatchP = Promise.resolve(true);
  }
  return newBatchP;
};
var processNext = (queue) => {
  let processPromise;
  const ids = getNextIdGroup(queue);
  if (ids) {
    const currentCount = queue.getCurrentActiveCount(), {
      concurrent = false,
      maxConcurrent = 0
    } = queue.getOptions();
    if (!currentCount || concurrent && currentCount < maxConcurrent) {
      logger_exports.debugLog("uploader.processor: Processing next upload - ", {
        ids,
        currentCount
      });
      processPromise = processNextWithBatch(queue, ids).then((failedOrCancelled) => {
        if (!failedOrCancelled) {
          processBatchItems_default(queue, ids, processNext);
          if (concurrent) {
            processNext(queue);
          }
        }
      });
    }
  }
  return processPromise;
};
var processQueueNext_default = processNext;

// node_modules/@rpldy/uploader/lib/esm/queue/processAbort.js
var getFinalizeAbortedItem = (queue) => (id, data) => processFinishedRequest_default(queue, [{
  id,
  info: data
}], processQueueNext_default);
var processAbortItem = (queue, id) => {
  const abortItemMethod = queue.getOptions().abortItem;
  (0, import_invariant.default)(!!abortItemMethod, "Abort Item method not provided yet abortItem was called");
  const state = queue.getState();
  return abortItemMethod(id, state.items, state.aborts, getFinalizeAbortedItem(queue));
};
var processAbortBatch = (queue, id) => {
  const abortBatchMethod = queue.getOptions().abortBatch;
  (0, import_invariant.default)(!!abortBatchMethod, "Abort Batch method not provided yet abortItem was called");
  const state = queue.getState(), batchData = state.batches[id], batch = batchData?.batch;
  if (batch && !getIsBatchFinalized(batch)) {
    finalizeBatch(queue, id, UPLOADER_EVENTS.BATCH_ABORT, BATCH_STATES.ABORTED);
    const {
      isFast
    } = abortBatchMethod(batch, batchData.batchOptions, state.aborts, state.itemQueue, getFinalizeAbortedItem(queue), queue.getOptions());
    if (isFast) {
      queue.clearBatchUploads(batch.id);
    }
  }
};
var processAbortAll = (queue) => {
  const abortAllMethod = queue.getOptions().abortAll;
  (0, import_invariant.default)(!!abortAllMethod, "Abort All method not provided yet abortAll was called");
  queue.trigger(UPLOADER_EVENTS.ALL_ABORT);
  const state = queue.getState();
  const {
    isFast
  } = abortAllMethod(state.items, state.aborts, state.itemQueue, getFinalizeAbortedItem(queue), queue.getOptions());
  if (isFast) {
    queue.clearAllUploads();
  }
};

// node_modules/@rpldy/uploader/lib/esm/queue/uploaderQueue.js
var createUploaderQueue = (options, trigger2, cancellable, sender, uploaderId) => {
  const {
    state,
    update
  } = createState_default({
    itemQueue: {},
    batchQueue: [],
    currentBatch: null,
    batchesStartPending: [],
    batches: {},
    items: {},
    activeIds: [],
    aborts: {}
  });
  const getState = () => state;
  const updateState = (updater) => {
    update(updater);
  };
  const add = (item) => {
    if (state.items[item.id] && !item.recycled) {
      throw new Error(`Uploader queue conflict - item ${item.id} already exists`);
    }
    if (item.recycled) {
      detachRecycledFromPreviousBatch(queueState, item);
    }
    updateState((state2) => {
      state2.items[item.id] = item;
    });
  };
  const handleItemProgress = (item, completed, loaded, total) => {
    if (state.items[item.id]) {
      updateState((state2) => {
        const stateItem = state2.items[item.id];
        stateItem.loaded = loaded;
        stateItem.completed = completed;
        stateItem.total = total;
      });
      trigger2(UPLOADER_EVENTS.ITEM_PROGRESS, getState().items[item.id]);
    }
  };
  sender.on(SENDER_EVENTS.ITEM_PROGRESS, handleItemProgress);
  sender.on(SENDER_EVENTS.BATCH_PROGRESS, (batch) => {
    const batchItems = state.batches[batch.id]?.batch.items;
    if (batchItems) {
      const [loaded, total] = batchItems.reduce((res, {
        id
      }) => {
        const {
          loaded: loaded2,
          file
        } = state.items[id];
        const size = file?.size || loaded2 || 1;
        res[0] += loaded2;
        res[1] += size;
        return res;
      }, [0, 0]);
      updateState((state2) => {
        const stateBatch = state2.batches[batch.id].batch;
        stateBatch.total = total;
        stateBatch.loaded = loaded;
        stateBatch.completed = loaded / total;
      });
      triggerUploaderBatchEvent(queueState, batch.id, UPLOADER_EVENTS.BATCH_PROGRESS);
    }
  });
  const queueState = {
    uploaderId,
    getOptions: () => options,
    getCurrentActiveCount: () => state.activeIds.length,
    getState,
    updateState,
    trigger: trigger2,
    runCancellable: (name, ...args) => {
      if (!(0, import_isFunction.default)(cancellable)) {
        throw new Error("Uploader queue - cancellable is of wrong type");
      }
      return cancellable(name, ...args);
    },
    sender,
    handleItemProgress,
    clearAllUploads: () => {
      queueState.updateState((state2) => {
        state2.itemQueue = {};
        state2.batchQueue = [];
        state2.currentBatch = null;
        state2.batches = {};
        state2.items = {};
        state2.activeIds = [];
      });
    },
    clearBatchUploads: (batchId) => {
      scheduleIdleWork_default(() => {
        logger_exports.debugLog(`uploader.queue: started scheduled work to clear batch uploads (${batchId})`);
        if (getState().batches[batchId]) {
          clearBatchData(queueState, batchId);
        }
      });
    }
  };
  if (hasWindow_default() && logger_exports.isDebugOn()) {
    window[`__rpldy_${uploaderId}_queue_state`] = queueState;
  }
  return {
    updateState,
    getState: queueState.getState,
    runCancellable: queueState.runCancellable,
    uploadBatch: (batch, batchOptions) => {
      if (batchOptions) {
        updateState((state2) => {
          state2.batches[batch.id].batchOptions = batchOptions;
        });
      }
      processQueueNext_default(queueState);
    },
    addBatch: (batch, batchOptions) => {
      updateState((state2) => {
        state2.batches[batch.id] = {
          batch,
          batchOptions,
          itemBatchOptions: {},
          finishedCounter: 0
        };
        state2.batchQueue.push(batch.id);
        state2.itemQueue[batch.id] = batch.items.map(({
          id
        }) => id);
      });
      batch.items.forEach(add);
      return getBatchFromState(state, batch.id);
    },
    abortItem: (...args) => processAbortItem(queueState, ...args),
    abortBatch: (...args) => processAbortBatch(queueState, ...args),
    abortAll: (...args) => processAbortAll(queueState, ...args),
    clearPendingBatches: () => {
      removePendingBatches(queueState);
    },
    uploadPendingBatches: (uploadOptions) => {
      preparePendingForUpload(queueState, uploadOptions);
      processQueueNext_default(queueState);
    },
    cancelBatch: (batch) => cancelBatchWithId(queueState, batch.id)
  };
};
var uploaderQueue_default = createUploaderQueue;

// node_modules/@rpldy/sender/lib/esm/consts.js
var XHR_SENDER_TYPE = "rpldy-sender";

// node_modules/@rpldy/sender/lib/esm/MissingUrlError.js
var MissingUrlError = class extends Error {
  constructor(senderType) {
    super(`${senderType} didn't receive upload URL`);
    this.name = "MissingUrlError";
  }
};

// node_modules/@rpldy/sender/lib/esm/xhrSender/prepareFormData.js
var addToFormData = (fd, name, ...rest) => {
  if ("set" in fd) {
    fd.set(name, ...rest);
  } else {
    if ("delete" in fd) {
      fd.delete(name);
    }
    fd.append(name, ...rest);
  }
};
var getFormFileField = (fd, items, options) => {
  const single = items.length === 1;
  items.forEach((item, i) => {
    const name = single ? options.paramName : (0, import_isFunction.default)(options.formatGroupParamName) ? options.formatGroupParamName(i, options.paramName) : `${options.paramName}[${i}]`;
    if (item.file) {
      addToFormData(fd, name, item.file, item.file.name);
    } else if (item.url) {
      addToFormData(fd, name, item.url);
    }
  });
};
var prepareFormData = (items, options) => {
  const fd = new FormData();
  if (options.params) {
    Object.entries(options.params).forEach(([key, val]) => {
      if (options.formDataAllowUndefined || val !== void 0) {
        addToFormData(fd, key, val);
      }
    });
  }
  getFormFileField(fd, items, options);
  return fd;
};
var prepareFormData_default = prepareFormData;

// node_modules/@rpldy/sender/lib/esm/xhrSender/xhrSender.js
var SUCCESS_CODES = [200, 201, 202, 203, 204];
var getRequestData = (items, options) => {
  let data;
  if (options.sendWithFormData) {
    logger_exports.debugLog(`uploady.sender: sending ${items.length} item(s) as form data`);
    data = prepareFormData_default(items, options);
  } else {
    if (items.length > 1) {
      throw new Error(`XHR Sender - Request without form data can only contain 1 item. received ${items.length}`);
    }
    const item = items[0];
    logger_exports.debugLog(`uploady.sender: sending item ${item.id} as request body`);
    data = item.file || item.url;
  }
  return data;
};
var makeRequest = (items, url, options, onProgress, config) => {
  let xhr;
  const data = config?.getRequestData ? config.getRequestData(items, options) : getRequestData(items, options);
  const issueRequest = (requestUrl = url, requestData = data, requestOptions) => {
    const resolvedRequestOptions = merge_default({
      ...pick_default(options, ["method", "headers", "withCredentials"]),
      preSend: (req) => {
        req.upload.onprogress = (e) => {
          if (e.lengthComputable && onProgress) {
            onProgress(e, items.slice());
          }
        };
      }
    }, requestOptions);
    const realPXhr = request_default2(requestUrl, requestData, resolvedRequestOptions);
    xhr = realPXhr.xhr;
    return realPXhr;
  };
  const pXhr = config?.preRequestHandler ? config.preRequestHandler(issueRequest, items, url, options, onProgress, config) : issueRequest();
  return {
    url,
    count: items.length,
    pXhr,
    getXhr: () => xhr,
    aborted: false
  };
};
var parseResponseJson = (response, headers, options) => {
  let parsed = response;
  const ct = headers?.["content-type"];
  if (options.forceJsonResponse || ct?.includes("json")) {
    try {
      parsed = JSON.parse(response);
    } catch {
    }
  }
  return parsed;
};
var checkIsResponseSuccessful = (xhr, options) => {
  const isSuccess = options.isSuccessfulCall ? options.isSuccessfulCall(xhr) : SUCCESS_CODES.includes(xhr.status);
  return isPromise_default(isSuccess) ? isSuccess : Promise.resolve(isSuccess);
};
var processResponse = (sendRequest, options) => sendRequest.pXhr.then((xhr) => {
  logger_exports.debugLog("uploady.sender: received upload response ", xhr);
  return checkIsResponseSuccessful(xhr, options).then((isSuccess) => {
    const state = isSuccess ? FILE_STATES.FINISHED : FILE_STATES.ERROR;
    const status = xhr.status;
    const resHeaders = parseResponseHeaders_default(xhr);
    const response = {
      data: options.formatServerResponse?.(xhr.response, status, resHeaders) ?? parseResponseJson(xhr.response, resHeaders, options),
      headers: resHeaders
    };
    return {
      status,
      state,
      response
    };
  });
}).catch((error) => {
  let state, response;
  if (sendRequest.aborted) {
    state = FILE_STATES.ABORTED;
    response = "aborted";
  } else {
    logger_exports.debugLog("uploady.sender: upload failed: ", error);
    state = FILE_STATES.ERROR;
    response = error;
  }
  return {
    error: true,
    state,
    response,
    status: 0
  };
});
var abortRequest = (sendRequest) => {
  let abortCalled = false;
  const {
    aborted,
    getXhr
  } = sendRequest;
  const xhr = getXhr();
  if (!aborted && xhr && xhr.readyState && xhr.readyState !== 4) {
    logger_exports.debugLog(`uploady.sender: cancelling request with ${sendRequest.count} items to: ${sendRequest.url}`);
    xhr.abort();
    sendRequest.aborted = true;
    abortCalled = true;
  }
  return abortCalled;
};
var getXhrSend = (config) => (items, url, options, onProgress) => {
  if (!url) {
    throw new MissingUrlError(XHR_SENDER_TYPE);
  }
  logger_exports.debugLog("uploady.sender: sending file: ", {
    items,
    url,
    options
  });
  const sendRequest = makeRequest(items, url, options, onProgress, config);
  return {
    request: processResponse(sendRequest, options),
    abort: () => abortRequest(sendRequest),
    senderType: XHR_SENDER_TYPE
  };
};
var xhrSender_default = getXhrSend;

// node_modules/@rpldy/sender/lib/esm/index.js
var send = xhrSender_default();
var esm_default2 = send;

// node_modules/@rpldy/uploader/lib/esm/defaults.js
var DEFAULT_PARAM_NAME = "file";
var DEFAULT_FILTER = () => true;
var DEFAULT_OPTIONS = devFreeze_default({
  autoUpload: true,
  clearPendingOnAdd: false,
  inputFieldName: "file",
  concurrent: false,
  maxConcurrent: 2,
  grouped: false,
  maxGroupSize: 5,
  method: "POST",
  params: {},
  fileFilter: DEFAULT_FILTER,
  forceJsonResponse: false,
  withCredentials: false,
  destination: {},
  send: null,
  sendWithFormData: true,
  formDataAllowUndefined: false,
  fastAbortThreshold: 100
});

// node_modules/@rpldy/uploader/lib/esm/batchItemsSender.js
var reportItemsProgress = (items, completed, loaded, total, trigger2) => {
  items.forEach((item) => {
    logger_exports.debugLog(`uploady.uploader.processor: file: ${item.id} progress event: loaded(${loaded}) - completed(${completed})`);
    trigger2(SENDER_EVENTS.ITEM_PROGRESS, item, completed, loaded, total);
  });
};
var onItemUploadProgress = (items, batch, e, trigger2) => {
  const completed = Math.min(e.loaded / e.total * 100, 100), completedPerItem = completed / items.length, loadedAverage = e.loaded / items.length;
  reportItemsProgress(items, completedPerItem, loadedAverage, e.total, trigger2);
  trigger2(SENDER_EVENTS.BATCH_PROGRESS, batch);
};
var createBatchItemsSender = () => {
  const {
    trigger: trigger2,
    target: sender
  } = lifeEvents_default({
    send: (items, batch, batchOptions) => {
      const destination = batchOptions.destination, url = destination?.url;
      const throttledProgress = functionThrottle((e) => onItemUploadProgress(items, batch, e, trigger2), PROGRESS_DELAY, true);
      const send2 = (0, import_isFunction.default)(batchOptions.send) ? batchOptions.send : esm_default2;
      return send2(items, url, {
        method: destination?.method || batchOptions.method || DEFAULT_OPTIONS.method,
        paramName: destination?.filesParamName || batchOptions.inputFieldName || DEFAULT_PARAM_NAME,
        params: {
          ...batchOptions.params,
          ...destination?.params
        },
        forceJsonResponse: batchOptions.forceJsonResponse,
        withCredentials: batchOptions.withCredentials,
        formatGroupParamName: batchOptions.formatGroupParamName,
        headers: destination?.headers,
        sendWithFormData: batchOptions.sendWithFormData,
        formatServerResponse: batchOptions.formatServerResponse,
        formDataAllowUndefined: batchOptions.formDataAllowUndefined,
        isSuccessfulCall: batchOptions.isSuccessfulCall
      }, throttledProgress);
    }
  }, Object.values(SENDER_EVENTS));
  return sender;
};
var batchItemsSender_default = createBatchItemsSender;

// node_modules/@rpldy/uploader/lib/esm/utils.js
var FILE_LIST_SUPPORT = hasWindow_default() && "FileList" in window;
var getMandatoryDestination = (dest) => {
  return {
    params: {},
    ...dest
  };
};
var getMandatoryOptions = (options) => {
  return {
    ...DEFAULT_OPTIONS,
    ...options,
    destination: options && options.destination ? getMandatoryDestination(options.destination) : null
  };
};
var getIsFileList = (files) => FILE_LIST_SUPPORT && files instanceof FileList || files.toString() === "[object FileList]";
var deepProxyUnwrap = (obj, level = 0) => {
  let result = obj;
  if (!(0, import_isProduction2.default)()) {
    if (level < 3 && isProxy(obj)) {
      result = unwrapProxy(obj);
    } else if (level < 3 && isProxiable(obj)) {
      result = Array.isArray(obj) ? obj.map((sub) => deepProxyUnwrap(sub, level + 1)) : Object.keys(obj).reduce((res, key) => {
        res[key] = deepProxyUnwrap(obj[key], level + 1);
        return res;
      }, {});
    }
  }
  return result;
};

// node_modules/@rpldy/uploader/lib/esm/batch.js
var bCounter = 0;
var processFiles = (batchId, files, isPending, fileFilter) => {
  const all = fileFilter ? Array.prototype.map.call(files, (f) => getIsBatchItem(f) ? f.file || f.url : f) : [];
  return Promise.all(Array.prototype.map.call(files, (f, index) => {
    const filterResult = (fileFilter || DEFAULT_FILTER)(all[index], index, all);
    return isPromise_default(filterResult) ? filterResult.then((result) => !!result && f) : !!filterResult && f;
  })).then((filtered) => filtered.filter(Boolean).map((f) => batchItem_default(f, batchId, isPending)));
};
var createBatch = (files, uploaderId, options) => {
  bCounter += 1;
  const id = `batch-${bCounter}`;
  const isFileList = getIsFileList(files);
  const usedFiles = Array.isArray(files) || isFileList ? files : [files];
  const isPending = !options.autoUpload;
  return processFiles(id, usedFiles, isPending, options.fileFilter).then((items) => {
    return {
      id,
      uploaderId,
      items,
      state: isPending ? BATCH_STATES.PENDING : BATCH_STATES.ADDED,
      completed: 0,
      loaded: 0,
      orgItemCount: items.length,
      additionalInfo: null
    };
  });
};
var batch_default = createBatch;

// node_modules/@rpldy/uploader/lib/esm/processor.js
var createProcessor = (trigger2, cancellable, options, uploaderId) => {
  const sender = batchItemsSender_default(), queue = uploaderQueue_default(options, trigger2, cancellable, sender, uploaderId);
  return {
    abortBatch: (batchId) => {
      queue.abortBatch(batchId);
    },
    abort: (id) => {
      if (id) {
        queue.abortItem(id);
      } else {
        queue.abortAll();
      }
    },
    addNewBatch: (files, processOptions) => batch_default(files, uploaderId, processOptions).then((batch) => {
      let resultP;
      if (batch.items.length) {
        const addedBatch = queue.addBatch(batch, processOptions);
        resultP = queue.runCancellable(UPLOADER_EVENTS.BATCH_ADD, addedBatch, processOptions).then((isCancelled) => {
          if (!isCancelled) {
            logger_exports.debugLog(`uploady.uploader [${uploaderId}]: new items added - auto upload =
                       ${String(processOptions.autoUpload)}`, addedBatch.items);
            if (processOptions.autoUpload) {
              queue.uploadBatch(addedBatch);
            }
          } else {
            queue.cancelBatch(addedBatch);
          }
          return addedBatch;
        });
      } else {
        logger_exports.debugLog(`uploady.uploader: no items to add. batch ${batch.id} is empty. check fileFilter if this isn't intended`);
      }
      return resultP || Promise.resolve(null);
    }),
    clearPendingBatches: () => {
      queue.clearPendingBatches();
    },
    processPendingBatches: (uploadOptions) => {
      queue.uploadPendingBatches(uploadOptions);
    }
  };
};
var processor_default = createProcessor;

// node_modules/@rpldy/uploader/lib/esm/composeEnhancers.js
var composeEnhancers = (...enhancers) => (uploader, ...args) => enhancers.reduce((enhanced, e) => e(enhanced, ...args) || enhanced, uploader);
var composeEnhancers_default = composeEnhancers;

// node_modules/@rpldy/uploader/lib/esm/uploader.js
var EVENT_NAMES = Object.values(UPLOADER_EVENTS);
var EXT_OUTSIDE_ENHANCER_TIME = "Uploady - uploader extensions can only be registered by enhancers";
var EXT_ALREADY_EXISTS = "Uploady - uploader extension by this name [%s] already exists";
var counter = 0;
var getComposedEnhancer = (extEnhancer) => composeEnhancers_default(esm_default(), extEnhancer);
var getEnhancedUploader = (uploader, options, triggerWithUnwrap, setEnhancerTime) => {
  const enhancer = options.enhancer ? getComposedEnhancer(options.enhancer) : esm_default();
  setEnhancerTime(true);
  const enhanced = enhancer(uploader, triggerWithUnwrap);
  setEnhancerTime(false);
  return enhanced || uploader;
};
var createUploader = (options) => {
  counter += 1;
  const uploaderId = `uploader-${counter}`;
  let enhancerTime = false;
  const extensions = {};
  logger_exports.debugLog(`uploady.uploader: creating new instance (${uploaderId})`, {
    options,
    counter
  });
  let uploaderOptions = getMandatoryOptions(options);
  const clearPending = () => {
    processor.clearPendingBatches();
  };
  const getOptions = () => {
    return clone_default(uploaderOptions);
  };
  let {
    trigger: trigger2,
    target: uploader
  } = lifeEvents_default({
    id: uploaderId,
    update: (updateOptions) => {
      uploaderOptions = merge_default({}, uploaderOptions, updateOptions);
      return uploader;
    },
    add: (files, addOptions) => {
      const processOptions = merge_default({}, uploaderOptions, addOptions);
      if (processOptions.clearPendingOnAdd) {
        clearPending();
      }
      return processor.addNewBatch(files, processOptions).then(() => {
        logger_exports.debugLog(`uploady.uploader: finished adding file data to be processed`);
      });
    },
    upload: (uploadOptions) => {
      processor.processPendingBatches(uploadOptions);
    },
    abort: (id) => {
      processor.abort(id);
    },
    abortBatch: (id) => {
      processor.abortBatch(id);
    },
    getOptions,
    clearPending,
    registerExtension: (name, methods) => {
      (0, import_invariant.default)(enhancerTime, EXT_OUTSIDE_ENHANCER_TIME);
      (0, import_invariant.default)(!extensions[name], EXT_ALREADY_EXISTS, name);
      logger_exports.debugLog(`uploady.uploader: registering extension: ${name.toString()}`, methods);
      extensions[name] = methods;
    },
    getExtension: (name) => {
      return extensions[name];
    }
  }, EVENT_NAMES, {
    canAddEvents: false,
    canRemoveEvents: false
  });
  const triggerWithUnwrap = (name, ...data) => {
    const lp = lifePack_default(() => data.map(deepProxyUnwrap));
    return trigger2(name, lp);
  };
  const cancellable = triggerCancellable_default(triggerWithUnwrap);
  const enhancedUploader = getEnhancedUploader(uploader, uploaderOptions, triggerWithUnwrap, (state) => {
    enhancerTime = state;
  });
  const processor = processor_default(triggerWithUnwrap, cancellable, uploaderOptions, enhancedUploader.id);
  return devFreeze_default(enhancedUploader);
};
var uploader_default = createUploader;

// node_modules/@rpldy/uploader/lib/esm/index.js
var esm_default3 = uploader_default;

// node_modules/@rpldy/shared-ui/lib/esm/hooks/hooksUtils.js
var import_react3 = __toESM(require_react());

// node_modules/@rpldy/shared-ui/lib/esm/hooks/useUploadyContext.js
var import_react2 = __toESM(require_react());

// node_modules/@rpldy/shared-ui/lib/esm/UploadyContext.js
var import_react = __toESM(require_react());

// node_modules/@rpldy/shared-ui/lib/esm/uploadyVersion.js
var GLOBAL_VERSION_SYM = Symbol.for("_rpldy-version_");
var getVersion = () => "1.9.0";
var getGlobal = () => hasWindow_default() ? window : globalThis || process;
var getRegisteredVersion = () => {
  const global = getGlobal();
  return global[GLOBAL_VERSION_SYM];
};
var registerUploadyContextVersion = () => {
  const global = getGlobal();
  global[GLOBAL_VERSION_SYM] = getVersion();
};
var getIsVersionRegisteredAndDifferent = () => {
  const registeredVersion = getRegisteredVersion();
  return !!registeredVersion && registeredVersion !== getVersion();
};

// node_modules/@rpldy/shared-ui/lib/esm/UploadyContext.js
var UploadyContext = /* @__PURE__ */ import_react.default.createContext(null);
var NO_INPUT_ERROR_MSG = "Uploady - Context. File input isn't available";
var createContextApi = (uploader, internalInputRef) => {
  let fileInputRef, showFileUploadOptions;
  let isUsingExternalInput = false;
  if (internalInputRef) {
    fileInputRef = internalInputRef;
  } else {
    logger_exports.debugLog("Uploady context - didn't receive input field ref - waiting for external ref");
  }
  const getInputField = () => fileInputRef?.current;
  const getInternalFileInput = () => {
    if (fileInputRef) {
      isUsingExternalInput = true;
    }
    return fileInputRef;
  };
  const getIsUsingExternalInput = () => isUsingExternalInput;
  const onFileInputChange = () => {
    const input = getInputField();
    (0, import_invariant.default)(input, NO_INPUT_ERROR_MSG);
    input.removeEventListener("change", onFileInputChange);
    const addOptions = showFileUploadOptions;
    showFileUploadOptions = null;
    upload(input.files, addOptions);
  };
  const upload = (files, addOptions) => {
    uploader.add(files, addOptions);
  };
  registerUploadyContextVersion();
  return {
    hasUploader: () => !!uploader,
    getInternalFileInput,
    setExternalFileInput: (extRef) => {
      isUsingExternalInput = true;
      fileInputRef = extRef;
    },
    getIsUsingExternalInput,
    showFileUpload: (addOptions) => {
      const input = getInputField();
      (0, import_invariant.default)(input, NO_INPUT_ERROR_MSG);
      showFileUploadOptions = addOptions;
      input.removeEventListener("change", onFileInputChange);
      input.addEventListener("change", onFileInputChange);
      input.value = "";
      input.click();
    },
    upload,
    processPending: (uploadOptions) => {
      uploader.upload(uploadOptions);
    },
    clearPending: () => {
      uploader.clearPending();
    },
    setOptions: (options) => {
      uploader.update(options);
    },
    getOptions: () => {
      return uploader.getOptions();
    },
    getExtension: (name) => {
      return uploader.getExtension(name);
    },
    abort: (itemId) => {
      uploader.abort(itemId);
    },
    abortBatch: (batchId) => {
      uploader.abortBatch(batchId);
    },
    on: (name, cb) => {
      return uploader.on(name, cb);
    },
    once: (name, cb) => {
      return uploader.once(name, cb);
    },
    off: (name, cb) => {
      return uploader.off(name, cb);
    }
  };
};
var UploadyContext_default = UploadyContext;

// node_modules/@rpldy/shared-ui/lib/esm/assertContext.js
var ERROR_MSG = "Uploady - Valid UploadyContext not found. Make sure you render inside <Uploady>";
var DIFFERENT_VERSION_ERROR_MSG = `Uploady - Valid UploadyContext not found.
You may be using packages of different Uploady versions. <Uploady> and all other packages using the context provider must be of the same version: %s`;
var assertContext = (context) => {
  (0, import_invariant.default)(!getIsVersionRegisteredAndDifferent(), DIFFERENT_VERSION_ERROR_MSG, getRegisteredVersion());
  (0, import_invariant.default)(context && context.hasUploader(), ERROR_MSG);
  return context;
};
var assertContext_default = assertContext;

// node_modules/@rpldy/shared-ui/lib/esm/hooks/useUploadyContext.js
var useUploadyContext = () => assertContext_default((0, import_react2.useContext)(UploadyContext_default));
var useUploadyContext_default = useUploadyContext;

// node_modules/@rpldy/shared-ui/lib/esm/hooks/hooksUtils.js
var useEventEffect = (event, fn) => {
  const context = useUploadyContext_default();
  const {
    on,
    off
  } = context;
  (0, import_react3.useEffect)(() => {
    on(event, fn);
    return () => {
      off(event, fn);
    };
  }, [event, fn, on, off]);
};
var generateUploaderEventHookWithState = (event, stateCalculator) => (fn, id) => {
  const [eventState, setEventState] = (0, import_react3.useState)(null);
  let cbFn = fn;
  let usedId = id;
  if (fn && !(0, import_isFunction.default)(fn)) {
    usedId = fn;
    cbFn = void 0;
  }
  const eventCallback = (0, import_react3.useCallback)((eventObj, ...args) => {
    if (!usedId || eventObj.id === usedId) {
      setEventState(stateCalculator(eventObj, ...args));
      if ((0, import_isFunction.default)(cbFn)) {
        cbFn(eventObj, ...args);
      }
    }
  }, [cbFn, usedId]);
  useEventEffect(event, eventCallback);
  return eventState;
};
var generateUploaderEventHook = (event, canScope = true) => (fn, id) => {
  const eventCallback = (0, import_react3.useCallback)((eventObj, ...args) => {
    return fn && (!canScope || !id || eventObj.id === id) ? fn(eventObj, ...args) : void 0;
  }, [fn, id]);
  useEventEffect(event, eventCallback);
};

// node_modules/@rpldy/shared-ui/lib/esm/hooks/eventListenerHooks.js
var useBatchAddListener = generateUploaderEventHook(UPLOADER_EVENTS.BATCH_ADD, false);
var useBatchStartListener = generateUploaderEventHook(UPLOADER_EVENTS.BATCH_START);
var useBatchFinishListener = generateUploaderEventHook(UPLOADER_EVENTS.BATCH_FINISH);
var useBatchCancelledListener = generateUploaderEventHook(UPLOADER_EVENTS.BATCH_CANCEL);
var useBatchErrorListener = generateUploaderEventHook(UPLOADER_EVENTS.BATCH_ERROR);
var useBatchFinalizeListener = generateUploaderEventHook(UPLOADER_EVENTS.BATCH_FINALIZE);
var useBatchAbortListener = generateUploaderEventHook(UPLOADER_EVENTS.BATCH_ABORT);
var useBatchProgressListener = generateUploaderEventHookWithState(UPLOADER_EVENTS.BATCH_PROGRESS, (batch) => ({
  ...batch
}));
var useItemStartListener = generateUploaderEventHook(UPLOADER_EVENTS.ITEM_START);
var useItemFinishListener = generateUploaderEventHook(UPLOADER_EVENTS.ITEM_FINISH);
var useItemCancelListener = generateUploaderEventHook(UPLOADER_EVENTS.ITEM_CANCEL);
var useItemErrorListener = generateUploaderEventHook(UPLOADER_EVENTS.ITEM_ERROR);
var useItemAbortListener = generateUploaderEventHook(UPLOADER_EVENTS.ITEM_ABORT);
var useItemFinalizeListener = generateUploaderEventHook(UPLOADER_EVENTS.ITEM_FINALIZE);
var useItemProgressListener = generateUploaderEventHookWithState(UPLOADER_EVENTS.ITEM_PROGRESS, (item) => ({
  ...item
}));
var useRequestPreSend = generateUploaderEventHook(UPLOADER_EVENTS.REQUEST_PRE_SEND, false);
var useAllAbortListener = generateUploaderEventHook(UPLOADER_EVENTS.ALL_ABORT, false);

// node_modules/@rpldy/shared-ui/lib/esm/consts.js
var UPLOAD_OPTIONS_COMP = Symbol.for("rpldy_component");

// node_modules/@rpldy/shared-ui/lib/esm/utils.js
var markAsUploadOptionsComponent = (Component) => {
  Component[UPLOAD_OPTIONS_COMP] = true;
};

// node_modules/@rpldy/shared-ui/lib/esm/NoDomUploady.js
var import_react5 = __toESM(require_react());

// node_modules/@rpldy/shared-ui/lib/esm/hooks/useUploader.js
var import_react4 = __toESM(require_react());
var useUploader = (options, listeners) => {
  const uploader = (0, import_react4.useMemo)(() => {
    logger_exports.debugLog("Uploady creating a new uploader instance", options);
    return esm_default3(options);
  }, [options.enhancer]);
  uploader.update(options);
  (0, import_react4.useEffect)(() => {
    if (listeners) {
      logger_exports.debugLog("Uploady setting event listeners", listeners);
      Object.entries(listeners).forEach(([name, m]) => {
        uploader.on(name, m);
      });
    }
    return () => {
      if (listeners) {
        logger_exports.debugLog("Uploady removing event listeners", listeners);
        Object.entries(listeners).forEach(([name, m]) => uploader.off(name, m));
      }
    };
  }, [listeners, uploader]);
  return uploader;
};
var useUploader_default = useUploader;

// node_modules/@rpldy/shared-ui/lib/esm/NoDomUploady.js
var NoDomUploady = (props) => {
  const {
    listeners,
    debug,
    children,
    inputRef,
    ...uploadOptions
  } = props;
  logger_exports.setDebug(!!debug);
  logger_exports.debugLog("@@@@@@ Uploady Rendering @@@@@@", props);
  const uploader = useUploader_default(uploadOptions, listeners);
  const api = (0, import_react5.useMemo)(() => createContextApi(uploader, inputRef), [uploader, inputRef]);
  return /* @__PURE__ */ import_react5.default.createElement(UploadyContext_default.Provider, {
    value: api
  }, children);
};
var NoDomUploady_default = NoDomUploady;

// node_modules/@rpldy/shared-ui/lib/esm/hooks/useUploadOptions.js
var useUploadOptions = (options) => {
  const context = useUploadyContext_default();
  if (options) {
    context.setOptions(options);
  }
  return context.getOptions();
};
var useUploadOptions_default = useUploadOptions;

// node_modules/@rpldy/shared-ui/lib/esm/hooks/useAbortItem.js
var import_react6 = __toESM(require_react());

// node_modules/@rpldy/shared-ui/lib/esm/hooks/useAbortBatch.js
var import_react7 = __toESM(require_react());

// node_modules/@rpldy/shared-ui/lib/esm/hooks/useAbortAll.js
var import_react8 = __toESM(require_react());

// node_modules/@rpldy/shared-ui/lib/esm/hocs/createRequestUpdateHoc.js
var import_react9 = __toESM(require_react());
function _extends() {
  return _extends = Object.assign ? Object.assign.bind() : function(n) {
    for (var e = 1; e < arguments.length; e++) {
      var t = arguments[e];
      for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]);
    }
    return n;
  }, _extends.apply(null, arguments);
}
var createRequestUpdateHoc = ({
  eventType,
  getIsValidEventData,
  getRequestData: getRequestData2
}) => (Component) => (props) => {
  const context = useUploadyContext_default();
  const [updater, setUpdater] = (0, import_react9.useState)({
    updateRequest: null,
    requestData: null
  });
  const {
    id
  } = props;
  (0, import_react9.useLayoutEffect)(() => {
    const handleEvent = (...params) => getIsValidEventData(id, ...params) === true ? new Promise((resolve) => {
      setUpdater({
        updateRequest: (data) => {
          context.off(eventType, handleEvent);
          resolve(data);
        },
        requestData: getRequestData2(...params)
      });
    }) : void 0;
    if (id) {
      context.on(eventType, handleEvent);
    }
    return () => {
      if (id) {
        context.off(eventType, handleEvent);
      }
    };
  }, [context, id]);
  return /* @__PURE__ */ import_react9.default.createElement(Component, _extends({}, props, updater));
};

// node_modules/@rpldy/shared-ui/lib/esm/hocs/withRequestPreSendUpdate.js
var withRequestPreSendUpdate = createRequestUpdateHoc({
  eventType: UPLOADER_EVENTS.REQUEST_PRE_SEND,
  getIsValidEventData: (id, {
    items
  }) => !!items.find((item) => item.id === id),
  getRequestData: ({
    items,
    options
  }) => ({
    items,
    options
  })
});

// node_modules/@rpldy/shared-ui/lib/esm/hocs/withBatchStartUpdate.js
var withBatchStartUpdate = createRequestUpdateHoc({
  eventType: UPLOADER_EVENTS.BATCH_START,
  getIsValidEventData: (id, batch) => batch.id === id,
  getRequestData: (batch, batchOptions) => ({
    batch,
    items: batch.items,
    options: batchOptions
  })
});

export {
  hasWindow_default,
  import_invariant,
  useUploadyContext_default,
  useItemProgressListener,
  markAsUploadOptionsComponent,
  NoDomUploady_default,
  useUploadOptions_default
};
