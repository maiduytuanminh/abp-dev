using System.Collections.Generic;

namespace SmartSoftware.ClientSimulation;

public class ClientSimulationOptions
{
    public List<ScenarioConfiguration> Scenarios { get; }

    public ClientSimulationOptions()
    {
        Scenarios = new List<ScenarioConfiguration>();
    }
}
