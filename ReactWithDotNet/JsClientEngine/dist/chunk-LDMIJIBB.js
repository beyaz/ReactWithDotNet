import{a as bt,i as at,j as ot,k as st,m as it,n as e,o as N}from"./chunk-3P3XDPDG.js";import{a as xt}from"./chunk-G37S2DMD.js";import{a as L}from"./chunk-2W22VGCX.js";import{a as d,b as k,d as w,f as nt,h as C}from"./chunk-GYULANB4.js";var dt=nt(c=>{"use strict";var Q=Symbol.for("react.element"),tt=Symbol.for("react.portal"),F=Symbol.for("react.fragment"),M=Symbol.for("react.strict_mode"),U=Symbol.for("react.profiler"),E=Symbol.for("react.provider"),T=Symbol.for("react.context"),zt=Symbol.for("react.server_context"),A=Symbol.for("react.forward_ref"),J=Symbol.for("react.suspense"),P=Symbol.for("react.suspense_list"),q=Symbol.for("react.memo"),W=Symbol.for("react.lazy"),Ct=Symbol.for("react.offscreen"),lt;lt=Symbol.for("react.module.reference");function x(t){if(typeof t=="object"&&t!==null){var r=t.$$typeof;switch(r){case Q:switch(t=t.type,t){case F:case U:case M:case J:case P:return t;default:switch(t=t&&t.$$typeof,t){case zt:case T:case A:case W:case q:case E:return t;default:return r}}case tt:return r}}}c.ContextConsumer=T;c.ContextProvider=E;c.Element=Q;c.ForwardRef=A;c.Fragment=F;c.Lazy=W;c.Memo=q;c.Portal=tt;c.Profiler=U;c.StrictMode=M;c.Suspense=J;c.SuspenseList=P;c.isAsyncMode=function(){return!1};c.isConcurrentMode=function(){return!1};c.isContextConsumer=function(t){return x(t)===T};c.isContextProvider=function(t){return x(t)===E};c.isElement=function(t){return typeof t=="object"&&t!==null&&t.$$typeof===Q};c.isForwardRef=function(t){return x(t)===A};c.isFragment=function(t){return x(t)===F};c.isLazy=function(t){return x(t)===W};c.isMemo=function(t){return x(t)===q};c.isPortal=function(t){return x(t)===tt};c.isProfiler=function(t){return x(t)===U};c.isStrictMode=function(t){return x(t)===M};c.isSuspense=function(t){return x(t)===J};c.isSuspenseList=function(t){return x(t)===P};c.isValidElementType=function(t){return typeof t=="string"||typeof t=="function"||t===F||t===U||t===M||t===J||t===P||t===Ct||typeof t=="object"&&t!==null&&(t.$$typeof===W||t.$$typeof===q||t.$$typeof===E||t.$$typeof===T||t.$$typeof===A||t.$$typeof===lt||t.getModuleId!==void 0)};c.typeOf=x});var ut=nt((je,pt)=>{"use strict";pt.exports=dt()});var Y=C(xt());var xe=C(L());var Re=C(bt());var ct=Y.Fragment,f=function(r,n,a){return at.call(n,"css")?Y.jsx(st,ot(r,n),a):Y.jsx(r,n,a)};var h=C(L(),1);var _=C(L(),1),z=C(L(),1);var vt=Object.defineProperty,Ot=(t,r,n)=>r in t?vt(t,r,{enumerable:!0,configurable:!0,writable:!0,value:n}):t[r]=n,B=(t,r,n)=>Ot(t,typeof r!="symbol"?r+"":r,n),Z=new Map,D=new WeakMap,ft=0,wt=void 0;function Rt(t){return t?(D.has(t)||(ft+=1,D.set(t,ft.toString())),D.get(t)):"0"}function It(t){return Object.keys(t).sort().filter(r=>t[r]!==void 0).map(r=>`${r}_${r==="root"?Rt(t.root):t[r]}`).toString()}function St(t){let r=It(t),n=Z.get(r);if(!n){let a=new Map,s,o=new IntersectionObserver(i=>{i.forEach(m=>{var p;let y=m.isIntersecting&&s.some(g=>m.intersectionRatio>=g);t.trackVisibility&&typeof m.isVisible=="undefined"&&(m.isVisible=y),(p=a.get(m.target))==null||p.forEach(g=>{g(y,m)})})},t);s=o.thresholds||(Array.isArray(t.threshold)?t.threshold:[t.threshold||0]),n={id:r,observer:o,elements:a},Z.set(r,n)}return n}function mt(t,r,n={},a=wt){if(typeof window.IntersectionObserver=="undefined"&&a!==void 0){let p=t.getBoundingClientRect();return r(a,{isIntersecting:a,target:t,intersectionRatio:typeof n.threshold=="number"?n.threshold:0,time:0,boundingClientRect:p,intersectionRect:p,rootBounds:p}),()=>{}}let{id:s,observer:o,elements:i}=St(n),m=i.get(t)||[];return i.has(t)||i.set(t,m),m.push(r),o.observe(t),function(){m.splice(m.indexOf(r),1),m.length===0&&(i.delete(t),o.unobserve(t)),i.size===0&&(o.disconnect(),Z.delete(s))}}function kt(t){return typeof t.children!="function"}var G=class extends _.Component{constructor(t){super(t),B(this,"node",null),B(this,"_unobserveCb",null),B(this,"handleNode",r=>{this.node&&(this.unobserve(),!r&&!this.props.triggerOnce&&!this.props.skip&&this.setState({inView:!!this.props.initialInView,entry:void 0})),this.node=r||null,this.observeNode()}),B(this,"handleChange",(r,n)=>{r&&this.props.triggerOnce&&this.unobserve(),kt(this.props)||this.setState({inView:r,entry:n}),this.props.onChange&&this.props.onChange(r,n)}),this.state={inView:!!t.initialInView,entry:void 0}}componentDidMount(){this.unobserve(),this.observeNode()}componentDidUpdate(t){(t.rootMargin!==this.props.rootMargin||t.root!==this.props.root||t.threshold!==this.props.threshold||t.skip!==this.props.skip||t.trackVisibility!==this.props.trackVisibility||t.delay!==this.props.delay)&&(this.unobserve(),this.observeNode())}componentWillUnmount(){this.unobserve()}observeNode(){if(!this.node||this.props.skip)return;let{threshold:t,root:r,rootMargin:n,trackVisibility:a,delay:s,fallbackInView:o}=this.props;this._unobserveCb=mt(this.node,this.handleChange,{threshold:t,root:r,rootMargin:n,trackVisibility:a,delay:s},o)}unobserve(){this._unobserveCb&&(this._unobserveCb(),this._unobserveCb=null)}render(){let{children:t}=this.props;if(typeof t=="function"){let{inView:b,entry:l}=this.state;return t({inView:b,entry:l,ref:this.handleNode})}let O=this.props,{as:r,triggerOnce:n,threshold:a,root:s,rootMargin:o,onChange:i,skip:m,trackVisibility:p,delay:y,initialInView:g,fallbackInView:R}=O,v=w(O,["as","triggerOnce","threshold","root","rootMargin","onChange","skip","trackVisibility","delay","initialInView","fallbackInView"]);return _.createElement(r||"div",d({ref:this.handleNode},v),t)}};function K({threshold:t,delay:r,trackVisibility:n,rootMargin:a,root:s,triggerOnce:o,skip:i,initialInView:m,fallbackInView:p,onChange:y}={}){var g;let[R,v]=z.useState(null),O=z.useRef(y),[b,l]=z.useState({inView:!!m,entry:void 0});O.current=y,z.useEffect(()=>{if(i||!R)return;let I;return I=mt(R,(X,H)=>{l({inView:X,entry:H}),O.current&&O.current(X,H),H.isIntersecting&&o&&I&&(I(),I=void 0)},{root:s,rootMargin:a,threshold:t,trackVisibility:n,delay:r},p),()=>{I&&I()}},[Array.isArray(t)?t.toString():t,R,s,a,o,i,n,p,r]);let $=(g=b.entry)==null?void 0:g.target,j=z.useRef(void 0);!R&&$&&!o&&!i&&j.current!==$&&(j.current=$,l({inView:!!m,entry:void 0}));let u=[v,b.inView,b.entry];return u.ref=u[0],u.inView=u[1],u.entry=u[2],u}var yt=C(ut(),1);var Xt=e`
  from,
  20%,
  53%,
  to {
    animation-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
    transform: translate3d(0, 0, 0);
  }

  40%,
  43% {
    animation-timing-function: cubic-bezier(0.755, 0.05, 0.855, 0.06);
    transform: translate3d(0, -30px, 0) scaleY(1.1);
  }

  70% {
    animation-timing-function: cubic-bezier(0.755, 0.05, 0.855, 0.06);
    transform: translate3d(0, -15px, 0) scaleY(1.05);
  }

  80% {
    transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
    transform: translate3d(0, 0, 0) scaleY(0.95);
  }

  90% {
    transform: translate3d(0, -4px, 0) scaleY(1.02);
  }
`,Yt=e`
  from,
  50%,
  to {
    opacity: 1;
  }

  25%,
  75% {
    opacity: 0;
  }
`,$t=e`
  0% {
    transform: translateX(0);
  }

  6.5% {
    transform: translateX(-6px) rotateY(-9deg);
  }

  18.5% {
    transform: translateX(5px) rotateY(7deg);
  }

  31.5% {
    transform: translateX(-3px) rotateY(-5deg);
  }

  43.5% {
    transform: translateX(2px) rotateY(3deg);
  }

  50% {
    transform: translateX(0);
  }
`,jt=e`
  0% {
    transform: scale(1);
  }

  14% {
    transform: scale(1.3);
  }

  28% {
    transform: scale(1);
  }

  42% {
    transform: scale(1.3);
  }

  70% {
    transform: scale(1);
  }
`,Vt=e`
  from,
  11.1%,
  to {
    transform: translate3d(0, 0, 0);
  }

  22.2% {
    transform: skewX(-12.5deg) skewY(-12.5deg);
  }

  33.3% {
    transform: skewX(6.25deg) skewY(6.25deg);
  }

  44.4% {
    transform: skewX(-3.125deg) skewY(-3.125deg);
  }

  55.5% {
    transform: skewX(1.5625deg) skewY(1.5625deg);
  }

  66.6% {
    transform: skewX(-0.78125deg) skewY(-0.78125deg);
  }

  77.7% {
    transform: skewX(0.390625deg) skewY(0.390625deg);
  }

  88.8% {
    transform: skewX(-0.1953125deg) skewY(-0.1953125deg);
  }
`,Lt=e`
  from {
    transform: scale3d(1, 1, 1);
  }

  50% {
    transform: scale3d(1.05, 1.05, 1.05);
  }

  to {
    transform: scale3d(1, 1, 1);
  }
`,Nt=e`
  from {
    transform: scale3d(1, 1, 1);
  }

  30% {
    transform: scale3d(1.25, 0.75, 1);
  }

  40% {
    transform: scale3d(0.75, 1.25, 1);
  }

  50% {
    transform: scale3d(1.15, 0.85, 1);
  }

  65% {
    transform: scale3d(0.95, 1.05, 1);
  }

  75% {
    transform: scale3d(1.05, 0.95, 1);
  }

  to {
    transform: scale3d(1, 1, 1);
  }
`,Bt=e`
  from,
  to {
    transform: translate3d(0, 0, 0);
  }

  10%,
  30%,
  50%,
  70%,
  90% {
    transform: translate3d(-10px, 0, 0);
  }

  20%,
  40%,
  60%,
  80% {
    transform: translate3d(10px, 0, 0);
  }
`,Dt=e`
  from,
  to {
    transform: translate3d(0, 0, 0);
  }

  10%,
  30%,
  50%,
  70%,
  90% {
    transform: translate3d(-10px, 0, 0);
  }

  20%,
  40%,
  60%,
  80% {
    transform: translate3d(10px, 0, 0);
  }
`,_t=e`
  from,
  to {
    transform: translate3d(0, 0, 0);
  }

  10%,
  30%,
  50%,
  70%,
  90% {
    transform: translate3d(0, -10px, 0);
  }

  20%,
  40%,
  60%,
  80% {
    transform: translate3d(0, 10px, 0);
  }
`,Ft=e`
  20% {
    transform: rotate3d(0, 0, 1, 15deg);
  }

  40% {
    transform: rotate3d(0, 0, 1, -10deg);
  }

  60% {
    transform: rotate3d(0, 0, 1, 5deg);
  }

  80% {
    transform: rotate3d(0, 0, 1, -5deg);
  }

  to {
    transform: rotate3d(0, 0, 1, 0deg);
  }
`,Mt=e`
  from {
    transform: scale3d(1, 1, 1);
  }

  10%,
  20% {
    transform: scale3d(0.9, 0.9, 0.9) rotate3d(0, 0, 1, -3deg);
  }

  30%,
  50%,
  70%,
  90% {
    transform: scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, 3deg);
  }

  40%,
  60%,
  80% {
    transform: scale3d(1.1, 1.1, 1.1) rotate3d(0, 0, 1, -3deg);
  }

  to {
    transform: scale3d(1, 1, 1);
  }
`,Ut=e`
  from {
    transform: translate3d(0, 0, 0);
  }

  15% {
    transform: translate3d(-25%, 0, 0) rotate3d(0, 0, 1, -5deg);
  }

  30% {
    transform: translate3d(20%, 0, 0) rotate3d(0, 0, 1, 3deg);
  }

  45% {
    transform: translate3d(-15%, 0, 0) rotate3d(0, 0, 1, -3deg);
  }

  60% {
    transform: translate3d(10%, 0, 0) rotate3d(0, 0, 1, 2deg);
  }

  75% {
    transform: translate3d(-5%, 0, 0) rotate3d(0, 0, 1, -1deg);
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,Et=e`
  from {
    opacity: 0;
  }

  to {
    opacity: 1;
  }
