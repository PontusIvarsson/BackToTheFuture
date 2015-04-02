using BackToTheFuture.Core.ResourceTypes;
using System.IO;

namespace BackToTheFuture.Core.Plugins
{
    public interface IFileTransformPlugin
    {
        Stream Transform(SourceResource file);
    }
}
