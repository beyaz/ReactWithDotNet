import {
  require_react_dom
} from "./chunk-QNQS7X5M.js";
import {
  require_prop_types
} from "./chunk-FLAFLVMN.js";
import "./chunk-IHPEMKKY.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __commonJS,
  __toESM
} from "./chunk-QRPWKJ4C.js";

// node_modules/eventemitter3/index.js
var require_eventemitter3 = __commonJS({
  "node_modules/eventemitter3/index.js"(exports, module) {
    "use strict";
    var has = Object.prototype.hasOwnProperty;
    var prefix = "~";
    function Events() {
    }
    if (Object.create) {
      Events.prototype = /* @__PURE__ */ Object.create(null);
      if (!new Events().__proto__) prefix = false;
    }
    function EE(fn, context, once) {
      this.fn = fn;
      this.context = context;
      this.once = once || false;
    }
    function addListener(emitter, event, fn, context, once) {
      if (typeof fn !== "function") {
        throw new TypeError("The listener must be a function");
      }
      var listener = new EE(fn, context || emitter, once), evt = prefix ? prefix + event : event;
      if (!emitter._events[evt]) emitter._events[evt] = listener, emitter._eventsCount++;
      else if (!emitter._events[evt].fn) emitter._events[evt].push(listener);
      else emitter._events[evt] = [emitter._events[evt], listener];
      return emitter;
    }
    function clearEvent(emitter, evt) {
      if (--emitter._eventsCount === 0) emitter._events = new Events();
      else delete emitter._events[evt];
    }
    function EventEmitter() {
      this._events = new Events();
      this._eventsCount = 0;
    }
    EventEmitter.prototype.eventNames = function eventNames() {
      var names = [], events, name;
      if (this._eventsCount === 0) return names;
      for (name in events = this._events) {
        if (has.call(events, name)) names.push(prefix ? name.slice(1) : name);
      }
      if (Object.getOwnPropertySymbols) {
        return names.concat(Object.getOwnPropertySymbols(events));
      }
      return names;
    };
    EventEmitter.prototype.listeners = function listeners(event) {
      var evt = prefix ? prefix + event : event, handlers = this._events[evt];
      if (!handlers) return [];
      if (handlers.fn) return [handlers.fn];
      for (var i = 0, l2 = handlers.length, ee = new Array(l2); i < l2; i++) {
        ee[i] = handlers[i].fn;
      }
      return ee;
    };
    EventEmitter.prototype.listenerCount = function listenerCount(event) {
      var evt = prefix ? prefix + event : event, listeners = this._events[evt];
      if (!listeners) return 0;
      if (listeners.fn) return 1;
      return listeners.length;
    };
    EventEmitter.prototype.emit = function emit(event, a1, a2, a3, a4, a5) {
      var evt = prefix ? prefix + event : event;
      if (!this._events[evt]) return false;
      var listeners = this._events[evt], len = arguments.length, args, i;
      if (listeners.fn) {
        if (listeners.once) this.removeListener(event, listeners.fn, void 0, true);
        switch (len) {
          case 1:
            return listeners.fn.call(listeners.context), true;
          case 2:
            return listeners.fn.call(listeners.context, a1), true;
          case 3:
            return listeners.fn.call(listeners.context, a1, a2), true;
          case 4:
            return listeners.fn.call(listeners.context, a1, a2, a3), true;
          case 5:
            return listeners.fn.call(listeners.context, a1, a2, a3, a4), true;
          case 6:
            return listeners.fn.call(listeners.context, a1, a2, a3, a4, a5), true;
        }
        for (i = 1, args = new Array(len - 1); i < len; i++) {
          args[i - 1] = arguments[i];
        }
        listeners.fn.apply(listeners.context, args);
      } else {
        var length = listeners.length, j2;
        for (i = 0; i < length; i++) {
          if (listeners[i].once) this.removeListener(event, listeners[i].fn, void 0, true);
          switch (len) {
            case 1:
              listeners[i].fn.call(listeners[i].context);
              break;
            case 2:
              listeners[i].fn.call(listeners[i].context, a1);
              break;
            case 3:
              listeners[i].fn.call(listeners[i].context, a1, a2);
              break;
            case 4:
              listeners[i].fn.call(listeners[i].context, a1, a2, a3);
              break;
            default:
              if (!args) for (j2 = 1, args = new Array(len - 1); j2 < len; j2++) {
                args[j2 - 1] = arguments[j2];
              }
              listeners[i].fn.apply(listeners[i].context, args);
          }
        }
      }
      return true;
    };
    EventEmitter.prototype.on = function on(event, fn, context) {
      return addListener(this, event, fn, context, false);
    };
    EventEmitter.prototype.once = function once(event, fn, context) {
      return addListener(this, event, fn, context, true);
    };
    EventEmitter.prototype.removeListener = function removeListener(event, fn, context, once) {
      var evt = prefix ? prefix + event : event;
      if (!this._events[evt]) return this;
      if (!fn) {
        clearEvent(this, evt);
        return this;
      }
      var listeners = this._events[evt];
      if (listeners.fn) {
        if (listeners.fn === fn && (!once || listeners.once) && (!context || listeners.context === context)) {
          clearEvent(this, evt);
        }
      } else {
        for (var i = 0, events = [], length = listeners.length; i < length; i++) {
          if (listeners[i].fn !== fn || once && !listeners[i].once || context && listeners[i].context !== context) {
            events.push(listeners[i]);
          }
        }
        if (events.length) this._events[evt] = events.length === 1 ? events[0] : events;
        else clearEvent(this, evt);
      }
      return this;
    };
    EventEmitter.prototype.removeAllListeners = function removeAllListeners(event) {
      var evt;
      if (event) {
        evt = prefix ? prefix + event : event;
        if (this._events[evt]) clearEvent(this, evt);
      } else {
        this._events = new Events();
        this._eventsCount = 0;
      }
      return this;
    };
    EventEmitter.prototype.off = EventEmitter.prototype.removeListener;
    EventEmitter.prototype.addListener = EventEmitter.prototype.on;
    EventEmitter.prefixed = prefix;
    EventEmitter.EventEmitter = EventEmitter;
    if ("undefined" !== typeof module) {
      module.exports = EventEmitter;
    }
  }
});

// node_modules/@mapbox/point-geometry/index.js
var require_point_geometry = __commonJS({
  "node_modules/@mapbox/point-geometry/index.js"(exports, module) {
    "use strict";
    module.exports = Point;
    function Point(x2, y2) {
      this.x = x2;
      this.y = y2;
    }
    Point.prototype = {
      /**
       * Clone this point, returning a new point that can be modified
       * without affecting the old one.
       * @return {Point} the clone
       */
      clone: function() {
        return new Point(this.x, this.y);
      },
      /**
       * Add this point's x & y coordinates to another point,
       * yielding a new point.
       * @param {Point} p the other point
       * @return {Point} output point
       */
      add: function(p2) {
        return this.clone()._add(p2);
      },
      /**
       * Subtract this point's x & y coordinates to from point,
       * yielding a new point.
       * @param {Point} p the other point
       * @return {Point} output point
       */
      sub: function(p2) {
        return this.clone()._sub(p2);
      },
      /**
       * Multiply this point's x & y coordinates by point,
       * yielding a new point.
       * @param {Point} p the other point
       * @return {Point} output point
       */
      multByPoint: function(p2) {
        return this.clone()._multByPoint(p2);
      },
      /**
       * Divide this point's x & y coordinates by point,
       * yielding a new point.
       * @param {Point} p the other point
       * @return {Point} output point
       */
      divByPoint: function(p2) {
        return this.clone()._divByPoint(p2);
      },
      /**
       * Multiply this point's x & y coordinates by a factor,
       * yielding a new point.
       * @param {Point} k factor
       * @return {Point} output point
       */
      mult: function(k2) {
        return this.clone()._mult(k2);
      },
      /**
       * Divide this point's x & y coordinates by a factor,
       * yielding a new point.
       * @param {Point} k factor
       * @return {Point} output point
       */
      div: function(k2) {
        return this.clone()._div(k2);
      },
      /**
       * Rotate this point around the 0, 0 origin by an angle a,
       * given in radians
       * @param {Number} a angle to rotate around, in radians
       * @return {Point} output point
       */
      rotate: function(a2) {
        return this.clone()._rotate(a2);
      },
      /**
       * Rotate this point around p point by an angle a,
       * given in radians
       * @param {Number} a angle to rotate around, in radians
       * @param {Point} p Point to rotate around
       * @return {Point} output point
       */
      rotateAround: function(a2, p2) {
        return this.clone()._rotateAround(a2, p2);
      },
      /**
       * Multiply this point by a 4x1 transformation matrix
       * @param {Array<Number>} m transformation matrix
       * @return {Point} output point
       */
      matMult: function(m2) {
        return this.clone()._matMult(m2);
      },
      /**
       * Calculate this point but as a unit vector from 0, 0, meaning
       * that the distance from the resulting point to the 0, 0
       * coordinate will be equal to 1 and the angle from the resulting
       * point to the 0, 0 coordinate will be the same as before.
       * @return {Point} unit vector point
       */
      unit: function() {
        return this.clone()._unit();
      },
      /**
       * Compute a perpendicular point, where the new y coordinate
       * is the old x coordinate and the new x coordinate is the old y
       * coordinate multiplied by -1
       * @return {Point} perpendicular point
       */
      perp: function() {
        return this.clone()._perp();
      },
      /**
       * Return a version of this point with the x & y coordinates
       * rounded to integers.
       * @return {Point} rounded point
       */
      round: function() {
        return this.clone()._round();
      },
      /**
       * Return the magitude of this point: this is the Euclidean
       * distance from the 0, 0 coordinate to this point's x and y
       * coordinates.
       * @return {Number} magnitude
       */
      mag: function() {
        return Math.sqrt(this.x * this.x + this.y * this.y);
      },
      /**
       * Judge whether this point is equal to another point, returning
       * true or false.
       * @param {Point} other the other point
       * @return {boolean} whether the points are equal
       */
      equals: function(other) {
        return this.x === other.x && this.y === other.y;
      },
      /**
       * Calculate the distance from this point to another point
       * @param {Point} p the other point
       * @return {Number} distance
       */
      dist: function(p2) {
        return Math.sqrt(this.distSqr(p2));
      },
      /**
       * Calculate the distance from this point to another point,
       * without the square root step. Useful if you're comparing
       * relative distances.
       * @param {Point} p the other point
       * @return {Number} distance
       */
      distSqr: function(p2) {
        var dx = p2.x - this.x, dy = p2.y - this.y;
        return dx * dx + dy * dy;
      },
      /**
       * Get the angle from the 0, 0 coordinate to this point, in radians
       * coordinates.
       * @return {Number} angle
       */
      angle: function() {
        return Math.atan2(this.y, this.x);
      },
      /**
       * Get the angle from this point to another point, in radians
       * @param {Point} b the other point
       * @return {Number} angle
       */
      angleTo: function(b2) {
        return Math.atan2(this.y - b2.y, this.x - b2.x);
      },
      /**
       * Get the angle between this point and another point, in radians
       * @param {Point} b the other point
       * @return {Number} angle
       */
      angleWith: function(b2) {
        return this.angleWithSep(b2.x, b2.y);
      },
      /*
       * Find the angle of the two vectors, solving the formula for
       * the cross product a x b = |a||b|sin(θ) for θ.
       * @param {Number} x the x-coordinate
       * @param {Number} y the y-coordinate
       * @return {Number} the angle in radians
       */
      angleWithSep: function(x2, y2) {
        return Math.atan2(
          this.x * y2 - this.y * x2,
          this.x * x2 + this.y * y2
        );
      },
      _matMult: function(m2) {
        var x2 = m2[0] * this.x + m2[1] * this.y, y2 = m2[2] * this.x + m2[3] * this.y;
        this.x = x2;
        this.y = y2;
        return this;
      },
      _add: function(p2) {
        this.x += p2.x;
        this.y += p2.y;
        return this;
      },
      _sub: function(p2) {
        this.x -= p2.x;
        this.y -= p2.y;
        return this;
      },
      _mult: function(k2) {
        this.x *= k2;
        this.y *= k2;
        return this;
      },
      _div: function(k2) {
        this.x /= k2;
        this.y /= k2;
        return this;
      },
      _multByPoint: function(p2) {
        this.x *= p2.x;
        this.y *= p2.y;
        return this;
      },
      _divByPoint: function(p2) {
        this.x /= p2.x;
        this.y /= p2.y;
        return this;
      },
      _unit: function() {
        this._div(this.mag());
        return this;
      },
      _perp: function() {
        var y2 = this.y;
        this.y = this.x;
        this.x = -y2;
        return this;
      },
      _rotate: function(angle) {
        var cos = Math.cos(angle), sin = Math.sin(angle), x2 = cos * this.x - sin * this.y, y2 = sin * this.x + cos * this.y;
        this.x = x2;
        this.y = y2;
        return this;
      },
      _rotateAround: function(angle, p2) {
        var cos = Math.cos(angle), sin = Math.sin(angle), x2 = p2.x + cos * (this.x - p2.x) - sin * (this.y - p2.y), y2 = p2.y + sin * (this.x - p2.x) + cos * (this.y - p2.y);
        this.x = x2;
        this.y = y2;
        return this;
      },
      _round: function() {
        this.x = Math.round(this.x);
        this.y = Math.round(this.y);
        return this;
      }
    };
    Point.convert = function(a2) {
      if (a2 instanceof Point) {
        return a2;
      }
      if (Array.isArray(a2)) {
        return new Point(a2[0], a2[1]);
      }
      return a2;
    };
  }
});

