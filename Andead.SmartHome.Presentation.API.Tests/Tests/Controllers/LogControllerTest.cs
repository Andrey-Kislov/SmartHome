using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Andead.SmartHome.Presentation.API.Inerfaces;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Mock;
using Andead.SmartHome.Presentation.API.Models;
using Andead.SmartHome.UnitOfWork.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Andead.SmartHome.Presentation.API.Tests.Tests.Controllers
{
    [TestFixture]
    public class LogControllerTest : TestBase
    {
        private ILogController _logController;
        private IRepository _repository;

        [SetUp]
        public void Setup()
        {
            var repositoryFactory = new MockRepositoryFactory();
            _logController = LogController(repositoryFactory);

            _repository = repositoryFactory.Create();
            _repository.Add(new Log { Message = "First message" });
            _repository.Add(new Log { Message = "Second message" });
            _repository.Add(new Log { Message = "Third message" });
            _repository.Commit();
        }

        [Test]
        public void GetById_Should_Return_Count_Of_Items()
        {
            var result = _logController.Get();

            var response = (result as OkObjectResult).Value as CustomApiResponse;
            var expect = _repository.Get<Log>().ToArray();

            Assert.IsTrue((result as OkObjectResult).StatusCode == 200);
            Assert.IsTrue((response.Result as Log[]).Length > 0);
            Assert.AreEqual(expect.Length, (response.Result as Log[]).Length);
        }

        [Test]
        public void GetById_Should_Return_Log_Item()
        {
            var result = _logController.GetById(2);

            var response = (result as OkObjectResult).Value as CustomApiResponse;

            Assert.IsTrue((result as OkObjectResult).StatusCode == 200);
            Assert.IsTrue((response.Result as Log).Id == 2);
        }

        [Test]
        public void GetById_Should_Return_Not_Fround_When_Log_Item_Is_Not_Exist()
        {
            var result = _logController.GetById(100);

            Assert.IsTrue((result as NotFoundObjectResult).StatusCode == 404);
        }

        [Test]
        public async Task Add_Should_Correct_Save_Log_Item()
        {
            var logItemMessage = "Test message";

            var result = await _logController.Add(logItemMessage);
            var logItems = _repository.Get<Log>().ToArray();

            Assert.IsTrue((result as OkObjectResult).StatusCode == 200);
            Assert.IsTrue(logItems.Length == 4);
            Assert.IsTrue(logItems.Any(x => x.Message.Equals(logItemMessage)));
        }
    }
}
