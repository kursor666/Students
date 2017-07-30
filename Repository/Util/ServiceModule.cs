using Domain;
using Ninject.Modules;
using Repository.Interfaces;

namespace Repository.Util
{
    public class ServiceModule : NinjectModule
    {
        private readonly UniversityContext _context;

        public ServiceModule(UniversityContext context)
        {
            _context = context;
        }

        public override void Load()
        {
            Bind<IDbRepository<BaseModel>>().To<DbRepository<BaseModel>>().WithConstructorArgument(_context);
        }
    }
}