using JetBrains.Annotations;

namespace SmartSoftware.DependencyInjection;

public interface IObjectAccessor<out T>
{
    T? Value { get; }
}