// node_modules/google-map-react/dist/index.modern.js
var import_react = __toESM(require_react());
var import_prop_types = __toESM(require_prop_types());
var import_react_dom = __toESM(require_react_dom());
var import_eventemitter3 = __toESM(require_eventemitter3());

// node_modules/@googlemaps/js-api-loader/dist/index.mjs
function __awaiter(thisArg, _arguments, P2, generator) {
  function adopt(value) {
    return value instanceof P2 ? value : new P2(function(resolve) {
      resolve(value);
    });
  }
  return new (P2 || (P2 = Promise))(function(resolve, reject) {
    function fulfilled(value) {
      try {
        step(generator.next(value));
      } catch (e2) {
        reject(e2);
      }
    }
    function rejected(value) {
      try {
        step(generator["throw"](value));
      } catch (e2) {
        reject(e2);
      }
    }
    function step(result) {
      result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected);
    }
    step((generator = generator.apply(thisArg, _arguments || [])).next());
  });
}
function getDefaultExportFromCjs(x2) {
  return x2 && x2.__esModule && Object.prototype.hasOwnProperty.call(x2, "default") ? x2["default"] : x2;
}
var fastDeepEqual = function equal(a2, b2) {
  if (a2 === b2) return true;
  if (a2 && b2 && typeof a2 == "object" && typeof b2 == "object") {
    if (a2.constructor !== b2.constructor) return false;
    var length, i, keys;
    if (Array.isArray(a2)) {
      length = a2.length;
      if (length != b2.length) return false;
      for (i = length; i-- !== 0; )
        if (!equal(a2[i], b2[i])) return false;
      return true;
    }
    if (a2.constructor === RegExp) return a2.source === b2.source && a2.flags === b2.flags;
    if (a2.valueOf !== Object.prototype.valueOf) return a2.valueOf() === b2.valueOf();
    if (a2.toString !== Object.prototype.toString) return a2.toString() === b2.toString();
    keys = Object.keys(a2);
    length = keys.length;
    if (length !== Object.keys(b2).length) return false;
    for (i = length; i-- !== 0; )
      if (!Object.prototype.hasOwnProperty.call(b2, keys[i])) return false;
    for (i = length; i-- !== 0; ) {
      var key = keys[i];
      if (!equal(a2[key], b2[key])) return false;
    }
    return true;
  }
  return a2 !== a2 && b2 !== b2;
};
var isEqual = /* @__PURE__ */ getDefaultExportFromCjs(fastDeepEqual);
var DEFAULT_ID = "__googleMapsScriptId";
var LoaderStatus;
(function(LoaderStatus2) {
  LoaderStatus2[LoaderStatus2["INITIALIZED"] = 0] = "INITIALIZED";
  LoaderStatus2[LoaderStatus2["LOADING"] = 1] = "LOADING";
  LoaderStatus2[LoaderStatus2["SUCCESS"] = 2] = "SUCCESS";
  LoaderStatus2[LoaderStatus2["FAILURE"] = 3] = "FAILURE";
})(LoaderStatus || (LoaderStatus = {}));
var Loader = class _Loader {
  /**
   * Creates an instance of Loader using [[LoaderOptions]]. No defaults are set
   * using this library, instead the defaults are set by the Google Maps
   * JavaScript API server.
   *
   * ```
   * const loader = Loader({apiKey, version: 'weekly', libraries: ['places']});
   * ```
   */
  constructor({ apiKey, authReferrerPolicy, channel, client, id = DEFAULT_ID, language, libraries = [], mapIds, nonce, region, retries = 3, url = "https://maps.googleapis.com/maps/api/js", version }) {
    this.callbacks = [];
    this.done = false;
    this.loading = false;
    this.errors = [];
    this.apiKey = apiKey;
    this.authReferrerPolicy = authReferrerPolicy;
    this.channel = channel;
    this.client = client;
    this.id = id || DEFAULT_ID;
    this.language = language;
    this.libraries = libraries;
    this.mapIds = mapIds;
    this.nonce = nonce;
    this.region = region;
    this.retries = retries;
    this.url = url;
    this.version = version;
    if (_Loader.instance) {
      if (!isEqual(this.options, _Loader.instance.options)) {
        throw new Error(`Loader must not be called again with different options. ${JSON.stringify(this.options)} !== ${JSON.stringify(_Loader.instance.options)}`);
      }
      return _Loader.instance;
    }
    _Loader.instance = this;
  }
  get options() {
    return {
      version: this.version,
      apiKey: this.apiKey,
      channel: this.channel,
      client: this.client,
      id: this.id,
      libraries: this.libraries,
      language: this.language,
      region: this.region,
      mapIds: this.mapIds,
      nonce: this.nonce,
      url: this.url,
      authReferrerPolicy: this.authReferrerPolicy
    };
  }
  get status() {
    if (this.errors.length) {
      return LoaderStatus.FAILURE;
    }
    if (this.done) {
      return LoaderStatus.SUCCESS;
    }
    if (this.loading) {
      return LoaderStatus.LOADING;
    }
    return LoaderStatus.INITIALIZED;
  }
  get failed() {
    return this.done && !this.loading && this.errors.length >= this.retries + 1;
  }
  /**
   * CreateUrl returns the Google Maps JavaScript API script url given the [[LoaderOptions]].
   *
   * @ignore
   * @deprecated
   */
  createUrl() {
    let url = this.url;
    url += `?callback=__googleMapsCallback&loading=async`;
    if (this.apiKey) {
      url += `&key=${this.apiKey}`;
    }
    if (this.channel) {
      url += `&channel=${this.channel}`;
    }
    if (this.client) {
      url += `&client=${this.client}`;
    }
    if (this.libraries.length > 0) {
      url += `&libraries=${this.libraries.join(",")}`;
    }
    if (this.language) {
      url += `&language=${this.language}`;
    }
    if (this.region) {
      url += `&region=${this.region}`;
    }
    if (this.version) {
      url += `&v=${this.version}`;
    }
    if (this.mapIds) {
      url += `&map_ids=${this.mapIds.join(",")}`;
    }
    if (this.authReferrerPolicy) {
      url += `&auth_referrer_policy=${this.authReferrerPolicy}`;
    }
    return url;
  }
  deleteScript() {
    const script = document.getElementById(this.id);
    if (script) {
      script.remove();
    }
  }
  /**
   * Load the Google Maps JavaScript API script and return a Promise.
   * @deprecated, use importLibrary() instead.
   */
  load() {
    return this.loadPromise();
  }
  /**
   * Load the Google Maps JavaScript API script and return a Promise.
   *
   * @ignore
   * @deprecated, use importLibrary() instead.
   */
  loadPromise() {
    return new Promise((resolve, reject) => {
      this.loadCallback((err) => {
        if (!err) {
          resolve(window.google);
        } else {
          reject(err.error);
        }
      });
    });
  }
  importLibrary(name) {
    this.execute();
    return google.maps.importLibrary(name);
  }
  /**
   * Load the Google Maps JavaScript API script with a callback.
   * @deprecated, use importLibrary() instead.
   */
  loadCallback(fn) {
    this.callbacks.push(fn);
    this.execute();
  }
  /**
   * Set the script on document.
   */
  setScript() {
    var _a, _b;
    if (document.getElementById(this.id)) {
      this.callback();
      return;
    }
    const params = {
      key: this.apiKey,
      channel: this.channel,
      client: this.client,
      libraries: this.libraries.length && this.libraries,
      v: this.version,
      mapIds: this.mapIds,
      language: this.language,
      region: this.region,
      authReferrerPolicy: this.authReferrerPolicy
    };
    Object.keys(params).forEach(
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      (key) => !params[key] && delete params[key]
    );
    if (!((_b = (_a = window === null || window === void 0 ? void 0 : window.google) === null || _a === void 0 ? void 0 : _a.maps) === null || _b === void 0 ? void 0 : _b.importLibrary)) {
      ((g2) => {
        let h2, a2, k2, p2 = "The Google Maps JavaScript API", c2 = "google", l2 = "importLibrary", q = "__ib__", m2 = document, b2 = window;
        b2 = b2[c2] || (b2[c2] = {});
        const d2 = b2.maps || (b2.maps = {}), r2 = /* @__PURE__ */ new Set(), e2 = new URLSearchParams(), u2 = () => (
          // @ts-ignore
          h2 || (h2 = new Promise((f2, n2) => __awaiter(this, void 0, void 0, function* () {
            var _a2;
            yield a2 = m2.createElement("script");
            a2.id = this.id;
            e2.set("libraries", [...r2] + "");
            for (k2 in g2)
              e2.set(k2.replace(/[A-Z]/g, (t2) => "_" + t2[0].toLowerCase()), g2[k2]);
            e2.set("callback", c2 + ".maps." + q);
            a2.src = this.url + `?` + e2;
            d2[q] = f2;
            a2.onerror = () => h2 = n2(Error(p2 + " could not load."));
            a2.nonce = this.nonce || ((_a2 = m2.querySelector("script[nonce]")) === null || _a2 === void 0 ? void 0 : _a2.nonce) || "";
            m2.head.append(a2);
          })))
        );
        d2[l2] ? console.warn(p2 + " only loads once. Ignoring:", g2) : d2[l2] = (f2, ...n2) => r2.add(f2) && u2().then(() => d2[l2](f2, ...n2));
      })(params);
    }
    const libraryPromises = this.libraries.map((library) => this.importLibrary(library));
    if (!libraryPromises.length) {
      libraryPromises.push(this.importLibrary("core"));
    }
    Promise.all(libraryPromises).then(() => this.callback(), (error) => {
      const event = new ErrorEvent("error", { error });
      this.loadErrorCallback(event);
    });
  }
  /**
   * Reset the loader state.
   */
  reset() {
    this.deleteScript();
    this.done = false;
    this.loading = false;
    this.errors = [];
    this.onerrorEvent = null;
  }
  resetIfRetryingFailed() {
    if (this.failed) {
      this.reset();
    }
  }
  loadErrorCallback(e2) {
    this.errors.push(e2);
    if (this.errors.length <= this.retries) {
      const delay = this.errors.length * Math.pow(2, this.errors.length);
      console.error(`Failed to load Google Maps script, retrying in ${delay} ms.`);
      setTimeout(() => {
        this.deleteScript();
        this.setScript();
      }, delay);
    } else {
      this.onerrorEvent = e2;
      this.callback();
    }
  }
  callback() {
    this.done = true;
    this.loading = false;
    this.callbacks.forEach((cb) => {
      cb(this.onerrorEvent);
    });
    this.callbacks = [];
  }
  execute() {
    this.resetIfRetryingFailed();
    if (this.loading) {
      return;
    }
    if (this.done) {
      this.callback();
    } else {
      if (window.google && window.google.maps && window.google.maps.version) {
        console.warn("Google Maps already loaded outside @googlemaps/js-api-loader. This may result in undesirable behavior as options and script parameters may not match.");
        this.callback();
        return;
      }
      this.loading = true;
      this.setScript();
    }
  }
};

