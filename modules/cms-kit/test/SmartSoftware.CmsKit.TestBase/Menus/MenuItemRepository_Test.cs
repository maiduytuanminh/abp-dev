using System;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Modularity;

namespace SmartSoftware.CmsKit.Menus;

public abstract class MenuItemRepository_Test<TStartupModule> : CmsKitTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{

    private readonly CmsKitTestData testData;
    private readonly IMenuItemRepository menuItemRepository;

    public MenuItemRepository_Test()
    {
        testData = GetRequiredService<CmsKitTestData>();
        menuItemRepository = GetRequiredService<IMenuItemRepository>();
    }
}
