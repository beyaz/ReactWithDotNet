import{a as S}from"./chunk-KRUAA7WP.js";import{a as B}from"./chunk-RVEJIPMH.js";import{a as x}from"./chunk-M5SPJYNF.js";import{B as j,C as w,a as l,d as m,e as _,m as R,u as h}from"./chunk-NI4MO4TP.js";import{a as Q}from"./chunk-2W22VGCX.js";import{h as G}from"./chunk-GYULANB4.js";var o=G(Q());function g(){return g=Object.assign?Object.assign.bind():function(a){for(var t=1;t<arguments.length;t++){var e=arguments[t];for(var r in e)Object.prototype.hasOwnProperty.call(e,r)&&(a[r]=e[r])}return a},g.apply(this,arguments)}function f(a){"@babel/helpers - typeof";return f=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(t){return typeof t}:function(t){return t&&typeof Symbol=="function"&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},f(a)}function V(a,t){if(f(a)!=="object"||a===null)return a;var e=a[Symbol.toPrimitive];if(e!==void 0){var r=e.call(a,t||"default");if(f(r)!=="object")return r;throw new TypeError("@@toPrimitive must return a primitive value.")}return(t==="string"?String:Number)(a)}function W(a){var t=V(a,"string");return f(t)==="symbol"?t:String(t)}function s(a,t,e){return t=W(t),t in a?Object.defineProperty(a,t,{value:e,enumerable:!0,configurable:!0,writable:!0}):a[t]=e,a}var Z={root:function(t){var e=t.props;return l("p-badge p-component",s({"p-badge-no-gutter":m.isNotEmpty(e.value)&&String(e.value).length===1,"p-badge-dot":m.isEmpty(e.value),"p-badge-lg":e.size==="large","p-badge-xl":e.size==="xlarge"},"p-badge-".concat(e.severity),e.severity!==null))}},ee=`
@layer primereact {
    .p-badge {
        display: inline-block;
        border-radius: 10px;
        text-align: center;
        padding: 0 .5rem;
    }
    
    .p-overlay-badge {
        position: relative;
    }
    
    .p-overlay-badge .p-badge {
        position: absolute;
        top: 0;
        right: 0;
        transform: translate(50%,-50%);
        transform-origin: 100% 0;
        margin: 0;
    }
    
    .p-badge-dot {
        width: .5rem;
        min-width: .5rem;
        height: .5rem;
        border-radius: 50%;
        padding: 0;
    }
    
    .p-badge-no-gutter {
        padding: 0;
        border-radius: 50%;
    }
}
`,y=j.extend({defaultProps:{__TYPE:"Badge",__parentMetadata:null,value:null,severity:null,size:null,style:null,className:null,children:void 0},css:{classes:Z,styles:ee}});function D(a,t){var e=Object.keys(a);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(a);t&&(r=r.filter(function(n){return Object.getOwnPropertyDescriptor(a,n).enumerable})),e.push.apply(e,r)}return e}function te(a){for(var t=1;t<arguments.length;t++){var e=arguments[t]!=null?arguments[t]:{};t%2?D(Object(e),!0).forEach(function(r){s(a,r,e[r])}):Object.getOwnPropertyDescriptors?Object.defineProperties(a,Object.getOwnPropertyDescriptors(e)):D(Object(e)).forEach(function(r){Object.defineProperty(a,r,Object.getOwnPropertyDescriptor(e,r))})}return a}var M=o.memo(o.forwardRef(function(a,t){var e=h(),r=o.useContext(R),n=y.getProps(a,r),i=y.setMetaData(te({props:n},n.__parentMetadata)),v=i.ptm,d=i.cx,c=i.isUnstyled;w(y.css.styles,c,{name:"badge"});var u=o.useRef(null);o.useImperativeHandle(t,function(){return{props:n,getElement:function(){return u.current}}});var P=e({ref:u,style:n.style,className:l(n.className,d("root"))},y.getOtherProps(n),v("root"));return o.createElement("span",P,n.value)}));M.displayName="Badge";var ne={icon:function(t){var e=t.props;return l("p-button-icon p-c",s({},"p-button-icon-".concat(e.iconPos),e.label))},loadingIcon:function(t){var e=t.props,r=t.className;return l(r,{"p-button-loading-icon":e.loading})},label:"p-button-label p-c",root:function(t){var e=t.props,r=t.size,n=t.disabled;return l("p-button p-component",s(s(s(s({"p-button-icon-only":(e.icon||e.loading)&&!e.label&&!e.children,"p-button-vertical":(e.iconPos==="top"||e.iconPos==="bottom")&&e.label,"p-disabled":n,"p-button-loading":e.loading,"p-button-outlined":e.outlined,"p-button-raised":e.raised,"p-button-link":e.link,"p-button-text":e.text,"p-button-rounded":e.rounded,"p-button-loading-label-only":e.loading&&!e.icon&&e.label},"p-button-loading-".concat(e.iconPos),e.loading&&e.label),"p-button-".concat(r),r),"p-button-".concat(e.severity),e.severity),"p-button-plain",e.plain))}},O=j.extend({defaultProps:{__TYPE:"Button",__parentMetadata:null,badge:null,badgeClassName:null,className:null,children:void 0,disabled:!1,icon:null,iconPos:"left",label:null,link:!1,loading:!1,loadingIcon:null,outlined:!1,plain:!1,raised:!1,rounded:!1,severity:null,size:null,text:!1,tooltip:null,tooltipOptions:null,visible:!0},css:{classes:ne}});function I(a,t){var e=Object.keys(a);if(Object.getOwnPropertySymbols){var r=Object.getOwnPropertySymbols(a);t&&(r=r.filter(function(n){return Object.getOwnPropertyDescriptor(a,n).enumerable})),e.push.apply(e,r)}return e}function E(a){for(var t=1;t<arguments.length;t++){var e=arguments[t]!=null?arguments[t]:{};t%2?I(Object(e),!0).forEach(function(r){s(a,r,e[r])}):Object.getOwnPropertyDescriptors?Object.defineProperties(a,Object.getOwnPropertyDescriptors(e)):I(Object(e)).forEach(function(r){Object.defineProperty(a,r,Object.getOwnPropertyDescriptor(e,r))})}return a}var ae=o.memo(o.forwardRef(function(a,t){var e=h(),r=o.useContext(R),n=O.getProps(a,r),i=n.disabled||n.loading,v=E(E({props:n},n.__parentMetadata),{},{context:{disabled:i}}),d=O.setMetaData(v),c=d.ptm,u=d.cx,P=d.isUnstyled;w(O.css.styles,P,{name:"button",styled:!0});var b=o.useRef(t);if(o.useEffect(function(){m.combinedRefs(b,t)},[b,t]),n.visible===!1)return null;var z=function(){var p=l("p-button-icon p-c",s({},"p-button-icon-".concat(n.iconPos),n.label)),J=e({className:u("icon")},c("icon"));p=l(p,{"p-button-loading-icon":n.loading});var X=e({className:u("loadingIcon",{className:p})},c("loadingIcon")),q=n.loading?n.loadingIcon||o.createElement(S,g({},X,{spin:!0})):n.icon;return _.getJSXIcon(q,E({},J),{props:n})},T=function(){var p=e({className:u("label")},c("label"));return n.label?o.createElement("span",p,n.label):!n.children&&!n.label&&o.createElement("span",g({},p,{dangerouslySetInnerHTML:{__html:"&nbsp;"}}))},C=function(){if(n.badge){var p=e({className:l(n.badgeClassName),value:n.badge,unstyled:n.unstyled,__parentMetadata:{parent:v}},c("badge"));return o.createElement(M,p,n.badge)}return null},U=!i||n.tooltipOptions&&n.tooltipOptions.showOnDisabled,$=m.isNotEmpty(n.tooltip)&&U,L={large:"lg",small:"sm"},H=L[n.size],K=z(),F=T(),Y=C(),k=n.label?n.label+(n.badge?" "+n.badge:""):n["aria-label"],A=e({ref:b,"aria-label":k,"data-pc-autofocus":n.autoFocus,className:l(n.className,u("root",{size:H,disabled:i})),disabled:i},O.getOtherProps(n),c("root"));return o.createElement(o.Fragment,null,o.createElement("button",A,K,F,n.children,Y,o.createElement(x,null)),$&&o.createElement(B,g({target:b,content:n.tooltip,pt:c("tooltip")},n.tooltipOptions)))}));ae.displayName="Button";export{ae as a};
