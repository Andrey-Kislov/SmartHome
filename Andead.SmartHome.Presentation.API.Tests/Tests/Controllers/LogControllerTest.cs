using NUnit.Framework;
using Andead.SmartHome.Presentation.API.Inerfaces;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Mock;
using Andead.SmartHome.Presentation.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Andead.SmartHome.Presentation.API.Tests.Tests.Controllers
{
    [TestFixture]
    public class LogControllerTest : TestBase
    {
        private ILogController _logController;

        [SetUp]
        public void Setup()
        {
            var repositoryFactory = new MockRepositoryFactory();
            _logController = LogController(repositoryFactory);

            var repository = repositoryFactory.Create();
            repository.Add(new Log { Message = "First message" });
            repository.Add(new Log { Message = "Second message" });
            repository.Add(new Log { Message = "Third message" });
            repository.Commit();
        }

        [Test]
        public void GetById_Should_Return_Count_Of_Items()
        {
            var result = _logController.Get();

            var response = (result as OkObjectResult).Value as CustomApiResponse;

            Assert.IsTrue((response.Result as Log[]).Length == 3);
        }

        [Test]
        public void GetById_Should_Return_Log_Item()
        {
            var result = _logController.GetById(2);

            var response = (result as OkObjectResult).Value as CustomApiResponse;

            Assert.IsNotNull((response.Result as Log).Id == 2);
        }
    }
}
