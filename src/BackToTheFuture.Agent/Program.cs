using BackToTheFuture.Core;
using BackToTheFuture.Core.SiteSources;
using BackToTheFuture.Core.SiteTargets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheFuture.Agent
{
    class Program
    {
        //netsh http add urlacl url=http://+:80/ user=Everyone listen=yes
        //netsh http delete urlacl url=http://+:80/
        static void Main(string[] args)
        {
            //create server with auto assigned port
            
            
            

            var site = new InMemorySiteTarget("d:/site");
            ISiteSource source = new DirectorySiteSource(new DirectoryInfo(@"d:\site"));
            site.ProcessSource(source);

            SimpleHTTPServer myServer = new SimpleHTTPServer(@"D:\site", 80, site);
            Process.Start(string.Format("http://localhost:{0}", myServer.Port));
            
            Console.ReadKey();
            myServer.Stop();
            Console.WriteLine("stopped");
            Console.ReadKey();
        }
    }
}
