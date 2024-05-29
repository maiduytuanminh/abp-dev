$(function () {

    let l = ss.localization.getResource("CmsKit");

    $('#deleteBlogPost').on('click', '', function (e) {
        ss.message.confirm(l("DeleteBlogPostMessage"), function (ok) {
            if (ok) {
                smartsoftware.cmsKit.public.blogs.blogPostPublic.delete(
                    $('#BlogId').val()
                ).then(function () {
                    document.location.href = "/";
                });
            }
        })
    });
});
