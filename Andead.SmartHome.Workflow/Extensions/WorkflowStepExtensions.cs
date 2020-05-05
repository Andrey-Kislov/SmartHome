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
        public static void SetNextSteps(this IStep step, ILifetimeScope scope, WorkflowStep[] steps, WorkflowNextStep[] nextSteps)
        {
            var nextStepIds = nextSteps.Where(x => x.PreviousStepId == step.Id).Select(x => x.NextStepId);
            if (nextStepIds.Count() == 0)
                return;

            var workflowNextSteps = steps.Where(x => nextStepIds.Contains(x.Id));

            IList<IStep> result = new List<IStep>();
            foreach (var workflowNextStep in workflowNextSteps)
            {
                var resolvedNextStep = scope.ResolveNamed<IStep>(workflowNextStep.WorkflowLogic.ClassName);
                resolvedNextStep.Id = workflowNextStep.Id;
                resolvedNextStep.Name = workflowNextStep.StepName;
                resolvedNextStep.SetNextSteps(scope, steps, nextSteps);

                result.Add(resolvedNextStep);
            }

            step.SetNextSteps(result);
        }
    }
}
