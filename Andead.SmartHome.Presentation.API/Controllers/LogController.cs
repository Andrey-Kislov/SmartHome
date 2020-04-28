using System;
using Andead.SmartHome.Presentation.API.Inerfaces;
using Andead.SmartHome.Presentation.API.Models;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Extensions;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Andead.SmartHome.Presentation.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LogController : SmartHomeBaseController, ILogController
    {
        private readonly ILogger<LogController> _logger;
        private readonly IRepositoryFactory _repositoryFactory;

        public LogController(ILogger<LogController> logger, IRepositoryFactory repositoryFactory)
        {
            _logger = logger;
            _repositoryFactory = repositoryFactory;
        }

        [HttpPost("[action]")]
        public IActionResult Add(string message)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                repository.Add(new Log
                {
                    Message = message
                });
                repository.Commit();

                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                return Ok(repository.Get<Log>().ToArray());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetById(int id)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                var logItem = repository.Get<Log>().ById(id).FirstOrDefault();

                if (logItem == null)
                    return NotFound();

                return Ok(logItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
