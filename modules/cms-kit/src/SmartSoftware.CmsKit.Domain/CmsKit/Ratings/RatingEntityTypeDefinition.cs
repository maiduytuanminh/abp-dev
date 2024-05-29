using JetBrains.Annotations;

namespace SmartSoftware.CmsKit.Ratings;

public class RatingEntityTypeDefinition : EntityTypeDefinition
{
    public RatingEntityTypeDefinition(
        [NotNull] string entityType) : base(entityType)
    {
    }
}
