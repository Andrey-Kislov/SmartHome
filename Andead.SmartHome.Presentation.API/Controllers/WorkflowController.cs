using System;
using Microsoft.AspNetCore.Mvc;
using Andead.SmartHome.Services;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.Presentation.API.Models;

namespace Andead.SmartHome.Presentation.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WorkflowController : SmartHomeBaseController
    {
        private readonly WorkflowService _workflowService;
        private readonly IRepositoryFactory _repositoryFactory;

        public WorkflowController(IRepositoryFactory repositoryFactory, WorkflowService workflowService)
        {
            _workflowService = workflowService;
            _repositoryFactory = repositoryFactory;
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
                    ClassName = command.ClassName,
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

        [HttpGet("[action]")]
        public IActionResult Test(int workflowId)
        {
            try
            {
                var steps = _workflowService.GetWorkflowSteps(workflowId);
                foreach (var step in steps)
                {
                    step.Run();
                }

                return Ok(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
