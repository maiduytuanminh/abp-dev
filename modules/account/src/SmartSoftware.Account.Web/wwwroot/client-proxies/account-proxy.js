/* This file is automatically generated by SS framework to use MVC Controllers from javascript. */


// module account

(function(){

  // controller smartsoftware.account.account

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.account.account');

    smartsoftware.account.account.register = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/register',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.account.account.sendPasswordResetCode = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/send-password-reset-code',
        type: 'POST',
        dataType: null,
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.account.account.verifyPasswordResetToken = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/verify-password-reset-token',
        type: 'POST',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.account.account.resetPassword = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/reset-password',
        type: 'POST',
        dataType: null,
        data: JSON.stringify(input)
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.account.web.areas.account.controllers.account

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.account.web.areas.account.controllers.account');

    smartsoftware.account.web.areas.account.controllers.account.login = function(login, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/login',
        type: 'POST',
        data: JSON.stringify(login)
      }, ajaxParams));
    };

    smartsoftware.account.web.areas.account.controllers.account.logout = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/logout',
        type: 'GET',
        dataType: null
      }, ajaxParams));
    };

    smartsoftware.account.web.areas.account.controllers.account.checkPassword = function(login, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/check-password',
        type: 'POST',
        data: JSON.stringify(login)
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.account.dynamicClaims

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.account.dynamicClaims');

    smartsoftware.account.dynamicClaims.get = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/dynamic-claims',
        type: 'GET'
      }, ajaxParams));
    };

  })();

  // controller smartsoftware.account.profile

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.account.profile');

    smartsoftware.account.profile.get = function(ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/my-profile',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.account.profile.update = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/my-profile',
        type: 'PUT',
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.account.profile.changePassword = function(input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/account/my-profile/change-password',
        type: 'POST',
        dataType: null,
        data: JSON.stringify(input)
      }, ajaxParams));
    };

  })();

})();


