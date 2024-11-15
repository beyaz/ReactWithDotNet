import{B as w,C as _,a as O,b as P,d as v,e as j,m as R,u as A}from"./chunk-NI4MO4TP.js";import{a as q}from"./chunk-2W22VGCX.js";import{h as B}from"./chunk-GYULANB4.js";var o=B(q());function m(e){"@babel/helpers - typeof";return m=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(t){return typeof t}:function(t){return t&&typeof Symbol=="function"&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},m(e)}function J(e,t){if(m(e)!=="object"||e===null)return e;var r=e[Symbol.toPrimitive];if(r!==void 0){var n=r.call(e,t||"default");if(m(n)!=="object")return n;throw new TypeError("@@toPrimitive must return a primitive value.")}return(t==="string"?String:Number)(e)}function K(e){var t=J(e,"string");return m(t)==="symbol"?t:String(t)}function X(e,t,r){return t=K(t),t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function S(){return S=Object.assign?Object.assign.bind():function(e){for(var t=1;t<arguments.length;t++){var r=arguments[t];for(var n in r)Object.prototype.hasOwnProperty.call(r,n)&&(e[n]=r[n])}return e},S.apply(this,arguments)}function L(e){if(Array.isArray(e))return e}function W(e,t){var r=e==null?null:typeof Symbol!="undefined"&&e[Symbol.iterator]||e["@@iterator"];if(r!=null){var n,a,i,c,s=[],l=!0,p=!1;try{if(i=(r=r.call(e)).next,t===0){if(Object(r)!==r)return;l=!1}else for(;!(l=(n=i.call(r)).done)&&(s.push(n.value),s.length!==t);l=!0);}catch(b){p=!0,a=b}finally{try{if(!l&&r.return!=null&&(c=r.return(),Object(c)!==c))return}finally{if(p)throw a}}return s}}function I(e,t){(t==null||t>e.length)&&(t=e.length);for(var r=0,n=new Array(t);r<t;r++)n[r]=e[r];return n}function Y(e,t){if(e){if(typeof e=="string")return I(e,t);var r=Object.prototype.toString.call(e).slice(8,-1);if(r==="Object"&&e.constructor&&(r=e.constructor.name),r==="Map"||r==="Set")return Array.from(e);if(r==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(r))return I(e,t)}}function G(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function x(e,t){return L(e)||W(e,t)||Y(e,t)||G()}var Q={root:function(t){var r=t.props,n=t.state;return O("p-avatar p-component",{"p-avatar-image":v.isNotEmpty(r.image)&&!n.imageFailed,"p-avatar-circle":r.shape==="circle","p-avatar-lg":r.size==="large","p-avatar-xl":r.size==="xlarge","p-avatar-clickable":!!r.onClick})},label:"p-avatar-text",icon:"p-avatar-icon"},V=`
@layer primereact {
    .p-avatar {
        display: inline-flex;
        align-items: center;
        justify-content: center;
        width: 2rem;
        height: 2rem;
        font-size: 1rem;
    }
    
    .p-avatar.p-avatar-image {
        background-color: transparent;
    }
    
    .p-avatar.p-avatar-circle {
        border-radius: 50%;
    }
    
    .p-avatar.p-avatar-circle img {
        border-radius: 50%;
    }
    
    .p-avatar .p-avatar-icon {
        font-size: 1rem;
    }
    
    .p-avatar img {
        width: 100%;
        height: 100%;
    }
    
    .p-avatar-clickable {
        cursor: pointer;
    }
}
`,y=w.extend({defaultProps:{__TYPE:"Avatar",className:null,icon:null,image:null,imageAlt:"avatar",imageFallback:"default",label:null,onImageError:null,shape:"square",size:"normal",style:null,template:null,children:void 0},css:{classes:Q,styles:V}});function N(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter(function(a){return Object.getOwnPropertyDescriptor(e,a).enumerable})),r.push.apply(r,n)}return r}function Z(e){for(var t=1;t<arguments.length;t++){var r=arguments[t]!=null?arguments[t]:{};t%2?N(Object(r),!0).forEach(function(n){X(e,n,r[n])}):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):N(Object(r)).forEach(function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(r,n))})}return e}var F=o.forwardRef(function(e,t){var r=A(),n=o.useContext(R),a=y.getProps(e,n),i=o.useRef(null),c=o.useState(!1),s=x(c,2),l=s[0],p=s[1],b=o.useState(!1),E=x(b,2),C=E[0],D=E[1],d=y.setMetaData({props:a,state:{imageFailed:l,nested:C}}),f=d.ptm,h=d.cx,T=d.isUnstyled;_(y.css.styles,T,{name:"avatar"});var $=function(){if(v.isNotEmpty(a.image)&&!l){var u=r({src:a.image,onError:z},f("image"));return o.createElement("img",S({alt:a.imageAlt},u))}else if(a.label){var H=r({className:h("label")},f("label"));return o.createElement("span",H,a.label)}else if(a.icon){var M=r({className:h("icon")},f("icon"));return j.getJSXIcon(a.icon,Z({},M),{props:a})}return null},z=function(u){a.imageFallback==="default"?a.onImageError||(p(!0),u.target.src=null):u.target.src=a.imageFallback,a.onImageError&&a.onImageError(u)};o.useEffect(function(){var g=P.isAttributeEquals(i.current.parentElement,"data-pc-name","avatargroup");D(g)},[]),o.useImperativeHandle(t,function(){return{props:a,getElement:function(){return i.current}}});var U=r({ref:i,style:a.style,className:O(a.className,h("root",{imageFailed:l}))},y.getOtherProps(a),f("root")),k=a.template?v.getJSXElement(a.template,a):$();return o.createElement("div",U,k,a.children)});F.displayName="Avatar";export{F as default};
