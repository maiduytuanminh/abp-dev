using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.MongoDB;

namespace MyCompanyName.MyProjectName.MongoDB;

[DependsOn(
    typeof(MyProjectNameDomainModule),
    typeof(SmartSoftwareMongoDbModule)
    )]
public class MyProjectNameMongoDbModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<MyProjectNameMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
