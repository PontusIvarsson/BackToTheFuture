using BackToTheFuture.Tests.Infra;
using Fixie;

namespace BackToTheFuture.Core.Tests
{
    public class DefaultTestAssembly : TestAssembly
    {
        public DefaultTestAssembly()
        {
            Apply<DefaultConfiguration>();
        }
    }

    public class Configuration : Convention
    {
        public Configuration()
        {
            //Override default configuration here
        }
    }
}
