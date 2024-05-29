
$(function () {
    var l = ss.localization.getResource("CmsKit");

    var createModal = new ss.ModalManager({ viewUrl: ss.appPath + "CmsKit/Blogs/CreateModal", modalClass: 'createBlog' });
    var updateModal = new ss.ModalManager({ viewUrl: ss.appPath + "CmsKit/Blogs/UpdateModal", modalClass: 'updateBlog' });
    var featuresModal = new ss.ModalManager(ss.appPath + "CmsKit/Blogs/FeaturesModal");

    var blogsService = smartsoftware.cmsKit.admin.blogs.blogAdmin;

    var dataTable = $("#BlogsTable").DataTable(ss.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollCollapse: true,
        scrollX: true,
        ordering: true,
        order: [[1, "desc"]],
        ajax: ss.libs.datatables.createAjax(blogsService.getList),
        columnDefs: [
            {
                title: l("Details"),
                targets: 0,
                rowAction: {
                    items: [
                        {
                            text: l('Features'),
                            visible: ss.auth.isGranted('CmsKit.Blogs.Features'),
                            action: function (data) {
                                featuresModal.open({ blogId: data.record.id });
                            }
                        },
                        {
                            text: l('Edit'),
                            visible: ss.auth.isGranted('CmsKit.Blogs.Update'),
                            action: function (data) {
                                updateModal.open({ id: data.record.id });
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: ss.auth.isGranted('CmsKit.Blogs.Delete'),
                            confirmMessage: function (data) {
                                return l("BlogDeletionConfirmationMessage", data.record.name)
                            },
                            action: function (data) {
                                blogsService
                                    .delete(data.record.id)
                                    .then(function () {
                                        dataTable.ajax.reloadEx();
                                        ss.notify.success(l('DeletedSuccessfully'));
                                    });
                            }
                        }
                    ]
                }
            },
            {
                title: l("Name"),
                orderable: true,
                data: "name"
            },
            {
                title: l("Slug"),
                orderable: true,
                data: "slug"
            }
        ]
    }));

    $('#SmartSoftwareContentToolbar button[name=CreateBlog]').on('click', function (e) {
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