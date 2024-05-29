(function ($) {
    var l = ss.localization.getResource('SmartSoftwareIdentity');

    var _identityRoleAppService = smartsoftware.identity.identityRole;
    var _permissionsModal = new ss.ModalManager(
        ss.appPath + 'SmartSoftwarePermissionManagement/PermissionManagementModal'
    );
    var _editModal = new ss.ModalManager(
        ss.appPath + 'Identity/Roles/EditModal'
    );
    var _createModal = new ss.ModalManager(
        ss.appPath + 'Identity/Roles/CreateModal'
    );

    var _dataTable = null;

    ss.ui.extensions.entityActions.get('identity.role').addContributor(
        function(actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: ss.auth.isGranted(
                            'SmartSoftwareIdentity.Roles.Update'
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
                            'SmartSoftwareIdentity.Roles.ManagePermissions'
                        ),
                        action: function (data) {
                            _permissionsModal.open({
                                providerName: 'R',
                                providerKey: data.record.name,
                                providerKeyDisplayName: data.record.name
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: function (data) {
                            return (
                                !data.isStatic &&
                                ss.auth.isGranted(
                                    'SmartSoftwareIdentity.Roles.Delete'
                                )
                            ); //TODO: Check permission
                        },
                        confirmMessage: function (data) {
                            return l(
                                'RoleDeletionConfirmationMessage',
                                data.record.name
                            );
                        },
                        action: function (data) {
                            _identityRoleAppService
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

    ss.ui.extensions.tableColumns.get('identity.role').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: ss.ui.extensions.entityActions.get('identity.role').actions.toArray()
                        }
                    },
                    {
                        title: l('RoleName'),
                        data: 'name',
                        render: function (data, type, row) {
                            var name = '<span>' + $.fn.dataTable.render.text().display(data) + '</span>'; //prevent against possible XSS
                            if (row.isDefault) {
                                name +=
                                    '<span class="badge rounded-pill bg-success ms-1">' +
                                    l('DisplayName:IsDefault') +
                                    '</span>';
                            }
                            if (row.isPublic) {
                                name +=
                                    '<span class="badge rounded-pill bg-info ms-1">' +
                                    l('DisplayName:IsPublic') +
                                    '</span>';
                            }
                            return name;
                        },
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );
    
    $(function () {
        var _$wrapper = $('#IdentityRolesWrapper');
        var _$table = _$wrapper.find('table');

        _dataTable = _$table.DataTable(
            ss.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                searching: false,
                processing: true,
                serverSide: true,
                scrollX: true,
                paging: true,
                ajax: ss.libs.datatables.createAjax(
                    _identityRoleAppService.getList
                ),
                columnDefs: ss.ui.extensions.tableColumns.get('identity.role').columns.toArray()
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reloadEx();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reloadEx();
        });

        $('#SmartSoftwareContentToolbar button[name=CreateRole]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})(jQuery);
