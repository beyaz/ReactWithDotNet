import{a as Ne}from"./chunk-35DEAE55.js";import{A as W,B as Ae,C as Me,a as q,b as h,d as Oe,g as O,m as Re,n as J,p as xe,s as Pe,t as Ce,u as De,v as je,w as Ie,x as Le}from"./chunk-MY5M2XOA.js";import{a as ft}from"./chunk-2W22VGCX.js";import{h as pt}from"./chunk-GYULANB4.js";var f=pt(ft());function B(){return B=Object.assign?Object.assign.bind():function(t){for(var o=1;o<arguments.length;o++){var n=arguments[o];for(var c in n)({}).hasOwnProperty.call(n,c)&&(t[c]=n[c])}return t},B.apply(null,arguments)}function R(t){"@babel/helpers - typeof";return R=typeof Symbol=="function"&&typeof Symbol.iterator=="symbol"?function(o){return typeof o}:function(o){return o&&typeof Symbol=="function"&&o.constructor===Symbol&&o!==Symbol.prototype?"symbol":typeof o},R(t)}function dt(t,o){if(R(t)!="object"||!t)return t;var n=t[Symbol.toPrimitive];if(n!==void 0){var c=n.call(t,o||"default");if(R(c)!="object")return c;throw new TypeError("@@toPrimitive must return a primitive value.")}return(o==="string"?String:Number)(t)}function vt(t){var o=dt(t,"string");return R(o)=="symbol"?o:o+""}function He(t,o,n){return(o=vt(o))in t?Object.defineProperty(t,o,{value:n,enumerable:!0,configurable:!0,writable:!0}):t[o]=n,t}function Q(t,o){(o==null||o>t.length)&&(o=t.length);for(var n=0,c=Array(o);n<o;n++)c[n]=t[n];return c}function mt(t){if(Array.isArray(t))return Q(t)}function ht(t){if(typeof Symbol!="undefined"&&t[Symbol.iterator]!=null||t["@@iterator"]!=null)return Array.from(t)}function ke(t,o){if(t){if(typeof t=="string")return Q(t,o);var n={}.toString.call(t).slice(8,-1);return n==="Object"&&t.constructor&&(n=t.constructor.name),n==="Map"||n==="Set"?Array.from(t):n==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)?Q(t,o):void 0}}function yt(){throw new TypeError(`Invalid attempt to spread non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function bt(t){return mt(t)||ht(t)||ke(t)||yt()}function gt(t){if(Array.isArray(t))return t}function Et(t,o){var n=t==null?null:typeof Symbol!="undefined"&&t[Symbol.iterator]||t["@@iterator"];if(n!=null){var c,i,x,S,y=[],w=!0,P=!1;try{if(x=(n=n.call(t)).next,o===0){if(Object(n)!==n)return;w=!1}else for(;!(w=(c=x.call(n)).done)&&(y.push(c.value),y.length!==o);w=!0);}catch(C){P=!0,i=C}finally{try{if(!w&&n.return!=null&&(S=n.return(),Object(S)!==S))return}finally{if(P)throw i}}return y}}function wt(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function T(t,o){return gt(t)||Et(t,o)||ke(t,o)||wt()}var St={root:function(o){var n=o.positionState,c=o.classNameState;return q("p-tooltip p-component",He({},"p-tooltip-".concat(n),!0),c)},arrow:"p-tooltip-arrow",text:"p-tooltip-text"},Tt={arrow:function(o){var n=o.context;return{top:n.bottom?"0":n.right||n.left||!n.right&&!n.left&&!n.top&&!n.bottom?"50%":null,bottom:n.top?"0":null,left:n.right||!n.right&&!n.left&&!n.top&&!n.bottom?"0":n.top||n.bottom?"50%":null,right:n.left?"0":null}}},Ot=`
@layer primereact {
    .p-tooltip {
        position: absolute;
        padding: .25em .5rem;
        /* #3687: Tooltip prevent scrollbar flickering */
        top: -9999px;
        left: -9999px;
    }
    
    .p-tooltip.p-tooltip-right,
    .p-tooltip.p-tooltip-left {
        padding: 0 .25rem;
    }
    
    .p-tooltip.p-tooltip-top,
    .p-tooltip.p-tooltip-bottom {
        padding:.25em 0;
    }
    
    .p-tooltip .p-tooltip-text {
       white-space: pre-line;
       word-break: break-word;
    }
    
    .p-tooltip-arrow {
        position: absolute;
        width: 0;
        height: 0;
        border-color: transparent;
        border-style: solid;
    }
    
    .p-tooltip-right .p-tooltip-arrow {
        top: 50%;
        left: 0;
        margin-top: -.25rem;
        border-width: .25em .25em .25em 0;
    }
    
    .p-tooltip-left .p-tooltip-arrow {
        top: 50%;
        right: 0;
        margin-top: -.25rem;
        border-width: .25em 0 .25em .25rem;
    }
    
    .p-tooltip.p-tooltip-top {
        padding: .25em 0;
    }
    
    .p-tooltip-top .p-tooltip-arrow {
        bottom: 0;
        left: 50%;
        margin-left: -.25rem;
        border-width: .25em .25em 0;
    }
    
    .p-tooltip-bottom .p-tooltip-arrow {
        top: 0;
        left: 50%;
        margin-left: -.25rem;
        border-width: 0 .25em .25rem;
    }

    .p-tooltip-target-wrapper {
        display: inline-flex;
    }
}
`,$=Ae.extend({defaultProps:{__TYPE:"Tooltip",appendTo:null,at:null,autoHide:!0,autoZIndex:!0,baseZIndex:0,className:null,closeOnEscape:!1,content:null,disabled:!1,event:null,hideDelay:0,hideEvent:"mouseleave",id:null,mouseTrack:!1,mouseTrackLeft:5,mouseTrackTop:5,my:null,onBeforeHide:null,onBeforeShow:null,onHide:null,onShow:null,position:"right",showDelay:0,showEvent:"mouseenter",showOnDisabled:!1,style:null,target:null,updateDelay:0,children:void 0},css:{classes:St,styles:Ot,inlineStyles:Tt}});function _e(t,o){var n=Object.keys(t);if(Object.getOwnPropertySymbols){var c=Object.getOwnPropertySymbols(t);o&&(c=c.filter(function(i){return Object.getOwnPropertyDescriptor(t,i).enumerable})),n.push.apply(n,c)}return n}function Rt(t){for(var o=1;o<arguments.length;o++){var n=arguments[o]!=null?arguments[o]:{};o%2?_e(Object(n),!0).forEach(function(c){He(t,c,n[c])}):Object.getOwnPropertyDescriptors?Object.defineProperties(t,Object.getOwnPropertyDescriptors(n)):_e(Object(n)).forEach(function(c){Object.defineProperty(t,c,Object.getOwnPropertyDescriptor(n,c))})}return t}var xt=f.memo(f.forwardRef(function(t,o){var n=De(),c=f.useContext(Re),i=$.getProps(t,c),x=f.useState(!1),S=T(x,2),y=S[0],w=S[1],P=f.useState(i.position||"right"),C=T(P,2),E=C[0],U=C[1],We=f.useState(""),ee=T(We,2),te=ee[0],ne=ee[1],$e=f.useState(!1),re=T($e,2),Be=re[0],oe=re[1],ie={props:i,state:{visible:y,position:E,className:te},context:{right:E==="right",left:E==="left",top:E==="top",bottom:E==="bottom"}},D=$.setMetaData(ie),Z=D.ptm,F=D.cx,Ue=D.sx,Ze=D.isUnstyled;Me($.css.styles,Ze,{name:"tooltip"}),Ce({callback:function(){b()},when:i.closeOnEscape,priority:[Pe.TOOLTIP,0]});var p=f.useRef(null),j=f.useRef(null),d=f.useRef(null),I=f.useRef(null),L=f.useRef(!0),ae=f.useRef({}),le=f.useRef(null),Fe=Le({listener:function(e){!h.isTouchDevice()&&b(e)}}),ue=T(Fe,2),Ke=ue[0],Ye=ue[1],Xe=Ie({target:d.current,listener:function(e){b(e)},when:y}),se=T(Xe,2),Ge=se[0],ze=se[1],Ve=function(e){return!(i.content||v(e,"tooltip"))},qe=function(e){return!(i.content||v(e,"tooltip")||i.children)},K=function(e){return v(e,"mousetrack")||i.mouseTrack},ce=function(e){return v(e,"disabled")==="true"||fe(e,"disabled")||i.disabled},pe=function(e){return v(e,"showondisabled")||i.showOnDisabled},A=function(){return v(d.current,"autohide")||i.autoHide},v=function(e,r){return fe(e,"data-pr-".concat(r))?e.getAttribute("data-pr-".concat(r)):null},fe=function(e,r){return e&&e.hasAttribute(r)},de=function(e){var r=[v(e,"showevent")||i.showEvent],s=[v(e,"hideevent")||i.hideEvent];if(K(e))r=["mousemove"],s=["mouseleave"];else{var l=v(e,"event")||i.event;l==="focus"&&(r=["focus"],s=["blur"]),l==="both"&&(r=["focus","mouseenter"],s=Be?["blur"]:["mouseleave","blur"])}return{showEvents:r,hideEvents:s}},ve=function(e){return v(e,"position")||E},Je=function(e){var r=v(e,"mousetracktop")||i.mouseTrackTop,s=v(e,"mousetrackleft")||i.mouseTrackLeft;return{top:r,left:s}},me=function(e,r){if(j.current){var s=v(e,"tooltip")||i.content;s?(j.current.innerHTML="",j.current.appendChild(document.createTextNode(s)),r()):i.children&&r()}},he=function(e){me(d.current,function(){var r=le.current,s=r.pageX,l=r.pageY;i.autoZIndex&&!O.get(p.current)&&O.set("tooltip",p.current,c&&c.autoZIndex||J.autoZIndex,i.baseZIndex||c&&c.zIndex.tooltip||J.zIndex.tooltip),p.current.style.left="",p.current.style.top="",A()&&(p.current.style.pointerEvents="none");var u=K(d.current)||e==="mouse";(u&&!I.current||u)&&(I.current={width:h.getOuterWidth(p.current),height:h.getOuterHeight(p.current)}),ye(d.current,{x:s,y:l},e)})},M=function(e){e.type&&e.type==="focus"&&oe(!0),d.current=e.currentTarget;var r=ce(d.current),s=qe(pe(d.current)&&r?d.current.firstChild:d.current);if(!(s||r))if(le.current=e,y)N("updateDelay",he);else{var l=_(i.onBeforeShow,{originalEvent:e,target:d.current});l&&N("showDelay",function(){w(!0),_(i.onShow,{originalEvent:e,target:d.current})})}},b=function(e){if(e&&e.type==="blur"&&oe(!1),be(),y){var r=_(i.onBeforeHide,{originalEvent:e,target:d.current});r&&N("hideDelay",function(){!A()&&L.current===!1||(O.clear(p.current),h.removeClass(p.current,"p-tooltip-active"),w(!1),_(i.onHide,{originalEvent:e,target:d.current}))})}},ye=function(e,r,s){var l=0,u=0,m=s||E;if((K(e)||m=="mouse")&&r){var g={width:h.getOuterWidth(p.current),height:h.getOuterHeight(p.current)};l=r.x,u=r.y;var we=Je(e),H=we.top,k=we.left;switch(m){case"left":l=l-(g.width+k),u=u-(g.height/2-H);break;case"right":case"mouse":l=l+k,u=u-(g.height/2-H);break;case"top":l=l-(g.width/2-k),u=u-(g.height+H);break;case"bottom":l=l-(g.width/2-k),u=u+H;break}l<=0||I.current.width>g.width?(p.current.style.left="0px",p.current.style.right=window.innerWidth-g.width-l+"px"):(p.current.style.right="",p.current.style.left=l+"px"),p.current.style.top=u+"px",h.addClass(p.current,"p-tooltip-active")}else{var G=h.findCollisionPosition(m),lt=v(e,"my")||i.my||G.my,ut=v(e,"at")||i.at||G.at;p.current.style.padding="0px",h.flipfitCollision(p.current,e,lt,ut,function(z){var Se=z.at,V=Se.x,st=Se.y,ct=z.my.x,Te=i.at?V!=="center"&&V!==ct?V:st:z.at["".concat(G.axis)];p.current.style.padding="",U(Te),Qe(Te),h.addClass(p.current,"p-tooltip-active")})}},Qe=function(e){if(p.current){var r=getComputedStyle(p.current);e==="left"?p.current.style.left=parseFloat(r.left)-parseFloat(r.paddingLeft)*2+"px":e==="top"&&(p.current.style.top=parseFloat(r.top)-parseFloat(r.paddingTop)*2+"px")}},et=function(){A()||(L.current=!1)},tt=function(e){A()||(L.current=!0,b(e))},nt=function(e){if(e){var r=de(e),s=r.showEvents,l=r.hideEvents,u=ge(e);s.forEach(function(m){return u==null?void 0:u.addEventListener(m,M)}),l.forEach(function(m){return u==null?void 0:u.addEventListener(m,b)})}},rt=function(e){if(e){var r=de(e),s=r.showEvents,l=r.hideEvents,u=ge(e);s.forEach(function(m){return u==null?void 0:u.removeEventListener(m,M)}),l.forEach(function(m){return u==null?void 0:u.removeEventListener(m,b)})}},N=function(e,r){be();var s=v(d.current,e.toLowerCase())||i[e];s?ae.current["".concat(e)]=setTimeout(function(){return r()},s):r()},_=function(e){if(e){for(var r=arguments.length,s=new Array(r>1?r-1:0),l=1;l<r;l++)s[l-1]=arguments[l];var u=e.apply(void 0,s);return u===void 0&&(u=!0),u}return!0},be=function(){Object.values(ae.current).forEach(function(e){return clearTimeout(e)})},ge=function(e){if(e){if(pe(e)){if(!e.hasWrapper){var r=document.createElement("div"),s=e.nodeName==="INPUT";return s?h.addMultipleClasses(r,"p-tooltip-target-wrapper p-inputwrapper"):h.addClass(r,"p-tooltip-target-wrapper"),e.parentNode.insertBefore(r,e),r.appendChild(e),e.hasWrapper=!0,r}return e.parentElement}else if(e.hasWrapper){var l;(l=e.parentElement).replaceWith.apply(l,bt(e.parentElement.childNodes)),delete e.hasWrapper}return e}return null},ot=function(e){X(e),Y(e)},Y=function(e){Ee(e||i.target,nt)},X=function(e){Ee(e||i.target,rt)},Ee=function(e,r){if(e=Oe.getRefElement(e),e)if(h.isElement(e))r(e);else{var s=function(u){var m=h.find(document,u);m.forEach(function(g){r(g)})};e instanceof Array?e.forEach(function(l){s(l)}):s(e)}};je(function(){y&&d.current&&ce(d.current)&&b()}),W(function(){return Y(),function(){X()}},[M,b,i.target]),W(function(){if(y){var a=ve(d.current),e=v(d.current,"classname");U(a),ne(e),he(a),Ke(),Ge()}else U(i.position||"right"),ne(""),d.current=null,I.current=null,L.current=!0;return function(){Ye(),ze()}},[y]),W(function(){var a=ve(d.current);y&&a!=="mouse"&&N("updateDelay",function(){me(d.current,function(){ye(d.current)})})},[i.content]),xe(function(){b(),O.clear(p.current)}),f.useImperativeHandle(o,function(){return{props:i,updateTargetEvents:ot,loadTargetEvents:Y,unloadTargetEvents:X,show:M,hide:b,getElement:function(){return p.current},getTarget:function(){return d.current}}});var it=function(){var e=Ve(d.current),r=n({id:i.id,className:q(i.className,F("root",{positionState:E,classNameState:te})),style:i.style,role:"tooltip","aria-hidden":y,onMouseEnter:function(m){return et()},onMouseLeave:function(m){return tt(m)}},$.getOtherProps(i),Z("root")),s=n({className:F("arrow"),style:Ue("arrow",Rt({},ie))},Z("arrow")),l=n({className:F("text")},Z("text"));return f.createElement("div",B({ref:p},r),f.createElement("div",s),f.createElement("div",B({ref:j},l),e&&i.children))};if(y){var at=it();return f.createElement(Ne,{element:at,appendTo:i.appendTo,visible:!0})}return null}));xt.displayName="Tooltip";export{xt as a};
