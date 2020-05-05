using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork
{
    public class QueryBuilder<T> : IQueryBuilder<T> where T : Entity
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

        public IQueryBuilder<T> Distinct(IEqualityComparer<T> comparer = null)
        {
            return new QueryBuilder<T>((comparer == null) ? Query.Distinct() : Query.Distinct(comparer));
        }

        public IQueryBuilder<T> Include<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return new QueryBuilder<T>(QueryableExtensions.Include(Query, expression));
        }
    }
}
