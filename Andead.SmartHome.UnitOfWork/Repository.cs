using System;
using System.Reflection;
using Andead.SmartHome.UnitOfWork.Extensions;
using Andead.SmartHome.UnitOfWork.Interfaces;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Andead.SmartHome.UnitOfWork
{
    public class Repository : DbContext, IRepository, IDisposable
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
            Database.OpenConnection();
            Database.EnsureCreated();
        }

        public virtual new EntityEntry<TEntity> Add<TEntity>(TEntity item) where TEntity : Entity
        {
            return Set<TEntity>().Add(item);
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Can't save entity: {ex.Message}");
            }
        }

        public IQueryBuilder<TEntity> Get<TEntity>() where TEntity : Entity
        {
            return new QueryBuilder<TEntity>(Set<TEntity>());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
            options.UseExceptionProcessor();
            options.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.HasDefaultSchema("smart");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.UseSerialColumns();
            modelBuilder.Seed();
        }

        public virtual new void Attach<TEntity>(TEntity entity) where TEntity : Entity
        {
            bool autoDetectChangesEnabled = ChangeTracker.AutoDetectChangesEnabled;
            ChangeTracker.AutoDetectChangesEnabled = false;
            Set<TEntity>().Attach(entity);
            ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;
        }
    }
}
