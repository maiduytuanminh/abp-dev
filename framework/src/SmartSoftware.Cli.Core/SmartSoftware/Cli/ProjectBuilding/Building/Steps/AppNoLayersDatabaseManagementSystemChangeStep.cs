using System;
using System.Linq;
using SmartSoftware.Cli.ProjectBuilding.Files;

namespace SmartSoftware.Cli.ProjectBuilding.Building.Steps;

public class AppNoLayersDatabaseManagementSystemChangeStep : ProjectBuildPipelineStep
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
                ChangeUseSqlServer(context, "UseOracle");
                break;

            case DatabaseManagementSystem.OracleDevart:
                ChangeEntityFrameworkCoreDependency(context, "SmartSoftware.EntityFrameworkCore.Oracle.Devart",
                    "SmartSoftware.EntityFrameworkCore.Oracle.Devart",
                    "SmartSoftwareEntityFrameworkCoreOracleDevartModule");
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

    private void AddMySqlServerVersion(ProjectBuildContext context)
    {
        var dbContextFactoryFile = context.Files.FirstOrDefault(f => f.Name.EndsWith("DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));

        dbContextFactoryFile?.ReplaceText("configuration.GetConnectionString(\"Default\")",
            "configuration.GetConnectionString(\"Default\"), MySqlServerVersion.LatestSupportedServerVersion");
    }

    private void ChangeEntityFrameworkCoreDependency(ProjectBuildContext context, string newPackageName, string newModuleNamespace, string newModuleClass)
    {
        var projectFile = context.Files.FirstOrDefault(f => f.Name.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase));
        projectFile?.ReplaceText("SmartSoftware.EntityFrameworkCore.SqlServer", newPackageName);

        var moduleClass = context.Files.FirstOrDefault(f => f.Name.EndsWith("Module.cs", StringComparison.OrdinalIgnoreCase));
        moduleClass?.ReplaceText("SmartSoftware.EntityFrameworkCore.SqlServer", newModuleNamespace);
        moduleClass?.ReplaceText("SmartSoftwareEntityFrameworkCoreSqlServerModule", newModuleClass);
    }

    private void ChangeUseSqlServer(ProjectBuildContext context, string newUseMethodForEfModule, string newUseMethodForDbContext = null)
    {
        if (newUseMethodForDbContext == null)
        {
            newUseMethodForDbContext = newUseMethodForEfModule;
        }

        var oldUseMethod = "UseSqlServer";

        var moduleClass = context.Files.FirstOrDefault(f => f.Name.EndsWith("Module.cs", StringComparison.OrdinalIgnoreCase));
        moduleClass?.ReplaceText(oldUseMethod, newUseMethodForEfModule);

        var dbContextFactoryFile = context.Files.FirstOrDefault(f => f.Name.EndsWith("DbContextFactory.cs", StringComparison.OrdinalIgnoreCase));
        dbContextFactoryFile?.ReplaceText(oldUseMethod, newUseMethodForDbContext);
    }
}
