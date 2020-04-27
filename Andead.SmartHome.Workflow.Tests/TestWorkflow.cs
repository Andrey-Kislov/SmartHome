using System.Collections.Concurrent;
using System.Collections.Generic;
using Andead.SmartHome.Workflow.Interfaces;

namespace Andead.SmartHome.Workflow.Tests
{
    public class TestWorkflow : WorkflowBase
    {
        private readonly BlockingCollection<string> _messages;

        public TestWorkflow(IStep firstStep, IWorkflowAction action) : base(firstStep, action)
        {
            _messages = new BlockingCollection<string>();
        }

        public TestWorkflow(BlockingCollection<string> messages, IStep firstStep, IWorkflowAction action) : base(firstStep, action)
        {
            _messages = messages;
        }
    }
}
