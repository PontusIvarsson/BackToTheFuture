using System;
using System.Collections.Generic;
using BackToTheFuture.Core.ResourceTypes;


namespace BackToTheFuture.Core.SiteSources
{
    public interface ISiteSource
    {
        List<SourceResource> Scan();
    }
}
