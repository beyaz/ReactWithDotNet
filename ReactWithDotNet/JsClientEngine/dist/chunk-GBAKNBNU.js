import {
  ObjectUtils,
  PrimeReact,
  PrimeReactContext,
  useUpdateEffect
} from "./chunk-NBA33TRK.js";
import {
  CSSTransition_default
} from "./chunk-M4ZKUW6V.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/primereact/csstransition/csstransition.esm.js
var React = __toESM(require_react());
function _typeof(o) {
  "@babel/helpers - typeof";
  return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function(o2) {
    return typeof o2;
  } : function(o2) {
    return o2 && "function" == typeof Symbol && o2.constructor === Symbol && o2 !== Symbol.prototype ? "symbol" : typeof o2;
  }, _typeof(o);
}
function toPrimitive(t, r) {
  if ("object" != _typeof(t) || !t) return t;
  var e = t[Symbol.toPrimitive];
  if (void 0 !== e) {
    var i = e.call(t, r || "default");
    if ("object" != _typeof(i)) return i;
    throw new TypeError("@@toPrimitive must return a primitive value.");
  }
  return ("string" === r ? String : Number)(t);
}
function toPropertyKey(t) {
  var i = toPrimitive(t, "string");
  return "symbol" == _typeof(i) ? i : i + "";
}
function _defineProperty(e, r, t) {
  return (r = toPropertyKey(r)) in e ? Object.defineProperty(e, r, {
    value: t,
    enumerable: true,
    configurable: true,
    writable: true
  }) : e[r] = t, e;
}
var CSSTransitionBase = {
  defaultProps: {
    __TYPE: "CSSTransition",
    children: void 0
  },
  getProps: function getProps(props) {
    return ObjectUtils.getMergedProps(props, CSSTransitionBase.defaultProps);
  },
  getOtherProps: function getOtherProps(props) {
    return ObjectUtils.getDiffProps(props, CSSTransitionBase.defaultProps);
  }
};
function ownKeys(e, r) {
  var t = Object.keys(e);
  if (Object.getOwnPropertySymbols) {
    var o = Object.getOwnPropertySymbols(e);
    r && (o = o.filter(function(r2) {
      return Object.getOwnPropertyDescriptor(e, r2).enumerable;
    })), t.push.apply(t, o);
  }
  return t;
}
function _objectSpread(e) {
  for (var r = 1; r < arguments.length; r++) {
    var t = null != arguments[r] ? arguments[r] : {};
    r % 2 ? ownKeys(Object(t), true).forEach(function(r2) {
      _defineProperty(e, r2, t[r2]);
    }) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : ownKeys(Object(t)).forEach(function(r2) {
      Object.defineProperty(e, r2, Object.getOwnPropertyDescriptor(t, r2));
    });
  }
  return e;
}
var CSSTransition = /* @__PURE__ */ React.forwardRef(function(inProps, ref) {
  var props = CSSTransitionBase.getProps(inProps);
  var context = React.useContext(PrimeReactContext);
  var disabled = props.disabled || props.options && props.options.disabled || context && !context.cssTransition || !PrimeReact.cssTransition;
  var onEnter = function onEnter2(node, isAppearing) {
    props.onEnter && props.onEnter(node, isAppearing);
    props.options && props.options.onEnter && props.options.onEnter(node, isAppearing);
  };
  var onEntering = function onEntering2(node, isAppearing) {
    props.onEntering && props.onEntering(node, isAppearing);
    props.options && props.options.onEntering && props.options.onEntering(node, isAppearing);
  };
  var onEntered = function onEntered2(node, isAppearing) {
    props.onEntered && props.onEntered(node, isAppearing);
    props.options && props.options.onEntered && props.options.onEntered(node, isAppearing);
  };
  var onExit = function onExit2(node) {
    props.onExit && props.onExit(node);
    props.options && props.options.onExit && props.options.onExit(node);
  };
  var onExiting = function onExiting2(node) {
    props.onExiting && props.onExiting(node);
    props.options && props.options.onExiting && props.options.onExiting(node);
  };
  var onExited = function onExited2(node) {
    props.onExited && props.onExited(node);
    props.options && props.options.onExited && props.options.onExited(node);
  };
  useUpdateEffect(function() {
    if (disabled) {
      var node = ObjectUtils.getRefElement(props.nodeRef);
      if (props["in"]) {
        onEnter(node, true);
        onEntering(node, true);
        onEntered(node, true);
      } else {
        onExit(node);
        onExiting(node);
        onExited(node);
      }
    }
  }, [props["in"]]);
  if (disabled) {
    return props["in"] ? props.children : null;
  }
  var immutableProps = {
    nodeRef: props.nodeRef,
    "in": props["in"],
    appear: props.appear,
    onEnter,
    onEntering,
    onEntered,
    onExit,
    onExiting,
    onExited
  };
  var mutableProps = {
    classNames: props.classNames,
    timeout: props.timeout,
    unmountOnExit: props.unmountOnExit
  };
  var mergedProps = _objectSpread(_objectSpread(_objectSpread({}, mutableProps), props.options || {}), immutableProps);
  return /* @__PURE__ */ React.createElement(CSSTransition_default, mergedProps, props.children);
});
CSSTransition.displayName = "CSSTransition";

export {
  CSSTransition
};
