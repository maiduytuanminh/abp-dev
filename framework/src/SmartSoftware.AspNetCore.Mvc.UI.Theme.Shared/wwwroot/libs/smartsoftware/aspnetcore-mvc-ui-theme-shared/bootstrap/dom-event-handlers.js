(function ($) {

    ss.dom = ss.dom || {};

    ss.dom.initializers = ss.dom.initializers || {};

    ss.dom.initializers.initializeForms = function ($forms, validate) {
        if ($forms.length) {
            $forms.each(function () {
                var $form = $(this);

                if (validate === true) {
                    $.validator.unobtrusive.parse($form);
                }

                var confirmText = $form.attr('data-confirm');
                if (confirmText) {
                    $form.submit(function (e) {
                        if (!$form.data('ss-confirmed')) {
                            e.preventDefault();
                            ss.message.confirm(confirmText).done(function (accepted) {
                                if (accepted) {
                                    $form.data('ss-confirmed', true);
                                    $form.submit();
                                    $form.data('ss-confirmed', undefined);
                                }
                            });
                        }
                    });
                }

                if ($form.attr('data-ajaxForm') === 'true') {
                    $form.ssAjaxForm();
                }
            });
        }
    };

    ss.dom.initializers.initializeScript = function ($el) {
        $el.findWithSelf('[data-script-class]').each(function () {
            var scriptClassName = $(this).attr('data-script-class');
            if (!scriptClassName) {
                return;
            }

            var scriptClass = eval(scriptClassName);
            if (!scriptClass) {
                return;
            }

            var scriptObject = new scriptClass();
            $el.data('ss-script-object', scriptObject);

            scriptObject.initDom && scriptObject.initDom($el);
        });
    }

    ss.dom.initializers.initializeToolTips = function ($tooltips) {
        for (var i = 0; i < $tooltips.length; i++) {
            new bootstrap.Tooltip($tooltips[i], {
                container: `body`
              });
        }
    }

    ss.dom.initializers.initializePopovers = function ($popovers) {
        for (var i = 0; i < $popovers.length; i++) {
            new bootstrap.Popover($popovers[i], {
                container: `body`
              });
        }
    }

    ss.dom.initializers.initializeTimeAgos = function ($timeagos) {
        $timeagos.timeago();
    }

    ss.dom.initializers.initializeAutocompleteSelects = function ($autocompleteSelects) {
        if ($autocompleteSelects.length) {
            $autocompleteSelects.each(function () {
                let $select = $(this);
                let url = $(this).data("autocompleteApiUrl");
                let displayName = $(this).data("autocompleteDisplayProperty");
                let displayValue = $(this).data("autocompleteValueProperty");
                let itemsPropertyName = $(this).data("autocompleteItemsProperty");
                let filterParamName = $(this).data("autocompleteFilterParamName");
                let selectedText = $(this).data("autocompleteSelectedItemName");
                let parentSelector = $(this).data("autocompleteParentSelector");
                let allowClear = $(this).data("autocompleteAllowClear");
                let placeholder = $(this).data("autocompletePlaceholder");
                if (allowClear && placeholder == undefined) {
                    placeholder = " ";
                }

                if (!parentSelector && $select.parents(".modal.fade").length === 1) {
                    parentSelector = ".modal.fade";
                }
                let name = $(this).attr("name");
                let selectedTextInputName = name + "_Text";
                if(name.indexOf(".ExtraProperties[") > 0) {
                    selectedTextInputName = name.substring(0, name.length - 1) + "_Text]"
                }
                let selectedTextInput = $('<input>', {
                    type: 'hidden',
                    id: selectedTextInputName,
                    name: selectedTextInputName,
                });
                if (selectedText != "") {
                    selectedTextInput.val(selectedText);
                }
                selectedTextInput.insertAfter($select);
                $select.select2({
                    ajax: {
                        url: url,
                        delay: 250,
                        dataType: "json",
                        data: function (params) {
                            let query = {};
                            query[filterParamName] = params.term;
                            return query;
                        },
                        processResults: function (data) {
                            let retVal = [];
                            let items = data;
                            if (itemsPropertyName) {
                                items = data[itemsPropertyName];
                            }

                            items.forEach(function (item, index) {
                                retVal.push({
                                    id: item[displayValue],
                                    text: item[displayName]
                                })
                            });
                            return {
                                results: retVal
                            };
                        }
                    },
                    width: '100%',
                    dropdownParent: parentSelector ? $(parentSelector) : $('body'),
                    allowClear: allowClear,
                    language: ss.localization.currentCulture.cultureName,
                    placeholder: {
                        id: '-1',
                        text: placeholder
                    }
                });
                $select.on('select2:select', function (e) {
                    selectedTextInput.val(e.params.data.text);
                });
            });
        }
    }

    ss.libs = ss.libs = ss.libs || {};
    ss.libs.bootstrapDatepicker = {
        packageName: "bootstrap-datepicker",
        normalizeLanguageConfig: function () {
            var language = ss.localization.getLanguagesMap(this.packageName);
            var languageConfig = $.fn.datepicker.dates[language];
            if (languageConfig && (!languageConfig.format || language !== ss.localization.currentCulture.name)) {
                languageConfig.format = ss.localization.currentCulture.dateTimeFormat.shortDatePattern.toLowerCase();
            }
        },
        getFormattedValue: function (isoFormattedValue) {
            if (!isoFormattedValue) {
                return isoFormattedValue;
            }
            return luxon
                .DateTime
                .fromISO(isoFormattedValue, {
                    locale: ss.localization.currentCulture.name
                }).toLocaleString();
        },
        getOptions: function ($input) { //$input may needed if developer wants to override this method
            return {
                todayBtn: "linked",
                autoclose: true,
                language: ss.localization.getLanguagesMap(this.packageName)
            };
        }
    };

    ss.dom.initializers.initializeDatepickers = function ($rootElement) {
        $rootElement
            .findWithSelf('input.datepicker,input[type=date][ss-data-datepicker!=false]')
            .each(function () {
                var $input = $(this);
                $input
                    .attr('type', 'text')
                    .val(ss.libs.bootstrapDatepicker.getFormattedValue($input.val()))
                    .datepicker(ss.libs.bootstrapDatepicker.getOptions($input))
                    .on('hide', function (e) {
                        e.stopPropagation();
                    });
            });
    }

   

    ss.dom.initializers.initializeSmartSoftwareCspStyles =  function ($ssCspStyles){
        $ssCspStyles.attr("rel", "stylesheet");
    }

    ss.dom.onNodeAdded(function (args) {
        ss.dom.initializers.initializeToolTips(args.$el.findWithSelf('[data-bs-toggle="tooltip"]'));
        ss.dom.initializers.initializePopovers(args.$el.findWithSelf('[data-bs-toggle="popover"]'));
        ss.dom.initializers.initializeTimeAgos(args.$el.findWithSelf('.timeago'));
        ss.dom.initializers.initializeForms(args.$el.findWithSelf('form'), true);
        ss.dom.initializers.initializeScript(args.$el);
        ss.dom.initializers.initializeAutocompleteSelects(args.$el.findWithSelf('.auto-complete-select'));
        ss.dom.initializers.initializeSmartSoftwareCspStyles($("link[ss-csp-style]"));
    });

    ss.dom.onNodeRemoved(function (args) {
        args.$el.findWithSelf('[data-bs-toggle="tooltip"]').each(function () {
            $('#' + $(this).attr('aria-describedby')).remove();
        });
    });

    ss.event.on('ss.configurationInitialized', function () {
        ss.libs.bootstrapDatepicker.normalizeLanguageConfig();
    });
    

    $(function () {
        ss.dom.initializers.initializeToolTips($('[data-bs-toggle="tooltip"]'));
        ss.dom.initializers.initializePopovers($('[data-bs-toggle="popover"]'));
        ss.dom.initializers.initializeTimeAgos($('.timeago'));
        ss.dom.initializers.initializeDatepickers($(document));
        ss.dom.initializers.initializeForms($('form'));
        ss.dom.initializers.initializeAutocompleteSelects($('.auto-complete-select'));
        $('[data-auto-focus="true"]').first().findWithSelf('input,select').focus();
        ss.dom.initializers.initializeSmartSoftwareCspStyles($("link[ss-csp-style]"));
    });

})(jQuery);
