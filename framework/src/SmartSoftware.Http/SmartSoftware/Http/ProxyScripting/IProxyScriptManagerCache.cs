using System;

namespace SmartSoftware.Http.ProxyScripting;

public interface IProxyScriptManagerCache
{
    string GetOrAdd(string key, Func<string> factory);

    void Set(string key, string value);
}
