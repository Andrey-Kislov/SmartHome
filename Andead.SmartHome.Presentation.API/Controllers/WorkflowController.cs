using AutoMapper;
using System;
using Microsoft.AspNetCore.Mvc;
using Andead.SmartHome.Services;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.Presentation.API.Models;
using Andead.SmartHome.UnitOfWork.Extensions;

namespace Andead.SmartHome.Presentation.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WorkflowController : SmartHomeBaseController
    {
        private readonly WorkflowService _workflowService;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        public WorkflowController(IRepositoryFactory repositoryFactory, WorkflowService workflowService, IMapper mapper)
        {
            _workflowService = workflowService;
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public IActionResult AddWorkflow(string workflowName, int deviceId = 1, int userId = 1, bool isActive = true)
        {
            try
            {
                using var repository = _repositoryFactory.Create();

                repository.Add(new UnitOfWork.Entities.Workflow
                {
                    WorkflowName = workflowName,
                    DeviceId = deviceId,
                    UserId = userId,
                    IsActive = isActive
                });

                repository.Commit();
                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("get")]
        public IActionResult GetWorkflows()
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                var result = _mapper.Map<WorkflowDto[]>(repository.Get<UnitOfWork.Entities.Workflow>().ToArray());
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("get")]
        public IActionResult GetWorkflows(int userId, int deviceId)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                var result = _mapper.Map<WorkflowDto[]>(repository.Get<UnitOfWork.Entities.Workflow>().ByUserId(userId, deviceId).ToArray());

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("step/add")]
        public IActionResult AddWorkflowStep(AddWorkflowStepCommand command)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                
                repository.Add(new WorkflowStep
                {
                    StepName = command.StepName,
                    WorkflowId = command.WorkflowId,
                    WorkflowLogicId = command.WorkflowLogicId,
                    ParentStepId = command.ParentStepId,
                    IsFirstStep = command.IsFirstStep
                });

                repository.Commit();
                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("steps/get")]
        public IActionResult GetWorkflowSteps()
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                var result = _mapper.Map<WorkflowStepDto[]>(repository.Get<WorkflowStep>().ToArray());

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("action/add")]
        public IActionResult AddWorkflowAction(AddWorkflowActionCommand command)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                repository.Add(new WorkflowAction
                {
                    ActionName = command.ActionName,
                    WorkflowId = command.WorkflowId,
                    WorkflowLogicId = command.WorkflowLogicId
                });

                repository.Commit();
                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("logic/add")]
        public IActionResult AddWorkflowLogic(string logicName, string className)
        {
            try
            {
                using var repository = _repositoryFactory.Create();
                repository.Add(new WorkflowLogic
                {
                    LogicName = logicName,
                    ClassName = className
                });

                repository.Commit();
                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("[action]")]
        public IActionResult Start(int workflowId)
        {
            try
            {
                return Ok(_workflowService.StartWorkflow(workflowId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
