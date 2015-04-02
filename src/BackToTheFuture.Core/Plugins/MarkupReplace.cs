using BackToTheFuture.Core.ResourceTypes;
using System.IO;

namespace BackToTheFuture.Core.Plugins
{
    public class MarkupReplace : IFileTransformPlugin
    {
        public Stream Transform(SourceResource file)
        {
            return file.GetStream();
        }
    }
}
