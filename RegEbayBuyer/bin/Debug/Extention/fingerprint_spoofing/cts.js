!function(t){var e={};function n(r){if(e[r])return e[r].exports;var o=e[r]={i:r,l:!1,exports:{}};return t[r].call(o.exports,o,o.exports,n),o.l=!0,o.exports}n.m=t,n.c=e,n.d=function(t,e,r){n.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:r})},n.r=function(t){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},n.t=function(t,e){if(1&e&&(t=n(t)),8&e)return t;if(4&e&&"object"==typeof t&&t&&t.__esModule)return t;var r=Object.create(null);if(n.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var o in t)n.d(r,o,function(e){return t[e]}.bind(null,o));return r},n.n=function(t){var e=t&&t.__esModule?function(){return t.default}:function(){return t};return n.d(e,"a",e),e},n.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},n.p="",n(n.s=0)}([function(t,e){var n=function(t){var e=JSON.parse(t),n=function(t){return Math.floor(e.s*t)};window.fpsco?e=Object.assign(window.fpsco,e):window.fpsco=e,Object.assign(e,{i:0,c:7,n:[7,11,13,17,19,2053]}),e.s=(512|e.v)==e.v?Math.random():parseFloat(e.s),e.r=n(1e6);var r=function(){return e.i%e.c==0&&(e.i=1,e.n.push(e.c=e.n.shift())),e.r%e.c==e.i++?1:0},o=function(t,n){return n.forEach((function(n){return e[n]=(t|e.v)==e.v}))};if(o(1,["toDataURL","toBlob","getImageData","getLineDash","measureText"]),o(2,["readPixels","bufferData","getParameter"]),o(4,["getChannelData","getFloatFrequencyData"]),o(8,["offsetWidth","offsetHeight"]),o(16,["getClientRects"]),o(32,["enumerateDevices","getUserMedia","webkitGetUserMedia","MediaStreamTrack","RTCPeerConnection","RTCSessionDescription","webkitMediaStreamTrack","webkitRTCPeerConnection","webkitRTCSessionDescription"]),o(64,["getBattery","getGamepads","getVRDisplays","screen","plugins","mimeTypes","platform","language","languages"]),o(128,["getTimezoneOffset","resolvedOptions"]),o(256,["execCommand"]),!window.fpsld){var i=function(t){if(t.fpsld)return t;var o=function(t,n){var r=arguments.length>2&&void 0!==arguments[2]?arguments[2]:function(){return 0},o=function(n,r){var o=t[n];Object.defineProperty(t,n,{get:function(){return(!1!==e[n]?r:o).bind(this)}})};t&&("string"==typeof n?o(n,r):n instanceof Array?n.forEach((function(t){return o(t,r)})):Object.keys(n).forEach((function(t){return o(t,n[t])})))},i=function(t,n){return Object.keys(n).forEach((function(r){var o=t[r];Object.defineProperty(t,r,{get:function(){return!1!==e[r]?n[r]:o}})}))},a=function(n){try{n(t)}catch(t){e.debug&&console.error(t)}},c=t.document.createElement("canvas"),u=c.getContext("2d"),s=function(t){var n=t.width,r=t.height;return Object.assign(c,{width:n,height:r}),u.fillStyle="rgba(255,255,255,".concat(e.s,")"),u.fillRect(0,0,n,r),c};return a((function(t){var e=t.HTMLCanvasElement.prototype,n=e.toDataURL,r=e.toBlob;o(t.HTMLCanvasElement.prototype,{toDataURL:function(){return n.apply(s(this),arguments)},toBlob:function(){return r.apply(s(this),arguments)}})})),a((function(t){var e=t.CanvasRenderingContext2D.prototype,r=e.getImageData,i=e.getLineDash;o(t.CanvasRenderingContext2D.prototype,{getImageData:function(){return r.apply(s(this).getContext("2d"),arguments)},getLineDash:function(){return i.apply(s(this).getContext("2d"),arguments)},measureText:function(){return{width:.01*n(21543),__proto__:t.TextMetrics.prototype}}})})),a((function(t){o(t.WebGL2RenderingContext.prototype,{getParameter:function(){return n(8190)},readPixels:function(){},bufferData:function(){}})})),a((function(t){o(t.WebGLRenderingContext.prototype,{getParameter:function(){return n(8190)},readPixels:function(){},bufferData:function(){}})})),a((function(t){var r=null,i=t.AudioBuffer.prototype.getChannelData;o(t.AudioBuffer.prototype,{getChannelData:function(){var t=i.apply(this,arguments);if(r==t)return r;r=t;for(var o=0;o<r.length;o+=88){var a=n(o);r[a]=(r[a]+e.s)/2}return r}})})),a((function(t){var r=t.AnalyserNode.prototype.getFloatFrequencyData;o(t.AnalyserNode.prototype,{getFloatFrequencyData:function(){for(var t=r.apply(this,arguments),o=0;o<arguments[0].length;o+=88){var i=n(o);arguments[i]=(arguments[i]+e.s)/2}return t}})})),a((function(t){return n=t.HTMLElement.prototype,o={offsetWidth:function(){return Math.floor(this.getBoundingClientRect().width)+r()},offsetHeight:function(){return Math.floor(this.getBoundingClientRect().height)+r()}},Object.keys(o).forEach((function(t){var r=n.__lookupGetter__(t);Object.defineProperty(n,t,{get:function(){return(!1!==e[t]?o[t]:r).apply(this,arguments)}})}));var n,o})),a((function(t){return o(t.Element.prototype,"getClientRects",(function(){return{0:{x:0,y:0,top:0,bottom:0,left:0,right:0,height:n(5e3),width:n(4e3),__proto__:t.DOMRect.prototype},length:1,__proto__:t.DOMRectList.prototype}}))})),a((function(t){return o(t.navigator.mediaDevices,["enumerateDevices","getUserMedia"])})),a((function(t){return o(t.navigator,["getUserMedia","webkitGetUserMedia","getBattery","getGamepads","getVRDisplays"])})),a((function(t){return o(t,["MediaStreamTrack","RTCPeerConnection","RTCSessionDescription","webkitMediaStreamTrack","webkitRTCPeerConnection","webkitRTCSessionDescription"])})),a((function(t){return i(t,{screen:{availLeft:0,availTop:0,availWidth:1024,availHeight:768,width:1024,height:768,colorDepth:16,pixelDepth:16,__proto__:t.Screen.prototype,orientation:{angle:0,type:"landscape-primary",onchange:null,__proto__:t.ScreenOrientation.prototype}}})})),a((function(t){return i(t.navigator,{plugins:{length:0,__proto__:t.PluginArray.prototype},mimeTypes:{length:0,__proto__:t.MimeTypeArray.prototype},platform:"Win32",language:"en-US",languages:["en-US"]})})),a((function(t){return o(t.Intl.DateTimeFormat.prototype,"resolvedOptions",(function(){return{calendar:"gregory",day:"numeric",locale:"en-US",month:"numeric",numberingSystem:"latn",timeZone:"UTC",year:"numeric"}}))})),a((function(t){return o(t.Date.prototype,"getTimezoneOffset",(function(){return[720,660,600,570,540,480,420,360,300,240,210,180,120,60,0,-60,-120,-180,-210,-240,-270,-300,-330,-345,-360,-390,-420,-480,-510,-525,-540,-570,-600,-630,-660,-720,-765,-780,-840][n(39)]}))})),a((function(t){var e=t.document.execCommand;o(t.document,"execCommand",(function(t){return["cut","copy"].includes(t)||e.apply(this,arguments)}))})),t.fpsld=!0,t};i(window);var a=HTMLIFrameElement.prototype.__lookupGetter__("contentWindow"),c=HTMLIFrameElement.prototype.__lookupGetter__("contentDocument");Object.defineProperties(HTMLIFrameElement.prototype,{contentWindow:{get:function(){var t=a.apply(this,arguments);return this.src&&-1!=this.src.indexOf("//")&&location.host!=this.src.split("/")[2]?t:i(t)}},contentDocument:{get:function(){return this.src&&-1!=this.src.indexOf("//")&&location.host!=this.src.split("/")[2]?c.apply(this,arguments):(i(a.apply(this,arguments)),c.apply(this,arguments))}}})}},r=function(t){return chrome.storage.sync.get(["v","s"],(function(t){if(null==t.v)chrome.storage.sync.set({v:63,s:".448398"});else{var e=JSON.stringify(t),r=document.createElement("script");r.text="(".concat(n,")('").concat(e,"')"),document.documentElement.appendChild(r).parentNode.removeChild(r)}}))};r(),chrome.storage.onChanged.addListener(r)}]);