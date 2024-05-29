(function($) {

    var tenantSwitchModal = new ss.ModalManager(ss.appPath + 'SmartSoftware/MultiTenancy/TenantSwitchModal');

    $(function() {
        $('#SmartSoftwareTenantSwitchLink').click(function(e) {
            e.preventDefault();
            tenantSwitchModal.open();
        });

        tenantSwitchModal.onResult(function() {
            location.assign(location.href);
        });
    });

})(jQuery);