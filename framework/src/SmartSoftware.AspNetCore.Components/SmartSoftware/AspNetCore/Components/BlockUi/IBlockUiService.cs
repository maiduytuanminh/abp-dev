using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Components.BlockUi;

public interface IBlockUiService
{
    Task Block(string? selectors, bool busy = false);

    Task UnBlock();
}
