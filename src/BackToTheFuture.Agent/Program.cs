using System;
using BackToTheFuture.Core.SiteSources;
using BackToTheFuture.Core;
using System.Diagnostics;
using System.IO;
using BackToTheFuture.Hosting.StandAloneHttpServer;

namespace BackToTheFuture.Agent
{
    class Program
    {
        //netsh http add urlacl url=http://+:80/ user=Everyone listen=yes
        //netsh http delete urlacl url=http://+:80/
        static void Main(string[] args)
        {
            var source = new DirectorySiteSource(new DirectoryInfo(@"d:\site"));
            var site = new InMemorySiteTarget(source);

            SimpleStandAloneHttpServer myServer = new SimpleStandAloneHttpServer(@"D:\site", 80, site);
            
            Process.Start(string.Format("http://localhost:{0}", myServer.Port));
            Console.ReadKey();
            myServer.Stop();
        }
    }
}
