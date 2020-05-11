using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Andead.SmartHome.Constants;
using Andead.SmartHome.Presentation.API.Models;
using Andead.SmartHome.Services;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.Presentation.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : SmartHomeBaseController
    {
        private readonly MqttService _mqttService;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        public UserController(MqttService mqttService, IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _mqttService = mqttService;
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
    }

        [HttpPost("users/add")]
        public IActionResult AddUser(AddUserCommand command)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                repository.Add(new User
                {
                    Username = command.Username,
                    FirstName = command.FirstName,
                    LastName = command.LastName
                });
                repository.Commit();

                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
