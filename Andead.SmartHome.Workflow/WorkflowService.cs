using Autofac;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Andead.SmartHome.UnitOfWork.Extensions;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Andead.SmartHome.Workflow.Interfaces;
using Andead.SmartHome.Workflow.Steps;

namespace Andead.SmartHome.Workflow
{
    public class WorkflowService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ILogger _logger;
        private IContainer _container;

        public WorkflowService(IRepositoryFactory repositoryFactory, ILogger logger)
        {
            _repositoryFactory = repositoryFactory;
            _logger = logger;
        }

        public void Setup()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterType<LogWorkflowStep>()
                    .Named<IStep>("LogWorkflowStep")
                    .WithParameter(new TypedParameter(typeof(ILogger<LogWorkflowStep>), _logger));

            _container = builder.Build();
        }

        public IList<UnitOfWork.Entities.Workflow> GetWorkflows(int userId, int deviceId)
        {
            using var repository = _repositoryFactory.Create();
            return repository.Get<UnitOfWork.Entities.Workflow>().ByUserId(userId, deviceId).ToArray();
        }

        public IList<IStep> GetWorkflowSteps(int workflowId)
        {
            using var repository = _repositoryFactory.Create();
            var steps = repository.Get<UnitOfWork.Entities.WorkflowStep>().ByWorkflowId(workflowId).ToArray();

            List<IStep> result = new List<IStep>();
            using (var scope = _container.BeginLifetimeScope())
            {
                foreach (var step in steps)
                {
                    result.Add(scope.ResolveNamed<IStep>(step.ClassName));
                }
            }

            return result;
        }
    }
}
