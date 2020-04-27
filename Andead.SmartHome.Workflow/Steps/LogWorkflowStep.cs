using System.Collections.Generic;
using Andead.SmartHome.Workflow.Interfaces;
using Microsoft.Extensions.Logging;

namespace Andead.SmartHome.Workflow.Steps
{
    public class LogWorkflowStep : StepBase
    {
        private readonly ILogger _logger;

        public LogWorkflowStep(ILogger<LogWorkflowStep> logger)
        {
            _logger = logger;
        }

        public LogWorkflowStep(ILogger<LogWorkflowStep> logger, IList<IStep> nextSteps) : base(nextSteps)
        {
            _logger = logger;
        }

        public override bool Run()
        {
            _logger.LogInformation("This is log workflow step");
            return true;
        }
    }
}
