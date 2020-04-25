using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork.Extensions
{
    public static class WorkflowExtensions
    {
        public static IQueryBuilder<Workflow> ByUserId(this IQueryBuilder<Workflow> repository, int userId, int deviceId)
        {
            return repository.Condition(new Specification<Workflow>(x => x.UserId == userId && x.DeviceId == deviceId && x.IsActive));
        }
    }
}
