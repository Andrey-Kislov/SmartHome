using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.Workflow.Interfaces;

namespace Andead.SmartHome.Workflow.Extensions
{
    public static class WorkflowStepExtensions
    {
        public static void SetNextSteps(this IStep step, ILifetimeScope scope, WorkflowStep[] steps)
        {
            IList<IStep> result = new List<IStep>();
            foreach (var workflowNextStep in steps)
            {
                var resolvedNextStep = scope.ResolveNamed<IStep>(workflowNextStep.WorkflowLogic.ClassName);
                resolvedNextStep.Id = workflowNextStep.Id;
                resolvedNextStep.Name = workflowNextStep.StepName;
                resolvedNextStep.SetNextSteps(scope, steps.SelectMany(x => x.NextSteps).Distinct().ToArray());

                result.Add(resolvedNextStep);
            }

            step.SetNextSteps(result);
        }
    }
}
