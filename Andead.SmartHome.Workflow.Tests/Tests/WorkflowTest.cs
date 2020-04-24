using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NUnit.Framework;

namespace Andead.SmartHome.Workflow.Tests
{
    public class WorkflowTest
    {
        [Test]
        public void Should_NotBorken_When_NotHaveSteps()
        {
            var workflow = new TestWorkflow(new List<TestStep>().ToArray());
            var result = workflow.Start();

            Assert.IsFalse(result);
        }

        [Test]
        public void Should_ThrowArgumentException_When_NextSteps_AreNull()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var workflow = new TestWorkflow(null);
            });
        }

        [Test]
        public void Should_SuccessOneStep_RunAction()
        {
            var expectedMessages = new BlockingCollection<string>()
            {
                "Success step: 1",
                "Run action"
            };

            BlockingCollection<string> actualMessages = new BlockingCollection<string>();

            var firstStep = new TestStep(actualMessages) { StepId = 1 };

            List<TestStep> steps = new List<TestStep>
            {
                firstStep
            };

            var workflow = new TestWorkflow(actualMessages, steps.ToArray());
            var result = workflow.Start();

            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expectedMessages, actualMessages);
        }

        [Test]
        public void Should_SuccessThreeSteps_RunAction()
        {
            var expectedMessages = new BlockingCollection<string>()
            {
                "Success step: 1",
                "Success step: 2",
                "Success step: 3",
                "Run action"
            };

            BlockingCollection<string> actualMessages = new BlockingCollection<string>();

            var thirdStep = new TestStep(actualMessages) { StepId = 3 };
            var secondStep = new TestStep(actualMessages, new[] { thirdStep }) { StepId = 2 };
            var firstStep = new TestStep(actualMessages, new[] { secondStep }) { StepId = 1 };

            List<TestStep> steps = new List<TestStep>
            {
                firstStep,
                secondStep,
                thirdStep
            };

            var workflow = new TestWorkflow(actualMessages, steps.ToArray());
            var result = workflow.Start();

            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expectedMessages, actualMessages);
        }

        [Test]
        public void Should_SuccessThreeSteps_NotRunningThirdStep_RunAction()
        {
            var expectedMessages = new BlockingCollection<string>()
            {
                "Success step: 1",
                "Success step: 2",
                "Success step: 4",
                "Run action"
            };

            BlockingCollection<string> actualMessages = new BlockingCollection<string>();

            var fourthStep = new TestStep(actualMessages) { StepId = 4 };
            var thirdStep = new TestStep(actualMessages, new[] { fourthStep }) { StepId = 3 };
            var secondStep = new TestStep(actualMessages, new[] { fourthStep }) { StepId = 2 };
            var firstStep = new TestStep(actualMessages, new[] { secondStep, thirdStep }) { StepId = 1 };

            List<TestStep> steps = new List<TestStep>
            {
                firstStep,
                secondStep,
                thirdStep,
                fourthStep
            };

            var workflow = new TestWorkflow(actualMessages, steps.ToArray());
            var result = workflow.Start();

            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expectedMessages, actualMessages);
        }

        [Test]
        public void Should_SuccessThreeSteps_FailedSecondStep_RunAction()
        {
            var expectedMessages = new BlockingCollection<string>()
            {
                "Success step: 1",
                "Failed step: 2",
                "Success step: 3",
                "Success step: 4",
                "Run action"
            };

            BlockingCollection<string> actualMessages = new BlockingCollection<string>();

            var fourthStep = new TestStep(actualMessages) { StepId = 4 };
            var thirdStep = new TestStep(actualMessages, new[] { fourthStep }) { StepId = 3 };
            var secondStep = new TestStep(actualMessages, new[] { fourthStep }) { StepId = 2, IsError = true };
            var firstStep = new TestStep(actualMessages, new[] { secondStep, thirdStep }) { StepId = 1 };

            List<TestStep> steps = new List<TestStep>
            {
                firstStep,
                secondStep,
                thirdStep,
                fourthStep
            };

            var workflow = new TestWorkflow(actualMessages, steps.ToArray());
            var result = workflow.Start();

            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expectedMessages, actualMessages);
        }

        [Test]
        public void Should_SuccessOneStep_FailedTwoSteps_NotRunAction()
        {
            var expectedMessages = new BlockingCollection<string>()
            {
                "Success step: 1",
                "Failed step: 2",
                "Failed step: 3"
            };

            BlockingCollection<string> actualMessages = new BlockingCollection<string>();

            var fourthStep = new TestStep(actualMessages) { StepId = 4 };
            var thirdStep = new TestStep(actualMessages, new[] { fourthStep }) { StepId = 3, IsError = true };
            var secondStep = new TestStep(actualMessages, new[] { fourthStep }) { StepId = 2, IsError = true };
            var firstStep = new TestStep(actualMessages, new[] { secondStep, thirdStep }) { StepId = 1 };

            List<TestStep> steps = new List<TestStep>
            {
                firstStep,
                secondStep,
                thirdStep,
                fourthStep
            };

            var workflow = new TestWorkflow(actualMessages, steps.ToArray());
            var result = workflow.Start();

            Assert.IsFalse(result);
            CollectionAssert.AreEqual(expectedMessages, actualMessages);
        }

        [Test]
        public void Should_SuccessFourSteps_NotRunningThirdStep_RunAction()
        {
            var expectedMessages = new BlockingCollection<string>()
            {
                "Success step: 1",
                "Success step: 2",
                "Success step: 4",
                "Success step: 5",
                "Run action"
            };

            BlockingCollection<string> actualMessages = new BlockingCollection<string>();

            var fifthStep = new TestStep(actualMessages) { StepId = 5 };
            var fourthStep = new TestStep(actualMessages, new[] { fifthStep }) { StepId = 4 };
            var thirdStep = new TestStep(actualMessages, new[] { fifthStep }) { StepId = 3 };
            var secondStep = new TestStep(actualMessages, new[] { fourthStep }) { StepId = 2 };
            var firstStep = new TestStep(actualMessages, new[] { secondStep, thirdStep }) { StepId = 1 };

            List<TestStep> steps = new List<TestStep>
            {
                firstStep,
                secondStep,
                thirdStep,
                fourthStep,
                fifthStep
            };

            var workflow = new TestWorkflow(actualMessages, steps.ToArray());
            var result = workflow.Start();

            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expectedMessages, actualMessages);
        }

        [Test]
        public void Should_SuccessThreeSteps_FailedSecondStep_NotRunningFourthStep_RunAction()
        {
            var expectedMessages = new BlockingCollection<string>()
            {
                "Success step: 1",
                "Failed step: 2",
                "Success step: 3",
                "Success step: 5",
                "Run action"
            };

            BlockingCollection<string> actualMessages = new BlockingCollection<string>();

            var fifthStep = new TestStep(actualMessages) { StepId = 5 };
            var fourthStep = new TestStep(actualMessages, new[] { fifthStep }) { StepId = 4 };
            var thirdStep = new TestStep(actualMessages, new[] { fifthStep }) { StepId = 3 };
            var secondStep = new TestStep(actualMessages, new[] { fourthStep }) { StepId = 2, IsError = true };
            var firstStep = new TestStep(actualMessages, new[] { secondStep, thirdStep }) { StepId = 1 };

            List<TestStep> steps = new List<TestStep>
            {
                firstStep,
                secondStep,
                thirdStep,
                fourthStep,
                fifthStep
            };

            var workflow = new TestWorkflow(actualMessages, steps.ToArray());
            var result = workflow.Start();

            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expectedMessages, actualMessages);
        }

        [Test]
        public void Should_SuccessFourSteps_FailedFourthStep_RunAction()
        {
            var expectedMessages = new BlockingCollection<string>()
            {
                "Success step: 1",
                "Success step: 2",
                "Failed step: 4",
                "Success step: 3",
                "Success step: 5",
                "Run action"
            };

            BlockingCollection<string> actualMessages = new BlockingCollection<string>();

            var fifthStep = new TestStep(actualMessages) { StepId = 5 };
            var fourthStep = new TestStep(actualMessages, new[] { fifthStep }) { StepId = 4, IsError = true };
            var thirdStep = new TestStep(actualMessages, new[] { fifthStep }) { StepId = 3 };
            var secondStep = new TestStep(actualMessages, new[] { fourthStep }) { StepId = 2 };
            var firstStep = new TestStep(actualMessages, new[] { secondStep, thirdStep }) { StepId = 1 };

            List<TestStep> steps = new List<TestStep>
            {
                firstStep,
                secondStep,
                thirdStep,
                fourthStep,
                fifthStep
            };

            var workflow = new TestWorkflow(actualMessages, steps.ToArray());
            var result = workflow.Start();

            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expectedMessages, actualMessages);
        }

        [Test]
        public void Should_SuccessEightSteps_FailedSeventhStep_RunAction()
        {
            var expectedMessages = new BlockingCollection<string>()
            {
                "Success step: 1",
                "Success step: 2",
                "Success step: 4",
                "Failed step: 7",
                "Success step: 5",
                "Success step: 3",
                "Success step: 6",
                "Success step: 8",
                "Success step: 9",
                "Run action"
            };

            BlockingCollection<string> actualMessages = new BlockingCollection<string>();

            var ninthStep = new TestStep(actualMessages) { StepId = 9 };
            var eighthStep = new TestStep(actualMessages, new[] { ninthStep }) { StepId = 8 };
            var seventhStep = new TestStep(actualMessages, new[] { eighthStep }) { StepId = 7, IsError = true };
            var sixthStep = new TestStep(actualMessages, new[] { eighthStep }) { StepId = 6 };
            var fifthStep = new TestStep(actualMessages, new[] { seventhStep }) { StepId = 5 };
            var fourthStep = new TestStep(actualMessages, new[] { seventhStep }) { StepId = 4 };
            var thirdStep = new TestStep(actualMessages, new[] { fourthStep, sixthStep }) { StepId = 3 };
            var secondStep = new TestStep(actualMessages, new[] { fourthStep, fifthStep }) { StepId = 2 };
            var firstStep = new TestStep(actualMessages, new[] { secondStep, thirdStep }) { StepId = 1 };

            List<TestStep> steps = new List<TestStep>
            {
                firstStep,
                secondStep,
                thirdStep,
                fourthStep,
                fifthStep,
                sixthStep,
                seventhStep,
                eighthStep,
                ninthStep
            };

            var workflow = new TestWorkflow(actualMessages, steps.ToArray());
            var result = workflow.Start();

            Assert.IsTrue(result);
            CollectionAssert.AreEqual(expectedMessages, actualMessages);
        }

        [Test]
        public void Should_ReturnFalse_When_LoopingSteps()
        {
            var expectedMessages = new BlockingCollection<string>()
            {
                "Success step: 1",
                "Success step: 2",
                "Success step: 3",
                "Success step: 4",
                "Success step: 5",
                "Success step: 6",
                "Success step: 7",
                "Success step: 8",
                "Success step: 9"
            };

            BlockingCollection<string> actualMessages = new BlockingCollection<string>();

            TestStep ninthStep = new TestStep(actualMessages) { StepId = 9 };
            var eighthStep = new TestStep(actualMessages, new[] { ninthStep }) { StepId = 8 };
            var seventhStep = new TestStep(actualMessages, new[] { eighthStep }) { StepId = 7 };
            var sixthStep = new TestStep(actualMessages, new[] { seventhStep }) { StepId = 6 };
            var fifthStep = new TestStep(actualMessages, new[] { sixthStep }) { StepId = 5 };
            var fourthStep = new TestStep(actualMessages, new[] { fifthStep }) { StepId = 4 };
            var thirdStep = new TestStep(actualMessages, new[] { fourthStep }) { StepId = 3 };
            var secondStep = new TestStep(actualMessages, new[] { thirdStep }) { StepId = 2 };
            var firstStep = new TestStep(actualMessages, new[] { secondStep }) { StepId = 1 };

            ninthStep.NextSteps.Add(fifthStep);

            List<TestStep> steps = new List<TestStep>
            {
                firstStep,
                secondStep,
                thirdStep,
                fourthStep,
                fifthStep,
                sixthStep,
                seventhStep,
                eighthStep,
                ninthStep
            };

            var workflow = new TestWorkflow(actualMessages, steps.ToArray());
            var result = workflow.Start();

            Assert.IsFalse(result);
            CollectionAssert.AreEqual(expectedMessages, actualMessages);
        }
    }
}