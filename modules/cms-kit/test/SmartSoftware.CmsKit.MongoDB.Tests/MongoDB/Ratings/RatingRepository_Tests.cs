using SmartSoftware.CmsKit.Ratings;
using Xunit;

namespace SmartSoftware.CmsKit.MongoDB.Ratings;

[Collection(MongoTestCollection.Name)]
public class RatingRepository_Tests : RatingRepository_Tests<CmsKitMongoDbTestModule>
{

}
