import{a as q}from"./chunk-A6SSPAVQ.js";import{a as W}from"./chunk-HHHF7C2S.js";import{a as L}from"./chunk-WMGOVPOA.js";import{a as F}from"./chunk-73MOHHDI.js";import"./chunk-3Q5HW2ZY.js";import{B,C as D,a as P,d as v,e as M,f as U,m as X,u as $,v as H}from"./chunk-MY5M2XOA.js";import"./chunk-UCYY7ZYD.js";import"./chunk-FXFK3YHI.js";import"./chunk-DLL45UCJ.js";import"./chunk-SI5MANRI.js";import"./chunk-22HPGI5J.js";import{a as pe}from"./chunk-2W22VGCX.js";import{h as ce}from"./chunk-GYULANB4.js";var a=ce(pe());function I(){return I=Object.assign?Object.assign.bind():function(n){for(var r=1;r<arguments.length;r++){var t=arguments[r];for(var o in t)({}).hasOwnProperty.call(t,o)&&(n[o]=t[o])}return n},I.apply(null,arguments)}function ue(n){if(Array.isArray(n))return n}function ge(n,r){var t=n==null?null:typeof Symbol!="undefined"&&n[Symbol.iterator]||n["@@iterator"];if(t!=null){var o,e,y,d,i=[],f=!0,b=!1;try{if(y=(t=t.call(n)).next,r===0){if(Object(t)!==t)return;f=!1}else for(;!(f=(o=y.call(t)).done)&&(i.push(o.value),i.length!==r);f=!0);}catch(E){b=!0,e=E}finally{try{if(!f&&t.return!=null&&(d=t.return(),Object(d)!==d))return}finally{if(b)throw e}}return i}}function Y(n,r){(r==null||r>n.length)&&(r=n.length);for(var t=0,o=Array(r);t<r;t++)o[t]=n[t];return o}function me(n,r){if(n){if(typeof n=="string")return Y(n,r);var t={}.toString.call(n).slice(8,-1);return t==="Object"&&n.constructor&&(t=n.constructor.name),t==="Map"||t==="Set"?Array.from(n):t==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t)?Y(n,r):void 0}}function de(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function z(n,r){return ue(n)||ge(n,r)||me(n,r)||de()}var x=B.extend({defaultProps:{__TYPE:"Panel",id:null,header:null,headerTemplate:null,footer:null,footerTemplate:null,toggleable:null,style:null,className:null,collapsed:null,expandIcon:null,collapseIcon:null,icons:null,transitionOptions:null,onExpand:null,onCollapse:null,onToggle:null,children:void 0},css:{classes:{root:function(r){var t=r.props;return P("p-panel p-component",{"p-panel-toggleable":t.toggleable})},header:"p-panel-header",title:"p-panel-title",icons:"p-panel-icons",toggler:"p-panel-header-icon p-panel-toggler p-link",togglerIcon:"p-panel-header-icon p-panel-toggler p-link",toggleableContent:"p-toggleable-content",content:"p-panel-content",footer:"p-panel-footer",transition:"p-toggleable-content"},styles:`
        @layer primereact {
            .p-panel-header {
              display: flex;
              justify-content: space-between;
              align-items: center;
            }
            
            .p-panel-title {
              line-height: 1;
            }
            
            .p-panel-header-icon {
              display: inline-flex;
              justify-content: center;
              align-items: center;
              cursor: pointer;
              text-decoration: none;
              overflow: hidden;
              position: relative;
            }
        }
        `}}),G=a.forwardRef(function(n,r){var t=$(),o=a.useContext(X),e=x.getProps(n,o),y=a.useState(e.id),d=z(y,2),i=d[0],f=d[1],b=a.useState(e.collapsed),E=z(b,2),K=E[0],N=E[1],_=a.useRef(null),S=a.useRef(null),u=e.toggleable?e.onToggle?e.collapsed:K:!1,O=i+"_header",A=i+"_content",C=x.setMetaData({props:e,state:{id:i,collapsed:u}}),c=C.ptm,p=C.cx,Q=C.isUnstyled;D(x.css.styles,Q,{name:"panel"});var T=function(l){e.toggleable&&(u?j(l):w(l),l&&(e.onToggle&&e.onToggle({originalEvent:l,value:!u}),l.preventDefault()))},j=function(l){e.onToggle||N(!1),e.onExpand&&l&&e.onExpand(l)},w=function(l){e.onToggle||N(!0),e.onCollapse&&l&&e.onCollapse(l)};a.useImperativeHandle(r,function(){return{props:e,toggle:T,expand:j,collapse:w,getElement:function(){return _.current},getContent:function(){return S.current}}}),H(function(){i||f(U())});var V=function(){if(e.toggleable){var l=i+"_label",m=t({className:p("toggler"),onClick:T,id:l,"aria-controls":A,"aria-expanded":!u,type:"button",role:"button","aria-label":e.header},c("toggler")),s=t(c("togglericon")),h=u?e.expandIcon||a.createElement(q,s):e.collapseIcon||a.createElement(W,s),R=M.getJSXIcon(h,s,{props:e,collapsed:u});return a.createElement("button",m,R,a.createElement(F,null))}return null},Z=function(){var l=v.getJSXElement(e.header,e),m=v.getJSXElement(e.icons,e),s=V(),h=t({id:O,className:p("title")},c("title")),R=a.createElement("span",h,l),oe=t({className:p("icons")},c("icons")),k=a.createElement("div",oe,m,s),ie=t({className:p("header")},c("header")),J=a.createElement("div",ie,R,k);if(e.headerTemplate){var se={className:"p-panel-header",titleClassName:"p-panel-title",iconsClassName:"p-panel-icons",togglerClassName:"p-panel-header-icon p-panel-toggler p-link",onTogglerClick:T,titleElement:R,iconsElement:k,togglerElement:s,element:J,id:i+"_header",props:e,collapsed:u};return v.getJSXElement(e.headerTemplate,se)}else if(e.header||e.toggleable)return J;return null},ee=function(){var l=v.getJSXElement(e.footer,e),m=t({className:p("footer")},c("footer")),s=a.createElement("div",m,l);if(e.footerTemplate){var h={className:p("footer"),element:s,props:e};return v.getJSXElement(e.footerTemplate,h)}else if(e.footer)return s;return null},te=function(){var l=t({ref:S,className:p("toggleableContent"),"aria-hidden":u,role:"region",id:A,"aria-labelledby":O},c("toggleablecontent")),m=t({className:p("content")},c("content")),s=t({classNames:p("transition"),timeout:{enter:1e3,exit:450},in:!u,unmountOnExit:!0,options:e.transitionOptions},c("transition"));return a.createElement(L,I({nodeRef:S},s),a.createElement("div",l,a.createElement("div",m,e.children)))},ne=t({id:i,ref:_,style:e.style,className:P(e.className,p("root"))},x.getOtherProps(e),c("root")),ae=Z(),re=te(),le=ee();return a.createElement("div",ne,ae,re,le)});G.displayName="Panel";export{G as default};