(function ($) {

    let l = ss.localization.getResource('CmsKit');

    ss.widgets.CmsCommenting = function ($widget) {
        let widgetManager = $widget.data('ss-widget-manager');
        let $commentArea = $widget.find('.cms-comment-area');

        function getFilters() {
            return {
                entityType: $commentArea.attr('data-entity-type'),
                entityId: $commentArea.attr('data-entity-id')
            };
        }

        function registerEditLinks($container) {
            $container.find('.comment-edit-link').each(function () {
                let $link = $(this);
                $link.on('click', function (e) {
                    e.preventDefault();

                    let commentId = $link.data('id');

                    let $relatedCommentContentArea = $container.find('.cms-comment-content-area[data-id=' + commentId + ']');
                    let $relatedCommentEditFormArea = $container.find('.cms-comment-edit-area[data-id=' + commentId + ']');

                    $relatedCommentContentArea.hide();
                    $relatedCommentEditFormArea.show();
                    $link.removeAttr('href');
                });
            });
            $container.find('.comment-edit-cancel-button').each(function () {
                let $button = $(this);
                $button.on('click', function (e) {
                    e.preventDefault();

                    let commentId = $button.data('id');

                    let $relatedCommentContentArea = $container.find('.cms-comment-content-area[data-id=' + commentId + ']');
                    let $relatedCommentEditFormArea = $container.find('.cms-comment-edit-area[data-id=' + commentId + ']');
                    let $link = $container.find('.comment-edit-link[data-id=' + commentId + ']');

                    $relatedCommentContentArea.show();
                    $relatedCommentEditFormArea.hide();
                    $link.attr('href', '#');
                });
            });
        }

        function registerReplyLinks($container) {
            $container.find('.comment-reply-link').each(function () {
                let $link = $(this);
                $link.on('click', function (e) {
                    e.preventDefault();

                    let replyCommentId = $link.data('reply-id');

                    let $relatedCommentArea = $container.find('.cms-comment-form-area[data-reply-id=' + replyCommentId + ']');
                    let $links = $container.find('.comment-reply-link[data-reply-id=' + replyCommentId + ']');

                    $relatedCommentArea.show();
                    $relatedCommentArea.find('textarea').focus();
                    $links.addClass('disabled');
                });
            });
            $container.find('.reply-cancel-button').each(function () {
                let $button = $(this);
                $button.on('click', function (e) {
                    e.preventDefault();

                    let replyCommentId = $button.data('reply-id');

                    let $relatedCommentArea = $container.find('.cms-comment-form-area[data-reply-id=' + replyCommentId + ']');
                    let $links = $container.find('.comment-reply-link[data-reply-id=' + replyCommentId + ']');

                    $relatedCommentArea.hide();
                    $links.removeClass('disabled');
                });
            });
        }

        function registerDeleteLinks($container) {
            $container.find('.comment-delete-link').each(function () {
                let $link = $(this);

                let allowDelete = ss.auth.isGranted('CmsKitPublic.Comments.DeleteAll');
                let isCurrentUser = ss.currentUser.id == $link.data('author-id');
                if (!allowDelete && !isCurrentUser) {
                    $link.hide();
                }
                else {
                    $link.on('click', '', function (e) {
                        e.preventDefault();

                        ss.message.confirm(l("MessageDeletionConfirmationMessage"), function (ok) {
                            if (ok) {
                                smartsoftware.cmsKit.public.comments.commentPublic.delete($link.data('id')
                                ).then(function () {
                                    widgetManager.refresh($widget);
                                });
                            }
                        });
                    });
                }
            });
        }

        function registerUpdateOfNewComment($container) {
            $container.find('.cms-comment-update-form').each(function () {
                var $form = $(this);

                $form.submit(function (e) {
                    e.preventDefault();
                    
                    ss.ui.setBusy($form.find("button[type='submit']"));

                    let formAsObject = $form.serializeFormToObject();
                    
                    $.ajax({
                        type: 'POST',
                        url: '/CmsKitPublicComments/Update/' + formAsObject.id,
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: JSON.stringify({
                            text: formAsObject.commentText,
                            concurrencyStamp: formAsObject.commentConcurrencyStamp,
                            captchaToken: formAsObject.captchaId,
                            captchaAnswer: formAsObject.input?.captcha
                        }),
                        success: function () {
                            widgetManager.refresh($widget);
                            ss.ui.clearBusy();
                        },
                        error: function (data) {
                            ss.message.error(data.responseJSON.error.message);
                            ss.ui.clearBusy();
                        }
                    });
                });
            });
        }

        function registerSubmissionOfNewComment($container) {
            $container.find('.cms-comment-form').each(function () {
                var $form = $(this);

                $form.submit(function (e) {
                    e.preventDefault();

                    ss.ui.setBusy("button[type='submit']");

                    var formAsObject = $form.serializeFormToObject();

                    if (formAsObject.repliedCommentId == '') {
                        formAsObject.repliedCommentId = null;
                    }

                    if (formAsObject.commentText == '') {
                        ss.message.error(l("CommentTextRequired"));
                        ss.ui.clearBusy();
                        return;
                    }

                    $.ajax({
                        type: 'POST',
                        url: '/CmsKitPublicComments/Validate',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: JSON.stringify({
                            entityId: $commentArea.attr('data-entity-id'),
                            entityType: $commentArea.attr('data-entity-type'),
                            repliedCommentId: formAsObject.repliedCommentId,
                            text: formAsObject.commentText,
                            url: window.location.href,
                            captchaToken: formAsObject.captchaId,
                            captchaAnswer: formAsObject.input?.captcha,
                            idempotencyToken: formAsObject.idempotencyToken
                        }),
                        success: function () {
                            widgetManager.refresh($widget);
                            ss.ui.clearBusy();
                        },
                        error: function (data) {
                            ss.message.error(data.responseJSON.error.message);
                            ss.ui.clearBusy();
                        }
                    });
                });
            });
        }

        function focusOnHash($container) {
            if (!location.hash.toLowerCase().startsWith('#cms-comment')) {
                return;
            }

            let $link = $(location.hash + '_link');

            if ($link.length > 0) {
                $link.click();
            }
            else {
                $(location.hash).find('textarea').focus();
            }
        }
        
        function init() {
            registerReplyLinks($widget);
            registerEditLinks($widget);
            registerDeleteLinks($widget);

            registerUpdateOfNewComment($widget);
            registerSubmissionOfNewComment($widget);

            focusOnHash($widget);
        }

        return {
            init: init,
            getFilters: getFilters
        };
    };

})(jQuery);
