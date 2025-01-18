import {
  __commonJS
} from "./chunk-QRPWKJ4C.js";

// node_modules/memoize-one/dist/memoize-one.cjs.js
var require_memoize_one_cjs = __commonJS({
  "node_modules/memoize-one/dist/memoize-one.cjs.js"(exports, module) {
    "use strict";
    var safeIsNaN = Number.isNaN || function ponyfill(value) {
      return typeof value === "number" && value !== value;
    };
    function isEqual(first, second) {
      if (first === second) {
        return true;
      }
      if (safeIsNaN(first) && safeIsNaN(second)) {
        return true;
      }
      return false;
    }
    function areInputsEqual(newInputs, lastInputs) {
      if (newInputs.length !== lastInputs.length) {
        return false;
      }
      for (var i = 0; i < newInputs.length; i++) {
        if (!isEqual(newInputs[i], lastInputs[i])) {
          return false;
        }
      }
      return true;
    }
    function memoizeOne(resultFn, isEqual2) {
      if (isEqual2 === void 0) {
        isEqual2 = areInputsEqual;
      }
      var lastThis;
      var lastArgs = [];
      var lastResult;
      var calledOnce = false;
      function memoized() {
        var newArgs = [];
        for (var _i = 0; _i < arguments.length; _i++) {
          newArgs[_i] = arguments[_i];
        }
        if (calledOnce && lastThis === this && isEqual2(newArgs, lastArgs)) {
          return lastResult;
        }
        lastResult = resultFn.apply(this, newArgs);
        calledOnce = true;
        lastThis = this;
        lastArgs = newArgs;
        return lastResult;
      }
      return memoized;
    }
    module.exports = memoizeOne;
  }
});

export {
  require_memoize_one_cjs
};
