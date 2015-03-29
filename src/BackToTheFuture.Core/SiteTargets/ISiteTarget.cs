using BackToTheFuture.Core.SiteSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheFuture.Core.SiteTargets
{
    public interface ISiteTarget
    {
        void AddFile(SourceFile source);
        TargetFile GetByRelativeName(string name);
        List<TargetFile> GetByRelativePath(string name);
        void ProcessSource(ISiteSource source);
    }
}
