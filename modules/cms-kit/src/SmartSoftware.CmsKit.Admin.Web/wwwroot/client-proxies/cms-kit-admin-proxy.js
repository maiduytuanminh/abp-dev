/* This file is automatically generated by SS framework to use MVC Controllers from javascript. */


// module cms-kit-admin

(function(){

  // controller smartsoftware.cmsKit.admin.tags.entityTagAdmin

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.admin.tags.entityTagAdmin');

    smartsoftware.cmsKit.admin.tags.entityTagAdmin.addTagToEntity = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/entity-tags',
        type: 'POST',
        dataType: null,
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.tags.entityTagAdmin.removeTagFromEntity = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/entity-tags' + ss.utils.buildQueryString([{ name: 'tagId', value: input.tagId }, { name: 'entityType', value: input.entityType }, { name: 'entityId', value: input.entityId }]) + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.tags.entityTagAdmin.setEntityTags = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/entity-tags',
        type: 'PUT',
        dataType: null,
        data: JSON.stringify(input)
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.admin.tags.tagAdmin

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.admin.tags.tagAdmin');

    smartsoftware.cmsKit.admin.tags.tagAdmin.create = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/tags',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.tags.tagAdmin['delete'] = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/tags/' + id + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.tags.tagAdmin.get = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/tags/' + id + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.tags.tagAdmin.getList = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/tags' + ss.utils.buildQueryString([{ name: 'filter', value: input.filter }, { name: 'sorting', value: input.sorting }, { name: 'skipCount', value: input.skipCount }, { name: 'maxResultCount', value: input.maxResultCount }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.tags.tagAdmin.update = function(id, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/tags/' + id + '',
        type: 'PUT',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.tags.tagAdmin.getTagDefinitions = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/tags/tag-definitions',
        type: 'GET'
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.admin.pages.pageAdmin

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.admin.pages.pageAdmin');

    smartsoftware.cmsKit.admin.pages.pageAdmin.get = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/pages/' + id + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.pages.pageAdmin.getList = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/pages' + ss.utils.buildQueryString([{ name: 'filter', value: input.filter }, { name: 'sorting', value: input.sorting }, { name: 'skipCount', value: input.skipCount }, { name: 'maxResultCount', value: input.maxResultCount }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.pages.pageAdmin.create = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/pages',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.pages.pageAdmin.update = function(id, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/pages/' + id + '',
        type: 'PUT',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.pages.pageAdmin['delete'] = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/pages/' + id + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.pages.pageAdmin.setAsHomePage = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/pages/setashomepage/' + id + '',
        type: 'PUT',
        dataType: null
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.admin.menus.menuItemAdmin

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.admin.menus.menuItemAdmin');

    smartsoftware.cmsKit.admin.menus.menuItemAdmin.getList = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/menu-items',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.menus.menuItemAdmin.get = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/menu-items/' + id + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.menus.menuItemAdmin.create = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/menu-items',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.menus.menuItemAdmin.update = function(id, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/menu-items/' + id + '',
        type: 'PUT',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.menus.menuItemAdmin['delete'] = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/menu-items/' + id + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.menus.menuItemAdmin.moveMenuItem = function(id, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/menu-items/' + id + '/move',
        type: 'PUT',
        dataType: null,
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.menus.menuItemAdmin.getPageLookup = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/menu-items/lookup/pages' + ss.utils.buildQueryString([{ name: 'filter', value: input.filter }, { name: 'sorting', value: input.sorting }, { name: 'skipCount', value: input.skipCount }, { name: 'maxResultCount', value: input.maxResultCount }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.admin.mediaDescriptors.mediaDescriptorAdmin

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.admin.mediaDescriptors.mediaDescriptorAdmin');

    smartsoftware.cmsKit.admin.mediaDescriptors.mediaDescriptorAdmin.create = function(entityType, inputStream, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/media/' + entityType + '' + ss.utils.buildQueryString([{ name: 'name', value: inputStream.name }]) + '',
        type: 'POST'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.mediaDescriptors.mediaDescriptorAdmin['delete'] = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/media/' + id + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.admin.globalResources.globalResourceAdmin

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.admin.globalResources.globalResourceAdmin');

    smartsoftware.cmsKit.admin.globalResources.globalResourceAdmin.get = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/global-resources',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.globalResources.globalResourceAdmin.setGlobalResources = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/global-resources',
        type: 'POST',
        dataType: null,
        data: JSON.stringify(input)
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.admin.comments.commentAdmin

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.admin.comments.commentAdmin');

    smartsoftware.cmsKit.admin.comments.commentAdmin.getList = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/comments' + ss.utils.buildQueryString([{ name: 'entityType', value: input.entityType }, { name: 'text', value: input.text }, { name: 'repliedCommentId', value: input.repliedCommentId }, { name: 'author', value: input.author }, { name: 'creationStartDate', value: input.creationStartDate }, { name: 'creationEndDate', value: input.creationEndDate }, { name: 'sorting', value: input.sorting }, { name: 'skipCount', value: input.skipCount }, { name: 'maxResultCount', value: input.maxResultCount }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.comments.commentAdmin.get = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/comments/' + id + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.comments.commentAdmin['delete'] = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/comments/' + id + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.admin.blogs.blogAdmin

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.admin.blogs.blogAdmin');

    smartsoftware.cmsKit.admin.blogs.blogAdmin.get = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/' + id + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogAdmin.getList = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs' + ss.utils.buildQueryString([{ name: 'filter', value: input.filter }, { name: 'sorting', value: input.sorting }, { name: 'skipCount', value: input.skipCount }, { name: 'maxResultCount', value: input.maxResultCount }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogAdmin.create = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogAdmin.update = function(id, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/' + id + '',
        type: 'PUT',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogAdmin['delete'] = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/' + id + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.admin.blogs.blogFeatureAdmin

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.admin.blogs.blogFeatureAdmin');

    smartsoftware.cmsKit.admin.blogs.blogFeatureAdmin.getList = function(blogId, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/' + blogId + '/features',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogFeatureAdmin.set = function(blogId, dto, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/' + blogId + '/features',
        type: 'PUT',
        dataType: null,
        data: JSON.stringify(dto)
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.admin.blogs.blogPostAdmin

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.admin.blogs.blogPostAdmin');

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin.create = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin['delete'] = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts/' + id + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin.get = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts/' + id + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin.getList = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts' + ss.utils.buildQueryString([{ name: 'filter', value: input.filter }, { name: 'blogId', value: input.blogId }, { name: 'authorId', value: input.authorId }, { name: 'tagId', value: input.tagId }, { name: 'status', value: input.status }, { name: 'sorting', value: input.sorting }, { name: 'skipCount', value: input.skipCount }, { name: 'maxResultCount', value: input.maxResultCount }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin.update = function(id, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts/' + id + '',
        type: 'PUT',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin.publish = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts/' + id + '/publish',
        type: 'POST',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin.draft = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts/' + id + '/draft',
        type: 'POST',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin.createAndPublish = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts/create-and-publish',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin.sendToReview = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts/' + id + '/send-to-review',
        type: 'POST',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin.createAndSendToReview = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts/create-and-send-to-review',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.admin.blogs.blogPostAdmin.hasBlogPostWaitingForReview = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-admin/blogs/blog-posts/has-blogpost-waiting-for-review',
        type: 'GET'
      }, ajaxParams));
    };

  })();

})();

