import {
  Portal
} from "./chunk-CMYF6AIZ.js";
import {
  ComponentBase,
  DomHandler,
  ObjectUtils,
  PrimeReact,
  PrimeReactContext,
  ZIndexUtils,
  classNames,
  useHandleStyle,
  useMergeProps,
  useMountEffect,
  useUnmountEffect,
  useUpdateEffect
} from "./chunk-NBA33TRK.js";
import "./chunk-QNQS7X5M.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/primereact/blockui/blockui.esm.js
var React = __toESM(require_react());
function _extends() {
  return _extends = Object.assign ? Object.assign.bind() : function(n) {
    for (var e = 1; e < arguments.length; e++) {
      var t = arguments[e];
      for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]);
    }
    return n;
  }, _extends.apply(null, arguments);
}
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
function _arrayWithHoles(r) {
  if (Array.isArray(r)) return r;
}
function _iterableToArrayLimit(r, l) {
  var t = null == r ? null : "undefined" != typeof Symbol && r[Symbol.iterator] || r["@@iterator"];
  if (null != t) {
    var e, n, i, u, a = [], f = true, o = false;
    try {
      if (i = (t = t.call(r)).next, 0 === l) {
        if (Object(t) !== t) return;
        f = false;
      } else for (; !(f = (e = i.call(t)).done) && (a.push(e.value), a.length !== l); f = true) ;
    } catch (r2) {
      o = true, n = r2;
    } finally {
      try {
        if (!f && null != t["return"] && (u = t["return"](), Object(u) !== u)) return;
      } finally {
        if (o) throw n;
      }
    }
    return a;
  }
}
function _arrayLikeToArray(r, a) {
  (null == a || a > r.length) && (a = r.length);
  for (var e = 0, n = Array(a); e < a; e++) n[e] = r[e];
  return n;
}
function _unsupportedIterableToArray(r, a) {
  if (r) {
    if ("string" == typeof r) return _arrayLikeToArray(r, a);
    var t = {}.toString.call(r).slice(8, -1);
    return "Object" === t && r.constructor && (t = r.constructor.name), "Map" === t || "Set" === t ? Array.from(r) : "Arguments" === t || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t) ? _arrayLikeToArray(r, a) : void 0;
  }
}
function _nonIterableRest() {
  throw new TypeError("Invalid attempt to destructure non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
}
function _slicedToArray(r, e) {
  return _arrayWithHoles(r) || _iterableToArrayLimit(r, e) || _unsupportedIterableToArray(r, e) || _nonIterableRest();
}
var classes = {
  root: "p-blockui-container",
  mask: function mask(_ref) {
    var props = _ref.props;
    return classNames("p-blockui p-component-overlay p-component-overlay-enter", {
      "p-blockui-document": props.fullScreen
    });
  }
};
var styles = "\n@layer primereact {\n    .p-blockui-container {\n        position: relative;\n    }\n    \n    .p-blockui {\n        opacity: 1;\n        display: flex;\n        align-items: center;\n        justify-content: center;\n    }\n    \n    .p-blockui.p-component-overlay {\n        position: absolute;\n    }\n    \n    .p-blockui-document.p-component-overlay {\n        position: fixed;\n    }\n}\n";
var BlockUIBase = ComponentBase.extend({
  defaultProps: {
    __TYPE: "BlockUI",
    autoZIndex: true,
    baseZIndex: 0,
    blocked: false,
    className: null,
    containerClassName: null,
    containerStyle: null,
    fullScreen: false,
    id: null,
    onBlocked: null,
    onUnblocked: null,
    style: null,
    template: null,
    children: void 0
  },
  css: {
    classes,
    styles
  }
});
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
var BlockUI = /* @__PURE__ */ React.forwardRef(function(inProps, ref) {
  var mergeProps = useMergeProps();
  var context = React.useContext(PrimeReactContext);
  var props = BlockUIBase.getProps(inProps, context);
  var _React$useState = React.useState(props.blocked), _React$useState2 = _slicedToArray(_React$useState, 2), visibleState = _React$useState2[0], setVisibleState = _React$useState2[1];
  var elementRef = React.useRef(null);
  var maskRef = React.useRef(null);
  var activeElementRef = React.useRef(null);
  var _BlockUIBase$setMetaD = BlockUIBase.setMetaData({
    props
  }), ptm = _BlockUIBase$setMetaD.ptm, cx = _BlockUIBase$setMetaD.cx, isUnstyled = _BlockUIBase$setMetaD.isUnstyled;
  useHandleStyle(BlockUIBase.css.styles, isUnstyled, {
    name: "blockui"
  });
  var block = function block2() {
    setVisibleState(true);
    activeElementRef.current = document.activeElement;
  };
  var unblock = function unblock2() {
    !isUnstyled() && DomHandler.addClass(maskRef.current, "p-component-overlay-leave");
    if (DomHandler.hasCSSAnimation(maskRef.current) > 0) {
      maskRef.current.addEventListener("animationend", function() {
        removeMask();
      });
    } else {
      removeMask();
    }
  };
  var removeMask = function removeMask2() {
    ZIndexUtils.clear(maskRef.current);
    setVisibleState(false);
    if (props.fullScreen) {
      DomHandler.unblockBodyScroll();
      activeElementRef.current && activeElementRef.current.focus();
    }
    props.onUnblocked && props.onUnblocked();
  };
  var onPortalMounted = function onPortalMounted2() {
    if (props.fullScreen) {
      DomHandler.blockBodyScroll();
      activeElementRef.current && activeElementRef.current.blur();
    }
    if (props.autoZIndex) {
      var key = props.fullScreen ? "modal" : "overlay";
      ZIndexUtils.set(key, maskRef.current, context && context.autoZIndex || PrimeReact.autoZIndex, props.baseZIndex || context && context.zIndex[key] || PrimeReact.zIndex[key]);
    }
    props.onBlocked && props.onBlocked();
  };
  useMountEffect(function() {
    visibleState && block();
  });
  useUpdateEffect(function() {
    props.blocked ? block() : unblock();
  }, [props.blocked]);
  useUnmountEffect(function() {
    props.fullScreen && DomHandler.unblockBodyScroll();
    ZIndexUtils.clear(maskRef.current);
  });
  React.useImperativeHandle(ref, function() {
    return {
      props,
      block,
      unblock,
      getElement: function getElement() {
        return elementRef.current;
      }
    };
  });
  var createMask = function createMask2() {
    if (visibleState) {
      var appendTo = props.fullScreen ? document.body : "self";
      var maskProps = mergeProps({
        className: classNames(props.className, cx("mask")),
        style: _objectSpread(_objectSpread({}, props.style), {}, {
          position: props.fullScreen ? "fixed" : "absolute",
          top: "0",
          left: "0",
          width: "100%",
          height: "100%"
        })
      }, ptm("mask"));
      var content = props.template ? ObjectUtils.getJSXElement(props.template, props) : null;
      var _mask = /* @__PURE__ */ React.createElement("div", _extends({
        ref: maskRef
      }, maskProps), content);
      return /* @__PURE__ */ React.createElement(Portal, {
        element: _mask,
        appendTo,
        onMounted: onPortalMounted
      });
    }
    return null;
  };
  var mask2 = createMask();
  var rootProps = mergeProps({
    id: props.id,
    ref: elementRef,
    style: props.containerStyle,
    className: classNames(props.containerClassName, cx("root")),
    "aria-busy": props.blocked
  }, BlockUIBase.getOtherProps(props), ptm("root"));
  return /* @__PURE__ */ React.createElement("div", rootProps, props.children, mask2);
});
BlockUI.displayName = "BlockUI";
export {
  BlockUI as default
};
