using System.Collections.Generic;


namespace BackToTheFuture.Core.SiteSources
{
    public interface ISiteSource
    {
        List<SourceFile> Scan();
    }
}
