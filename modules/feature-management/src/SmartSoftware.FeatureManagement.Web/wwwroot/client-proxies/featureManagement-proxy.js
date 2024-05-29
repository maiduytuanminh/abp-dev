/* This file is automatically generated by SS framework to use MVC Controllers from javascript. */


// module featureManagement

(function(){

  // controller smartsoftware.featureManagement.features

  (function(){

    ss.utils.createNamespace(window, 'smartsoftware.featureManagement.features');

    smartsoftware.featureManagement.features.get = function(providerName, providerKey, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/feature-management/features' + ss.utils.buildQueryString([{ name: 'providerName', value: providerName }, { name: 'providerKey', value: providerKey }]) + '',
        type: 'GET'
      }, ajaxParams));
    };

    smartsoftware.featureManagement.features.update = function(providerName, providerKey, input, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/feature-management/features' + ss.utils.buildQueryString([{ name: 'providerName', value: providerName }, { name: 'providerKey', value: providerKey }]) + '',
        type: 'PUT',
        dataType: null,
        data: JSON.stringify(input)
      }, ajaxParams));
    };

    smartsoftware.featureManagement.features['delete'] = function(providerName, providerKey, ajaxParams) {
      return ss.ajax($.extend(true, {
        url: ss.appPath + 'api/feature-management/features' + ss.utils.buildQueryString([{ name: 'providerName', value: providerName }, { name: 'providerKey', value: providerKey }]) + '',
        type: 'DELETE',
        dataType: null
      }, ajaxParams));
    };

  })();

})();


