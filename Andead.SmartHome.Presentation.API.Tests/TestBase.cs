using Andead.SmartHome.Presentation.API.Inerfaces;
using Andead.SmartHome.Presentation.API.Tests.Wrappers;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.Presentation.API.Tests
{
    public class TestBase
    {
        public ILogController LogController(IRepositoryFactory repositoryFactory)
        {
            return new LogControllerWrapper(repositoryFactory);
        }
    }
}