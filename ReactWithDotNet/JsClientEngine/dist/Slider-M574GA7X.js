import{B as te,C as ae,a as k,b as M,d as I,m as ne,q as P,u as re}from"./chunk-MY5M2XOA.js";import{a as Me}from"./chunk-2W22VGCX.js";import{h as ke}from"./chunk-GYULANB4.js";var u=ke(Me());function K(){return K=Object.assign?Object.assign.bind():function(r){for(var t=1;t<arguments.length;t++){var a=arguments[t];for(var l in a)({}).hasOwnProperty.call(a,l)&&(r[l]=a[l])}return r},K.apply(null,arguments)}function O(r){"@babel/helpers - typeof";return O=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(t){return typeof t}:function(t){return t&&typeof Symbol=="function"&&t.constructor===Symbol&&t!==Symbol.prototype?"symbol":typeof t},O(r)}function Ie(r,t){if(O(r)!="object"||!r)return r;var a=r[Symbol.toPrimitive];if(a!==void 0){var l=a.call(r,t||"default");if(O(l)!="object")return l;throw new TypeError("@@toPrimitive must return a primitive value.")}return(t==="string"?String:Number)(r)}function Ce(r){var t=Ie(r,"string");return O(t)=="symbol"?t:t+""}function He(r,t,a){return(t=Ce(t))in r?Object.defineProperty(r,t,{value:a,enumerable:!0,configurable:!0,writable:!0}):r[t]=a,r}function W(r,t){(t==null||t>r.length)&&(t=r.length);for(var a=0,l=Array(t);a<t;a++)l[a]=r[a];return l}function ze(r){if(Array.isArray(r))return W(r)}function Ne(r){if(typeof Symbol!="undefined"&&r[Symbol.iterator]!=null||r["@@iterator"]!=null)return Array.from(r)}function oe(r,t){if(r){if(typeof r=="string")return W(r,t);var a={}.toString.call(r).slice(8,-1);return a==="Object"&&r.constructor&&(a=r.constructor.name),a==="Map"||a==="Set"?Array.from(r):a==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(a)?W(r,t):void 0}}function Ue(){throw new TypeError(`Invalid attempt to spread non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function Ke(r){return ze(r)||Ne(r)||oe(r)||Ue()}function We(r){if(Array.isArray(r))return r}function Be(r,t){var a=r==null?null:typeof Symbol!="undefined"&&r[Symbol.iterator]||r["@@iterator"];if(a!=null){var l,v,h,R,e=[],g=!0,m=!1;try{if(h=(a=a.call(r)).next,t===0){if(Object(a)!==a)return;g=!1}else for(;!(g=(l=h.call(a)).done)&&(e.push(l.value),e.length!==t);g=!0);}catch(w){m=!0,v=w}finally{try{if(!g&&a.return!=null&&(R=a.return(),Object(R)!==R))return}finally{if(m)throw v}}return e}}function Ye(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function C(r,t){return We(r)||Be(r,t)||oe(r,t)||Ye()}var Xe={handle:function(t){var a=t.index,l=t.handleIndex;return k("p-slider-handle",{"p-slider-handle-start":a===0,"p-slider-handle-end":a===1,"p-slider-handle-active":l.current===a})},range:"p-slider-range",root:function(t){var a=t.props,l=t.vertical,v=t.horizontal;return k("p-slider p-component",{"p-disabled":a.disabled,"p-slider-horizontal":v,"p-slider-vertical":l})}},$e=`
@layer primereact {
    .p-slider {
        position: relative;
    }
    
    .p-slider .p-slider-handle {
        position: absolute;
        cursor: grab;
        touch-action: none;
        display: block;
        z-index: 1;
    }
    
    .p-slider .p-slider-handle.p-slider-handle-active {
        z-index: 2;
    }
    
    .p-slider-range {
        position: absolute;
        display: block;
    }
    
    .p-slider-horizontal .p-slider-range {
        top: 0;
        left: 0;
        height: 100%;
    }
    
    .p-slider-horizontal .p-slider-handle {
        top: 50%;
    }
    
    .p-slider-vertical {
        height: 100px;
    }
    
    .p-slider-vertical .p-slider-handle {
        left: 50%;
    }
    
    .p-slider-vertical .p-slider-range {
        bottom: 0;
        left: 0;
        width: 100%;
    }
}
`,Fe={handle:{position:"absolute"},range:{position:"absolute"}},_=te.extend({defaultProps:{__TYPE:"Slider",id:null,value:null,min:0,max:100,orientation:"horizontal",step:null,range:!1,style:null,className:null,disabled:!1,tabIndex:0,onChange:null,onSlideEnd:null,children:void 0},css:{classes:Xe,styles:$e,inlineStyles:Fe}});function ie(r,t){var a=Object.keys(r);if(Object.getOwnPropertySymbols){var l=Object.getOwnPropertySymbols(r);t&&(l=l.filter(function(v){return Object.getOwnPropertyDescriptor(r,v).enumerable})),a.push.apply(a,l)}return a}function S(r){for(var t=1;t<arguments.length;t++){var a=arguments[t]!=null?arguments[t]:{};t%2?ie(Object(a),!0).forEach(function(l){He(r,l,a[l])}):Object.getOwnPropertyDescriptors?Object.defineProperties(r,Object.getOwnPropertyDescriptors(a)):ie(Object(a)).forEach(function(l){Object.defineProperty(r,l,Object.getOwnPropertyDescriptor(a,l))})}return r}var le=u.memo(u.forwardRef(function(r,t){var a,l,v,h=re(),R=u.useContext(ne),e=_.getProps(r,R),g=u.useRef(null),m=u.useRef(0),w=u.useRef(!1),x=u.useRef(!1),B=u.useRef(0),Y=u.useRef(0),X=u.useRef(0),H=u.useRef(0),z=u.useRef(),d=e.range?(a=e.value)!==null&&a!==void 0?a:[e.min,e.max]:(l=(v=e.value)!==null&&v!==void 0?v:e.min)!==null&&l!==void 0?l:0,y=e.orientation==="horizontal",ue=e.orientation==="vertical",se=P({type:"mousemove",listener:function(n){return Q(n)}}),$=C(se,2),ce=$[0],de=$[1],pe=P({type:"mouseup",listener:function(n){return Z(n)}}),F=C(pe,2),fe=F[0],me=F[1],ve=P({type:"touchmove",listener:function(n){return Q(n)}}),q=C(ve,2),ge=q[0],he=q[1],ye=P({type:"touchend",listener:function(n){return Z(n)}}),G=C(ye,2),be=G[0],Se=G[1],T=_.setMetaData({props:e}),j=T.ptm,A=T.cx,N=T.sx,xe=T.isUnstyled;ae(_.css.styles,xe,{name:"slider"});var D=function(n,i){var o=e.range?d[m.current]:d,c=(e.step||1)*i;ee(n,o+c),n.preventDefault()},J=function(n,i){e.disabled||(x.current=!0,V(),w.current=!0,e.range&&d[0]===e.max?m.current=0:m.current=i,n.preventDefault())},Q=function(n){x.current&&(U(n),n.preventDefault())},Z=function(n){if(x.current){x.current=!1;var i=U(n);e.onSlideEnd&&e.onSlideEnd({originalEvent:n,value:i}),z.current=void 0,de(),me(),he(),Se()}},De=function(n,i){ce(),fe(),J(n,i)},Ee=function(n,i){n.changedTouches&&n.changedTouches[0]&&(z.current=n.changedTouches[0].identifier),ge(),be(),J(n,i)},Re=function(n,i){if(!e.disabled){m.current=i;var o=n.key;switch(o){case"ArrowRight":case"ArrowUp":D(n,1);break;case"ArrowLeft":case"ArrowDown":D(n,-1);break;case"PageUp":D(n,10),n.preventDefault();break;case"PageDown":D(n,-10),n.preventDefault();break;case"Home":D(n,-d),n.preventDefault();break;case"End":D(n,e.max),n.preventDefault();break}}},we=function(n){if(!e.disabled){if(!w.current){V();var i=U(n);e.onSlideEnd&&e.onSlideEnd({originalEvent:n,value:i})}w.current=!1}},V=function(){var n=g.current.getBoundingClientRect();B.current=n.left+M.getWindowScrollLeft(),Y.current=n.top+M.getWindowScrollTop(),X.current=g.current.offsetWidth,H.current=g.current.offsetHeight},Pe=function(n){var i,o=Array.from((i=n.changedTouches)!==null&&i!==void 0?i:[]).find(function(c){return c.identifier===z.current})||n;return{pageX:o.pageX,pageY:o.pageY}},U=function(n){var i,o=Pe(n),c=o.pageX,b=o.pageY;if(!(!c||!b)){y?i=(c-B.current)*100/X.current:i=(Y.current+H.current-b)*100/H.current;var p=(e.max-e.min)*(i/100)+e.min;if(e.step){var f=e.range?d[m.current]:d,L=p-f;L<0?p=f+Math.ceil(p/e.step-f/e.step)*e.step:L>0&&(p=f+Math.floor(p/e.step-f/e.step)*e.step)}else p=Math.floor(p);return ee(n,p)}},ee=function(n,i){var o=parseFloat(i.toFixed(10)),c=o;return e.range?(m.current===0?o<e.min?o=e.min:o>e.max&&(o=e.max):o>e.max?o=e.max:o<e.min&&(o=e.min),c=Ke(d),c[m.current]=o,e.onChange&&e.onChange({originalEvent:n,value:c})):(o<e.min?o=e.min:o>e.max&&(o=e.max),c=o,e.onChange&&e.onChange({originalEvent:n,value:c})),c},E=function(n,i,o){n=I.isEmpty(n)?null:n,i=I.isEmpty(i)?null:i;var c={transition:x.current?"none":null,left:n!=null?n+"%":null,bottom:i!=null?i+"%":null},b=h(S({className:A("handle",{index:o,handleIndex:m}),style:S(S({},N("handle",{dragging:x,leftValue:n,bottomValue:i})),c),tabIndex:e.tabIndex,role:"slider",onMouseDown:function(f){return De(f,o)},onTouchStart:function(f){return Ee(f,o)},onKeyDown:function(f){return Re(f,o)},"aria-valuemin":e.min,"aria-valuemax":e.max,"aria-valuenow":n||i||0,"aria-orientation":e.orientation},je),j("handle"));return u.createElement("span",b)},_e=function(){var n=(d[0]<e.min?e.min:d[0]-e.min)*100/(e.max-e.min),i=(d[1]>e.max?e.max:d[1]-e.min)*100/(e.max-e.min),o=y?E(n,null,0):E(null,n,0),c=y?E(i,null,1):E(null,i,1),b=i>n?i-n:n-i,p=i>n?n:i,f=y?{left:p+"%",width:b+"%"}:{bottom:p+"%",height:b+"%"},L=h({className:A("range"),style:S(S({},N("range")),f)},j("range"));return u.createElement(u.Fragment,null,u.createElement("span",L),o,c)},Oe=function(){var n;d<e.min?n=e.min:d>e.max?n=e.max:n=(d-e.min)*100/(e.max-e.min);var i=y?{width:n+"%"}:{height:n+"%"},o=y?E(n,null,null):E(null,n,null),c=h({className:A("range"),style:S(S({},N("range")),i)},j("range"));return u.createElement(u.Fragment,null,u.createElement("span",c),o)};u.useImperativeHandle(t,function(){return{props:e,getElement:function(){return g.current}}});var Te=_.getOtherProps(e),je=I.reduceKeys(Te,M.ARIA_PROPS),Ae=e.range?_e():Oe(),Le=h({style:e.style,className:k(e.className,A("root",{vertical:ue,horizontal:y})),onClick:we},_.getOtherProps(e),j("root"));return u.createElement("div",K({id:e.id,ref:g},Le),Ae)}));le.displayName="Slider";export{le as default};
