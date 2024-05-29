using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SmartSoftware.Identity.MongoDB;

[Collection(MongoTestCollection.Name)]
public class OrganizationUnitRepository_Tests : OrganizationUnitRepository_Tests<SmartSoftwareIdentityMongoDbTestModule>
{
}
