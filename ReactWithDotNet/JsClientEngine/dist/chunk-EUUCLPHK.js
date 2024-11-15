import{a as at}from"./chunk-KNTBFN6K.js";import{a as Q,i as M,j as J,k as A,m as W,n as r,o as N}from"./chunk-742UFDVP.js";import{a as tt}from"./chunk-G37S2DMD.js";import{a as B}from"./chunk-2W22VGCX.js";import{a as l,b as R,d as O,h as X}from"./chunk-GYULANB4.js";var z=X(tt());var Qa=X(B());var nr=X(Q());var H=z.Fragment;function c(t,a,n){return M.call(a,"css")?z.jsx(A,J(t,a),n):z.jsx(t,a,n)}var y=X(B(),1);var L=X(B(),1),k=X(B(),1);var rt=Object.defineProperty,nt=(t,a,n)=>a in t?rt(t,a,{enumerable:!0,configurable:!0,writable:!0,value:n}):t[a]=n,j=(t,a,n)=>nt(t,typeof a!="symbol"?a+"":a,n),$=new Map,D=new WeakMap,Z=0,et=void 0;function ot(t){return t?(D.has(t)||(Z+=1,D.set(t,Z.toString())),D.get(t)):"0"}function st(t){return Object.keys(t).sort().filter(a=>t[a]!==void 0).map(a=>`${a}_${a==="root"?ot(t.root):t[a]}`).toString()}function it(t){let a=st(t),n=$.get(a);if(!n){let e=new Map,s,o=new IntersectionObserver(i=>{i.forEach(f=>{var d;let u=f.isIntersecting&&s.some(g=>f.intersectionRatio>=g);t.trackVisibility&&typeof f.isVisible=="undefined"&&(f.isVisible=u),(d=e.get(f.target))==null||d.forEach(g=>{g(u,f)})})},t);s=o.thresholds||(Array.isArray(t.threshold)?t.threshold:[t.threshold||0]),n={id:a,observer:o,elements:e},$.set(a,n)}return n}function q(t,a,n={},e=et){if(typeof window.IntersectionObserver=="undefined"&&e!==void 0){let d=t.getBoundingClientRect();return a(e,{isIntersecting:e,target:t,intersectionRatio:typeof n.threshold=="number"?n.threshold:0,time:0,boundingClientRect:d,intersectionRect:d,rootBounds:d}),()=>{}}let{id:s,observer:o,elements:i}=it(n),f=i.get(t)||[];return i.has(t)||i.set(t,f),f.push(a),o.observe(t),function(){f.splice(f.indexOf(a),1),f.length===0&&(i.delete(t),o.unobserve(t)),i.size===0&&(o.disconnect(),$.delete(s))}}function ct(t){return typeof t.children!="function"}var _=class extends L.Component{constructor(t){super(t),j(this,"node",null),j(this,"_unobserveCb",null),j(this,"handleNode",a=>{this.node&&(this.unobserve(),!a&&!this.props.triggerOnce&&!this.props.skip&&this.setState({inView:!!this.props.initialInView,entry:void 0})),this.node=a||null,this.observeNode()}),j(this,"handleChange",(a,n)=>{a&&this.props.triggerOnce&&this.unobserve(),ct(this.props)||this.setState({inView:a,entry:n}),this.props.onChange&&this.props.onChange(a,n)}),this.state={inView:!!t.initialInView,entry:void 0}}componentDidMount(){this.unobserve(),this.observeNode()}componentDidUpdate(t){(t.rootMargin!==this.props.rootMargin||t.root!==this.props.root||t.threshold!==this.props.threshold||t.skip!==this.props.skip||t.trackVisibility!==this.props.trackVisibility||t.delay!==this.props.delay)&&(this.unobserve(),this.observeNode())}componentWillUnmount(){this.unobserve()}observeNode(){if(!this.node||this.props.skip)return;let{threshold:t,root:a,rootMargin:n,trackVisibility:e,delay:s,fallbackInView:o}=this.props;this._unobserveCb=q(this.node,this.handleChange,{threshold:t,root:a,rootMargin:n,trackVisibility:e,delay:s},o)}unobserve(){this._unobserveCb&&(this._unobserveCb(),this._unobserveCb=null)}render(){let{children:t}=this.props;if(typeof t=="function"){let{inView:h,entry:m}=this.state;return t({inView:h,entry:m,ref:this.handleNode})}let x=this.props,{as:a,triggerOnce:n,threshold:e,root:s,rootMargin:o,onChange:i,skip:f,trackVisibility:d,delay:u,initialInView:g,fallbackInView:w}=x,b=O(x,["as","triggerOnce","threshold","root","rootMargin","onChange","skip","trackVisibility","delay","initialInView","fallbackInView"]);return L.createElement(a||"div",l({ref:this.handleNode},b),t)}};function T({threshold:t,delay:a,trackVisibility:n,rootMargin:e,root:s,triggerOnce:o,skip:i,initialInView:f,fallbackInView:d,onChange:u}={}){var g;let[w,b]=k.useState(null),x=k.useRef(),[h,m]=k.useState({inView:!!f,entry:void 0});x.current=u,k.useEffect(()=>{if(i||!w)return;let v;return v=q(w,(S,U)=>{m({inView:S,entry:U}),x.current&&x.current(S,U),U.isIntersecting&&o&&v&&(v(),v=void 0)},{root:s,rootMargin:e,threshold:t,trackVisibility:n,delay:a},d),()=>{v&&v()}},[Array.isArray(t)?t.toString():t,w,s,e,o,i,n,d,a]);let Y=(g=h.entry)==null?void 0:g.target,C=k.useRef();!w&&Y&&!o&&!i&&C.current!==Y&&(C.current=Y,m({inView:!!f,entry:void 0}));let p=[b,h.inView,h.entry];return p.ref=p[0],p.inView=p[1],p.entry=p[2],p}var G=X(at(),1);var ft=r`
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
`,mt=r`
  from,
  50%,
  to {
    opacity: 1;
  }

  25%,
  75% {
    opacity: 0;
  }
`,lt=r`
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
`,dt=r`
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
`,pt=r`
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
`,ut=r`
  from {
    transform: scale3d(1, 1, 1);
  }

  50% {
    transform: scale3d(1.05, 1.05, 1.05);
  }

  to {
    transform: scale3d(1, 1, 1);
  }
`,gt=r`
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
`,yt=r`
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
`,ht=r`
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
`,bt=r`
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
`,xt=r`
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
`,Ot=r`
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
`,wt=r`
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
`,vt=r`
  from {
    opacity: 0;
  }

  to {
    opacity: 1;
  }
`,It=r`
  from {
    opacity: 0;
    transform: translate3d(-100%, 100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Rt=r`
  from {
    opacity: 0;
    transform: translate3d(100%, 100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,kt=r`
  from {
    opacity: 0;
    transform: translate3d(0, -100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Xt=r`
  from {
    opacity: 0;
    transform: translate3d(0, -2000px, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,E=r`
  from {
    opacity: 0;
    transform: translate3d(-100%, 0, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,St=r`
  from {
    opacity: 0;
    transform: translate3d(-2000px, 0, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,zt=r`
  from {
    opacity: 0;
    transform: translate3d(100%, 0, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Yt=r`
  from {
    opacity: 0;
    transform: translate3d(2000px, 0, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Ct=r`
  from {
    opacity: 0;
    transform: translate3d(-100%, -100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Vt=r`
  from {
    opacity: 0;
    transform: translate3d(100%, -100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Bt=r`
  from {
    opacity: 0;
    transform: translate3d(0, 100%, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,Nt=r`
  from {
    opacity: 0;
    transform: translate3d(0, 2000px, 0);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`;function jt({duration:t=1e3,delay:a=0,timingFunction:n="ease",keyframes:e=E,iterationCount:s=1}){return W`
    animation-duration: ${t}ms;
    animation-timing-function: ${n};
    animation-delay: ${a}ms;
    animation-name: ${e};
    animation-direction: normal;
    animation-fill-mode: both;
    animation-iteration-count: ${s};

    @media (prefers-reduced-motion: reduce) {
      animation: none;
    }
  `}function Dt(t){return t==null}function Lt(t){return typeof t=="string"||typeof t=="number"||typeof t=="boolean"}function K(t,a){return n=>n?t():a()}function V(t){return K(t,()=>null)}function F(t){return V(()=>({opacity:0}))(t)}var I=t=>{let{cascade:a=!1,damping:n=.5,delay:e=0,duration:s=1e3,fraction:o=0,keyframes:i=E,triggerOnce:f=!1,className:d,style:u,childClassName:g,childStyle:w,children:b,onVisibilityChange:x}=t,h=(0,y.useMemo)(()=>jt({keyframes:i,duration:s}),[s,i]);return Dt(b)?null:Lt(b)?c($t,R(l({},t),{animationStyles:h,children:String(b)})):(0,G.isFragment)(b)?c(P,R(l({},t),{animationStyles:h})):c(H,{children:y.Children.map(b,(m,Y)=>{if(!(0,y.isValidElement)(m))return null;let C=e+(a?Y*s*n:0);switch(m.type){case"ol":case"ul":return c(N,{children:({cx:p})=>c(m.type,R(l({},m.props),{className:p(d,m.props.className),style:Object.assign({},u,m.props.style),children:c(I,R(l({},t),{children:m.props.children}))}))});case"li":return c(_,{threshold:o,triggerOnce:f,onChange:x,children:({inView:p,ref:v})=>c(N,{children:({cx:S})=>c(m.type,R(l({},m.props),{ref:v,className:S(g,m.props.className),css:V(()=>h)(p),style:Object.assign({},w,m.props.style,F(!p),{animationDelay:C+"ms"})}))})});default:return c(_,{threshold:o,triggerOnce:f,onChange:x,children:({inView:p,ref:v})=>c("div",{ref:v,className:d,css:V(()=>h)(p),style:Object.assign({},u,F(!p),{animationDelay:C+"ms"}),children:c(N,{children:({cx:S})=>c(m.type,R(l({},m.props),{className:S(g,m.props.className),style:Object.assign({},w,m.props.style)}))})})})}})})},Ut={display:"inline-block",whiteSpace:"pre"},$t=t=>{let{animationStyles:a,cascade:n=!1,damping:e=.5,delay:s=0,duration:o=1e3,fraction:i=0,triggerOnce:f=!1,className:d,style:u,children:g,onVisibilityChange:w}=t,{ref:b,inView:x}=T({triggerOnce:f,threshold:i,onChange:w});return K(()=>c("div",{ref:b,className:d,style:Object.assign({},u,Ut),children:g.split("").map((h,m)=>c("span",{css:V(()=>a)(x),style:{animationDelay:s+m*o*e+"ms"},children:h},m))}),()=>c(P,R(l({},t),{children:g})))(n)},P=t=>{let{animationStyles:a,fraction:n=0,triggerOnce:e=!1,className:s,style:o,children:i,onVisibilityChange:f}=t,{ref:d,inView:u}=T({triggerOnce:e,threshold:n,onChange:f});return c("div",{ref:d,className:s,css:V(()=>a)(u),style:Object.assign({},o,F(!u)),children:i})};function _t(t){switch(t){case"bounce":return[ft,{transformOrigin:"center bottom"}];case"flash":return[mt];case"headShake":return[lt,{animationTimingFunction:"ease-in-out"}];case"heartBeat":return[dt,{animationTimingFunction:"ease-in-out"}];case"jello":return[pt,{transformOrigin:"center"}];case"pulse":return[ut,{animationTimingFunction:"ease-in-out"}];case"rubberBand":return[gt];case"shake":return[yt];case"shakeX":return[ht];case"shakeY":return[bt];case"swing":return[xt,{transformOrigin:"top center"}];case"tada":return[Ot];case"wobble":return[wt]}}var ur=t=>{let i=t,{effect:a="bounce",style:n}=i,e=O(i,["effect","style"]),[s,o]=(0,y.useMemo)(()=>_t(a),[a]);return c(I,l({keyframes:s,style:Object.assign({},n,o)},e))},Tt=r`
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
`,Ft=r`
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
`,Et=r`
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
`,Mt=r`
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
`,Jt=r`
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
`,At=r`
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
`,Wt=r`
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
`,Ht=r`
  20% {
    opacity: 1;
    transform: translate3d(20px, 0, 0) scaleX(0.9);
  }

  to {
    opacity: 0;
    transform: translate3d(-2000px, 0, 0) scaleX(2);
  }
`,Zt=r`
  20% {
    opacity: 1;
    transform: translate3d(-20px, 0, 0) scaleX(0.9);
  }

  to {
    opacity: 0;
    transform: translate3d(2000px, 0, 0) scaleX(2);
  }
`,qt=r`
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
`;function Gt(t,a){switch(a){case"down":return t?Wt:Ft;case"left":return t?Ht:Et;case"right":return t?Zt:Mt;case"up":return t?qt:Jt;default:return t?At:Tt}}var gr=t=>{let o=t,{direction:a,reverse:n=!1}=o,e=O(o,["direction","reverse"]),s=(0,y.useMemo)(()=>Gt(n,a),[a,n]);return c(I,l({keyframes:s},e))},Kt=r`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
  }
`,Pt=r`
  from {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }

  to {
    opacity: 0;
    transform: translate3d(-100%, 100%, 0);
  }
`,Qt=r`
  from {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }

  to {
    opacity: 0;
    transform: translate3d(100%, 100%, 0);
  }
`,ta=r`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(0, 100%, 0);
  }
`,aa=r`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(0, 2000px, 0);
  }
`,ra=r`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(-100%, 0, 0);
  }
`,na=r`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(-2000px, 0, 0);
  }
`,ea=r`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(100%, 0, 0);
  }
`,oa=r`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(2000px, 0, 0);
  }
`,sa=r`
  from {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }

  to {
    opacity: 0;
    transform: translate3d(-100%, -100%, 0);
  }
`,ia=r`
  from {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }

  to {
    opacity: 0;
    transform: translate3d(100%, -100%, 0);
  }
`,ca=r`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(0, -100%, 0);
  }
`,fa=r`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(0, -2000px, 0);
  }
`;function ma(t,a,n){switch(n){case"bottom-left":return a?Pt:It;case"bottom-right":return a?Qt:Rt;case"down":return t?a?aa:Xt:a?ta:kt;case"left":return t?a?na:St:a?ra:E;case"right":return t?a?oa:Yt:a?ea:zt;case"top-left":return a?sa:Ct;case"top-right":return a?ia:Vt;case"up":return t?a?fa:Nt:a?ca:Bt;default:return a?Kt:vt}}var yr=t=>{let i=t,{big:a=!1,direction:n,reverse:e=!1}=i,s=O(i,["big","direction","reverse"]),o=(0,y.useMemo)(()=>ma(a,e,n),[a,n,e]);return c(I,l({keyframes:o},s))},la=r`
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
`,da=r`
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
`,pa=r`
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
`,ua=r`
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
`,ga=r`
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
`;function ya(t,a){switch(a){case"horizontal":return t?ua:da;case"vertical":return t?ga:pa;default:return la}}var ha={backfaceVisibility:"visible"},hr=t=>{let i=t,{direction:a,reverse:n=!1,style:e}=i,s=O(i,["direction","reverse","style"]),o=(0,y.useMemo)(()=>ya(n,a),[a,n]);return c(I,l({keyframes:o,style:Object.assign({},e,ha)},s))},ba=r`
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
`,xa=r`
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
`,br=r`
  from {
    opacity: 0;
    transform: translate3d(-100%, 0, 0) rotate3d(0, 0, 1, -120deg);
  }

  to {
    opacity: 1;
    transform: translate3d(0, 0, 0);
  }
`,xr=r`
  from {
    opacity: 1;
  }

  to {
    opacity: 0;
    transform: translate3d(100%, 0, 0) rotate3d(0, 0, 1, 120deg);
  }
`,Oa={transformOrigin:"top left"},Or=t=>{let e=t,{style:a}=e,n=O(e,["style"]);return c(I,l({keyframes:ba,style:Object.assign({},a,Oa)},n))},wr=t=>c(I,l({keyframes:xa},t));var wa=r`
  from {
    transform: rotate3d(0, 0, 1, -200deg);
    opacity: 0;
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
`,va=r`
  from {
    transform: rotate3d(0, 0, 1, -45deg);
    opacity: 0;
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
`,Ia=r`
  from {
    transform: rotate3d(0, 0, 1, 45deg);
    opacity: 0;
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
`,Ra=r`
  from {
    transform: rotate3d(0, 0, 1, 45deg);
    opacity: 0;
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
`,ka=r`
  from {
    transform: rotate3d(0, 0, 1, -90deg);
    opacity: 0;
  }

  to {
    transform: translate3d(0, 0, 0);
    opacity: 1;
  }
`,Xa=r`
  from {
    opacity: 1;
  }

  to {
    transform: rotate3d(0, 0, 1, 200deg);
    opacity: 0;
  }
`,Sa=r`
  from {
    opacity: 1;
  }

  to {
    transform: rotate3d(0, 0, 1, 45deg);
    opacity: 0;
  }
`,za=r`
  from {
    opacity: 1;
  }

  to {
    transform: rotate3d(0, 0, 1, -45deg);
    opacity: 0;
  }
`,Ya=r`
  from {
    opacity: 1;
  }

  to {
    transform: rotate3d(0, 0, 1, -45deg);
    opacity: 0;
  }
`,Ca=r`
  from {
    opacity: 1;
  }

  to {
    transform: rotate3d(0, 0, 1, 90deg);
    opacity: 0;
  }
`;function Va(t,a){switch(a){case"bottom-left":return t?[Sa,{transformOrigin:"left bottom"}]:[va,{transformOrigin:"left bottom"}];case"bottom-right":return t?[za,{transformOrigin:"right bottom"}]:[Ia,{transformOrigin:"right bottom"}];case"top-left":return t?[Ya,{transformOrigin:"left bottom"}]:[Ra,{transformOrigin:"left bottom"}];case"top-right":return t?[Ca,{transformOrigin:"right bottom"}]:[ka,{transformOrigin:"right bottom"}];default:return t?[Xa,{transformOrigin:"center"}]:[wa,{transformOrigin:"center"}]}}var vr=t=>{let f=t,{direction:a,reverse:n=!1,style:e}=f,s=O(f,["direction","reverse","style"]),[o,i]=(0,y.useMemo)(()=>Va(n,a),[a,n]);return c(I,l({keyframes:o,style:Object.assign({},e,i)},s))},Ba=r`
  from {
    transform: translate3d(0, -100%, 0);
    visibility: visible;
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,Na=r`
  from {
    transform: translate3d(-100%, 0, 0);
    visibility: visible;
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,ja=r`
  from {
    transform: translate3d(100%, 0, 0);
    visibility: visible;
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,Da=r`
  from {
    transform: translate3d(0, 100%, 0);
    visibility: visible;
  }

  to {
    transform: translate3d(0, 0, 0);
  }
`,La=r`
  from {
    transform: translate3d(0, 0, 0);
  }

  to {
    visibility: hidden;
    transform: translate3d(0, 100%, 0);
  }
`,Ua=r`
  from {
    transform: translate3d(0, 0, 0);
  }

  to {
    visibility: hidden;
    transform: translate3d(-100%, 0, 0);
  }
`,$a=r`
  from {
    transform: translate3d(0, 0, 0);
  }

  to {
    visibility: hidden;
    transform: translate3d(100%, 0, 0);
  }
`,_a=r`
  from {
    transform: translate3d(0, 0, 0);
  }

  to {
    visibility: hidden;
    transform: translate3d(0, -100%, 0);
  }
`;function Ta(t,a){switch(a){case"down":return t?La:Ba;case"right":return t?$a:ja;case"up":return t?_a:Da;case"left":default:return t?Ua:Na}}var Ir=t=>{let o=t,{direction:a,reverse:n=!1}=o,e=O(o,["direction","reverse"]),s=(0,y.useMemo)(()=>Ta(n,a),[a,n]);return c(I,l({keyframes:s},e))},Fa=r`
  from {
    opacity: 0;
    transform: scale3d(0.3, 0.3, 0.3);
  }

  50% {
    opacity: 1;
  }
`,Ea=r`
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
`,Ma=r`
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
`,Ja=r`
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
`,Aa=r`
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
`,Wa=r`
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
`,Ha=r`
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
`,Za=r`
  40% {
    opacity: 1;
    transform: scale3d(0.475, 0.475, 0.475) translate3d(42px, 0, 0);
  }

  to {
    opacity: 0;
    transform: scale(0.1) translate3d(-2000px, 0, 0);
  }
`,qa=r`
  40% {
    opacity: 1;
    transform: scale3d(0.475, 0.475, 0.475) translate3d(-42px, 0, 0);
  }

  to {
    opacity: 0;
    transform: scale(0.1) translate3d(2000px, 0, 0);
  }
`,Ga=r`
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
`;function Ka(t,a){switch(a){case"down":return t?Ha:Ea;case"left":return t?Za:Ma;case"right":return t?qa:Ja;case"up":return t?Ga:Aa;default:return t?Wa:Fa}}var Rr=t=>{let o=t,{direction:a,reverse:n=!1}=o,e=O(o,["direction","reverse"]),s=(0,y.useMemo)(()=>Ka(n,a),[a,n]);return c(I,l({keyframes:s},e))};export{ur as a,gr as b,yr as c,hr as d,Or as e,wr as f,vr as g,Ir as h,Rr as i};
