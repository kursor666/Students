using Ninject.Modules;

namespace Common.Infrastructure
{
    public class ServiceModule:NinjectModule
    {
        public override void Load()
        {
            Bind(I)
        }
    }
}