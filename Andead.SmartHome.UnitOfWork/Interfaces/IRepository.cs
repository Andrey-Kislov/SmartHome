using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Andead.SmartHome.UnitOfWork.Interfaces
{
    public interface IRepository : IDisposable
    {
        EntityEntry<TEntity> Add<TEntity>(TEntity item) where TEntity : Entity;
        void Commit();
        IQueryBuilder<TEntity> Get<TEntity>() where TEntity : Entity;
        void Attach<TEntity>(TEntity entity) where TEntity : Entity;
    }
}
