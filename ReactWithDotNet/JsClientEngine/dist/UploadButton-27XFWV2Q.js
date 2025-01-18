import {
  markAsUploadOptionsComponent,
  useUploadyContext_default
} from "./chunk-KZP6WMIN.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/@rpldy/upload-button/lib/esm/UploadButton.js
var import_react2 = __toESM(require_react());

// node_modules/@rpldy/upload-button/lib/esm/asUploadButton.js
var import_react = __toESM(require_react());
function _extends() {
  return _extends = Object.assign ? Object.assign.bind() : function(n) {
    for (var e = 1; e < arguments.length; e++) {
      var t = arguments[e];
      for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]);
    }
    return n;
  }, _extends.apply(null, arguments);
}
var asUploadButton = (Component) => {
  const AsUploadButton = (props, ref) => {
    const {
      showFileUpload
    } = useUploadyContext_default();
    const {
      id,
      className,
      text,
      children,
      extraProps,
      onClick,
      ...uploadOptions
    } = props;
    const uploadOptionsRef = (0, import_react.useRef)();
    uploadOptionsRef.current = uploadOptions;
    const onButtonClick = (0, import_react.useCallback)((e) => {
      showFileUpload(uploadOptionsRef.current);
      onClick?.(e);
    }, [showFileUpload, uploadOptionsRef, onClick]);
    return /* @__PURE__ */ import_react.default.createElement(Component, _extends({
      ref,
      onClick: onButtonClick,
      id,
      className
    }, extraProps), children || text || "Upload");
  };
  markAsUploadOptionsComponent(AsUploadButton);
  return /* @__PURE__ */ (0, import_react.forwardRef)(AsUploadButton);
};
var asUploadButton_default = asUploadButton;

// node_modules/@rpldy/upload-button/lib/esm/UploadButton.js
function _extends2() {
  return _extends2 = Object.assign ? Object.assign.bind() : function(n) {
    for (var e = 1; e < arguments.length; e++) {
      var t = arguments[e];
      for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]);
    }
    return n;
  }, _extends2.apply(null, arguments);
}
var UploadButton = asUploadButton_default(/* @__PURE__ */ (0, import_react2.forwardRef)((props, ref) => /* @__PURE__ */ import_react2.default.createElement("button", _extends2({
  ref
}, props))));
var UploadButton_default = UploadButton;

// node_modules/@rpldy/upload-button/lib/esm/index.js
var esm_default = UploadButton_default;

// react-with-dotnet/libraries/_uploady_/UploadButton.jsx
var UploadButton_default2 = esm_default;
export {
  UploadButton_default2 as default
};