// node_modules/google-map-react/dist/index.modern.js
var import_point_geometry = __toESM(require_point_geometry());
function a() {
  return (a = Object.assign ? Object.assign.bind() : function(e2) {
    for (var t2 = 1; t2 < arguments.length; t2++) {
      var o2 = arguments[t2];
      for (var n2 in o2) Object.prototype.hasOwnProperty.call(o2, n2) && (e2[n2] = o2[n2]);
    }
    return e2;
  }).apply(this, arguments);
}
function p(e2, t2) {
  var o2, n2;
  e2.prototype = Object.create(t2.prototype), e2.prototype.constructor = e2, o2 = e2, n2 = t2, (Object.setPrototypeOf ? Object.setPrototypeOf.bind() : function(e3, t3) {
    return e3.__proto__ = t3, e3;
  })(o2, n2);
}
function l(e2) {
  if (void 0 === e2) throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
  return e2;
}
var u = { width: "100%", height: "100%", left: 0, top: 0, margin: 0, padding: 0, position: "absolute" };
var h = function(t2) {
  function o2() {
    return t2.apply(this, arguments) || this;
  }
  p(o2, t2);
  var n2 = o2.prototype;
  return n2.shouldComponentUpdate = function() {
    return false;
  }, n2.render = function() {
    return import_react.default.createElement("div", { ref: this.props.registerChild, style: u });
  }, o2;
}(import_react.Component);
var c = function(e2) {
  function t2(t3) {
    var o3;
    return (o3 = e2.call(this) || this).gmapInstance = t3, o3;
  }
  p(t2, e2);
  var o2 = t2.prototype;
  return o2.getChildren = function() {
    return this.gmapInstance.props.children;
  }, o2.getMousePosition = function() {
    return this.gmapInstance.mouse_;
  }, o2.getUpdateCounter = function() {
    return this.gmapInstance.updateCounter_;
  }, o2.dispose = function() {
    this.gmapInstance = null, this.removeAllListeners();
  }, t2;
}(import_eventemitter3.default);
var d = function(e2, t2) {
  for (var o2 = a({}, (function(e3) {
    if (null == e3) throw new TypeError("Cannot destructure " + e3);
  }(e2), e2)), n2 = 0; n2 < t2.length; n2++) {
    var r2 = t2[n2];
    r2 in o2 && delete o2[r2];
  }
  return o2;
};
var m = Object.prototype.hasOwnProperty;
function g(e2, t2) {
  return e2 === t2 ? 0 !== e2 || 0 !== t2 || 1 / e2 == 1 / t2 : e2 != e2 && t2 != t2;
}
function _(e2, t2) {
  if (g(e2, t2)) return true;
  if ("object" != typeof e2 || null === e2 || "object" != typeof t2 || null === t2) return false;
  var o2 = Object.keys(e2), n2 = Object.keys(t2);
  if (o2.length !== n2.length) return false;
  for (var r2 = 0; r2 < o2.length; r2++) if (!m.call(t2, o2[r2]) || !g(e2[o2[r2]], t2[o2[r2]])) return false;
  return true;
}
var f = { width: "100%", height: "100%", left: 0, top: 0, margin: 0, padding: 0, position: "absolute" };
var v = { width: 0, height: 0, left: 0, top: 0, backgroundColor: "transparent", position: "absolute" };
var M = function(t2) {
  function o2(o3) {
    var n3;
    return (n3 = t2.call(this, o3) || this)._getState = function() {
      return { children: n3.props.dispatcher.getChildren(), updateCounter: n3.props.dispatcher.getUpdateCounter() };
    }, n3._onChangeHandler = function() {
      if (n3.dimensionsCache_) {
        var e2 = (n3.state.children || []).length, t3 = n3._getState();
        n3.setState(t3, function() {
          return (t3.children || []).length !== e2 && n3._onMouseChangeHandler();
        });
      }
    }, n3._onChildClick = function() {
      n3.props.onChildClick && n3.hoverChildProps_ && n3.props.onChildClick(n3.hoverKey_, n3.hoverChildProps_);
    }, n3._onChildMouseDown = function() {
      n3.props.onChildMouseDown && n3.hoverChildProps_ && n3.props.onChildMouseDown(n3.hoverKey_, n3.hoverChildProps_);
    }, n3._onChildMouseEnter = function(e2, t3) {
      n3.dimensionsCache_ && (n3.props.onChildMouseEnter && n3.props.onChildMouseEnter(e2, t3), n3.hoverChildProps_ = t3, n3.hoverKey_ = e2, n3.setState({ hoverKey: e2 }));
    }, n3._onChildMouseLeave = function() {
      if (n3.dimensionsCache_) {
        var e2 = n3.hoverKey_;
        null != e2 && (n3.props.onChildMouseLeave && n3.props.onChildMouseLeave(e2, n3.hoverChildProps_), n3.hoverKey_ = null, n3.hoverChildProps_ = null, n3.setState({ hoverKey: null }));
      }
    }, n3._onMouseAllow = function(e2) {
      e2 || n3._onChildMouseLeave(), n3.allowMouse_ = e2;
    }, n3._onMouseChangeHandler = function() {
      n3.allowMouse_ && n3._onMouseChangeHandlerRaf();
    }, n3._onMouseChangeHandlerRaf = function() {
      if (n3.dimensionsCache_) {
        var t3 = n3.props.dispatcher.getMousePosition();
        if (t3) {
          var o4 = [], r2 = n3.props.getHoverDistance();
          if (import_react.default.Children.forEach(n3.state.children, function(e2, i2) {
            if (e2 && (void 0 !== e2.props.latLng || void 0 !== e2.props.lat || void 0 !== e2.props.lng)) {
              var s3 = null != e2.key ? e2.key : i2, a2 = n3.props.distanceToMouse(n3.dimensionsCache_[s3], t3, e2.props);
              a2 < r2 && o4.push({ key: s3, dist: a2, props: e2.props });
            }
          }), o4.length) {
            o4.sort(function(e2, t4) {
              return e2.dist - t4.dist;
            });
            var i = o4[0].key, s2 = o4[0].props;
            n3.hoverKey_ !== i && (n3._onChildMouseLeave(), n3._onChildMouseEnter(i, s2));
          } else n3._onChildMouseLeave();
        } else n3._onChildMouseLeave();
      }
    }, n3._getDimensions = function(e2) {
      return n3.dimensionsCache_[e2];
    }, n3.dimensionsCache_ = {}, n3.hoverKey_ = null, n3.hoverChildProps_ = null, n3.allowMouse_ = true, n3.state = a({}, n3._getState(), { hoverKey: null }), n3;
  }
  p(o2, t2);
  var n2 = o2.prototype;
  return n2.componentDidMount = function() {
    this.props.dispatcher.on("kON_CHANGE", this._onChangeHandler), this.props.dispatcher.on("kON_MOUSE_POSITION_CHANGE", this._onMouseChangeHandler), this.props.dispatcher.on("kON_CLICK", this._onChildClick), this.props.dispatcher.on("kON_MDOWN", this._onChildMouseDown);
  }, n2.shouldComponentUpdate = function(e2, t3) {
    return true === this.props.experimental ? !_(this.props, e2) || !_(d(this.state, ["hoverKey"]), d(t3, ["hoverKey"])) : !_(this.props, e2) || !_(this.state, t3);
  }, n2.componentWillUnmount = function() {
    this.props.dispatcher.removeListener("kON_CHANGE", this._onChangeHandler), this.props.dispatcher.removeListener("kON_MOUSE_POSITION_CHANGE", this._onMouseChangeHandler), this.props.dispatcher.removeListener("kON_CLICK", this._onChildClick), this.props.dispatcher.removeListener("kON_MDOWN", this._onChildMouseDown), this.dimensionsCache_ = null;
  }, n2.render = function() {
    var t3 = this, o3 = this.props.style || f;
    this.dimensionsCache_ = {};
    var n3 = import_react.default.Children.map(this.state.children, function(o4, n4) {
      if (o4) {
        if (void 0 === o4.props.latLng && void 0 === o4.props.lat && void 0 === o4.props.lng) return import_react.default.cloneElement(o4, { $geoService: t3.props.geoService, $onMouseAllow: t3._onMouseAllow, $prerender: t3.props.prerender });
        var r2 = void 0 !== o4.props.latLng ? o4.props.latLng : { lat: o4.props.lat, lng: o4.props.lng }, i = t3.props.insideMapPanes ? t3.props.geoService.fromLatLngToDivPixel(r2) : t3.props.geoService.fromLatLngToCenterPixel(r2), s2 = { left: i.x, top: i.y };
        if (void 0 !== o4.props.seLatLng || void 0 !== o4.props.seLat && void 0 !== o4.props.seLng) {
          var p2 = void 0 !== o4.props.seLatLng ? o4.props.seLatLng : { lat: o4.props.seLat, lng: o4.props.seLng }, l2 = t3.props.insideMapPanes ? t3.props.geoService.fromLatLngToDivPixel(p2) : t3.props.geoService.fromLatLngToCenterPixel(p2);
          s2.width = l2.x - i.x, s2.height = l2.y - i.y;
        }
        var u2 = t3.props.geoService.fromLatLngToContainerPixel(r2), h2 = null != o4.key ? o4.key : n4;
        return t3.dimensionsCache_[h2] = a({ x: u2.x, y: u2.y }, r2), import_react.default.createElement("div", { key: h2, style: a({}, v, s2), className: o4.props.$markerHolderClassName }, import_react.default.cloneElement(o4, { $hover: h2 === t3.state.hoverKey, $getDimensions: t3._getDimensions, $dimensionKey: h2, $geoService: t3.props.geoService, $onMouseAllow: t3._onMouseAllow, $prerender: t3.props.prerender }));
      }
    });
    return import_react.default.createElement("div", { style: o3 }, n3);
  }, o2;
}(import_react.Component);
M.propTypes = { geoService: import_prop_types.default.any, style: import_prop_types.default.any, distanceToMouse: import_prop_types.default.func, dispatcher: import_prop_types.default.any, onChildClick: import_prop_types.default.func, onChildMouseDown: import_prop_types.default.func, onChildMouseLeave: import_prop_types.default.func, onChildMouseEnter: import_prop_types.default.func, getHoverDistance: import_prop_types.default.func, insideMapPanes: import_prop_types.default.bool, prerender: import_prop_types.default.bool }, M.defaultProps = { insideMapPanes: false, prerender: false };
var y = { width: "50%", height: "50%", left: "50%", top: "50%", margin: 0, padding: 0, position: "absolute" };
function C(t2) {
  return import_react.default.createElement("div", { style: y }, import_react.default.createElement(M, a({}, t2, { prerender: true })));
}
var w;
var L;
var b;
var D = ["key"];
var z = new Promise(function(e2) {
  b = e2;
});
var O = function(e2, t2) {
  if (!e2) return z;
  if (L) return L;
  e2.libraries || (e2.libraries = []);
  var o2 = [].concat(e2.libraries);
  if (t2 && (0 !== o2.length && o2.includes("visualization") || o2.push("visualization"), console.warn("heatmapLibrary will be deprecated in the future. Please use { libraries: ['visualization'] } in bootstrapURLKeys property instead")), Object.keys(e2).indexOf("callback") > -1) {
    var n2 = '"callback" key in bootstrapURLKeys is not allowed,\n                      use onGoogleApiLoaded property instead';
    throw console.error(n2), new Error(n2);
  }
  if ("undefined" == typeof window) throw new Error("google map cannot be loaded outside browser env");
  var r2 = e2.key, s2 = function(e3, t3) {
    if (null == e3) return {};
    var o3, n3, r3 = {}, i = Object.keys(e3);
    for (n3 = 0; n3 < i.length; n3++) t3.indexOf(o3 = i[n3]) >= 0 || (r3[o3] = e3[o3]);
    return r3;
  }(e2, D);
  return w || (w = new Loader(a({ apiKey: r2 || "" }, s2, { libraries: o2 }))), L = w.load().then(function() {
    return b(window.google.maps), window.google.maps;
  }), b(L), L;
};
function k(e2, t2, o2) {
  var n2 = o2 - t2;
  return e2 === o2 ? e2 : ((e2 - t2) % n2 + n2) % n2 + t2;
}
var x = function() {
  function e2(e3, t2) {
    if (isNaN(e3) || isNaN(t2)) throw new Error("Invalid LatLng object: (" + e3 + ", " + t2 + ")");
    this.lat = +e3, this.lng = +t2;
  }
  return e2.prototype.wrap = function() {
    return new e2(this.lat, k(this.lng, -180, 180));
  }, e2;
}();
x.convert = function(e2) {
  return e2 instanceof x ? e2 : Array.isArray(e2) ? new x(e2[0], e2[1]) : "lng" in e2 && "lat" in e2 ? new x(e2.lat, e2.lng) : e2;
};
var S = function() {
  function e2(e3, t3, o3) {
    this.tileSize = e3 || 512, this._minZoom = t3 || 0, this._maxZoom = o3 || 52, this.latRange = [-85.05113, 85.05113], this.width = 0, this.height = 0, this.zoom = 0, this.center = new x(0, 0), this.angle = 0;
  }
  var t2, o2, n2 = e2.prototype;
  return n2.zoomScale = function(e3) {
    return Math.pow(2, e3);
  }, n2.scaleZoom = function(e3) {
    return Math.log(e3) / Math.LN2;
  }, n2.project = function(e3, t3) {
    return new import_point_geometry.default(this.lngX(e3.lng, t3), this.latY(e3.lat, t3));
  }, n2.unproject = function(e3, t3) {
    return new x(this.yLat(e3.y, t3), this.xLng(e3.x, t3));
  }, n2.lngX = function(e3, t3) {
    return (180 + e3) * (t3 || this.worldSize) / 360;
  }, n2.latY = function(e3, t3) {
    return (180 - 180 / Math.PI * Math.log(Math.tan(Math.PI / 4 + e3 * Math.PI / 360))) * (t3 || this.worldSize) / 360;
  }, n2.xLng = function(e3, t3) {
    return 360 * e3 / (t3 || this.worldSize) - 180;
  }, n2.yLat = function(e3, t3) {
    return 360 / Math.PI * Math.atan(Math.exp((180 - 360 * e3 / (t3 || this.worldSize)) * Math.PI / 180)) - 90;
  }, n2.locationPoint = function(e3) {
    var t3 = this.project(e3);
    return this.centerPoint._sub(this.point._sub(t3)._rotate(this.angle));
  }, n2.pointLocation = function(e3) {
    var t3 = this.centerPoint._sub(e3)._rotate(-this.angle);
    return this.unproject(this.point.sub(t3));
  }, t2 = e2, (o2 = [{ key: "minZoom", get: function() {
    return this._minZoom;
  }, set: function(e3) {
    this._minZoom = e3, this.zoom = Math.max(this.zoom, e3);
  } }, { key: "maxZoom", get: function() {
    return this._maxZoom;
  }, set: function(e3) {
    this._maxZoom = e3, this.zoom = Math.min(this.zoom, e3);
  } }, { key: "worldSize", get: function() {
    return this.tileSize * this.scale;
  } }, { key: "centerPoint", get: function() {
    return new import_point_geometry.default(0, 0);
  } }, { key: "size", get: function() {
    return new import_point_geometry.default(this.width, this.height);
  } }, { key: "bearing", get: function() {
    return -this.angle / Math.PI * 180;
  }, set: function(e3) {
    this.angle = -k(e3, -180, 180) * Math.PI / 180;
  } }, { key: "zoom", get: function() {
    return this._zoom;
  }, set: function(e3) {
    var t3 = Math.min(Math.max(e3, this.minZoom), this.maxZoom);
    this._zoom = t3, this.scale = this.zoomScale(t3), this.tileZoom = Math.floor(t3), this.zoomFraction = t3 - this.tileZoom;
  } }, { key: "x", get: function() {
    return this.lngX(this.center.lng);
  } }, { key: "y", get: function() {
    return this.latY(this.center.lat);
  } }, { key: "point", get: function() {
    return new import_point_geometry.default(this.x, this.y);
  } }]) && function(e3, t3) {
    for (var o3 = 0; o3 < t3.length; o3++) {
      var n3 = t3[o3];
      n3.enumerable = n3.enumerable || false, n3.configurable = true, "value" in n3 && (n3.writable = true), Object.defineProperty(e3, "symbol" == typeof (r2 = function(e4, t4) {
        if ("object" != typeof e4 || null === e4) return e4;
        var o4 = e4[Symbol.toPrimitive];
        if (void 0 !== o4) {
          var n4 = o4.call(e4, "string");
          if ("object" != typeof n4) return n4;
          throw new TypeError("@@toPrimitive must return a primitive value.");
        }
        return String(e4);
      }(n3.key)) ? r2 : String(r2), n3);
    }
    var r2;
  }(t2.prototype, o2), Object.defineProperty(t2, "prototype", { writable: false }), e2;
}();
var T = function() {
  function e2(e3) {
    this.hasSize_ = false, this.hasView_ = false, this.transform_ = new S(e3 || 512);
  }
  var t2 = e2.prototype;
  return t2.setView = function(e3, t3, o2) {
    this.transform_.center = x.convert(e3), this.transform_.zoom = +t3, this.transform_.bearing = +o2, this.hasView_ = true;
  }, t2.setViewSize = function(e3, t3) {
    this.transform_.width = e3, this.transform_.height = t3, this.hasSize_ = true;
  }, t2.setMapCanvasProjection = function(e3, t3) {
    this.maps_ = e3, this.mapCanvasProjection_ = t3;
  }, t2.canProject = function() {
    return this.hasSize_ && this.hasView_;
  }, t2.hasSize = function() {
    return this.hasSize_;
  }, t2.fromLatLngToCenterPixel = function(e3) {
    return this.transform_.locationPoint(x.convert(e3));
  }, t2.fromLatLngToDivPixel = function(e3) {
    if (this.mapCanvasProjection_) {
      var t3 = new this.maps_.LatLng(e3.lat, e3.lng);
      return this.mapCanvasProjection_.fromLatLngToDivPixel(t3);
    }
    return this.fromLatLngToCenterPixel(e3);
  }, t2.fromLatLngToContainerPixel = function(e3) {
    if (this.mapCanvasProjection_) {
      var t3 = new this.maps_.LatLng(e3.lat, e3.lng);
      return this.mapCanvasProjection_.fromLatLngToContainerPixel(t3);
    }
    var o2 = this.fromLatLngToCenterPixel(e3);
    return o2.x -= this.transform_.worldSize * Math.round(o2.x / this.transform_.worldSize), o2.x += this.transform_.width / 2, o2.y += this.transform_.height / 2, o2;
  }, t2.fromContainerPixelToLatLng = function(e3) {
    if (this.mapCanvasProjection_) {
      var t3 = this.mapCanvasProjection_.fromContainerPixelToLatLng(e3);
      return { lat: t3.lat(), lng: t3.lng() };
    }
    var o2 = a({}, e3);
    o2.x -= this.transform_.width / 2, o2.y -= this.transform_.height / 2;
    var n2 = this.transform_.pointLocation(import_point_geometry.default.convert(o2));
    return n2.lng -= 360 * Math.round(n2.lng / 360), n2;
  }, t2.getWidth = function() {
    return this.transform_.width;
  }, t2.getHeight = function() {
    return this.transform_.height;
  }, t2.getZoom = function() {
    return this.transform_.zoom;
  }, t2.getCenter = function() {
    return this.transform_.pointLocation({ x: 0, y: 0 });
  }, t2.getBounds = function(e3, t3) {
    var o2 = e3 && e3[0] || 0, n2 = e3 && e3[1] || 0, r2 = e3 && e3[2] || 0, i = e3 && e3[3] || 0;
    if (this.getWidth() - n2 - i > 0 && this.getHeight() - o2 - r2 > 0) {
      var a2 = this.transform_.pointLocation(import_point_geometry.default.convert({ x: i - this.getWidth() / 2, y: o2 - this.getHeight() / 2 })), p2 = this.transform_.pointLocation(import_point_geometry.default.convert({ x: this.getWidth() / 2 - n2, y: this.getHeight() / 2 - r2 })), l2 = [a2.lat, a2.lng, p2.lat, p2.lng, p2.lat, a2.lng, a2.lat, p2.lng];
      return t3 && (l2 = l2.map(function(e4) {
        return Math.round(e4 * t3) / t3;
      })), l2;
    }
    return [0, 0, 0, 0];
  }, e2;
}();
function E(e2) {
  if (window.requestAnimationFrame) return window.requestAnimationFrame(e2);
  var t2 = window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame;
  return t2 ? t2(e2) : window.setTimeout(e2, 1e3 / 60);
}
var P = Math.log2 ? Math.log2 : function(e2) {
  return Math.log(e2) / Math.LN2;
};
function A(e2, t2) {
  return Object.keys(e2).reduce(function(o2, n2) {
    return t2(e2[n2]) && (o2[n2] = e2[n2]), o2;
  }, {});
}
var I = function(e2) {
  if (null !== e2 && "object" == typeof e2) {
    if (0 === Object.keys(e2).length) return true;
  } else if (null == e2 || "" === e2) return true;
  return false;
};
var N = Object.prototype.toString;
function Z(e2) {
  return "number" == typeof e2 || /* @__PURE__ */ function(e3) {
    return !!e3 && "object" == typeof e3;
  }(e2) && "[object Number]" === N.call(e2);
}
var j = null;
function U() {
  if (j) return j;
  if ("undefined" != typeof navigator) {
    var e2 = navigator.userAgent.indexOf("MSIE") > -1, t2 = navigator.userAgent.indexOf("Firefox") > -1, o2 = navigator.userAgent.toLowerCase().indexOf("op") > -1, n2 = navigator.userAgent.indexOf("Chrome") > -1, r2 = navigator.userAgent.indexOf("Safari") > -1;
    return n2 && r2 && (r2 = false), n2 && o2 && (n2 = false), j = { isExplorer: e2, isFirefox: t2, isOpera: o2, isChrome: n2, isSafari: r2 };
  }
  return j = { isChrome: true, isExplorer: false, isFirefox: false, isOpera: false, isSafari: false };
}
var H = function(e2) {
  return Function.prototype.toString.call(e2);
};
function K(e2) {
  if (!e2 || "object" != typeof e2) return false;
  var t2 = "function" == typeof e2.constructor ? Object.getPrototypeOf(e2) : Object.prototype;
  if (null === t2) return true;
  var o2 = t2.constructor;
  return "function" == typeof o2 && o2 instanceof o2 && H(o2) === H(Object);
}
function R(e2, t2, o2, n2) {
  e2.addEventListener(t2, o2, function() {
    var e3 = false;
    try {
      var t3 = Object.defineProperty({}, "passive", { get: function() {
        e3 = true;
      } });
      window.addEventListener("test", t3, t3), window.removeEventListener("test", t3, t3);
    } catch (t4) {
      e3 = false;
    }
    return e3;
  }() ? { capture: n2, passive: true } : n2);
}
var G;
var B = !("undefined" == typeof window || !window.document || !window.document.createElement);
G = B ? window : "undefined" != typeof self ? self : void 0;
var W;
var V = "undefined" != typeof document && document.attachEvent;
var F = false;
if (B && !V) {
  $ = function() {
    var e2 = G.requestAnimationFrame || G.mozRequestAnimationFrame || G.webkitRequestAnimationFrame || function(e3) {
      return G.setTimeout(e3, 20);
    };
    return function(t2) {
      return e2(t2);
    };
  }(), q = (W = G.cancelAnimationFrame || G.mozCancelAnimationFrame || G.webkitCancelAnimationFrame || G.clearTimeout, function(e2) {
    return W(e2);
  }), Y = function(e2) {
    var t2 = e2.__resizeTriggers__, o2 = t2.firstElementChild, n2 = t2.lastElementChild, r2 = o2.firstElementChild;
    n2.scrollLeft = n2.scrollWidth, n2.scrollTop = n2.scrollHeight, r2.style.width = o2.offsetWidth + 1 + "px", r2.style.height = o2.offsetHeight + 1 + "px", o2.scrollLeft = o2.scrollWidth, o2.scrollTop = o2.scrollHeight;
  }, X = function(e2) {
    var t2 = this;
    Y(this), this.__resizeRAF__ && q(this.__resizeRAF__), this.__resizeRAF__ = $(function() {
      (function(e3) {
        return e3.offsetWidth != e3.__resizeLast__.width || e3.offsetHeight != e3.__resizeLast__.height;
      })(t2) && (t2.__resizeLast__.width = t2.offsetWidth, t2.__resizeLast__.height = t2.offsetHeight, t2.__resizeListeners__.forEach(function(o2) {
        o2.call(t2, e2);
      }));
    });
  }, J = false, Q = "", ee = "animationstart", te = "Webkit Moz O ms".split(" "), oe = "webkitAnimationStart animationstart oAnimationStart MSAnimationStart".split(" ");
  if (B) {
    ne = document.createElement("fakeelement");
    if (void 0 !== ne.style.animationName && (J = true), false === J) {
      for (re = 0; re < te.length; re++) if (void 0 !== ne.style[te[re] + "AnimationName"]) {
        Q = "-" + te[re].toLowerCase() + "-", ee = oe[re], J = true;
        break;
      }
    }
  }
  ie = "resizeanim", se = "@" + Q + "keyframes " + ie + " { from { opacity: 0; } to { opacity: 0; } } ", ae = Q + "animation: 1ms " + ie + "; ";
}
var $;
var q;
var Y;
var X;
var J;
var Q;
var ee;
var te;
var oe;
var ne;
var re;
var ie;
var se;
var ae;
var pe = void 0 !== import_react_dom.default.createPortal;
var le = pe ? import_react_dom.default.createPortal : import_react_dom.default.unstable_renderSubtreeIntoContainer;
var ue = function(e2) {
  return K(e2) ? e2 : { lat: e2[0], lng: e2[1] };
};
var he = function(e2, t2) {
  return e2 < t2 && console.warn("GoogleMap: minZoom option is less than recommended minZoom option for your map sizes.\noverrided to value " + t2), t2 < e2 ? e2 : t2;
};
var ce = function(t2) {
  function o2(o3) {
    var r3;
    if ((r3 = t2.call(this, o3) || this)._getMinZoom = function() {
      if (r3.geoService_.getWidth() > 0 || r3.geoService_.getHeight() > 0) {
        var e2 = Math.ceil(r3.geoService_.getWidth() / 256) + 2, t3 = Math.ceil(r3.geoService_.getHeight() / 256) + 2, o4 = Math.max(e2, t3);
        return Math.ceil(P(o4));
      }
      return 3;
    }, r3._computeMinZoom = function(e2) {
      return I(e2) ? r3._getMinZoom() : e2;
    }, r3._mapDomResizeCallback = function() {
      if (r3.resetSizeOnIdle_ = true, r3.maps_) {
        var e2 = r3.props.center || r3.props.defaultCenter, t3 = r3.map_.getCenter();
        r3.maps_.event.trigger(r3.map_, "resize"), r3.map_.setCenter(r3.props.resetBoundsOnResize ? e2 : t3);
      }
    }, r3._setLayers = function(e2) {
      e2.forEach(function(e3) {
        r3.layers_[e3] = new r3.maps_[e3](), r3.layers_[e3].setMap(r3.map_);
      });
    }, r3._renderPortal = function() {
      return import_react.default.createElement(M, { experimental: r3.props.experimental, onChildClick: r3._onChildClick, onChildMouseDown: r3._onChildMouseDown, onChildMouseEnter: r3._onChildMouseEnter, onChildMouseLeave: r3._onChildMouseLeave, geoService: r3.geoService_, insideMapPanes: true, distanceToMouse: r3.props.distanceToMouse, getHoverDistance: r3._getHoverDistance, dispatcher: r3.markersDispatcher_ });
    }, r3._initMap = function() {
      if (!r3.initialized_) {
        r3.initialized_ = true;
        var e2 = ue(r3.props.center || r3.props.defaultCenter);
        r3.geoService_.setView(e2, r3.props.zoom || r3.props.defaultZoom, 0), r3._onBoundsChanged();
        var t3 = a({}, r3.props.apiKey && { key: r3.props.apiKey }, r3.props.bootstrapURLKeys);
        r3.props.googleMapLoader(t3, r3.props.heatmapLibrary).then(function(e3) {
          if (r3.mounted_) {
            var t4, o4, i2 = r3.geoService_.getCenter(), s2 = { zoom: r3.props.zoom || r3.props.defaultZoom, center: new e3.LatLng(i2.lat, i2.lng) };
            r3.props.heatmap.positions && (Object.assign(l(r3), { heatmap: (t4 = e3, o4 = r3.props.heatmap, new t4.visualization.HeatmapLayer({ data: o4.positions.reduce(function(e4, o5) {
              var n2 = o5.weight, r4 = void 0 === n2 ? 1 : n2;
              return e4.push({ location: new t4.LatLng(o5.lat, o5.lng), weight: r4 }), e4;
            }, []) })) }), function(e4, t5) {
              var o5 = t5.options, n2 = void 0 === o5 ? {} : o5;
              Object.keys(n2).map(function(t6) {
                return e4.set(t6, n2[t6]);
              });
            }(r3.heatmap, r3.props.heatmap));
            var p2 = A(e3, K), u2 = "function" == typeof r3.props.options ? r3.props.options(p2) : r3.props.options, h2 = !I(r3.props.draggable) && { draggable: r3.props.draggable }, c2 = r3._computeMinZoom(u2.minZoom);
            r3.minZoom_ = c2;
            var d2 = a({}, { overviewMapControl: false, streetViewControl: false, rotateControl: true, mapTypeControl: false, styles: [{ featureType: "poi", elementType: "labels", stylers: [{ visibility: "off" }] }], minZoom: 3 }, { minZoom: c2 }, u2, s2);
            r3.defaultDraggableOption_ = I(d2.draggable) ? r3.defaultDraggableOption_ : d2.draggable;
            var m2 = a({}, d2, h2);
            m2.minZoom = he(m2.minZoom, c2);
            var g2 = new e3.Map(import_react_dom.default.findDOMNode(r3.googleMapDom_), m2);
            r3.map_ = g2, r3.maps_ = e3, r3._setLayers(r3.props.layerTypes);
            var _2 = e3.version.match(/^3\.(\d+)\./), f2 = _2 && Number(_2[1]), v2 = l(r3), M2 = Object.assign(new e3.OverlayView(), { onAdd: function() {
              var t5 = "undefined" != typeof screen ? screen.width + "px" : "2000px", o5 = "undefined" != typeof screen ? screen.height + "px" : "2000px", n2 = document.createElement("div");
              if (n2.style.backgroundColor = "transparent", n2.style.position = "absolute", n2.style.left = "0px", n2.style.top = "0px", n2.style.width = t5, n2.style.height = o5, v2.props.overlayViewDivStyle) {
                var r4 = v2.props.overlayViewDivStyle;
                "object" == typeof r4 && Object.keys(r4).forEach(function(e4) {
                  n2.style[e4] = r4[e4];
                });
              }
              this.getPanes().overlayMouseTarget.appendChild(n2), v2.geoService_.setMapCanvasProjection(e3, M2.getProjection()), pe ? v2.setState({ overlay: n2 }) : le(v2, v2._renderPortal(), n2, function() {
                return v2.setState({ overlay: n2 });
              });
            }, onRemove: function() {
              var e4 = v2.state.overlay;
              e4 && !pe && import_react_dom.default.unmountComponentAtNode(e4), v2.setState({ overlay: null });
            }, draw: function() {
              if (v2.updateCounter_++, v2._onBoundsChanged(g2, e3, !v2.props.debounced), v2.googleApiLoadedCalled_ || (v2._onGoogleApiLoaded({ map: g2, maps: e3, ref: v2.googleMapDom_ }), v2.googleApiLoadedCalled_ = true), v2.mouse_) {
                var t5 = v2.geoService_.fromContainerPixelToLatLng(v2.mouse_);
                v2.mouse_.lat = t5.lat, v2.mouse_.lng = t5.lng;
              }
              v2._onChildMouseMove(), v2.markersDispatcher_ && (v2.markersDispatcher_.emit("kON_CHANGE"), v2.fireMouseEventOnIdle_ && v2.markersDispatcher_.emit("kON_MOUSE_POSITION_CHANGE"));
            } });
            r3.overlay_ = M2, M2.setMap(g2), r3.props.heatmap.positions && r3.heatmap.setMap(g2), r3.props.onTilesLoaded && e3.event.addListener(g2, "tilesloaded", function() {
              v2._onTilesLoaded();
            }), e3.event.addListener(g2, "zoom_changed", function() {
              v2.geoService_.getZoom() !== g2.getZoom() && (v2.zoomAnimationInProgress_ || (v2.zoomAnimationInProgress_ = true, v2._onZoomAnimationStart(g2.zoom)), f2 < 32) && ((/* @__PURE__ */ new Date()).getTime() - r3.zoomControlClickTime_ < 300 ? E(function() {
                return E(function() {
                  v2.updateCounter_++, v2._onBoundsChanged(g2, e3);
                });
              }) : (v2.updateCounter_++, v2._onBoundsChanged(g2, e3)));
            }), e3.event.addListener(g2, "idle", function() {
              if (r3.resetSizeOnIdle_) {
                r3._setViewSize();
                var t5 = r3._computeMinZoom(u2.minZoom);
                t5 !== r3.minZoom_ && (r3.minZoom_ = t5, g2.setOptions({ minZoom: t5 })), r3.resetSizeOnIdle_ = false;
              }
              v2.zoomAnimationInProgress_ && (v2.zoomAnimationInProgress_ = false, v2._onZoomAnimationEnd(g2.zoom)), v2.updateCounter_++, v2._onBoundsChanged(g2, e3), v2.dragTime_ = 0, v2.markersDispatcher_ && v2.markersDispatcher_.emit("kON_CHANGE");
            }), e3.event.addListener(g2, "mouseover", function() {
              v2.mouseInMap_ = true;
            }), e3.event.addListener(g2, "click", function() {
              v2.mouseInMap_ = true;
            }), e3.event.addListener(g2, "mouseout", function() {
              v2.mouseInMap_ = false, v2.mouse_ = null, v2.markersDispatcher_.emit("kON_MOUSE_POSITION_CHANGE");
            }), e3.event.addListener(g2, "drag", function() {
              v2.dragTime_ = (/* @__PURE__ */ new Date()).getTime(), v2._onDrag(g2);
            }), e3.event.addListener(g2, "dragend", function() {
              var t5 = e3.event.addListener(g2, "idle", function() {
                e3.event.removeListener(t5), v2._onDragEnd(g2);
              });
            }), e3.event.addListener(g2, "maptypeid_changed", function() {
              v2._onMapTypeIdChange(g2.getMapTypeId());
            });
          }
        }).catch(function(e3) {
          throw r3._onGoogleApiLoaded({ map: null, maps: null, ref: r3.googleMapDom_ }), console.error(e3), e3;
        });
      }
    }, r3._onGoogleApiLoaded = function() {
      var e2;
      r3.props.onGoogleApiLoaded && (true !== r3.props.yesIWantToUseGoogleMapApiInternals && console.warn("GoogleMap: Usage of internal api objects is dangerous and can cause a lot of issues.\nTo hide this warning add yesIWantToUseGoogleMapApiInternals={true} to <GoogleMap instance"), (e2 = r3.props).onGoogleApiLoaded.apply(e2, arguments));
    }, r3._getHoverDistance = function() {
      return r3.props.hoverDistance;
    }, r3._onDrag = function() {
      var e2;
      return r3.props.onDrag && (e2 = r3.props).onDrag.apply(e2, arguments);
    }, r3._onDragEnd = function() {
      var e2;
      return r3.props.onDragEnd && (e2 = r3.props).onDragEnd.apply(e2, arguments);
    }, r3._onMapTypeIdChange = function() {
      var e2;
      return r3.props.onMapTypeIdChange && (e2 = r3.props).onMapTypeIdChange.apply(e2, arguments);
    }, r3._onZoomAnimationStart = function() {
      var e2;
      return r3.props.onZoomAnimationStart && (e2 = r3.props).onZoomAnimationStart.apply(e2, arguments);
    }, r3._onZoomAnimationEnd = function() {
      var e2;
      return r3.props.onZoomAnimationEnd && (e2 = r3.props).onZoomAnimationEnd.apply(e2, arguments);
    }, r3._onTilesLoaded = function() {
      return r3.props.onTilesLoaded && r3.props.onTilesLoaded();
    }, r3._onChildClick = function() {
      var e2;
      if (r3.props.onChildClick) return (e2 = r3.props).onChildClick.apply(e2, arguments);
    }, r3._onChildMouseDown = function(e2, t3) {
      r3.childMouseDownArgs_ = [e2, t3], r3.props.onChildMouseDown && r3.props.onChildMouseDown(e2, t3, a({}, r3.mouse_));
    }, r3._onChildMouseUp = function() {
      var e2;
      r3.childMouseDownArgs_ && (r3.props.onChildMouseUp && (e2 = r3.props).onChildMouseUp.apply(e2, r3.childMouseDownArgs_.concat([a({}, r3.mouse_)])), r3.childMouseDownArgs_ = null, r3.childMouseUpTime_ = (/* @__PURE__ */ new Date()).getTime());
    }, r3._onChildMouseMove = function() {
      var e2;
      r3.childMouseDownArgs_ && r3.props.onChildMouseMove && (e2 = r3.props).onChildMouseMove.apply(e2, r3.childMouseDownArgs_.concat([a({}, r3.mouse_)]));
    }, r3._onChildMouseEnter = function() {
      var e2;
      if (r3.props.onChildMouseEnter) return (e2 = r3.props).onChildMouseEnter.apply(e2, arguments);
    }, r3._onChildMouseLeave = function() {
      var e2;
      if (r3.props.onChildMouseLeave) return (e2 = r3.props).onChildMouseLeave.apply(e2, arguments);
    }, r3._setViewSize = function() {
      if (r3.mounted_) {
        if (document.fullscreen || document.webkitIsFullScreen || document.mozFullScreen || document.msFullscreenElement) r3.geoService_.setViewSize(window.innerWidth, window.innerHeight);
        else {
          var e2 = import_react_dom.default.findDOMNode(r3.googleMapDom_);
          r3.geoService_.setViewSize(e2.clientWidth, e2.clientHeight);
        }
        r3._onBoundsChanged();
      }
    }, r3._onWindowResize = function() {
      r3.resetSizeOnIdle_ = true;
    }, r3._onMapMouseMove = function(e2) {
      if (r3.mouseInMap_) {
        var t3 = (/* @__PURE__ */ new Date()).getTime();
        t3 - r3.mouseMoveTime_ > 50 && (r3.boundingRect_ = e2.currentTarget.getBoundingClientRect()), r3.mouseMoveTime_ = t3;
        var o4 = e2.clientX - r3.boundingRect_.left, n2 = e2.clientY - r3.boundingRect_.top;
        r3.mouse_ || (r3.mouse_ = { x: 0, y: 0, lat: 0, lng: 0 }), r3.mouse_.x = o4, r3.mouse_.y = n2;
        var i2 = r3.geoService_.fromContainerPixelToLatLng(r3.mouse_);
        r3.mouse_.lat = i2.lat, r3.mouse_.lng = i2.lng, r3._onChildMouseMove(), t3 - r3.dragTime_ < 100 ? r3.fireMouseEventOnIdle_ = true : (r3.markersDispatcher_.emit("kON_MOUSE_POSITION_CHANGE"), r3.fireMouseEventOnIdle_ = false);
      }
    }, r3._onClick = function() {
      var e2;
      return r3.props.onClick && !r3.childMouseDownArgs_ && (/* @__PURE__ */ new Date()).getTime() - r3.childMouseUpTime_ > 300 && 0 === r3.dragTime_ && (e2 = r3.props).onClick.apply(e2, arguments);
    }, r3._onMapClick = function(e2) {
      r3.markersDispatcher_ && (r3._onMapMouseMove(e2), (/* @__PURE__ */ new Date()).getTime() - r3.dragTime_ > 100 && (r3.mouse_ && r3._onClick(a({}, r3.mouse_, { event: e2 })), r3.markersDispatcher_.emit("kON_CLICK", e2)));
    }, r3._onMapMouseDownNative = function(e2) {
      r3.mouseInMap_ && r3._onMapMouseDown(e2);
    }, r3._onMapMouseDown = function(e2) {
      r3.markersDispatcher_ && (/* @__PURE__ */ new Date()).getTime() - r3.dragTime_ > 100 && (r3._onMapMouseMove(e2), r3.markersDispatcher_.emit("kON_MDOWN", e2));
    }, r3._onMapMouseDownCapture = function() {
      U().isChrome && (r3.zoomControlClickTime_ = (/* @__PURE__ */ new Date()).getTime());
    }, r3._onKeyDownCapture = function() {
      U().isChrome && (r3.zoomControlClickTime_ = (/* @__PURE__ */ new Date()).getTime());
    }, r3._isCenterDefined = function(e2) {
      return e2 && (K(e2) && Z(e2.lat) && Z(e2.lng) || 2 === e2.length && Z(e2[0]) && Z(e2[1]));
    }, r3._onBoundsChanged = function(e2, t3, o4) {
      if (e2) {
        var n2 = e2.getCenter();
        r3.geoService_.setView([n2.lat(), n2.lng()], e2.getZoom(), 0);
      }
      if ((r3.props.onChange || r3.props.onBoundsChange) && r3.geoService_.canProject()) {
        var i2 = r3.geoService_.getZoom(), s2 = r3.geoService_.getBounds(), p2 = r3.geoService_.getCenter();
        if (!function(e3, t4, o5) {
          if (e3 && t4) {
            for (var n3 = 0; n3 !== e3.length; ++n3) if (Math.abs(e3[n3] - t4[n3]) > 1e-5) return false;
            return true;
          }
          return false;
        }(s2, r3.prevBounds_) && false !== o4) {
          var l2 = r3.geoService_.getBounds(r3.props.margin);
          r3.props.onBoundsChange && r3.props.onBoundsChange(r3.centerIsObject_ ? a({}, p2) : [p2.lat, p2.lng], i2, s2, l2), r3.props.onChange && r3.props.onChange({ center: a({}, p2), zoom: i2, bounds: { nw: { lat: s2[0], lng: s2[1] }, se: { lat: s2[2], lng: s2[3] }, sw: { lat: s2[4], lng: s2[5] }, ne: { lat: s2[6], lng: s2[7] } }, marginBounds: { nw: { lat: l2[0], lng: l2[1] }, se: { lat: l2[2], lng: l2[3] }, sw: { lat: l2[4], lng: l2[5] }, ne: { lat: l2[6], lng: l2[7] } }, size: r3.geoService_.hasSize() ? { width: r3.geoService_.getWidth(), height: r3.geoService_.getHeight() } : { width: 0, height: 0 } }), r3.prevBounds_ = s2;
        }
      }
    }, r3._registerChild = function(e2) {
      r3.googleMapDom_ = e2;
    }, r3.mounted_ = false, r3.initialized_ = false, r3.googleApiLoadedCalled_ = false, r3.map_ = null, r3.maps_ = null, r3.prevBounds_ = null, r3.heatmap = null, r3.layers_ = {}, r3.mouse_ = null, r3.mouseMoveTime_ = 0, r3.boundingRect_ = null, r3.mouseInMap_ = true, r3.dragTime_ = 0, r3.fireMouseEventOnIdle_ = false, r3.updateCounter_ = 0, r3.markersDispatcher_ = new c(l(r3)), r3.geoService_ = new T(256), r3.centerIsObject_ = K(r3.props.center), r3.minZoom_ = 3, r3.defaultDraggableOption_ = true, r3.zoomControlClickTime_ = 0, r3.childMouseDownArgs_ = null, r3.childMouseUpTime_ = 0, r3.googleMapDom_ = null, r3.props.apiKey && console.warn("GoogleMap: apiKey is deprecated, use bootstrapURLKeys={{key: YOUR_API_KEY}} instead."), r3.props.onBoundsChange && console.warn("GoogleMap: onBoundsChange is deprecated, use onChange({center, zoom, bounds, ...other}) instead."), I(r3.props.center) && I(r3.props.defaultCenter) && console.warn("GoogleMap: center or defaultCenter property must be defined"), I(r3.props.zoom) && I(r3.props.defaultZoom) && console.warn("GoogleMap: zoom or defaultZoom property must be defined"), r3._isCenterDefined(r3.props.center || r3.props.defaultCenter)) {
      var i = ue(r3.props.center || r3.props.defaultCenter);
      r3.geoService_.setView(i, r3.props.zoom || r3.props.defaultZoom, 0);
    }
    return r3.zoomAnimationInProgress_ = false, r3.state = { overlay: null }, r3;
  }
  p(o2, t2);
  var r2 = o2.prototype;
  return r2.componentDidMount = function() {
    var e2 = this;
    this.mounted_ = true, this.markersDispatcher_ = new c(this), R(window, "resize", this._onWindowResize, false), R(window, "keydown", this._onKeyDownCapture, true);
    var t3 = import_react_dom.default.findDOMNode(this.googleMapDom_);
    t3 && R(t3, "mousedown", this._onMapMouseDownNative, true), R(window, "mouseup", this._onChildMouseUp, false);
    var o3 = a({}, this.props.apiKey && { key: this.props.apiKey }, this.props.bootstrapURLKeys);
    this.props.googleMapLoader(o3, this.props.heatmapLibrary), setTimeout(function() {
      e2._setViewSize(), e2._isCenterDefined(e2.props.center || e2.props.defaultCenter) && e2._initMap();
    }, 0, this), this.props.resetBoundsOnResize && function(e3, t4) {
      if (void 0 === e3.parentNode) {
        var o4 = document.createElement("div");
        e3.parentNode = o4;
      }
      e3 = e3.parentNode, V ? e3.attachEvent("onresize", t4) : (e3.__resizeTriggers__ || ("static" == getComputedStyle(e3).position && (e3.style.position = "relative"), function() {
        if (!F) {
          var e4 = (se || "") + ".resize-triggers { " + (ae || "") + 'visibility: hidden; opacity: 0; } .resize-triggers, .resize-triggers > div, .contract-trigger:before { content: " "; display: block; position: absolute; top: 0; left: 0; height: 100%; width: 100%; overflow: hidden; } .resize-triggers > div { background: #eee; overflow: auto; } .contract-trigger:before { width: 200%; height: 200%; }', t5 = document.head || document.getElementsByTagName("head")[0], o5 = document.createElement("style");
          o5.type = "text/css", o5.styleSheet ? o5.styleSheet.cssText = e4 : o5.appendChild(document.createTextNode(e4)), t5.appendChild(o5), F = true;
        }
      }(), e3.__resizeLast__ = {}, e3.__resizeListeners__ = [], (e3.__resizeTriggers__ = document.createElement("div")).className = "resize-triggers", e3.__resizeTriggers__.innerHTML = '<div class="expand-trigger"><div></div></div><div class="contract-trigger"></div>', e3.appendChild(e3.__resizeTriggers__), Y(e3), R(e3, "scroll", X, true), ee && e3.__resizeTriggers__.addEventListener(ee, function(t5) {
        t5.animationName == ie && Y(e3);
      })), e3.__resizeListeners__.push(t4));
    }(t3, this._mapDomResizeCallback);
  }, r2.shouldComponentUpdate = function(e2, t3) {
    return !_(d(this.props, ["draggable"]), d(e2, ["draggable"])) || !_(this.state, t3);
  }, r2.componentDidUpdate = function(e2) {
    var t3 = this;
    if (_(e2.defaultCenter, this.props.defaultCenter) || console.warn("GoogleMap: defaultCenter prop changed. You can't change default props."), _(e2.defaultZoom, this.props.defaultZoom) || console.warn("GoogleMap: defaultZoom prop changed. You can't change default props."), !this._isCenterDefined(e2.center) && this._isCenterDefined(this.props.center) && setTimeout(function() {
      return t3._initMap();
    }, 0), this.map_) {
      var o3 = this.geoService_.getCenter();
      if (this._isCenterDefined(this.props.center)) {
        var n2 = ue(this.props.center), r3 = this._isCenterDefined(e2.center) ? ue(e2.center) : null;
        (!r3 || Math.abs(n2.lat - r3.lat) + Math.abs(n2.lng - r3.lng) > 1e-5) && Math.abs(n2.lat - o3.lat) + Math.abs(n2.lng - o3.lng) > 1e-5 && this.map_.panTo({ lat: n2.lat, lng: n2.lng });
      }
      if (I(this.props.zoom) || Math.abs(this.props.zoom - e2.zoom) > 0 && this.map_.setZoom(this.props.zoom), !I(e2.draggable) && I(this.props.draggable) ? this.map_.setOptions({ draggable: this.defaultDraggableOption_ }) : _(e2.draggable, this.props.draggable) || this.map_.setOptions({ draggable: this.props.draggable }), !I(this.props.options) && !_(e2.options, this.props.options)) {
        var i = A(this.maps_, K), s2 = "function" == typeof this.props.options ? this.props.options(i) : this.props.options;
        if ("minZoom" in (s2 = d(s2, ["zoom", "center", "draggable"]))) {
          var a2 = this._computeMinZoom(s2.minZoom);
          s2.minZoom = he(s2.minZoom, a2);
        }
        this.map_.setOptions(s2);
      }
      _(this.props.layerTypes, e2.layerTypes) || (Object.keys(this.layers_).forEach(function(e3) {
        t3.layers_[e3].setMap(null), delete t3.layers_[e3];
      }), this._setLayers(this.props.layerTypes)), this.heatmap && !_(this.props.heatmap.positions, e2.heatmap.positions) && this.heatmap.setData(this.props.heatmap.positions.map(function(e3) {
        return { location: new t3.maps_.LatLng(e3.lat, e3.lng), weight: e3.weight };
      })), this.heatmap && !_(this.props.heatmap.options, e2.heatmap.options) && Object.keys(this.props.heatmap.options).forEach(function(e3) {
        t3.heatmap.set(e3, t3.props.heatmap.options[e3]);
      });
    }
    this.markersDispatcher_.emit("kON_CHANGE"), _(this.props.hoverDistance, e2.hoverDistance) || this.markersDispatcher_.emit("kON_MOUSE_POSITION_CHANGE");
  }, r2.componentWillUnmount = function() {
    this.mounted_ = false;
    var e2, t3, o3 = import_react_dom.default.findDOMNode(this.googleMapDom_);
    o3 && o3.removeEventListener("mousedown", this._onMapMouseDownNative, true), window.removeEventListener("resize", this._onWindowResize), window.removeEventListener("keydown", this._onKeyDownCapture), window.removeEventListener("mouseup", this._onChildMouseUp, false), this.props.resetBoundsOnResize && (t3 = this._mapDomResizeCallback, e2 = (e2 = o3).parentNode, V ? e2.detachEvent("onresize", t3) : (e2.__resizeListeners__.splice(e2.__resizeListeners__.indexOf(t3), 1), e2.__resizeListeners__.length || (e2.removeEventListener("scroll", X), e2.__resizeTriggers__ = !e2.removeChild(e2.__resizeTriggers__)))), this.overlay_ && this.overlay_.setMap(null), this.maps_ && this.map_ && this.props.shouldUnregisterMapOnUnmount && (this.map_.setOptions({ scrollwheel: false }), this.maps_.event.clearInstanceListeners(this.map_)), this.props.shouldUnregisterMapOnUnmount && (this.map_ = null, this.maps_ = null), this.markersDispatcher_.dispose(), this.resetSizeOnIdle_ = false, this.props.shouldUnregisterMapOnUnmount && (delete this.map_, delete this.markersDispatcher_);
  }, r2.render = function() {
    var t3 = this.state.overlay, o3 = t3 ? null : import_react.default.createElement(C, { experimental: this.props.experimental, onChildClick: this._onChildClick, onChildMouseDown: this._onChildMouseDown, onChildMouseEnter: this._onChildMouseEnter, onChildMouseLeave: this._onChildMouseLeave, geoService: this.geoService_, insideMapPanes: false, distanceToMouse: this.props.distanceToMouse, getHoverDistance: this._getHoverDistance, dispatcher: this.markersDispatcher_ });
    return import_react.default.createElement("div", { style: this.props.style, onMouseMove: this._onMapMouseMove, onMouseDownCapture: this._onMapMouseDownCapture, onClick: this._onMapClick }, import_react.default.createElement(h, { registerChild: this._registerChild }), pe && t3 && le(this._renderPortal(), t3), o3);
  }, o2;
}(import_react.Component);
ce.propTypes = { apiKey: import_prop_types.default.string, bootstrapURLKeys: import_prop_types.default.any, defaultCenter: import_prop_types.default.oneOfType([import_prop_types.default.array, import_prop_types.default.shape({ lat: import_prop_types.default.number, lng: import_prop_types.default.number })]), center: import_prop_types.default.oneOfType([import_prop_types.default.array, import_prop_types.default.shape({ lat: import_prop_types.default.number, lng: import_prop_types.default.number })]), defaultZoom: import_prop_types.default.number, zoom: import_prop_types.default.number, onBoundsChange: import_prop_types.default.func, onChange: import_prop_types.default.func, onClick: import_prop_types.default.func, onChildClick: import_prop_types.default.func, onChildMouseDown: import_prop_types.default.func, onChildMouseUp: import_prop_types.default.func, onChildMouseMove: import_prop_types.default.func, onChildMouseEnter: import_prop_types.default.func, onChildMouseLeave: import_prop_types.default.func, onZoomAnimationStart: import_prop_types.default.func, onZoomAnimationEnd: import_prop_types.default.func, onDrag: import_prop_types.default.func, onDragEnd: import_prop_types.default.func, onMapTypeIdChange: import_prop_types.default.func, onTilesLoaded: import_prop_types.default.func, options: import_prop_types.default.any, distanceToMouse: import_prop_types.default.func, hoverDistance: import_prop_types.default.number, debounced: import_prop_types.default.bool, margin: import_prop_types.default.array, googleMapLoader: import_prop_types.default.any, onGoogleApiLoaded: import_prop_types.default.func, yesIWantToUseGoogleMapApiInternals: import_prop_types.default.bool, draggable: import_prop_types.default.bool, style: import_prop_types.default.any, resetBoundsOnResize: import_prop_types.default.bool, layerTypes: import_prop_types.default.arrayOf(import_prop_types.default.string), shouldUnregisterMapOnUnmount: import_prop_types.default.bool }, ce.defaultProps = { distanceToMouse: function(e2, t2) {
  return Math.sqrt((e2.x - t2.x) * (e2.x - t2.x) + (e2.y - t2.y) * (e2.y - t2.y));
}, hoverDistance: 30, debounced: true, options: function() {
  return { overviewMapControl: false, streetViewControl: false, rotateControl: true, mapTypeControl: false, styles: [{ featureType: "poi", elementType: "labels", stylers: [{ visibility: "off" }] }], minZoom: 3 };
}, googleMapLoader: O, yesIWantToUseGoogleMapApiInternals: false, style: { width: "100%", height: "100%", margin: 0, padding: 0, position: "relative" }, layerTypes: [], heatmap: {}, heatmapLibrary: false, shouldUnregisterMapOnUnmount: true }, ce.googleMapLoader = O;
var index_modern_default = ce;

// react-with-dotnet/libraries/google-map-react/GoogleMap.jsx
var GoogleMap_default = index_modern_default;
export {
  GoogleMap_default as default
};
