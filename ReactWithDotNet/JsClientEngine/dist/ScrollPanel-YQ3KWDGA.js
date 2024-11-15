import{B as ce,C as se,a as re,b as c,f as te,m as ne,p as ae,u as oe,v as le}from"./chunk-NI4MO4TP.js";import{a as Le}from"./chunk-2W22VGCX.js";import{h as Te}from"./chunk-GYULANB4.js";var r=Te(Le());function z(){return z=Object.assign?Object.assign.bind():function(n){for(var a=1;a<arguments.length;a++){var t=arguments[a];for(var u in t)Object.prototype.hasOwnProperty.call(t,u)&&(n[u]=t[u])}return n},z.apply(this,arguments)}function Pe(n){if(Array.isArray(n))return n}function Ee(n,a){var t=n==null?null:typeof Symbol!="undefined"&&n[Symbol.iterator]||n["@@iterator"];if(t!=null){var u,l,S,v,g=[],h=!0,x=!1;try{if(S=(t=t.call(n)).next,a===0){if(Object(t)!==t)return;h=!1}else for(;!(h=(u=S.call(t)).done)&&(g.push(u.value),g.length!==a);h=!0);}catch(B){x=!0,l=B}finally{try{if(!h&&t.return!=null&&(v=t.return(),Object(v)!==v))return}finally{if(x)throw l}}return g}}function ie(n,a){(a==null||a>n.length)&&(a=n.length);for(var t=0,u=new Array(a);t<a;t++)u[t]=n[t];return u}function _e(n,a){if(n){if(typeof n=="string")return ie(n,a);var t=Object.prototype.toString.call(n).slice(8,-1);if(t==="Object"&&n.constructor&&(t=n.constructor.name),t==="Map"||t==="Set")return Array.from(n);if(t==="Arguments"||/^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(t))return ie(n,a)}}function He(){throw new TypeError(`Invalid attempt to destructure non-iterable instance.
In order to be iterable, non-array objects must have a [Symbol.iterator]() method.`)}function A(n,a){return Pe(n)||Ee(n,a)||_e(n,a)||He()}var T=ce.extend({defaultProps:{__TYPE:"ScrollPanel",id:null,style:null,className:null,children:void 0,step:5},css:{classes:{root:"p-scrollpanel p-component",wrapper:"p-scrollpanel-wrapper",content:"p-scrollpanel-content",barx:"p-scrollpanel-bar p-scrollpanel-bar-x",bary:"p-scrollpanel-bar p-scrollpanel-bar-y"},styles:`
        @layer primereact {
            .p-scrollpanel-wrapper {
                overflow: hidden;
                width: 100%;
                height: 100%;
                position: relative;
                z-index: 1;
                float: left;
            }

            .p-scrollpanel-content {
                height: calc(100% + 18px);
                width: calc(100% + 18px);
                padding: 0 18px 18px 0;
                position: relative;
                overflow: auto;
                box-sizing: border-box;
            }

            .p-scrollpanel-bar {
                position: relative;
                background: #c1c1c1;
                border-radius: 3px;
                z-index: 2;
                cursor: pointer;
                opacity: 0;
                transition: opacity 0.25s linear;
            }

            .p-scrollpanel-bar-y {
                width: 9px;
                top: 0;
            }

            .p-scrollpanel-bar-x {
                height: 9px;
                bottom: 0;
            }

            .p-scrollpanel-hidden {
                visibility: hidden;
            }

            .p-scrollpanel:hover .p-scrollpanel-bar,
            .p-scrollpanel:active .p-scrollpanel-bar {
                opacity: 1;
            }

            .p-scrollpanel-grabbed {
                user-select: none;
            }
        }
        `}}),ue=r.forwardRef(function(n,a){var t=oe(),u=r.useContext(ne),l=T.getProps(n,u),S=r.useState(l.id),v=A(S,2),g=v[0],h=v[1],x=r.useState("vertical"),B=A(x,2),L=B[0],b=B[1],P=T.setMetaData({props:l}),y=P.ptm,R=P.cx,pe=P.isUnstyled;se(T.css.styles,pe,{name:"scrollpanel"});var d=r.useRef(null),s=r.useRef(null),p=r.useRef(null),f=r.useRef(null),fe=r.useState(0),N=A(fe,2),O=N[0],de=N[1],me=r.useState(0),$=A(me,2),W=$[0],ve=$[1],E=r.useRef(!1),_=r.useRef(!1),H=r.useRef(null),F=r.useRef(null),M=r.useRef(null),D=r.useRef(null),w=r.useRef(null),j=r.useRef(!1),Y=r.useRef(null),k=g+"_content",he=function(){var e=getComputedStyle(d.current),i=getComputedStyle(p.current),U=c.getHeight(d.current)-parseInt(i.height,10);e["max-height"]!=="none"&&U===0&&(s.current.offsetHeight+parseInt(i.height,10)>parseInt(e["max-height"],10)?d.current.style.height=e["max-height"]:d.current.style.height=s.current.offsetHeight+parseFloat(e.paddingTop)+parseFloat(e.paddingBottom)+parseFloat(e.borderTopWidth)+parseFloat(e.borderBottomWidth)+"px")},m=function(){var e=s.current.scrollWidth,i=s.current.clientWidth,U=(d.current.clientHeight-p.current.clientHeight)*-1;M.current=i/e;var ee=s.current.scrollHeight,Ce=s.current.clientHeight,Ae=(d.current.clientWidth-f.current.clientWidth)*-1;D.current=Ce/ee,w.current=window.requestAnimationFrame(function(){M.current>=1?c.addClass(p.current,"p-scrollpanel-hidden"):(c.removeClass(p.current,"p-scrollpanel-hidden"),c.applyStyle(p.current,{width:Math.max(M.current*100,10)+"%",left:s.current.scrollLeft/e*100+"%",bottom:U+"px"})),D.current>=1?c.addClass(f.current,"p-scrollpanel-hidden"):(c.removeClass(f.current,"p-scrollpanel-hidden"),c.applyStyle(f.current,{height:Math.max(D.current*100,10)+"%",top:"calc("+s.current.scrollTop/ee*100+"% - "+p.current.clientHeight+"px)",right:Ae+"px"}))})},K=function(e){p.current.isSameNode(e.target)?b("horizontal"):f.current.isSameNode(e.target)&&b("vertical")},q=function(){L==="horizontal"&&b("vertical")},ge=function(e){O!==e.target.scrollLeft?(de(e.target.scrollLeft),b("horizontal")):W!==e.target.scrollTop&&(ve(e.target.scrollTop),b("vertical")),m()},be=function(e){_.current=!0,F.current=e.pageY,c.addClass(f.current,"p-scrollpanel-grabbed"),c.addClass(document.body,"p-scrollpanel-grabbed"),document.addEventListener("mousemove",I),document.addEventListener("mouseup",X),e.preventDefault()},ye=function(e){E.current=!0,H.current=e.pageX,c.addClass(p.current,"p-scrollpanel-grabbed"),c.addClass(document.body,"p-scrollpanel-grabbed"),document.addEventListener("mousemove",I),document.addEventListener("mouseup",X),e.preventDefault()},I=function(e){E.current?G(e):(_.current||G(e),J(e))},G=function(e){var i=e.pageX-H.current;H.current=e.pageX,w.current=window.requestAnimationFrame(function(){s.current.scrollLeft+=i/M.current})},J=function(e){var i=e.pageY-F.current;F.current=e.pageY,w.current=window.requestAnimationFrame(function(){s.current.scrollTop+=i/D.current})},X=function(e){c.removeClass(f.current,"p-scrollpanel-grabbed"),c.removeClass(p.current,"p-scrollpanel-grabbed"),c.removeClass(document.body,"p-scrollpanel-grabbed"),document.removeEventListener("mousemove",I),document.removeEventListener("mouseup",X),E.current=!1,_.current=!1},Q=function(e){if(L==="vertical")switch(e.code){case"ArrowDown":{C("scrollTop",l.step),e.preventDefault();break}case"ArrowUp":{C("scrollTop",l.step*-1),e.preventDefault();break}case"ArrowLeft":case"ArrowRight":{e.preventDefault();break}}else if(L==="horizontal")switch(e.code){case"ArrowRight":{C("scrollLeft",l.step),e.preventDefault();break}case"ArrowLeft":{C("scrollLeft",l.step*-1),e.preventDefault();break}case"ArrowDown":case"ArrowUp":{e.preventDefault();break}}},V=function(){Z()},Re=function(e,i){s.current[e]+=i,m()},C=function(e,i){Z(),Y.current=setTimeout(function(){Re(e,i)},40)},Z=function(){Y.current&&clearTimeout(Y.current)},we=function(){m()};le(function(){l.id||h(te()),m(),window.addEventListener("resize",m),he(),j.current=!0}),ae(function(){j.current&&window.removeEventListener("resize",m),w.current&&window.cancelAnimationFrame(w.current)}),r.useImperativeHandle(a,function(){return{props:l,refresh:we,getElement:function(){return d.current},getContent:function(){return s.current},getXBar:function(){return p.current},getYBar:function(){return f.current}}});var Se=t({id:l.id,ref:d,style:l.style,className:re(l.className,R("root"))},T.getOtherProps(l),y("root")),xe=t({className:R("wrapper")},y("wrapper")),Be=t({className:R("content"),onScroll:ge,onMouseEnter:m},y("content")),Me=t({ref:p,role:"scrollbar",className:R("barx"),tabIndex:0,"aria-valuenow":W,"aria-controls":k,"aria-orientation":"horizontal",onFocus:K,onBlur:q,onKeyDown:Q,onKeyUp:V,onMouseDown:ye},y("barx")),De=t({ref:f,role:"scrollbar",className:R("bary"),tabIndex:0,"aria-valuenow":O,"aria-controls":k,"aria-orientation":"vertical",onFocus:K,onBlur:q,onKeyDown:Q,onKeyUp:V,onMouseDown:be},y("bary"));return r.createElement("div",Se,r.createElement("div",xe,r.createElement("div",z({ref:s},Be),l.children)),r.createElement("div",Me),r.createElement("div",De))});ue.displayName="ScrollPanel";export{ue as default};
