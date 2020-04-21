using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork.Mock
{
    public class MockRepositoryFactory : IRepositoryFactory
    {
        public IRepository Create()
        {
            return new MockRepository();
        }
    }
}
