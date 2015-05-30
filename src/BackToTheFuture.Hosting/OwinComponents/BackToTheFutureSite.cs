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

        public async Task Invoke(IDictionary<string, object> env)
        {
            Console.WriteLine(string.Format("0: {0}", env.FirstOrDefault().ToString()));
            var responseHeaders = (IDictionary<string, string[]>)env["owin.ResponseHeaders"];

            var requestPath = env["owin.RequestPath"].ToString().Remove(0, 1);
            var resourceToServer = _site.GetByName(requestPath);
            var response = env["owin.ResponseBody"] as Stream;
            if (resourceToServer != null)
                resourceToServer.GetStream().CopyTo(response);

            await _next(env);
        }

    }
}
