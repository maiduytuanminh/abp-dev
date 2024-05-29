﻿using System.Collections.Generic;
using JetBrains.Annotations;
using SmartSoftware.Cli.ProjectBuilding.Building;

namespace SmartSoftware.Cli.ProjectBuilding;

public class ProjectBuildArgs
{
    [NotNull]
    public SolutionName SolutionName { get; }

    [CanBeNull]
    public string TemplateName { get; set; }

    [CanBeNull]
    public string Version { get; set; }

    public bool TrustUserVersion { get; set; }

    public DatabaseProvider DatabaseProvider { get; set; }

    public DatabaseManagementSystem DatabaseManagementSystem { get; set; }

    public UiFramework UiFramework { get; set; }

    public MobileApp? MobileApp { get; set; }

    public bool PublicWebSite { get; set; }

    [CanBeNull]
    public string SmartSoftwareGitHubLocalRepositoryPath { get; set; }

    [CanBeNull]
    public string TemplateSource { get; set; }

    [CanBeNull]
    public string ConnectionString { get; set; }

    [NotNull]
    public string OutputFolder { get; set; }

    public bool Pwa { get; set; }

    public Theme? Theme { get; set; }

    public ThemeStyle? ThemeStyle { get; set; }

    public bool SkipCache { get; set; }

    [NotNull]
    public Dictionary<string, string> ExtraProperties { get; set; }

    public ProjectBuildArgs(
        [NotNull] SolutionName solutionName,
        [CanBeNull] string templateName = null,
        [CanBeNull] string version = null,
        string outputFolder = null,
        DatabaseProvider databaseProvider = DatabaseProvider.NotSpecified,
        DatabaseManagementSystem databaseManagementSystem = DatabaseManagementSystem.NotSpecified,
        UiFramework uiFramework = UiFramework.NotSpecified,
        MobileApp? mobileApp = null,
        bool publicWebSite = false,
        [CanBeNull] string ssGitHubLocalRepositoryPath = null,
        [CanBeNull] string smartsoftwareGitHubLocalRepositoryPath = null,
        [CanBeNull] string templateSource = null,
        Dictionary<string, string> extraProperties = null,
        [CanBeNull] string connectionString = null,
        bool pwa = false,
        Theme? theme = null,
        ThemeStyle? themeStyle = null,
        bool skipCache = false,
        bool trustUserVersion = false)
    {
        SolutionName = Check.NotNull(solutionName, nameof(solutionName));
        TemplateName = templateName;
        Version = version;
        OutputFolder = outputFolder;
        DatabaseProvider = databaseProvider;
        DatabaseManagementSystem = databaseManagementSystem;
        UiFramework = uiFramework;
        MobileApp = mobileApp;
        PublicWebSite = publicWebSite;
        SmartSoftwareGitHubLocalRepositoryPath = ssGitHubLocalRepositoryPath;
        SmartSoftwareGitHubLocalRepositoryPath = smartsoftwareGitHubLocalRepositoryPath;
        TemplateSource = templateSource;
        ExtraProperties = extraProperties ?? new Dictionary<string, string>();
        ConnectionString = connectionString;
        Pwa = pwa;
        Theme = theme;
        ThemeStyle = themeStyle;
        SkipCache = skipCache;
        TrustUserVersion = trustUserVersion;
    }
}