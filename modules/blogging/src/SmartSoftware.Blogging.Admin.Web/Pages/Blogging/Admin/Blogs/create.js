var ss = ss || {};
$(function () {
    ss.modals.blogCreate = function () {
        var initModal = function (publicApi, args) {
            var $form = publicApi.getForm();
        };

        return {
            initModal: initModal,
        };
    };
});
