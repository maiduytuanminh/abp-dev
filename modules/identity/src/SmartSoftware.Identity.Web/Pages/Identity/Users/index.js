(function ($) {
    var l = ss.localization.getResource('SmartSoftwareIdentity');

    var _identityUserAppService = smartsoftware.identity.identityUser;

    var togglePasswordVisibility = function () {
        $("#PasswordVisibilityButton").click(function (e) {
            var button = $(this);
            var passwordInput = button.parent().find("input");
            if(!passwordInput) {
                return;
            }

            if(passwordInput.attr("type") === "password") {
                passwordInput.attr("type", "text");
            }
            else {
                passwordInput.attr("type", "password");
            }

            var icon = button.find("i");
            if(icon) {
                icon.toggleClass("fa-eye-slash").toggleClass("fa-eye");
            }
        });
    }
    
    ss.modals.createUser = function () {
        var initModal = function (publicApi, args) {
            togglePasswordVisibility();
        };
        return { initModal: initModal };
    }
    
    ss.modals.editUser = function () {
        var initModal = function (publicApi, args) {
            togglePasswordVisibility();
        };
        return { initModal: initModal };
    }
    
    var _editModal = new ss.ModalManager({
        viewUrl: ss.appPath + 'Identity/Users/EditModal',
        modalClass: "editUser"
    });
    var _createModal = new ss.ModalManager({
        viewUrl: ss.appPath + 'Identity/Users/CreateModal',
        modalClass: "createUser"
    });
    var _permissionsModal = new ss.ModalManager(
        ss.appPath + 'SmartSoftwarePermissionManagement/PermissionManagementModal'
    );

    var _dataTable = null;

    ss.ui.extensions.entityActions.get('identity.user').addContributor(
        function(actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: ss.auth.isGranted(
                            'SmartSoftwareIdentity.Users.Update'
                        ),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Permissions'),
                        visible: ss.auth.isGranted(
                            'SmartSoftwareIdentity.Users.ManagePermissions'
                        ),
                        action: function (data) {
                            _permissionsModal.open({
                                providerName: 'U',
                                providerKey: data.record.id,
                                providerKeyDisplayName: data.record.userName
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: function(data) {
                            return ss.auth.isGranted('SmartSoftwareIdentity.Users.Delete') && ss.currentUser.id !== data.id;
                        },
                        confirmMessage: function (data) {
                            return l(
                                'UserDeletionConfirmationMessage',
                                data.record.userName
                            );
                        },
                        action: function (data) {
                            _identityUserAppService
                                .delete(data.record.id)
                                .then(function () {
                                    _dataTable.ajax.reloadEx();
                                    ss.notify.success(l('DeletedSuccessfully'));
                                });
                        },
                    }
                ]
            );
        }
    );

    ss.ui.extensions.tableColumns.get('identity.user').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: ss.ui.extensions.entityActions.get('identity.user').actions.toArray()
                        }
                    },
                    {
                        title: l('UserName'),
                        data: 'userName',
                        render: function (data, type, row) {
                            row.userName = $.fn.dataTable.render.text().display(row.userName);
                            if (!row.isActive) {
                                return  '<i data-toggle="tooltip" data-placement="top" title="' +
                                    l('ThisUserIsNotActiveMessage') +
                                    '" class="fa fa-ban text-danger"></i> ' +
                                    '<span class="opc-65">' + row.userName + '</span>';
                            }

                            return row.userName;
                        }
                    },
                    {
                        title: l('EmailAddress'),
                        data: 'email',
                    },
                    {
                        title: l('PhoneNumber'),
                        data: 'phoneNumber',
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );

    $(function () {
        var _$wrapper = $('#IdentityUsersWrapper');
        var _$table = _$wrapper.find('table');
        _dataTable = _$table.DataTable(
            ss.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                processing: true,
                serverSide: true,
                scrollX: true,
                paging: true,
                ajax: ss.libs.datatables.createAjax(
                    _identityUserAppService.getList
                ),
                columnDefs: ss.ui.extensions.tableColumns.get('identity.user').columns.toArray()
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reloadEx();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reloadEx();
        });

        $('#SmartSoftwareContentToolbar button[name=CreateUser]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})(jQuery);
