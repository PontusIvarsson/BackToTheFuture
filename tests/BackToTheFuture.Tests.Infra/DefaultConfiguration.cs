using BackToTheFuture.Tests.Infra;
using Fixie;

namespace BackToTheFuture.Tests.Infra
{
    public class DefaultConfiguration : Convention
    {
        public DefaultConfiguration()
        {
            Classes.NameEndsWith("Tests");
            Parameters
            .Add<FromSectionInFile>()
            .Add<FromInputAttributes>();
        }
    }
}
