using Sharpmake;

namespace SharpmakeGen.Platforms
{
    [Generate]
    public class KhaXboxProject : PlatformProject
    {
        public KhaXboxProject()
        {
            Name = "Sharpmake.KhaXbox";
        }

        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.AddPrivateDependency<CommonPlatformsProject>(target);
        }
    }
}
