using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly string _connectionString;
        public RepositoryFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IRepository Create()
        {
            return new Repository(_connectionString);
        }
    }
}
