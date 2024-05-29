using System;
using SmartSoftware.ClientSimulation.Scenarios;
using SmartSoftware.ClientSimulation.Snapshot;

namespace SmartSoftware.ClientSimulation.Clients;

public interface IClient
{
    event EventHandler Stopped;

    ClientState State { get; }

    void Initialize(Scenario scenario);

    void Start();

    void Stop();

    ClientSnapshot CreateSnapshot();
}
