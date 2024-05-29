(function ($) {
    if (!$ || !$.fn.ajaxForm) {
        return;
    }

    $.fn.ssAjaxForm = function (userOptions) {
        var $form = $(this);
        userOptions = $.extend({
            formSubmitting: {
                disableSubmitButtonBusy: false,
                disableOtherButtonsBusy: false,
                $excludedBusyButtons: {} /*Prevents busy or disabled effect on these buttons. Must be jQuery instances*/
            }
        }, userOptions || {});

        var options = $.extend({}, $.fn.ssAjaxForm.defaults, userOptions);

        options.beforeSubmit = function (arr, $form) {
            if ((userOptions.beforeSubmit && userOptions.beforeSubmit.apply(this, arguments)) === false) {
                return false;
            }

            if (!$form.valid()) {
                return false;
            }

            if (!userOptions.formSubmitting.disableSubmitButtonBusy) {
                var submitButtons = $form.find("button[type=submit]").not(userOptions.formSubmitting.$excludedBusyButtons);
                if (submitButtons.length === 1) {
                    submitButtons.buttonBusy(true);
                } else if (submitButtons.length > 1) {
                    $(document.activeElement).buttonBusy(true);
                }
            }

            if (!userOptions.formSubmitting.disableOtherButtonsBusy) {
                var otherButtons = $form.find("button[type!=submit]").not(userOptions.formSubmitting.$excludedBusyButtons);
                otherButtons.prop('disabled', true);
            }

            return true;
        };

        options.success = function (responseText, statusText, xhr, $form) {
            userOptions.success && userOptions.success.apply(this, arguments);
            $form.trigger('ss-ajax-success',
                {
                    responseText: responseText,
                    statusText: statusText,
                    xhr: xhr,
                    $form: $form
                });
        };

        options.error = function (jqXhr) {
            if ((userOptions.error && userOptions.error.apply(this, arguments)) === false) {
                return;
            }

            if (jqXhr.getResponseHeader('_SmartSoftwareErrorFormat') === 'true') {
                ss.ajax.logError(jqXhr.responseJSON.error);
                var messagePromise = ss.ajax.showError(jqXhr.responseJSON.error);
                if (jqXhr.status === 401) {
                    ss.ajax.handleUnAuthorizedRequest(messagePromise);
                }
            } else {
                ss.ajax.handleErrorStatusCode(jqXhr.status);
            }
        };

        options.complete = function (jqXhr, status, $form) {
            if ($.contains(document, $form[0])) {
                $form.find("button[type='submit']").buttonBusy(false);
                if (!userOptions.formSubmitting.disableOtherButtonsBusy) {
                    var otherButtons = $form.find("button[type!=submit]").not(userOptions.formSubmitting.$excludedBusyButtons);
                    otherButtons.prop('disabled', false);
                }
            }

            $form.trigger('ss-ajax-complete',
                {
                    status: status,
                    jqXhr: jqXhr,
                    $form: $form
                });

            userOptions.complete && userOptions.complete.apply(this, arguments);
        };

        return $form.ajaxForm(options);
    };

    $.fn.ssAjaxForm.defaults = {
        method: 'POST'
    };

})(jQuery);