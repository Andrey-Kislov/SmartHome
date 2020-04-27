using Andead.SmartHome.Workflow.Interfaces;

namespace Andead.SmartHome.Workflow
{
    public class Workflow : WorkflowBase
    {
        public Workflow(IStep firstStep, IWorkflowAction action) : base(firstStep, action)
        {
        }
    }
}
