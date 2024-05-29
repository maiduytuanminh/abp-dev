(function ($) {
    $(function () {

        var l = ss.localization.getResource('SmartSoftwareSettingManagement');

        $("#TimeZoneSettingsForm").on('submit', function (event) {
            event.preventDefault();

            smartsoftware.settingManagement.timeZoneSettings.update($("#Timezone").val()).then(function (result) {
                $(document).trigger("SmartSoftwareSettingSaved");
            });

        });
    });
})(jQuery);
