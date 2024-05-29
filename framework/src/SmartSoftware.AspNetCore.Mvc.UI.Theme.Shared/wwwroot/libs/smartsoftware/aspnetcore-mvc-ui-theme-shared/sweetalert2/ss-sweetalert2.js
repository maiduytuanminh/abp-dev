var ss = ss || {};
(function ($) {
    if (!Swal || !$) {
        return;
    }

    /* DEFAULTS *************************************************/

    ss.libs = ss.libs || {};
    ss.libs.sweetAlert = {
        config: {
            'default': {

            },
            info: {
                icon: 'info'
            },
            success: {
                icon: 'success'
            },
            warn: {
                icon: 'warning'
            },
            error: {
                icon: 'error'
            },
            confirm: {
                icon: 'warning',
                title: 'Are you sure?',
                showCancelButton: true,
                reverseButtons: true
            }
        }
    };

    /* MESSAGE **************************************************/

    ss.utils = ss.utils || {};
    ss.utils.htmlEscape = ss.utils.htmlEscape || function (str) { return str; };
    var showMessage = function (type, message, title) {
        var opts = $.extend(
            {},
            ss.libs.sweetAlert.config['default'],
            ss.libs.sweetAlert.config[type],
            {
                title: title,
                html: ss.utils.htmlEscape(message).replace(/\n/g, '<br>')
            }
        );

        return $.Deferred(function ($dfd) {
            Swal.fire(opts).then(function () {
                $dfd.resolve();
            });
        });
    };

    ss.message.info = function (message, title) {
        return showMessage('info', message, title);
    };

    ss.message.success = function (message, title) {
        return showMessage('success', message, title);
    };

    ss.message.warn = function (message, title) {
        return showMessage('warn', message, title);
    };

    ss.message.error = function (message, title) {
        return showMessage('error', message, title);
    };

    ss.message.confirm = function (message, titleOrCallback, callback) {

        var userOpts = {
            text: message
        };

        if ($.isFunction(titleOrCallback)) {
            closeOnEsc = callback;
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        var opts = $.extend(
            {},
            ss.libs.sweetAlert.config['default'],
            ss.libs.sweetAlert.config.confirm,
            userOpts
        );

        return $.Deferred(function ($dfd) {
            Swal.fire(opts).then(result  => {
                callback && callback(result.value);
                $dfd.resolve(result.value);
            })
        });
    };

    ss.event.on('ss.configurationInitialized', function () {
        var l = ss.localization.getResource('SmartSoftwareUi');

        ss.libs.sweetAlert.config.default.confirmButtonText = l('Ok');
        ss.libs.sweetAlert.config.default.denyButtonText = l('No');
        ss.libs.sweetAlert.config.default.cancelButtonText = l('Cancel');
        ss.libs.sweetAlert.config.default.buttonsStyling = false;
        ss.libs.sweetAlert.config.default.customClass = {
            confirmButton: "btn btn-primary",
            cancelButton: "btn btn-outline-primary mx-2",
            denyButton: "btn btn-outline-primary mx-2"
        };

        ss.libs.sweetAlert.config.confirm.title = l('AreYouSure');
        ss.libs.sweetAlert.config.confirm.confirmButtonText = l('Yes');
        ss.libs.sweetAlert.config.confirm.showCancelButton = true;
        ss.libs.sweetAlert.config.confirm.reverseButtons = true;
    });

})(jQuery);
