$(function () {
    var l = ss.localization.getResource("CmsKit");

    var createModal = new ss.ModalManager(ss.appPath + "CmsKit/Tags/CreateModal");
    var updateModal = new ss.ModalManager(ss.appPath + "CmsKit/Tags/EditModal");

    var service = smartsoftware.cmsKit.admin.tags.tagAdmin;

    var getFilter = function () {
        return {
            filter: $('#CmsKitTagsWrapper input.page-search-filter-text').val()
        };
    };

    let dataTable = $("#TagsTable").DataTable(ss.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollCollapse: true,
        scrollX: true,
        ordering: false,
        ajax: ss.libs.datatables.createAjax(service.getList, getFilter),
        columnDefs: [
            {
                title: l("Actions"),
                rowAction: {
                    items: [
                        {
                            text: l("Edit"),
                            visible: ss.auth.isGranted('CmsKit.Tags.Update'),
                            action: function (data) {
                                updateModal.open({ id: data.record.id });
                            }
                        },
                        {
                            text: l("Delete"),
                            visible: ss.auth.isGranted('CmsKit.Tags.Delete'),
                            confirmMessage: function (data) {
                                return l("TagDeletionConfirmationMessage", data.record.name)
                            },
                            action: function (data) {
                                service
                                    .delete(data.record.id)
                                    .then(function () {
                                        dataTable.ajax.reload(null, false);
                                    });
                            }
                        }
                    ]
                }
            },
            {
                title: l("EntityType"),
                data: "entityType"
            },
            {
                title: l("Name"),
                data: "name"
            }
        ]
    }));

    $('#CmsKitTagsWrapper form.page-search-form').on('submit', function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();
    });

    $('#SmartSoftwareContentToolbar button[name=NewButton]').on('click', function (e) {
        e.preventDefault();
        createModal.open();
    });

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();
    });

    updateModal.onResult(function () {
        dataTable.ajax.reloadEx();
    });
});