import{a as T}from"./chunk-35DEAE55.js";import{A,B as N,C,a as v,b as s,d as w,g as k,m as U,n as O,p as B,u as _,v as M}from"./chunk-MY5M2XOA.js";import"./chunk-DLL45UCJ.js";import{a as q}from"./chunk-2W22VGCX.js";import{h as Y}from"./chunk-GYULANB4.js";var l=Y(q());function h(){return h=Object.assign?Object.assign.bind():function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var o in n)({}).hasOwnProperty.call(n,o)&&(e[o]=n[o])}return e},h.apply(null,arguments)}function y(e){"@babel/helpers - typeof";return y=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(t){return typeof t}:function(t){return t&&typeof Symbol=="function"&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},y(e)}function F(e,t){if(y(e)!="object"||!e)return e;var n=e[Symbol.toPrimitive];if(n!==void 0){var o=n.call(e,t||"default");if(y(o)!="object")return o;throw new TypeError("@@toPrimitive must return a primitive value.")}return(t==="string"?String:Number)(e)}function G(e){var t=F(e,"string");return y(t)=="symbol"?t:t+""}function Q(e,t,n){return(t=G(t))in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function ee(e){if(Array.isArray(e))return e}function te(e,t){var n=e==null?null:typeof Symbol!="undefined"&&e[Symbol.iterator]||e["@@iterator"];if(n!=null){var o,r,d,u,i=[],c=!0,f=!1;try{if(d=(n=n.call(e)).next,t===0){if(Object(n)!==n)return;c=!1}else for(;!(c=(o=d.call(n)).done)&&(i.push(o.value),i.length!==t);c=!0);}catch(a){f=!0,r=a}finally{try{if(!c&&n.return!=null&&(u=n.return(),Object(u)!==u))return}finally{if(f)throw r}}return i}}function D(e,t){(t==null||t>e.length)&&(t=e.length);for(var n=0,o=Array(t);n<t;n++)o[n]=e[n];return o}function ne(e,t){if(e){if(typeof e=="string")return D(e,t);var n={}.toString.call(e).slice(8,-1);return n==="Object"&&e.constructor&&(n=e.constructor.name),n==="Map"||n==="Set"?Array.from(e):n==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)?D(e,t):void 0}}function re(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function oe(e,t){return ee(e)||te(e,t)||ne(e,t)||re()}var le={root:"p-blockui-container",mask:function(t){var n=t.props;return v("p-blockui p-component-overlay p-component-overlay-enter",{"p-blockui-document":n.fullScreen})}},ae=`
@layer primereact {
    .p-blockui-container {
        position: relative;
    }
    
    .p-blockui {
        opacity: 1;
        display: flex;
        align-items: center;
        justify-content: center;
    }
    
    .p-blockui.p-component-overlay {
        position: absolute;
    }
    
    .p-blockui-document.p-component-overlay {
        position: fixed;
    }
}
`,S=N.extend({defaultProps:{__TYPE:"BlockUI",autoZIndex:!0,baseZIndex:0,blocked:!1,className:null,containerClassName:null,containerStyle:null,fullScreen:!1,id:null,onBlocked:null,onUnblocked:null,style:null,template:null,children:void 0},css:{classes:le,styles:ae}});function Z(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var o=Object.getOwnPropertySymbols(e);t&&(o=o.filter(function(r){return Object.getOwnPropertyDescriptor(e,r).enumerable})),n.push.apply(n,o)}return n}function H(e){for(var t=1;t<arguments.length;t++){var n=arguments[t]!=null?arguments[t]:{};t%2?Z(Object(n),!0).forEach(function(o){Q(e,o,n[o])}):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):Z(Object(n)).forEach(function(o){Object.defineProperty(e,o,Object.getOwnPropertyDescriptor(n,o))})}return e}var $=l.forwardRef(function(e,t){var n=_(),o=l.useContext(U),r=S.getProps(e,o),d=l.useState(r.blocked),u=oe(d,2),i=u[0],c=u[1],f=l.useRef(null),a=l.useRef(null),p=l.useRef(null),g=S.setMetaData({props:r}),j=g.ptm,R=g.cx,x=g.isUnstyled;C(S.css.styles,x,{name:"blockui"});var P=function(){c(!0),p.current=document.activeElement},I=function(){!x()&&s.addClass(a.current,"p-component-overlay-leave"),s.hasCSSAnimation(a.current)>0?a.current.addEventListener("animationend",function(){E()}):E()},E=function(){k.clear(a.current),c(!1),r.fullScreen&&(s.unblockBodyScroll(),p.current&&p.current.focus()),r.onUnblocked&&r.onUnblocked()},L=function(){if(r.fullScreen&&(s.blockBodyScroll(),p.current&&p.current.blur()),r.autoZIndex){var b=r.fullScreen?"modal":"overlay";k.set(b,a.current,o&&o.autoZIndex||O.autoZIndex,r.baseZIndex||o&&o.zIndex[b]||O.zIndex[b])}r.onBlocked&&r.onBlocked()};M(function(){i&&P()}),A(function(){r.blocked?P():I()},[r.blocked]),B(function(){r.fullScreen&&s.unblockBodyScroll(),k.clear(a.current)}),l.useImperativeHandle(t,function(){return{props:r,block:P,unblock:I,getElement:function(){return f.current}}});var z=function(){if(i){var b=r.fullScreen?document.body:"self",V=n({className:v(r.className,R("mask")),style:H(H({},r.style),{},{position:r.fullScreen?"fixed":"absolute",top:"0",left:"0",width:"100%",height:"100%"})},j("mask")),W=r.template?w.getJSXElement(r.template,r):null,X=l.createElement("div",h({ref:a},V),W);return l.createElement(T,{element:X,appendTo:b,onMounted:L})}return null},K=z(),J=n({id:r.id,ref:f,style:r.containerStyle,className:v(r.containerClassName,R("root")),"aria-busy":r.blocked},S.getOtherProps(r),j("root"));return l.createElement("div",J,r.children,K)});$.displayName="BlockUI";export{$ as default};