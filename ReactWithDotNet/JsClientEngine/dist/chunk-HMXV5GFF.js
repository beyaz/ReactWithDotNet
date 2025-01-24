import{a as H}from"./chunk-JNIGDCU5.js";import{c as ge}from"./chunk-UCYY7ZYD.js";import{a as ue}from"./chunk-2RGZXOVO.js";import{c as be,e as Pe,f as ae}from"./chunk-LX5LFF4Z.js";import{a as Te,e as Re,f as ee,s as _,v as te}from"./chunk-HI6IROIK.js";import{a as m}from"./chunk-SORH75JF.js";import{n as v}from"./chunk-3P3XDPDG.js";import{a as le}from"./chunk-G37S2DMD.js";import{a as Z}from"./chunk-2W22VGCX.js";import{a as $,b as q,d as Q,h as z,j as ye}from"./chunk-GYULANB4.js";var M=z(Z());var oe=z(Z());var ce=class r{constructor(){ye(this,"mountEffect",()=>{this.shouldMount&&!this.didMount&&this.ref.current!==null&&(this.didMount=!0,this.mounted.resolve())});this.ref={current:null},this.mounted=null,this.didMount=!1,this.shouldMount=!1,this.setShouldMount=null}static create(){return new r}static use(){let s=be(r.create).current,[t,l]=oe.useState(!1);return s.shouldMount=t,s.setShouldMount=l,oe.useEffect(s.mountEffect,[t]),s}mount(){return this.mounted||(this.mounted=Fe(),this.shouldMount=!0,this.setShouldMount(this.shouldMount)),this.mounted}start(...s){this.mount().then(()=>{var t;return(t=this.ref.current)==null?void 0:t.start(...s)})}stop(...s){this.mount().then(()=>{var t;return(t=this.ref.current)==null?void 0:t.stop(...s)})}pulsate(...s){this.mount().then(()=>{var t;return(t=this.ref.current)==null?void 0:t.pulsate(...s)})}};function se(){return ce.use()}function Fe(){let r,s,t=new Promise((l,c)=>{r=l,s=c});return t.resolve=r,t.reject=s,t}var n=z(Z());var re=z(Z());var fe=z(le());function Xe(r){let{className:s,classes:t,pulsate:l=!1,rippleX:c,rippleY:a,rippleSize:h,in:w,onExited:y,timeout:u}=r,[R,i]=re.useState(!1),T=m(s,t.ripple,t.rippleVisible,l&&t.ripplePulsate),N={width:h,height:h,top:-(h/2)+a,left:-(h/2)+c},f=m(t.child,R&&t.childLeaving,l&&t.childPulsate);return!w&&!R&&i(!0),re.useEffect(()=>{if(!w&&y!=null){let L=setTimeout(y,u);return()=>{clearTimeout(L)}}},[y,w,u]),(0,fe.jsx)("span",{className:T,style:N,children:(0,fe.jsx)("span",{className:f})})}var Me=Xe;var Ye=ee("MuiTouchRipple",["root","ripple","rippleVisible","ripplePulsate","child","childLeaving","childPulsate"]),d=Ye;var ne=z(le());var me=550,qe=80,_e=v`
  0% {
    transform: scale(0);
    opacity: 0.1;
  }

  100% {
    transform: scale(1);
    opacity: 0.3;
  }
`,He=v`
  0% {
    opacity: 1;
  }

  100% {
    opacity: 0;
  }
`,We=v`
  0% {
    transform: scale(1);
  }

  50% {
    transform: scale(0.92);
  }

  100% {
    transform: scale(1);
  }
`,Ge=_("span",{name:"MuiTouchRipple",slot:"Root"})({overflow:"hidden",pointerEvents:"none",position:"absolute",zIndex:0,top:0,right:0,bottom:0,left:0,borderRadius:"inherit"}),Je=_(Me,{name:"MuiTouchRipple",slot:"Ripple"})`
  opacity: 0;
  position: absolute;

  &.${d.rippleVisible} {
    opacity: 0.3;
    transform: scale(1);
    animation-name: ${_e};
    animation-duration: ${me}ms;
    animation-timing-function: ${({theme:r})=>r.transitions.easing.easeInOut};
  }

  &.${d.ripplePulsate} {
    animation-duration: ${({theme:r})=>r.transitions.duration.shorter}ms;
  }

  & .${d.child} {
    opacity: 1;
    display: block;
    width: 100%;
    height: 100%;
    border-radius: 50%;
    background-color: currentColor;
  }

  & .${d.childLeaving} {
    opacity: 0;
    animation-name: ${He};
    animation-duration: ${me}ms;
    animation-timing-function: ${({theme:r})=>r.transitions.easing.easeInOut};
  }

  & .${d.childPulsate} {
    position: absolute;
    /* @noflip */
    left: 0px;
    top: 0;
    animation-name: ${We};
    animation-duration: 2500ms;
    animation-timing-function: ${({theme:r})=>r.transitions.easing.easeInOut};
    animation-iteration-count: infinite;
    animation-delay: 200ms;
  }
`,Qe=n.forwardRef(function(s,t){let A=te({props:s,name:"MuiTouchRipple"}),{center:c=!1,classes:a={},className:h}=A,w=Q(A,["center","classes","className"]),[y,u]=n.useState([]),R=n.useRef(0),i=n.useRef(null);n.useEffect(()=>{i.current&&(i.current(),i.current=null)},[y]);let T=n.useRef(!1),N=Pe(),f=n.useRef(null),L=n.useRef(null),x=n.useCallback(o=>{let{pulsate:b,rippleX:P,rippleY:V,rippleSize:K,cb:F}=o;u(g=>[...g,(0,ne.jsx)(Je,{classes:{ripple:m(a.ripple,d.ripple),rippleVisible:m(a.rippleVisible,d.rippleVisible),ripplePulsate:m(a.ripplePulsate,d.ripplePulsate),child:m(a.child,d.child),childLeaving:m(a.childLeaving,d.childLeaving),childPulsate:m(a.childPulsate,d.childPulsate)},timeout:me,pulsate:b,rippleX:P,rippleY:V,rippleSize:K},R.current)]),R.current+=1,i.current=F},[a]),I=n.useCallback((o={},b={},P=()=>{})=>{let{pulsate:V=!1,center:K=c||b.pulsate,fakeElement:F=!1}=b;if((o==null?void 0:o.type)==="mousedown"&&T.current){T.current=!1;return}(o==null?void 0:o.type)==="touchstart"&&(T.current=!0);let g=F?null:L.current,D=g?g.getBoundingClientRect():{width:0,height:0,left:0,top:0},C,k,S;if(K||o===void 0||o.clientX===0&&o.clientY===0||!o.clientX&&!o.touches)C=Math.round(D.width/2),k=Math.round(D.height/2);else{let{clientX:j,clientY:B}=o.touches&&o.touches.length>0?o.touches[0]:o;C=Math.round(j-D.left),k=Math.round(B-D.top)}if(K)S=Math.sqrt((2*D.width**2+D.height**2)/3),S%2===0&&(S+=1);else{let j=Math.max(Math.abs((g?g.clientWidth:0)-C),C)*2+2,B=Math.max(Math.abs((g?g.clientHeight:0)-k),k)*2+2;S=Math.sqrt(j**2+B**2)}o!=null&&o.touches?f.current===null&&(f.current=()=>{x({pulsate:V,rippleX:C,rippleY:k,rippleSize:S,cb:P})},N.start(qe,()=>{f.current&&(f.current(),f.current=null)})):x({pulsate:V,rippleX:C,rippleY:k,rippleSize:S,cb:P})},[c,x,N]),W=n.useCallback(()=>{I({},{pulsate:!0})},[I]),O=n.useCallback((o,b)=>{if(N.clear(),(o==null?void 0:o.type)==="touchend"&&f.current){f.current(),f.current=null,N.start(0,()=>{O(o,b)});return}f.current=null,u(P=>P.length>0?P.slice(1):P),i.current=b},[N]);return n.useImperativeHandle(t,()=>({pulsate:W,start:I,stop:O}),[W,I,O]),(0,ne.jsx)(Ge,q($({className:m(d.root,a.root,h),ref:L},w),{children:(0,ne.jsx)(ge,{component:null,exit:!0,children:y})}))}),xe=Qe;function Ce(r){return Re("MuiButtonBase",r)}var Ze=ee("MuiButtonBase",["root","disabled","focusVisible"]),Be=Ze;var ie=z(le());var ve=r=>{let{disabled:s,focusVisible:t,focusVisibleClassName:l,classes:c}=r,h=Te({root:["root",s&&"disabled",t&&"focusVisible"]},Ce,c);return t&&l&&(h.root+=` ${l}`),h},et=_("button",{name:"MuiButtonBase",slot:"Root",overridesResolver:(r,s)=>s.root})({display:"inline-flex",alignItems:"center",justifyContent:"center",position:"relative",boxSizing:"border-box",WebkitTapHighlightColor:"transparent",backgroundColor:"transparent",outline:0,border:0,margin:0,borderRadius:0,padding:0,cursor:"pointer",userSelect:"none",verticalAlign:"middle",MozAppearance:"none",WebkitAppearance:"none",textDecoration:"none",color:"inherit","&::-moz-focus-inner":{borderStyle:"none"},[`&.${Be.disabled}`]:{pointerEvents:"none",cursor:"default"},"@media print":{colorAdjust:"exact"}}),tt=M.forwardRef(function(s,t){let l=te({props:s,name:"MuiButtonBase"}),he=l,{action:c,centerRipple:a=!1,children:h,className:w,component:y="button",disabled:u=!1,disableRipple:R=!1,disableTouchRipple:i=!1,focusRipple:T=!1,focusVisibleClassName:N,LinkComponent:f="a",onBlur:L,onClick:x,onContextMenu:I,onDragLeave:W,onFocus:O,onFocusVisible:A,onKeyDown:o,onKeyUp:b,onMouseDown:P,onMouseLeave:V,onMouseUp:K,onTouchEnd:F,onTouchMove:g,onTouchStart:D,tabIndex:C=0,TouchRippleProps:k,touchRippleRef:S,type:j}=he,B=Q(he,["action","centerRipple","children","className","component","disabled","disableRipple","disableTouchRipple","focusRipple","focusVisibleClassName","LinkComponent","onBlur","onClick","onContextMenu","onDragLeave","onFocus","onFocusVisible","onKeyDown","onKeyUp","onMouseDown","onMouseLeave","onMouseUp","onTouchEnd","onTouchMove","onTouchStart","tabIndex","TouchRippleProps","touchRippleRef","type"]),X=M.useRef(null),p=se(),Ee=ue(p.ref,S),[U,G]=M.useState(!1);u&&U&&G(!1),M.useImperativeHandle(c,()=>({focusVisible:()=>{G(!0),X.current.focus()}}),[]);let Ne=p.shouldMount&&!R&&!u;M.useEffect(()=>{U&&T&&!R&&p.pulsate()},[R,T,U,p]);let De=E(p,"start",P,i),ke=E(p,"stop",I,i),Se=E(p,"stop",W,i),we=E(p,"stop",K,i),Le=E(p,"stop",e=>{U&&e.preventDefault(),V&&V(e)},i),Ve=E(p,"start",D,i),je=E(p,"stop",F,i),Ue=E(p,"stop",g,i),$e=E(p,"stop",e=>{ae(e.target)||G(!1),L&&L(e)},!1),ze=H(e=>{X.current||(X.current=e.currentTarget),ae(e.target)&&(G(!0),A&&A(e)),O&&O(e)}),pe=()=>{let e=X.current;return y&&y!=="button"&&!(e.tagName==="A"&&e.href)},Ie=H(e=>{T&&!e.repeat&&U&&e.key===" "&&p.stop(e,()=>{p.start(e)}),e.target===e.currentTarget&&pe()&&e.key===" "&&e.preventDefault(),o&&o(e),e.target===e.currentTarget&&pe()&&e.key==="Enter"&&!u&&(e.preventDefault(),x&&x(e))}),Oe=H(e=>{T&&e.key===" "&&U&&!e.defaultPrevented&&p.stop(e,()=>{p.pulsate(e)}),b&&b(e),x&&e.target===e.currentTarget&&pe()&&e.key===" "&&!e.defaultPrevented&&x(e)}),J=y;J==="button"&&(B.href||B.to)&&(J=f);let Y={};J==="button"?(Y.type=j===void 0?"button":j,Y.disabled=u):(!B.href&&!B.to&&(Y.role="button"),u&&(Y["aria-disabled"]=u));let Ke=ue(t,X),de=q($({},l),{centerRipple:a,component:y,disabled:u,disableRipple:R,disableTouchRipple:i,focusRipple:T,tabIndex:C,focusVisible:U}),Ae=ve(de);return(0,ie.jsxs)(et,q($($({as:J,className:m(Ae.root,w),ownerState:de,onBlur:$e,onClick:x,onContextMenu:ke,onFocus:ze,onKeyDown:Ie,onKeyUp:Oe,onMouseDown:De,onMouseLeave:Le,onMouseUp:we,onDragLeave:Se,onTouchEnd:je,onTouchMove:Ue,onTouchStart:Ve,ref:Ke,tabIndex:u?-1:C,type:j},Y),B),{children:[h,Ne?(0,ie.jsx)(xe,$({ref:Ee,center:a},k)):null]}))});function E(r,s,t,l=!1){return H(c=>(t&&t(c),l||r[s](c),!0))}var ot=tt;export{ot as a};
