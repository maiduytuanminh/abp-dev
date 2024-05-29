(function ($) {
    var l = ss.localization.getResource('SmartSoftwareSettingManagement');

    $('#tabs-nav .nav-item .nav-link').click(function () {
        var _this = $(this);
        if(_this.attr('data-bs-target') !== undefined) {
            return;
        }

        var id = _this.data("id");
        var tabId = id.replace(/\./g, '-');
        ss.ui.block({
          elm: '#tab-content',
          busy: true,
          promise: ss.ajax({
               type: 'POST',
               url: 'SettingManagement?handler=RenderView&id=' + id,
               dataType: "html",
               contentType: false,
               processData: false
           }).done(function (response) {
               $('#tab-content').children('.tab-pane').removeClass('show').removeClass('active');
               _this.attr('data-bs-target', '#' + tabId);
               $('#tab-content').append('<div id=' + tabId + ' class="tab-pane fade active show ss-md-form">' + response + '</div>');
           })
        });
    }).first().click();

    $(document).on('SmartSoftwareSettingSaved', function () {
        ss.notify.success(l('SavedSuccessfully'));

        ss.ajax({
           url: ss.appPath + 'SettingManagement?handler=RefreshConfiguration'
        });
    });
})(jQuery);
