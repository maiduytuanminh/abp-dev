using SmartSoftware.Threading;

namespace MyCompanyName.MyProjectName;

public static class MyProjectNameGlobalFeatureConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            /* You can configure (enable/disable) global features of the used modules here.
             * Please refer to the documentation to learn more about the Global Features System:
             * https://docs.smartsoftware.io/en/ss/latest/Global-Features
             */
        });
    }
}
