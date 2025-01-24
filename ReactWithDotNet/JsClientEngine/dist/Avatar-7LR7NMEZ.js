import{B as w,C as _,a as O,b as E,d as y,e as P,m as R,u as A}from"./chunk-MY5M2XOA.js";import{a as q}from"./chunk-2W22VGCX.js";import{h as B}from"./chunk-GYULANB4.js";var o=B(q());function m(e){"@babel/helpers - typeof";return m=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(r){return typeof r}:function(r){return r&&typeof Symbol=="function"&&r.constructor===Symbol&&r!==Symbol.prototype?"symbol":typeof r},m(e)}function J(e,r){if(m(e)!="object"||!e)return e;var t=e[Symbol.toPrimitive];if(t!==void 0){var n=t.call(e,r||"default");if(m(n)!="object")return n;throw new TypeError("@@toPrimitive must return a primitive value.")}return(r==="string"?String:Number)(e)}function K(e){var r=J(e,"string");return m(r)=="symbol"?r:r+""}function L(e,r,t){return(r=K(r))in e?Object.defineProperty(e,r,{value:t,enumerable:!0,configurable:!0,writable:!0}):e[r]=t,e}function S(){return S=Object.assign?Object.assign.bind():function(e){for(var r=1;r<arguments.length;r++){var t=arguments[r];for(var n in t)({}).hasOwnProperty.call(t,n)&&(e[n]=t[n])}return e},S.apply(null,arguments)}function X(e){if(Array.isArray(e))return e}function W(e,r){var t=e==null?null:typeof Symbol!="undefined"&&e[Symbol.iterator]||e["@@iterator"];if(t!=null){var n,a,i,c,s=[],l=!0,p=!1;try{if(i=(t=t.call(e)).next,r===0){if(Object(t)!==t)return;l=!1}else for(;!(l=(n=i.call(t)).done)&&(s.push(n.value),s.length!==r);l=!0);}catch(b){p=!0,a=b}finally{try{if(!l&&t.return!=null&&(c=t.return(),Object(c)!==c))return}finally{if(p)throw a}}return s}}function I(e,r){(r==null||r>e.length)&&(r=e.length);for(var t=0,n=Array(r);t<r;t++)n[t]=e[t];return n}function Y(e,r){if(e){if(typeof e=="string")return I(e,r);var t={}.toString.call(e).slice(8,-1);return t==="Object"&&e.constructor&&(t=e.constructor.name),t==="Map"||t==="Set"?Array.from(e):t==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t)?I(e,r):void 0}}function G(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function x(e,r){return X(e)||W(e,r)||Y(e,r)||G()}var Q={root:function(r){var t=r.props,n=r.state;return O("p-avatar p-component",{"p-avatar-image":y.isNotEmpty(t.image)&&!n.imageFailed,"p-avatar-circle":t.shape==="circle","p-avatar-lg":t.size==="large","p-avatar-xl":t.size==="xlarge","p-avatar-clickable":!!t.onClick})},label:"p-avatar-text",icon:"p-avatar-icon"},V=`
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
`,g=w.extend({defaultProps:{__TYPE:"Avatar",className:null,icon:null,image:null,imageAlt:"avatar",imageFallback:"default",label:null,onImageError:null,shape:"square",size:"normal",style:null,template:null,children:void 0},css:{classes:Q,styles:V}});function N(e,r){var t=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);r&&(n=n.filter(function(a){return Object.getOwnPropertyDescriptor(e,a).enumerable})),t.push.apply(t,n)}return t}function Z(e){for(var r=1;r<arguments.length;r++){var t=arguments[r]!=null?arguments[r]:{};r%2?N(Object(t),!0).forEach(function(n){L(e,n,t[n])}):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(t)):N(Object(t)).forEach(function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(t,n))})}return e}var k=o.forwardRef(function(e,r){var t=A(),n=o.useContext(R),a=g.getProps(e,n),i=o.useRef(null),c=o.useState(!1),s=x(c,2),l=s[0],p=s[1],b=o.useState(!1),j=x(b,2),F=j[0],C=j[1],d=g.setMetaData({props:a,state:{imageFailed:l,nested:F}}),f=d.ptm,h=d.cx,D=d.isUnstyled;_(g.css.styles,D,{name:"avatar"});var T=function(){if(y.isNotEmpty(a.image)&&!l){var u=t({src:a.image,onError:$},f("image"));return o.createElement("img",S({alt:a.imageAlt},u))}else if(a.label){var H=t({className:h("label")},f("label"));return o.createElement("span",H,a.label)}else if(a.icon){var M=t({className:h("icon")},f("icon"));return P.getJSXIcon(a.icon,Z({},M),{props:a})}return null},$=function(u){a.imageFallback==="default"?a.onImageError||(p(!0),u.target.src=null):u.target.src=a.imageFallback,a.onImageError&&a.onImageError(u)};o.useEffect(function(){var v=E.isAttributeEquals(i.current.parentElement,"data-pc-name","avatargroup");C(v)},[]),o.useImperativeHandle(r,function(){return{props:a,getElement:function(){return i.current}}});var z=t({ref:i,style:a.style,className:O(a.className,h("root",{imageFailed:l}))},g.getOtherProps(a),f("root")),U=a.template?y.getJSXElement(a.template,a):T();return o.createElement("div",z,U,a.children)});k.displayName="Avatar";export{k as default};
