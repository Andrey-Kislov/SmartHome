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

        private bool HasLoop(WorkflowStep[] steps, WorkflowNextStep[] nextSteps)
        {
            var prevStepIds = nextSteps.Select(x => x.PreviousStepId);
            return steps.All(x => prevStepIds.Contains(x.Id));
        }

        private bool CheckFirstStep(WorkflowStep[] steps)
        {
            return steps.Count(x => x.IsFirstStep) != 1;
        }

        public IStep GetWorkflowSteps(int workflowId)
        {
            using var repository = _repositoryFactory.Create();
            var steps = repository.Get<WorkflowStep>().ByWorkflowId(workflowId).Distinct().ToArray();
            var nextSteps = repository.Get<WorkflowNextStep>().ByStepIds(steps.Select(x => x.Id).ToArray()).Distinct().ToArray();

            if (HasLoop(steps, nextSteps))
                throw new Exception("Workflow has loop");

            if (CheckFirstStep(steps))
                throw new Exception("Workflow has problem with first step");

            using var scope = _container.BeginLifetimeScope();

            var step = steps.Single(x => x.IsFirstStep);
            var resolvedStep = scope.ResolveNamed<IStep>(step.WorkflowLogic.ClassName);
            resolvedStep.Id = step.Id;
            resolvedStep.Name = step.StepName;

            resolvedStep.SetNextSteps(scope, steps, nextSteps);

            return resolvedStep;
        }

        public IWorkflowAction GetWorkflowAction(int workflowId)
        {
            using var repository = _repositoryFactory.Create();
            var action = repository.Get<WorkflowAction>().ByWorkflowId(workflowId).FirstOrDefault();

            if (action == null)
                throw new Exception("Not found workflow action");

            using var scope = _container.BeginLifetimeScope();
            return scope.ResolveNamed<IWorkflowAction>(action.ClassName);
        }

        public IWorkflow GetWorkflowById(int workflowId)
        {
            var steps = GetWorkflowSteps(workflowId);
            var action = GetWorkflowAction(workflowId);

            return new Workflow.Workflow(steps, action);
        }

        public bool StartWorkflow(int workflowId)
        {
            var workflow = GetWorkflowById(workflowId);
            return workflow.Start();
        }
    }
}
