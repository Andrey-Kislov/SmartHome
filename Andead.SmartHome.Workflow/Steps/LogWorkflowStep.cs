using System;
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

        public override bool Run()
        {
            _logger.LogInformation("This is log workflow step");
            return true;
        }
    }
}
