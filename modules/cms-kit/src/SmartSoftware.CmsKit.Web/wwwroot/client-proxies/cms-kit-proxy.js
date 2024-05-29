/* This file is automatically generated by SS framework to use MVC Controllers from javascript. */


// module cms-kit

(function(){

  // controller smartsoftware.cmsKit.public.tags.tagPublic

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.public.tags.tagPublic');

    smartsoftware.cmsKit.public.tags.tagPublic.getAllRelatedTags = function(entityType, entityId, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/tags/' + entityType + '/' + entityId + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.tags.tagPublic.getPopularTags = function(entityType, maxCount, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/tags/popular/' + entityType + '/' + maxCount + '',
        type: 'GET'
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.public.reactions.reactionPublic

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.public.reactions.reactionPublic');

    smartsoftware.cmsKit.public.reactions.reactionPublic.getForSelection = function(entityType, entityId, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/reactions/' + entityType + '/' + entityId + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.reactions.reactionPublic.create = function(entityType, entityId, reaction, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/reactions/' + entityType + '/' + entityId + '/' + reaction + '',
        type: 'PUT',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.reactions.reactionPublic['delete'] = function(entityType, entityId, reaction, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/reactions/' + entityType + '/' + entityId + '/' + reaction + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.public.ratings.ratingPublic

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.public.ratings.ratingPublic');

    smartsoftware.cmsKit.public.ratings.ratingPublic.create = function(entityType, entityId, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/ratings/' + entityType + '/' + entityId + '',
        type: 'PUT',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.ratings.ratingPublic['delete'] = function(entityType, entityId, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/ratings/' + entityType + '/' + entityId + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.ratings.ratingPublic.getGroupedStarCounts = function(entityType, entityId, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/ratings/' + entityType + '/' + entityId + '',
        type: 'GET'
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.public.pages.pagesPublic

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.public.pages.pagesPublic');

    smartsoftware.cmsKit.public.pages.pagesPublic.findBySlug = function(slug, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/pages/' + slug + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.pages.pagesPublic.findDefaultHomePage = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/pages',
        type: 'GET'
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.public.menus.menuItemPublic

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.public.menus.menuItemPublic');

    smartsoftware.cmsKit.public.menus.menuItemPublic.getList = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/menu-items',
        type: 'GET'
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.public.globalResources.globalResourcePublic

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.public.globalResources.globalResourcePublic');

    smartsoftware.cmsKit.public.globalResources.globalResourcePublic.getGlobalScript = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/global-resources/script',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.globalResources.globalResourcePublic.getGlobalStyle = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/global-resources/style',
        type: 'GET'
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.public.comments.commentPublic

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.public.comments.commentPublic');

    smartsoftware.cmsKit.public.comments.commentPublic.getList = function(entityType, entityId, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/comments/' + entityType + '/' + entityId + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.comments.commentPublic.create = function(entityType, entityId, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/comments/' + entityType + '/' + entityId + '',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.comments.commentPublic.update = function(id, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/comments/' + id + '',
        type: 'PUT',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.comments.commentPublic['delete'] = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/comments/' + id + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.cmsKit.public.blogs.blogPostPublic

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.cmsKit.public.blogs.blogPostPublic');

    smartsoftware.cmsKit.public.blogs.blogPostPublic.get = function(blogSlug, blogPostSlug, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/blog-posts/' + blogSlug + '/' + blogPostSlug + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.blogs.blogPostPublic.getList = function(blogSlug, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/blog-posts/' + blogSlug + '' + ss.utils.buildQueryString([{ name: 'authorId', value: input.authorId }, { name: 'tagId', value: input.tagId }, { name: 'sorting', value: input.sorting }, { name: 'skipCount', value: input.skipCount }, { name: 'maxResultCount', value: input.maxResultCount }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.blogs.blogPostPublic.getAuthorsHasBlogPosts = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/blog-posts/authors' + ss.utils.buildQueryString([{ name: 'filter', value: input.filter }, { name: 'sorting', value: input.sorting }, { name: 'skipCount', value: input.skipCount }, { name: 'maxResultCount', value: input.maxResultCount }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.blogs.blogPostPublic.getAuthorHasBlogPost = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/blog-posts/authors/' + id + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.cmsKit.public.blogs.blogPostPublic['delete'] = function(id, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/cms-kit-public/blog-posts/' + id + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

  })();

})();


