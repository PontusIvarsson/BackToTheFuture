using System.Collections.Generic;
using BackToTheFuture.Core.ResourceTypes;
using BackToTheFuture.Core.SiteSources;
using System;

namespace BackToTheFuture.Core.SiteTargets
{
    public interface ISiteTarget
    {
        void AddFile(SourceResource source);
        TargetResource GetByName(string name);
        List<TargetResource> GetByPath(string name);
        void ProcessSource(ISiteSource source);
        ITargetConfiguration TargetConfiguration { get; }
    }

    public interface ITargetConfiguration
    {
        Uri BaseUri { get; }
    }
}
