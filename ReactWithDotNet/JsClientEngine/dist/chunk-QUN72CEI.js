import{B as ue,C as be,a as U,b as c,d as _,f as ve,m as me,q as ae,u as ye,v as he}from"./chunk-MY5M2XOA.js";import{a as Ve}from"./chunk-2W22VGCX.js";import{h as Qe}from"./chunk-GYULANB4.js";var i=Qe(Ve());function oe(){return oe=Object.assign?Object.assign.bind():function(t){for(var n=1;n<arguments.length;n++){var a=arguments[n];for(var l in a)({}).hasOwnProperty.call(a,l)&&(t[l]=a[l])}return t},oe.apply(null,arguments)}function se(t,n){(n==null||n>t.length)&&(n=t.length);for(var a=0,l=Array(n);a<n;a++)l[a]=t[a];return l}function Ze(t){if(Array.isArray(t))return se(t)}function et(t){if(typeof Symbol!="undefined"&&t[Symbol.iterator]!=null||t["@@iterator"]!=null)return Array.from(t)}function Pe(t,n){if(t){if(typeof t=="string")return se(t,n);var a={}.toString.call(t).slice(8,-1);return a==="Object"&&t.constructor&&(a=t.constructor.name),a==="Map"||a==="Set"?Array.from(t):a==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(a)?se(t,n):void 0}}function tt(){throw new TypeError(`Invalid attempt to spread non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function Se(t){return Ze(t)||et(t)||Pe(t)||tt()}function L(t){"@babel/helpers - typeof";return L=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(n){return typeof n}:function(n){return n&&typeof Symbol=="function"&&n.constructor===Symbol&&n!==Symbol.prototype?"symbol":typeof n},L(t)}function rt(t,n){if(L(t)!="object"||!t)return t;var a=t[Symbol.toPrimitive];if(a!==void 0){var l=a.call(t,n||"default");if(L(l)!="object")return l;throw new TypeError("@@toPrimitive must return a primitive value.")}return(n==="string"?String:Number)(t)}function nt(t){var n=rt(t,"string");return L(n)=="symbol"?n:n+""}function at(t,n,a){return(n=nt(n))in t?Object.defineProperty(t,n,{value:a,enumerable:!0,configurable:!0,writable:!0}):t[n]=a,t}function ut(t){if(Array.isArray(t))return t}function it(t,n){var a=t==null?null:typeof Symbol!="undefined"&&t[Symbol.iterator]||t["@@iterator"];if(a!=null){var l,u,D,p,d=[],m=!0,f=!1;try{if(D=(a=a.call(t)).next,n===0){if(Object(a)!==a)return;m=!1}else for(;!(m=(l=D.call(a)).done)&&(d.push(l.value),d.length!==n);m=!0);}catch(x){f=!0,u=x}finally{try{if(!m&&a.return!=null&&(p=a.return(),Object(p)!==p))return}finally{if(f)throw u}}return d}}function ot(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function B(t,n){return ut(t)||it(t,n)||Pe(t,n)||ot()}var st={root:function(n){var a=n.props;return U("p-splitter p-component p-splitter-".concat(a.layout))},gutter:"p-splitter-gutter",gutterHandler:"p-splitter-gutter-handle",panel:{root:"p-splitter-panel"}},lt=`
@layer primereact {
    .p-splitter {
        display: flex;
        flex-wrap: nowrap;
    }

    .p-splitter-vertical {
        flex-direction: column;
    }

    .p-splitter-panel {
        flex-grow: 1;
    }

    .p-splitter-panel-nested {
        display: flex;
    }

    .p-splitter-panel .p-splitter {
        flex-grow: 1;
        border: 0 none;
    }

    .p-splitter-gutter {
        flex-grow: 0;
        flex-shrink: 0;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: col-resize;
    }

    .p-splitter-horizontal.p-splitter-resizing {
        cursor: col-resize;
        user-select: none;
    }

    .p-splitter-horizontal > .p-splitter-gutter > .p-splitter-gutter-handle {
        height: 24px;
        width: 100%;
    }

    .p-splitter-horizontal > .p-splitter-gutter {
        cursor: col-resize;
    }

    .p-splitter-vertical.p-splitter-resizing {
        cursor: row-resize;
        user-select: none;
    }

    .p-splitter-vertical > .p-splitter-gutter {
        cursor: row-resize;
    }

    .p-splitter-vertical > .p-splitter-gutter > .p-splitter-gutter-handle {
        width: 24px;
        height: 100%;
    }
}

`,W=ue.extend({defaultProps:{__TYPE:"Splitter",className:null,gutterSize:4,id:null,step:5,layout:"horizontal",onResizeEnd:null,stateKey:null,stateStorage:"session",style:null,children:void 0},css:{classes:st,styles:lt}}),Y=ue.extend({defaultProps:{__TYPE:"SplitterPanel",className:null,minSize:null,size:null,style:null,children:void 0},getCProps:function(n){return _.getComponentProps(n,Y.defaultProps)},getCOtherProps:function(n){return _.getComponentDiffProps(n,Y.defaultProps)},getCProp:function(n,a){return _.getComponentProp(n,a,Y.defaultProps)}});function Re(t,n){var a=Object.keys(t);if(Object.getOwnPropertySymbols){var l=Object.getOwnPropertySymbols(t);n&&(l=l.filter(function(u){return Object.getOwnPropertyDescriptor(t,u).enumerable})),a.push.apply(a,l)}return a}function ie(t){for(var n=1;n<arguments.length;n++){var a=arguments[n]!=null?arguments[n]:{};n%2?Re(Object(a),!0).forEach(function(l){at(t,l,a[l])}):Object.getOwnPropertyDescriptors?Object.defineProperties(t,Object.getOwnPropertyDescriptors(a)):Re(Object(a)).forEach(function(l){Object.defineProperty(t,l,Object.getOwnPropertyDescriptor(a,l))})}return t}var ct=function(){},pt=i.memo(i.forwardRef(function(t,n){var a=ye(),l=i.useContext(me),u=W.getProps(t,l),D=i.useRef(""),p=i.useRef(null),d=i.useRef(),m=i.useRef({}),f=i.useRef(null),x=i.useRef(null),I=i.useRef(null),b=i.useRef(null),S=i.useRef(null),z=i.useRef(null),T=i.useRef(null),N=i.useRef(null),w=i.useRef(null),k=i.useRef(null),v=i.useRef(null),A=i.useRef(null),ze=i.useState([]),le=B(ze,2),C=le[0],X=le[1],we=i.useState(!1),ce=B(we,2),Ee=ce[0],Oe=ce[1],$=u.stateKey!=null,pe=u.children&&u.children.length||1,F=function(e,r){return r in e?e[r]:u.children&&[].concat(u.children)[r].props.size||100/pe},g=u.layout==="horizontal",_e={props:u,state:{panelSizes:C,nested:c.getAttribute(p.current&&p.current.parentElement,"data-p-splitter-panel-nested")===!0}},J=W.setMetaData(ie({},_e)),H=J.ptm,K=J.cx,E=J.isUnstyled;be(W.css.styles,E,{name:"splitter"});var De=function(e){return H(e,{context:{nested:Ee}})},Te=ae({type:"mousemove",listener:function(e){return V(e)}}),fe=B(Te,2),Ae=fe[0],Ce=fe[1],je=ae({type:"mouseup",listener:function(e){Z(e),Ie()}}),ge=B(je,2),Me=ge[0],Le=ge[1],xe=function(){Ae(),Me()},Ie=function(){Ce(),Le()},h=function(e,r){return Y.getCProp(e,r)},Ne=function(e,r){return!(e>100||e<0||r>100||r<0||u.children[v.current].props&&u.children[v.current].props.minSize&&u.children[v.current].props.minSize>e||u.children[v.current+1].props&&u.children[v.current+1].props.minSize&&u.children[v.current+1].props.minSize>r)},ke=function(){x.current=!1,f.current=null,I.current=null,b.current=null,S.current=null,z.current=null,N.current=null,w.current=null,k.current=null,v.current=null},q=i.useCallback(function(){switch(u.stateStorage){case"local":return window.localStorage;case"session":return window.sessionStorage;default:throw new Error(u.stateStorage+' is not a valid value for the state storage, supported values are "local" and "session".')}},[u.stateStorage]),He=function(e){_.isArray(e)&&q().setItem(u.stateKey,JSON.stringify(e))},de=i.useCallback(function(){var o=q().getItem(u.stateKey);o&&X(JSON.parse(o))},[q,u.stateKey]),Q=function(e,r,s){var R=e.type==="touchstart"?e.touches[0].pageX:e.pageX,P=e.type==="touchstart"?e.touches[0].pageY:e.pageY;d.current=m.current[r],f.current=g?c.getWidth(p.current):c.getHeight(p.current),x.current=!0,I.current=g?R:P,b.current=d.current.previousElementSibling,S.current=d.current.nextElementSibling,s?(z.current=g?c.getOuterWidth(b.current,!0):c.getOuterHeight(b.current,!0),w.current=g?c.getOuterWidth(S.current,!0):c.getOuterHeight(S.current,!0)):(z.current=100*(g?c.getOuterWidth(b.current,!0):c.getOuterHeight(b.current,!0))/f.current,w.current=100*(g?c.getOuterWidth(S.current,!0):c.getOuterHeight(S.current,!0))/f.current),N.current=z.current,k.current=w.current,v.current=r,!E()&&c.addClass(d.current,"p-splitter-gutter-resizing"),d.current.setAttribute("data-p-splitter-gutter-resizing",!0),!E()&&c.addClass(p.current,"p-splitter-resizing"),p.current.setAttribute("data-p-splitter-resizing",!0)},V=function(e){var r=arguments.length>1&&arguments[1]!==void 0?arguments[1]:0,s=arguments.length>2&&arguments[2]!==void 0?arguments[2]:!1,R,P,O,re=e.type==="touchmove"?e.touches[0].pageX:e.pageX,ne=e.type==="touchmove"?e.touches[0].pageY:e.pageY;s?g?(O=100*(z.current+r)/f.current,P=100*(w.current-r)/f.current):(O=100*(z.current-r)/f.current,P=100*(w.current+r)/f.current):(g?R=re*100/f.current-I.current*100/f.current:R=ne*100/f.current-I.current*100/f.current,O=z.current+R,P=w.current-R),j(v.current,O,P)},Z=function(e){var r=Se(C);r[v.current]=N.current,r[v.current+1]=k.current,u.onResizeEnd&&u.onResizeEnd({originalEvent:e,sizes:r}),$&&He(r),X(r),!E()&&c.removeClass(d.current,"p-splitter-gutter-resizing"),m.current&&Object.keys(m.current).forEach(function(s){return m.current[s].setAttribute("data-p-splitter-gutter-resizing",!1)}),!E()&&c.removeClass(p.current,"p-splitter-resizing"),p.current.setAttribute("data-p-splitter-resizing",!1),ke()},Ke=function(){Be(),Z()},Ge=function(e,r){var s=u.children[r].props&&u.children[r].props.minSize||0;switch(e.code){case"ArrowLeft":{g&&G(e,r,u.step*-1),e.preventDefault();break}case"ArrowRight":{g&&G(e,r,u.step),e.preventDefault();break}case"ArrowDown":{g||G(e,r,u.step*-1),e.preventDefault();break}case"ArrowUp":{g||G(e,r,u.step),e.preventDefault();break}case"Home":{j(r,100-s,s),e.preventDefault();break}case"End":{j(r,s,100-s),e.preventDefault();break}case"NumpadEnter":case"Enter":{T.current>=100-(s||5)?j(r,s,100-s):j(r,100-s,s),e.preventDefault();break}}},j=function(e,r,s){v.current=e,d.current=m.current[e],f.current=g?c.getWidth(p.current):c.getHeight(p.current),b.current=d.current.previousElementSibling,S.current=d.current.nextElementSibling,Ne(r,s)&&(N.current=r,k.current=s,b.current.style.flexBasis="calc("+r+"% - "+(u.children.length-1)*u.gutterSize+"px)",S.current.style.flexBasis="calc("+s+"% - "+(u.children.length-1)*u.gutterSize+"px)",T.current=parseFloat(r).toFixed(4))},Ue=function(e,r,s){Q(e,r,!0),V(e,s,!0)},G=function(e,r,s){A.current||(A.current=setInterval(function(){Ue(e,r,s)},40))},Be=function(){A.current&&(clearInterval(A.current),A.current=null)},We=function(e,r){Q(e,r,!1),xe()},Ye=function(e,r){Q(e,r,!1),window.addEventListener("touchmove",ee,{passive:!1,cancelable:!1}),window.addEventListener("touchend",te)},ee=function(e){V(e)},te=function(e){Z(e),window.removeEventListener("touchmove",ee),window.removeEventListener("touchend",te)};i.useImperativeHandle(n,function(){return{props:u,getElement:function(){return p.current}}}),he(function(){p.current&&(D.current=ve())}),i.useEffect(function(){var o=Se(p.current.children).filter(function(r){return c.getAttribute(r,"data-pc-section")==="splitterpanel.root"}),e=[];o.map(function(r,s){T.current=F(C,0),e[s]=F(C,s),r.childNodes&&_.isNotEmpty(c.find(r,"[data-pc-name='splitter']")&&c.find(r,"[data-pc-section='root']"))&&(!E()&&c.addClass(r,"p-splitter-panel-nested"),r.setAttribute("data-p-splitter-panel-nested",!0),Oe(!0))}),X(e)},[]),i.useEffect(function(){$&&de()},[de,$]);var Xe=function(e,r){var s=h(e,"id")||"".concat(D.current,"_").concat(r),R=U(h(e,"className"),K("panel.root")),P=a({ref:function(y){return m.current[r]=y},className:K("gutter"),style:g?{width:u.gutterSize+"px"}:{height:u.gutterSize+"px"},onMouseDown:function(y){return We(y,r)},onKeyDown:function(y){return Ge(y,r)},onKeyUp:Ke,onTouchStart:function(y){return Ye(y,r)},onTouchMove:function(y){return ee(y)},onTouchEnd:function(y){return te(y)},"data-p-splitter-gutter-resizing":!1},H("gutter")),O=a({tabIndex:h(e,"tabIndex")||0,className:K("gutterHandler"),role:"separator","aria-orientation":g?"vertical":"horizontal","aria-controls":s,"aria-label":h(e,"aria-label"),"aria-labelledby":h(e,"aria-labelledby"),"aria-valuenow":T.current,"aria-valuetext":parseFloat(T.current).toFixed(0)+"%","aria-valuemin":h(e,"minSize")||"0","aria-valuemax":"100"},H("gutterHandler")),re=r!==u.children.length-1&&i.createElement("div",P,i.createElement("div",O)),ne="calc("+F(C,r)+"% - "+(pe-1)*u.gutterSize+"px)",qe=a({key:r,id:s,className:R,style:ie(ie({},h(e,"style")),{},{flexBasis:ne}),role:"presentation","data-p-splitter-panel-nested":!1,onClick:h(e,"onClick")},De("splitterpanel.root"));return i.createElement(i.Fragment,null,i.createElement("div",qe,h(e,"children")),re)},$e=function(){return i.Children.map(u.children,Xe)},Fe=a({id:u.id,style:u.style,className:U(u.className,K("root")),"data-p-splitter-resizing":!1},W.getOtherProps(u),H("root")),Je=$e();return i.createElement("div",oe({ref:p},Fe),Je)}));ct.displayName="SplitterPanel";pt.displayName="Splitter";export{ct as a,pt as b};
