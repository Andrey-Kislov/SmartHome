using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Andead.SmartHome.Presentation.API.Controllers;
using Andead.SmartHome.Presentation.API.Inerfaces;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.Presentation.API.Tests.Wrappers
{
    public class LogControllerWrapper : ILogController
    {
        private readonly ILogController _logController;

        public LogControllerWrapper(IRepositoryFactory repositoryFactory)
        {
            _logController = new LogController(null, repositoryFactory);
        }

        public IActionResult Add(string message)
        {
            var result = Execute(_logController.Add, message);
            return result;
        }

        public IActionResult Get()
        {
            var result = Execute(_logController.Get);
            return result;
        }

        public IActionResult GetById(long id)
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
