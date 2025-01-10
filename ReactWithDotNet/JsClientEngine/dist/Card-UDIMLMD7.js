import {
  Paper_default
} from "./chunk-T6FPBEPB.js";
import "./chunk-VLKPQKCD.js";
import {
  chainPropTypes
} from "./chunk-PJOETPFJ.js";
import "./chunk-VZKQEGQI.js";
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

// node_modules/@mui/material/Card/Card.js
var React = __toESM(require_react());
var import_prop_types = __toESM(require_prop_types());

// node_modules/@mui/material/Card/cardClasses.js
function getCardUtilityClass(slot) {
  return generateUtilityClass("MuiCard", slot);
}
var cardClasses = generateUtilityClasses("MuiCard", ["root"]);

// node_modules/@mui/material/Card/Card.js
var import_jsx_runtime = __toESM(require_jsx_runtime());
var useUtilityClasses = (ownerState) => {
  const {
    classes
  } = ownerState;
  const slots = {
    root: ["root"]
  };
  return composeClasses(slots, getCardUtilityClass, classes);
};
var CardRoot = styled_default(Paper_default, {
  name: "MuiCard",
  slot: "Root",
  overridesResolver: (props, styles) => styles.root
})({
  overflow: "hidden"
});
var Card = /* @__PURE__ */ React.forwardRef(function Card2(inProps, ref) {
  const props = useDefaultProps({
    props: inProps,
    name: "MuiCard"
  });
  const {
    className,
    raised = false,
    ...other
  } = props;
  const ownerState = {
    ...props,
    raised
  };
  const classes = useUtilityClasses(ownerState);
  return /* @__PURE__ */ (0, import_jsx_runtime.jsx)(CardRoot, {
    className: clsx_default(classes.root, className),
    elevation: raised ? 8 : void 0,
    ref,
    ownerState,
    ...other
  });
});
true ? Card.propTypes = {
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
   * If `true`, the card will use raised styling.
   * @default false
   */
  raised: chainPropTypes(import_prop_types.default.bool, (props) => {
    if (props.raised && props.variant === "outlined") {
      return new Error('MUI: Combining `raised={true}` with `variant="outlined"` has no effect.');
    }
    return null;
  }),
  /**
   * The system prop that allows defining system overrides as well as additional CSS styles.
   */
  sx: import_prop_types.default.oneOfType([import_prop_types.default.arrayOf(import_prop_types.default.oneOfType([import_prop_types.default.func, import_prop_types.default.object, import_prop_types.default.bool])), import_prop_types.default.func, import_prop_types.default.object])
} : void 0;
var Card_default = Card;

// react-with-dotnet/libraries/mui-core/Card.jsx
var Card_default2 = Card_default;
export {
  Card_default2 as default
};
