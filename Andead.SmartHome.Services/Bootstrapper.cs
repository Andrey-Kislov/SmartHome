using Autofac;
using Microsoft.Extensions.Logging;
using Andead.SmartHome.Workflow.Actions;
using Andead.SmartHome.Workflow.Interfaces;
using Andead.SmartHome.Workflow.Steps;

namespace Andead.SmartHome.Services
{
    public static class Bootstrapper
    {
        public static IContainer RegisterDependencies(ContainerBuilder builder, ILoggerFactory loggerFactory)
        {
            builder
                .RegisterType<LogWorkflowStep>()
                    .Named<IStep>(nameof(LogWorkflowStep))
                    .WithParameter(new TypedParameter(typeof(ILogger<LogWorkflowStep>), loggerFactory.CreateLogger<LogWorkflowStep>()));

            builder
                .RegisterType<LogAction>()
                    .Named<IWorkflowAction>(nameof(LogAction))
                    .WithParameter(new TypedParameter(typeof(ILogger<LogAction>), loggerFactory.CreateLogger<LogAction>()));

            return builder.Build();
        }
    }
}
