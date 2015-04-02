using System;
using System.Linq;
using System.Collections.Generic;
using BackToTheFuture.Core.SiteSources;
using BackToTheFuture.Core.SiteTargets;
using BackToTheFuture.Core.Plugins;
using BackToTheFuture.Core.ResourceTypes;


namespace BackToTheFuture.Core
{
    public class InMemorySiteTarget : ISiteTarget
    {
        public InMemorySiteTarget()
        {
            Resources = new List<TargetResource>();
        }

        public InMemorySiteTarget(ISiteSource soruce)
        {
            Resources = new List<TargetResource>();
            SetupFrom(soruce);
        }

        ITargetConfiguration _targetConfiguration;

        private List<TargetResource> Resources { get; set; }

        public ITargetConfiguration TargetConfiguration
        {
            get { return _targetConfiguration; }
        }

        public TargetResource GetByName(string name)
        {
            var result =  Resources.Where(x=>x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return result;
        }

        public List<TargetResource> GetByPath(string name)
        {
            var result = Resources.Where(x => x.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            return result;
        }


        public void AddResource(SourceResource source)
        {
            MemoryTargetResource tf = new MemoryTargetResource(source.Name, source.GetStream());
            Resources.Add(tf);
        }

        public void SetupFrom(ISiteSource source)
        {
            foreach(SourceResource newSource in source.Scan())
            {

                AddResource(newSource);
            }
        }
    }
}
