using System;
using System.Linq;
using SmartSoftware.Cli.ProjectBuilding.Files;
using SmartSoftware.Cli.ProjectBuilding.Templates.App;

namespace SmartSoftware.Cli.ProjectBuilding.Building.Steps;

public class AppModuleDatabaseManagementSystemChangeStep : ProjectBuildPipelineStep
{
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
        var dbContextFactoryFiles = context.Files.Where(f => f.Name.EndsWith("DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var dbContextFactoryFile in dbContextFactoryFiles)
        {
            dbContextFactoryFile?.ReplaceText("new DbContextOptionsBuilder",
                $"(DbContextOptionsBuilder<{context.BuildArgs.SolutionName.ProjectName}{(false ? "Migrations" : string.Empty)}DbContext>) new DbContextOptionsBuilder");
        }
    }

    private void AddMySqlServerVersion(ProjectBuildContext context)
    {
        var dbContextFactoryFiles = context.Files.Where(f => f.Name.EndsWith("DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var dbContextFactoryFile in dbContextFactoryFiles)
        {
            dbContextFactoryFile?.ReplaceText("configuration.GetConnectionString(\"Default\")", "configuration.GetConnectionString(\"Default\"), MySqlServerVersion.LatestSupportedServerVersion");
        }
    }

    private void ChangeEntityFrameworkCoreDependency(ProjectBuildContext context, string newPackageName, string newModuleNamespace, string newModuleClass)
    {
        var efCoreProjectFiles = context.Files.Where(f => f.Name.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase));
        foreach (var efCoreProjectFile in efCoreProjectFiles)
        {
            efCoreProjectFile?.ReplaceText("SmartSoftware.EntityFrameworkCore.SqlServer", newPackageName);
        }

        var efCoreModuleClasses = context.Files.Where(f => f.Name.EndsWith("Module.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var efCoreModuleClass in efCoreModuleClasses)
        {
            efCoreModuleClass?.ReplaceText("SmartSoftware.EntityFrameworkCore.SqlServer", newModuleNamespace);
            efCoreModuleClass?.ReplaceText("SmartSoftwareEntityFrameworkCoreSqlServerModule", newModuleClass);
        }
    }

    private void ChangeUseSqlServer(ProjectBuildContext context, string newUseMethodForEfModule, string newUseMethodForDbContext = null)
    {
        if (newUseMethodForDbContext == null)
        {
            newUseMethodForDbContext = newUseMethodForEfModule;
        }

        const string oldUseMethod = "UseSqlServer";

        var efCoreModuleClasses = context.Files.Where(f => f.Name.EndsWith("Module.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var efCoreModuleClass in efCoreModuleClasses)
        {
            efCoreModuleClass.ReplaceText(oldUseMethod, newUseMethodForEfModule);
        }

        var dbContextFactoryFiles = context.Files.Where(f => f.Name.EndsWith("DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));
        foreach (var dbContextFactoryFile in dbContextFactoryFiles)
        {
            dbContextFactoryFile?.ReplaceText(oldUseMethod, newUseMethodForDbContext);
        }
    }
}
