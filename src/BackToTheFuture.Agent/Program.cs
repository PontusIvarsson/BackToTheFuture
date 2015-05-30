using System;
using BackToTheFuture.Core.SiteSources;
using BackToTheFuture.Core;
using System.Diagnostics;
using System.IO;
using BackToTheFuture.Hosting.StandAloneHttpServer;
using Microsoft.Owin.Hosting;
using Owin;
using BackToTheFuture.Core.SiteTargets;
using BackToTheFuture.Hosting.OwinComponents;

namespace BackToTheFuture.Agent
{
    //netsh http add urlacl url=http://+:80/ user=Everyone listen=yes
    //netsh http delete urlacl url=http://+:80/
    class Program
    {
        public static InMemorySiteTarget Site { get; set; }
        static void Main(string[] args)
        {
            var source = new DirectorySiteSource(new DirectoryInfo(@"C:\Users\Pontus\SkyDrive\Documents\Blogg"));
            Site = new InMemorySiteTarget(source);
            Site.AddPlugin(new MarkupReplacePlugin());

            SimpleHttp(Site, 80);
            OwinHttp(Site, 8081);
        }

        private static void SimpleHttp(ISiteTarget site, int port)
        {
            SimpleStandAloneHttpServer myServer = new SimpleStandAloneHttpServer(port, site);

            Process.Start(string.Format("http://localhost:{0}/test.json", myServer.Port));
        }

        private static void OwinHttp(ISiteTarget site, int port)
        {
            string uri = string.Format("http://localhost:{0}", port);
            using(WebApp.Start<Startup>(uri))
            {
                Process.Start(string.Format("http://localhost:{0}/test.json", port));
                Console.ReadKey();
            }
        }
    }

    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var site = Program.Site;
            app.UseBackToTheFutureSite(site);
        }
    }
}
