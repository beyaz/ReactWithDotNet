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
  useMergeProps,
  useMountEffect
} from "./chunk-NBA33TRK.js";
import "./chunk-QNQS7X5M.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/primereact/inputswitch/inputswitch.esm.js
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
    var props = _ref.props, checked = _ref.checked;
    return classNames("p-inputswitch p-component", {
      "p-highlight": checked,
      "p-disabled": props.disabled,
      "p-invalid": props.invalid
    });
  },
  input: "p-inputswitch-input",
  slider: "p-inputswitch-slider"
};
var InputSwitchBase = ComponentBase.extend({
  defaultProps: {
    __TYPE: "InputSwitch",
    autoFocus: false,
    checked: false,
    className: null,
    disabled: false,
    falseValue: false,
    id: null,
    inputId: null,
    inputRef: null,
    invalid: false,
    name: null,
    onBlur: null,
    onChange: null,
    onFocus: null,
    style: null,
    tabIndex: null,
    tooltip: null,
    tooltipOptions: null,
    trueValue: true,
    children: void 0
  },
  css: {
    classes
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
var InputSwitch = /* @__PURE__ */ React.memo(/* @__PURE__ */ React.forwardRef(function(inProps, ref) {
  var mergeProps = useMergeProps();
  var context = React.useContext(PrimeReactContext);
  var props = InputSwitchBase.getProps(inProps, context);
  var _InputSwitchBase$setM = InputSwitchBase.setMetaData({
    props
  }), ptm = _InputSwitchBase$setM.ptm, cx = _InputSwitchBase$setM.cx, isUnstyled = _InputSwitchBase$setM.isUnstyled;
  useHandleStyle(InputSwitchBase.css.styles, isUnstyled, {
    name: "inputswitch"
  });
  var elementRef = React.useRef(null);
  var inputRef = React.useRef(props.inputRef);
  var checked = props.checked === props.trueValue;
  var onChange = function onChange2(event) {
    if (props.onChange) {
      var value = checked ? props.falseValue : props.trueValue;
      props.onChange({
        originalEvent: event,
        value,
        stopPropagation: function stopPropagation() {
          event === null || event === void 0 || event.stopPropagation();
        },
        preventDefault: function preventDefault() {
          event === null || event === void 0 || event.preventDefault();
        },
        target: {
          name: props.name,
          id: props.id,
          value
        }
      });
    }
  };
  var onFocus = function onFocus2(event) {
    var _props$onFocus;
    props === null || props === void 0 || (_props$onFocus = props.onFocus) === null || _props$onFocus === void 0 || _props$onFocus.call(props, event);
  };
  var onBlur = function onBlur2(event) {
    var _props$onBlur;
    props === null || props === void 0 || (_props$onBlur = props.onBlur) === null || _props$onBlur === void 0 || _props$onBlur.call(props, event);
  };
  React.useImperativeHandle(ref, function() {
    return {
      props,
      focus: function focus() {
        return DomHandler.focus(inputRef.current);
      },
      getElement: function getElement() {
        return elementRef.current;
      },
      getInput: function getInput() {
        return inputRef.current;
      }
    };
  });
  React.useEffect(function() {
    ObjectUtils.combinedRefs(inputRef, props.inputRef);
  }, [inputRef, props.inputRef]);
  useMountEffect(function() {
    if (props.autoFocus) {
      DomHandler.focus(inputRef.current, props.autoFocus);
    }
  });
  var hasTooltip = ObjectUtils.isNotEmpty(props.tooltip);
  var otherProps = InputSwitchBase.getOtherProps(props);
  var ariaProps = ObjectUtils.reduceKeys(otherProps, DomHandler.ARIA_PROPS);
  var rootProps = mergeProps({
    className: classNames(props.className, cx("root", {
      checked
    })),
    style: props.style,
    role: "checkbox",
    "aria-checked": checked,
    "data-p-highlight": checked,
    "data-p-disabled": props.disabled
  }, otherProps, ptm("root"));
  var inputProps = mergeProps(_objectSpread({
    type: "checkbox",
    id: props.inputId,
    name: props.name,
    checked,
    onChange,
    onFocus,
    onBlur,
    disabled: props.disabled,
    role: "switch",
    tabIndex: props.tabIndex,
    "aria-checked": checked,
    className: cx("input")
  }, ariaProps), ptm("input"));
  var sliderProps = mergeProps({
    className: cx("slider")
  }, ptm("slider"));
  return /* @__PURE__ */ React.createElement(React.Fragment, null, /* @__PURE__ */ React.createElement("div", _extends({
    id: props.id,
    ref: elementRef
  }, rootProps), /* @__PURE__ */ React.createElement("input", _extends({
    ref: inputRef
  }, inputProps)), /* @__PURE__ */ React.createElement("span", sliderProps)), hasTooltip && /* @__PURE__ */ React.createElement(Tooltip, _extends({
    target: elementRef,
    content: props.tooltip,
    pt: ptm("tooltip")
  }, props.tooltipOptions)));
}));
InputSwitch.displayName = "InputSwitch";
export {
  InputSwitch as default
};
