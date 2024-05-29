var ss = ss || {};
$(function () {
    ss.modals.projectEdit = function () {
        var initModal = function (publicApi, args) {
            var $form = publicApi.getForm();
        };

        return {
            initModal: initModal,
        };
    };
});
