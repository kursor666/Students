using Ninject.Modules;
using Repository;
using Repository.Interfaces;

namespace Common.Infrastructure
{
    public class ServiceModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}