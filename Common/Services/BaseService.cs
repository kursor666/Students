using Common.Infrastructure;
using Repository.Interfaces;

namespace Common.Services
{
    public class BaseService
    {
        protected IUnitOfWork Database;
        protected ValidateModule ValidateModule;

        public void Dispose()
        {
            Database?.Dispose();
        }
    }
}