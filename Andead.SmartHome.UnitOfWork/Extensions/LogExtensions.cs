using Andead.SmartHome.UnitOfWork.Entities;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork.Extensions
{
    public static class LogExtensions
    {
        public static IQueryBuilder<Log> ById(this IQueryBuilder<Log> repository, long id)
        {
            return repository.Condition(new Specification<Log>(x => x.Id == id));
        }
    }
}
