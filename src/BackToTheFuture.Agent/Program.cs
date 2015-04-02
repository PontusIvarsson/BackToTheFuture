using System;
using BackToTheFuture.Core.SiteSources;
using BackToTheFuture.Core;
using System.Diagnostics;
using System.IO;
using BackToTheFuture.Hosting.StandAloneHttpServer;
using BackToTheFuture.Core.SiteTargets;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using BackToTheFuture.Hosting;

namespace BackToTheFuture.Agent
{
    //netsh http add urlacl url=http://+:80/ user=Everyone listen=yes
    //netsh http delete urlacl url=http://+:80/
    class Program
    {
        static SimpleStandAloneHttpServer myServer;
        static int portNumber = 801;

        static void Main(string[] args)
        {
            var source = new DirectorySiteSource(new DirectoryInfo(@"d:\site"));
            var site = new InMemorySiteTarget(source);

            //StartAsOwin(site);
            StartAsSimpleStandAloneHttpServer(site);


            Process.Start(string.Format("http://localhost:{0}", portNumber));
            Console.ReadKey();
        }

        public static void StartAsOwin(ISiteTarget target)
        {
            string uri = "http://localhost:" + portNumber;
            using (WebApp.Start<Startup>(uri))
            {
                Console.ReadKey();
            }
        }

        public static void StartAsSimpleStandAloneHttpServer(ISiteTarget target)
        {
            myServer = new SimpleStandAloneHttpServer(@"D:\site", portNumber, target);
        }

        public static void TareDown()
        {
            if (myServer != null)
                myServer.Stop();
        }
    }
}
