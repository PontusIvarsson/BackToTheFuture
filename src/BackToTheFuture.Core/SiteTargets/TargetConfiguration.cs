
using System;
namespace BackToTheFuture.Core.SiteTargets
{
    public class TargetConfiguration : ITargetConfiguration
    {
        public TargetConfiguration(Uri baseuri)
        {
            BaseUri = baseuri;
        }

        //Todo this is temp, should be some kind of routtable
        public Uri BaseUri { get; private set; }
    }
}
