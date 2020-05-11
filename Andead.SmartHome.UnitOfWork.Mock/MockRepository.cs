using System;
using System.Reflection;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Andead.SmartHome.UnitOfWork.Mock
{
    public class MockRepository : DbContext, IRepository, IDisposable
    {
        public MockRepository()
        {
            Database.OpenConnection();
            Database.EnsureCreated();
        }

        public virtual new EntityEntry<TEntity> Add<TEntity>(TEntity item) where TEntity : Entity
        {
            return Set<TEntity>().Add(item);
        }

        public void Commit()
        {
            SaveChanges();
        }

        public IQueryBuilder<TEntity> Get<TEntity>() where TEntity : Entity
        {
            return new QueryBuilder<TEntity>(Set<TEntity>());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=InMemorySample;Mode=Memory;Cache=Shared;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(IRepository)));
        }

        void IRepository.Attach<TEntity>(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
