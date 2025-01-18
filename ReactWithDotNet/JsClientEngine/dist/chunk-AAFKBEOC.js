import {
  inputBaseClasses_default
} from "./chunk-UP6RQLGD.js";
import {
  generateUtilityClass,
  generateUtilityClasses
} from "./chunk-L3PWPRGS.js";

// node_modules/@mui/material/Input/inputClasses.js
function getInputUtilityClass(slot) {
  return generateUtilityClass("MuiInput", slot);
}
var inputClasses = {
  ...inputBaseClasses_default,
  ...generateUtilityClasses("MuiInput", ["root", "underline", "input"])
};
var inputClasses_default = inputClasses;

export {
  getInputUtilityClass,
  inputClasses_default
};
