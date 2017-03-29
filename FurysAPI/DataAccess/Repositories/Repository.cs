using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FurysAPI.DataAccess.DataContext;
using FurysAPI.DataAccess.Repositories.Interfaces;

namespace FurysAPI.DataAccess.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly FurysApiDbContext Context;
        private readonly DbSet<T> _entities;
        protected Repository(FurysApiDbContext context)
        {
            Context = context;
            _entities = Context.Set<T>();
        }

        public T Get(Guid id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entity)
        {
            _entities.AddRange(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _entities.RemoveRange(entity);
        }
    }
}