$(function () {
    var l = ss.localization.getResource('Blogging');
    var _createModal = new ss.ModalManager(
        ss.appPath + 'Blogging/Admin/Blogs/Create'
    );
    var _editModal = new ss.ModalManager(
        ss.appPath + 'Blogging/Admin/Blogs/Edit'
    );

    var _dataTable = $('#BlogsTable').DataTable(
        ss.libs.datatables.normalizeConfiguration({
            processing: true,
            serverSide: true,
            paging: false,
            info: false,
            scrollX: true,
            searching: false,
            autoWidth: false,
            scrollCollapse: true,
            order: [[3, 'desc']],
            ajax: ss.libs.datatables.createAjax(
                smartsoftware.blogging.admin.blogManagement.getList
            ),
            columnDefs: [
                {
                    rowAction: {
                        items: [
                            {
                                text: l('Edit'),
                                visible: ss.auth.isGranted(
                                    'Blogging.Blog.Update'
                                ),
                                action: function (data) {
                                    _editModal.open({
                                        blogId: data.record.id,
                                    });
                                },
                            },
                            {
                                text: l('Delete'),
                                visible: ss.auth.isGranted(
                                    'Blogging.Blog.Delete'
                                ),
                                confirmMessage: function (data) {
                                    return l('BlogDeletionWarningMessage');
                                },
                                action: function (data) {
                                    smartsoftware.blogging.admin.blogManagement
                                        .delete(data.record.id)
                                        .then(function () {
                                            _dataTable.ajax.reloadEx();
                                            ss.notify.success(l('DeletedSuccessfully'));
                                        });
                                },
                            },
                            {
                                text: l("ClearCache"),
                                visible: ss.auth.isGranted(
                                  'Blogging.Blog.ClearCache'
                                ),
                                confirmMessage: function (data) {
                                    return l("ClearCacheConfirmationMessage");
                                },
                                action: function (data) {
                                    smartsoftware.blogging.admin.blogManagement
                                        .clearCache(data.record.id)
                                        .then(function () {
                                            _dataTable.ajax.reloadEx();
                                        })
                                }
                            }
                        ],
                    },
                },
                {
                    target: 1,
                    data: 'name',
                },
                {
                    target: 2,
                    data: 'shortName',
                },
                {
                    target: 3,
                    data: 'creationTime',
                    dataFormat: "datetime"
                },
                {
                    target: 4,
                    data: 'description',
                },
            ],
        })
    );

    $('#CreateNewBlogButtonId').click(function () {
        _createModal.open();
    });

    _createModal.onClose(function () {
        _dataTable.ajax.reloadEx();
    });

    _editModal.onResult(function () {
        _dataTable.ajax.reloadEx();
    });
});
