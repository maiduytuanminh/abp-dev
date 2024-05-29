using System;
using System.Linq;
using SmartSoftware.Cli.ProjectBuilding.Files;
using SmartSoftware.Cli.ProjectBuilding.Templates.App;

namespace SmartSoftware.Cli.ProjectBuilding.Building.Steps;

public class DatabaseManagementSystemChangeStep : ProjectBuildPipelineStep
{
    private readonly bool _hasDbMigrations;

    public DatabaseManagementSystemChangeStep(bool hasDbMigrations)
    {
        _hasDbMigrations = hasDbMigrations;
    }

    public override void Execute(ProjectBuildContext context)
    {
        switch (context.BuildArgs.DatabaseManagementSystem)
        {
            case DatabaseManagementSystem.MySQL:
                ChangeEntityFrameworkCoreDependency(context, "SmartSoftware.EntityFrameworkCore.MySQL",
                    "SmartSoftware.EntityFrameworkCore.MySQL",
                    "SmartSoftwareEntityFrameworkCoreMySQLModule");
                AddMySqlServerVersion(context);
                ChangeUseSqlServer(context, "UseMySQL", "UseMySql");
                break;

            case DatabaseManagementSystem.PostgreSQL:
                ChangeEntityFrameworkCoreDependency(context, "SmartSoftware.EntityFrameworkCore.PostgreSql",
                    "SmartSoftware.EntityFrameworkCore.PostgreSql",
                    "SmartSoftwareEntityFrameworkCorePostgreSqlModule");
                ChangeUseSqlServer(context, "UseNpgsql");
                break;

            case DatabaseManagementSystem.Oracle:
                ChangeEntityFrameworkCoreDependency(context, "SmartSoftware.EntityFrameworkCore.Oracle",
                    "SmartSoftware.EntityFrameworkCore.Oracle",
                    "SmartSoftwareEntityFrameworkCoreOracleModule");
                AdjustOracleDbContextOptionsBuilder(context);
                ChangeUseSqlServer(context, "UseOracle");
                break;

            case DatabaseManagementSystem.OracleDevart:
                ChangeEntityFrameworkCoreDependency(context, "SmartSoftware.EntityFrameworkCore.Oracle.Devart",
                    "SmartSoftware.EntityFrameworkCore.Oracle.Devart",
                    "SmartSoftwareEntityFrameworkCoreOracleDevartModule");
                AdjustOracleDbContextOptionsBuilder(context);
                ChangeUseSqlServer(context, "UseOracle");
                break;

            case DatabaseManagementSystem.SQLite:
                ChangeEntityFrameworkCoreDependency(context, "SmartSoftware.EntityFrameworkCore.Sqlite",
                    "SmartSoftware.EntityFrameworkCore.Sqlite",
                    "SmartSoftwareEntityFrameworkCoreSqliteModule");
                ChangeUseSqlServer(context, "UseSqlite");
                break;

            default:
                return;
        }
    }

    private void AdjustOracleDbContextOptionsBuilder(ProjectBuildContext context)
    {
        var dbContextFactoryFile = context.Files.FirstOrDefault(f => f.Name.EndsWith($"{(_hasDbMigrations ? "Migrations" : string.Empty)}DbContextFactoryBase.cs", StringComparison.OrdinalIgnoreCase))
                                   ?? context.Files.FirstOrDefault(f => f.Name.EndsWith($"{(_hasDbMigrations ? "Migrations" : string.Empty)}DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));

        dbContextFactoryFile?.ReplaceText("new DbContextOptionsBuilder",
            $"(DbContextOptionsBuilder<{context.BuildArgs.SolutionName.ProjectName}{(_hasDbMigrations ? "Migrations" : string.Empty)}DbContext>) new DbContextOptionsBuilder");
    }

    private void AddMySqlServerVersion(ProjectBuildContext context)
    {
        var dbContextFactoryFile = context.Files.FirstOrDefault(f => f.Name.EndsWith($"{(_hasDbMigrations ? "Migrations" : string.Empty)}DbContextFactoryBase.cs", StringComparison.OrdinalIgnoreCase))
                                   ?? context.Files.FirstOrDefault(f => f.Name.EndsWith($"{(_hasDbMigrations ? "Migrations" : string.Empty)}DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));

        dbContextFactoryFile?.ReplaceText("configuration.GetConnectionString(\"Default\")",
            "configuration.GetConnectionString(\"Default\"), MySqlServerVersion.LatestSupportedServerVersion");
    }

    private void ChangeEntityFrameworkCoreDependency(ProjectBuildContext context, string newPackageName, string newModuleNamespace, string newModuleClass)
    {
        var efCoreProjectFile = context.Files.FirstOrDefault(f => f.Name.EndsWith("EntityFrameworkCore.csproj", StringComparison.OrdinalIgnoreCase));
        efCoreProjectFile?.ReplaceText("SmartSoftware.EntityFrameworkCore.SqlServer", newPackageName);

        var efCoreModuleClass = context.Files.FirstOrDefault(f => f.Name.EndsWith("EntityFrameworkCoreModule.cs", StringComparison.OrdinalIgnoreCase));
        efCoreModuleClass?.ReplaceText("SmartSoftware.EntityFrameworkCore.SqlServer", newModuleNamespace);
        efCoreModuleClass?.ReplaceText("SmartSoftwareEntityFrameworkCoreSqlServerModule", newModuleClass);
    }

    private void ChangeUseSqlServer(ProjectBuildContext context, string newUseMethodForEfModule, string newUseMethodForDbContext = null)
    {
        if (newUseMethodForDbContext == null)
        {
            newUseMethodForDbContext = newUseMethodForEfModule;
        }

        var oldUseMethod = "UseSqlServer";

        var efCoreModuleClass = context.Files.FirstOrDefault(f => f.Name.EndsWith("EntityFrameworkCoreModule.cs", StringComparison.OrdinalIgnoreCase));
        
        if(efCoreModuleClass == null)
        {
            return;
        }
        
        efCoreModuleClass.ReplaceText(oldUseMethod, newUseMethodForEfModule);

        var dbContextFactoryFile = context.Files.FirstOrDefault(f => f.Name.EndsWith($"{(_hasDbMigrations ? "Migrations" : string.Empty)}DbContextFactoryBase.cs", StringComparison.OrdinalIgnoreCase))
                                   ?? context.Files.FirstOrDefault(f => f.Name.EndsWith($"{(_hasDbMigrations ? "Migrations" : string.Empty)}DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));
        dbContextFactoryFile?.ReplaceText(oldUseMethod, newUseMethodForDbContext);
    }
}
