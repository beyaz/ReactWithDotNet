import {
  SwiperSlide
} from "./chunk-E4SL5VQW.js";
import {
  motion
} from "./chunk-Z4BSGXGW.js";
import {
  ChevronRightIcon
} from "./chunk-WVAF3FHQ.js";
import {
  TimesIcon
} from "./chunk-Z36BX2W4.js";
import {
  Typography_default
} from "./chunk-WEZRGM2G.js";
import {
  Ripple
} from "./chunk-56TI4EJO.js";
import {
  IconBase
} from "./chunk-2FIYIUK5.js";
import {
  ComponentBase,
  DomHandler,
  IconUtils,
  ObjectUtils,
  PrimeReactContext,
  UniqueComponentId,
  ariaLabel,
  classNames,
  useHandleStyle,
  useMergeProps,
  useMountEffect,
  useUpdateEffect
} from "./chunk-NBA33TRK.js";
import "./chunk-EHPEYKPY.js";
import "./chunk-AYGLMMKX.js";
import {
  react_with_dotnet_default
} from "./chunk-32Z5PYVM.js";
import {
  useSlot
} from "./chunk-E74I5C76.js";
import "./chunk-QNQS7X5M.js";
import "./chunk-ITRKCCZH.js";
import {
  formControlState
} from "./chunk-AFZHPMRE.js";
import {
  useFormControl
} from "./chunk-DLXPPBKX.js";
import "./chunk-LSFN63OR.js";
import {
  refType_default
} from "./chunk-AQXVDT6X.js";
import {
  capitalize_default
} from "./chunk-7XH2SCBW.js";
import {
  memoTheme_default
} from "./chunk-VZKQEGQI.js";
import {
  composeClasses,
  generateUtilityClass,
  generateUtilityClasses,
  styled_default,
  useDefaultProps
} from "./chunk-L3PWPRGS.js";
import {
  clsx_default
} from "./chunk-7PXK4W6T.js";
import "./chunk-QN5L25IB.js";
import "./chunk-JHFQ3DUR.js";
import "./chunk-P3GLDRUK.js";
import {
  require_jsx_runtime
} from "./chunk-7E54WPGA.js";
import "./chunk-SEPJ2F45.js";
import {
  require_prop_types
} from "./chunk-FLAFLVMN.js";
import "./chunk-IHPEMKKY.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// react-with-dotnet/libraries/mui-core/all.jsx
var import_react = __toESM(require_react());

// node_modules/@mui/material/FormGroup/FormGroup.js
var React = __toESM(require_react());
var import_prop_types = __toESM(require_prop_types());

// node_modules/@mui/material/FormGroup/formGroupClasses.js
function getFormGroupUtilityClass(slot) {
  return generateUtilityClass("MuiFormGroup", slot);
}
var formGroupClasses = generateUtilityClasses("MuiFormGroup", ["root", "row", "error"]);

// node_modules/@mui/material/FormGroup/FormGroup.js
var import_jsx_runtime = __toESM(require_jsx_runtime());
var useUtilityClasses = (ownerState) => {
  const {
    classes: classes2,
    row,
    error
  } = ownerState;
  const slots = {
    root: ["root", row && "row", error && "error"]
  };
  return composeClasses(slots, getFormGroupUtilityClass, classes2);
};
var FormGroupRoot = styled_default("div", {
  name: "MuiFormGroup",
  slot: "Root",
  overridesResolver: (props, styles) => {
    const {
      ownerState
    } = props;
    return [styles.root, ownerState.row && styles.row];
  }
})({
  display: "flex",
  flexDirection: "column",
  flexWrap: "wrap",
  variants: [{
    props: {
      row: true
    },
    style: {
      flexDirection: "row"
    }
  }]
});
var FormGroup = /* @__PURE__ */ React.forwardRef(function FormGroup2(inProps, ref) {
  const props = useDefaultProps({
    props: inProps,
    name: "MuiFormGroup"
  });
  const {
    className,
    row = false,
    ...other
  } = props;
  const muiFormControl = useFormControl();
  const fcs = formControlState({
    props,
    muiFormControl,
    states: ["error"]
  });
  const ownerState = {
    ...props,
    row,
    error: fcs.error
  };
  const classes2 = useUtilityClasses(ownerState);
  return /* @__PURE__ */ (0, import_jsx_runtime.jsx)(FormGroupRoot, {
    className: clsx_default(classes2.root, className),
    ownerState,
    ref,
    ...other
  });
});
true ? FormGroup.propTypes = {
  // ┌────────────────────────────── Warning ──────────────────────────────┐
  // │ These PropTypes are generated from the TypeScript type definitions. │
  // │    To update them, edit the d.ts file and run `pnpm proptypes`.     │
  // └─────────────────────────────────────────────────────────────────────┘
  /**
   * The content of the component.
   */
  children: import_prop_types.default.node,
  /**
   * Override or extend the styles applied to the component.
   */
  classes: import_prop_types.default.object,
  /**
   * @ignore
   */
  className: import_prop_types.default.string,
  /**
   * Display group of elements in a compact row.
   * @default false
   */
  row: import_prop_types.default.bool,
  /**
   * The system prop that allows defining system overrides as well as additional CSS styles.
   */
  sx: import_prop_types.default.oneOfType([import_prop_types.default.arrayOf(import_prop_types.default.oneOfType([import_prop_types.default.func, import_prop_types.default.object, import_prop_types.default.bool])), import_prop_types.default.func, import_prop_types.default.object])
} : void 0;
var FormGroup_default = FormGroup;

// node_modules/@mui/material/FormControlLabel/FormControlLabel.js
var React2 = __toESM(require_react());
var import_prop_types2 = __toESM(require_prop_types());

// node_modules/@mui/material/FormControlLabel/formControlLabelClasses.js
function getFormControlLabelUtilityClasses(slot) {
  return generateUtilityClass("MuiFormControlLabel", slot);
}
var formControlLabelClasses = generateUtilityClasses("MuiFormControlLabel", ["root", "labelPlacementStart", "labelPlacementTop", "labelPlacementBottom", "disabled", "label", "error", "required", "asterisk"]);
var formControlLabelClasses_default = formControlLabelClasses;

