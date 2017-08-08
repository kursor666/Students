using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Domain;
using Repository.Interfaces;

namespace Repository
{
    public class DbFakeRepository<TEntityBase> : IDbRepository<TEntityBase> where TEntityBase : BaseModel
    {
        protected UniversityContext DbProvider;

        protected DbSet<TEntityBase> DbSet;

        public DbFakeRepository(UniversityContext context)
        {
            DbProvider = context;
            DbSet = DbProvider.Set<TEntityBase>();
        }

        public void Edit(TEntityBase entity)
        {
            TEntityBase editingEntity = GetById(entity.Id);
            Mapper.Initialize(cfg => cfg.CreateMap<TEntityBase, TEntityBase>());
            Mapper.Map(entity, editingEntity);
            DbProvider.Entry(editingEntity).State = EntityState.Modified;
        }

        public void Add(TEntityBase entity)
        {
            DbSet.Add(entity);
        }

        public TEntityBase GetById(int id)
        {
            var findResult = DbSet.Find(id);
            if (findResult == null) return null;
            return findResult.IsDeleted ? null : findResult;
        }

        public TEntityBase GetById<TResult>(int id, Expression<Func<TEntityBase, TResult>> selector)
        {
            return DbSet
                .Where(entity => entity.Id == id)
                .Where(entity => !entity.IsDeleted)
                .Include(selector)
                .FirstOrDefault();
        }

        public IEnumerable<TEntityBase> GetAll()
        {
            return DbSet.AsQueryable().Where(tEntity => !tEntity.IsDeleted);
        }

        public IQueryable<TResult> GetMany<TResult>(Expression<Func<TEntityBase, TResult>> selector)
        {
            return GetAll().AsQueryable().Select(selector);
        }

        public IQueryable<TEntityBase> GetMany(Expression<Func<TEntityBase, bool>> predicate)
        {
            return GetAll().AsQueryable().Where(predicate);
        }

        public IQueryable<TResult> GetMany<TResult>(Expression<Func<TEntityBase, TResult>> selector,
            Expression<Func<TEntityBase, bool>> predicate)
        {
            return GetAll()
                .AsQueryable()
                .Where(predicate)
                .Select(selector);
        }

        public int Count => DbSet.Count(tEntity => tEntity.IsDeleted == false);

        public void Delete(TEntityBase entity)
        {
            TEntityBase removingEntity = GetById(entity.Id);
            removingEntity.IsDeleted = true;
        }

    }
}