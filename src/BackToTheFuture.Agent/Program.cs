using System;
using BackToTheFuture.Core.SiteSources;
using BackToTheFuture.Core;
using System.Diagnostics;
using System.IO;
using BackToTheFuture.Hosting.StandAloneHttpServer;
<<<<<<< HEAD
using BackToTheFuture.Core.SiteTargets;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using BackToTheFuture.Hosting;

=======
using Microsoft.Owin.Hosting;
using Owin;
using BackToTheFuture.Core.SiteTargets;
using BackToTheFuture.Hosting.OwinComponents;
>>>>>>> origin/master
namespace BackToTheFuture.Agent
{
    //netsh http add urlacl url=http://+:80/ user=Everyone listen=yes
    //netsh http delete urlacl url=http://+:80/
    class Program
    {
<<<<<<< HEAD
        static SimpleStandAloneHttpServer myServer;
        static int portNumber = 801;
=======
        public static InMemorySiteTarget Site { get; set; }
>>>>>>> origin/master

        static void Main(string[] args)
        {
            var source = new DirectorySiteSource(new DirectoryInfo(@"c:\site"));
            Site = new InMemorySiteTarget(source);

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
                Process.Start(string.Format("http://localhost:{0}", port));
                Console.ReadKey();
            }
        }
    }

<<<<<<< HEAD
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
=======
    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var site = Program.Site;
            app.UseBackToTheFutureSite(site);
>>>>>>> origin/master
        }
    }
}
