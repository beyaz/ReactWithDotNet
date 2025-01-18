import {
  TimesCircleIcon
} from "./chunk-HZCU5KMY.js";
import {
  OverlayService
} from "./chunk-VT76XFEX.js";
import {
  ChevronDownIcon
} from "./chunk-H5HP4A3G.js";
import {
  CSSTransition
} from "./chunk-GBAKNBNU.js";
import {
  VirtualScroller
} from "./chunk-G7G43RN5.js";
import {
  Button
} from "./chunk-SR7S3A4L.js";
import {
  SpinnerIcon
} from "./chunk-TEWXDFAB.js";
import {
  InputText
} from "./chunk-JAM7SNCO.js";
import "./chunk-WPUEZBIL.js";
import {
  Tooltip
} from "./chunk-2PX4WDKA.js";
import {
  Portal
} from "./chunk-CMYF6AIZ.js";
import {
  Ripple
} from "./chunk-56TI4EJO.js";
import "./chunk-2FIYIUK5.js";
import {
  ComponentBase,
  DomHandler,
  IconUtils,
  ObjectUtils,
  PrimeReact,
  PrimeReactContext,
  UniqueComponentId,
  ZIndexUtils,
  classNames,
  localeOption,
  useHandleStyle,
  useMergeProps,
  useMountEffect,
  useOverlayListener,
  useUnmountEffect,
  useUpdateEffect
} from "./chunk-NBA33TRK.js";
import "./chunk-M4ZKUW6V.js";
import "./chunk-A6RS5VZJ.js";
import "./chunk-QNQS7X5M.js";
import "./chunk-SEPJ2F45.js";
import "./chunk-FLAFLVMN.js";
import "./chunk-IHPEMKKY.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/primereact/autocomplete/autocomplete.esm.js
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
function _extends() {
  return _extends = Object.assign ? Object.assign.bind() : function(n) {
    for (var e = 1; e < arguments.length; e++) {
      var t = arguments[e];
      for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]);
    }
    return n;
  }, _extends.apply(null, arguments);
}
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
var classes = {
  root: function root(_ref) {
    var props = _ref.props, focusedState = _ref.focusedState;
    return classNames("p-autocomplete p-component p-inputwrapper", {
      "p-autocomplete-dd": props.dropdown,
      "p-autocomplete-multiple": props.multiple,
      "p-inputwrapper-filled": props.value,
      "p-invalid": props.invalid,
      "p-inputwrapper-focus": focusedState
    });
  },
  container: function container(_ref2) {
    var props = _ref2.props, context = _ref2.context;
    return classNames("p-autocomplete-multiple-container p-component p-inputtext", {
      "p-disabled": props.disabled,
      "p-variant-filled": props.variant ? props.variant === "filled" : context && context.inputStyle === "filled"
    });
  },
  loadingIcon: "p-autocomplete-loader",
  dropdownButton: "p-autocomplete-dropdown",
  removeTokenIcon: "p-autocomplete-token-icon",
  token: "p-autocomplete-token p-highlight",
  tokenLabel: "p-autocomplete-token-label",
  inputToken: "p-autocomplete-input-token",
  input: function input(_ref3) {
    var props = _ref3.props, context = _ref3.context;
    return classNames("p-autocomplete-input", {
      "p-autocomplete-dd-input": props.dropdown,
      "p-variant-filled": props.variant ? props.variant === "filled" : context && context.inputStyle === "filled"
    });
  },
  panel: function panel(_ref4) {
    var context = _ref4.context;
    return classNames("p-autocomplete-panel p-component", {
      "p-ripple-disabled": context && context.ripple === false || PrimeReact.ripple === false
    });
  },
  listWrapper: "p-autocomplete-items-wrapper",
  list: function list(_ref5) {
    var virtualScrollerOptions = _ref5.virtualScrollerOptions, options = _ref5.options;
    return virtualScrollerOptions ? classNames("p-autocomplete-items", options.className) : "p-autocomplete-items";
  },
  emptyMessage: "p-autocomplete-item",
  item: function item(_ref6) {
    var suggestion = _ref6.suggestion, optionGroupLabel = _ref6.optionGroupLabel, selected = _ref6.selected;
    return optionGroupLabel ? classNames("p-autocomplete-item", {
      "p-disabled": suggestion.disabled
    }, {
      selected
    }) : classNames("p-autocomplete-item", {
      "p-disabled": suggestion.disabled
    }, {
      "p-highlight": selected
    });
  },
  itemGroup: "p-autocomplete-item-group",
  footer: "p-autocomplete-footer",
  transition: "p-connected-overlay"
};
var styles = "\n@layer primereact {\n    .p-autocomplete {\n        display: inline-flex;\n        position: relative;\n    }\n    \n    .p-autocomplete-loader {\n        position: absolute;\n        top: 50%;\n        margin-top: -.5rem;\n    }\n    \n    .p-autocomplete-dd .p-autocomplete-input {\n        flex: 1 1 auto;\n        width: 1%;\n    }\n    \n    .p-autocomplete-dd .p-autocomplete-input,\n    .p-autocomplete-dd .p-autocomplete-multiple-container {\n         border-top-right-radius: 0;\n         border-bottom-right-radius: 0;\n     }\n    \n    .p-autocomplete-dd .p-autocomplete-dropdown {\n         border-top-left-radius: 0;\n         border-bottom-left-radius: 0px;\n    }\n    \n    .p-autocomplete .p-autocomplete-panel {\n        min-width: 100%;\n    }\n    \n    .p-autocomplete-panel {\n        position: absolute;\n        top: 0;\n        left: 0;\n    }\n    \n    .p-autocomplete-items {\n        margin: 0;\n        padding: 0;\n        list-style-type: none;\n    }\n    \n    .p-autocomplete-item {\n        cursor: pointer;\n        white-space: nowrap;\n        position: relative;\n        overflow: hidden;\n    }\n    \n    .p-autocomplete-multiple-container {\n        margin: 0;\n        padding: 0;\n        list-style-type: none;\n        cursor: text;\n        overflow: hidden;\n        display: flex;\n        align-items: center;\n        flex-wrap: wrap;\n    }\n    \n    .p-autocomplete-token {\n        cursor: default;\n        display: inline-flex;\n        align-items: center;\n        flex: 0 0 auto;\n    }\n    \n    .p-autocomplete-token-icon {\n        cursor: pointer;\n    }\n    \n    .p-autocomplete-input-token {\n        flex: 1 1 auto;\n        display: inline-flex;\n    }\n    \n    .p-autocomplete-input-token input {\n        border: 0 none;\n        outline: 0 none;\n        background-color: transparent;\n        margin: 0;\n        padding: 0;\n        box-shadow: none;\n        border-radius: 0;\n        width: 100%;\n    }\n    \n    .p-fluid .p-autocomplete {\n        display: flex;\n    }\n    \n    .p-fluid .p-autocomplete-dd .p-autocomplete-input {\n        width: 1%;\n    }\n    \n    .p-autocomplete-items-wrapper {\n        overflow: auto;\n    } \n}\n";
var AutoCompleteBase = ComponentBase.extend({
  defaultProps: {
    __TYPE: "AutoComplete",
    id: null,
    appendTo: null,
    autoFocus: false,
    autoHighlight: false,
    className: null,
    completeMethod: null,
    delay: 300,
    disabled: false,
    dropdown: false,
    dropdownAriaLabel: null,
    dropdownAutoFocus: true,
    dropdownIcon: null,
    dropdownMode: "blank",
    emptyMessage: null,
    field: null,
    forceSelection: false,
    inputClassName: null,
    inputId: null,
    inputRef: null,
    inputStyle: null,
    variant: null,
    invalid: false,
    itemTemplate: null,
    loadingIcon: null,
    maxLength: null,
    minLength: 1,
    multiple: false,
    name: null,
    onBlur: null,
    onChange: null,
    onClear: null,
    onClick: null,
    onContextMenu: null,
    onDblClick: null,
    onDropdownClick: null,
    onFocus: null,
    onHide: null,
    onKeyPress: null,
    onKeyUp: null,
    onMouseDown: null,
    onSelect: null,
    onShow: null,
    onUnselect: null,
    optionGroupChildren: null,
    optionGroupLabel: null,
    optionGroupTemplate: null,
    panelClassName: null,
    panelFooterTemplate: null,
    panelStyle: null,
    placeholder: null,
    readOnly: false,
    removeTokenIcon: null,
    scrollHeight: "200px",
    selectedItemTemplate: null,
    selectionLimit: null,
    showEmptyMessage: false,
    size: null,
    style: null,
    suggestions: null,
    tabIndex: null,
    tooltip: null,
    tooltipOptions: null,
    transitionOptions: null,
    type: "text",
    value: null,
    virtualScrollerOptions: null,
    children: void 0
  },
  css: {
    classes,
    styles
  }
});
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
var AutoCompletePanel = /* @__PURE__ */ React.memo(/* @__PURE__ */ React.forwardRef(function(props, ref) {
  var mergeProps = useMergeProps();
  var ptm = props.ptm, cx = props.cx;
  var context = React.useContext(PrimeReactContext);
  var _ptm = function _ptm2(key, options) {
    return ptm(key, _objectSpread$1({
      hostName: props.hostName
    }, options));
  };
  var getPTOptions = function getPTOptions2(item2, key) {
    return _ptm(key, {
      context: {
        selected: props.selectedItem.current === item2,
        disabled: item2.disabled
      }
    });
  };
  var getOptionGroupRenderKey = function getOptionGroupRenderKey2(optionGroup) {
    return ObjectUtils.resolveFieldData(optionGroup, props.optionGroupLabel);
  };
  var getOptionRenderKey = function getOptionRenderKey2(option) {
    return ObjectUtils.resolveFieldData(option, props.field);
  };
  var createFooter = function createFooter2() {
    if (props.panelFooterTemplate) {
      var content = ObjectUtils.getJSXElement(props.panelFooterTemplate, props, props.onOverlayHide);
      var footerProps = mergeProps({
        className: cx("footer")
      }, _ptm("footer"));
      return /* @__PURE__ */ React.createElement("div", footerProps, content);
    }
    return null;
  };
  var findKeyIndex = function findKeyIndex2(array, key, value) {
    return array.findIndex(function(obj) {
      return obj[key] === value;
    });
  };
  var latestKey = React.useRef({
    key: null,
    index: 0,
    keyIndex: 0
  });
  var createLabelItem = function createLabelItem2(item2, key, index, labelItemProps) {
    var content = props.optionGroupTemplate ? ObjectUtils.getJSXElement(props.optionGroupTemplate, item2, index) : props.getOptionGroupLabel(item2) || item2;
    var itemGroupProps = mergeProps(_objectSpread$1({
      index,
      className: cx("itemGroup"),
      "data-p-highlight": false
    }, labelItemProps), _ptm("itemGroup"));
    return /* @__PURE__ */ React.createElement("li", _extends({}, itemGroupProps, {
      key: key ? key : null
    }), content);
  };
  var isOptionSelected = function isOptionSelected2(item2) {
    if (props.selectedItem && props.selectedItem.current && Array.isArray(props.selectedItem.current)) {
      return props.selectedItem.current.some(function(selectedItem) {
        return ObjectUtils.deepEquals(selectedItem, item2);
      });
    } else {
      return ObjectUtils.deepEquals(props.selectedItem.current, item2);
    }
  };
  var createListItem = function createListItem2(item2, key, index, listItemProps) {
    var selected = isOptionSelected(item2);
    var content = props.itemTemplate ? ObjectUtils.getJSXElement(props.itemTemplate, item2, index) : props.field ? ObjectUtils.resolveFieldData(item2, props.field) : item2;
    var itemProps = mergeProps(_objectSpread$1({
      index,
      role: "option",
      className: cx("item", {
        optionGroupLabel: props.optionGroupLabel,
        suggestion: item2,
        selected
      }),
      onClick: function onClick(e) {
        return props.onItemClick(e, item2);
      },
      "aria-selected": selected
    }, listItemProps), getPTOptions(item2, "item"));
    return /* @__PURE__ */ React.createElement("li", _extends({
      key
    }, itemProps), content, /* @__PURE__ */ React.createElement(Ripple, null));
  };
  var createGroupChildren = function createGroupChildren2(optionGroup, i) {
    var groupChildren = props.getOptionGroupChildren(optionGroup);
    return groupChildren.map(function(item2, j) {
      var key = i + "_" + j;
      var itemProps = mergeProps({
        "data-group": i,
        "data-index": j,
        "data-p-disabled": item2.disabled
      });
      return createListItem(item2, key, j, itemProps);
    });
  };
  var createItem = function createItem2(suggestion, index) {
    var scrollerOptions = arguments.length > 2 && arguments[2] !== void 0 ? arguments[2] : {};
    var style = {
      height: scrollerOptions.props ? scrollerOptions.props.itemSize : void 0
    };
    if (props.optionGroupLabel) {
      if (props.virtualScrollerOptions) {
        var keyIndex = findKeyIndex(props.suggestions, props.optionGroupLabel, suggestion);
        if (keyIndex !== -1) {
          latestKey.current = {
            key: suggestion,
            index,
            keyIndex
          };
          var _key = index + "_" + getOptionGroupRenderKey(suggestion);
          return createLabelItem(suggestion, _key, index, {
            style
          });
        }
        var _key2 = index + "_" + latestKey.current.keyIndex;
        var _itemProps = mergeProps({
          style,
          "data-group": latestKey.current.keyIndex,
          "data-index": index - latestKey.current.index - 1,
          "data-p-disabled": suggestion.disabled
        });
        return createListItem(suggestion, _key2, index, _itemProps);
      }
      var childrenContent = createGroupChildren(suggestion, index);
      var _key3 = index + "_" + getOptionGroupRenderKey(suggestion);
      return /* @__PURE__ */ React.createElement(React.Fragment, {
        key: _key3
      }, createLabelItem(suggestion, void 0, index, {
        style
      }), childrenContent);
    }
    var key = "".concat(index, "_").concat(ObjectUtils.isObject(suggestion) ? getOptionRenderKey(suggestion) : suggestion);
    var itemProps = mergeProps({
      style,
      "data-p-disabled": suggestion.disabled
    }, getPTOptions(suggestion, "item"));
    return createListItem(suggestion, key, index, itemProps);
  };
  var createItems = function createItems2() {
    return props.suggestions ? props.suggestions.map(createItem) : null;
  };
  var flattenGroupedItems = function flattenGroupedItems2(items) {
    try {
      return items === null || items === void 0 ? void 0 : items.map(function(item2) {
        return [item2 === null || item2 === void 0 ? void 0 : item2[props === null || props === void 0 ? void 0 : props.optionGroupLabel]].concat(_toConsumableArray(item2 === null || item2 === void 0 ? void 0 : item2[props === null || props === void 0 ? void 0 : props.optionGroupChildren]));
      }).flat();
    } catch (e) {
    }
  };
  var createContent = function createContent2() {
    if (props.showEmptyMessage && ObjectUtils.isEmpty(props.suggestions)) {
      var emptyMessage = props.emptyMessage || localeOption("emptyMessage");
      var emptyMessageProps = mergeProps({
        className: cx("emptyMessage")
      }, _ptm("emptyMesage"));
      var _listProps = mergeProps({
        className: cx("list")
      }, _ptm("list"));
      return /* @__PURE__ */ React.createElement("ul", _listProps, /* @__PURE__ */ React.createElement("li", emptyMessageProps, emptyMessage));
    }
    if (props.virtualScrollerOptions) {
      var _items = props.suggestions ? props.optionGroupLabel ? flattenGroupedItems(props === null || props === void 0 ? void 0 : props.suggestions) : props.suggestions : null;
      var virtualScrollerProps = _objectSpread$1(_objectSpread$1({}, props.virtualScrollerOptions), {
        style: _objectSpread$1(_objectSpread$1({}, props.virtualScrollerOptions.style), {
          height: props.scrollHeight
        }),
        autoSize: true,
        items: _items,
        itemTemplate: function itemTemplate(item2, options) {
          return item2 && createItem(item2, options.index, options);
        },
        contentTemplate: function contentTemplate(options) {
          var listProps2 = mergeProps({
            id: props.listId,
            ref: options.contentRef,
            style: options.style,
            className: cx("list", {
              virtualScrollerProps,
              options
            }),
            role: "listbox"
          }, _ptm("list"));
          return /* @__PURE__ */ React.createElement("ul", listProps2, options.children);
        }
      });
      return /* @__PURE__ */ React.createElement(VirtualScroller, _extends({
        ref: props.virtualScrollerRef
      }, virtualScrollerProps, {
        pt: _ptm("virtualScroller"),
        __parentMetadata: {
          parent: props.metaData
        }
      }));
    }
    var items = createItems();
    var listProps = mergeProps({
      id: props.listId,
      className: cx("list"),
      role: "listbox"
    }, _ptm("list"));
    var listWrapperProps = mergeProps({
      className: cx("listWrapper"),
      style: {
        maxHeight: props.scrollHeight || "auto"
      }
    }, _ptm("listWrapper"));
    return /* @__PURE__ */ React.createElement("div", listWrapperProps, /* @__PURE__ */ React.createElement("ul", listProps, items));
  };
  var createElement2 = function createElement3() {
    var style = _objectSpread$1({}, props.panelStyle || {});
    var content = createContent();
    var footer = createFooter();
    var panelProps = mergeProps({
      className: classNames(props.panelClassName, cx("panel", {
        context
      })),
      style,
      onClick: function onClick(e) {
        return props.onClick(e);
      }
    }, _ptm("panel"));
    var transitionProps = mergeProps({
      classNames: cx("transition"),
      "in": props["in"],
      timeout: {
        enter: 120,
        exit: 100
      },
      options: props.transitionOptions,
      unmountOnExit: true,
      onEnter: props.onEnter,
      onEntering: props.onEntering,
      onEntered: props.onEntered,
      onExit: props.onExit,
      onExited: props.onExited
    }, _ptm("transition"));
    return /* @__PURE__ */ React.createElement(CSSTransition, _extends({
      nodeRef: ref
    }, transitionProps), /* @__PURE__ */ React.createElement("div", _extends({
      ref
    }, panelProps), content, footer));
  };
  var element = createElement2();
  return /* @__PURE__ */ React.createElement(Portal, {
    element,
    appendTo: props.appendTo
  });
}));
AutoCompletePanel.displayName = "AutoCompletePanel";
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
var AutoComplete = /* @__PURE__ */ React.memo(/* @__PURE__ */ React.forwardRef(function(inProps, ref) {
  var mergeProps = useMergeProps();
  var context = React.useContext(PrimeReactContext);
  var props = AutoCompleteBase.getProps(inProps, context);
  var _React$useState = React.useState(props.id), _React$useState2 = _slicedToArray(_React$useState, 2), idState = _React$useState2[0], setIdState = _React$useState2[1];
  var _React$useState3 = React.useState(false), _React$useState4 = _slicedToArray(_React$useState3, 2), searchingState = _React$useState4[0], setSearchingState = _React$useState4[1];
  var _React$useState5 = React.useState(false), _React$useState6 = _slicedToArray(_React$useState5, 2), focusedState = _React$useState6[0], setFocusedState = _React$useState6[1];
  var _React$useState7 = React.useState(false), _React$useState8 = _slicedToArray(_React$useState7, 2), overlayVisibleState = _React$useState8[0], setOverlayVisibleState = _React$useState8[1];
  var metaData = {
    props,
    state: {
      id: idState,
      searching: searchingState,
      focused: focusedState,
      overlayVisible: overlayVisibleState
    }
  };
  var _AutoCompleteBase$set = AutoCompleteBase.setMetaData(metaData), ptm = _AutoCompleteBase$set.ptm, cx = _AutoCompleteBase$set.cx, sx = _AutoCompleteBase$set.sx, isUnstyled = _AutoCompleteBase$set.isUnstyled;
  useHandleStyle(AutoCompleteBase.css.styles, isUnstyled, {
    name: "autocomplete"
  });
  var elementRef = React.useRef(null);
  var overlayRef = React.useRef(null);
  var inputRef = React.useRef(props.inputRef);
  var multiContainerRef = React.useRef(null);
  var virtualScrollerRef = React.useRef(null);
  var timeout = React.useRef(null);
  var selectedItem = React.useRef(null);
  var _useOverlayListener = useOverlayListener({
    target: elementRef,
    overlay: overlayRef,
    listener: function listener(event, _ref) {
      var type = _ref.type, valid = _ref.valid;
      if (valid) {
        type === "outside" ? !isInputClicked(event) && hide() : hide();
      }
    },
    when: overlayVisibleState
  }), _useOverlayListener2 = _slicedToArray(_useOverlayListener, 2), bindOverlayListener = _useOverlayListener2[0], unbindOverlayListener = _useOverlayListener2[1];
  var isInputClicked = function isInputClicked2(event) {
    return props.multiple ? event.target === multiContainerRef.current || multiContainerRef.current.contains(event.target) : event.target === inputRef.current;
  };
  var onInputChange = function onInputChange2(event) {
    if (timeout.current) {
      clearTimeout(timeout.current);
    }
    var query = event.target.value;
    if (!props.multiple) {
      updateModel(event, query);
    }
    if (ObjectUtils.isEmpty(query)) {
      hide();
      props.onClear && props.onClear(event);
    } else if (query.length >= props.minLength) {
      timeout.current = setTimeout(function() {
        search(event, query, "input");
      }, props.delay);
    } else {
      hide();
    }
  };
  var search = function search2(event, query, source) {
    if (query === void 0 || query === null) {
      return;
    }
    if (source === "input" && query.trim().length === 0) {
      return;
    }
    if (props.completeMethod) {
      setSearchingState(true);
      props.completeMethod({
        originalEvent: event,
        query
      });
    }
  };
  var selectItem = function selectItem2(event, option, preventInputFocus) {
    if (props.multiple) {
      inputRef.current.value = "";
      if (!isSelected(option) && isAllowMoreValues()) {
        var newValue = props.value ? [].concat(_toConsumableArray(props.value), [option]) : [option];
        updateModel(event, newValue);
      }
    } else {
      updateInputField(option);
      updateModel(event, option);
    }
    if (props.onSelect) {
      props.onSelect({
        originalEvent: event,
        value: option
      });
    }
    if (!preventInputFocus) {
      DomHandler.focus(inputRef.current);
      hide();
    }
  };
  var updateModel = function updateModel2(event, value) {
    if (props.onChange) {
      props.onChange({
        originalEvent: event,
        value,
        stopPropagation: function stopPropagation() {
          event.stopPropagation();
        },
        preventDefault: function preventDefault() {
          event.preventDefault();
        },
        target: {
          name: props.name,
          id: idState,
          value
        }
      });
    }
    selectedItem.current = ObjectUtils.isNotEmpty(value) ? value : null;
  };
  var formatValue = function formatValue2(value) {
    var useTemplate = arguments.length > 1 && arguments[1] !== void 0 ? arguments[1] : false;
    if (ObjectUtils.isEmpty(value)) return "";
    if (typeof value === "string") return value;
    if (useTemplate && props.selectedItemTemplate) {
      return ObjectUtils.getJSXElement(props.selectedItemTemplate, value) || value;
    }
    if (props.field) {
      var _ObjectUtils$resolveF;
      return (_ObjectUtils$resolveF = ObjectUtils.resolveFieldData(value, props.field)) !== null && _ObjectUtils$resolveF !== void 0 ? _ObjectUtils$resolveF : value;
    }
    return value;
  };
  var updateInputField = function updateInputField2(value) {
    inputRef.current.value = formatValue(value);
  };
  var show = function show2() {
    setOverlayVisibleState(true);
  };
  var hide = function hide2() {
    setOverlayVisibleState(false);
    setSearchingState(false);
  };
  var onOverlayEnter = function onOverlayEnter2() {
    ZIndexUtils.set("overlay", overlayRef.current, context && context.autoZIndex || PrimeReact.autoZIndex, context && context.zIndex.overlay || PrimeReact.zIndex.overlay);
    DomHandler.addStyles(overlayRef.current, {
      position: "absolute",
      top: "0",
      left: "0"
    });
    alignOverlay();
  };
  var onOverlayEntering = function onOverlayEntering2() {
    if (props.autoHighlight && props.suggestions && props.suggestions.length) {
      autoHighlightFirstOption();
    }
  };
  var autoHighlightFirstOption = function autoHighlightFirstOption2() {
    var _getScrollableElement;
    var element = (_getScrollableElement = getScrollableElement()) === null || _getScrollableElement === void 0 || (_getScrollableElement = _getScrollableElement.firstChild) === null || _getScrollableElement === void 0 ? void 0 : _getScrollableElement.firstChild;
    if (element) {
      !isUnstyled() && DomHandler.addClass(element, "p-highlight");
      element.setAttribute("data-p-highlight", true);
    }
  };
  var onOverlayEntered = function onOverlayEntered2() {
    bindOverlayListener();
    props.onShow && props.onShow();
  };
  var onOverlayExit = function onOverlayExit2() {
    unbindOverlayListener();
  };
  var onOverlayExited = function onOverlayExited2() {
    ZIndexUtils.clear(overlayRef.current);
    props.onHide && props.onHide();
  };
  var alignOverlay = function alignOverlay2() {
    var target = props.multiple ? multiContainerRef.current : inputRef.current;
    DomHandler.alignOverlay(overlayRef.current, target, props.appendTo || context && context.appendTo || PrimeReact.appendTo);
  };
  var onPanelClick = function onPanelClick2(event) {
    OverlayService.emit("overlay-click", {
      originalEvent: event,
      target: elementRef.current
    });
  };
  var onDropdownClick = function onDropdownClick2(event) {
    if (props.dropdownAutoFocus) {
      DomHandler.focus(inputRef.current, props.dropdownAutoFocus);
    }
    if (props.dropdownMode === "blank") {
      search(event, "", "dropdown");
    } else if (props.dropdownMode === "current") {
      search(event, inputRef.current.value, "dropdown");
    }
    if (props.onDropdownClick) {
      props.onDropdownClick({
        originalEvent: event,
        query: inputRef.current.value
      });
    }
  };
  var removeItem = function removeItem2(event, index) {
    var removedValue = props.value[index];
    var newValue = props.value.filter(function(_, i) {
      return index !== i;
    });
    updateModel(event, newValue);
    if (props.onUnselect) {
      props.onUnselect({
        originalEvent: event,
        value: removedValue
      });
    }
  };
  var onInputKeyDown = function onInputKeyDown2(event) {
    if (overlayVisibleState) {
      var highlightItem = DomHandler.findSingle(overlayRef.current, 'li[data-p-highlight="true"]');
      switch (event.which) {
        //down
        case 40:
          if (highlightItem) {
            var nextElement = _findNextItem(highlightItem);
            if (nextElement) {
              !isUnstyled() && DomHandler.addClass(nextElement, "p-highlight");
              nextElement.setAttribute("data-p-highlight", true);
              !isUnstyled() && DomHandler.removeClass(highlightItem, "p-highlight");
              highlightItem.setAttribute("data-p-highlight", false);
              DomHandler.scrollInView(getScrollableElement(), nextElement);
            }
          } else {
            highlightItem = DomHandler.findSingle(overlayRef.current, "li");
            if (DomHandler.getAttribute(highlightItem, "data-pc-section") === "itemgroup") {
              highlightItem = _findNextItem(highlightItem);
            }
            if (highlightItem) {
              !isUnstyled() && DomHandler.addClass(highlightItem, "p-highlight");
              highlightItem.setAttribute("data-p-highlight", true);
            }
          }
          event.preventDefault();
          break;
        //up
        case 38:
          if (highlightItem) {
            var previousElement = _findPrevItem(highlightItem);
            if (previousElement) {
              !isUnstyled() && DomHandler.addClass(previousElement, "p-highlight");
              previousElement.setAttribute("data-p-highlight", true);
              !isUnstyled() && DomHandler.removeClass(highlightItem, "p-highlight");
              highlightItem.setAttribute("data-p-highlight", false);
              DomHandler.scrollInView(getScrollableElement(), previousElement);
            }
          }
          event.preventDefault();
          break;
        //enter
        case 13:
          if (highlightItem) {
            selectHighlightItem(event, highlightItem);
            hide();
            event.preventDefault();
          }
          break;
        //escape
        case 27:
          hide();
          event.preventDefault();
          break;
        //tab
        case 9:
          if (highlightItem) {
            selectHighlightItem(event, highlightItem);
          }
          hide();
          break;
      }
    }
    if (props.multiple) {
      switch (event.which) {
        //backspace
        case 8:
          if (props.value && props.value.length && !inputRef.current.value) {
            var removedValue = props.value[props.value.length - 1];
            var newValue = props.value.slice(0, -1);
            updateModel(event, newValue);
            if (props.onUnselect) {
              props.onUnselect({
                originalEvent: event,
                value: removedValue
              });
            }
          }
          break;
      }
    }
  };
  var selectHighlightItem = function selectHighlightItem2(event, item2) {
    if (props.optionGroupLabel) {
      var optionGroup = props.suggestions[item2.dataset.group];
      selectItem(event, getOptionGroupChildren(optionGroup)[item2.dataset.index]);
    } else {
      selectItem(event, props.suggestions[item2.getAttribute("index")]);
    }
  };
  var _findNextItem = function findNextItem(item2) {
    var nextItem = item2.nextElementSibling;
    return nextItem ? DomHandler.getAttribute(nextItem, "data-pc-section") === "itemgroup" ? _findNextItem(nextItem) : nextItem : null;
  };
  var _findPrevItem = function findPrevItem(item2) {
    var prevItem = item2.previousElementSibling;
    return prevItem ? DomHandler.getAttribute(prevItem, "data-pc-section") === "itemgroup" ? _findPrevItem(prevItem) : prevItem : null;
  };
  var onInputFocus = function onInputFocus2(event) {
    setFocusedState(true);
    props.onFocus && props.onFocus(event);
  };
  var forceItemSelection = function forceItemSelection2(event) {
    if (props.multiple) {
      inputRef.current.value = "";
      return;
    }
    var inputValue = ObjectUtils.trim(event.target.value).toLowerCase();
    var allItems = (props.suggestions || []).flatMap(function(group) {
      return group.items ? group.items : [group];
    });
    var item2 = allItems.find(function(it) {
      var value = props.field ? ObjectUtils.resolveFieldData(it, props.field) : it;
      var trimmedValue = value ? ObjectUtils.trim(value).toLowerCase() : "";
      return trimmedValue && inputValue === trimmedValue;
    });
    if (item2) {
      selectItem(event, item2, true);
    } else {
      inputRef.current.value = "";
      updateModel(event, null);
      props.onClear && props.onClear(event);
    }
  };
  var onInputBlur = function onInputBlur2(event) {
    setFocusedState(false);
    if (props.forceSelection) {
      forceItemSelection(event);
    }
    props.onBlur && props.onBlur(event);
  };
  var onMultiContainerClick = function onMultiContainerClick2(event) {
    DomHandler.focus(inputRef.current);
    props.onClick && props.onClick(event);
  };
  var onMultiInputFocus = function onMultiInputFocus2(event) {
    onInputFocus(event);
    !isUnstyled() && DomHandler.addClass(multiContainerRef.current, "p-focus");
    multiContainerRef.current.setAttribute("data-p-focus", true);
  };
  var onMultiInputBlur = function onMultiInputBlur2(event) {
    onInputBlur(event);
    !isUnstyled() && DomHandler.removeClass(multiContainerRef.current, "p-focus");
    multiContainerRef.current.setAttribute("data-p-focus", false);
  };
  var isSelected = function isSelected2(val) {
    return props.value ? props.value.some(function(v) {
      return ObjectUtils.equals(v, val);
    }) : false;
  };
  var getScrollableElement = function getScrollableElement2() {
    var _overlayRef$current;
    return overlayRef === null || overlayRef === void 0 || (_overlayRef$current = overlayRef.current) === null || _overlayRef$current === void 0 ? void 0 : _overlayRef$current.firstChild;
  };
  var getOptionGroupLabel = function getOptionGroupLabel2(optionGroup) {
    return props.optionGroupLabel ? ObjectUtils.resolveFieldData(optionGroup, props.optionGroupLabel) : optionGroup;
  };
  var getOptionGroupChildren = function getOptionGroupChildren2(optionGroup) {
    return ObjectUtils.resolveFieldData(optionGroup, props.optionGroupChildren);
  };
  var isAllowMoreValues = function isAllowMoreValues2() {
    return !props.value || !props.selectionLimit || props.value.length < props.selectionLimit;
  };
  React.useEffect(function() {
    ObjectUtils.combinedRefs(inputRef, props.inputRef);
  }, [inputRef, props.inputRef]);
  useMountEffect(function() {
    if (!idState) {
      setIdState(UniqueComponentId());
    }
    if (props.autoFocus) {
      DomHandler.focus(inputRef.current, props.autoFocus);
    }
    alignOverlay();
  });
  useUpdateEffect(function() {
    if (searchingState && props.autoHighlight && props.suggestions && props.suggestions.length) {
      autoHighlightFirstOption();
    }
  }, [searchingState]);
  useUpdateEffect(function() {
    if (searchingState) {
      ObjectUtils.isNotEmpty(props.suggestions) || props.showEmptyMessage ? show() : hide();
      setSearchingState(false);
    }
  }, [props.suggestions]);
  useUpdateEffect(function() {
    if (inputRef.current && !props.multiple) {
      updateInputField(props.value);
    }
    if (overlayVisibleState) {
      alignOverlay();
    }
  });
  useUnmountEffect(function() {
    if (timeout.current) {
      clearTimeout(timeout.current);
    }
    ZIndexUtils.clear(overlayRef.current);
  });
  React.useImperativeHandle(ref, function() {
    return {
      props,
      search,
      show,
      hide,
      focus: function focus() {
        return DomHandler.focus(inputRef.current);
      },
      getElement: function getElement() {
        return elementRef.current;
      },
      getOverlay: function getOverlay() {
        return overlayRef.current;
      },
      getInput: function getInput() {
        return inputRef.current;
      },
      getVirtualScroller: function getVirtualScroller() {
        return virtualScrollerRef.current;
      }
    };
  });
  var createSimpleAutoComplete = function createSimpleAutoComplete2() {
    var value = formatValue(props.value);
    var ariaControls = overlayVisibleState ? idState + "_list" : null;
    return /* @__PURE__ */ React.createElement(InputText, _extends({
      ref: inputRef,
      id: props.inputId,
      type: props.type,
      name: props.name,
      defaultValue: value,
      role: "combobox",
      "aria-autocomplete": "list",
      "aria-controls": ariaControls,
      "aria-haspopup": "listbox",
      "aria-expanded": overlayVisibleState,
      className: classNames(props.inputClassName, cx("input", {
        context
      })),
      style: props.inputStyle,
      autoComplete: "off",
      readOnly: props.readOnly,
      required: props.required,
      disabled: props.disabled,
      placeholder: props.placeholder,
      size: props.size,
      maxLength: props.maxLength,
      tabIndex: props.tabIndex,
      onBlur: onInputBlur,
      onFocus: onInputFocus,
      onChange: onInputChange,
      onMouseDown: props.onMouseDown,
      onKeyUp: props.onKeyUp,
      onKeyDown: onInputKeyDown,
      onKeyPress: props.onKeyPress,
      onContextMenu: props.onContextMenu,
      onClick: props.onClick,
      onDoubleClick: props.onDblClick,
      pt: ptm("input"),
      unstyled: props.unstyled
    }, ariaProps, {
      __parentMetadata: {
        parent: metaData
      }
    }));
  };
  var createChips = function createChips2() {
    if (ObjectUtils.isNotEmpty(props.value)) {
      return props.value.map(function(val, index) {
        var key = index + "multi-item";
        var removeTokenIconProps = mergeProps({
          className: cx("removeTokenIcon"),
          onClick: function onClick(e) {
            return removeItem(e, index);
          }
        }, ptm("removeTokenIcon"));
        var icon = props.removeTokenIcon || /* @__PURE__ */ React.createElement(TimesCircleIcon, removeTokenIconProps);
        var removeTokenIcon = !props.disabled && IconUtils.getJSXIcon(icon, _objectSpread({}, removeTokenIconProps), {
          props
        });
        var tokenProps = mergeProps({
          className: cx("token")
        }, ptm("token"));
        var tokenLabelProps = mergeProps({
          className: cx("tokenLabel")
        }, ptm("tokenLabel"));
        return /* @__PURE__ */ React.createElement("li", _extends({
          key
        }, tokenProps), /* @__PURE__ */ React.createElement("span", tokenLabelProps, formatValue(val, true)), removeTokenIcon);
      });
    }
    selectedItem.current = null;
    return null;
  };
  var createMultiInput = function createMultiInput2(allowMoreValues) {
    var ariaControls = overlayVisibleState ? idState + "_list" : null;
    var inputTokenProps = mergeProps({
      className: cx("inputToken")
    }, ptm("inputToken"));
    var inputProps = mergeProps(_objectSpread({
      id: props.inputId,
      ref: inputRef,
      "aria-autocomplete": "list",
      "aria-controls": ariaControls,
      "aria-expanded": overlayVisibleState,
      "aria-haspopup": "listbox",
      autoComplete: "off",
      className: props.inputClassName,
      disabled: props.disabled,
      maxLength: props.maxLength,
      name: props.name,
      onBlur: onMultiInputBlur,
      onChange: allowMoreValues ? onInputChange : void 0,
      onFocus: onMultiInputFocus,
      onKeyDown: allowMoreValues ? onInputKeyDown : void 0,
      onKeyPress: props.onKeyPress,
      onKeyUp: props.onKeyUp,
      placeholder: allowMoreValues ? props.placeholder : void 0,
      readOnly: props.readOnly || !allowMoreValues,
      required: props.required,
      role: "combobox",
      style: props.inputStyle,
      tabIndex: props.tabIndex,
      type: props.type
    }, ariaProps), ptm("input"));
    return /* @__PURE__ */ React.createElement("li", inputTokenProps, /* @__PURE__ */ React.createElement("input", inputProps));
  };
  var createMultipleAutoComplete = function createMultipleAutoComplete2() {
    var allowMoreValues = isAllowMoreValues();
    var tokens = createChips();
    var input3 = createMultiInput(allowMoreValues);
    var containerProps = mergeProps({
      ref: multiContainerRef,
      className: cx("container", {
        context
      }),
      onClick: allowMoreValues ? onMultiContainerClick : void 0,
      onContextMenu: props.onContextMenu,
      onMouseDown: props.onMouseDown,
      onDoubleClick: props.onDblClick,
      "data-p-focus": focusedState,
      "data-p-disabled": props.disabled
    }, ptm("container"));
    return /* @__PURE__ */ React.createElement("ul", containerProps, tokens, input3);
  };
  var createDropdown = function createDropdown2() {
    if (props.dropdown) {
      var ariaLabel = props.dropdownAriaLabel || props.placeholder || localeOption("choose");
      return /* @__PURE__ */ React.createElement(Button, {
        type: "button",
        icon: props.dropdownIcon || /* @__PURE__ */ React.createElement(ChevronDownIcon, null),
        className: cx("dropdownButton"),
        disabled: props.disabled,
        onClick: onDropdownClick,
        "aria-label": ariaLabel,
        pt: ptm("dropdownButton"),
        __parentMetadata: {
          parent: metaData
        }
      });
    }
    return null;
  };
  var createLoader = function createLoader2() {
    if (searchingState) {
      var loadingIconProps = mergeProps({
        className: cx("loadingIcon")
      }, ptm("loadingIcon"));
      var icon = props.loadingIcon || /* @__PURE__ */ React.createElement(SpinnerIcon, _extends({}, loadingIconProps, {
        spin: true
      }));
      var loaderIcon = IconUtils.getJSXIcon(icon, _objectSpread({}, loadingIconProps), {
        props
      });
      return loaderIcon;
    }
    return null;
  };
  var createInput = function createInput2() {
    return props.multiple ? createMultipleAutoComplete() : createSimpleAutoComplete();
  };
  var listId = idState + "_list";
  var hasTooltip = ObjectUtils.isNotEmpty(props.tooltip);
  var otherProps = AutoCompleteBase.getOtherProps(props);
  var ariaProps = ObjectUtils.reduceKeys(otherProps, DomHandler.ARIA_PROPS);
  var loader = createLoader();
  var input2 = createInput();
  var dropdown = createDropdown();
  var rootProps = mergeProps({
    id: idState,
    ref: elementRef,
    style: props.style,
    className: classNames(props.className, cx("root", {
      focusedState
    }))
  }, otherProps, ptm("root"));
  return /* @__PURE__ */ React.createElement(React.Fragment, null, /* @__PURE__ */ React.createElement("span", rootProps, input2, loader, dropdown, /* @__PURE__ */ React.createElement(AutoCompletePanel, _extends({
    hostName: "AutoComplete",
    ref: overlayRef,
    virtualScrollerRef
  }, props, {
    listId,
    onItemClick: selectItem,
    selectedItem,
    onClick: onPanelClick,
    getOptionGroupLabel,
    getOptionGroupChildren,
    "in": overlayVisibleState,
    onEnter: onOverlayEnter,
    onEntering: onOverlayEntering,
    onEntered: onOverlayEntered,
    onExit: onOverlayExit,
    onExited: onOverlayExited,
    ptm,
    cx,
    sx
  }))), hasTooltip && /* @__PURE__ */ React.createElement(Tooltip, _extends({
    target: elementRef,
    content: props.tooltip,
    pt: ptm("tooltip")
  }, props.tooltipOptions)));
}));
AutoComplete.displayName = "AutoComplete";
export {
  AutoComplete as default
};
