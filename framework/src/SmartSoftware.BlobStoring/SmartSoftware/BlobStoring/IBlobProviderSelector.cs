using JetBrains.Annotations;

namespace SmartSoftware.BlobStoring;

public interface IBlobProviderSelector
{
    [NotNull]
    IBlobProvider Get([NotNull] string containerName);
}
