using BackToTheFuture.Core;
using BackToTheFuture.Core.SiteSources;
using BackToTheFuture.Core.SiteTargets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheFuture.Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            var site = new InMemorySiteTarget();
            ISiteSource source = new DirectorySiteSource(new DirectoryInfo(@"d:\import"));
            site.ProcessSource(source);
            site.GetByRelativeName("test");
        }
    }
}
