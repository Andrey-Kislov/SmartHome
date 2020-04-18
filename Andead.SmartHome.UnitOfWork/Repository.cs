using System;
using System.Reflection;
using Andead.SmartHome.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public virtual new void Add<TEntity>(TEntity item) where TEntity : Entity
        {
            Set<TEntity>().Add(item);
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
            options.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.HasDefaultSchema("smart");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
