import"./chunk-SRDJZ2SW.js";import{d as a}from"./chunk-MZOZXVYI.js";import"./chunk-DLL45UCJ.js";import{a as u}from"./chunk-2W22VGCX.js";import{a as n,b as p,h as m}from"./chunk-GYULANB4.js";var r=m(u());var c=()=>{let[o,d]=r.default.useState({}),e=a();if(e&&e.completed){let s=o[e.id]||{name:e.url||e.file.name,progress:[0]};~s.progress.indexOf(e.completed)||(s.progress.push(e.completed),d(p(n({},o),{[e.id]:s})))}let i=Object.entries(o);return r.default.createElement("div",null,i.map(([s,{progress:t,name:g}])=>{let l=t[t.length-1];return r.default.createElement("progress",{key:s,max:100,value:l})}))},v=c;export{v as default};