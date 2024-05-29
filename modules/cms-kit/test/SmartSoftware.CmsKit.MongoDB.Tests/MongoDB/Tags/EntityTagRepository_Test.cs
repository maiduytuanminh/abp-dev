using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSoftware.CmsKit.Tags;
using Xunit;

namespace SmartSoftware.CmsKit.MongoDB.Tags;

[Collection(MongoTestCollection.Name)]
public class EntityTagRepository_Test : EntityTagRepository_Test<CmsKitMongoDbTestModule>
{

}
