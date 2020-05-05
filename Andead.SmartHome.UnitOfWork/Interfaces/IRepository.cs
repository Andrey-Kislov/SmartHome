using System;

namespace Andead.SmartHome.UnitOfWork.Interfaces
{
    public interface IRepository : IDisposable
    {
        void Add<TEntity>(TEntity item) where TEntity : Entity;
        void Commit();
        IQueryBuilder<TEntity> Get<TEntity>() where TEntity : Entity;
        void Attach<TEntity>(TEntity entity) where TEntity : Entity;
    }
}
