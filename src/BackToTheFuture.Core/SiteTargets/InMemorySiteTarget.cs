using System;
using System.Linq;
using System.Collections.Generic;
using BackToTheFuture.Core.SiteSources;
using BackToTheFuture.Core.SiteTargets;
using BackToTheFuture.Core.Plugins;


namespace BackToTheFuture.Core
{
    public class InMemorySiteTarget : ISiteTarget
    {
        public InMemorySiteTarget()
        {
            Files = new List<TargetFile>();
        }

        private List<TargetFile> Files { get; set; }

        public TargetFile GetByRelativeName(string name)
        {
            
            return Files.Where(x=>x.Fullname.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        public List<TargetFile> GetByRelativePath(string name)
        {
            return Files.Where(x => x.RelativeFolderPath.Equals(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public void AddFile(SourceFile source)
        {
            Files.Add(Transform(source));
        }

        public void ProcessSource(ISiteSource source)
        {
            foreach(SourceFile newSource in source.Scan())
            {
                AddFile(newSource);
            }
        }

        private TargetFile Transform(SourceFile source)
        {
            TargetFile tf = new TargetFile(source.Name, source.RelativeFolderPath);

            //todo run plugins etc.
            
            return tf;
        }


    }
}
