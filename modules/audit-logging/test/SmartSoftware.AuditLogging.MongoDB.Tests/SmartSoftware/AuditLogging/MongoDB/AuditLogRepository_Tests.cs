using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SmartSoftware.AuditLogging.MongoDB;

[Collection(MongoTestCollection.Name)]
public class AuditLogRepository_Tests : AuditLogRepository_Tests<SmartSoftwareAuditLoggingMongoDbTestModule>
{

}
