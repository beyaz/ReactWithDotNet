import{b as o}from"./chunk-MY5M2XOA.js";function u(e,t){(t==null||t>e.length)&&(t=e.length);for(var r=0,n=Array(t);r<t;r++)n[r]=e[r];return n}function s(e){if(Array.isArray(e))return u(e)}function d(e){if(typeof Symbol!="undefined"&&e[Symbol.iterator]!=null||e["@@iterator"]!=null)return Array.from(e)}function c(e,t){if(e){if(typeof e=="string")return u(e,t);var r={}.toString.call(e).slice(8,-1);return r==="Object"&&e.constructor&&(r=e.constructor.name),r==="Map"||r==="Set"?Array.from(e):r==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(r)?u(e,t):void 0}}function y(){throw new TypeError(`Invalid attempt to spread non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function v(e){return s(e)||d(e)||c(e)||y()}var l={DEFAULT_MASKS:{pint:/[\d]/,int:/[\d\-]/,pnum:/[\d\.]/,money:/[\d\.\s,]/,num:/[\d\-\.]/,hex:/[0-9a-f]/i,email:/[a-z0-9_\.\-@]/i,alpha:/[a-z_]/i,alphanum:/[a-z0-9_]/i},getRegex:function(t){return l.DEFAULT_MASKS[t]?l.DEFAULT_MASKS[t]:t},onBeforeInput:function(t,r,n){n||!o.isAndroid()||this.validateKey(t,t.data,r)},onKeyPress:function(t,r,n){n||o.isAndroid()||t.ctrlKey||t.altKey||t.metaKey||this.validateKey(t,t.key,r)},onPaste:function(t,r,n){if(!n){var a=this.getRegex(r),i=t.clipboardData.getData("text");v(i).forEach(function(f){if(!a.test(f))return t.preventDefault(),!1})}},validateKey:function(t,r,n){if(r!=null){var a=r.length<=2;if(a){var i=this.getRegex(n);i.test(r)||t.preventDefault()}}},validate:function(t,r){var n=t.target.value,a=!0,i=this.getRegex(r);return n&&!i.test(n)&&(a=!1),a}};export{l as a};