using Xunit;

namespace SmartSoftware.AuditLogging.MongoDB;

[Collection(MongoTestCollection.Name)]
public class AuditStore_Basic_Tests : AuditStore_Basic_Tests<SmartSoftwareAuditLoggingMongoDbTestModule>
{

}
