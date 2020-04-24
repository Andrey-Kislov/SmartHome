using NUnit.Framework;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Andead.SmartHome.Workflow.Interfaces;

namespace Andead.SmartHome.Workflow.Tests
{
    public class TestStep : StepBase
    {
        public int StepId { get; set; }
        public bool IsError { get; set; }

        private readonly BlockingCollection<string> _messages;

        public TestStep(BlockingCollection<string> messages)
        {
            _messages = messages;
        }

        public TestStep(BlockingCollection<string> messages, IList<IStep> nextSteps) : base(nextSteps)
        {
            _messages = messages;
        }

        public static TestStep AddNextSteps(BlockingCollection<string> messages, IList<IStep> nextSteps)
        {
            return new TestStep(messages, nextSteps);
        }

        public override bool Run()
        {
            var message = $"Success step: {StepId}";

            if (IsError)
            {
                message = $"Failed step: {StepId}";
                TestContext.WriteLine(message);
                _messages.Add(message);

                return false;
            }

            TestContext.WriteLine(message);
            _messages.Add(message);

            return true;
        }
    }
}
