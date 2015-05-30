using BackToTheFuture.Core.ResourceTypes;
using System.IO;

namespace BackToTheFuture.Core.SiteTargets
{
    public class MarkupReplacePlugin : ISiteTargetStaticPlugin
    {
        public bool AppliesTo(SourceResource file)
        {
            return file.Name.EndsWith(".md");
        }

        public Stream Execute(SourceResource file)
        {
            return file.GetStream();
        }
    }
}
