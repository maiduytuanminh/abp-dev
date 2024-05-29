using SmartSoftware.GlobalFeatures;
using SmartSoftware.Threading;
using SmartSoftware.CmsKit.GlobalFeatures;

namespace SmartSoftware.CmsKit
{
    public static class FeatureConfigurer
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                GlobalFeatureManager.Instance.Modules.CmsKit().EnableAll();
            });
        }
    }
}
