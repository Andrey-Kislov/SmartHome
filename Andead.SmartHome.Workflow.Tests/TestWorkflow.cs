using System.Collections.Concurrent;
using System.Collections.Generic;
using NUnit.Framework;
using Andead.SmartHome.Workflow.Interfaces;

namespace Andead.SmartHome.Workflow.Tests
{
    public class TestWorkflow : WorkflowBase
    {
        private readonly BlockingCollection<string> _messages;

        public TestWorkflow(IList<IStep> steps) : base(steps)
        {
            _messages = new BlockingCollection<string>();
        }

        public TestWorkflow(BlockingCollection<string> messages, IList<IStep> steps) : base(steps)
        {
            _messages = messages;
        }

        public override bool Action()
        {
            var message = "Run action";
            TestContext.WriteLine(message);
            _messages.Add(message);

            return true;
        }
    }
}
