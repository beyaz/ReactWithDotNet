import{A as P,B as C,a as _,b as u,d as h,m as x,n as A,p as D,u as T,v as k,z as M}from"./chunk-NI4MO4TP.js";import{a as z}from"./chunk-2W22VGCX.js";import{h as q}from"./chunk-GYULANB4.js";var a=q(z());function O(){return O=Object.assign?Object.assign.bind():function(e){for(var t=1;t<arguments.length;t++){var r=arguments[t];for(var n in r)Object.prototype.hasOwnProperty.call(r,n)&&(e[n]=r[n])}return e},O.apply(this,arguments)}function d(e){"@babel/helpers - typeof";return d=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(t){return typeof t}:function(t){return t&&typeof Symbol=="function"&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},d(e)}function F(e,t){if(d(e)!=="object"||e===null)return e;var r=e[Symbol.toPrimitive];if(r!==void 0){var n=r.call(e,t||"default");if(d(n)!=="object")return n;throw new TypeError("@@toPrimitive must return a primitive value.")}return(t==="string"?String:Number)(e)}function G(e){var t=F(e,"string");return d(t)==="symbol"?t:String(t)}function J(e,t,r){return t=G(t),t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function Q(e){if(Array.isArray(e))return e}function V(e,t){var r=e==null?null:typeof Symbol!="undefined"&&e[Symbol.iterator]||e["@@iterator"];if(r!=null){var n,l,g,o,i=[],p=!0,f=!1;try{if(g=(r=r.call(e)).next,t===0){if(Object(r)!==r)return;p=!1}else for(;!(p=(n=g.call(r)).done)&&(i.push(n.value),i.length!==t);p=!0);}catch(y){f=!0,l=y}finally{try{if(!p&&r.return!=null&&(o=r.return(),Object(o)!==o))return}finally{if(f)throw l}}return i}}function I(e,t){(t==null||t>e.length)&&(t=e.length);for(var r=0,n=new Array(t);r<t;r++)n[r]=e[r];return n}function Z(e,t){if(e){if(typeof e=="string")return I(e,t);var r=Object.prototype.toString.call(e).slice(8,-1);if(r==="Object"&&e.constructor&&(r=e.constructor.name),r==="Map"||r==="Set")return Array.from(e);if(r==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(r))return I(e,t)}}function ee(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function te(e,t){return Q(e)||V(e,t)||Z(e,t)||ee()}var re=`
@layer primereact {
    .p-ripple {
        overflow: hidden;
        position: relative;
    }
    
    .p-ink {
        display: block;
        position: absolute;
        background: rgba(255, 255, 255, 0.5);
        border-radius: 100%;
        transform: scale(0);
    }
    
    .p-ink-active {
        animation: ripple 0.4s linear;
    }
    
    .p-ripple-disabled .p-ink {
        display: none;
    }
}

@keyframes ripple {
    100% {
        opacity: 0;
        transform: scale(2.5);
    }
}

`,ne={root:"p-ink"},m=C.extend({defaultProps:{__TYPE:"Ripple",children:void 0},css:{styles:re,classes:ne},getProps:function(t){return h.getMergedProps(t,m.defaultProps)},getOtherProps:function(t){return h.getDiffProps(t,m.defaultProps)}});function H(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var n=Object.getOwnPropertySymbols(e);t&&(n=n.filter(function(l){return Object.getOwnPropertyDescriptor(e,l).enumerable})),r.push.apply(r,n)}return r}function oe(e){for(var t=1;t<arguments.length;t++){var r=arguments[t]!=null?arguments[t]:{};t%2?H(Object(r),!0).forEach(function(n){J(e,n,r[n])}):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):H(Object(r)).forEach(function(n){Object.defineProperty(e,n,Object.getOwnPropertyDescriptor(r,n))})}return e}var ie=a.memo(a.forwardRef(function(e,t){var r=a.useState(!1),n=te(r,2),l=n[0],g=n[1],o=a.useRef(null),i=a.useRef(null),p=T(),f=a.useContext(x),y=m.getProps(e,f),R=f&&f.ripple||A.ripple,L={props:y};M(m.css.styles,{name:"ripple",manual:!R});var w=m.setMetaData(oe({},L)),N=w.ptm,U=w.cx,j=function(){return o.current&&o.current.parentElement},S=function(){i.current&&i.current.addEventListener("pointerdown",E)},W=function(){i.current&&i.current.removeEventListener("pointerdown",E)},E=function(c){var v=u.getOffset(i.current),K=c.pageX-v.left+document.body.scrollTop-u.getWidth(o.current)/2,X=c.pageY-v.top+document.body.scrollLeft-u.getHeight(o.current)/2;$(K,X)},$=function(c,v){!o.current||getComputedStyle(o.current,null).display==="none"||(u.removeClass(o.current,"p-ink-active"),b(),o.current.style.top=v+"px",o.current.style.left=c+"px",u.addClass(o.current,"p-ink-active"))},B=function(c){u.removeClass(c.currentTarget,"p-ink-active")},b=function(){if(o.current&&!u.getHeight(o.current)&&!u.getWidth(o.current)){var c=Math.max(u.getOuterWidth(i.current),u.getOuterHeight(i.current));o.current.style.height=c+"px",o.current.style.width=c+"px"}};if(a.useImperativeHandle(t,function(){return{props:y,getInk:function(){return o.current},getTarget:function(){return i.current}}}),k(function(){g(!0)}),P(function(){l&&o.current&&(i.current=j(),b(),S())},[l]),P(function(){o.current&&!i.current&&(i.current=j(),b(),S())}),D(function(){o.current&&(i.current=null,W())}),!R)return null;var Y=p({"aria-hidden":!0,className:_(U("root"))},m.getOtherProps(y),N("root"));return a.createElement("span",O({role:"presentation",ref:o},Y,{onAnimationEnd:B}))}));ie.displayName="Ripple";export{ie as a};
