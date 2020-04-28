using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork.Extensions
{
    public static class WorkflowActionExtensions
    {
        public static IQueryBuilder<WorkflowAction> ByWorkflowId(this IQueryBuilder<WorkflowAction> repository, int workflowId)
        {
            return repository.Condition(new Specification<WorkflowAction>(x => x.WorkflowId == workflowId));
        }
    }
}