// node_modules/@mui/material/FormControlLabel/FormControlLabel.js
var import_jsx_runtime2 = __toESM(require_jsx_runtime());
var useUtilityClasses2 = (ownerState) => {
  const {
    classes: classes2,
    disabled,
    labelPlacement,
    error,
    required
  } = ownerState;
  const slots = {
    root: ["root", disabled && "disabled", `labelPlacement${capitalize_default(labelPlacement)}`, error && "error", required && "required"],
    label: ["label", disabled && "disabled"],
    asterisk: ["asterisk", error && "error"]
  };
  return composeClasses(slots, getFormControlLabelUtilityClasses, classes2);
};
var FormControlLabelRoot = styled_default("label", {
  name: "MuiFormControlLabel",
  slot: "Root",
  overridesResolver: (props, styles) => {
    const {
      ownerState
    } = props;
    return [{
      [`& .${formControlLabelClasses_default.label}`]: styles.label
    }, styles.root, styles[`labelPlacement${capitalize_default(ownerState.labelPlacement)}`]];
  }
})(memoTheme_default(({
  theme
}) => ({
  display: "inline-flex",
  alignItems: "center",
  cursor: "pointer",
  // For correct alignment with the text.
  verticalAlign: "middle",
  WebkitTapHighlightColor: "transparent",
  marginLeft: -11,
  marginRight: 16,
  // used for row presentation of radio/checkbox
  [`&.${formControlLabelClasses_default.disabled}`]: {
    cursor: "default"
  },
  [`& .${formControlLabelClasses_default.label}`]: {
    [`&.${formControlLabelClasses_default.disabled}`]: {
      color: (theme.vars || theme).palette.text.disabled
    }
  },
  variants: [{
    props: {
      labelPlacement: "start"
    },
    style: {
      flexDirection: "row-reverse",
      marginRight: -11
    }
  }, {
    props: {
      labelPlacement: "top"
    },
    style: {
      flexDirection: "column-reverse"
    }
  }, {
    props: {
      labelPlacement: "bottom"
    },
    style: {
      flexDirection: "column"
    }
  }, {
    props: ({
      labelPlacement
    }) => labelPlacement === "start" || labelPlacement === "top" || labelPlacement === "bottom",
    style: {
      marginLeft: 16
      // used for row presentation of radio/checkbox
    }
  }]
})));
var AsteriskComponent = styled_default("span", {
  name: "MuiFormControlLabel",
  slot: "Asterisk",
  overridesResolver: (props, styles) => styles.asterisk
})(memoTheme_default(({
  theme
}) => ({
  [`&.${formControlLabelClasses_default.error}`]: {
    color: (theme.vars || theme).palette.error.main
  }
})));
var FormControlLabel = /* @__PURE__ */ React2.forwardRef(function FormControlLabel2(inProps, ref) {
  const props = useDefaultProps({
    props: inProps,
    name: "MuiFormControlLabel"
  });
  const {
    checked,
    className,
    componentsProps = {},
    control,
    disabled: disabledProp,
    disableTypography,
    inputRef,
    label: labelProp,
    labelPlacement = "end",
    name,
    onChange,
    required: requiredProp,
    slots = {},
    slotProps = {},
    value,
    ...other
  } = props;
  const muiFormControl = useFormControl();
  const disabled = disabledProp ?? control.props.disabled ?? muiFormControl?.disabled;
  const required = requiredProp ?? control.props.required;
  const controlProps = {
    disabled,
    required
  };
  ["checked", "name", "onChange", "value", "inputRef"].forEach((key) => {
    if (typeof control.props[key] === "undefined" && typeof props[key] !== "undefined") {
      controlProps[key] = props[key];
    }
  });
  const fcs = formControlState({
    props,
    muiFormControl,
    states: ["error"]
  });
  const ownerState = {
    ...props,
    disabled,
    labelPlacement,
    required,
    error: fcs.error
  };
  const classes2 = useUtilityClasses2(ownerState);
  const externalForwardedProps = {
    slots,
    slotProps: {
      ...componentsProps,
      ...slotProps
    }
  };
  const [TypographySlot, typographySlotProps] = useSlot("typography", {
    elementType: Typography_default,
    externalForwardedProps,
    ownerState
  });
  let label = labelProp;
  if (label != null && label.type !== Typography_default && !disableTypography) {
    label = /* @__PURE__ */ (0, import_jsx_runtime2.jsx)(TypographySlot, {
      component: "span",
      ...typographySlotProps,
      className: clsx_default(classes2.label, typographySlotProps?.className),
      children: label
    });
  }
  return /* @__PURE__ */ (0, import_jsx_runtime2.jsxs)(FormControlLabelRoot, {
    className: clsx_default(classes2.root, className),
    ownerState,
    ref,
    ...other,
    children: [/* @__PURE__ */ React2.cloneElement(control, controlProps), required ? /* @__PURE__ */ (0, import_jsx_runtime2.jsxs)("div", {
      children: [label, /* @__PURE__ */ (0, import_jsx_runtime2.jsxs)(AsteriskComponent, {
        ownerState,
        "aria-hidden": true,
        className: classes2.asterisk,
        children: ["\u2009", "*"]
      })]
    }) : label]
  });
});
true ? FormControlLabel.propTypes = {
  // ┌────────────────────────────── Warning ──────────────────────────────┐
  // │ These PropTypes are generated from the TypeScript type definitions. │
  // │    To update them, edit the d.ts file and run `pnpm proptypes`.     │
  // └─────────────────────────────────────────────────────────────────────┘
  /**
   * If `true`, the component appears selected.
   */
  checked: import_prop_types2.default.bool,
  /**
   * Override or extend the styles applied to the component.
   */
  classes: import_prop_types2.default.object,
  /**
   * @ignore
   */
  className: import_prop_types2.default.string,
  /**
   * The props used for each slot inside.
   * @default {}
   * @deprecated use the `slotProps` prop instead. This prop will be removed in v7. See [Migrating from deprecated APIs](https://mui.com/material-ui/migration/migrating-from-deprecated-apis/) for more details.
   */
  componentsProps: import_prop_types2.default.shape({
    typography: import_prop_types2.default.object
  }),
  /**
   * A control element. For instance, it can be a `Radio`, a `Switch` or a `Checkbox`.
   */
  control: import_prop_types2.default.element.isRequired,
  /**
   * If `true`, the control is disabled.
   */
  disabled: import_prop_types2.default.bool,
  /**
   * If `true`, the label is rendered as it is passed without an additional typography node.
   */
  disableTypography: import_prop_types2.default.bool,
  /**
   * Pass a ref to the `input` element.
   */
  inputRef: refType_default,
  /**
   * A text or an element to be used in an enclosing label element.
   */
  label: import_prop_types2.default.node,
  /**
   * The position of the label.
   * @default 'end'
   */
  labelPlacement: import_prop_types2.default.oneOf(["bottom", "end", "start", "top"]),
  /**
   * @ignore
   */
  name: import_prop_types2.default.string,
  /**
   * Callback fired when the state is changed.
   *
   * @param {React.SyntheticEvent} event The event source of the callback.
   * You can pull out the new checked state by accessing `event.target.checked` (boolean).
   */
  onChange: import_prop_types2.default.func,
  /**
   * If `true`, the label will indicate that the `input` is required.
   */
  required: import_prop_types2.default.bool,
  /**
   * The props used for each slot inside.
   * @default {}
   */
  slotProps: import_prop_types2.default.shape({
    typography: import_prop_types2.default.oneOfType([import_prop_types2.default.func, import_prop_types2.default.object])
  }),
  /**
   * The components used for each slot inside.
   * @default {}
   */
  slots: import_prop_types2.default.shape({
    typography: import_prop_types2.default.elementType
  }),
  /**
   * The system prop that allows defining system overrides as well as additional CSS styles.
   */
  sx: import_prop_types2.default.oneOfType([import_prop_types2.default.arrayOf(import_prop_types2.default.oneOfType([import_prop_types2.default.func, import_prop_types2.default.object, import_prop_types2.default.bool])), import_prop_types2.default.func, import_prop_types2.default.object]),
  /**
   * The value of the component.
   */
  value: import_prop_types2.default.any
} : void 0;
var FormControlLabel_default = FormControlLabel;

