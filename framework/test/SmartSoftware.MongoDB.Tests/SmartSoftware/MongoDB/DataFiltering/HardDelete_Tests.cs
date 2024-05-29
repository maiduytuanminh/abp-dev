using System;
using System.Collections.Generic;
using System.Text;
using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.DataFiltering;

[Collection(MongoTestCollection.Name)]
public class HardDelete_Tests : HardDelete_Tests<SmartSoftwareMongoDbTestModule>
{
}
