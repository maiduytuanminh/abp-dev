(function ($) {
    $(function () {
        var l = ss.localization.getResource("SmartSoftwareAccount");

        $('#PersonalSettingsForm').submit(function (e) {
            e.preventDefault();

            if (!$('#PersonalSettingsForm').valid()) {
                return false;
            }

            var input = $('#PersonalSettingsForm').serializeFormToObject(false);

            smartsoftware.account.profile.update(input).then(function (result) {
                ss.notify.success(l('PersonalSettingsSaved'));
                updateConcurrencyStamp();
            });
        });
    });

    ss.event.on('passwordChanged', updateConcurrencyStamp);
    
    function updateConcurrencyStamp(){
        smartsoftware.account.profile.get().then(function(profile){
            $("#ConcurrencyStamp").val(profile.concurrencyStamp);
        });
    }
})(jQuery);
