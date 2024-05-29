using JetBrains.Annotations;
using System.Collections.Generic;

namespace SmartSoftware.CmsKit.Ratings;

public class CmsKitRatingOptions
{
    [NotNull]
    public List<RatingEntityTypeDefinition> EntityTypes { get; } = new();
}