`,Tt=e`
  from {
    opacity: 0;
    transform: translate3d(-100%, 100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,At=e`
  from {
    opacity: 0;
    transform: translate3d(100%, 100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Jt=e`
  from {
    opacity: 0;
    transform: translate3d(0, -100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Pt=e`
  from {
    opacity: 0;
    transform: translate3d(0, -2000px, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,et=e`
  from {
    opacity: 0;
    transform: translate3d(-100%, 0, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,qt=e`
  from {
    opacity: 0;
    transform: translate3d(-2000px, 0, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Wt=e`
  from {
    opacity: 0;
    transform: translate3d(100%, 0, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Ht=e`
  from {
    opacity: 0;
    transform: translate3d(2000px, 0, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Zt=e`
  from {
    opacity: 0;
    transform: translate3d(-100%, -100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Gt=e`
  from {
    opacity: 0;
    transform: translate3d(100%, -100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Kt=e`
  from {
    opacity: 0;
    transform: translate3d(0, 100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Qt=e`
  from {
    opacity: 0;
    transform: translate3d(0, 2000px, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`;function tr({duration:t=1e3,delay:r=0,timingFunction:n="ease",keyframes:a=et,iterationCount:s=1}){return it`
    animation-duration: ${t}ms;
    animation-timing-function: ${n};
    animation-delay: ${r}ms;
    animation-name: ${a};
    animation-direction: normal;
    animation-fill-mode: both;
    animation-iteration-count: ${s};

    @media (prefers-reduced-motion: reduce) {
      animation: none;
    }
  `}function rr(t){return t==null}function er(t){return typeof t=="string"||typeof t=="number"||typeof t=="boolean"}function gt(t,r){return n=>n?t():r()}function V(t){return gt(t,()=>null)}function rt(t){return V(()=>({opacity:0}))(t)}var S=t=>{let{cascade:r=!1,damping:n=.5,delay:a=0,duration:s=1e3,fraction:o=0,keyframes:i=et,triggerOnce:m=!1,className:p,style:y,childClassName:g,childStyle:R,children:v,onVisibilityChange:O}=t,b=(0,h.useMemo)(()=>tr({keyframes:i,duration:s}),[s,i]);return rr(v)?null:er(v)?f(ar,k(d({},t),{animationStyles:b,children:String(v)})):(0,yt.isFragment)(v)?f(ht,k(d({},t),{animationStyles:b})):f(ct,{children:h.Children.map(v,(l,$)=>{if(!(0,h.isValidElement)(l))return null;let j=a+(r?$*s*n:0);switch(l.type){case"ol":case"ul":return f(N,{children:({cx:u})=>f(l.type,k(d({},l.props),{className:u(p,l.props.className),style:Object.assign({},y,l.props.style),children:f(S,k(d({},t),{children:l.props.children}))}))});case"li":return f(G,{threshold:o,triggerOnce:m,onChange:O,children:({inView:u,ref:I})=>f(N,{children:({cx:X})=>f(l.type,k(d({},l.props),{ref:I,className:X(g,l.props.className),css:V(()=>b)(u),style:Object.assign({},R,l.props.style,rt(!u),{animationDelay:j+"ms"})}))})});default:return f(G,{threshold:o,triggerOnce:m,onChange:O,children:({inView:u,ref:I})=>f("div",{ref:I,className:p,css:V(()=>b)(u),style:Object.assign({},y,rt(!u),{animationDelay:j+"ms"}),children:f(N,{children:({cx:X})=>f(l.type,k(d({},l.props),{className:X(g,l.props.className),style:Object.assign({},R,l.props.style)}))})})})}})})},nr={display:"inline-block",whiteSpace:"pre"},ar=t=>{let{animationStyles:r,cascade:n=!1,damping:a=.5,delay:s=0,duration:o=1e3,fraction:i=0,triggerOnce:m=!1,className:p,style:y,children:g,onVisibilityChange:R}=t,{ref:v,inView:O}=K({triggerOnce:m,threshold:i,onChange:R});return gt(()=>f("div",{ref:v,className:p,style:Object.assign({},y,nr),children:g.split("").map((b,l)=>f("span",{css:V(()=>r)(O),style:{animationDelay:s+l*o*a+"ms"},children:b},l))}),()=>f(ht,k(d({},t),{children:g})))(n)},ht=t=>{let{animationStyles:r,fraction:n=0,triggerOnce:a=!1,className:s,style:o,children:i,onVisibilityChange:m}=t,{ref:p,inView:y}=K({triggerOnce:a,threshold:n,onChange:m});return f("div",{ref:p,className:s,css:V(()=>r)(y),style:Object.assign({},o,rt(!y)),children:i})};function or(t){switch(t){case"bounce":return[Xt,{transformOrigin:"center bottom"}];case"flash":return[Yt];case"headShake":return[$t,{animationTimingFunction:"ease-in-out"}];case"heartBeat":return[jt,{animationTimingFunction:"ease-in-out"}];case"jello":return[Vt,{transformOrigin:"center"}];case"pulse":return[Lt,{animationTimingFunction:"ease-in-out"}];case"rubberBand":return[Nt];case"shake":return[Bt];case"shakeX":return[Dt];case"shakeY":return[_t];case"swing":return[Ft,{transformOrigin:"top center"}];case"tada":return[Mt];case"wobble":return[Ut]}}var Be=t=>{let i=t,{effect:r="bounce",style:n}=i,a=w(i,["effect","style"]),[s,o]=(0,h.useMemo)(()=>or(r),[r]);return f(S,d({keyframes:s,style:Object.assign({},n,o)},a))},sr=e`
  from,
  20%,
  40%,
  60%,
  80%,
  to {
    animation-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
  }

  0% {
    opacity: 0;
    transform: scale3d(0.3, 0.3, 0.3);
  }

  20% {
    transform: scale3d(1.1, 1.1, 1.1);
  }

  40% {
    transform: scale3d(0.9, 0.9, 0.9);
  }

  60% {
    opacity: 1;
    transform: scale3d(1.03, 1.03, 1.03);
  }

  80% {
    transform: scale3d(0.97, 0.97, 0.97);
  }

  to {
    opacity: 1;
    transform: scale3d(1, 1, 1);
  }
`,ir=e`
  from,
  60%,
  75%,
  90%,
  to {
    animation-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
  }

  0% {
    opacity: 0;
    transform: translate3d(0, -3000px, 0) scaleY(3);
  }

  60% {
    opacity: 1;
    transform: translate3d(0, 25px, 0) scaleY(0.9);
  }

  75% {
    transform: translate3d(0, -10px, 0) scaleY(0.95);
  }

  90% {
    transform: translate3d(0, 5px, 0) scaleY(0.985);
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,cr=e`
  from,
  60%,
  75%,
  90%,
  to {
    animation-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
  }

  0% {
    opacity: 0;
    transform: translate3d(-3000px, 0, 0) scaleX(3);
  }

  60% {
    opacity: 1;
    transform: translate3d(25px, 0, 0) scaleX(1);
  }

  75% {
    transform: translate3d(-10px, 0, 0) scaleX(0.98);
  }

  90% {
    transform: translate3d(5px, 0, 0) scaleX(0.995);
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,fr=e`
  from,
  60%,
  75%,
  90%,
  to {
    animation-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
  }

  from {
    opacity: 0;
    transform: translate3d(3000px, 0, 0) scaleX(3);
  }

  60% {
    opacity: 1;
    transform: translate3d(-25px, 0, 0) scaleX(1);
  }

  75% {
    transform: translate3d(10px, 0, 0) scaleX(0.98);
  }

  90% {
    transform: translate3d(-5px, 0, 0) scaleX(0.995);
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,mr=e`
  from,
  60%,
  75%,
  90%,
  to {
    animation-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
  }

  from {
    opacity: 0;
    transform: translate3d(0, 3000px, 0) scaleY(5);
  }

  60% {
    opacity: 1;
    transform: translate3d(0, -20px, 0) scaleY(0.9);
  }

  75% {
    transform: translate3d(0, 10px, 0) scaleY(0.95);
  }

  90% {
    transform: translate3d(0, -5px, 0) scaleY(0.985);
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,lr=e`
  20% {
    transform: scale3d(0.9, 0.9, 0.9);
  }

  50%,
  55% {
    opacity: 1;
    transform: scale3d(1.1, 1.1, 1.1);
  }

  to {
    opacity: 0;
    transform: scale3d(0.3, 0.3, 0.3);
  }
`,dr=e`
  20% {
    transform: translate3d(0, 10px, 0) scaleY(0.985);
  }

  40%,
  45% {
    opacity: 1;
    transform: translate3d(0, -20px, 0) scaleY(0.9);
  }

  to {
    opacity: 0;
    transform: translate3d(0, 2000px, 0) scaleY(3);
  }
`,pr=e`
  20% {
    opacity: 1;
    transform: translate3d(20px, 0, 0) scaleX(0.9);
  }

  to {
    opacity: 0;
    transform: translate3d(-2000px, 0, 0) scaleX(2);
  }
`,ur=e`
  20% {
    opacity: 1;
    transform: translate3d(-20px, 0, 0) scaleX(0.9);
  }

  to {
    opacity: 0;
    transform: translate3d(2000px, 0, 0) scaleX(2);
  }
`,yr=e`
  20% {
    transform: translate3d(0, -10px, 0) scaleY(0.985);
  }

  40%,
  45% {
    opacity: 1;
    transform: translate3d(0, 20px, 0) scaleY(0.9);
  }

  to {
    opacity: 0;
    transform: translate3d(0, -2000px, 0) scaleY(3);
  }
`;function gr(t,r){switch(r){case"down":return t?dr:ir;case"left":return t?pr:cr;case"right":return t?ur:fr;case"up":return t?yr:mr;default:return t?lr:sr}}var De=t=>{let o=t,{direction:r,reverse:n=!1}=o,a=w(o,["direction","reverse"]),s=(0,h.useMemo)(()=>gr(n,r),[r,n]);return f(S,d({keyframes:s},a))},hr=e`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
  }
`,br=e`
  from {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }

  to {
    opacity: 0;
    transform: translate3d(-100%, 100%, 0);
  }
`,xr=e`
  from {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }

  to {
    opacity: 0;
    transform: translate3d(100%, 100%, 0);
  }
`,vr=e`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(0, 100%, 0);
  }
`,Or=e`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(0, 2000px, 0);
  }
`,wr=e`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(-100%, 0, 0);
  }
`,Rr=e`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(-2000px, 0, 0);
  }
`,Ir=e`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(100%, 0, 0);
  }
`,Sr=e`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(2000px, 0, 0);
  }
`,kr=e`
  from {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }

  to {
    opacity: 0;
    transform: translate3d(-100%, -100%, 0);
  }
`,zr=e`
  from {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }

  to {
    opacity: 0;
    transform: translate3d(100%, -100%, 0);
  }
`,Cr=e`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(0, -100%, 0);
  }
`,Xr=e`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(0, -2000px, 0);
  }
`;function Yr(t,r,n){switch(n){case"bottom-left":return r?br:Tt;case"bottom-right":return r?xr:At;case"down":return t?r?Or:Pt:r?vr:Jt;case"left":return t?r?Rr:qt:r?wr:et;case"right":return t?r?Sr:Ht:r?Ir:Wt;case"top-left":return r?kr:Zt;case"top-right":return r?zr:Gt;case"up":return t?r?Xr:Qt:r?Cr:Kt;default:return r?hr:Et}}var _e=t=>{let i=t,{big:r=!1,direction:n,reverse:a=!1}=i,s=w(i,["big","direction","reverse"]),o=(0,h.useMemo)(()=>Yr(r,a,n),[r,n,a]);return f(S,d({keyframes:o},s))},$r=e`
  from {
    transform: perspective(400px) scale3d(1, 1, 1) translate3d(0, 0, 0) rotate3d(0, 1, 0, -360deg);
    animation-timing-function: ease-out;
  }

  40% {
    transform: perspective(400px) scale3d(1, 1, 1) translate3d(0, 0, 150px)
      rotate3d(0, 1, 0, -190deg);
    animation-timing-function: ease-out;
  }

  50% {
    transform: perspective(400px) scale3d(1, 1, 1) translate3d(0, 0, 150px)
      rotate3d(0, 1, 0, -170deg);
    animation-timing-function: ease-in;
  }

  80% {
    transform: perspective(400px) scale3d(0.95, 0.95, 0.95) translate3d(0, 0, 0)
      rotate3d(0, 1, 0, 0deg);
    animation-timing-function: ease-in;
  }

  to {
    transform: perspective(400px) scale3d(1, 1, 1) translate3d(0, 0, 0) rotate3d(0, 1, 0, 0deg);
    animation-timing-function: ease-in;
  }
`,jr=e`
  from {
    transform: perspective(400px) rotate3d(1, 0, 0, 90deg);
    animation-timing-function: ease-in;
    opacity: 0;
  }

  40% {
    transform: perspective(400px) rotate3d(1, 0, 0, -20deg);
    animation-timing-function: ease-in;
  }

  60% {
    transform: perspective(400px) rotate3d(1, 0, 0, 10deg);
    opacity: 1;
  }

  80% {
    transform: perspective(400px) rotate3d(1, 0, 0, -5deg);
  }

  to {
    transform: perspective(400px);
  }
`,Vr=e`
  from {
    transform: perspective(400px) rotate3d(0, 1, 0, 90deg);
    animation-timing-function: ease-in;
    opacity: 0;
  }

  40% {
    transform: perspective(400px) rotate3d(0, 1, 0, -20deg);
    animation-timing-function: ease-in;
  }

  60% {
    transform: perspective(400px) rotate3d(0, 1, 0, 10deg);
    opacity: 1;
  }

  80% {
    transform: perspective(400px) rotate3d(0, 1, 0, -5deg);
  }

  to {
    transform: perspective(400px);
  }
`,Lr=e`
  from {
    transform: perspective(400px);
  }

  30% {
    transform: perspective(400px) rotate3d(1, 0, 0, -20deg);
    opacity: 1;
  }

  to {
    transform: perspective(400px) rotate3d(1, 0, 0, 90deg);
    opacity: 0;
  }
`,Nr=e`
  from {
    transform: perspective(400px);
  }

  30% {
    transform: perspective(400px) rotate3d(0, 1, 0, -15deg);
    opacity: 1;
  }

  to {
    transform: perspective(400px) rotate3d(0, 1, 0, 90deg);
    opacity: 0;
  }
`;function Br(t,r){switch(r){case"horizontal":return t?Lr:jr;case"vertical":return t?Nr:Vr;default:return $r}}var Dr={backfaceVisibility:"visible"},Fe=t=>{let i=t,{direction:r,reverse:n=!1,style:a}=i,s=w(i,["direction","reverse","style"]),o=(0,h.useMemo)(()=>Br(n,r),[r,n]);return f(S,d({keyframes:o,style:Object.assign({},a,Dr)},s))},_r=e`
  0% {
    animation-timing-function: ease-in-out;
  }

  20%,
  60% {
    transform: rotate3d(0, 0, 1, 80deg);
    animation-timing-function: ease-in-out;
  }

  40%,
  80% {
    transform: rotate3d(0, 0, 1, 60deg);
    animation-timing-function: ease-in-out;
    opacity: 1;
  }

  to {
    transform: translate3d(0, 700px, 0);
    opacity: 0;
  }
`,Fr=e`
  from {
    opacity: 0;
    transform: scale(0.1) rotate(30deg);
    transform-origin: center bottom;
  }

  50% {
    transform: rotate(-10deg);
  }

  70% {
    transform: rotate(3deg);
  }

  to {
    opacity: 1;
    transform: scale(1);
  }
`,Me=e`
  from {
    opacity: 0;
    transform: translate3d(-100%, 0, 0) rotate3d(0, 0, 1, -120deg);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Ue=e`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(100%, 0, 0) rotate3d(0, 0, 1, 120deg);
  }
`,Mr={transformOrigin:"top left"},Ee=t=>{let a=t,{style:r}=a,n=w(a,["style"]);return f(S,d({keyframes:_r,style:Object.assign({},r,Mr)},n))},Te=t=>f(S,d({keyframes:Fr},t));var Ur=e`
  from {
    transform: rotate3d(0, 0, 1, -200deg);
    opacity: 0;
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
`,Er=e`
  from {
    transform: rotate3d(0, 0, 1, -45deg);
    opacity: 0;
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
`,Tr=e`
  from {
    transform: rotate3d(0, 0, 1, 45deg);
    opacity: 0;
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
`,Ar=e`
  from {
    transform: rotate3d(0, 0, 1, 45deg);
    opacity: 0;
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
`,Jr=e`
  from {
    transform: rotate3d(0, 0, 1, -90deg);
    opacity: 0;
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
`,Pr=e`
  from {
    opacity: 1;
  }

  to {
    transform: rotate3d(0, 0, 1, 200deg);
    opacity: 0;
  }
`,qr=e`
  from {
    opacity: 1;
  }

  to {
    transform: rotate3d(0, 0, 1, 45deg);
    opacity: 0;
  }
`,Wr=e`
  from {
    opacity: 1;
  }

  to {
    transform: rotate3d(0, 0, 1, -45deg);
    opacity: 0;
  }
`,Hr=e`
  from {
    opacity: 1;
  }

  to {
    transform: rotate3d(0, 0, 1, -45deg);
    opacity: 0;
  }
`,Zr=e`
  from {
    opacity: 1;
  }

  to {
    transform: rotate3d(0, 0, 1, 90deg);
    opacity: 0;
  }
`;function Gr(t,r){switch(r){case"bottom-left":return t?[qr,{transformOrigin:"left bottom"}]:[Er,{transformOrigin:"left bottom"}];case"bottom-right":return t?[Wr,{transformOrigin:"right bottom"}]:[Tr,{transformOrigin:"right bottom"}];case"top-left":return t?[Hr,{transformOrigin:"left bottom"}]:[Ar,{transformOrigin:"left bottom"}];case"top-right":return t?[Zr,{transformOrigin:"right bottom"}]:[Jr,{transformOrigin:"right bottom"}];default:return t?[Pr,{transformOrigin:"center"}]:[Ur,{transformOrigin:"center"}]}}var Ae=t=>{let m=t,{direction:r,reverse:n=!1,style:a}=m,s=w(m,["direction","reverse","style"]),[o,i]=(0,h.useMemo)(()=>Gr(n,r),[r,n]);return f(S,d({keyframes:o,style:Object.assign({},a,i)},s))},Kr=e`
  from {
    transform: translate3d(0, -100%, 0);
    visibility: visible;
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,Qr=e`
  from {
    transform: translate3d(-100%, 0, 0);
    visibility: visible;
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,te=e`
  from {
    transform: translate3d(100%, 0, 0);
    visibility: visible;
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,re=e`
  from {
    transform: translate3d(0, 100%, 0);
    visibility: visible;
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,ee=e`
  from {
    transform: translate3d(0, 0, 0);
  }

  to {
    visibility: hidden;
    transform: translate3d(0, 100%, 0);
  }
`,ne=e`
  from {
    transform: translate3d(0, 0, 0);
  }

  to {
    visibility: hidden;
    transform: translate3d(-100%, 0, 0);
  }
`,ae=e`
  from {
    transform: translate3d(0, 0, 0);
  }

  to {
    visibility: hidden;
    transform: translate3d(100%, 0, 0);
  }
`,oe=e`
  from {
    transform: translate3d(0, 0, 0);
  }

  to {
    visibility: hidden;
    transform: translate3d(0, -100%, 0);
  }
`;function se(t,r){switch(r){case"down":return t?ee:Kr;case"right":return t?ae:te;case"up":return t?oe:re;case"left":default:return t?ne:Qr}}var Je=t=>{let o=t,{direction:r,reverse:n=!1}=o,a=w(o,["direction","reverse"]),s=(0,h.useMemo)(()=>se(n,r),[r,n]);return f(S,d({keyframes:s},a))},ie=e`
  from {
    opacity: 0;
    transform: scale3d(0.3, 0.3, 0.3);
  }

  50% {
    opacity: 1;
  }
`,ce=e`
  from {
    opacity: 0;
    transform: scale3d(0.1, 0.1, 0.1) translate3d(0, -1000px, 0);
    animation-timing-function: cubic-bezier(0.55, 0.055, 0.675, 0.19);
  }

  60% {
    opacity: 1;
    transform: scale3d(0.475, 0.475, 0.475) translate3d(0, 60px, 0);
    animation-timing-function: cubic-bezier(0.175, 0.885, 0.32, 1);
  }
`,fe=e`
  from {
    opacity: 0;
    transform: scale3d(0.1, 0.1, 0.1) translate3d(-1000px, 0, 0);
    animation-timing-function: cubic-bezier(0.55, 0.055, 0.675, 0.19);
  }

  60% {
    opacity: 1;
    transform: scale3d(0.475, 0.475, 0.475) translate3d(10px, 0, 0);
    animation-timing-function: cubic-bezier(0.175, 0.885, 0.32, 1);
  }
`,me=e`
  from {
    opacity: 0;
    transform: scale3d(0.1, 0.1, 0.1) translate3d(1000px, 0, 0);
    animation-timing-function: cubic-bezier(0.55, 0.055, 0.675, 0.19);
  }

  60% {
    opacity: 1;
    transform: scale3d(0.475, 0.475, 0.475) translate3d(-10px, 0, 0);
    animation-timing-function: cubic-bezier(0.175, 0.885, 0.32, 1);
  }
`,le=e`
  from {
    opacity: 0;
    transform: scale3d(0.1, 0.1, 0.1) translate3d(0, 1000px, 0);
    animation-timing-function: cubic-bezier(0.55, 0.055, 0.675, 0.19);
  }

  60% {
    opacity: 1;
    transform: scale3d(0.475, 0.475, 0.475) translate3d(0, -60px, 0);
    animation-timing-function: cubic-bezier(0.175, 0.885, 0.32, 1);
  }
`,de=e`
  from {
    opacity: 1;
  }

  50% {
    opacity: 0;
    transform: scale3d(0.3, 0.3, 0.3);
  }

  to {
    opacity: 0;
  }
`,pe=e`
  40% {
    opacity: 1;
    transform: scale3d(0.475, 0.475, 0.475) translate3d(0, -60px, 0);
    animation-timing-function: cubic-bezier(0.55, 0.055, 0.675, 0.19);
  }

  to {
    opacity: 0;
    transform: scale3d(0.1, 0.1, 0.1) translate3d(0, 2000px, 0);
    animation-timing-function: cubic-bezier(0.175, 0.885, 0.32, 1);
  }
`,ue=e`
  40% {
    opacity: 1;
    transform: scale3d(0.475, 0.475, 0.475) translate3d(42px, 0, 0);
  }

  to {
    opacity: 0;
    transform: scale(0.1) translate3d(-2000px, 0, 0);
  }
`,ye=e`
  40% {
    opacity: 1;
    transform: scale3d(0.475, 0.475, 0.475) translate3d(-42px, 0, 0);
  }

  to {
    opacity: 0;
    transform: scale(0.1) translate3d(2000px, 0, 0);
  }
`,ge=e`
  40% {
    opacity: 1;
    transform: scale3d(0.475, 0.475, 0.475) translate3d(0, 60px, 0);
    animation-timing-function: cubic-bezier(0.55, 0.055, 0.675, 0.19);
  }

  to {
    opacity: 0;
    transform: scale3d(0.1, 0.1, 0.1) translate3d(0, -2000px, 0);
    animation-timing-function: cubic-bezier(0.175, 0.885, 0.32, 1);
  }
`;function he(t,r){switch(r){case"down":return t?pe:ce;case"left":return t?ue:fe;case"right":return t?ye:me;case"up":return t?ge:le;default:return t?de:ie}}var Pe=t=>{let o=t,{direction:r,reverse:n=!1}=o,a=w(o,["direction","reverse"]),s=(0,h.useMemo)(()=>he(n,r),[r,n]);return f(S,d({keyframes:s},a))};export{Be as a,De as b,_e as c,Fe as d,Ee as e,Te as f,Ae as g,Je as h,Pe as i};
/*! Bundled license information:

react-is/cjs/react-is.production.min.js:
  (**
   * @license React
   * react-is.production.min.js
   *
   * Copyright (c) Facebook, Inc. and its affiliates.
   *
   * This source code is licensed under the MIT license found in the
   * LICENSE file in the root directory of this source tree.
   *)
*/
