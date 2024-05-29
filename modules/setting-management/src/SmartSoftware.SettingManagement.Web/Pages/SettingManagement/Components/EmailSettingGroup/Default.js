(function ($) {

    var _sendTestEmailModal = new ss.ModalManager(
        ss.appPath + 'SettingManagement/Components/EmailSettingGroup/SendTestEmailModal'
    );

    $(function () {

        var l = ss.localization.getResource('SmartSoftwareSettingManagement');

        $("#EmailSettingsForm").on('submit', function (event) {
            event.preventDefault();

            if (!$(this).valid()) {
                return;
            }

            var form = $(this).serializeFormToObject();
            smartsoftware.settingManagement.emailSettings.update(form).then(function (result) {
                $(document).trigger("SmartSoftwareSettingSaved");
            });

        });

        $('#SmtpUseDefaultCredentials').change(function () {
            if (this.checked) {
                $('#HideSectionWhenUseDefaultCredentialsIsChecked').slideUp();
            } else {
                $('#HideSectionWhenUseDefaultCredentialsIsChecked').slideDown();
            }
        });

        _sendTestEmailModal.onOpen(function () {
            var $form = _sendTestEmailModal.getForm();
            _sendTestEmailModal.getForm().off('ss-ajax-success');

            $form.on('ss-ajax-success', function () {
                _sendTestEmailModal.setResult();
            });
        })
        
        _sendTestEmailModal.onResult(function () {
            ss.notify.success(l('SuccessfullySent'));
        });

        $("#SendTestEmailButton").click(function (e) {
            e.preventDefault();
            _sendTestEmailModal.open();
        });
    });

})(jQuery);
