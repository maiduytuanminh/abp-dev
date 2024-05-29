﻿using System;

namespace SmartSoftware.Threading;

public interface IAmbientScopeProvider<T>
{
    T? GetValue(string contextKey);

    IDisposable BeginScope(string contextKey, T value);
}
