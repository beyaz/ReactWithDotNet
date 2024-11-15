import{a as H}from"./chunk-JNIGDCU5.js";import{c as xe}from"./chunk-UCYY7ZYD.js";import{a as ue}from"./chunk-2RGZXOVO.js";import{c as ge,e as Me,f as ae}from"./chunk-LX5LFF4Z.js";import{a as be,e as Pe,f as ee,v as _,y as te}from"./chunk-5XWBU6XQ.js";import{a as u}from"./chunk-SORH75JF.js";import{n as v}from"./chunk-742UFDVP.js";import{a as le}from"./chunk-G37S2DMD.js";import{a as Z}from"./chunk-2W22VGCX.js";import{a as $,b as q,d as Q,h as z,j as Re}from"./chunk-GYULANB4.js";var g=z(Z());var oe=z(Z());var ce=class r{constructor(){Re(this,"mountEffect",()=>{this.shouldMount&&!this.didMount&&this.ref.current!==null&&(this.didMount=!0,this.mounted.resolve())});this.ref={current:null},this.mounted=null,this.didMount=!1,this.shouldMount=!1,this.setShouldMount=null}static create(){return new r}static use(){let s=ge(r.create).current,[t,l]=oe.useState(!1);return s.shouldMount=t,s.setShouldMount=l,oe.useEffect(s.mountEffect,[t]),s}mount(){return this.mounted||(this.mounted=qe(),this.shouldMount=!0,this.setShouldMount(this.shouldMount)),this.mounted}start(...s){this.mount().then(()=>{var t;return(t=this.ref.current)==null?void 0:t.start(...s)})}stop(...s){this.mount().then(()=>{var t;return(t=this.ref.current)==null?void 0:t.stop(...s)})}pulsate(...s){this.mount().then(()=>{var t;return(t=this.ref.current)==null?void 0:t.pulsate(...s)})}};function se(){return ce.use()}function qe(){let r,s,t=new Promise((l,h)=>{r=l,s=h});return t.resolve=r,t.reject=s,t}var n=z(Z());var re=z(Z());var fe=z(le());function _e(r){let{className:s,classes:t,pulsate:l=!1,rippleX:h,rippleY:i,rippleSize:f,in:w,onExited:m,timeout:p}=r,[y,T]=re.useState(!1),d=u(s,t.ripple,t.rippleVisible,l&&t.ripplePulsate),N={width:f,height:f,top:-(f/2)+i,left:-(f/2)+h},a=u(t.child,y&&t.childLeaving,l&&t.childPulsate);return!w&&!y&&T(!0),re.useEffect(()=>{if(!w&&m!=null){let L=setTimeout(m,p);return()=>{clearTimeout(L)}}},[m,w,p]),(0,fe.jsx)("span",{className:d,style:N,children:(0,fe.jsx)("span",{className:a})})}var Ce=_e;var He=ee("MuiTouchRipple",["root","ripple","rippleVisible","ripplePulsate","child","childLeaving","childPulsate"]),c=He;var ne=z(le());var me=550,We=80,Ge=v`
  0% {
    transform: scale(0);
    opacity: 0.1;
  }

  100% {
    transform: scale(1);
    opacity: 0.3;
  }
`,Je=v`
  0% {
    opacity: 1;
  }

  100% {
    opacity: 0;
  }
`,Qe=v`
  0% {
    transform: scale(1);
  }

  50% {
    transform: scale(0.92);
  }

  100% {
    transform: scale(1);
  }
`,Ze=_("span",{name:"MuiTouchRipple",slot:"Root"})({overflow:"hidden",pointerEvents:"none",position:"absolute",zIndex:0,top:0,right:0,bottom:0,left:0,borderRadius:"inherit"}),ve=_(Ce,{name:"MuiTouchRipple",slot:"Ripple"})`
  opacity: 0;
  position: absolute;

  &.${c.rippleVisible} {
    opacity: 0.3;
    transform: scale(1);
    animation-name: ${Ge};
    animation-duration: ${me}ms;
    animation-timing-function: ${({theme:r})=>r.transitions.easing.easeInOut};
  }

  &.${c.ripplePulsate} {
    animation-duration: ${({theme:r})=>r.transitions.duration.shorter}ms;
  }

  & .${c.child} {
    opacity: 1;
    display: block;
    width: 100%;
    height: 100%;
    border-radius: 50%;
    background-color: currentColor;
  }

  & .${c.childLeaving} {
    opacity: 0;
    animation-name: ${Je};
    animation-duration: ${me}ms;
    animation-timing-function: ${({theme:r})=>r.transitions.easing.easeInOut};
  }

  & .${c.childPulsate} {
    position: absolute;
    /* @noflip */
    left: 0px;
    top: 0;
    animation-name: ${Qe};
    animation-duration: 2500ms;
    animation-timing-function: ${({theme:r})=>r.transitions.easing.easeInOut};
    animation-iteration-count: infinite;
    animation-delay: 200ms;
  }
`,et=n.forwardRef(function(s,t){let A=te({props:s,name:"MuiTouchRipple"}),{center:h=!1,classes:i={},className:f}=A,w=Q(A,["center","classes","className"]),[m,p]=n.useState([]),y=n.useRef(0),T=n.useRef(null);n.useEffect(()=>{T.current&&(T.current(),T.current=null)},[m]);let d=n.useRef(!1),N=Me(),a=n.useRef(null),L=n.useRef(null),M=n.useCallback(o=>{let{pulsate:R,rippleX:b,rippleY:V,rippleSize:K,cb:F}=o;p(P=>[...P,(0,ne.jsx)(ve,{classes:{ripple:u(i.ripple,c.ripple),rippleVisible:u(i.rippleVisible,c.rippleVisible),ripplePulsate:u(i.ripplePulsate,c.ripplePulsate),child:u(i.child,c.child),childLeaving:u(i.childLeaving,c.childLeaving),childPulsate:u(i.childPulsate,c.childPulsate)},timeout:me,pulsate:R,rippleX:b,rippleY:V,rippleSize:K},y.current)]),y.current+=1,T.current=F},[i]),I=n.useCallback((o={},R={},b=()=>{})=>{let{pulsate:V=!1,center:K=h||R.pulsate,fakeElement:F=!1}=R;if((o==null?void 0:o.type)==="mousedown"&&d.current){d.current=!1;return}(o==null?void 0:o.type)==="touchstart"&&(d.current=!0);let P=F?null:L.current,D=P?P.getBoundingClientRect():{width:0,height:0,left:0,top:0},x,k,S;if(K||o===void 0||o.clientX===0&&o.clientY===0||!o.clientX&&!o.touches)x=Math.round(D.width/2),k=Math.round(D.height/2);else{let{clientX:j,clientY:C}=o.touches&&o.touches.length>0?o.touches[0]:o;x=Math.round(j-D.left),k=Math.round(C-D.top)}if(K)S=Math.sqrt((2*D.width**2+D.height**2)/3),S%2===0&&(S+=1);else{let j=Math.max(Math.abs((P?P.clientWidth:0)-x),x)*2+2,C=Math.max(Math.abs((P?P.clientHeight:0)-k),k)*2+2;S=Math.sqrt(j**2+C**2)}o!=null&&o.touches?a.current===null&&(a.current=()=>{M({pulsate:V,rippleX:x,rippleY:k,rippleSize:S,cb:b})},N.start(We,()=>{a.current&&(a.current(),a.current=null)})):M({pulsate:V,rippleX:x,rippleY:k,rippleSize:S,cb:b})},[h,M,N]),W=n.useCallback(()=>{I({},{pulsate:!0})},[I]),O=n.useCallback((o,R)=>{if(N.clear(),(o==null?void 0:o.type)==="touchend"&&a.current){a.current(),a.current=null,N.start(0,()=>{O(o,R)});return}a.current=null,p(b=>b.length>0?b.slice(1):b),T.current=R},[N]);return n.useImperativeHandle(t,()=>({pulsate:W,start:I,stop:O}),[W,I,O]),(0,ne.jsx)(Ze,q($({className:u(c.root,i.root,f),ref:L},w),{children:(0,ne.jsx)(xe,{component:null,exit:!0,children:m})}))}),Be=et;function Ee(r){return Pe("MuiButtonBase",r)}var tt=ee("MuiButtonBase",["root","disabled","focusVisible"]),Ne=tt;var ie=z(le());var ot=r=>{let{disabled:s,focusVisible:t,focusVisibleClassName:l,classes:h}=r,f=be({root:["root",s&&"disabled",t&&"focusVisible"]},Ee,h);return t&&l&&(f.root+=` ${l}`),f},st=_("button",{name:"MuiButtonBase",slot:"Root",overridesResolver:(r,s)=>s.root})({display:"inline-flex",alignItems:"center",justifyContent:"center",position:"relative",boxSizing:"border-box",WebkitTapHighlightColor:"transparent",backgroundColor:"transparent",outline:0,border:0,margin:0,borderRadius:0,padding:0,cursor:"pointer",userSelect:"none",verticalAlign:"middle",MozAppearance:"none",WebkitAppearance:"none",textDecoration:"none",color:"inherit","&::-moz-focus-inner":{borderStyle:"none"},[`&.${Ne.disabled}`]:{pointerEvents:"none",cursor:"default"},"@media print":{colorAdjust:"exact"}}),rt=g.forwardRef(function(s,t){let l=te({props:s,name:"MuiButtonBase"}),he=l,{action:h,centerRipple:i=!1,children:f,className:w,component:m="button",disabled:p=!1,disableRipple:y=!1,disableTouchRipple:T=!1,focusRipple:d=!1,focusVisibleClassName:N,LinkComponent:a="a",onBlur:L,onClick:M,onContextMenu:I,onDragLeave:W,onFocus:O,onFocusVisible:A,onKeyDown:o,onKeyUp:R,onMouseDown:b,onMouseLeave:V,onMouseUp:K,onTouchEnd:F,onTouchMove:P,onTouchStart:D,tabIndex:x=0,TouchRippleProps:k,touchRippleRef:S,type:j}=he,C=Q(he,["action","centerRipple","children","className","component","disabled","disableRipple","disableTouchRipple","focusRipple","focusVisibleClassName","LinkComponent","onBlur","onClick","onContextMenu","onDragLeave","onFocus","onFocusVisible","onKeyDown","onKeyUp","onMouseDown","onMouseLeave","onMouseUp","onTouchEnd","onTouchMove","onTouchStart","tabIndex","TouchRippleProps","touchRippleRef","type"]),X=g.useRef(null),B=se(),De=ue(B.ref,S),[U,G]=g.useState(!1);p&&U&&G(!1),g.useImperativeHandle(h,()=>({focusVisible:()=>{G(!0),X.current.focus()}}),[]);let ke=B.shouldMount&&!y&&!p;g.useEffect(()=>{U&&d&&!y&&B.pulsate()},[y,d,U,B]);function E(e,ye,Ye=T){return H(Te=>(ye&&ye(Te),Ye||B[e](Te),!0))}let Se=E("start",b),we=E("stop",I),Le=E("stop",W),Ve=E("stop",K),je=E("stop",e=>{U&&e.preventDefault(),V&&V(e)}),Ue=E("start",D),$e=E("stop",F),ze=E("stop",P),Ie=E("stop",e=>{ae(e.target)||G(!1),L&&L(e)},!1),Oe=H(e=>{X.current||(X.current=e.currentTarget),ae(e.target)&&(G(!0),A&&A(e)),O&&O(e)}),pe=()=>{let e=X.current;return m&&m!=="button"&&!(e.tagName==="A"&&e.href)},Ke=H(e=>{d&&!e.repeat&&U&&e.key===" "&&B.stop(e,()=>{B.start(e)}),e.target===e.currentTarget&&pe()&&e.key===" "&&e.preventDefault(),o&&o(e),e.target===e.currentTarget&&pe()&&e.key==="Enter"&&!p&&(e.preventDefault(),M&&M(e))}),Ae=H(e=>{d&&e.key===" "&&U&&!e.defaultPrevented&&B.stop(e,()=>{B.pulsate(e)}),R&&R(e),M&&e.target===e.currentTarget&&pe()&&e.key===" "&&!e.defaultPrevented&&M(e)}),J=m;J==="button"&&(C.href||C.to)&&(J=a);let Y={};J==="button"?(Y.type=j===void 0?"button":j,Y.disabled=p):(!C.href&&!C.to&&(Y.role="button"),p&&(Y["aria-disabled"]=p));let Fe=ue(t,X),de=q($({},l),{centerRipple:i,component:m,disabled:p,disableRipple:y,disableTouchRipple:T,focusRipple:d,tabIndex:x,focusVisible:U}),Xe=ot(de);return(0,ie.jsxs)(st,q($($({as:J,className:u(Xe.root,w),ownerState:de,onBlur:Ie,onClick:M,onContextMenu:we,onFocus:Oe,onKeyDown:Ke,onKeyUp:Ae,onMouseDown:Se,onMouseLeave:je,onMouseUp:Ve,onDragLeave:Le,onTouchEnd:$e,onTouchMove:ze,onTouchStart:Ue,ref:Fe,tabIndex:p?-1:x,type:j},Y),C),{children:[f,ke?(0,ie.jsx)(Be,$({ref:De,center:i},k)):null]}))}),nt=rt;export{nt as a};
