using System.Linq;
using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork.Extensions
{
    public static class WorkflowNextStepExtensions
    {
        public static IQueryBuilder<WorkflowNextStep> ByStepId(this IQueryBuilder<WorkflowNextStep> repository, long stepId)
        {
            return repository.Condition(new Specification<WorkflowNextStep>(x => x.PreviousStepId == stepId));
        }

        public static IQueryBuilder<WorkflowNextStep> ByStepIds(this IQueryBuilder<WorkflowNextStep> repository, long[] stepIds)
        {
            return repository.Condition(new Specification<WorkflowNextStep>(x => stepIds.Contains(x.PreviousStepId)));
        }
    }
}
