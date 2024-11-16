import{a as I}from"./chunk-RVEJIPMH.js";import"./chunk-G7S3TOUE.js";import{B as j,C as E,a as P,b as m,d as b,m as R,u as w,v as S}from"./chunk-NI4MO4TP.js";import"./chunk-DLL45UCJ.js";import{a as U}from"./chunk-2W22VGCX.js";import{h as K}from"./chunk-GYULANB4.js";var o=K(U());function p(){return p=Object.assign?Object.assign.bind():function(n){for(var t=1;t<arguments.length;t++){var r=arguments[t];for(var a in r)Object.prototype.hasOwnProperty.call(r,a)&&(n[a]=r[a])}return n},p.apply(this,arguments)}function f(n){"@babel/helpers - typeof";return f=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(t){return typeof t}:function(t){return t&&typeof Symbol=="function"&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},f(n)}function $(n,t){if(f(n)!=="object"||n===null)return n;var r=n[Symbol.toPrimitive];if(r!==void 0){var a=r.call(n,t||"default");if(f(a)!=="object")return a;throw new TypeError("@@toPrimitive must return a primitive value.")}return(t==="string"?String:Number)(n)}function A(n){var t=$(n,"string");return f(t)==="symbol"?t:String(t)}function Y(n,t,r){return t=A(t),t in n?Object.defineProperty(n,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):n[t]=r,n}var q={root:function(t){var r=t.props,a=t.checked;return P("p-inputswitch p-component",{"p-highlight":a,"p-disabled":r.disabled,"p-invalid":r.invalid})},input:"p-inputswitch-input",slider:"p-inputswitch-slider"},v=j.extend({defaultProps:{__TYPE:"InputSwitch",autoFocus:!1,checked:!1,className:null,disabled:!1,falseValue:!1,id:null,inputId:null,inputRef:null,invalid:!1,name:null,onBlur:null,onChange:null,onFocus:null,style:null,tabIndex:null,tooltip:null,tooltipOptions:null,trueValue:!0,children:void 0},css:{classes:q}});function x(n,t){var r=Object.keys(n);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(n);t&&(a=a.filter(function(e){return Object.getOwnPropertyDescriptor(n,e).enumerable})),r.push.apply(r,a)}return r}function z(n){for(var t=1;t<arguments.length;t++){var r=arguments[t]!=null?arguments[t]:{};t%2?x(Object(r),!0).forEach(function(a){Y(n,a,r[a])}):Object.getOwnPropertyDescriptors?Object.defineProperties(n,Object.getOwnPropertyDescriptors(r)):x(Object(r)).forEach(function(a){Object.defineProperty(n,a,Object.getOwnPropertyDescriptor(r,a))})}return n}var _=o.memo(o.forwardRef(function(n,t){var r=w(),a=o.useContext(R),e=v.getProps(n,a),h=v.setMetaData({props:e}),d=h.ptm,g=h.cx,F=h.isUnstyled;E(v.css.styles,F,{name:"inputswitch"});var y=o.useRef(null),l=o.useRef(e.inputRef),c=e.checked===e.trueValue,B=function(i){if(e.onChange){var u=c?e.falseValue:e.trueValue;e.onChange({originalEvent:i,value:u,stopPropagation:function(){i==null||i.stopPropagation()},preventDefault:function(){i==null||i.preventDefault()},target:{name:e.name,id:e.id,value:u}})}},C=function(i){var u;e==null||(u=e.onFocus)===null||u===void 0||u.call(e,i)},D=function(i){var u;e==null||(u=e.onBlur)===null||u===void 0||u.call(e,i)};o.useImperativeHandle(t,function(){return{props:e,focus:function(){return m.focus(l.current)},getElement:function(){return y.current},getInput:function(){return l.current}}}),o.useEffect(function(){b.combinedRefs(l,e.inputRef)},[l,e.inputRef]),S(function(){e.autoFocus&&m.focus(l.current,e.autoFocus)});var N=b.isNotEmpty(e.tooltip),O=v.getOtherProps(e),k=b.reduceKeys(O,m.ARIA_PROPS),V=r({className:P(e.className,g("root",{checked:c})),style:e.style,role:"checkbox","aria-checked":c,"data-p-highlight":c,"data-p-disabled":e.disabled},O,d("root")),M=r(z({type:"checkbox",id:e.inputId,name:e.name,checked:c,onChange:B,onFocus:C,onBlur:D,disabled:e.disabled,role:"switch",tabIndex:e.tabIndex,"aria-checked":c,className:g("input")},k),d("input")),T=r({className:g("slider")},d("slider"));return o.createElement(o.Fragment,null,o.createElement("div",p({id:e.id,ref:y},V),o.createElement("input",p({ref:l},M)),o.createElement("span",T)),N&&o.createElement(I,p({target:y,content:e.tooltip,pt:d("tooltip")},e.tooltipOptions)))}));_.displayName="InputSwitch";export{_ as default};