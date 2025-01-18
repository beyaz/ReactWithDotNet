import {
  MinusIcon
} from "./chunk-QMKDTUKP.js";
import {
  CheckIcon
} from "./chunk-O7CBK6CT.js";
import {
  ChevronDownIcon
} from "./chunk-H5HP4A3G.js";
import {
  SearchIcon
} from "./chunk-JB3TRCHR.js";
import {
  SpinnerIcon
} from "./chunk-TEWXDFAB.js";
import {
  Tooltip
} from "./chunk-2PX4WDKA.js";
import "./chunk-CMYF6AIZ.js";
import {
  ChevronRightIcon
} from "./chunk-WVAF3FHQ.js";
import {
  Ripple
} from "./chunk-56TI4EJO.js";
import "./chunk-2FIYIUK5.js";
import {
  ComponentBase,
  DomHandler,
  IconUtils,
  ObjectUtils,
  PrimeReactContext,
  classNames,
  localeOption,
  useHandleStyle,
  useMergeProps,
  useMountEffect,
  useUpdateEffect
} from "./chunk-NBA33TRK.js";
import "./chunk-QNQS7X5M.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/primereact/tree/tree.esm.js
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
function _arrayLikeToArray$2(r, a) {
  (null == a || a > r.length) && (a = r.length);
  for (var e = 0, n = Array(a); e < a; e++) n[e] = r[e];
  return n;
}
function _arrayWithoutHoles(r) {
  if (Array.isArray(r)) return _arrayLikeToArray$2(r);
}
function _iterableToArray(r) {
  if ("undefined" != typeof Symbol && null != r[Symbol.iterator] || null != r["@@iterator"]) return Array.from(r);
}
function _unsupportedIterableToArray$2(r, a) {
  if (r) {
    if ("string" == typeof r) return _arrayLikeToArray$2(r, a);
    var t = {}.toString.call(r).slice(8, -1);
    return "Object" === t && r.constructor && (t = r.constructor.name), "Map" === t || "Set" === t ? Array.from(r) : "Arguments" === t || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t) ? _arrayLikeToArray$2(r, a) : void 0;
  }
}
function _nonIterableSpread() {
  throw new TypeError("Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
}
function _toConsumableArray(r) {
  return _arrayWithoutHoles(r) || _iterableToArray(r) || _unsupportedIterableToArray$2(r) || _nonIterableSpread();
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
  return _arrayWithHoles(r) || _iterableToArrayLimit(r, e) || _unsupportedIterableToArray$2(r, e) || _nonIterableRest();
}
var useUpdateEffect2 = function useUpdateEffect3(fn, deps) {
  var mounted = React.useRef(false);
  return React.useEffect(function() {
    if (!mounted.current) {
      mounted.current = true;
      return;
    }
    return fn && fn();
  }, deps);
};
var classes$1 = {
  root: function root(_ref) {
    var props = _ref.props;
    return classNames("p-tree p-component", {
      "p-tree-selectable": props.selectionMode,
      "p-tree-loading": props.loading,
      "p-disabled": props.disabled
    });
  },
  loadingOverlay: "p-tree-loading-overlay p-component-overlay",
  loadingIcon: "p-tree-loading-icon",
  filterContainer: "p-tree-filter-container",
  input: "p-tree-filter p-inputtext p-component",
  searchIcon: "p-tree-filter-icon",
  container: "p-tree-container",
  node: function node(_ref2) {
    var isLeaf = _ref2.isLeaf;
    return classNames("p-treenode", {
      "p-treenode-leaf": isLeaf
    });
  },
  content: function content(_ref3) {
    var props = _ref3.nodeProps, checked = _ref3.checked, selected = _ref3.selected, isCheckboxSelectionMode = _ref3.isCheckboxSelectionMode;
    return classNames("p-treenode-content", {
      "p-treenode-selectable": props.selectionMode && props.node.selectable !== false,
      "p-highlight": isCheckboxSelectionMode() ? checked : selected,
      "p-highlight-contextmenu": props.contextMenuSelectionKey && props.contextMenuSelectionKey === props.node.key,
      "p-disabled": props.disabled
    });
  },
  toggler: "p-tree-toggler p-link",
  togglerIcon: "p-tree-toggler-icon",
  nodeCheckbox: function nodeCheckbox(_ref4) {
    var partialChecked = _ref4.partialChecked;
    return classNames({
      "p-indeterminate": partialChecked
    });
  },
  nodeIcon: "p-treenode-icon",
  label: "p-treenode-label",
  subgroup: "p-treenode-children",
  checkIcon: "p-checkbox-icon",
  emptyMessage: "p-treenode p-tree-empty-message",
  droppoint: "p-treenode-droppoint",
  header: "p-tree-header",
  footer: "p-tree-footer"
};
var TreeBase = ComponentBase.extend({
  defaultProps: {
    __TYPE: "Tree",
    __parentMetadata: null,
    id: null,
    value: null,
    ariaLabel: null,
    ariaLabelledBy: null,
    checkboxIcon: null,
    className: null,
    collapseIcon: null,
    contentClassName: null,
    contentStyle: null,
    contextMenuSelectionKey: null,
    disabled: false,
    dragdropScope: null,
    emptyMessage: null,
    expandIcon: null,
    expandedKeys: null,
    filter: false,
    filterBy: "label",
    filterIcon: null,
    filterLocale: void 0,
    filterMode: "lenient",
    filterPlaceholder: null,
    filterTemplate: null,
    filterValue: null,
    footer: null,
    header: null,
    level: 0,
    loading: false,
    loadingIcon: null,
    metaKeySelection: false,
    nodeTemplate: null,
    onCollapse: null,
    onContextMenu: null,
    onContextMenuSelectionChange: null,
    onDragDrop: null,
    onExpand: null,
    onFilterValueChange: null,
    onNodeClick: null,
    onNodeDoubleClick: null,
    onSelect: null,
    onSelectionChange: null,
    onToggle: null,
    onUnselect: null,
    propagateSelectionDown: true,
    propagateSelectionUp: true,
    selectionKeys: null,
    selectionMode: null,
    showHeader: true,
    style: null,
    togglerTemplate: null,
    children: void 0
  },
  css: {
    classes: classes$1
  }
});
var classes = {
  box: "p-checkbox-box",
  input: "p-checkbox-input",
  icon: "p-checkbox-icon",
  root: function root2(_ref) {
    var props = _ref.props, checked = _ref.checked, context = _ref.context;
    return classNames("p-checkbox p-component", {
      "p-highlight": checked,
      "p-disabled": props.disabled,
      "p-invalid": props.invalid,
      "p-variant-filled": props.variant ? props.variant === "filled" : context && context.inputStyle === "filled"
    });
  }
};
var CheckboxBase = ComponentBase.extend({
  defaultProps: {
    __TYPE: "Checkbox",
    autoFocus: false,
    checked: false,
    className: null,
    disabled: false,
    falseValue: false,
    icon: null,
    id: null,
    inputId: null,
    inputRef: null,
    invalid: false,
    variant: null,
    name: null,
    onChange: null,
    onContextMenu: null,
    onMouseDown: null,
    readOnly: false,
    required: false,
    style: null,
    tabIndex: null,
    tooltip: null,
    tooltipOptions: null,
    trueValue: true,
    value: null,
    children: void 0
  },
  css: {
    classes
  }
});
function ownKeys$2(e, r) {
  var t = Object.keys(e);
  if (Object.getOwnPropertySymbols) {
    var o = Object.getOwnPropertySymbols(e);
    r && (o = o.filter(function(r2) {
      return Object.getOwnPropertyDescriptor(e, r2).enumerable;
    })), t.push.apply(t, o);
  }
  return t;
}
function _objectSpread$2(e) {
  for (var r = 1; r < arguments.length; r++) {
    var t = null != arguments[r] ? arguments[r] : {};
    r % 2 ? ownKeys$2(Object(t), true).forEach(function(r2) {
      _defineProperty(e, r2, t[r2]);
    }) : Object.getOwnPropertyDescriptors ? Object.defineProperties(e, Object.getOwnPropertyDescriptors(t)) : ownKeys$2(Object(t)).forEach(function(r2) {
      Object.defineProperty(e, r2, Object.getOwnPropertyDescriptor(t, r2));
    });
  }
  return e;
}
var Checkbox = /* @__PURE__ */ React.memo(/* @__PURE__ */ React.forwardRef(function(inProps, ref) {
  var mergeProps = useMergeProps();
  var context = React.useContext(PrimeReactContext);
  var props = CheckboxBase.getProps(inProps, context);
  var _React$useState = React.useState(false), _React$useState2 = _slicedToArray(_React$useState, 2), focusedState = _React$useState2[0], setFocusedState = _React$useState2[1];
  var _CheckboxBase$setMeta = CheckboxBase.setMetaData({
    props,
    state: {
      focused: focusedState
    },
    context: {
      checked: props.checked === props.trueValue,
      disabled: props.disabled
    }
  }), ptm = _CheckboxBase$setMeta.ptm, cx = _CheckboxBase$setMeta.cx, isUnstyled = _CheckboxBase$setMeta.isUnstyled;
  useHandleStyle(CheckboxBase.css.styles, isUnstyled, {
    name: "checkbox"
  });
  var elementRef = React.useRef(null);
  var inputRef = React.useRef(props.inputRef);
  var isChecked = function isChecked2() {
    return props.checked === props.trueValue;
  };
  var _onChange = function onChange(event) {
    if (props.disabled || props.readonly) {
      return;
    }
    if (props.onChange) {
      var _props$onChange;
      var _checked = isChecked();
      var value = _checked ? props.falseValue : props.trueValue;
      var eventData = {
        originalEvent: event,
        value: props.value,
        checked: value,
        stopPropagation: function stopPropagation() {
          event === null || event === void 0 || event.stopPropagation();
        },
        preventDefault: function preventDefault() {
          event === null || event === void 0 || event.preventDefault();
        },
        target: {
          type: "checkbox",
          name: props.name,
          id: props.id,
          value: props.value,
          checked: value
        }
      };
      props === null || props === void 0 || (_props$onChange = props.onChange) === null || _props$onChange === void 0 || _props$onChange.call(props, eventData);
      if (event.defaultPrevented) {
        return;
      }
      DomHandler.focus(inputRef.current);
    }
  };
  var _onFocus = function onFocus() {
    var _props$onFocus;
    setFocusedState(true);
    props === null || props === void 0 || (_props$onFocus = props.onFocus) === null || _props$onFocus === void 0 || _props$onFocus.call(props);
  };
  var _onBlur = function onBlur() {
    var _props$onBlur;
    setFocusedState(false);
    props === null || props === void 0 || (_props$onBlur = props.onBlur) === null || _props$onBlur === void 0 || _props$onBlur.call(props);
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
  useUpdateEffect(function() {
    inputRef.current.checked = isChecked();
  }, [props.checked, props.trueValue]);
  useMountEffect(function() {
    if (props.autoFocus) {
      DomHandler.focus(inputRef.current, props.autoFocus);
    }
  });
  var checked = isChecked();
  var hasTooltip = ObjectUtils.isNotEmpty(props.tooltip);
  var otherProps = CheckboxBase.getOtherProps(props);
  var rootProps = mergeProps({
    id: props.id,
    className: classNames(props.className, cx("root", {
      checked,
      context
    })),
    style: props.style,
    "data-p-highlight": checked,
    "data-p-disabled": props.disabled,
    onContextMenu: props.onContextMenu,
    onMouseDown: props.onMouseDown
  }, otherProps, ptm("root"));
  var createInputElement = function createInputElement2() {
    var ariaProps = ObjectUtils.reduceKeys(otherProps, DomHandler.ARIA_PROPS);
    var inputProps = mergeProps(_objectSpread$2({
      id: props.inputId,
      type: "checkbox",
      className: cx("input"),
      name: props.name,
      tabIndex: props.tabIndex,
      onFocus: function onFocus(e) {
        return _onFocus();
      },
      onBlur: function onBlur(e) {
        return _onBlur();
      },
      onChange: function onChange(e) {
        return _onChange(e);
      },
      disabled: props.disabled,
      readOnly: props.readOnly,
      required: props.required,
      "aria-invalid": props.invalid,
      checked
    }, ariaProps), ptm("input"));
    return /* @__PURE__ */ React.createElement("input", _extends({
      ref: inputRef
    }, inputProps));
  };
  var createBoxElement = function createBoxElement2() {
    var iconProps = mergeProps({
      className: cx("icon")
    }, ptm("icon"));
    var boxProps = mergeProps({
      className: cx("box", {
        checked
      }),
      "data-p-highlight": checked,
      "data-p-disabled": props.disabled
    }, ptm("box"));
    var icon = checked ? props.icon || /* @__PURE__ */ React.createElement(CheckIcon, iconProps) : null;
    var checkboxIcon = IconUtils.getJSXIcon(icon, _objectSpread$2({}, iconProps), {
      props,
      checked
    });
    return /* @__PURE__ */ React.createElement("div", boxProps, checkboxIcon);
  };
  return /* @__PURE__ */ React.createElement(React.Fragment, null, /* @__PURE__ */ React.createElement("div", _extends({
    ref: elementRef
  }, rootProps), createInputElement(), createBoxElement()), hasTooltip && /* @__PURE__ */ React.createElement(Tooltip, _extends({
    target: elementRef,
    content: props.tooltip,
    pt: ptm("tooltip")
  }, props.tooltipOptions)));
}));
Checkbox.displayName = "Checkbox";
function _createForOfIteratorHelper$1(r, e) {
  var t = "undefined" != typeof Symbol && r[Symbol.iterator] || r["@@iterator"];
  if (!t) {
    if (Array.isArray(r) || (t = _unsupportedIterableToArray$1(r)) || e && r && "number" == typeof r.length) {
      t && (r = t);
      var _n = 0, F = function F2() {
      };
      return { s: F, n: function n() {
        return _n >= r.length ? { done: true } : { done: false, value: r[_n++] };
      }, e: function e2(r2) {
        throw r2;
      }, f: F };
    }
    throw new TypeError("Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
  }
  var o, a = true, u = false;
  return { s: function s() {
    t = t.call(r);
  }, n: function n() {
    var r2 = t.next();
    return a = r2.done, r2;
  }, e: function e2(r2) {
    u = true, o = r2;
  }, f: function f() {
    try {
      a || null == t["return"] || t["return"]();
    } finally {
      if (u) throw o;
    }
  } };
}
function _unsupportedIterableToArray$1(r, a) {
  if (r) {
    if ("string" == typeof r) return _arrayLikeToArray$1(r, a);
    var t = {}.toString.call(r).slice(8, -1);
    return "Object" === t && r.constructor && (t = r.constructor.name), "Map" === t || "Set" === t ? Array.from(r) : "Arguments" === t || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t) ? _arrayLikeToArray$1(r, a) : void 0;
  }
}
function _arrayLikeToArray$1(r, a) {
  (null == a || a > r.length) && (a = r.length);
  for (var e = 0, n = Array(a); e < a; e++) n[e] = r[e];
  return n;
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
var UITreeNode = /* @__PURE__ */ React.memo(function(props) {
  var contentRef = React.useRef(null);
  var elementRef = React.useRef(null);
  var nodeTouched = React.useRef(false);
  var mergeProps = useMergeProps();
  var isLeaf = props.isNodeLeaf(props.node);
  var label = props.node.label;
  var expanded = (props.expandedKeys ? props.expandedKeys[props.node.key] !== void 0 : false) || props.node.expanded;
  var ptm = props.ptm, cx = props.cx;
  var getPTOptions = function getPTOptions2(key) {
    return ptm(key, {
      hostName: props.hostName,
      context: {
        selected: !isCheckboxSelectionMode() ? isSelected() : false,
        expanded: expanded || false,
        checked: isCheckboxSelectionMode() ? isChecked() : false,
        isLeaf
      }
    });
  };
  var expand = function expand2(event) {
    var navigateFocusToChild = arguments.length > 1 && arguments[1] !== void 0 ? arguments[1] : false;
    var expandedKeys = props.expandedKeys ? _objectSpread$1({}, props.expandedKeys) : {};
    expandedKeys[props.node.key] = true;
    props.onToggle({
      originalEvent: event,
      value: expandedKeys,
      navigateFocusToChild
    });
    invokeToggleEvents(event, true);
  };
  var collapse = function collapse2(event) {
    var expandedKeys = _objectSpread$1({}, props.expandedKeys);
    delete expandedKeys[props.node.key];
    props.onToggle({
      originalEvent: event,
      value: expandedKeys
    });
    invokeToggleEvents(event, false);
  };
  var onTogglerClick = function onTogglerClick2(event) {
    if (props.disabled) {
      return;
    }
    expanded ? collapse(event) : expand(event, false);
    event.preventDefault();
    event.stopPropagation();
  };
  var invokeToggleEvents = function invokeToggleEvents2(event, isExpanded) {
    if (isExpanded) {
      if (props.onExpand) {
        props.onExpand({
          originalEvent: event,
          node: props.node
        });
      }
    } else if (props.onCollapse) {
      props.onCollapse({
        originalEvent: event,
        node: props.node
      });
    }
  };
  var findNextNonDroppointSibling = function findNextNonDroppointSibling2(nodeElement) {
    var nextNodeSibling = nodeElement.nextSibling;
    if (nextNodeSibling) {
      var isNextDropPoint = nextNodeSibling.getAttribute("data-pc-section") === "droppoint";
      if (isNextDropPoint) {
        if (nextNodeSibling.nextElementSibling) {
          return nextNodeSibling.nextElementSibling;
        } else {
          return null;
        }
      }
      return nextNodeSibling;
    }
    return null;
  };
  var _findNextSiblingOfAncestor = function findNextSiblingOfAncestor(nodeElement) {
    var parentNodeElement = getParentNodeElement(nodeElement);
    return parentNodeElement ? findNextNonDroppointSibling(parentNodeElement) || _findNextSiblingOfAncestor(parentNodeElement) : null;
  };
  var _findLastVisibleDescendant = function findLastVisibleDescendant(nodeElement) {
    var childrenListElement = nodeElement.children[1];
    if (childrenListElement) {
      var offset = props.dragdropScope ? 2 : 1;
      var lastChildElement = childrenListElement.children[childrenListElement.children.length - offset];
      return _findLastVisibleDescendant(lastChildElement);
    }
    return nodeElement;
  };
  var getParentNodeElement = function getParentNodeElement2(nodeElement) {
    var parentNodeElement = nodeElement.parentElement.parentElement;
    return DomHandler.hasClass(parentNodeElement, "p-treenode") ? parentNodeElement : null;
  };
  var focusNode = function focusNode2(element) {
    element && element.focus();
  };
  var onClick = function onClick2(event) {
    if (props.onClick) {
      props.onClick({
        originalEvent: event,
        node: props.node
      });
    }
    var targetNode = event.target.nodeName;
    if (props.disabled || targetNode === "INPUT" || targetNode === "BUTTON" || targetNode === "A" || DomHandler.hasClass(event.target, "p-clickable")) {
      return;
    }
    if (props.selectionMode && props.node.selectable !== false) {
      var selectionKeys;
      if (isCheckboxSelectionMode()) {
        var checked = isChecked();
        selectionKeys = props.selectionKeys ? _objectSpread$1({}, props.selectionKeys) : {};
        if (checked) {
          if (props.propagateSelectionDown) {
            _propagateDown(props.node, false, selectionKeys);
          } else {
            delete selectionKeys[props.node.key];
          }
          if (props.propagateSelectionUp && props.onPropagateUp) {
            props.onPropagateUp({
              originalEvent: event,
              check: false,
              selectionKeys
            });
          }
          if (props.onUnselect) {
            props.onUnselect({
              originalEvent: event,
              node: props.node
            });
          }
        } else {
          if (props.propagateSelectionDown) {
            _propagateDown(props.node, true, selectionKeys);
          } else {
            selectionKeys[props.node.key] = {
              checked: true
            };
          }
          if (props.propagateSelectionUp && props.onPropagateUp) {
            props.onPropagateUp({
              originalEvent: event,
              check: true,
              selectionKeys
            });
          }
          if (props.onSelect) {
            props.onSelect({
              originalEvent: event,
              node: props.node
            });
          }
        }
      } else {
        var selected = isSelected();
        var metaSelection = nodeTouched.current ? false : props.metaKeySelection;
        if (metaSelection) {
          var metaKey = event.metaKey || event.ctrlKey;
          if (selected && metaKey) {
            if (isSingleSelectionMode()) {
              selectionKeys = null;
            } else {
              selectionKeys = _objectSpread$1({}, props.selectionKeys);
              delete selectionKeys[props.node.key];
            }
            if (props.onUnselect) {
              props.onUnselect({
                originalEvent: event,
                node: props.node
              });
            }
          } else {
            if (isSingleSelectionMode()) {
              selectionKeys = props.node.key;
            } else if (isMultipleSelectionMode()) {
              selectionKeys = !metaKey ? {} : props.selectionKeys ? _objectSpread$1({}, props.selectionKeys) : {};
              selectionKeys[props.node.key] = true;
            }
            if (props.onSelect) {
              props.onSelect({
                originalEvent: event,
                node: props.node
              });
            }
          }
        } else if (isSingleSelectionMode()) {
          if (selected) {
            selectionKeys = null;
            if (props.onUnselect) {
              props.onUnselect({
                originalEvent: event,
                node: props.node
              });
            }
          } else {
            selectionKeys = props.node.key;
            if (props.onSelect) {
              props.onSelect({
                originalEvent: event,
                node: props.node
              });
            }
          }
        } else if (selected) {
          selectionKeys = _objectSpread$1({}, props.selectionKeys);
          delete selectionKeys[props.node.key];
          if (props.onUnselect) {
            props.onUnselect({
              originalEvent: event,
              node: props.node
            });
          }
        } else {
          selectionKeys = props.selectionKeys ? _objectSpread$1({}, props.selectionKeys) : {};
          selectionKeys[props.node.key] = true;
          if (props.onSelect) {
            props.onSelect({
              originalEvent: event,
              node: props.node
            });
          }
        }
      }
      if (props.onSelectionChange) {
        props.onSelectionChange({
          originalEvent: event,
          value: selectionKeys
        });
      }
    }
    nodeTouched.current = false;
  };
  var onDoubleClick = function onDoubleClick2(event) {
    if (props.onDoubleClick) {
      props.onDoubleClick({
        originalEvent: event,
        node: props.node
      });
    }
  };
  var onRightClick = function onRightClick2(event) {
    if (props.disabled) {
      return;
    }
    DomHandler.clearSelection();
    if (props.onContextMenuSelectionChange) {
      props.onContextMenuSelectionChange({
        originalEvent: event,
        value: props.node.key
      });
    }
    if (props.onContextMenu) {
      props.onContextMenu({
        originalEvent: event,
        node: props.node
      });
    }
  };
  var onKeyDown = function onKeyDown2(event) {
    if (!isSameNode(event)) {
      return;
    }
    switch (event.code) {
      case "Tab":
        onTabKey();
        break;
      case "ArrowDown":
        onArrowDown(event);
        break;
      case "ArrowUp":
        onArrowUp(event);
        break;
      case "ArrowRight":
        onArrowRight(event);
        break;
      case "ArrowLeft":
        onArrowLeft(event);
        break;
      case "Enter":
      case "NumpadEnter":
        onEnterKey(event);
        break;
      case "Space":
        if (!["INPUT"].includes(event.target.nodeName)) {
          onEnterKey(event);
        }
        break;
    }
  };
  var onArrowDown = function onArrowDown2(event) {
    var nodeElement = event.target.getAttribute("data-pc-section") === "toggler" ? event.target.closest('[role="treeitem"]') : event.target;
    var listElement = nodeElement.children[1];
    var nextElement = getNextElement(nodeElement);
    if (listElement) {
      focusRowChange(nodeElement, props.dragdropScope ? listElement.children[1] : listElement.children[0]);
    } else if (nextElement) {
      focusRowChange(nodeElement, nextElement);
    } else {
      var nextSiblingAncestor = _findNextSiblingOfAncestor(nodeElement);
      if (nextSiblingAncestor) {
        focusRowChange(nodeElement, nextSiblingAncestor);
      }
    }
    event.preventDefault();
  };
  var getPreviousElement = function getPreviousElement2(element) {
    var prev = element.previousElementSibling;
    if (prev) {
      return !props.dragdropScope ? prev : prev.previousElementSibling;
    }
    return null;
  };
  var getNextElement = function getNextElement2(element) {
    var next = element.nextElementSibling;
    if (next) {
      return !props.dragdropScope ? next : next.nextElementSibling;
    }
    return null;
  };
  var onArrowUp = function onArrowUp2(event) {
    var nodeElement = event.target;
    var previous = getPreviousElement(nodeElement);
    if (previous) {
      focusRowChange(nodeElement, previous, _findLastVisibleDescendant(previous));
    } else {
      var parentNodeElement = getParentNodeElement(nodeElement);
      if (parentNodeElement) {
        focusRowChange(nodeElement, parentNodeElement);
      }
    }
    event.preventDefault();
  };
  var onArrowRight = function onArrowRight2(event) {
    if (isLeaf || expanded) {
      return;
    }
    event.currentTarget.tabIndex = -1;
    expand(event, true);
  };
  var onArrowLeft = function onArrowLeft2(event) {
    var togglerElement = DomHandler.findSingle(event.currentTarget, '[data-pc-section="toggler"]');
    if (props.level === 0 && !expanded) {
      return false;
    }
    if (expanded && !isLeaf) {
      togglerElement.click();
      return false;
    }
    var target = _findBeforeClickableNode(event.currentTarget);
    if (target) {
      focusRowChange(event.currentTarget, target);
    }
  };
  var onEnterKey = function onEnterKey2(event) {
    setTabIndexForSelectionMode(event, nodeTouched.current);
    onClick(event);
    event.preventDefault();
  };
  var onTabKey = function onTabKey2() {
    setAllNodesTabIndexes();
  };
  var setAllNodesTabIndexes = function setAllNodesTabIndexes2() {
    var nodes = DomHandler.find(contentRef.current.closest('[data-pc-section="container"]'), '[role="treeitem"]');
    var hasSelectedNode = _toConsumableArray(nodes).some(function(node3) {
      return node3.getAttribute("aria-selected") === "true" || node3.getAttribute("aria-checked") === "true";
    });
    _toConsumableArray(nodes).forEach(function(node3) {
      node3.tabIndex = -1;
    });
    if (hasSelectedNode) {
      var selectedNodes = _toConsumableArray(nodes).filter(function(node3) {
        return node3.getAttribute("aria-selected") === "true" || node3.getAttribute("aria-checked") === "true";
      });
      selectedNodes[0].tabIndex = 0;
      return;
    }
    _toConsumableArray(nodes)[0].tabIndex = 0;
  };
  var setTabIndexForSelectionMode = function setTabIndexForSelectionMode2(event, nodeTouched2) {
    if (props.selectionMode !== null) {
      var elements = _toConsumableArray(DomHandler.find(elementRef.current.parentElement, '[role="treeitem"]'));
      event.currentTarget.tabIndex = nodeTouched2 === false ? -1 : 0;
      if (elements.every(function(element) {
        return element.tabIndex === -1;
      })) {
        elements[0].tabIndex = 0;
      }
    }
  };
  var focusRowChange = function focusRowChange2(firstFocusableRow, currentFocusedRow, lastVisibleDescendant) {
    firstFocusableRow.tabIndex = "-1";
    currentFocusedRow.tabIndex = "0";
    focusNode(lastVisibleDescendant || currentFocusedRow);
  };
  var _findBeforeClickableNode = function findBeforeClickableNode(node3) {
    var parentListElement = node3.closest("ul").closest("li");
    if (parentListElement) {
      var prevNodeButton = DomHandler.findSingle(parentListElement, "button");
      if (prevNodeButton && prevNodeButton.style.visibility !== "hidden") {
        return parentListElement;
      }
      return _findBeforeClickableNode(node3.previousElementSibling);
    }
    return null;
  };
  var propagateUp = function propagateUp2(event) {
    var check = event.check;
    var selectionKeys = event.selectionKeys;
    var checkedChildCount = 0;
    var _iterator = _createForOfIteratorHelper$1(props.node.children), _step;
    try {
      for (_iterator.s(); !(_step = _iterator.n()).done; ) {
        var child = _step.value;
        if (selectionKeys[child.key] && selectionKeys[child.key].checked) {
          checkedChildCount++;
        }
      }
    } catch (err) {
      _iterator.e(err);
    } finally {
      _iterator.f();
    }
    var parentKey = props.node.key;
    var children = ObjectUtils.findChildrenByKey(props.originalOptions, parentKey);
    var isParentPartiallyChecked = children.some(function(ele) {
      return ele.key in selectionKeys;
    });
    var isCompletelyChecked = children.every(function(ele) {
      return ele.key in selectionKeys && selectionKeys[ele.key].checked;
    });
    if (isParentPartiallyChecked && !isCompletelyChecked) {
      selectionKeys[parentKey] = {
        checked: false,
        partialChecked: true
      };
    } else if (isCompletelyChecked) {
      selectionKeys[parentKey] = {
        checked: true,
        partialChecked: false
      };
    } else if (check) {
      selectionKeys[parentKey] = {
        checked: false,
        partialChecked: false
      };
    } else {
      delete selectionKeys[parentKey];
    }
    if (props.propagateSelectionUp && props.onPropagateUp) {
      props.onPropagateUp(event);
    }
  };
  var _propagateDown = function propagateDown(node3, check, selectionKeys) {
    if (check) {
      selectionKeys[node3.key] = {
        checked: true,
        partialChecked: false
      };
    } else {
      delete selectionKeys[node3.key];
    }
    if (node3.children && node3.children.length) {
      for (var i = 0; i < node3.children.length; i++) {
        _propagateDown(node3.children[i], check, selectionKeys);
      }
    }
  };
  var isSelected = function isSelected2() {
    if (props.selectionMode && props.selectionKeys) {
      return isSingleSelectionMode() ? props.selectionKeys === props.node.key : props.selectionKeys[props.node.key] !== void 0;
    }
    return false;
  };
  var isChecked = function isChecked2() {
    return (props.selectionKeys ? props.selectionKeys[props.node.key] && props.selectionKeys[props.node.key].checked : false) || false;
  };
  var isSameNode = function isSameNode2(event) {
    return event.currentTarget && (event.currentTarget.isSameNode(event.target) || event.currentTarget.isSameNode(event.target.closest('[role="treeitem"]')));
  };
  var isPartialChecked = function isPartialChecked2() {
    return props.selectionKeys ? props.selectionKeys[props.node.key] && props.selectionKeys[props.node.key].partialChecked : false;
  };
  var isSingleSelectionMode = function isSingleSelectionMode2() {
    return props.selectionMode && props.selectionMode === "single";
  };
  var isMultipleSelectionMode = function isMultipleSelectionMode2() {
    return props.selectionMode && props.selectionMode === "multiple";
  };
  var isCheckboxSelectionMode = function isCheckboxSelectionMode2() {
    return props.selectionMode && props.selectionMode === "checkbox";
  };
  var onTouchEnd = function onTouchEnd2() {
    nodeTouched.current = true;
  };
  var onDropPoint = function onDropPoint2(event, position) {
    event.preventDefault();
    DomHandler.removeClass(event.target, "p-treenode-droppoint-active");
    if (props.onDropPoint) {
      var dropIndex = position === -1 ? props.index : props.index + 1;
      props.onDropPoint({
        originalEvent: event,
        path: props.path,
        index: dropIndex,
        position
      });
    }
  };
  var onDropPointDragOver = function onDropPointDragOver2(event) {
    if (props.dragdropScope && event.dataTransfer.types[1] === props.dragdropScope.toLocaleLowerCase()) {
      event.dataTransfer.dropEffect = "move";
      event.preventDefault();
    }
  };
  var onDropPointDragEnter = function onDropPointDragEnter2(event) {
    if (props.dragdropScope && event.dataTransfer.types[1] === props.dragdropScope.toLocaleLowerCase()) {
      DomHandler.addClass(event.target, "p-treenode-droppoint-active");
    }
  };
  var onDropPointDragLeave = function onDropPointDragLeave2(event) {
    if (props.dragdropScope && event.dataTransfer.types[1] === props.dragdropScope.toLocaleLowerCase()) {
      DomHandler.removeClass(event.target, "p-treenode-droppoint-active");
    }
  };
  var onDrop = function onDrop2(event) {
    if (props.dragdropScope && props.node.droppable !== false) {
      DomHandler.removeClass(contentRef.current, "p-treenode-dragover");
      event.preventDefault();
      event.stopPropagation();
      if (props.onDrop) {
        props.onDrop({
          originalEvent: event,
          path: props.path,
          index: props.index
        });
      }
    }
  };
  var onDragOver = function onDragOver2(event) {
    if (props.dragdropScope && event.dataTransfer.types[1] === props.dragdropScope.toLocaleLowerCase() && props.node.droppable !== false) {
      event.dataTransfer.dropEffect = "move";
      event.preventDefault();
      event.stopPropagation();
    }
  };
  var onDragEnter = function onDragEnter2(event) {
    if (props.dragdropScope && event.dataTransfer.types[1] === props.dragdropScope.toLocaleLowerCase() && props.node.droppable !== false) {
      DomHandler.addClass(contentRef.current, "p-treenode-dragover");
    }
  };
  var onDragLeave = function onDragLeave2(event) {
    if (props.dragdropScope && event.dataTransfer.types[1] === props.dragdropScope.toLocaleLowerCase() && props.node.droppable !== false) {
      var rect = event.currentTarget.getBoundingClientRect();
      if (event.nativeEvent.x > rect.left + rect.width || event.nativeEvent.x < rect.left || event.nativeEvent.y >= Math.floor(rect.top + rect.height) || event.nativeEvent.y < rect.top) {
        DomHandler.removeClass(contentRef.current, "p-treenode-dragover");
      }
    }
  };
  var onDragStart = function onDragStart2(event) {
    event.dataTransfer.setData("text", props.dragdropScope);
    event.dataTransfer.setData(props.dragdropScope, props.dragdropScope);
    if (props.onDragStart) {
      props.onDragStart({
        originalEvent: event,
        path: props.path,
        index: props.index
      });
    }
  };
  var onDragEnd = function onDragEnd2(event) {
    if (props.onDragEnd) {
      props.onDragEnd({
        originalEvent: event
      });
    }
  };
  var createLabel = function createLabel2() {
    var labelProps = mergeProps({
      className: cx("label")
    }, getPTOptions("label"));
    var content2 = /* @__PURE__ */ React.createElement("span", labelProps, label);
    if (props.nodeTemplate) {
      var defaultContentOptions = {
        onTogglerClick,
        className: "p-treenode-label",
        element: content2,
        props,
        expanded
      };
      content2 = ObjectUtils.getJSXElement(props.nodeTemplate, props.node, defaultContentOptions);
    }
    return content2;
  };
  var createCheckbox = function createCheckbox2() {
    if (isCheckboxSelectionMode() && props.node.selectable !== false) {
      var _props$isUnstyled;
      var checked = isChecked();
      var partialChecked = isPartialChecked();
      var checkboxIconProps = mergeProps({
        className: cx("checkIcon")
      });
      var icon = checked ? props.checkboxIcon || /* @__PURE__ */ React.createElement(CheckIcon, checkboxIconProps) : partialChecked ? props.checkboxIcon || /* @__PURE__ */ React.createElement(MinusIcon, checkboxIconProps) : null;
      var checkboxIcon = IconUtils.getJSXIcon(icon, _objectSpread$1({}, checkboxIconProps), props);
      var checkboxProps = mergeProps({
        className: cx("nodeCheckbox", {
          partialChecked
        }),
        checked: checked || partialChecked,
        icon: checkboxIcon,
        tabIndex: -1,
        unstyled: props === null || props === void 0 || (_props$isUnstyled = props.isUnstyled) === null || _props$isUnstyled === void 0 ? void 0 : _props$isUnstyled.call(props),
        "data-p-checked": checked,
        "data-p-partialchecked": partialChecked,
        onChange: onClick
      }, getPTOptions("nodeCheckbox"));
      return /* @__PURE__ */ React.createElement(Checkbox, checkboxProps);
    }
    return null;
  };
  var createIcon = function createIcon2() {
    var icon = props.node.icon || (expanded ? props.node.expandedIcon : props.node.collapsedIcon);
    if (icon) {
      var nodeIconProps = mergeProps({
        className: classNames(icon, cx("nodeIcon"))
      }, getPTOptions("nodeIcon"));
      return IconUtils.getJSXIcon(icon, _objectSpread$1({}, nodeIconProps), {
        props
      });
    }
    return null;
  };
  var createToggler = function createToggler2() {
    var togglerIconProps = mergeProps({
      className: cx("togglerIcon"),
      "aria-hidden": true
    }, getPTOptions("togglerIcon"));
    var icon = expanded ? props.collapseIcon || /* @__PURE__ */ React.createElement(ChevronDownIcon, togglerIconProps) : props.expandIcon || /* @__PURE__ */ React.createElement(ChevronRightIcon, togglerIconProps);
    var togglerIcon = IconUtils.getJSXIcon(icon, _objectSpread$1({}, togglerIconProps), {
      props,
      expanded
    });
    var togglerProps = mergeProps({
      type: "button",
      className: cx("toggler"),
      tabIndex: -1,
      "aria-hidden": false,
      onClick: onTogglerClick
    }, getPTOptions("toggler"));
    var content2 = /* @__PURE__ */ React.createElement("button", togglerProps, togglerIcon, /* @__PURE__ */ React.createElement(Ripple, null));
    if (props.togglerTemplate) {
      var defaultContentOptions = {
        onClick: onTogglerClick,
        containerClassName: "p-tree-toggler p-link",
        iconClassName: "p-tree-toggler-icon",
        element: content2,
        props,
        expanded
      };
      content2 = ObjectUtils.getJSXElement(props.togglerTemplate, props.node, defaultContentOptions);
    }
    return content2;
  };
  var createDropPoint = function createDropPoint2(position) {
    if (props.dragdropScope) {
      var droppointProps = mergeProps({
        className: cx("droppoint"),
        role: "treeitem",
        onDrop: function onDrop2(event) {
          return onDropPoint(event, position);
        },
        onDragOver: onDropPointDragOver,
        onDragEnter: onDropPointDragEnter,
        onDragLeave: onDropPointDragLeave
      }, getPTOptions("droppoint"));
      return /* @__PURE__ */ React.createElement("li", droppointProps);
    }
    return null;
  };
  var createContent = function createContent2() {
    var selected = isSelected();
    var checked = isChecked();
    var toggler = createToggler();
    var checkbox = createCheckbox();
    var icon = createIcon();
    var label2 = createLabel();
    var contentProps = mergeProps({
      ref: contentRef,
      className: classNames(props.node.className, cx("content", {
        checked,
        selected,
        nodeProps: props,
        isCheckboxSelectionMode
      })),
      style: props.node.style,
      onClick,
      onDoubleClick,
      onContextMenu: onRightClick,
      onTouchEnd,
      draggable: props.dragdropScope && props.node.draggable !== false && !props.disabled,
      onDrop,
      onDragOver,
      onDragEnter,
      onDragLeave,
      onDragStart,
      onDragEnd,
      "data-p-highlight": isCheckboxSelectionMode() ? checked : selected
    }, getPTOptions("content"));
    return /* @__PURE__ */ React.createElement("div", contentProps, toggler, checkbox, icon, label2);
  };
  var createChildren = function createChildren2() {
    var subgroupProps = mergeProps({
      className: cx("subgroup"),
      role: "group"
    }, getPTOptions("subgroup"));
    if (ObjectUtils.isNotEmpty(props.node.children) && expanded) {
      return /* @__PURE__ */ React.createElement("ul", subgroupProps, props.node.children.map(function(childNode, index) {
        return /* @__PURE__ */ React.createElement(UITreeNode, {
          key: childNode.key || childNode.label,
          node: childNode,
          checkboxIcon: props.checkboxIcon,
          collapseIcon: props.collapseIcon,
          contextMenuSelectionKey: props.contextMenuSelectionKey,
          cx,
          disabled: props.disabled,
          dragdropScope: props.dragdropScope,
          expandIcon: props.expandIcon,
          expandedKeys: props.expandedKeys,
          index,
          isNodeLeaf: props.isNodeLeaf,
          last: index === props.node.children.length - 1,
          metaKeySelection: props.metaKeySelection,
          nodeTemplate: props.nodeTemplate,
          onClick: props.onClick,
          onCollapse: props.onCollapse,
          onContextMenu: props.onContextMenu,
          onContextMenuSelectionChange: props.onContextMenuSelectionChange,
          onDoubleClick: props.onDoubleClick,
          onDragEnd: props.onDragEnd,
          onDragStart: props.onDragStart,
          onDrop: props.onDrop,
          onDropPoint: props.onDropPoint,
          onExpand: props.onExpand,
          onPropagateUp: propagateUp,
          onSelect: props.onSelect,
          onSelectionChange: props.onSelectionChange,
          onToggle: props.onToggle,
          onUnselect: props.onUnselect,
          originalOptions: props.originalOptions,
          parent: props.node,
          path: props.path + "-" + index,
          propagateSelectionDown: props.propagateSelectionDown,
          propagateSelectionUp: props.propagateSelectionUp,
          ptm,
          selectionKeys: props.selectionKeys,
          selectionMode: props.selectionMode,
          togglerTemplate: props.togglerTemplate
        });
      }));
    }
    return null;
  };
  var createNode = function createNode2() {
    var tabIndex = props.disabled || props.index !== 0 ? -1 : 0;
    var selected = isSelected();
    var checked = isChecked();
    var content2 = createContent();
    var children = createChildren();
    var nodeProps = mergeProps({
      ref: elementRef,
      className: classNames(props.node.className, cx("node", {
        isLeaf
      })),
      style: props.node.style,
      tabIndex,
      role: "treeitem",
      "aria-label": label,
      "aria-level": props.level,
      "aria-expanded": expanded,
      "aria-checked": checked,
      "aria-setsize": props.node.children ? props.node.children.length : 0,
      "aria-posinset": props.index + 1,
      onKeyDown,
      "aria-selected": checked || selected
    }, getPTOptions("node"));
    return /* @__PURE__ */ React.createElement("li", nodeProps, content2, children);
  };
  var node2 = createNode();
  if (props.dragdropScope && !props.disabled && (!props.parent || props.parent.droppable !== false)) {
    var beforeDropPoint = createDropPoint(-1);
    var afterDropPoint = props.last ? createDropPoint(1) : null;
    return /* @__PURE__ */ React.createElement(React.Fragment, null, beforeDropPoint, node2, afterDropPoint);
  }
  return node2;
});
UITreeNode.displayName = "UITreeNode";
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
function _createForOfIteratorHelper(r, e) {
  var t = "undefined" != typeof Symbol && r[Symbol.iterator] || r["@@iterator"];
  if (!t) {
    if (Array.isArray(r) || (t = _unsupportedIterableToArray(r)) || e && r && "number" == typeof r.length) {
      t && (r = t);
      var _n = 0, F = function F2() {
      };
      return { s: F, n: function n() {
        return _n >= r.length ? { done: true } : { done: false, value: r[_n++] };
      }, e: function e2(r2) {
        throw r2;
      }, f: F };
    }
    throw new TypeError("Invalid attempt to iterate non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
  }
  var o, a = true, u = false;
  return { s: function s() {
    t = t.call(r);
  }, n: function n() {
    var r2 = t.next();
    return a = r2.done, r2;
  }, e: function e2(r2) {
    u = true, o = r2;
  }, f: function f() {
    try {
      a || null == t["return"] || t["return"]();
    } finally {
      if (u) throw o;
    }
  } };
}
function _unsupportedIterableToArray(r, a) {
  if (r) {
    if ("string" == typeof r) return _arrayLikeToArray(r, a);
    var t = {}.toString.call(r).slice(8, -1);
    return "Object" === t && r.constructor && (t = r.constructor.name), "Map" === t || "Set" === t ? Array.from(r) : "Arguments" === t || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t) ? _arrayLikeToArray(r, a) : void 0;
  }
}
function _arrayLikeToArray(r, a) {
  (null == a || a > r.length) && (a = r.length);
  for (var e = 0, n = Array(a); e < a; e++) n[e] = r[e];
  return n;
}
var Tree = /* @__PURE__ */ React.memo(/* @__PURE__ */ React.forwardRef(function(inProps, ref) {
  var mergeProps = useMergeProps();
  var context = React.useContext(PrimeReactContext);
  var props = TreeBase.getProps(inProps, context);
  var _React$useState = React.useState(""), _React$useState2 = _slicedToArray(_React$useState, 2), filterValueState = _React$useState2[0], setFilterValueState = _React$useState2[1];
  var _React$useState3 = React.useState(props.expandedKeys), _React$useState4 = _slicedToArray(_React$useState3, 2), expandedKeysState = _React$useState4[0], setExpandedKeysState = _React$useState4[1];
  var elementRef = React.useRef(null);
  var filteredNodes = React.useRef([]);
  var dragState = React.useRef(null);
  var filterChanged = React.useRef(false);
  var filteredValue = props.onFilterValueChange ? props.filterValue : filterValueState;
  var expandedKeys = props.onToggle ? props.expandedKeys : expandedKeysState;
  var childFocusEvent = React.useRef(null);
  var _TreeBase$setMetaData = TreeBase.setMetaData({
    props,
    state: {
      filterValue: filteredValue,
      expandedKeys
    }
  }), ptm = _TreeBase$setMetaData.ptm, cx = _TreeBase$setMetaData.cx, isUnstyled = _TreeBase$setMetaData.isUnstyled;
  useHandleStyle(TreeBase.css.styles, isUnstyled, {
    name: "tree"
  });
  var filterOptions = {
    filter: function filter2(e) {
      return onFilterInputChange(e);
    },
    reset: function reset() {
      return resetFilter();
    }
  };
  var getRootNode = function getRootNode2() {
    return props.filter && filteredNodes.current ? filteredNodes.current : props.value;
  };
  var onToggle = function onToggle2(event) {
    var originalEvent = event.originalEvent, value = event.value, navigateFocusToChild = event.navigateFocusToChild;
    if (props.onToggle) {
      props.onToggle({
        originalEvent,
        value
      });
    } else {
      if (navigateFocusToChild) {
        childFocusEvent.current = originalEvent;
      }
      setExpandedKeysState(value);
    }
  };
  useUpdateEffect2(function() {
    if (childFocusEvent.current) {
      var event = childFocusEvent.current;
      var nodeElement = event.target.getAttribute("data-pc-section") === "toggler" ? event.target.closest('[role="treeitem"]') : event.target;
      var listElement = nodeElement.children[1];
      if (listElement) {
        if (nodeElement) {
          nodeElement.tabIndex = "-1";
        }
        var childElement = props.dragdropScope ? listElement.children[1] : listElement.children[0];
        if (childElement) {
          childElement.tabIndex = "0";
          childElement.focus();
        }
      }
      childFocusEvent.current = null;
    }
  }, [expandedKeys]);
  var onDragStart = function onDragStart2(event) {
    dragState.current = {
      path: event.path,
      index: event.index
    };
  };
  var onDragEnd = function onDragEnd2() {
    dragState.current = null;
  };
  var _cloneValue = function cloneValue(value) {
    if (Array.isArray(value)) {
      return value.map(_cloneValue);
    } else if (!!value && Object.getPrototypeOf(value) === Object.prototype) {
      var result = {};
      for (var key in value) {
        if (key !== "data") {
          result[key] = _cloneValue(value[key]);
        } else {
          result[key] = value[key];
        }
      }
      return result;
    }
    return value;
  };
  var onDrop = function onDrop2(event) {
    var _dragState$current;
    if (validateDropNode((_dragState$current = dragState.current) === null || _dragState$current === void 0 ? void 0 : _dragState$current.path, event.path)) {
      var value = _cloneValue(props.value);
      var dragPaths = dragState.current.path.split("-");
      dragPaths.pop();
      var dragNodeParent = _findNode(value, dragPaths);
      var dragNode = dragNodeParent ? dragNodeParent.children[dragState.current.index] : value[dragState.current.index];
      var dropNode = _findNode(value, event.path.split("-"));
      if (dropNode.children) {
        dropNode.children.push(dragNode);
      } else {
        dropNode.children = [dragNode];
      }
      if (dragNodeParent) {
        dragNodeParent.children.splice(dragState.current.index, 1);
      } else {
        value.splice(dragState.current.index, 1);
      }
      if (props.onDragDrop) {
        props.onDragDrop({
          originalEvent: event.originalEvent,
          value,
          dragNode,
          dropNode,
          dropIndex: event.index
        });
      }
    }
  };
  var onDropPoint = function onDropPoint2(event) {
    if (validateDropPoint(event)) {
      var value = _cloneValue(props.value);
      var dragPaths = dragState.current.path.split("-");
      dragPaths.pop();
      var dropPaths = event.path.split("-");
      dropPaths.pop();
      var dragNodeParent = _findNode(value, dragPaths);
      var dropNodeParent = _findNode(value, dropPaths);
      var dragNode = dragNodeParent ? dragNodeParent.children[dragState.current.index] : value[dragState.current.index];
      var siblings = areSiblings(dragState.current.path, event.path);
      if (dragNodeParent) {
        dragNodeParent.children.splice(dragState.current.index, 1);
      } else {
        value.splice(dragState.current.index, 1);
      }
      if (event.position < 0) {
        var dropIndex = siblings ? dragState.current.index > event.index ? event.index : event.index - 1 : event.index;
        if (dropNodeParent) {
          dropNodeParent.children.splice(dropIndex, 0, dragNode);
        } else {
          value.splice(dropIndex, 0, dragNode);
        }
      } else if (dropNodeParent) {
        dropNodeParent.children.push(dragNode);
      } else {
        value.push(dragNode);
      }
      if (props.onDragDrop) {
        props.onDragDrop({
          originalEvent: event.originalEvent,
          value,
          dragNode,
          dropNode: dropNodeParent,
          dropIndex: event.index
        });
      }
    }
  };
  var validateDrop = function validateDrop2(dragPath, dropPath) {
    if (!dragPath) {
      return false;
    }
    if (dragPath === dropPath) {
      return false;
    }
    if (dropPath.indexOf(dragPath) === 0) {
      return false;
    }
    return true;
  };
  var validateDropNode = function validateDropNode2(dragPath, dropPath) {
    var _validateDrop = validateDrop(dragPath, dropPath);
    if (_validateDrop) {
      if (dragPath.indexOf("-") > 0 && dragPath.substring(0, dragPath.lastIndexOf("-")) === dropPath) {
        return false;
      }
      return true;
    }
    return false;
  };
  var validateDropPoint = function validateDropPoint2(event) {
    var _dragState$current2;
    var _validateDrop = validateDrop((_dragState$current2 = dragState.current) === null || _dragState$current2 === void 0 ? void 0 : _dragState$current2.path, event.path);
    if (_validateDrop) {
      if (event.position === -1 && areSiblings(dragState.current.path, event.path) && dragState.current.index + 1 === event.index) {
        return false;
      }
      return true;
    }
    return false;
  };
  var areSiblings = function areSiblings2(path1, path2) {
    if (path1.length === 1 && path2.length === 1) {
      return true;
    }
    return path1.substring(0, path1.lastIndexOf("-")) === path2.substring(0, path2.lastIndexOf("-"));
  };
  var _findNode = function findNode(value, path) {
    if (path.length === 0) {
      return null;
    }
    var index = parseInt(path[0], 10);
    var nextSearchRoot = value.children ? value.children[index] : value[index];
    if (path.length === 1) {
      return nextSearchRoot;
    }
    path.shift();
    return _findNode(nextSearchRoot, path);
  };
  var isNodeLeaf = function isNodeLeaf2(node2) {
    return node2.leaf === false ? false : !(node2.children && node2.children.length);
  };
  var onFilterInputKeyDown = function onFilterInputKeyDown2(event) {
    if (event.which === 13) {
      event.preventDefault();
    }
  };
  var onFilterInputChange = function onFilterInputChange2(event) {
    filterChanged.current = true;
    var value = event.target.value;
    if (props.onFilterValueChange) {
      props.onFilterValueChange({
        originalEvent: event,
        value
      });
    } else {
      setFilterValueState(value);
    }
  };
  var filter = function filter2(value) {
    setFilterValueState(ObjectUtils.isNotEmpty(value) ? value : "");
    _filter();
  };
  var _filter = function _filter2() {
    if (!filterChanged.current) {
      return;
    }
    if (ObjectUtils.isEmpty(filteredValue)) {
      filteredNodes.current = props.value;
    } else {
      filteredNodes.current = [];
      var searchFields = props.filterBy.split(",");
      var filterText = filteredValue.toLocaleLowerCase(props.filterLocale);
      var isStrictMode = props.filterMode === "strict";
      var _iterator = _createForOfIteratorHelper(props.value), _step;
      try {
        for (_iterator.s(); !(_step = _iterator.n()).done; ) {
          var node2 = _step.value;
          var copyNode = _objectSpread({}, node2);
          var paramsWithoutNode = {
            searchFields,
            filterText,
            isStrictMode
          };
          if (isStrictMode && (findFilteredNodes(copyNode, paramsWithoutNode) || isFilterMatched(copyNode, paramsWithoutNode)) || !isStrictMode && (isFilterMatched(copyNode, paramsWithoutNode) || findFilteredNodes(copyNode, paramsWithoutNode))) {
            filteredNodes.current.push(copyNode);
          }
        }
      } catch (err) {
        _iterator.e(err);
      } finally {
        _iterator.f();
      }
    }
    filterChanged.current = false;
  };
  var findFilteredNodes = function findFilteredNodes2(node2, paramsWithoutNode) {
    if (node2) {
      var matched = false;
      if (node2.children) {
        var childNodes = _toConsumableArray(node2.children);
        node2.children = [];
        var _iterator2 = _createForOfIteratorHelper(childNodes), _step2;
        try {
          for (_iterator2.s(); !(_step2 = _iterator2.n()).done; ) {
            var childNode = _step2.value;
            var copyChildNode = _objectSpread({}, childNode);
            if (isFilterMatched(copyChildNode, paramsWithoutNode)) {
              matched = true;
              node2.children.push(copyChildNode);
            }
          }
        } catch (err) {
          _iterator2.e(err);
        } finally {
          _iterator2.f();
        }
      }
      if (matched) {
        node2.expanded = true;
        return true;
      }
    }
  };
  var isFilterMatched = function isFilterMatched2(node2, _ref) {
    var searchFields = _ref.searchFields, filterText = _ref.filterText, isStrictMode = _ref.isStrictMode;
    var matched = false;
    var _iterator3 = _createForOfIteratorHelper(searchFields), _step3;
    try {
      for (_iterator3.s(); !(_step3 = _iterator3.n()).done; ) {
        var field = _step3.value;
        var fieldValue = String(ObjectUtils.resolveFieldData(node2, field)).toLocaleLowerCase(props.filterLocale);
        if (fieldValue.indexOf(filterText) > -1) {
          matched = true;
        }
      }
    } catch (err) {
      _iterator3.e(err);
    } finally {
      _iterator3.f();
    }
    if (!matched || isStrictMode && !isNodeLeaf(node2)) {
      matched = findFilteredNodes(node2, {
        searchFields,
        filterText,
        isStrictMode
      }) || matched;
    }
    return matched;
  };
  var resetFilter = function resetFilter2() {
    setFilterValueState("");
  };
  React.useImperativeHandle(ref, function() {
    return {
      props,
      filter,
      getElement: function getElement() {
        return elementRef.current;
      }
    };
  });
  var createRootChild = function createRootChild2(node2, index, last) {
    return /* @__PURE__ */ React.createElement(UITreeNode, {
      hostName: "Tree",
      key: node2.key || node2.label,
      node: node2,
      level: props.level + 1,
      originalOptions: props.value,
      index,
      last,
      path: String(index),
      checkboxIcon: props.checkboxIcon,
      collapseIcon: props.collapseIcon,
      contextMenuSelectionKey: props.contextMenuSelectionKey,
      cx,
      disabled: props.disabled,
      dragdropScope: props.dragdropScope,
      expandIcon: props.expandIcon,
      expandedKeys,
      isNodeLeaf,
      metaKeySelection: props.metaKeySelection,
      nodeTemplate: props.nodeTemplate,
      onClick: props.onNodeClick,
      onCollapse: props.onCollapse,
      onContextMenu: props.onContextMenu,
      onContextMenuSelectionChange: props.onContextMenuSelectionChange,
      onDoubleClick: props.onNodeDoubleClick,
      onDragEnd,
      onDragStart,
      onDrop,
      onDropPoint,
      onExpand: props.onExpand,
      onSelect: props.onSelect,
      onSelectionChange: props.onSelectionChange,
      onToggle,
      onUnselect: props.onUnselect,
      propagateSelectionDown: props.propagateSelectionDown,
      propagateSelectionUp: props.propagateSelectionUp,
      ptm,
      selectionKeys: props.selectionKeys,
      selectionMode: props.selectionMode,
      togglerTemplate: props.togglerTemplate,
      isUnstyled
    });
  };
  var createEmptyMessageNode = function createEmptyMessageNode2() {
    var emptyMessageProps = mergeProps({
      className: classNames(props.contentClassName, cx("emptyMessage")),
      role: "treeitem"
    }, ptm("emptyMessage"));
    var message = ObjectUtils.getJSXElement(props.emptyMessage, props) || localeOption("emptyMessage");
    return /* @__PURE__ */ React.createElement("li", emptyMessageProps, /* @__PURE__ */ React.createElement("span", {
      className: "p-treenode-content"
    }, message));
  };
  var createRootChildrenContainer = function createRootChildrenContainer2(children) {
    var containerProps = mergeProps(_objectSpread({
      className: classNames(props.contentClassName, cx("container")),
      role: "tree",
      "aria-label": props.ariaLabel,
      "aria-labelledby": props.ariaLabelledBy,
      style: props.contentStyle
    }, ariaProps), ptm("container"));
    return /* @__PURE__ */ React.createElement("ul", containerProps, children);
  };
  var createRootChildren = function createRootChildren2(value) {
    return value.map(function(node2, index) {
      return createRootChild(node2, index, index === value.length - 1);
    });
  };
  var createModel = function createModel2() {
    if (props.value) {
      if (props.filter) {
        filterChanged.current = true;
        _filter();
      }
      var value = getRootNode();
      if (value.length > 0) {
        var rootNodes = createRootChildren(value);
        return createRootChildrenContainer(rootNodes);
      }
      var emptyMessageNode = createEmptyMessageNode();
      return createRootChildrenContainer(emptyMessageNode);
    }
    return null;
  };
  var createLoader = function createLoader2() {
    if (props.loading) {
      var loadingIconProps = mergeProps({
        className: cx("loadingIcon")
      }, ptm("loadingIcon"));
      var icon = props.loadingIcon || /* @__PURE__ */ React.createElement(SpinnerIcon, _extends({}, loadingIconProps, {
        spin: true
      }));
      var loadingIcon = IconUtils.getJSXIcon(icon, _objectSpread({}, loadingIconProps), {
        props
      });
      var loadingOverlayProps = mergeProps({
        className: cx("loadingOverlay")
      }, ptm("loadingOverlay"));
      return /* @__PURE__ */ React.createElement("div", loadingOverlayProps, loadingIcon);
    }
    return null;
  };
  var createFilter = function createFilter2() {
    if (props.filter) {
      var value = ObjectUtils.isNotEmpty(filteredValue) ? filteredValue : "";
      var searchIconProps = mergeProps({
        className: cx("searchIcon")
      }, ptm("searchIcon"));
      var icon = props.filterIcon || /* @__PURE__ */ React.createElement(SearchIcon, searchIconProps);
      var filterIcon = IconUtils.getJSXIcon(icon, _objectSpread({}, searchIconProps), {
        props
      });
      var filterContainerProps = mergeProps({
        className: cx("filterContainer")
      }, ptm("filterContainer"));
      var inputProps = mergeProps({
        type: "text",
        value,
        autoComplete: "off",
        className: cx("input"),
        placeholder: props.filterPlaceholder,
        "aria-label": props.filterPlaceholder,
        onKeyDown: onFilterInputKeyDown,
        onChange: onFilterInputChange,
        disabled: props.disabled
      }, ptm("input"));
      var _content = /* @__PURE__ */ React.createElement("div", filterContainerProps, /* @__PURE__ */ React.createElement("input", inputProps), filterIcon);
      if (props.filterTemplate) {
        var defaultContentOptions = {
          className: "p-tree-filter-container",
          element: _content,
          filterOptions,
          filterInputKeyDown: onFilterInputKeyDown,
          filterInputChange: onFilterInputChange,
          filterIconClassName: "p-dropdown-filter-icon",
          props
        };
        _content = ObjectUtils.getJSXElement(props.filterTemplate, defaultContentOptions);
      }
      return /* @__PURE__ */ React.createElement(React.Fragment, null, _content);
    }
    return null;
  };
  var createHeader = function createHeader2() {
    if (props.showHeader) {
      var filterElement = createFilter();
      var _content2 = filterElement;
      if (props.header) {
        var defaultContentOptions = {
          filterContainerClassName: "p-tree-filter-container",
          filterIconClassName: "p-tree-filter-icon",
          filterInput: {
            className: "p-tree-filter p-inputtext p-component",
            onKeyDown: onFilterInputKeyDown,
            onChange: onFilterInputChange
          },
          filterElement,
          element: _content2,
          props
        };
        _content2 = ObjectUtils.getJSXElement(props.header, defaultContentOptions);
      }
      var headerProps = mergeProps({
        className: cx("header")
      }, ptm("header"));
      return /* @__PURE__ */ React.createElement("div", headerProps, _content2);
    }
    return null;
  };
  var createFooter = function createFooter2() {
    var content3 = ObjectUtils.getJSXElement(props.footer, props);
    var footerProps = mergeProps({
      className: cx("footer")
    }, ptm("footer"));
    return /* @__PURE__ */ React.createElement("div", footerProps, content3);
  };
  var otherProps = TreeBase.getOtherProps(props);
  var ariaProps = ObjectUtils.reduceKeys(otherProps, DomHandler.ARIA_PROPS);
  var loader = createLoader();
  var content2 = createModel();
  var header = createHeader();
  var footer = createFooter();
  var rootProps = mergeProps({
    ref: elementRef,
    className: classNames(props.className, cx("root")),
    style: props.style,
    id: props.id
  }, TreeBase.getOtherProps(props), ptm("root"));
  return /* @__PURE__ */ React.createElement("div", rootProps, loader, header, content2, footer);
}));
Tree.displayName = "Tree";
export {
  Tree as default
};
