using Moq;
using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Andead.SmartHome.Presentation.API.Controllers;
using Andead.SmartHome.Presentation.API.Inerfaces;
using Andead.SmartHome.UnitOfWork.Interfaces;
using System.Threading.Tasks;
using Andead.SmartHome.Presentation.API.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Andead.SmartHome.Presentation.API.Tests.Wrappers
{
    public class LogControllerWrapper : ILogController
    {
        private readonly ILogController _logController;

        public LogControllerWrapper(IRepositoryFactory repositoryFactory)
        {
            var logHub = new Mock<IHubContext<LogHub, ILogHub>>();
            var mockClients = new Mock<IHubClients<ILogHub>>();
            var mockClientProxy = new Mock<ILogHub>();

            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);
            logHub.Setup(x => x.Clients).Returns(() => mockClients.Object);

            _logController = new LogController(null, repositoryFactory, logHub.Object);
        }

        public Task<IActionResult> Add(string message)
        {
            var result = Execute(_logController.Add, message);
            return result;
        }

        public IActionResult Get()
        {
            var result = Execute(_logController.Get);
            return result;
        }

        public IActionResult GetById(int id)
        {
            var result = Execute(_logController.GetById, id);
            return result;
        }

        private TResult Execute<TResult>(Func<TResult> func, [CallerMemberName] string methodeName = "")
        {
            return func();
        }

        private TResult Execute<TResult, TCommand>(Func<TCommand, TResult> func, TCommand command, [CallerMemberName] string methodeName = "")
        {
            return func(command);
        }
    }
}
