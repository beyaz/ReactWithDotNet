import{a as X}from"./chunk-RLEXGT7I.js";import"./chunk-YZIUSMWY.js";import"./chunk-Q4HHYYXZ.js";import"./chunk-35DEAE55.js";import{A as N,B as W,a as A,b,d as G,m as J,v as Q}from"./chunk-MY5M2XOA.js";import"./chunk-DLL45UCJ.js";import{a as me}from"./chunk-2W22VGCX.js";import{h as de}from"./chunk-GYULANB4.js";var c=de(me());function K(){return K=Object.assign?Object.assign.bind():function(P){for(var m=1;m<arguments.length;m++){var d=arguments[m];for(var r in d)({}).hasOwnProperty.call(d,r)&&(P[r]=d[r])}return P},K.apply(null,arguments)}var ge={root:function(m){var d=m.props,r=m.context;return A("p-inputmask",{"p-filled":d.filled,"p-invalid":d.invalid,"p-variant-filled":d.variant?d.variant==="filled":r&&r.inputStyle==="filled"})}},j=W.extend({defaultProps:{__TYPE:"InputMask",autoClear:!0,autoFocus:!1,className:null,disabled:!1,id:null,mask:null,maxLength:null,invalid:!1,variant:null,name:null,onBlur:null,onChange:null,onComplete:null,onFocus:null,placeholder:null,readOnly:!1,required:!1,size:null,slotChar:"_",style:null,tabIndex:null,tooltip:null,tooltipOptions:null,type:"text",unmask:!1,value:null,children:void 0},css:{classes:ge}}),ee=c.memo(c.forwardRef(function(P,m){var d=c.useContext(J),r=j.getProps(P,d),a=c.useRef(null),k=c.useRef(null),w=c.useRef(0),s=c.useRef([]),f=c.useRef([]),v=c.useRef(0),B=c.useRef(null),T=c.useRef(!1),E=c.useRef(null),re=c.useRef(null),x=c.useRef(null),I=c.useRef(null),L=c.useRef(null),z=c.useRef(!1),q={props:r},te=j.setMetaData(q),ne=te.cx,p=function(e,n){var t,u,l,o=a.current;return!o||!o.offsetParent||o!==document.activeElement?null:(typeof e=="number"?(u=e,l=typeof n=="number"?n:u,o.setSelectionRange?o.setSelectionRange(u,l):o.createTextRange&&(t=o.createTextRange(),t.collapse(!0),t.moveEnd("character",l),t.moveStart("character",u),t.select())):o.setSelectionRange?(u=o.selectionStart,l=o.selectionEnd):document.selection&&document.selection.createRange&&(t=document.selection.createRange(),u=0-t.duplicate().moveStart("character",-1e5),l=u+t.text.length),{begin:u,end:l})},D=function(){for(var e=k.current;e<=w.current;e++)if(s.current[e]&&f.current[e]===g(e))return!1;return!0},g=c.useCallback(function(i){return i<r.slotChar.length?r.slotChar.charAt(i):r.slotChar.charAt(0)},[r.slotChar]),F=function(){return r.unmask?O():a.current&&a.current.value},C=function(e){for(;++e<v.current&&!s.current[e];);return e},ue=function(e){for(;--e>=0&&!s.current[e];);return e},U=function(e,n){var t,u;if(!(e<0)){for(t=e,u=C(n);t<v.current;t++)if(s.current[t]){if(u<v.current&&s.current[t].test(f.current[u]))f.current[t]=f.current[u],f.current[u]=g(u);else break;u=C(u)}y(),p(Math.max(k.current,e))}},ae=function(e){var n,t,u,l;for(n=e,t=g(e);n<v.current;n++)if(s.current[n])if(u=C(n),l=f.current[n],f.current[n]=t,u<v.current&&s.current[u].test(l))t=l;else break},le=function(e){var n=a.current.value,t=p();if(t){if(B.current.length&&B.current.length>n.length){for(h(!0);t.begin>0&&!s.current[t.begin-1];)t.begin--;if(t.begin===0)for(;t.begin<k.current&&!s.current[t.begin];)t.begin++;p(t.begin,t.begin)}else{for(h(!0);t.begin<v.current&&!s.current[t.begin];)t.begin++;p(t.begin,t.begin)}r.onComplete&&D()&&r.onComplete({originalEvent:e,value:F()}),R(e)}},H=function(e){if(T.current=!1,h(),R(e),S(),r.onBlur&&r.onBlur(e),a.current.value!==E.current){var n=document.createEvent("HTMLEvents");n.initEvent("change",!0,!1),a.current.dispatchEvent(n)}},ce=function(e){if(!r.readOnly){var n=e.which||e.keyCode,t,u,l;if(B.current=a.current.value,n===8||n===46||b.isIOS()&&n===127){if(t=p(),!t)return;u=t.begin,l=t.end,l-u===0&&(u=n!==46?ue(u):l=C(u-1),l=n===46?C(l):l),M(u,l),U(u,l-1),R(e),e.preventDefault()}else n===13?(H(e),R(e)):n===27&&(a.current.value=E.current,p(0,h()),R(e),e.preventDefault())}},ie=function(e){if(!r.readOnly){var n=p();if(n){var t=e.which||e.keyCode,u,l,o,$;if(!(e.ctrlKey||e.altKey||e.metaKey||t<32)){if(t&&t!==13){if(n.end-n.begin!==0&&(M(n.begin,n.end),U(n.begin,n.end-1)),u=C(n.begin-1),u<v.current&&(l=String.fromCharCode(t),s.current[u].test(l))){if(ae(u),f.current[u]=l,y(),o=C(u),b.isAndroid()){var pe=function(){p(o)};setTimeout(pe,0)}else p(o);n.begin<=w.current&&($=D())}e.preventDefault()}R(e),r.onComplete&&$&&r.onComplete({originalEvent:e,value:F()})}}}},M=function(e,n){var t;for(t=e;t<n&&t<v.current;t++)s.current[t]&&(f.current[t]=g(t))},y=function(){a.current&&(a.current.value=f.current.join(""))},h=function(e){re.current=!0;var n=a.current&&a.current.value,t=-1,u,l,o;for(u=0,o=0;u<v.current;u++)if(s.current[u]){for(f.current[u]=g(u);o++<n.length;)if(l=n.charAt(o-1),s.current[u].test(l)){f.current[u]=l,t=u;break}if(o>n.length){M(u+1,v.current);break}}else f.current[u]===n.charAt(o)&&o++,u<x.current&&(t=u);return e?y():t+1<x.current?r.autoClear||f.current.join("")===I.current?(a.current&&a.current.value&&(a.current.value=""),M(0,v.current)):y():(y(),a.current&&(a.current.value=a.current.value.substring(0,t+1))),x.current?u:k.current},oe=function(e){if(!r.readOnly){T.current=!0,clearTimeout(L.current);var n;a.current?E.current=a.current.value:E.current="",n=h()||0,L.current=setTimeout(function(){a.current===document.activeElement&&(y(),n===r.mask.replace("?","").length?p(0,n):p(n),S())},100),r.onFocus&&r.onFocus(e)}},se=function(e){z.current?le(e):Z(e)},Z=function(e){var n=arguments.length>1&&arguments[1]!==void 0?arguments[1]:!1;if(!r.readOnly){if(!n){var t=h(!0);p(t)}R(e),r.onComplete&&D()&&r.onComplete({originalEvent:e,value:F()})}},O=c.useCallback(function(){for(var i=[],e=0;e<f.current.length;e++){var n=f.current[e];s.current[e]&&n!==g(e)&&i.push(n)}return i.join("")},[g]),R=function(e){if(r.onChange){var n=r.unmask?O():e&&e.target.value;r.onChange({originalEvent:e,value:I.current!==n?n:"",stopPropagation:function(){e.stopPropagation()},preventDefault:function(){e.preventDefault()},target:{name:r.name,id:r.id,value:I.current!==n?n:""}})}},S=function(){a.current&&a.current.value&&a.current.value.length>0?b.addClass(a.current,"p-filled"):b.removeClass(a.current,"p-filled")},V=function(e){var n;return a.current&&(r.value==null?a.current.value="":(a.current.value=r.value,n=h(e),setTimeout(function(){if(a.current)return y(),h(e)},10)),E.current=a.current.value),S(),n},Y=c.useCallback(function(){return r.unmask?r.value!==O():I.current!==a.current.value&&a.current.value!==r.value},[r.unmask,r.value,O]),_=function(){if(r.mask){s.current=[],x.current=r.mask.length,v.current=r.mask.length,k.current=null;var e={9:"[0-9]",a:"[A-Za-z]","*":"[A-Za-z0-9]"};z.current=b.isChrome()&&b.isAndroid();for(var n=r.mask.split(""),t=0;t<n.length;t++){var u=n[t];u==="?"?(v.current--,x.current=t):e[u]?(s.current.push(new RegExp(e[u])),k.current===null&&(k.current=s.current.length-1),t<x.current&&(w.current=s.current.length-1)):s.current.push(null)}f.current=[];for(var l=0;l<n.length;l++){var o=n[l];o!=="?"&&(e[o]?f.current.push(g(l)):f.current.push(o))}I.current=f.current.join("")}};c.useImperativeHandle(m,function(){return{props:r,focus:function(){return b.focus(a.current)},getElement:function(){return a.current}}}),c.useEffect(function(){G.combinedRefs(a,m)},[a,m]),Q(function(){_(),V()}),N(function(){_(),p(V(!0)),r.unmask&&R()},[r.mask]),N(function(){Y()&&V()},[Y]);var fe=j.getOtherProps(r),ve=A(r.className,ne("root",{context:d}));return c.createElement(X,K({ref:a,autoFocus:r.autoFocus,id:r.id,type:r.type,name:r.name,style:r.style,className:ve,value:r.value},fe,{placeholder:r.placeholder,size:r.size,maxLength:r.maxLength,tabIndex:r.tabIndex,disabled:r.disabled,invalid:r.invalid,readOnly:r.readOnly,onFocus:oe,onBlur:H,onKeyDown:ce,onKeyPress:ie,onInput:se,onPaste:function(e){return Z(e,!0)},required:r.required,tooltip:r.tooltip,tooltipOptions:r.tooltipOptions,pt:r.pt,unstyled:r.unstyled,__parentMetadata:{parent:q}}))}));ee.displayName="InputMask";export{ee as default};