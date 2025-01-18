import {
  NoDomUploady_default,
  hasWindow_default,
  import_invariant,
  useUploadOptions_default
} from "./chunk-KZP6WMIN.js";
import {
  require_react_dom
} from "./chunk-QNQS7X5M.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/@rpldy/uploady/lib/esm/Uploady.js
var import_react = __toESM(require_react());
var import_react_dom = __toESM(require_react_dom());
function _extends() {
  return _extends = Object.assign ? Object.assign.bind() : function(n) {
    for (var e = 1; e < arguments.length; e++) {
      var t = arguments[e];
      for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]);
    }
    return n;
  }, _extends.apply(null, arguments);
}
var NO_CONTAINER_ERROR_MSG = "Uploady - Container for file input must be a valid dom element";
var renderInput = (inputProps, instanceOptions, ref) => /* @__PURE__ */ import_react.default.createElement("input", _extends({}, inputProps, {
  name: instanceOptions.inputFieldName,
  type: "file",
  ref
}));
var renderInPortal = (container, isValidContainer, inputProps, instanceOptions, ref) => container && isValidContainer ? /* @__PURE__ */ import_react_dom.default.createPortal(renderInput(inputProps, instanceOptions, ref), container) : null;
var FileInputField = /* @__PURE__ */ (0, import_react.memo)(/* @__PURE__ */ (0, import_react.forwardRef)(({
  container,
  noPortal,
  ...inputProps
}, ref) => {
  const instanceOptions = useUploadOptions_default();
  const isValidContainer = container && container.nodeType === 1;
  (0, import_invariant.default)(isValidContainer || !hasWindow_default(), NO_CONTAINER_ERROR_MSG);
  return noPortal ? renderInput(inputProps, instanceOptions, ref) : renderInPortal(container, isValidContainer, inputProps, instanceOptions, ref);
}));
var Uploady = (props) => {
  const {
    multiple = true,
    capture,
    accept,
    webkitdirectory,
    children,
    inputFieldContainer,
    customInput,
    fileInputId,
    noPortal = false,
    ...noDomProps
  } = props;
  const container = !customInput ? inputFieldContainer || (hasWindow_default() ? document.body : null) : null;
  const internalInputFieldRef = (0, import_react.useRef)();
  return /* @__PURE__ */ import_react.default.createElement(NoDomUploady_default, _extends({}, noDomProps, {
    inputRef: internalInputFieldRef
  }), !customInput ? /* @__PURE__ */ import_react.default.createElement(FileInputField, {
    container,
    multiple,
    capture,
    accept,
    webkitdirectory: webkitdirectory?.toString(),
    style: {
      display: "none"
    },
    ref: internalInputFieldRef,
    id: fileInputId,
    noPortal
  }) : null, children);
};
var Uploady_default = Uploady;

// node_modules/@rpldy/uploady/lib/esm/useFileInput.js
var import_react2 = __toESM(require_react());

// node_modules/@rpldy/uploady/lib/esm/index.js
var esm_default = Uploady_default;

export {
  esm_default
};
