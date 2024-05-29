using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SmartSoftware.Data;

public class SmartSoftwareDatabaseInfoDictionary : Dictionary<string, SmartSoftwareDatabaseInfo>
{
    private Dictionary<string, SmartSoftwareDatabaseInfo> ConnectionIndex { get; set; }

    public SmartSoftwareDatabaseInfoDictionary()
    {
        ConnectionIndex = new Dictionary<string, SmartSoftwareDatabaseInfo>();
    }

    public SmartSoftwareDatabaseInfo? GetMappedDatabaseOrNull(string connectionStringName)
    {
        return ConnectionIndex.GetOrDefault(connectionStringName);
    }

    public SmartSoftwareDatabaseInfoDictionary Configure(string databaseName, Action<SmartSoftwareDatabaseInfo> configureAction)
    {
        var databaseInfo = this.GetOrAdd(
            databaseName,
            () => new SmartSoftwareDatabaseInfo(databaseName)
        );

        configureAction(databaseInfo);

        return this;
    }

    /// <summary>
    /// This method should be called if this dictionary changes.
    /// It refreshes indexes for quick access to the connection informations.
    /// </summary>
    public void RefreshIndexes()
    {
        ConnectionIndex = new Dictionary<string, SmartSoftwareDatabaseInfo>();

        foreach (var databaseInfo in Values)
        {
            foreach (var mappedConnection in databaseInfo.MappedConnections)
            {
                if (ConnectionIndex.ContainsKey(mappedConnection))
                {
                    throw new SmartSoftwareException(
                        $"A connection name can not map to multiple databases: {mappedConnection}."
                    );
                }

                ConnectionIndex[mappedConnection] = databaseInfo;
            }
        }
    }
}
