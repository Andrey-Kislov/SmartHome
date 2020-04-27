using Andead.SmartHome.Workflow.Interfaces;
using Microsoft.Extensions.Logging;

namespace Andead.SmartHome.Workflow.Actions
{
    public class LogAction : IWorkflowAction
    {
        private readonly ILogger _logger;

        public LogAction(ILogger<LogAction> logger)
        {
            _logger = logger;
        }

        public bool RunAction()
        {
            _logger.LogInformation("This is log action");
            return true;
        }
    }
}
