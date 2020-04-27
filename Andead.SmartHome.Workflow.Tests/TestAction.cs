using NUnit.Framework;
using System.Collections.Concurrent;
using Andead.SmartHome.Workflow.Interfaces;

namespace Andead.SmartHome.Workflow.Tests
{
    public class TestAction : IWorkflowAction
    {
        private readonly BlockingCollection<string> _messages;

        public TestAction(BlockingCollection<string> messages)
        {
            _messages = messages;
        }

        public bool RunAction()
        {
            var message = "Run action";
            TestContext.WriteLine(message);
            _messages.Add(message);

            return true;
        }
    }
}
