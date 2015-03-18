using BackToTheFuture.Core;
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
            StaticSite site = new StaticSite(Directory.GetCurrentDirectory());
            site.RefreshStaticContent();
        }
    }
}
