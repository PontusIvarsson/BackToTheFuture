using System.Collections.Generic;
using BackToTheFuture.Core.ResourceTypes;
using BackToTheFuture.Core.SiteSources;
using System;

namespace BackToTheFuture.Core.SiteTargets
{
    public interface ISiteTarget
    {
        void AddResource(SourceResource source);
        TargetResource GetByName(string name);
        List<TargetResource> GetByPath(string name);
        void SetupFrom(ISiteSource source);
        ITargetConfiguration TargetConfiguration { get; }

        void AddPlugin(ISiteTargetPlugin plugin);
    }

    public interface ITargetConfiguration
    {

    }
}
