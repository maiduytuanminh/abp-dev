var ss = ss || {};
(function($) {

    if (!$) {
        throw "ss/jquery library requires the jquery library included to the page!";
    }

    // SS CORE OVERRIDES /////////////////////////////////////////////////////

    ss.message._showMessage = function (message, title) {
        alert((title || '') + ' ' + message);

        return $.Deferred(function ($dfd) {
            $dfd.resolve();
        });
    };

    ss.message.confirm = function (message, titleOrCallback, callback) {
        if (titleOrCallback && !(typeof titleOrCallback == 'string')) {
            callback = titleOrCallback;
        }

        var result = confirm(message);
        callback && callback(result);

        return $.Deferred(function ($dfd) {
            $dfd.resolve(result);
        });
    };

    ss.utils.isFunction = function (obj) {
        return $.isFunction(obj);
    };

    // JQUERY EXTENSIONS //////////////////////////////////////////////////////

    $.fn.findWithSelf = function (selector) {
        return this.filter(selector).add(this.find(selector));
    };

    // DOM ////////////////////////////////////////////////////////////////////

    ss.dom = ss.dom || {};

    ss.dom.onNodeAdded = function (callback) {
        ss.event.on('ss.dom.nodeAdded', callback);
    };

    ss.dom.onNodeRemoved = function (callback) {
        ss.event.on('ss.dom.nodeRemoved', callback);
    };

    var mutationObserverCallback = function (mutationsList) {
        for (var i = 0; i < mutationsList.length; i++) {
            var mutation = mutationsList[i];
            if (mutation.type === 'childList') {
                if (mutation.addedNodes && mutation.removedNodes.length) {
                    for (var k = 0; k < mutation.removedNodes.length; k++) {
                        ss.event.trigger(
                            'ss.dom.nodeRemoved',
                            {
                                $el: $(mutation.removedNodes[k])
                            }
                        );
                    }
                }

                if (mutation.addedNodes && mutation.addedNodes.length) {
                    for (var j = 0; j < mutation.addedNodes.length; j++) {
                        ss.event.trigger(
                            'ss.dom.nodeAdded',
                            {
                                $el: $(mutation.addedNodes[j])
                            }
                        );
                    }
                }
            }
        }
    };

    $(function(){
        new MutationObserver(mutationObserverCallback).observe(
            $('body')[0],
            {
                subtree: true,
                childList: true
            }
        );
    });    

    // AJAX ///////////////////////////////////////////////////////////////////

    ss.ajax = function (userOptions) {
        userOptions = userOptions || {};

        var options = $.extend(true, {}, ss.ajax.defaultOpts, userOptions);

        options.success = undefined;
        options.error = undefined;

        var xhr = null;
        var promise = $.Deferred(function ($dfd) {
            xhr = $.ajax(options)
                .done(function (data, textStatus, jqXHR) {
                    $dfd.resolve(data);
                    userOptions.success && userOptions.success(data);
                }).fail(function (jqXHR) {
                    if(jqXHR.statusText === 'abort') {
                        //ajax request is abort, ignore error handle.
                        return;
                    }
                    if (jqXHR.getResponseHeader('_SmartSoftwareErrorFormat') === 'true') {
                        ss.ajax.handleSmartSoftwareErrorResponse(jqXHR, userOptions, $dfd);
                    } else {
                        ss.ajax.handleNonSmartSoftwareErrorResponse(jqXHR, userOptions, $dfd);
                    }
                });
        }).promise();

        promise['jqXHR'] = xhr;

        return promise;
    };

    $.extend(ss.ajax, {
        defaultOpts: {
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            }
        },

        defaultError: {
            message: 'An error has occurred!',
            details: 'Error detail not sent by server.'
        },

        defaultError401: {
            message: 'You are not authenticated!',
            details: 'You should be authenticated (sign in) in order to perform this operation.'
        },

        defaultError403: {
            message: 'You are not authorized!',
            details: 'You are not allowed to perform this operation.'
        },

        defaultError404: {
            message: 'Resource not found!',
            details: 'The resource requested could not found on the server.'
        },

        logError: function (error) {
            ss.log.error(error);
        },

        showError: function (error) {
            if (error.details) {
                return ss.message.error(error.details, error.message);
            } else {
                return ss.message.error(error.message || ss.ajax.defaultError.message);
            }
        },

        handleTargetUrl: function (targetUrl) {
            if (!targetUrl) {
                location.href = ss.appPath;
            } else {
                location.href = targetUrl;
            }
        },

        handleErrorStatusCode: function (status) {
            switch (status) {
                case 401:
                    ss.ajax.handleUnAuthorizedRequest(
                        ss.ajax.showError(ss.ajax.defaultError401),
                        ss.appPath
                    );
                    break;
                case 403:
                    ss.ajax.showError(ss.ajax.defaultError403);
                    break;
                case 404:
                    ss.ajax.showError(ss.ajax.defaultError404);
                    break;
                default:
                    ss.ajax.showError(ss.ajax.defaultError);
                    break;
            }
        },

        handleNonSmartSoftwareErrorResponse: function (jqXHR, userOptions, $dfd) {
            if (userOptions.ssHandleError !== false) {
                ss.ajax.handleErrorStatusCode(jqXHR.status);
            }

            $dfd.reject.apply(this, arguments);
            userOptions.error && userOptions.error.apply(this, arguments);
        },

        handleSmartSoftwareErrorResponse: function (jqXHR, userOptions, $dfd) {
            var messagePromise = null;

            var responseJSON = jqXHR.responseJSON ? jqXHR.responseJSON : JSON.parse(jqXHR.responseText);

            if (userOptions.ssHandleError !== false) {
                messagePromise = ss.ajax.showError(responseJSON.error);
            }

            ss.ajax.logError(responseJSON.error);

            $dfd && $dfd.reject(responseJSON.error, jqXHR);
            userOptions.error && userOptions.error(responseJSON.error, jqXHR);

            if (jqXHR.status === 401 && userOptions.ssHandleError !== false) {
                ss.ajax.handleUnAuthorizedRequest(messagePromise);
            }
        },

        handleUnAuthorizedRequest: function (messagePromise, targetUrl) {
            if (messagePromise) {
                messagePromise.done(function () {
                    ss.ajax.handleTargetUrl(targetUrl);
                });
            } else {
                ss.ajax.handleTargetUrl(targetUrl);
            }
        },

        blockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //block whole page
                    ss.ui.setBusy();
                } else { //block an element
                    ss.ui.setBusy(options.blockUI);
                }
            }
        },

        unblockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //unblock whole page
                    ss.ui.clearBusy();
                } else { //unblock an element
                    ss.ui.clearBusy(options.blockUI);
                }
            }
        },

        ajaxSendHandler: function (event, request, settings) {
            var token = ss.security.antiForgery.getToken();
            if (!token) {
                return;
            }

            if (!settings.headers || settings.headers[ss.security.antiForgery.tokenHeaderName] === undefined) {
                request.setRequestHeader(ss.security.antiForgery.tokenHeaderName, token);
            }
        }
    });

    $(document).ajaxSend(function (event, request, settings) {
        return ss.ajax.ajaxSendHandler(event, request, settings);
    });

    ss.event.on('ss.configurationInitialized', function () {
        var l = ss.localization.getResource('SmartSoftwareUi');

        ss.ajax.defaultError.message = l('DefaultErrorMessage');
        ss.ajax.defaultError.details = l('DefaultErrorMessageDetail');
        ss.ajax.defaultError401.message = l('DefaultErrorMessage401');
        ss.ajax.defaultError401.details = l('DefaultErrorMessage401Detail');
        ss.ajax.defaultError403.message = l('DefaultErrorMessage403');
        ss.ajax.defaultError403.details = l('DefaultErrorMessage403Detail');
        ss.ajax.defaultError404.message = l('DefaultErrorMessage404');
        ss.ajax.defaultError404.details = l('DefaultErrorMessage404Detail');
    });

    // RESOURCE LOADER ////////////////////////////////////////////////////////

    /* UrlStates enum */
    var UrlStates = {
        LOADING: 'LOADING',
        LOADED: 'LOADED',
        FAILED: 'FAILED'
    };

    /* UrlInfo class */
    function UrlInfo(url) {
        this.url = url;
        this.state = UrlStates.LOADING;
        this.loadCallbacks = [];
        this.failCallbacks = [];
    }

    UrlInfo.prototype.succeed = function () {
        this.state = UrlStates.LOADED;
        for (var i = 0; i < this.loadCallbacks.length; i++) {
            this.loadCallbacks[i]();
        }
    };

    UrlInfo.prototype.failed = function () {
        this.state = UrlStates.FAILED;
        for (var i = 0; i < this.failCallbacks.length; i++) {
            this.failCallbacks[i]();
        }
    };

    UrlInfo.prototype.handleCallbacks = function (loadCallback, failCallback) {
        switch (this.state) {
            case UrlStates.LOADED:
                loadCallback && loadCallback();
                break;
            case UrlStates.FAILED:
                failCallback && failCallback();
                break;
            case UrlStates.LOADING:
                this.addCallbacks(loadCallback, failCallback);
                break;
        }
    };

    UrlInfo.prototype.addCallbacks = function (loadCallback, failCallback) {
        loadCallback && this.loadCallbacks.push(loadCallback);
        failCallback && this.failCallbacks.push(failCallback);
    };

    /* ResourceLoader API */

    ss.ResourceLoader = (function () {

        var _urlInfos = {};

        function getCacheKey(url) {
            return url;
        }

        function appendTimeToUrl(url) {

            if (url.indexOf('?') < 0) {
                url += '?';
            } else {
                url += '&';
            }

            url += '_=' + new Date().getTime();

            return url;
        }

        var _loadFromUrl = function (url, loadCallback, failCallback, serverLoader) {

            var cacheKey = getCacheKey(url);

            var urlInfo = _urlInfos[cacheKey];

            if (urlInfo) {
                urlInfo.handleCallbacks(loadCallback, failCallback);
                return;
            }

            _urlInfos[cacheKey] = urlInfo = new UrlInfo(url);
            urlInfo.addCallbacks(loadCallback, failCallback);

            serverLoader(urlInfo);
        };

        var _loadScript = function (url, loadCallback, failCallback) {
            var nonce = document.body.nonce || document.body.getAttribute('nonce');
            _loadFromUrl(url, loadCallback, failCallback, function (urlInfo) {
                $.get({
                    url: url,
                    dataType: 'text'
                })
                .done(function (script) {
                    if(nonce){
                        $.globalEval(script, { nonce: nonce});
                    }else{
                        $.globalEval(script);
                    }
                    urlInfo.succeed();
                })
                .fail(function () {
                    urlInfo.failed();
                });
            });
        };

        var _loadStyle = function (url) {
            _loadFromUrl(url, undefined, undefined, function (urlInfo) {

                $('<link/>', {
                    rel: 'stylesheet',
                    type: 'text/css',
                    href: appendTimeToUrl(url)
                }).appendTo('head');
            });
        };

        return {
            loadScript: _loadScript,
            loadStyle: _loadStyle
        }
    })();

})(jQuery);