var ss = ss || {};
(function () {

    /* Application paths *****************************************/

    //Current application root path (including virtual directory if exists).
    ss.appPath = ss.appPath || '/';

    ss.pageLoadTime = new Date();

    //Converts given path to absolute path using ss.appPath variable.
    ss.toAbsAppPath = function (path) {
        if (path.indexOf('/') == 0) {
            path = path.substring(1);
        }

        return ss.appPath + path;
    };

    /* LOGGING ***************************************************/
    //Implements Logging API that provides secure & controlled usage of console.log

    ss.log = ss.log || {};

    ss.log.levels = {
        DEBUG: 1,
        INFO: 2,
        WARN: 3,
        ERROR: 4,
        FATAL: 5
    };

    ss.log.level = ss.log.levels.DEBUG;

    ss.log.log = function (logObject, logLevel) {
        if (!window.console || !window.console.log) {
            return;
        }

        if (logLevel != undefined && logLevel < ss.log.level) {
            return;
        }

        console.log(logObject);
    };

    ss.log.debug = function (logObject) {
        ss.log.log("DEBUG: ", ss.log.levels.DEBUG);
        ss.log.log(logObject, ss.log.levels.DEBUG);
    };

    ss.log.info = function (logObject) {
        ss.log.log("INFO: ", ss.log.levels.INFO);
        ss.log.log(logObject, ss.log.levels.INFO);
    };

    ss.log.warn = function (logObject) {
        ss.log.log("WARN: ", ss.log.levels.WARN);
        ss.log.log(logObject, ss.log.levels.WARN);
    };

    ss.log.error = function (logObject) {
        ss.log.log("ERROR: ", ss.log.levels.ERROR);
        ss.log.log(logObject, ss.log.levels.ERROR);
    };

    ss.log.fatal = function (logObject) {
        ss.log.log("FATAL: ", ss.log.levels.FATAL);
        ss.log.log(logObject, ss.log.levels.FATAL);
    };

    /* LOCALIZATION ***********************************************/

    ss.localization = ss.localization || {};
    ss.localization.internal = ss.localization.internal || {};
    ss.localization.values =  ss.localization.values || {};
    ss.localization.resources =  ss.localization.resources || {};

    ss.localization.internal.getResource = function (resourceName) {
        var resource = ss.localization.resources[resourceName];
        if (resource) {
            return resource;
        }
        
        var legacySource = ss.localization.values[resourceName];
        if (legacySource) {
            return {
                texts: ss.localization.values[resourceName],
                baseResources: []
            };
        }
        
        ss.log.warn('Could not find localization source: ' + resourceName);        
        return null;
    };
    
    ss.localization.internal.localize = function (key, sourceName) {
        var resource = ss.localization.internal.getResource(sourceName);
        if (!resource){
            return {
                value: key,
                found: false
            };
        }

        var value = resource.texts[key];
        if (value === undefined) {            
            for (var i = 0; i < resource.baseResources.length; i++){
                var basedArguments = Array.prototype.slice.call(arguments, 0);
                basedArguments[1] = resource.baseResources[i];

                var result = ss.localization.internal.localize.apply(this, basedArguments);
                if (result.found){
                    return result;
                }
            }
            
            return {
                value: key,
                found: false
            };
        }

        var copiedArguments = Array.prototype.slice.call(arguments, 0);
        copiedArguments.splice(1, 1);
        copiedArguments[0] = value;

        return {
            value: ss.utils.formatString.apply(this, copiedArguments),
            found: true
        };
    };

    ss.localization.localize = function (key, sourceName) {
        if (sourceName === '_') { //A convention to suppress the localization
            return key;
        }
        
        if (sourceName) {
            return ss.localization.internal.localize.apply(this, arguments).value;
        }

        if (!ss.localization.defaultResourceName) {
            ss.log.warn('Localization source name is not specified and the defaultResourceName was not defined!');
            return key;
        }

        var copiedArguments = Array.prototype.slice.call(arguments, 0);
        copiedArguments.splice(1, 1, ss.localization.defaultResourceName);

        return ss.localization.internal.localize.apply(this, copiedArguments).value;
    };

    ss.localization.isLocalized = function (key, sourceName) {
        if (sourceName === '_') { //A convention to suppress the localization
            return true;
        }

        sourceName = sourceName || ss.localization.defaultResourceName;
        if (!sourceName) {
            return false;
        }

        return ss.localization.internal.localize(key, sourceName).found;
    };

    ss.localization.getResource = function (name) {
        return function () {
            var copiedArguments = Array.prototype.slice.call(arguments, 0);
            copiedArguments.splice(1, 0, name);
            return ss.localization.localize.apply(this, copiedArguments);
        };
    };

    ss.localization.defaultResourceName = undefined;
    ss.localization.currentCulture = {
        cultureName: undefined
    };

    var getMapValue = function (packageMaps, packageName, language) {
        language = language || ss.localization.currentCulture.name;
        if (!packageMaps || !packageName || !language) {
            return language;
        }

        var packageMap = packageMaps[packageName];
        if (!packageMap) {
            return language;
        }

        for (var i = 0; i < packageMap.length; i++) {
            var map = packageMap[i];
            if (map.name === language){
                return map.value;
            }
        }

        return language;
    };

    ss.localization.getLanguagesMap = function (packageName, language) {
        return getMapValue(ss.localization.languagesMap, packageName, language);
    };

    ss.localization.getLanguageFilesMap = function (packageName, language) {
        return getMapValue(ss.localization.languageFilesMap, packageName, language);
    };

    /* AUTHORIZATION **********************************************/

    ss.auth = ss.auth || {};

    ss.auth.grantedPolicies = ss.auth.grantedPolicies || {};

    ss.auth.isGranted = function (policyName) {
        return ss.auth.grantedPolicies[policyName] != undefined;
    };

    ss.auth.isAnyGranted = function () {
        if (!arguments || arguments.length <= 0) {
            return true;
        }

        for (var i = 0; i < arguments.length; i++) {
            if (ss.auth.isGranted(arguments[i])) {
                return true;
            }
        }

        return false;
    };

    ss.auth.areAllGranted = function () {
        if (!arguments || arguments.length <= 0) {
            return true;
        }

        for (var i = 0; i < arguments.length; i++) {
            if (!ss.auth.isGranted(arguments[i])) {
                return false;
            }
        }

        return true;
    };

    ss.auth.tokenCookieName = 'SmartSoftware.AuthToken';

    ss.auth.setToken = function (authToken, expireDate) {
        ss.utils.setCookieValue(ss.auth.tokenCookieName, authToken, expireDate, ss.appPath, ss.domain);
    };

    ss.auth.getToken = function () {
        return ss.utils.getCookieValue(ss.auth.tokenCookieName);
    }

    ss.auth.clearToken = function () {
        ss.auth.setToken();
    }

    /* SETTINGS *************************************************/

    ss.setting = ss.setting || {};

    ss.setting.values = ss.setting.values || {};

    ss.setting.get = function (name) {
        return ss.setting.values[name];
    };

    ss.setting.getBoolean = function (name) {
        var value = ss.setting.get(name);
        return value == 'true' || value == 'True';
    };

    ss.setting.getInt = function (name) {
        return parseInt(ss.setting.values[name]);
    };

    /* NOTIFICATION *********************************************/
    //Defines Notification API, not implements it

    ss.notify = ss.notify || {};

    ss.notify.success = function (message, title, options) {
        ss.log.warn('ss.notify.success is not implemented!');
    };

    ss.notify.info = function (message, title, options) {
        ss.log.warn('ss.notify.info is not implemented!');
    };

    ss.notify.warn = function (message, title, options) {
        ss.log.warn('ss.notify.warn is not implemented!');
    };

    ss.notify.error = function (message, title, options) {
        ss.log.warn('ss.notify.error is not implemented!');
    };

    /* MESSAGE **************************************************/
    //Defines Message API, not implements it

    ss.message = ss.message || {};

    ss.message._showMessage = function (message, title) {
        alert((title || '') + ' ' + message);
    };

    ss.message.info = function (message, title) {
        ss.log.warn('ss.message.info is not implemented!');
        return ss.message._showMessage(message, title);
    };

    ss.message.success = function (message, title) {
        ss.log.warn('ss.message.success is not implemented!');
        return ss.message._showMessage(message, title);
    };

    ss.message.warn = function (message, title) {
        ss.log.warn('ss.message.warn is not implemented!');
        return ss.message._showMessage(message, title);
    };

    ss.message.error = function (message, title) {
        ss.log.warn('ss.message.error is not implemented!');
        return ss.message._showMessage(message, title);
    };

    ss.message.confirm = function (message, titleOrCallback, callback) {
        ss.log.warn('ss.message.confirm is not properly implemented!');

        if (titleOrCallback && !(typeof titleOrCallback == 'string')) {
            callback = titleOrCallback;
        }

        var result = confirm(message);
        callback && callback(result);
    };

    /* UI *******************************************************/

    ss.ui = ss.ui || {};

    /* UI BLOCK */
    //Defines UI Block API and implements basically

    var $ssBlockArea = document.createElement('div');
    $ssBlockArea.classList.add('ss-block-area');

    /* opts: { //Can be an object with options or a string for query a selector
     *  elm: a query selector (optional - default: document.body)
     *  busy: boolean (optional - default: false)
     *  promise: A promise with always or finally handler (optional - auto unblocks the ui if provided)
     * }
     */
    ss.ui.block = function (opts) {
        if (!opts) {
            opts = {};
        } else if (typeof opts == 'string') {
            opts = {
                elm: opts
            };
        }

        var $elm = document.querySelector(opts.elm) || document.body;

        if (opts.busy) {
            $ssBlockArea.classList.add('ss-block-area-busy');
        } else {
            $ssBlockArea.classList.remove('ss-block-area-busy');
        }

        if (document.querySelector(opts.elm)) {
            $ssBlockArea.style.position = 'absolute';
        } else {
            $ssBlockArea.style.position = 'fixed';
        }

        $elm.appendChild($ssBlockArea);

        if (opts.promise) {
            if (opts.promise.always) { //jQuery.Deferred style
                opts.promise.always(function () {
                    ss.ui.unblock({
                        $elm: opts.elm
                    });
                });
            } else if (opts.promise['finally']) { //Q style
                opts.promise['finally'](function () {
                    ss.ui.unblock({
                        $elm: opts.elm
                    });
                });
            }
        }
    };

    /* opts: {
     *
     * }
     */
    ss.ui.unblock = function (opts) {
        var element = document.querySelector('.ss-block-area');
        if (element) {
            element.classList.add('ss-block-area-disappearing');
            setTimeout(function () {
                if (element) {
                    element.classList.remove('ss-block-area-disappearing');
                    if (element.parentElement) {
                        element.parentElement.removeChild(element);
                    }
                }
            }, 250);
        }
    };

    /* UI BUSY */
    //Defines UI Busy API, not implements it

    ss.ui.setBusy = function (opts) {
        if (!opts) {
            opts = {
                busy: true
            };
        } else if (typeof opts == 'string') {
            opts = {
                elm: opts,
                busy: true
            };
        }

        ss.ui.block(opts);
    };

    ss.ui.clearBusy = function (opts) {
        ss.ui.unblock(opts);
    };

    /* SIMPLE EVENT BUS *****************************************/

    ss.event = (function () {

        var _callbacks = {};

        var on = function (eventName, callback) {
            if (!_callbacks[eventName]) {
                _callbacks[eventName] = [];
            }

            _callbacks[eventName].push(callback);
        };

        var off = function (eventName, callback) {
            var callbacks = _callbacks[eventName];
            if (!callbacks) {
                return;
            }

            var index = -1;
            for (var i = 0; i < callbacks.length; i++) {
                if (callbacks[i] === callback) {
                    index = i;
                    break;
                }
            }

            if (index < 0) {
                return;
            }

            _callbacks[eventName].splice(index, 1);
        };

        var trigger = function (eventName) {
            var callbacks = _callbacks[eventName];
            if (!callbacks || !callbacks.length) {
                return;
            }

            var args = Array.prototype.slice.call(arguments, 1);
            for (var i = 0; i < callbacks.length; i++) {
                callbacks[i].apply(this, args);
            }
        };

        // Public interface ///////////////////////////////////////////////////

        return {
            on: on,
            off: off,
            trigger: trigger
        };
    })();


    /* UTILS ***************************************************/

    ss.utils = ss.utils || {};

    /* Creates a name namespace.
    *  Example:
    *  var taskService = ss.utils.createNamespace(ss, 'services.task');
    *  taskService will be equal to ss.services.task
    *  first argument (root) must be defined first
    ************************************************************/
    ss.utils.createNamespace = function (root, ns) {
        var parts = ns.split('.');
        for (var i = 0; i < parts.length; i++) {
            if (typeof root[parts[i]] == 'undefined') {
                root[parts[i]] = {};
            }

            root = root[parts[i]];
        }

        return root;
    };

    /* Find and replaces a string (search) to another string (replacement) in
    *  given string (str).
    *  Example:
    *  ss.utils.replaceAll('This is a test string', 'is', 'X') = 'ThX X a test string'
    ************************************************************/
    ss.utils.replaceAll = function (str, search, replacement) {
        var fix = search.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
        return str.replace(new RegExp(fix, 'g'), replacement);
    };

    /* Formats a string just like string.format in C#.
    *  Example:
    *  ss.utils.formatString('Hello {0}','Tuana') = 'Hello Tuana'
    ************************************************************/
    ss.utils.formatString = function () {
        if (arguments.length < 1) {
            return null;
        }

        var str = arguments[0];

        for (var i = 1; i < arguments.length; i++) {
            var placeHolder = '{' + (i - 1) + '}';
            str = ss.utils.replaceAll(str, placeHolder, arguments[i]);
        }

        return str;
    };

    ss.utils.toPascalCase = function (str) {
        if (!str || !str.length) {
            return str;
        }

        if (str.length === 1) {
            return str.charAt(0).toUpperCase();
        }

        return str.charAt(0).toUpperCase() + str.substr(1);
    }

    ss.utils.toCamelCase = function (str) {
        if (!str || !str.length) {
            return str;
        }

        if (str.length === 1) {
            return str.charAt(0).toLowerCase();
        }

        return str.charAt(0).toLowerCase() + str.substr(1);
    }

    ss.utils.truncateString = function (str, maxLength) {
        if (!str || !str.length || str.length <= maxLength) {
            return str;
        }

        return str.substr(0, maxLength);
    };

    ss.utils.truncateStringWithPostfix = function (str, maxLength, postfix) {
        postfix = postfix || '...';

        if (!str || !str.length || str.length <= maxLength) {
            return str;
        }

        if (maxLength <= postfix.length) {
            return postfix.substr(0, maxLength);
        }

        return str.substr(0, maxLength - postfix.length) + postfix;
    };

    ss.utils.isFunction = function (obj) {
        return !!(obj && obj.constructor && obj.call && obj.apply);
    };

    /**
     * parameterInfos should be an array of { name, value } objects
     * where name is query string parameter name and value is it's value.
     * includeQuestionMark is true by default.
     */
    ss.utils.buildQueryString = function (parameterInfos, includeQuestionMark) {
        if (includeQuestionMark === undefined) {
            includeQuestionMark = true;
        }

        var qs = '';

        function addSeperator() {
            if (!qs.length) {
                if (includeQuestionMark) {
                    qs = qs + '?';
                }
            } else {
                qs = qs + '&';
            }
        }

        for (var i = 0; i < parameterInfos.length; ++i) {
            var parameterInfo = parameterInfos[i];
            if (parameterInfo.value === undefined) {
                continue;
            }

            if (parameterInfo.value === null) {
                parameterInfo.value = '';
            }

            addSeperator();

            if (parameterInfo.value.toJSON && typeof parameterInfo.value.toJSON === "function") {
                qs = qs + parameterInfo.name + '=' + encodeURIComponent(parameterInfo.value.toJSON());
            } else if (Array.isArray(parameterInfo.value) && parameterInfo.value.length) {
                for (var j = 0; j < parameterInfo.value.length; j++) {
                    if (j > 0) {
                        addSeperator();
                    }

                    qs = qs + parameterInfo.name + '[' + j + ']=' + encodeURIComponent(parameterInfo.value[j]);
                }
            } else {
                qs = qs + parameterInfo.name + '=' + encodeURIComponent(parameterInfo.value);
            }
        }

        return qs;
    }

    /**
     * Sets a cookie value for given key.
     * This is a simple implementation created to be used by SS.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @param {string} value
     * @param {Date} expireDate (optional). If not specified the cookie will expire at the end of session.
     * @param {string} path (optional)
     */
    ss.utils.setCookieValue = function (key, value, expireDate, path) {
        var cookieValue = encodeURIComponent(key) + '=';

        if (value) {
            cookieValue = cookieValue + encodeURIComponent(value);
        }

        if (expireDate) {
            cookieValue = cookieValue + "; expires=" + expireDate.toUTCString();
        }

        if (path) {
            cookieValue = cookieValue + "; path=" + path;
        }

        document.cookie = cookieValue;
    };

    /**
     * Gets a cookie with given key.
     * This is a simple implementation created to be used by SS.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @returns {string} Cookie value or null
     */
    ss.utils.getCookieValue = function (key) {
        var equalities = document.cookie.split('; ');
        for (var i = 0; i < equalities.length; i++) {
            if (!equalities[i]) {
                continue;
            }

            var splitted = equalities[i].split('=');
            if (splitted.length != 2) {
                continue;
            }

            if (decodeURIComponent(splitted[0]) === key) {
                return decodeURIComponent(splitted[1] || '');
            }
        }

        return null;
    };

    /**
     * Deletes cookie for given key.
     * This is a simple implementation created to be used by SS.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @param {string} path (optional)
     */
    ss.utils.deleteCookie = function (key, path) {
        var cookieValue = encodeURIComponent(key) + '=';

        cookieValue = cookieValue + "; expires=" + (new Date(new Date().getTime() - 86400000)).toUTCString();

        if (path) {
            cookieValue = cookieValue + "; path=" + path;
        }

        document.cookie = cookieValue;
    }

    /**
     * Escape HTML to help prevent XSS attacks.
     */
    ss.utils.htmlEscape = function (html) {
        return typeof html === 'string' ? html.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;') : html;
    }

    /* SECURITY ***************************************/
    ss.security = ss.security || {};
    ss.security.antiForgery = ss.security.antiForgery || {};

    ss.security.antiForgery.tokenCookieName = 'XSRF-TOKEN';
    ss.security.antiForgery.tokenHeaderName = 'RequestVerificationToken';

    ss.security.antiForgery.getToken = function () {
        return ss.utils.getCookieValue(ss.security.antiForgery.tokenCookieName);
    };

    /* CLOCK *****************************************/
    ss.clock = ss.clock || {};

    ss.clock.kind = 'Unspecified';

    ss.clock.supportsMultipleTimezone = function () {
        return ss.clock.kind === 'Utc';
    };

    var toLocal = function (date) {
        return new Date(
            date.getFullYear(),
            date.getMonth(),
            date.getDate(),
            date.getHours(),
            date.getMinutes(),
            date.getSeconds(),
            date.getMilliseconds()
        );
    };

    var toUtc = function (date) {
        return Date.UTC(
            date.getUTCFullYear(),
            date.getUTCMonth(),
            date.getUTCDate(),
            date.getUTCHours(),
            date.getUTCMinutes(),
            date.getUTCSeconds(),
            date.getUTCMilliseconds()
        );
    };

    ss.clock.now = function () {
        if (ss.clock.kind === 'Utc') {
            return toUtc(new Date());
        }
        return new Date();
    };

    ss.clock.normalize = function (date) {
        var kind = ss.clock.kind;

        if (kind === 'Unspecified') {
            return date;
        }

        if (kind === 'Local') {
            return toLocal(date);
        }

        if (kind === 'Utc') {
            return toUtc(date);
        }
    };

    /* FEATURES *************************************************/

    ss.features = ss.features || {};

    ss.features.values = ss.features.values || {};

    ss.features.isEnabled = function(name){
        var value = ss.features.get(name);
        return value == 'true' || value == 'True';
    }

    ss.features.get = function (name) {
        return ss.features.values[name];
    };

    /* GLOBAL FEATURES *************************************************/

    ss.globalFeatures = ss.globalFeatures || {};

    ss.globalFeatures.enabledFeatures = ss.globalFeatures.enabledFeatures || [];

    ss.globalFeatures.isEnabled = function(name){
        return ss.globalFeatures.enabledFeatures.indexOf(name) != -1;
    }

})();
