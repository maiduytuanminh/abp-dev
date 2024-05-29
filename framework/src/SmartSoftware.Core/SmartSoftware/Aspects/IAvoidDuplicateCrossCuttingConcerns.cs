using System.Collections.Generic;

namespace SmartSoftware.Aspects;

public interface IAvoidDuplicateCrossCuttingConcerns
{
    List<string> AppliedCrossCuttingConcerns { get; }
}