// react-with-dotnet/libraries/mui-core/all.jsx
function register(name, value) {
  react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.MUI.Material." + name, value);
}
register("Accordion", import_react.default.lazy(() => import("./Accordion-VDMWSSXP.js")));
register("Autocomplete", import_react.default.lazy(() => import("./Autocomplete-DMDCQGWU.js")));
register("Switch", import_react.default.lazy(() => import("./Switch-PDB7JZMM.js")));
register("Tooltip", import_react.default.lazy(() => import("./Tooltip-5EX57QEN.js")));
register("Button", import_react.default.lazy(() => import("./Button-TJLTASDD.js")));
register("Input", import_react.default.lazy(() => import("./Input-NY6IB7EQ.js")));
register("InputBase", import_react.default.lazy(() => import("./InputBase-Q5UYCP46.js")));
register("Paper", import_react.default.lazy(() => import("./Paper-3B72ZXZP.js")));
register("Divider", import_react.default.lazy(() => import("./Divider-COKBM4DQ.js")));
register("IconButton", import_react.default.lazy(() => import("./IconButton-TLMKCJ3N.js")));
register("TextField", import_react.default.lazy(() => import("./TextField-HOUCRCZH.js")));
register("CardMedia", import_react.default.lazy(() => import("./CardMedia-SMSCQ5Q5.js")));
register("Card", import_react.default.lazy(() => import("./Card-UDIMLMD7.js")));
register("CardContent", import_react.default.lazy(() => import("./CardContent-SLDAXL4A.js")));
register("CardActions", import_react.default.lazy(() => import("./CardActions-QHOMFMO3.js")));
register("Typography", import_react.default.lazy(() => import("./Typography-LHMCU527.js")));
register("CircularProgress", import_react.default.lazy(() => import("./CircularProgress-ADTGV5HI.js")));
register("Slider", import_react.default.lazy(() => import("./Slider-WWKX4XPG.js")));
register("FormGroup", FormGroup_default);
register("FormControlLabel", FormControlLabel_default);

// react-with-dotnet/libraries/google-map-react/all.jsx
var import_react2 = __toESM(require_react());
var GoogleMap = import_react2.default.lazy(() => import("./GoogleMap-XQBPVIQL.js"));
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.GoogleMapReact.GoogleMap", GoogleMap);

// react-with-dotnet/libraries/primereact/all.jsx
var import_react3 = __toESM(require_react());

// node_modules/primereact/tabview/tabview.esm.js
var React6 = __toESM(require_react());

// node_modules/primereact/icons/chevronleft/index.esm.js
var React5 = __toESM(require_react());
function _extends() {
  return _extends = Object.assign ? Object.assign.bind() : function(n) {
    for (var e = 1; e < arguments.length; e++) {
      var t = arguments[e];
      for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]);
    }
    return n;
  }, _extends.apply(null, arguments);
}
var ChevronLeftIcon = /* @__PURE__ */ React5.memo(/* @__PURE__ */ React5.forwardRef(function(inProps, ref) {
  var pti = IconBase.getPTI(inProps);
  return /* @__PURE__ */ React5.createElement("svg", _extends({
    ref,
    width: "14",
    height: "14",
    viewBox: "0 0 14 14",
    fill: "none",
    xmlns: "http://www.w3.org/2000/svg"
  }, pti), /* @__PURE__ */ React5.createElement("path", {
    d: "M9.61296 13C9.50997 13.0005 9.40792 12.9804 9.3128 12.9409C9.21767 12.9014 9.13139 12.8433 9.05902 12.7701L3.83313 7.54416C3.68634 7.39718 3.60388 7.19795 3.60388 6.99022C3.60388 6.78249 3.68634 6.58325 3.83313 6.43628L9.05902 1.21039C9.20762 1.07192 9.40416 0.996539 9.60724 1.00012C9.81032 1.00371 10.0041 1.08597 10.1477 1.22959C10.2913 1.37322 10.3736 1.56698 10.3772 1.77005C10.3808 1.97313 10.3054 2.16968 10.1669 2.31827L5.49496 6.99022L10.1669 11.6622C10.3137 11.8091 10.3962 12.0084 10.3962 12.2161C10.3962 12.4238 10.3137 12.6231 10.1669 12.7701C10.0945 12.8433 10.0083 12.9014 9.91313 12.9409C9.81801 12.9804 9.71596 13.0005 9.61296 13Z",
    fill: "currentColor"
  }));
}));
ChevronLeftIcon.displayName = "ChevronLeftIcon";

