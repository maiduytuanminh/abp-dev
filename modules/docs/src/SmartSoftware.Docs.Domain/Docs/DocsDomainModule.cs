using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware;
using SmartSoftware.AutoMapper;
using SmartSoftware.Domain;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.Threading;
using SmartSoftware.VirtualFileSystem;
using SmartSoftware.Docs.Documents;
using SmartSoftware.Docs.Documents.FullSearch.Elastic;
using SmartSoftware.Docs.FileSystem.Documents;
using SmartSoftware.Docs.GitHub;
using SmartSoftware.Docs.GitHub.Documents;
using SmartSoftware.Docs.Localization;
using SmartSoftware.Docs.Projects;

namespace SmartSoftware.Docs
{
    [DependsOn(
        typeof(DocsDomainSharedModule),
        typeof(SmartSoftwareDddDomainModule),
        typeof(SmartSoftwareAutoMapperModule)
        )]
    public class DocsDomainModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DocsDomainModule>();

            Configure<SmartSoftwareAutoMapperOptions>(options =>
            {
                options.AddProfile<DocsDomainMappingProfile>(validate: true);
            });

            Configure<SmartSoftwareDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Document, DocumentEto>(typeof(DocsDomainModule));
                options.EtoMappings.Add<Project, ProjectEto>(typeof(DocsDomainModule));
            });

            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets
                    .AddEmbedded<DocsDomainModule>();
            });

            Configure<SmartSoftwareLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DocsResource>()
                    .AddVirtualJson("/SmartSoftware/Docs/Localization/Domain");
            });

            Configure<DocumentSourceOptions>(options =>
            {
                options.Sources[GithubDocumentSource.Type] = typeof(GithubDocumentSource);
                options.Sources[FileSystemDocumentSource.Type] = typeof(FileSystemDocumentSource);
            });
            
            Configure<DocsGithubLanguageOptions>(options =>
            {
                options.DefaultLanguage = new LanguageConfigElement 
                {
                    Code = "en", 
                    DisplayName = "English", 
                    IsDefault = true
                };
            });

            context.Services.AddHttpClient(GithubRepositoryManager.HttpClientName, client =>
            {
                client.Timeout = TimeSpan.FromMilliseconds(15000);
            });
        }

        public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                if (scope.ServiceProvider.GetRequiredService<IOptions<DocsElasticSearchOptions>>().Value.Enable)
                {
                    var documentFullSearch = scope.ServiceProvider.GetRequiredService<IDocumentFullSearch>();
                    await documentFullSearch.CreateIndexIfNeededAsync();
                }
            }
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
        }
    }
}
