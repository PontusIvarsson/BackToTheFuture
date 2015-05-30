using BackToTheFuture.Core.ResourceTypes;
using System.IO;

namespace BackToTheFuture.Core.SiteTargets
{
    public interface ISiteTargetPlugin
    {
        Stream Execute(SourceResource file);
        bool AppliesTo(SourceResource file);
    }

    public interface ISiteTargetStaticPlugin : ISiteTargetPlugin
    {
    }

    public interface ISiteTargetDynamicPlugin : ISiteTargetPlugin
    {
    }
}
