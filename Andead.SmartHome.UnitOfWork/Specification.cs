using System;
using System.Linq.Expressions;
using Andead.SmartHome.UnitOfWork.Interfaces;

namespace Andead.SmartHome.UnitOfWork
{
    public sealed class Specification<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        private readonly Expression<Func<TEntity, bool>> _matchingCriteria;

        public Specification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == null)
            {
                throw new ArgumentNullException("matchingCriteria");
            }

            _matchingCriteria = matchingCriteria;
        }

        public Expression<Func<TEntity, bool>> SatisfiedBy()
        {
            return _matchingCriteria;
        }
    }
}
