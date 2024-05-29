(function () {
    var l = ss.localization.getResource('SmartSoftwareTenantManagement');
    var _tenantAppService = smartsoftware.tenantManagement.tenant;

    var _editModal = new ss.ModalManager(
        ss.appPath + 'TenantManagement/Tenants/EditModal'
    );
    var _createModal = new ss.ModalManager(
        ss.appPath + 'TenantManagement/Tenants/CreateModal'
    );
    var _featuresModal = new ss.ModalManager(
        ss.appPath + 'FeatureManagement/FeatureManagementModal'
    );

    var _dataTable = null;

    ss.ui.extensions.entityActions.get('tenantManagement.tenant').addContributor(
        function(actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: ss.auth.isGranted(
                            'SmartSoftwareTenantManagement.Tenants.Update'
                        ),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Features'),
                        visible: ss.auth.isGranted(
                            'SmartSoftwareTenantManagement.Tenants.ManageFeatures'
                        ),
                        action: function (data) {
                            _featuresModal.open({
                                providerName: 'T',
                                providerKey: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: ss.auth.isGranted(
                            'SmartSoftwareTenantManagement.Tenants.Delete'
                        ),
                        confirmMessage: function (data) {
                            return l(
                                'TenantDeletionConfirmationMessage',
                                data.record.name
                            );
                        },
                        action: function (data) {
                            _tenantAppService
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

    ss.ui.extensions.tableColumns.get('tenantManagement.tenant').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: ss.ui.extensions.entityActions.get('tenantManagement.tenant').actions.toArray()
                        }
                    },
                    {
                        title: l("TenantName"),
                        data: 'name',
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );

    $(function () {
        var _$wrapper = $('#TenantsWrapper');

        _dataTable = _$wrapper.find('table').DataTable(
            ss.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                processing: true,
                paging: true,
                scrollX: true,
                serverSide: true,
                ajax: ss.libs.datatables.createAjax(_tenantAppService.getList),
                columnDefs: ss.ui.extensions.tableColumns.get('tenantManagement.tenant').columns.toArray(),
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reloadEx();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reloadEx();
        });

        $('#SmartSoftwareContentToolbar button[name=CreateTenant]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})();