// node_modules/primereact/tabview/tabview.esm.js
function _arrayLikeToArray(r, a) {
  (null == a || a > r.length) && (a = r.length);
  for (var e = 0, n = Array(a); e < a; e++) n[e] = r[e];
  return n;
}
function _arrayWithoutHoles(r) {
  if (Array.isArray(r)) return _arrayLikeToArray(r);
}
function _iterableToArray(r) {
  if ("undefined" != typeof Symbol && null != r[Symbol.iterator] || null != r["@@iterator"]) return Array.from(r);
}
function _unsupportedIterableToArray(r, a) {
  if (r) {
    if ("string" == typeof r) return _arrayLikeToArray(r, a);
    var t = {}.toString.call(r).slice(8, -1);
    return "Object" === t && r.constructor && (t = r.constructor.name), "Map" === t || "Set" === t ? Array.from(r) : "Arguments" === t || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t) ? _arrayLikeToArray(r, a) : void 0;
  }
}
function _nonIterableSpread() {
  throw new TypeError("Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
}
function _toConsumableArray(r) {
  return _arrayWithoutHoles(r) || _iterableToArray(r) || _unsupportedIterableToArray(r) || _nonIterableSpread();
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
function _nonIterableRest() {
  throw new TypeError("Invalid attempt to destructure non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
}
function _slicedToArray(r, e) {
  return _arrayWithHoles(r) || _iterableToArrayLimit(r, e) || _unsupportedIterableToArray(r, e) || _nonIterableRest();
}
function ownKeys$1(e, r) {
  var t = Object.keys(e);
  if (Object.getOwnPropertySymbols) {
    var o = Object.getOwnPropertySymbols(e);
    r && (o = o.filter(function(r2) {
      return Object.getOwnPropertyDescriptor(e, r2).enumerable;
    })), t.push.apply(t, o);
  }
  return t;
}
function _objectSpread$1(e) {
  for (var r = 1; r < arguments.length; r++) {
    var t = null != arguments[r] ? arguments[r] : {};
    r % 2 ? ownKeys$1(Object(t), true).forEach(function(r2) {
      _defineProperty(e, r2, t[r2]);
    }) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : ownKeys$1(Object(t)).forEach(function(r2) {
      Object.defineProperty(e, r2, Object.getOwnPropertyDescriptor(t, r2));
    });
  }
  return e;
}
var classes = {
  navcontent: "p-tabview-nav-content",
  nav: "p-tabview-nav",
  inkbar: "p-tabview-ink-bar",
  panelcontainer: function panelcontainer(_ref) {
    var props = _ref.props;
    return classNames("p-tabview-panels", props.panelContainerClassName);
  },
  prevbutton: "p-tabview-nav-prev p-tabview-nav-btn p-link",
  nextbutton: "p-tabview-nav-next p-tabview-nav-btn p-link",
  root: function root(_ref2) {
    var props = _ref2.props;
    return classNames("p-tabview p-component", {
      "p-tabview-scrollable": props.scrollable
    });
  },
  navcontainer: "p-tabview-nav-container",
  tab: {
    header: function header(_ref3) {
      var selected = _ref3.selected, disabled = _ref3.disabled, headerClassName = _ref3.headerClassName, _className = _ref3._className;
      return classNames("p-unselectable-text", {
        "p-tabview-selected p-highlight": selected,
        "p-disabled": disabled
      }, headerClassName, _className);
    },
    headertitle: "p-tabview-title",
    headeraction: "p-tabview-nav-link",
    closeIcon: "p-tabview-close",
    content: function content(_ref4) {
      var props = _ref4.props, selected = _ref4.selected, getTabProp = _ref4.getTabProp, tab = _ref4.tab, isSelected = _ref4.isSelected, shouldUseTab = _ref4.shouldUseTab, index = _ref4.index;
      return shouldUseTab(tab, index) && (!props.renderActiveOnly || isSelected(index)) ? classNames(getTabProp(tab, "contentClassName"), getTabProp(tab, "className"), "p-tabview-panel", {
        "p-hidden": !selected
      }) : void 0;
    }
  }
};
var inlineStyles = {
  tab: {
    header: function header2(_ref5) {
      var headerStyle = _ref5.headerStyle, _style = _ref5._style;
      return _objectSpread$1(_objectSpread$1({}, headerStyle || {}), _style || {});
    },
    content: function content2(_ref6) {
      var props = _ref6.props, getTabProp = _ref6.getTabProp, tab = _ref6.tab, isSelected = _ref6.isSelected, shouldUseTab = _ref6.shouldUseTab, index = _ref6.index;
      return shouldUseTab(tab, index) && (!props.renderActiveOnly || isSelected(index)) ? _objectSpread$1(_objectSpread$1({}, getTabProp(tab, "contentStyle") || {}), getTabProp(tab, "style") || {}) : void 0;
    }
  }
};
var TabViewBase = ComponentBase.extend({
  defaultProps: {
    __TYPE: "TabView",
    id: null,
    activeIndex: 0,
    className: null,
    onBeforeTabChange: null,
    onBeforeTabClose: null,
    onTabChange: null,
    onTabClose: null,
    panelContainerClassName: null,
    panelContainerStyle: null,
    renderActiveOnly: true,
    scrollable: false,
    style: null,
    children: void 0
  },
  css: {
    classes,
    inlineStyles
  }
});
var TabPanelBase = ComponentBase.extend({
  defaultProps: {
    __TYPE: "TabPanel",
    children: void 0,
    className: null,
    closable: false,
    closeIcon: null,
    contentClassName: null,
    contentStyle: null,
    disabled: false,
    header: null,
    headerClassName: null,
    headerStyle: null,
    headerTemplate: null,
    leftIcon: null,
    nextButton: null,
    prevButton: null,
    rightIcon: null,
    style: null,
    visible: true
  },
  getCProp: function getCProp(tab, name) {
    return ObjectUtils.getComponentProp(tab, name, TabPanelBase.defaultProps);
  },
  getCProps: function getCProps(tab) {
    return ObjectUtils.getComponentProps(tab, TabPanelBase.defaultProps);
  },
  getCOtherProps: function getCOtherProps(tab) {
    return ObjectUtils.getComponentDiffProps(tab, TabPanelBase.defaultProps);
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
var TabPanel = function TabPanel2() {
};
var TabView = /* @__PURE__ */ React6.forwardRef(function(inProps, ref) {
  var mergeProps = useMergeProps();
  var context = React6.useContext(PrimeReactContext);
  var props = TabViewBase.getProps(inProps, context);
  var _React$useState = React6.useState(props.id), _React$useState2 = _slicedToArray(_React$useState, 2), idState = _React$useState2[0], setIdState = _React$useState2[1];
  var _React$useState3 = React6.useState(true), _React$useState4 = _slicedToArray(_React$useState3, 2), backwardIsDisabledState = _React$useState4[0], setBackwardIsDisabledState = _React$useState4[1];
  var _React$useState5 = React6.useState(false), _React$useState6 = _slicedToArray(_React$useState5, 2), forwardIsDisabledState = _React$useState6[0], setForwardIsDisabledState = _React$useState6[1];
  var _React$useState7 = React6.useState([]), _React$useState8 = _slicedToArray(_React$useState7, 2), hiddenTabsState = _React$useState8[0], setHiddenTabsState = _React$useState8[1];
  var _React$useState9 = React6.useState(props.activeIndex), _React$useState10 = _slicedToArray(_React$useState9, 2), activeIndexState = _React$useState10[0], setActiveIndexState = _React$useState10[1];
  var elementRef = React6.useRef(null);
  var contentRef = React6.useRef(null);
  var navRef = React6.useRef(null);
  var inkbarRef = React6.useRef(null);
  var prevBtnRef = React6.useRef(null);
  var nextBtnRef = React6.useRef(null);
  var tabsRef = React6.useRef({});
  var activeIndex = props.onTabChange ? props.activeIndex : activeIndexState;
  var count = React6.Children.count(props.children);
  var metaData = {
    props,
    state: {
      id: idState,
      isPrevButtonDisabled: backwardIsDisabledState,
      isNextButtonDisabled: forwardIsDisabledState,
      hiddenTabsState,
      activeIndex: activeIndexState
    }
  };
  var _TabViewBase$setMetaD = TabViewBase.setMetaData(_objectSpread({}, metaData)), ptm = _TabViewBase$setMetaD.ptm, ptmo = _TabViewBase$setMetaD.ptmo, cx = _TabViewBase$setMetaD.cx, sx = _TabViewBase$setMetaD.sx, isUnstyled = _TabViewBase$setMetaD.isUnstyled;
  useHandleStyle(TabViewBase.css.styles, isUnstyled, {
    name: "tabview"
  });
  var getTabPT = function getTabPT2(tab, key, index) {
    var tabMetaData = {
      props: tab.props,
      parent: metaData,
      context: {
        index,
        count,
        first: index === 0,
        last: index === count - 1,
        active: index == activeIndexState,
        disabled: getTabProp(tab, "disabled")
      }
    };
    return mergeProps(ptm("tab.".concat(key), {
      tab: tabMetaData
    }), ptm("tabpanel.".concat(key), {
      tabpanel: tabMetaData
    }), ptm("tabpanel.".concat(key), tabMetaData), ptmo(getTabProp(tab, "pt"), key, tabMetaData));
  };
  var isSelected = function isSelected2(index) {
    return index === activeIndex;
  };
  var getTabProp = function getTabProp2(tab, name) {
    return TabPanelBase.getCProp(tab, name);
  };
  var shouldUseTab = function shouldUseTab2(tab) {
    return tab && getTabProp(tab, "visible") && ObjectUtils.isValidChild(tab, "TabPanel") && hiddenTabsState.every(function(_i) {
      return _i !== tab.key;
    });
  };
  var findVisibleActiveTab = function findVisibleActiveTab2(i) {
    var tabsInfo = React6.Children.map(props.children, function(tab, index) {
      if (shouldUseTab(tab)) {
        return {
          tab,
          index
        };
      }
    });
    return tabsInfo.find(function(_ref) {
      var tab = _ref.tab, index = _ref.index;
      return !getTabProp(tab, "disabled") && index >= i;
    }) || tabsInfo.reverse().find(function(_ref2) {
      var tab = _ref2.tab, index = _ref2.index;
      return !getTabProp(tab, "disabled") && i > index;
    });
  };
  var onTabHeaderClose = function onTabHeaderClose2(event, index) {
    event.preventDefault();
    var onBeforeTabClose = props.onBeforeTabClose, onTabClose = props.onTabClose, children = props.children;
    var key = children[index].key;
    if (onBeforeTabClose && onBeforeTabClose({
      originalEvent: event,
      index
    }) === false) {
      return;
    }
    setHiddenTabsState([].concat(_toConsumableArray(hiddenTabsState), [key]));
    if (onTabClose) {
      onTabClose({
        originalEvent: event,
        index
      });
    }
  };
  var onTabHeaderClick = function onTabHeaderClick2(event, tab, index) {
    changeActiveIndex(event, tab, index);
  };
  var changeActiveIndex = function changeActiveIndex2(event, tab, index) {
    if (event) {
      event.preventDefault();
    }
    if (!getTabProp(tab, "disabled")) {
      if (props.onBeforeTabChange && props.onBeforeTabChange({
        originalEvent: event,
        index
      }) === false) {
        return;
      }
      if (props.onTabChange) {
        props.onTabChange({
          originalEvent: event,
          index
        });
      } else {
        setActiveIndexState(index);
      }
    }
    updateScrollBar({
      index
    });
  };
  var _onKeyDown = function onKeyDown(event, tab, index) {
    switch (event.code) {
      case "ArrowLeft":
        onTabArrowLeftKey(event);
        break;
      case "ArrowRight":
        onTabArrowRightKey(event);
        break;
      case "Home":
        onTabHomeKey(event);
        break;
      case "End":
        onTabEndKey(event);
        break;
      case "PageDown":
        onPageDownKey(event);
        break;
      case "PageUp":
        onPageUpKey(event);
        break;
      case "Enter":
      case "NumpadEnter":
      case "Space":
        onTabEnterKey(event, tab, index);
        break;
    }
  };
  var onTabArrowRightKey = function onTabArrowRightKey2(event) {
    var nextHeaderAction = _findNextHeaderAction(event.target.parentElement);
    nextHeaderAction ? changeFocusedTab(nextHeaderAction) : onTabHomeKey(event);
    event.preventDefault();
  };
  var onTabArrowLeftKey = function onTabArrowLeftKey2(event) {
    var prevHeaderAction = _findPrevHeaderAction(event.target.parentElement);
    prevHeaderAction ? changeFocusedTab(prevHeaderAction) : onTabEndKey(event);
    event.preventDefault();
  };
  var onTabHomeKey = function onTabHomeKey2(event) {
    var firstHeaderAction = findFirstHeaderAction();
    changeFocusedTab(firstHeaderAction);
    event.preventDefault();
  };
  var onTabEndKey = function onTabEndKey2(event) {
    var lastHeaderAction = findLastHeaderAction();
    changeFocusedTab(lastHeaderAction);
    event.preventDefault();
  };
  var onPageDownKey = function onPageDownKey2(event) {
    updateScrollBar({
      index: React6.Children.count(props.children) - 1
    });
    event.preventDefault();
  };
  var onPageUpKey = function onPageUpKey2(event) {
    updateScrollBar({
      index: 0
    });
    event.preventDefault();
  };
  var onTabEnterKey = function onTabEnterKey2(event, tab, index) {
    changeActiveIndex(event, tab, index);
    event.preventDefault();
  };
  var _findNextHeaderAction = function findNextHeaderAction(tabElement) {
    var selfCheck = arguments.length > 1 && arguments[1] !== void 0 ? arguments[1] : false;
    var headerElement = selfCheck ? tabElement : tabElement.nextElementSibling;
    return headerElement ? DomHandler.getAttribute(headerElement, "data-p-disabled") || DomHandler.getAttribute(headerElement, "data-pc-section") === "inkbar" ? _findNextHeaderAction(headerElement) : DomHandler.findSingle(headerElement, '[data-pc-section="headeraction"]') : null;
  };
  var _findPrevHeaderAction = function findPrevHeaderAction(tabElement) {
    var selfCheck = arguments.length > 1 && arguments[1] !== void 0 ? arguments[1] : false;
    var headerElement = selfCheck ? tabElement : tabElement.previousElementSibling;
    return headerElement ? DomHandler.getAttribute(headerElement, "data-p-disabled") || DomHandler.getAttribute(headerElement, "data-pc-section") === "inkbar" ? _findPrevHeaderAction(headerElement) : DomHandler.findSingle(headerElement, '[data-pc-section="headeraction"]') : null;
  };
  var findFirstHeaderAction = function findFirstHeaderAction2() {
    return _findNextHeaderAction(navRef.current.firstElementChild, true);
  };
  var findLastHeaderAction = function findLastHeaderAction2() {
    return _findPrevHeaderAction(navRef.current.lastElementChild, true);
  };
  var changeFocusedTab = function changeFocusedTab2(element) {
    if (element) {
      DomHandler.focus(element);
      updateScrollBar({
        element
      });
    }
  };
  var updateInkBar = function updateInkBar2() {
    var tabHeader = tabsRef.current["tab_".concat(activeIndex)];
    inkbarRef.current.style.width = DomHandler.getWidth(tabHeader) + "px";
    inkbarRef.current.style.left = DomHandler.getOffset(tabHeader).left - DomHandler.getOffset(navRef.current).left + "px";
  };
  var updateScrollBar = function updateScrollBar2(_ref3) {
    var index = _ref3.index, element = _ref3.element;
    var tabHeader = element || tabsRef.current["tab_".concat(index)];
    if (tabHeader && tabHeader.scrollIntoView) {
      tabHeader.scrollIntoView({
        block: "nearest"
      });
    }
  };
  var updateButtonState = function updateButtonState2() {
    var _contentRef$current = contentRef.current, scrollLeft = _contentRef$current.scrollLeft, scrollWidth = _contentRef$current.scrollWidth;
    var width = DomHandler.getWidth(contentRef.current);
    setBackwardIsDisabledState(scrollLeft === 0);
    setForwardIsDisabledState(parseInt(scrollLeft) === scrollWidth - width);
  };
  var onScroll = function onScroll2(event) {
    props.scrollable && updateButtonState();
    event.preventDefault();
  };
  var getVisibleButtonWidths = function getVisibleButtonWidths2() {
    return [prevBtnRef.current, nextBtnRef.current].reduce(function(acc, el) {
      return el ? acc + DomHandler.getWidth(el) : acc;
    }, 0);
  };
  var navBackward = function navBackward2() {
    var width = DomHandler.getWidth(contentRef.current) - getVisibleButtonWidths();
    var pos = contentRef.current.scrollLeft - width;
    contentRef.current.scrollLeft = pos <= 0 ? 0 : pos;
  };
  var navForward = function navForward2() {
    var width = DomHandler.getWidth(contentRef.current) - getVisibleButtonWidths();
    var pos = contentRef.current.scrollLeft + width;
    var lastPos = contentRef.current.scrollWidth - width;
    contentRef.current.scrollLeft = pos >= lastPos ? lastPos : pos;
  };
  var reset = function reset2() {
    setBackwardIsDisabledState(true);
    setForwardIsDisabledState(false);
    setHiddenTabsState([]);
    if (props.onTabChange) {
      props.onTabChange({
        index: activeIndex
      });
    } else {
      setActiveIndexState(props.activeIndex);
    }
  };
  React6.useEffect(function() {
    updateInkBar();
    updateButtonState();
  });
  useMountEffect(function() {
    if (!idState) {
      setIdState(UniqueComponentId());
    }
  });
  useUpdateEffect(function() {
    if (ObjectUtils.isNotEmpty(hiddenTabsState)) {
      var tabInfo = findVisibleActiveTab(hiddenTabsState[hiddenTabsState.length - 1]);
      tabInfo && onTabHeaderClick(null, tabInfo.tab, tabInfo.index);
    }
  }, [hiddenTabsState]);
  useUpdateEffect(function() {
    if (props.activeIndex !== activeIndexState) {
      updateScrollBar({
        index: props.activeIndex
      });
    }
  }, [props.activeIndex]);
  React6.useImperativeHandle(ref, function() {
    return {
      props,
      reset,
      getElement: function getElement() {
        return elementRef.current;
      }
    };
  });
  var createTabHeader = function createTabHeader2(tab, index) {
    var selected = isSelected(index);
    var _TabPanelBase$getCPro = TabPanelBase.getCProps(tab), headerStyle = _TabPanelBase$getCPro.headerStyle, headerClassName = _TabPanelBase$getCPro.headerClassName, _style = _TabPanelBase$getCPro.style, _className = _TabPanelBase$getCPro.className, disabled = _TabPanelBase$getCPro.disabled, leftIcon = _TabPanelBase$getCPro.leftIcon, rightIcon = _TabPanelBase$getCPro.rightIcon, header3 = _TabPanelBase$getCPro.header, headerTemplate = _TabPanelBase$getCPro.headerTemplate, closable = _TabPanelBase$getCPro.closable, closeIcon = _TabPanelBase$getCPro.closeIcon;
    var headerId = idState + "_header_" + index;
    var ariaControls = idState + index + "_content";
    var tabIndex = disabled || !selected ? -1 : 0;
    var leftIconElement = leftIcon && IconUtils.getJSXIcon(leftIcon, void 0, {
      props
    });
    var headerTitleProps = mergeProps({
      className: cx("tab.headertitle")
    }, getTabPT(tab, "headertitle", index));
    var titleElement = /* @__PURE__ */ React6.createElement("span", headerTitleProps, header3);
    var rightIconElement = rightIcon && IconUtils.getJSXIcon(rightIcon, void 0, {
      props
    });
    var closeIconProps = mergeProps({
      className: cx("tab.closeIcon"),
      onClick: function onClick(e) {
        return onTabHeaderClose(e, index);
      }
    }, getTabPT(tab, "closeIcon", index));
    var icon = closeIcon || /* @__PURE__ */ React6.createElement(TimesIcon, closeIconProps);
    var closableIconElement = closable ? IconUtils.getJSXIcon(icon, _objectSpread({}, closeIconProps), {
      props
    }) : null;
    var headerActionProps = mergeProps({
      id: headerId,
      role: "tab",
      className: cx("tab.headeraction"),
      tabIndex,
      "aria-controls": ariaControls,
      "aria-selected": selected,
      "aria-disabled": disabled,
      onClick: function onClick(e) {
        return onTabHeaderClick(e, tab, index);
      },
      onKeyDown: function onKeyDown(e) {
        return _onKeyDown(e, tab, index);
      }
    }, getTabPT(tab, "headeraction", index));
    var content4 = (
      // eslint-disable /
      /* @__PURE__ */ React6.createElement("a", headerActionProps, leftIconElement, titleElement, rightIconElement, closableIconElement, /* @__PURE__ */ React6.createElement(Ripple, null))
    );
    if (headerTemplate) {
      var defaultContentOptions = {
        className: "p-tabview-nav-link",
        titleClassName: "p-tabview-title",
        onClick: function onClick(e) {
          return onTabHeaderClick(e, tab, index);
        },
        onKeyDown: function onKeyDown(e) {
          return _onKeyDown(e, tab, index);
        },
        leftIconElement,
        titleElement,
        rightIconElement,
        element: content4,
        props,
        index,
        selected,
        ariaControls
      };
      content4 = ObjectUtils.getJSXElement(headerTemplate, defaultContentOptions);
    }
    var headerProps = mergeProps({
      ref: function ref2(el) {
        return tabsRef.current["tab_".concat(index)] = el;
      },
      className: cx("tab.header", {
        selected,
        disabled,
        headerClassName,
        _className
      }),
      style: sx("tab.header", {
        headerStyle,
        _style
      }),
      role: "presentation"
    }, getTabPT(tab, "root", index), getTabPT(tab, "header", index));
    return /* @__PURE__ */ React6.createElement("li", headerProps, content4);
  };
  var createTabHeaders = function createTabHeaders2() {
    return React6.Children.map(props.children, function(tab, index) {
      if (shouldUseTab(tab)) {
        return createTabHeader(tab, index);
      }
    });
  };
  var createNavigator = function createNavigator2() {
    var headers = createTabHeaders();
    var navContentProps = mergeProps({
      id: idState + "_navcontent",
      ref: contentRef,
      className: cx("navcontent"),
      style: props.style,
      onScroll
    }, ptm("navcontent"));
    var navProps = mergeProps({
      ref: navRef,
      className: cx("nav"),
      role: "tablist"
    }, ptm("nav"));
    var inkbarProps = mergeProps({
      ref: inkbarRef,
      "aria-hidden": "true",
      role: "presentation",
      className: cx("inkbar")
    }, ptm("inkbar"));
    return /* @__PURE__ */ React6.createElement("div", navContentProps, /* @__PURE__ */ React6.createElement("ul", navProps, headers, /* @__PURE__ */ React6.createElement("li", inkbarProps)));
  };
  var createContent = function createContent2() {
    var panelContainerProps = mergeProps({
      className: cx("panelcontainer"),
      style: props.panelContainerStyle
    }, ptm("panelcontainer"));
    var contents = React6.Children.map(props.children, function(tab, index) {
      if (shouldUseTab(tab) && (!props.renderActiveOnly || isSelected(index))) {
        var selected = isSelected(index);
        var contentId = idState + index + "_content";
        var ariaLabelledBy = idState + "_header_" + index;
        var contentProps = mergeProps({
          id: contentId,
          className: cx("tab.content", {
            props,
            selected,
            getTabProp,
            tab,
            isSelected,
            shouldUseTab,
            index
          }),
          style: sx("tab.content", {
            props,
            getTabProp,
            tab,
            isSelected,
            shouldUseTab,
            index
          }),
          role: "tabpanel",
          "aria-labelledby": ariaLabelledBy
        }, TabPanelBase.getCOtherProps(tab), getTabPT(tab, "root", index), getTabPT(tab, "content", index));
        return /* @__PURE__ */ React6.createElement("div", contentProps, !props.renderActiveOnly ? getTabProp(tab, "children") : selected && getTabProp(tab, "children"));
      }
    });
    return /* @__PURE__ */ React6.createElement("div", panelContainerProps, contents);
  };
  var createPrevButton = function createPrevButton2() {
    var prevIconProps = mergeProps({
      "aria-hidden": "true"
    }, ptm("previcon"));
    var icon = props.prevButton || /* @__PURE__ */ React6.createElement(ChevronLeftIcon, prevIconProps);
    var leftIcon = IconUtils.getJSXIcon(icon, _objectSpread({}, prevIconProps), {
      props
    });
    var prevButtonProps = mergeProps({
      ref: prevBtnRef,
      type: "button",
      className: cx("prevbutton"),
      "aria-label": ariaLabel("previousPageLabel"),
      onClick: function onClick(e) {
        return navBackward();
      }
    }, ptm("prevbutton"));
    if (props.scrollable && !backwardIsDisabledState) {
      return /* @__PURE__ */ React6.createElement("button", prevButtonProps, leftIcon, /* @__PURE__ */ React6.createElement(Ripple, null));
    }
    return null;
  };
  var createNextButton = function createNextButton2() {
    var nextIconProps = mergeProps({
      "aria-hidden": "true"
    }, ptm("nexticon"));
    var icon = props.nextButton || /* @__PURE__ */ React6.createElement(ChevronRightIcon, nextIconProps);
    var rightIcon = IconUtils.getJSXIcon(icon, _objectSpread({}, nextIconProps), {
      props
    });
    var nextButtonProps = mergeProps({
      ref: nextBtnRef,
      type: "button",
      className: cx("nextbutton"),
      "aria-label": ariaLabel("nextPageLabel"),
      onClick: function onClick(e) {
        return navForward();
      }
    }, ptm("nextbutton"));
    if (props.scrollable && !forwardIsDisabledState) {
      return /* @__PURE__ */ React6.createElement("button", nextButtonProps, rightIcon, /* @__PURE__ */ React6.createElement(Ripple, null));
    }
  };
  var rootProps = mergeProps({
    id: idState,
    ref: elementRef,
    style: props.style,
    className: classNames(props.className, cx("root"))
  }, TabViewBase.getOtherProps(props), ptm("root"));
  var navContainerProps = mergeProps({
    className: cx("navcontainer")
  }, ptm("navcontainer"));
  var navigator = createNavigator();
  var content3 = createContent();
  var prevButton = createPrevButton();
  var nextButton = createNextButton();
  return /* @__PURE__ */ React6.createElement("div", rootProps, /* @__PURE__ */ React6.createElement("div", navContainerProps, prevButton, navigator, nextButton), content3);
});
TabPanel.displayName = "TabPanel";
TabView.displayName = "TabView";

// react-with-dotnet/libraries/primereact/all.jsx
function register2(name, value) {
  react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.PrimeReact." + name, value);
}
function RegisterComponents() {
  register2("Avatar", import_react3.default.lazy(() => import("./Avatar-2E55PD4B.js")));
  register2("Button", import_react3.default.lazy(() => import("./Button-IPXBCLTL.js")));
  register2("InputText", import_react3.default.lazy(() => import("./InputText-XXWPMVDV.js")));
  register2("InputTextarea", import_react3.default.lazy(() => import("./InputTextarea-D5GSTGBQ.js")));
  register2("BlockUI", import_react3.default.lazy(() => import("./BlockUI-MSPOCE2X.js")));
  register2("Card", import_react3.default.lazy(() => import("./Card-V3G54I72.js")));
  register2("SplitterPanel", import_react3.default.lazy(() => import("./SplitterPanel-AFDGXMPM.js")));
  register2("Splitter", import_react3.default.lazy(() => import("./Splitter-IQ7QV47W.js")));
  register2("Slider", import_react3.default.lazy(() => import("./Slider-IWQ2WLJX.js")));
  register2("ListBox", import_react3.default.lazy(() => import("./ListBox-5CQW43X6.js")));
  register2("Dropdown", import_react3.default.lazy(() => import("./Dropdown-6WGNZGIG.js")));
  register2("Column", import_react3.default.lazy(() => import("./Column-PZD23MDV.js")));
  register2("DataTable", import_react3.default.lazy(() => import("./DataTable-GKGP4F5D.js")));
  register2("Checkbox", import_react3.default.lazy(() => import("./Checkbox-FMY5NFIY.js")));
  register2("InputMask", import_react3.default.lazy(() => import("./InputMask-4T5VGW32.js")));
  register2("AutoComplete", import_react3.default.lazy(() => import("./AutoComplete-5UQYLFNY.js")));
  register2("Tree", import_react3.default.lazy(() => import("./Tree-OMXQNOHW.js")));
  register2("InputSwitch", import_react3.default.lazy(() => import("./InputSwitch-7SM4V267.js")));
  register2("Panel", import_react3.default.lazy(() => import("./Panel-YLSIOTTX.js")));
  register2("Tooltip", import_react3.default.lazy(() => import("./Tooltip-4R2CGU3F.js")));
  register2("Message", import_react3.default.lazy(() => import("./Message-T3N37M6D.js")));
  register2("ScrollPanel", import_react3.default.lazy(() => import("./ScrollPanel-DNQA42SN.js")));
  register2("Dialog", import_react3.default.lazy(() => import("./Dialog-N57P2B24.js")));
  register2("TabView", TabView);
  register2("TabPanel", TabPanel);
  register2("Panel::GetHeaderTemplate", (key) => react_with_dotnet_default.GetExternalJsObject(key));
  register2("GrabOnlyValueParameterFromCommonPrimeReactEvent", function(argumentsAsArray) {
    const value = argumentsAsArray[0].value;
    return [{ value }];
  });
  register2("GrabWithoutOriginalEvent", function(argumentsAsArray) {
    const newInstance = {};
    const obj = argumentsAsArray[0];
    for (var propertyName in obj) {
      if (obj.hasOwnProperty(propertyName)) {
        const value = obj[propertyName];
        if (propertyName === "originalEvent" && value && value._reactName) {
          continue;
        }
        newInstance[propertyName] = value;
      }
    }
    return [newInstance];
  });
}
function RegisterGlobalStyles() {
  react_with_dotnet_default.TryLoadCssByHref("https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/themes/saga-blue/theme.css");
  react_with_dotnet_default.TryLoadCssByHref("https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/primereact.min.css");
  react_with_dotnet_default.TryLoadCssByHref("https://cdn.jsdelivr.net/npm/primeicons@5.0.0/primeicons.css");
}
var isFirstLoad = false;
react_with_dotnet_default.BeforeAnyThirdPartyComponentAccess((dotNetFullClassNameOf3rdPartyComponent) => {
  if (isFirstLoad) {
    return;
  }
  if (dotNetFullClassNameOf3rdPartyComponent.indexOf(".PrimeReact.") > 0) {
    isFirstLoad = true;
    RegisterGlobalStyles();
    RegisterComponents();
  }
});
react_with_dotnet_default.OnThirdPartyComponentPropsCalculated("ReactWithDotNet.ThirdPartyLibraries.PrimeReact.TabPanel", (props) => {
  if (props.headerTemplate) {
    var element = props.headerTemplate;
    props.headerTemplate = (options) => {
      return import_react3.default.cloneElement(element, { onClick: options.onClick });
    };
  }
  return props;
});

// react-with-dotnet/libraries/react-awesome-reveal/all.jsx
var import_react4 = __toESM(require_react());
function register3(name, value) {
  react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactAwesomeReveal." + name, value);
}
register3("AttentionSeeker", import_react4.default.lazy(() => import("./AttentionSeeker-H3B4BSFK.js")));
register3("Bounce", import_react4.default.lazy(() => import("./Bounce-BRJRVOGZ.js")));
register3("Fade", import_react4.default.lazy(() => import("./Fade-KBBNYK74.js")));
register3("Flip", import_react4.default.lazy(() => import("./Flip-UODRYGSS.js")));
register3("Hinge", import_react4.default.lazy(() => import("./Hinge-LJZGVKIB.js")));
register3("JackInTheBox", import_react4.default.lazy(() => import("./JackInTheBox-LMT2BLOD.js")));
register3("Rotate", import_react4.default.lazy(() => import("./Rotate-5XF5MFBJ.js")));
register3("Slide", import_react4.default.lazy(() => import("./Slide-3FXR3IOL.js")));
register3("Zoom", import_react4.default.lazy(() => import("./Zoom-L6MKLIMQ.js")));

// react-with-dotnet/libraries/react-free-scrollbar/all.jsx
var import_react5 = __toESM(require_react());
var FreeScrollbar = import_react5.default.lazy(() => import("./FreeScrollbar-V6Y425QR.js"));
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar.FreeScrollBar", FreeScrollbar);

// react-with-dotnet/libraries/react-xarrows/all.jsx
var import_react6 = __toESM(require_react());
var Xarrow = import_react6.default.lazy(() => import("./Xarrow-KG4HE5HH.js"));
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactXarrows.Xarrow", Xarrow);

// react-with-dotnet/libraries/rsuite/all.jsx
var import_react7 = __toESM(require_react());
function RegisterReactSuiteComponent(name, cmp) {
  const Prefix = "ReactWithDotNet.ThirdPartyLibraries.ReactSuite.";
  react_with_dotnet_default.RegisterExternalJsObject(Prefix + name, cmp);
}
RegisterReactSuiteComponent("AutoComplete", import_react7.default.lazy(() => import("./AutoComplete-P5YKKHOK.js")));
RegisterReactSuiteComponent("AutoComplete::OnChange", (args) => [args[0]]);
RegisterReactSuiteComponent("Modal", import_react7.default.lazy(() => import("./Modal-E2BEIOLZ.js")));
RegisterReactSuiteComponent("Modal+Header", import_react7.default.lazy(() => import("./Modal.Header-OTRWFE6Y.js")));
RegisterReactSuiteComponent("Modal+Body", import_react7.default.lazy(() => import("./Modal.Body-IJ6GN6IT.js")));
RegisterReactSuiteComponent("Uploader", import_react7.default.lazy(() => import("./Uploader-BDJVYBA7.js")));
var isFirstAccess = true;
react_with_dotnet_default.OnFindExternalObject((name) => {
  if (name.indexOf(".ReactSuite.") < 0) {
    return null;
  }
  if (isFirstAccess) {
    isFirstAccess = false;
    react_with_dotnet_default.TryLoadCssByHref("https://cdnjs.cloudflare.com/ajax/libs/rsuite/5.70.0/rsuite.min.css");
  }
  return null;
});

// react-with-dotnet/libraries/swiper/all.jsx
var import_react8 = __toESM(require_react());
function register4(name, value) {
  react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._Swiper_." + name, value);
}
register4("Swiper", import_react8.default.lazy(() => import("./Swiper-ER4SHOIK.js")));
register4("SwiperSlide", SwiperSlide);
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance", function(args) {
  return [{
    activeIndex: args[0].activeIndex
  }];
});

// react-with-dotnet/libraries/framer-motion/all.jsx
var import_react10 = __toESM(require_react());
motion.div;
function register5(name, value) {
  react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.FramerMotion." + name, value);
}
register5("motion+div", import_react10.default.lazy(() => import("./div-LHG3RJWM.js")));
register5("AnimatePresence", import_react10.default.lazy(() => import("./AnimatePresence-R5SNLU6V.js")));
register5("motion+button", import_react10.default.lazy(() => import("./button-ADG263MF.js")));

// react-with-dotnet/libraries/nextui-org/all.jsx
var import_react11 = __toESM(require_react());
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.NextUI.Avatar", import_react11.default.lazy(() => import("./Avatar-D6ANLI2L.js")));

// react-with-dotnet/libraries/react-player/all.jsx
var import_react12 = __toESM(require_react());
var ReactPlayer = import_react12.default.lazy(() => import("./ReactPlayer-LV235OOM.js"));
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.React_Player.ReactPlayer", ReactPlayer);

// react-with-dotnet/libraries/MonacoEditorReact/all.jsx
var import_react13 = __toESM(require_react());
var Editor = import_react13.default.lazy(() => import("./Editor-CCUMR7NG.js"));
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact.Editor", Editor);
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact::OnChange", function(args) {
  return [args[0]];
});

// react-with-dotnet/libraries/react-quill/all.jsx
var import_react14 = __toESM(require_react());
var ReactQuill = import_react14.default.lazy(() => import("./ReactQuill-ZPFVGITP.js"));
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._ReactQuill_.ReactQuill", ReactQuill);

// react-with-dotnet/libraries/react-split/all.jsx
var import_react15 = __toESM(require_react());
var Split = import_react15.default.lazy(() => import("./Split-I7SFB4V5.js"));
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._react_split_.Split", Split);

// react-with-dotnet/libraries/_uploady_/all.jsx
var import_react16 = __toESM(require_react());
var Uploady = import_react16.default.lazy(() => import("./Uploady-H6BIZRF7.js"));
var UploadButton = import_react16.default.lazy(() => import("./UploadButton-27XFWV2Q.js"));
var UploadProgress = import_react16.default.lazy(() => import("./UploadProgress-4F4HU7PJ.js"));
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._uploady_.Uploady", Uploady);
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._uploady_.UploadButton", UploadButton);
react_with_dotnet_default.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._uploady_.UploadProgress", UploadProgress);
export {
  react_with_dotnet_default as ReactWithDotNet
};
