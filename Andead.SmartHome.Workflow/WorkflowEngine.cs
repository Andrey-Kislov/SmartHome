using System.Collections.Generic;
using Andead.SmartHome.Workflow.Interfaces;

namespace Andead.SmartHome.Workflow
{
    public class WorkflowEngine
    {
        private readonly IList<IWorkflow> _workflows = new List<IWorkflow>();

        public WorkflowEngine() { }

        public WorkflowEngine(IList<IWorkflow> workflows)
        {
            _workflows = workflows;
        }

        public void AddWorkflow(IWorkflow workflow)
        {
            _workflows.Add(workflow);
        }

        public void Start()
        {
            foreach (var workflow in _workflows)
            {
                workflow.Start();
            }
        }
    }
}
