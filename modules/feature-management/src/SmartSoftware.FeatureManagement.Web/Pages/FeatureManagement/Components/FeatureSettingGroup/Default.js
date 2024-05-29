(function($){

    $(function(){
        var _featuresModal = new ss.ModalManager(
            ss.appPath + 'FeatureManagement/FeatureManagementModal'
        );

        $("#ManageHostFeatures").click(function (e) {
            e.preventDefault();
            _featuresModal.open({
                providerName: 'T'
            });
        });
    })
    
})(jQuery);