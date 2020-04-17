using System;
using System.Linq.Expressions;

namespace Andead.SmartHome.UnitOfWork.Interfaces
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> SatisfiedBy();
    }
}
