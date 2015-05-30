using System;
using System.Linq;
using System.Collections.Generic;
using BackToTheFuture.Core.SiteSources;
using BackToTheFuture.Core.SiteTargets;
using BackToTheFuture.Core.ResourceTypes;


namespace BackToTheFuture.Core
{
    public class InMemorySiteTarget : ISiteTarget
    {
        public InMemorySiteTarget()
        {
            Init();
        }

        public InMemorySiteTarget(ISiteSource soruce)
        {
            Init();
            SetupFrom(soruce);
        }

        private void Init()
        {
            Resources = new List<TargetResource>();
            Plugins = new List<ISiteTargetPlugin>();

        }

        ITargetConfiguration _targetConfiguration;
        List<ISiteTargetPlugin> Plugins;

        private List<TargetResource> Resources { get; set; }

        public ITargetConfiguration TargetConfiguration
        {
            get { return _targetConfiguration; }
        }

        public TargetResource GetByName(string name)
        {
            var result = Resources.Where(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return result;
        }

        public List<TargetResource> GetByPath(string name)
        {
            var result = Resources.Where(x => x.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            return result;
        }


        public void AddResource(SourceResource source)
        {
            var newStream = source.GetStream();
            foreach (var plugin in Plugins)
            {
                if (plugin is ISiteTargetStaticPlugin && plugin.AppliesTo(source))
                {
                    newStream = plugin.Execute(source);
                }

            }

            MemoryTargetResource tf = new MemoryTargetResource(source.Name, newStream);

            Resources.Add(tf);
        }

        public void SetupFrom(ISiteSource source)
        {
            foreach (SourceResource newSource in source.Scan())
            {
                AddResource(newSource);
            }
        }


        public void AddPlugin(ISiteTargetPlugin siteTargetPlugin)
        {
            Plugins.Add(siteTargetPlugin);
        }
    }
}
