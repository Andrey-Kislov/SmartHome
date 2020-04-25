using Moq;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Andead.SmartHome.UnitOfWork.Mock;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.Services;
using Andead.SmartHome.Workflow.Steps;

namespace Andead.SmartHome.Workflow.Tests.Tests
{
    [TestFixture]
    public class WorkflowServiceTest
    {
        private readonly IRepositoryFactory _repositoryFactory = new MockRepositoryFactory();
        private readonly List<string> _actualMessages = new List<string>();
        private Mock<ILoggerFactory> _loggerFactory;
        private Mock<ILogger<LogWorkflowStep>> _logger;

        [SetUp]
        public void Setup()
        {
            _loggerFactory = new Mock<ILoggerFactory>();
            _logger = new Mock<ILogger<LogWorkflowStep>>();

            _logger
                .Setup(x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
                .Callback(new InvocationAction(invocation =>
                {
                    _actualMessages.Add(invocation.Arguments[2].ToString());
                }));

            _loggerFactory
                .Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(_logger.Object);

            var repository = _repositoryFactory.Create();
            repository.Add(new WorkflowStep
            {
                StepName = "Log Step",
                WorkflowId = 1,
                ClassName = "LogWorkflowStep",
                IsFirstStep = true
            });
            repository.Add(new WorkflowStep
            {
                StepName = "Log Step 2",
                WorkflowId = 1,
                ClassName = "LogWorkflowStep",
                IsFirstStep = false
            });
            repository.Add(new WorkflowStep
            {
                StepName = "Log Step 3",
                WorkflowId = 2,
                ClassName = "LogWorkflowStep",
                IsFirstStep = true
            });
            repository.Commit();
        }

        [Test]
        public void GetWorkflowSteps_Should_ReturnSteps_ByWorkflowId()
        {
            var expectedMessages = new List<string>
            {
                "This is log workflow step",
                "This is log workflow step"
            };

            var service = new WorkflowService(_repositoryFactory, _loggerFactory.Object);
            service.Start();

            var steps = service.GetWorkflowSteps(1);
            Assert.IsTrue(steps.Count == 2);

            foreach (var step in steps)
            {
                step.Run();
            }

            CollectionAssert.AreEqual(expectedMessages, _actualMessages);
        }
    }
}
