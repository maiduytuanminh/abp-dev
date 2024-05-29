using System.Collections.Generic;

namespace SmartSoftware.TestBase.Logging;

public interface ICanLogOnObject
{
    List<string> Logs { get; }
}
