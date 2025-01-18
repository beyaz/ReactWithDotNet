import {
  require_prop_types
} from "./chunk-FLAFLVMN.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/@mui/utils/esm/refType/refType.js
var import_prop_types = __toESM(require_prop_types());
var refType = import_prop_types.default.oneOfType([import_prop_types.default.func, import_prop_types.default.object]);
var refType_default = refType;

// node_modules/@mui/utils/esm/setRef/setRef.js
function setRef(ref, value) {
  if (typeof ref === "function") {
    ref(value);
  } else if (ref) {
    ref.current = value;
  }
}

// node_modules/@mui/utils/esm/useForkRef/useForkRef.js
var React = __toESM(require_react());
function useForkRef(...refs) {
  return React.useMemo(() => {
    if (refs.every((ref) => ref == null)) {
      return null;
    }
    return (instance) => {
      refs.forEach((ref) => {
        setRef(ref, instance);
      });
    };
  }, refs);
}

export {
  refType_default,
  setRef,
  useForkRef
};
