﻿using System.Threading.Tasks;
using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.MongoDB.Repositories;

[Collection(MongoTestCollection.Name)]
public class Repository_Basic_Tests_With_Int_Pk : Repository_Basic_Tests_With_Int_Pk<SmartSoftwareMongoDbTestModule>
{
    [Fact(Skip = "Int PKs are not working for MongoDb")]
    public override Task Get()
    {
        return Task.CompletedTask;
    }
}
