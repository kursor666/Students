using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Domain;

namespace Repository
{
    public class DbRepository<TEntityBase> where TEntityBase : BaseModel
    {
        protected UniversityContext DbProvider = UniversityDbProvider.GetContext();

        protected DbSet<TEntityBase> DbSet;

        public DbRepository()
        {
            DbSet = DbProvider.Set<TEntityBase>();
        }

        public void Add(TEntityBase entity)
        {
            DbSet.Add(entity);
        }

        public TEntityBase GetById(int id)
        {
            return DbSet.Find(id);
        }

        public TEntityBase GetByPredicate(Expression<Func<TEntityBase, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public TResult GetById<TResult>(int id, Expression<Func<TEntityBase, TResult>> selector)
        {
            return DbSet
                .Where(entity => entity.Id == id)
                .Select(selector)
                .FirstOrDefault();
        }

        public IEnumerable<TEntityBase> GetAll()
        {
            return DbSet.ToArray();
        }

        public IQueryable<TResult> GetMany<TResult>(Expression<Func<TEntityBase, TResult>> selector)
        {
            return DbSet.Select(selector);
        }

        public IQueryable<TEntityBase> GetMany(Expression<Func<TEntityBase, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<TResult> GetMany<TResult>(Expression<Func<TEntityBase, TResult>> selector,
            Expression<Func<TEntityBase, bool>> predicate)
        {
            return DbSet
                .Where(predicate)
                .Select(selector);
        }

        public void Commit()
        {
            DbProvider.SaveChanges();
        }

        public void Edit(TEntityBase entity)
        {
            TEntityBase editingEntity = GetById(entity.Id);
            editingEntity = entity;
            DbProvider.Entry(editingEntity).State = EntityState.Modified;
        }

        public int Count => DbSet.Count();

        public void Delete(TEntityBase entity)
        {
            DbSet.Remove(entity);
        }
    }
}
