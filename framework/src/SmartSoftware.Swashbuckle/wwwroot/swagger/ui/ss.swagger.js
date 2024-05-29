var ss = ss || {};

(function () {

    ss.SwaggerUIBundle = function (configObject) {

        var excludeUrl = ["swagger.json", "connect/token"]
        var firstRequest = true;
        var oidcSupportedFlows = configObject.oidcSupportedFlows || [];
        var oidcSupportedScopes = configObject.oidcSupportedScopes || [];
        var oidcDiscoveryEndpoint = configObject.oidcDiscoveryEndpoint || [];
        var tenantPlaceHolders = ["{{tenantId}}", "{{tenantName}}" , "{0}"]
        ss.appPath = configObject.baseUrl || ss.appPath;

        var requestInterceptor = configObject.requestInterceptor;
        var responseInterceptor = configObject.responseInterceptor;
        
        configObject.requestInterceptor = async function (request) {

            if (request.url.includes(excludeUrl[1])) {
                firstRequest = true;
            }

            if (firstRequest && !excludeUrl.some(url => request.url.includes(url))) {
                await fetch(`${ss.appPath}ss/Swashbuckle/SetCsrfCookie`, {
                    headers: request.headers
                });
                firstRequest = false;
            }
            
            // Intercept .well-known request when the discoveryEndpoint is provided
            if (!firstRequest && oidcDiscoveryEndpoint.length !== 0 && request.url.includes(".well-known/openid-configuration")) {
                
                if (oidcDiscoveryEndpoint.endsWith(".well-known/openid-configuration")) {
                    request.url = await replaceTenantPlaceHolder(oidcDiscoveryEndpoint);
                    console.log(request.url);
                    return;
                }
                if (!oidcDiscoveryEndpoint.endsWith("/")) {
                    oidcDiscoveryEndpoint += "/"
                }
                request.url = await replaceTenantPlaceHolder(oidcDiscoveryEndpoint) + ".well-known/openid-configuration";
                
                console.log(request.url);
            }

            var antiForgeryToken = ss.security.antiForgery.getToken();
            if (antiForgeryToken) {
                request.headers[ss.security.antiForgery.tokenHeaderName] = antiForgeryToken;
            }

            if (!request.headers["X-Requested-With"]) {
                request.headers["X-Requested-With"] = "XMLHttpRequest";
            }

            if (requestInterceptor) {
                requestInterceptor(request);
            }
            return request;
        };

        configObject.responseInterceptor = async function (response) {
            if (response.url.endsWith(".well-known/openid-configuration") && response.status === 200) {
                var openIdConnectData = JSON.parse(response.text);

                if (oidcDiscoveryEndpoint.length > 0) {
                    openIdConnectData.grant_types_supported = oidcSupportedFlows;
                }

                if (oidcSupportedFlows.length > 0) {
                    openIdConnectData.grant_types_supported = oidcSupportedFlows;
                }

                if (oidcSupportedScopes.length > 0) {
                    openIdConnectData.scopes_supported = oidcSupportedScopes;
                }

                response.text = JSON.stringify(openIdConnectData);
            }

            if (responseInterceptor) {
                responseInterceptor(response);
            }
            return response;
        };
        
        async function replaceTenantPlaceHolder(url) {
            
            if(!ss.currentTenant){
                await getSmartSoftwareApplicationConfiguration();
            }
            
            url.replace(tenantPlaceHolders[0], ss.currentTenant.id);
            url.replace(tenantPlaceHolders[1], ss.currentTenant.name);
            
            if(ss.currentTenant.name != null){
                url.replace(tenantPlaceHolders[2], ss.currentTenant.name);
            }else if (ss.currentTenant.id != null){
                url.replace(tenantPlaceHolders[2], ss.currentTenant.id);
            }
            
            return url;
        }
        
        function getSmartSoftwareApplicationConfiguration() {
            return fetch(`${ss.appPath}api/ss/application-configuration`).then(response => response.json()).then(data => {
                ss.currentTenant = data.currentTenant; 
            });
        }

        return SwaggerUIBundle(configObject);
    }
})();
