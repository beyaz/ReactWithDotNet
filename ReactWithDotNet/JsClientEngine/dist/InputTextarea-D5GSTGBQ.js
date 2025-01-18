import {
  KeyFilter
} from "./chunk-WPUEZBIL.js";
import {
  Tooltip
} from "./chunk-2PX4WDKA.js";
import "./chunk-CMYF6AIZ.js";
import {
  ComponentBase,
  DomHandler,
  ObjectUtils,
  PrimeReactContext,
  classNames,
  useHandleStyle,
  useMergeProps
} from "./chunk-NBA33TRK.js";
import "./chunk-QNQS7X5M.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/primereact/inputtextarea/inputtextarea.esm.js
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
var classes = {
  root: function root(_ref) {
    var props = _ref.props, context = _ref.context, isFilled = _ref.isFilled;
    return classNames("p-inputtextarea p-inputtext p-component", {
      "p-disabled": props.disabled,
      "p-filled": isFilled,
      "p-inputtextarea-resizable": props.autoResize,
      "p-invalid": props.invalid,
      "p-variant-filled": props.variant ? props.variant === "filled" : context && context.inputStyle === "filled"
    });
  }
};
var styles = "\n@layer primereact {\n    .p-inputtextarea-resizable {\n        overflow: hidden;\n        resize: none;\n    }\n    \n    .p-fluid .p-inputtextarea {\n        width: 100%;\n    }\n}\n";
var InputTextareaBase = ComponentBase.extend({
  defaultProps: {
    __TYPE: "InputTextarea",
    __parentMetadata: null,
    autoResize: false,
    invalid: false,
    variant: null,
    keyfilter: null,
    onBlur: null,
    onFocus: null,
    onBeforeInput: null,
    onInput: null,
    onKeyDown: null,
    onKeyUp: null,
    onPaste: null,
    tooltip: null,
    tooltipOptions: null,
    validateOnly: false,
    children: void 0,
    className: null
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
var InputTextarea = /* @__PURE__ */ React.memo(/* @__PURE__ */ React.forwardRef(function(inProps, ref) {
  var mergeProps = useMergeProps();
  var context = React.useContext(PrimeReactContext);
  var props = InputTextareaBase.getProps(inProps, context);
  var elementRef = React.useRef(ref);
  var cachedScrollHeight = React.useRef(0);
  var _InputTextareaBase$se = InputTextareaBase.setMetaData(_objectSpread(_objectSpread({
    props
  }, props.__parentMetadata), {}, {
    context: {
      disabled: props.disabled
    }
  })), ptm = _InputTextareaBase$se.ptm, cx = _InputTextareaBase$se.cx, isUnstyled = _InputTextareaBase$se.isUnstyled;
  useHandleStyle(InputTextareaBase.css.styles, isUnstyled, {
    name: "inputtextarea"
  });
  var onFocus = function onFocus2(event) {
    if (props.autoResize) {
      resize();
    }
    props.onFocus && props.onFocus(event);
  };
  var onBlur = function onBlur2(event) {
    if (props.autoResize) {
      resize();
    }
    props.onBlur && props.onBlur(event);
  };
  var onKeyUp = function onKeyUp2(event) {
    if (props.autoResize) {
      resize();
    }
    props.onKeyUp && props.onKeyUp(event);
  };
  var onKeyDown = function onKeyDown2(event) {
    props.onKeyDown && props.onKeyDown(event);
    if (props.keyfilter) {
      KeyFilter.onKeyPress(event, props.keyfilter, props.validateOnly);
    }
  };
  var onBeforeInput = function onBeforeInput2(event) {
    props.onBeforeInput && props.onBeforeInput(event);
    if (props.keyfilter) {
      KeyFilter.onBeforeInput(event, props.keyfilter, props.validateOnly);
    }
  };
  var onPaste = function onPaste2(event) {
    props.onPaste && props.onPaste(event);
    if (props.keyfilter) {
      KeyFilter.onPaste(event, props.keyfilter, props.validateOnly);
    }
  };
  var onInput = function onInput2(event) {
    var target = event.target;
    if (props.autoResize) {
      resize(ObjectUtils.isEmpty(target.value));
    }
    props.onInput && props.onInput(event);
    ObjectUtils.isNotEmpty(target.value) ? DomHandler.addClass(target, "p-filled") : DomHandler.removeClass(target, "p-filled");
  };
  var resize = function resize2(initial) {
    var inputEl = elementRef.current;
    if (inputEl && DomHandler.isVisible(inputEl)) {
      if (!cachedScrollHeight.current) {
        cachedScrollHeight.current = inputEl.scrollHeight;
        inputEl.style.overflow = "hidden";
      }
      if (cachedScrollHeight.current !== inputEl.scrollHeight || initial) {
        inputEl.style.height = "";
        inputEl.style.height = inputEl.scrollHeight + "px";
        if (parseFloat(inputEl.style.height) >= parseFloat(inputEl.style.maxHeight)) {
          inputEl.style.overflowY = "scroll";
          inputEl.style.height = inputEl.style.maxHeight;
        } else {
          inputEl.style.overflow = "hidden";
        }
        cachedScrollHeight.current = inputEl.scrollHeight;
      }
    }
  };
  React.useEffect(function() {
    ObjectUtils.combinedRefs(elementRef, ref);
  }, [elementRef, ref]);
  React.useEffect(function() {
    if (props.autoResize) {
      resize(true);
    }
  }, [props.autoResize]);
  var isFilled = React.useMemo(function() {
    return ObjectUtils.isNotEmpty(props.value) || ObjectUtils.isNotEmpty(props.defaultValue);
  }, [props.value, props.defaultValue]);
  var hasTooltip = ObjectUtils.isNotEmpty(props.tooltip);
  var rootProps = mergeProps({
    ref: elementRef,
    className: classNames(props.className, cx("root", {
      context,
      isFilled
    })),
    onFocus,
    onBlur,
    onKeyUp,
    onKeyDown,
    onBeforeInput,
    onInput,
    onPaste
  }, InputTextareaBase.getOtherProps(props), ptm("root"));
  return /* @__PURE__ */ React.createElement(React.Fragment, null, /* @__PURE__ */ React.createElement("textarea", rootProps), hasTooltip && /* @__PURE__ */ React.createElement(Tooltip, _extends({
    target: elementRef,
    content: props.tooltip,
    pt: ptm("tooltip")
  }, props.tooltipOptions)));
}));
InputTextarea.displayName = "InputTextarea";
export {
  InputTextarea as default
};
