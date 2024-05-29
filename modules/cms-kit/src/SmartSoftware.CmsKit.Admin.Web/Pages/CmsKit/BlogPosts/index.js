
$(function () {
    var l = ss.localization.getResource("CmsKit");
    var $statusFilter = $("#StatusSelect");
    
    var blogPostStatus = {
        Draft: 0,
        Published: 1,
        SendToReview: 2
    };
    
    var blogsService = smartsoftware.cmsKit.admin.blogs.blogPostAdmin;
    
    var getFilter = function () {
        var filter = {
            filter: $('#CmsKitBlogPostsWrapper input.page-search-filter-text').val()
        };

        if ($statusFilter.val()) {
            filter.status = $statusFilter.val();
        }

        return filter;
    };
    
    var dataTable = $("#BlogPostsTable").DataTable(ss.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollCollapse: true,
        scrollX: true,
        ordering: true,
        order: [[2, "desc"]],
        ajax: ss.libs.datatables.createAjax(blogsService.getList, getFilter),
        columnDefs: [
            {
                title: l("Details"),
                targets: 0,
                rowAction: {
                    items: [
                        {
                            text: l('Edit'),
                            visible: ss.auth.isGranted('CmsKit.BlogPosts.Update'),
                            action: function (data) {
                                location.href = "BlogPosts/Update/" + data.record.id
                            }
                        },
                        {
                            text: l('Publish'),
                            visible: function(data) {
                                return data?.status !== blogPostStatus.Published && ss.auth.isGranted('CmsKit.BlogPosts.Publish');
                            },
                            confirmMessage: function (data) {
                                return l("BlogPostPublishConfirmationMessage", data.record.title)
                            },
                            action: function (data) {
                                blogsService
                                    .publish(data.record.id)
                                    .then(function () {
                                        dataTable.ajax.reloadEx();
                                        ss.notify.success(l('SuccessfullyPublished'));
                                        checkHasBlogPostWaitingForReview();
                                    });
                            }
                        },
                        {
                            text: l('SendToReview'),
                            visible: function(data) {
                                return data?.status === blogPostStatus.Draft && 
                                    !ss.auth.isGranted('CmsKit.BlogPosts.Publish');
                            },
                            confirmMessage: function (data) {
                                return l("BlogPostPublishConfirmationMessage", data.record.title)
                            },
                            action: function (data) {
                                blogsService
                                    .sendToReview(data.record.id)
                                    .then(function () {
                                        dataTable.ajax.reloadEx();
                                        ss.notify.success(l('BlogPostSendToReviewSuccessMessage', data.record.title));
                                    });
                            }
                        },
                        {
                            text: l('Draft'),
                            visible: function(data) {
                                return data?.status !== blogPostStatus.Draft && ss.auth.isGranted('CmsKit.BlogPosts.Update');
                            },
                            confirmMessage: function (data) {
                                return l("BlogPostDraftConfirmationMessage", data.record.title)
                            },
                            action: function (data) {
                                blogsService
                                    .draft(data.record.id)
                                    .then(function () {
                                        dataTable.ajax.reloadEx();
                                        ss.notify.success(l('SavedSuccessfully'));
                                        checkHasBlogPostWaitingForReview();
                                    });
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: ss.auth.isGranted('CmsKit.BlogPosts.Delete'),
                            confirmMessage: function (data) {
                                return l("BlogPostDeletionConfirmationMessage", data.record.title)
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
                title: l("Blog"),
                orderable: false,
                data: "blogName"
            },
            {
                title: l("Title"),
                orderable: true,
                data: "title"
            },
            {
                title: l("Slug"),
                orderable: true,
                data: "slug"
            },
            {
                title: l("CreationTime"),
                orderable: true,
                data: 'creationTime',
                dataFormat: "datetime"
            },
            {
                title: l("Status"),
                orderable: true,
                data: "status",
                render: function (data) {
                    return l("CmsKit.BlogPost.Status." + data);
                }
            },
        ]
    }));

    $('#CmsKitBlogPostsWrapper form.page-search-form').submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();
    });
    
    $('#SmartSoftwareContentToolbar button[name=CreateBlogPost]').on('click', function (e) {
        e.preventDefault();
        window.location.href = "BlogPosts/Create"
    });
    
    $('#button-show-waiting-for-review').on('click', function (e) {
        e.preventDefault();
        $statusFilter.val(blogPostStatus.SendToReview);
        dataTable.ajax.reloadEx();
    });
    
    function checkHasBlogPostWaitingForReview(){
        if (!ss.auth.isGranted('CmsKit.BlogPosts.Publish')){
            $('#alertHasBlogPostWaitingForReview').hide();
            return;
        }
        
        blogsService.hasBlogPostWaitingForReview().then(function (result) {
            if (result) {
                $('#alertHasBlogPostWaitingForReview').show('fast');
            } else {
                $('#alertHasBlogPostWaitingForReview').hide('fast');
            }
        });
    }
    checkHasBlogPostWaitingForReview();
});
