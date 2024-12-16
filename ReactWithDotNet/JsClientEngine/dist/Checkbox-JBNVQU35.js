import{a as N}from"./chunk-FM7MEQI2.js";import{a as M}from"./chunk-Q4HHYYXZ.js";import"./chunk-35DEAE55.js";import"./chunk-3Q5HW2ZY.js";import{A as D,B,C as F,a as E,b as h,d as O,e as I,m as w,u as _,v as A}from"./chunk-MY5M2XOA.js";import"./chunk-DLL45UCJ.js";import{a as Z}from"./chunk-2W22VGCX.js";import{h as Q}from"./chunk-GYULANB4.js";var a=Q(Z());function g(){return g=Object.assign?Object.assign.bind():function(t){for(var n=1;n<arguments.length;n++){var r=arguments[n];for(var o in r)({}).hasOwnProperty.call(r,o)&&(t[o]=r[o])}return t},g.apply(null,arguments)}function x(t){"@babel/helpers - typeof";return x=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(n){return typeof n}:function(n){return n&&typeof Symbol=="function"&&n.constructor===Symbol&&n!==Symbol.prototype?"symbol":typeof n},x(t)}function ee(t,n){if(x(t)!="object"||!t)return t;var r=t[Symbol.toPrimitive];if(r!==void 0){var o=r.call(t,n||"default");if(x(o)!="object")return o;throw new TypeError("@@toPrimitive must return a primitive value.")}return(n==="string"?String:Number)(t)}function te(t){var n=ee(t,"string");return x(n)=="symbol"?n:n+""}function ne(t,n,r){return(n=te(n))in t?Object.defineProperty(t,n,{value:r,enumerable:!0,configurable:!0,writable:!0}):t[n]=r,t}function re(t){if(Array.isArray(t))return t}function oe(t,n){var r=t==null?null:typeof Symbol!="undefined"&&t[Symbol.iterator]||t["@@iterator"];if(r!=null){var o,e,k,m,y=[],s=!0,b=!1;try{if(k=(r=r.call(t)).next,n===0){if(Object(r)!==r)return;s=!1}else for(;!(s=(o=k.call(r)).done)&&(y.push(o.value),y.length!==n);s=!0);}catch(p){b=!0,e=p}finally{try{if(!s&&r.return!=null&&(m=r.return(),Object(m)!==m))return}finally{if(b)throw e}}return y}}function T(t,n){(n==null||n>t.length)&&(n=t.length);for(var r=0,o=Array(n);r<n;r++)o[r]=t[r];return o}function ae(t,n){if(t){if(typeof t=="string")return T(t,n);var r={}.toString.call(t).slice(8,-1);return r==="Object"&&t.constructor&&(r=t.constructor.name),r==="Map"||r==="Set"?Array.from(t):r==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(r)?T(t,n):void 0}}function le(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function ce(t,n){return re(t)||oe(t,n)||ae(t,n)||le()}var ue={box:"p-checkbox-box",input:"p-checkbox-input",icon:"p-checkbox-icon",root:function(n){var r=n.props,o=n.checked,e=n.context;return E("p-checkbox p-component",{"p-highlight":o,"p-disabled":r.disabled,"p-invalid":r.invalid,"p-variant-filled":r.variant?r.variant==="filled":e&&e.inputStyle==="filled"})}},R=B.extend({defaultProps:{__TYPE:"Checkbox",autoFocus:!1,checked:!1,className:null,disabled:!1,falseValue:!1,icon:null,id:null,inputId:null,inputRef:null,invalid:!1,variant:null,name:null,onChange:null,onContextMenu:null,onMouseDown:null,readOnly:!1,required:!1,style:null,tabIndex:null,tooltip:null,tooltipOptions:null,trueValue:!0,value:null,children:void 0},css:{classes:ue}});function V(t,n){var r=Object.keys(t);if(Object.getOwnPropertySymbols){var o=Object.getOwnPropertySymbols(t);n&&(o=o.filter(function(e){return Object.getOwnPropertyDescriptor(t,e).enumerable})),r.push.apply(r,o)}return r}function U(t){for(var n=1;n<arguments.length;n++){var r=arguments[n]!=null?arguments[n]:{};n%2?V(Object(r),!0).forEach(function(o){ne(t,o,r[o])}):Object.getOwnPropertyDescriptors?Object.defineProperties(t,Object.getOwnPropertyDescriptors(r)):V(Object(r)).forEach(function(o){Object.defineProperty(t,o,Object.getOwnPropertyDescriptor(r,o))})}return t}var H=a.memo(a.forwardRef(function(t,n){var r=_(),o=a.useContext(w),e=R.getProps(t,o),k=a.useState(!1),m=ce(k,2),y=m[0],s=m[1],b=R.setMetaData({props:e,state:{focused:y},context:{checked:e.checked===e.trueValue,disabled:e.disabled}}),p=b.ptm,P=b.cx,$=b.isUnstyled;F(R.css.styles,$,{name:"checkbox"});var S=a.useRef(null),c=a.useRef(e.inputRef),j=function(){return e.checked===e.trueValue},q=function(l){if(!(e.disabled||e.readonly)&&e.onChange){var d,v=j(),i=v?e.falseValue:e.trueValue,z={originalEvent:l,value:e.value,checked:i,stopPropagation:function(){l==null||l.stopPropagation()},preventDefault:function(){l==null||l.preventDefault()},target:{type:"checkbox",name:e.name,id:e.id,value:e.value,checked:i}};if(e==null||(d=e.onChange)===null||d===void 0||d.call(e,z),l.defaultPrevented)return;h.focus(c.current)}},K=function(){var l;s(!0),e==null||(l=e.onFocus)===null||l===void 0||l.call(e)},L=function(){var l;s(!1),e==null||(l=e.onBlur)===null||l===void 0||l.call(e)};a.useImperativeHandle(n,function(){return{props:e,focus:function(){return h.focus(c.current)},getElement:function(){return S.current},getInput:function(){return c.current}}}),a.useEffect(function(){O.combinedRefs(c,e.inputRef)},[c,e.inputRef]),D(function(){c.current.checked=j()},[e.checked,e.trueValue]),A(function(){e.autoFocus&&h.focus(c.current,e.autoFocus)});var f=j(),J=O.isNotEmpty(e.tooltip),C=R.getOtherProps(e),W=r({id:e.id,className:E(e.className,P("root",{checked:f,context:o})),style:e.style,"data-p-highlight":f,"data-p-disabled":e.disabled,onContextMenu:e.onContextMenu,onMouseDown:e.onMouseDown},C,p("root")),X=function(){var l=O.reduceKeys(C,h.ARIA_PROPS),d=r(U({id:e.inputId,type:"checkbox",className:P("input"),name:e.name,tabIndex:e.tabIndex,onFocus:function(i){return K()},onBlur:function(i){return L()},onChange:function(i){return q(i)},disabled:e.disabled,readOnly:e.readOnly,required:e.required,"aria-invalid":e.invalid,checked:f},l),p("input"));return a.createElement("input",g({ref:c},d))},Y=function(){var l=r({className:P("icon")},p("icon")),d=r({className:P("box",{checked:f}),"data-p-highlight":f,"data-p-disabled":e.disabled},p("box")),v=f?e.icon||a.createElement(N,l):null,i=I.getJSXIcon(v,U({},l),{props:e,checked:f});return a.createElement("div",d,i)};return a.createElement(a.Fragment,null,a.createElement("div",g({ref:S},W),X(),Y()),J&&a.createElement(M,g({target:S,content:e.tooltip,pt:p("tooltip")},e.tooltipOptions)))}));H.displayName="Checkbox";export{H as default};