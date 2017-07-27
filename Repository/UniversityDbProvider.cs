namespace Repository
{
    internal class UniversityDbProvider
    {
        static UniversityContext _dbProvider;
        private UniversityDbProvider() { }
        public static UniversityContext GetContext()
        {
            return _dbProvider ?? (_dbProvider = new UniversityContext());
        }
    }
}