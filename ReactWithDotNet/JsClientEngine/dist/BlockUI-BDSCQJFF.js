import{a as T}from"./chunk-G7S3TOUE.js";import{A,B as N,C,a as v,b as s,d as E,g as k,m as _,n as P,p as U,u as B,v as M}from"./chunk-NI4MO4TP.js";import"./chunk-DLL45UCJ.js";import{a as q}from"./chunk-2W22VGCX.js";import{h as Y}from"./chunk-GYULANB4.js";var l=Y(q());function h(){return h=Object.assign?Object.assign.bind():function(e){for(var t=1;t<arguments.length;t++){var r=arguments[t];for(var o in r)Object.prototype.hasOwnProperty.call(r,o)&&(e[o]=r[o])}return e},h.apply(this,arguments)}function d(e){"@babel/helpers - typeof";return d=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(t){return typeof t}:function(t){return t&&typeof Symbol=="function"&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},d(e)}function F(e,t){if(d(e)!=="object"||e===null)return e;var r=e[Symbol.toPrimitive];if(r!==void 0){var o=r.call(e,t||"default");if(d(o)!=="object")return o;throw new TypeError("@@toPrimitive must return a primitive value.")}return(t==="string"?String:Number)(e)}function G(e){var t=F(e,"string");return d(t)==="symbol"?t:String(t)}function Q(e,t,r){return t=G(t),t in e?Object.defineProperty(e,t,{value:r,enumerable:!0,configurable:!0,writable:!0}):e[t]=r,e}function ee(e){if(Array.isArray(e))return e}function te(e,t){var r=e==null?null:typeof Symbol!="undefined"&&e[Symbol.iterator]||e["@@iterator"];if(r!=null){var o,n,y,u,i=[],c=!0,f=!1;try{if(y=(r=r.call(e)).next,t===0){if(Object(r)!==r)return;c=!1}else for(;!(c=(o=y.call(r)).done)&&(i.push(o.value),i.length!==t);c=!0);}catch(a){f=!0,n=a}finally{try{if(!c&&r.return!=null&&(u=r.return(),Object(u)!==u))return}finally{if(f)throw n}}return i}}function D(e,t){(t==null||t>e.length)&&(t=e.length);for(var r=0,o=new Array(t);r<t;r++)o[r]=e[r];return o}function re(e,t){if(e){if(typeof e=="string")return D(e,t);var r=Object.prototype.toString.call(e).slice(8,-1);if(r==="Object"&&e.constructor&&(r=e.constructor.name),r==="Map"||r==="Set")return Array.from(e);if(r==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(r))return D(e,t)}}function ne(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function oe(e,t){return ee(e)||te(e,t)||re(e,t)||ne()}var le={root:"p-blockui-container",mask:function(t){var r=t.props;return v("p-blockui p-component-overlay p-component-overlay-enter",{"p-blockui-document":r.fullScreen})}},ae=`
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
`,S=N.extend({defaultProps:{__TYPE:"BlockUI",autoZIndex:!0,baseZIndex:0,blocked:!1,className:null,containerClassName:null,containerStyle:null,fullScreen:!1,id:null,onBlocked:null,onUnblocked:null,style:null,template:null,children:void 0},css:{classes:le,styles:ae}});function Z(e,t){var r=Object.keys(e);if(Object.getOwnPropertySymbols){var o=Object.getOwnPropertySymbols(e);t&&(o=o.filter(function(n){return Object.getOwnPropertyDescriptor(e,n).enumerable})),r.push.apply(r,o)}return r}function H(e){for(var t=1;t<arguments.length;t++){var r=arguments[t]!=null?arguments[t]:{};t%2?Z(Object(r),!0).forEach(function(o){Q(e,o,r[o])}):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(r)):Z(Object(r)).forEach(function(o){Object.defineProperty(e,o,Object.getOwnPropertyDescriptor(r,o))})}return e}var $=l.forwardRef(function(e,t){var r=B(),o=l.useContext(_),n=S.getProps(e,o),y=l.useState(n.blocked),u=oe(y,2),i=u[0],c=u[1],f=l.useRef(null),a=l.useRef(null),p=l.useRef(null),g=S.setMetaData({props:n}),R=g.ptm,j=g.cx,x=g.isUnstyled;C(S.css.styles,x,{name:"blockui"});var O=function(){c(!0),p.current=document.activeElement},I=function(){!x()&&s.addClass(a.current,"p-component-overlay-leave"),s.hasCSSAnimation(a.current)>0?a.current.addEventListener("animationend",function(){w()}):w()},w=function(){k.clear(a.current),c(!1),n.fullScreen&&(s.unblockBodyScroll(),p.current&&p.current.focus()),n.onUnblocked&&n.onUnblocked()},z=function(){if(n.fullScreen&&(s.blockBodyScroll(),p.current&&p.current.blur()),n.autoZIndex){var b=n.fullScreen?"modal":"overlay";k.set(b,a.current,o&&o.autoZIndex||P.autoZIndex,n.baseZIndex||o&&o.zIndex[b]||P.zIndex[b])}n.onBlocked&&n.onBlocked()};M(function(){i&&O()}),A(function(){n.blocked?O():I()},[n.blocked]),U(function(){n.fullScreen&&s.unblockBodyScroll(),k.clear(a.current)}),l.useImperativeHandle(t,function(){return{props:n,block:O,unblock:I,getElement:function(){return f.current}}});var K=function(){if(i){var b=n.fullScreen?document.body:"self",V=r({className:v(n.className,j("mask")),style:H(H({},n.style),{},{position:n.fullScreen?"fixed":"absolute",top:"0",left:"0",width:"100%",height:"100%"})},R("mask")),W=n.template?E.getJSXElement(n.template,n):null,X=l.createElement("div",h({ref:a},V),W);return l.createElement(T,{element:X,appendTo:b,onMounted:z})}return null},L=K(),J=r({id:n.id,ref:f,style:n.containerStyle,className:v(n.containerClassName,j("root")),"aria-busy":n.blocked},S.getOtherProps(n),R("root"));return l.createElement("div",J,n.children,L)});$.displayName="BlockUI";export{$ as default};