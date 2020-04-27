using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork.Extensions
{
    public static class WorkflowStepExtensions
    {
        public static IQueryBuilder<WorkflowStep> ByWorkflowId(this IQueryBuilder<WorkflowStep> repository, long workflowId)
        {
            return repository.Condition(new Specification<WorkflowStep>(x => x.WorkflowId == workflowId));
        }
    }
}
