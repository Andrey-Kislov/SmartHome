using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Andead.SmartHome.UnitOfWork.Interfaces
{
    public interface IQueryBuilder<T> where T : Entity
    {
        IQueryBuilder<T> Condition(ISpecification<T> specification);
        T FirstOrDefault();
        T[] ToArray();
        IQueryBuilder<T> Distinct(IEqualityComparer<T> comparer = null);
        IQueryBuilder<T> Include<TProperty>(Expression<Func<T, TProperty>> expression);
    }
}
