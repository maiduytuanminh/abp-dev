using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSoftware.CmsKit.Blogs;
using Xunit;

namespace SmartSoftware.CmsKit.MongoDB.Blogs;

[Collection(MongoTestCollection.Name)]
public class BlogRepository_Test : BlogRepository_Test<CmsKitMongoDbTestModule>
{

}
