import{a as p,b as h,f as y,g as b}from"./chunk-QRFMBJ46.js";import{a as C}from"./chunk-DLL45UCJ.js";import{a as g}from"./chunk-2W22VGCX.js";import{d as c,h as m}from"./chunk-GYULANB4.js";var r=m(g()),O=m(C());function u(){return u=Object.assign?Object.assign.bind():function(t){for(var o=1;o<arguments.length;o++){var e=arguments[o];for(var n in e)({}).hasOwnProperty.call(e,n)&&(t[n]=e[n])}return t},u.apply(null,arguments)}var N="Uploady - Container for file input must be a valid dom element",x=(t,o,e)=>r.default.createElement("input",u({},t,{name:o.inputFieldName,type:"file",ref:e})),P=(t,o,e,n,a)=>t&&o?O.default.createPortal(x(e,n,a),t):null,v=(0,r.memo)((0,r.forwardRef)((a,n)=>{var i=a,{container:t,noPortal:o}=i,e=c(i,["container","noPortal"]);let l=b(),s=t&&t.nodeType===1;return(0,h.default)(s||!p(),N),o?x(e,l,n):P(t,s,e,l,n)})),D=t=>{let f=t,{multiple:o=!0,capture:e,accept:n,webkitdirectory:a,children:i,inputFieldContainer:l,customInput:s,fileInputId:U,noPortal:$=!1}=f,E=c(f,["multiple","capture","accept","webkitdirectory","children","inputFieldContainer","customInput","fileInputId","noPortal"]),w=s?null:l||(p()?document.body:null),d=(0,r.useRef)();return r.default.createElement(y,u({},E,{inputRef:d}),s?null:r.default.createElement(v,{container:w,multiple:o,capture:e,accept:n,webkitdirectory:a==null?void 0:a.toString(),style:{display:"none"},ref:d,id:U,noPortal:$}),i)},F=D;var A=m(g());var q=F;export{q as a};