using BackToTheFuture.Core.SiteTargets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Owin;


namespace BackToTheFuture.Hosting.OwinComponents
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    using BackToTheFuture.Core.ResourceTypes;

    public static class BackToTheFutureSiteExtentions
    {
        public static void UseBackToTheFutureSite(this IAppBuilder app, ISiteTarget site)
        {
            app.Use<BackToTheFutureSite>(site);
        }
    }

    public class BackToTheFutureSite
    {
        AppFunc _next;
        ISiteTarget _site;
        public BackToTheFutureSite(AppFunc next, ISiteTarget site)
        {
            _next = next;
            _site = site;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            Console.WriteLine(string.Format("0: {0}", environment.FirstOrDefault().ToString()));
            var requestPath = environment["owin.RequestPath"].ToString().Remove(0, 1);
            var resoruceToServer = _site.GetByName(requestPath);
            var response = environment["owin.ResponseBody"] as Stream;
            if (resoruceToServer != null)
                resoruceToServer.GetStream().CopyTo(response);

            await _next(environment);
        }

    }


    public static class BackToTheFutureSiteExtentions2
    {
        public static void UseBackToTheFutureSite2(this IAppBuilder app, ISiteTarget site)
        {
            app.Use<BackToTheFutureSite>(site);
        }
    }

    public class BackToTheFutureSite2
    {
        AppFunc _next;
        ISiteTarget _site;
        public BackToTheFutureSite2(AppFunc next, ISiteTarget site)
        {
            _next = next;
            _site = site;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            Console.WriteLine(string.Format("0: {0}", environment.FirstOrDefault().ToString()));
            var requestPath = environment["owin.RequestPath"].ToString().Remove(0, 1);
            var resoruceToServer = _site.GetByName(requestPath);
            var response = environment["owin.ResponseBody"] as Stream;
            if (resoruceToServer != null)
                resoruceToServer.GetStream().CopyTo(response);

            await _next(environment);
        }

    }




}
