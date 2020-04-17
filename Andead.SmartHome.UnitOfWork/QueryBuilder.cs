using System.Linq;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork
{
    class QueryBuilder<T> : IQueryBuilder<T> where T : Entity
    {
        protected IQueryable<T> Query { get; }

        public QueryBuilder(IQueryable<T> query)
        {
            Query = query;
        }

        public IQueryable<T> GetQuery()
        {
            return Query;
        }

        public IQueryBuilder<T> Condition(ISpecification<T> specification)
        {
            return new QueryBuilder<T>(Query.Where(specification.SatisfiedBy()));
        }

        public T FirstOrDefault()
        {
            return Query.FirstOrDefault();
        }

        public T[] ToArray()
        {
            return Query.ToArray();
        }
    }
}
