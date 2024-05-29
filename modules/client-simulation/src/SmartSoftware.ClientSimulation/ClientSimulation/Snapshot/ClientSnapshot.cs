using System;
using SmartSoftware.ClientSimulation.Clients;

namespace SmartSoftware.ClientSimulation.Snapshot;

[Serializable]
public class ClientSnapshot
{
    public ClientState State { get; set; }

    public ScenarioSnapshot Scenario { get; set; }
}
