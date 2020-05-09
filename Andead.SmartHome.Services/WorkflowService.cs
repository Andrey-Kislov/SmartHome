using System;
using System.Linq;
using Autofac;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Andead.SmartHome.UnitOfWork.Extensions;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Andead.SmartHome.Workflow.Interfaces;
using Andead.SmartHome.Services.Interfaces;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.Workflow.Extensions;

namespace Andead.SmartHome.Services
{
    public class WorkflowService : IService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ILoggerFactory _loggerFactory;
        private IContainer _container;

        public WorkflowService(IRepositoryFactory repositoryFactory, ILoggerFactory loggerFactory)
        {
            _repositoryFactory = repositoryFactory;
            _loggerFactory = loggerFactory;
        }

        public void Start()
        {
            var builder = new ContainerBuilder();
            _container = Bootstrapper.RegisterDependencies(builder, _loggerFactory);
        }

        public IList<UnitOfWork.Entities.Workflow> GetWorkflows(int userId, int deviceId)
        {
            using var repository = _repositoryFactory.Create();
            return repository.Get<UnitOfWork.Entities.Workflow>().ByUserId(userId, deviceId).ToArray();
        }

        private bool HasLoop(WorkflowStep[] steps)
        {
            return steps.All(x => x.ParentStepId.HasValue);
        }

        private bool CheckFirstStep(WorkflowStep[] steps)
        {
            return steps.Count(x => x.IsFirstStep) != 1;
        }

        public (IStep, IWorkflowAction) GetWorkflowSteps(int workflowId)
        {
            using var repository = _repositoryFactory.Create();
            var workflow = repository.Get<UnitOfWork.Entities.Workflow>().ById(workflowId).FirstOrDefault();

            if (workflow == null)
                throw new Exception("Workflow not found");

            var steps = workflow.Steps.ToArray();

            if (HasLoop(steps))
                throw new Exception("Workflow has loop");

            if (CheckFirstStep(steps))
                throw new Exception("Workflow has problem with first step");

            using var scope = _container.BeginLifetimeScope();

            var step = steps.Single(x => x.IsFirstStep);
            var resolvedStep = scope.ResolveNamed<IStep>(step.WorkflowLogic.ClassName);
            resolvedStep.Id = step.Id;
            resolvedStep.Name = step.StepName;

            resolvedStep.SetNextSteps(scope, steps.SelectMany(x => x.NextSteps).Distinct().ToArray());

            var resolvedAction = scope.ResolveNamed<IWorkflowAction>(workflow.Action.WorkflowLogic.ClassName);

            return (resolvedStep, resolvedAction);
        }

        public IWorkflow GetWorkflowById(int workflowId)
        {
            var (steps, action) = GetWorkflowSteps(workflowId);
            return new Workflow.Workflow(steps, action);
        }

        public bool StartWorkflow(int workflowId)
        {
            var workflow = GetWorkflowById(workflowId);
            return workflow.Start();
        }
    }
}
