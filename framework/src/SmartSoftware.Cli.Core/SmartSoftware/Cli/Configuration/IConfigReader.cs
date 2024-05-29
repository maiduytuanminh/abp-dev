using System.Threading.Tasks;

namespace SmartSoftware.Cli.Configuration;

public interface IConfigReader
{
    SmartSoftwareCliConfig Read(string directory);
}
