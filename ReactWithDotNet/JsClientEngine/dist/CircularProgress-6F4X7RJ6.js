import{a as I}from"./chunk-T5JEUZ75.js";import{a as i}from"./chunk-GOCSXDI2.js";import{a as T}from"./chunk-2YBCIER7.js";import{a as j,e as N,f as U,v as l,y as E}from"./chunk-5XWBU6XQ.js";import"./chunk-SYECDWPD.js";import{a as R}from"./chunk-SORH75JF.js";import{m as g,n as v}from"./chunk-742UFDVP.js";import"./chunk-Z5S7LPTP.js";import{a as B}from"./chunk-G37S2DMD.js";import"./chunk-SI5MANRI.js";import"./chunk-22HPGI5J.js";import{a as _}from"./chunk-2W22VGCX.js";import{a as t,b as h,d as M,h as O}from"./chunk-GYULANB4.js";var F=O(_());function z(e){return N("MuiCircularProgress",e)}var X=U("MuiCircularProgress",["root","determinate","indeterminate","colorPrimary","colorSecondary","svg","circle","circleDeterminate","circleIndeterminate","circleDisableShrink"]);var c=O(B());var s=44,k=v`
  0% {
    transform: rotate(0deg);
  }

  100% {
    transform: rotate(360deg);
  }
`,x=v`
  0% {
    stroke-dasharray: 1px, 200px;
    stroke-dashoffset: 0;
  }

  50% {
    stroke-dasharray: 100px, 200px;
    stroke-dashoffset: -15px;
  }

  100% {
    stroke-dasharray: 100px, 200px;
    stroke-dashoffset: -125px;
  }
`,G=typeof k!="string"?g`
        animation: ${k} 1.4s linear infinite;
      `:null,W=typeof x!="string"?g`
        animation: ${x} 1.4s ease-in-out infinite;
      `:null,Y=e=>{let{classes:r,variant:o,color:a,disableShrink:m}=e,f={root:["root",o,`color${i(a)}`],svg:["svg"],circle:["circle",`circle${i(o)}`,m&&"circleDisableShrink"]};return j(f,z,r)},Z=l("span",{name:"MuiCircularProgress",slot:"Root",overridesResolver:(e,r)=>{let{ownerState:o}=e;return[r.root,r[o.variant],r[`color${i(o.color)}`]]}})(T(({theme:e})=>({display:"inline-block",variants:[{props:{variant:"determinate"},style:{transition:e.transitions.create("transform")}},{props:{variant:"indeterminate"},style:G||{animation:`${k} 1.4s linear infinite`}},...Object.entries(e.palette).filter(I()).map(([r])=>({props:{color:r},style:{color:(e.vars||e).palette[r].main}}))]}))),q=l("svg",{name:"MuiCircularProgress",slot:"Svg",overridesResolver:(e,r)=>r.svg})({display:"block"}),H=l("circle",{name:"MuiCircularProgress",slot:"Circle",overridesResolver:(e,r)=>{let{ownerState:o}=e;return[r.circle,r[`circle${i(o.variant)}`],o.disableShrink&&r.circleDisableShrink]}})(T(({theme:e})=>({stroke:"currentColor",variants:[{props:{variant:"determinate"},style:{transition:e.transitions.create("stroke-dashoffset")}},{props:{variant:"indeterminate"},style:{strokeDasharray:"80px, 200px",strokeDashoffset:0}},{props:({ownerState:r})=>r.variant==="indeterminate"&&!r.disableShrink,style:W||{animation:`${x} 1.4s ease-in-out infinite`}}]}))),J=F.forwardRef(function(r,o){let a=E({props:r,name:"MuiCircularProgress"}),$=a,{className:m,color:f="primary",disableShrink:V=!1,size:u=40,style:A,thickness:n=3.6,value:y=0,variant:b="indeterminate"}=$,K=M($,["className","color","disableShrink","size","style","thickness","value","variant"]),p=h(t({},a),{color:f,disableShrink:V,size:u,thickness:n,value:y,variant:b}),d=Y(p),P={},S={},w={};if(b==="determinate"){let D=2*Math.PI*((s-n)/2);P.strokeDasharray=D.toFixed(3),w["aria-valuenow"]=Math.round(y),P.strokeDashoffset=`${((100-y)/100*D).toFixed(3)}px`,S.transform="rotate(-90deg)"}return(0,c.jsx)(Z,h(t(t({className:R(d.root,m),style:t(t({width:u,height:u},S),A),ownerState:p,ref:o,role:"progressbar"},w),K),{children:(0,c.jsx)(q,{className:d.svg,ownerState:p,viewBox:`${s/2} ${s/2} ${s} ${s}`,children:(0,c.jsx)(H,{className:d.circle,style:P,ownerState:p,cx:s,cy:s,r:(s-n)/2,fill:"none",strokeWidth:n})})}))}),C=J;var dr=C;export{dr as default};
