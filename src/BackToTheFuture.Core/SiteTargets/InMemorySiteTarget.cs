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
        public InMemorySiteTarget(string rootDir)
        {
            Files = new List<TargetResource>();
            _targetConfiguration = new TargetConfiguration(new Uri("file://" + rootDir));
        }

        ITargetConfiguration _targetConfiguration;

        private List<TargetResource> Files { get; set; }

        public ITargetConfiguration TargetConfiguration
        {
            get { return _targetConfiguration; }
        }

        public TargetResource GetByName(string name)
        {
            var result =  Files.Where(x=>x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return result;
        }

        public List<TargetResource> GetByPath(string name)
        {
            var result = Files.Where(x => x.Name.StartsWith(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            return result;
        }


        public void AddFile(SourceResource source)
        {
            Files.Add(Transform(source));
        }

        public void ProcessSource(ISiteSource source)
        {
            foreach(SourceResource newSource in source.Scan())
            {
                AddFile(newSource);
            }
        }

        private MemoryTargetResource Transform(SourceResource source)
        {
            MemoryTargetResource tf = new MemoryTargetResource(this, source.Name, source.GetStream());

            //todo run plugins etc.
            
            return tf;
        }






    }
}
