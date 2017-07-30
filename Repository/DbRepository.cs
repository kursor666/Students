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
    public class DbRepository<TEntityBase> : IDbRepository<TEntityBase> where TEntityBase : BaseModel
    {
        protected UniversityContext DbProvider;

        protected DbSet<TEntityBase> DbSet;

        public DbRepository(UniversityContext context)
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

        #region GetById

        public TEntityBase GetById(int id)
        {
            return DbSet.Find(id);
        }

        public TResult GetById<TResult>(int id, Expression<Func<TEntityBase, TResult>> selector)
        {
            return DbSet
                .Where(entity => entity.Id == id)
                .Select(selector)
                .FirstOrDefault();
        }

        #endregion

        #region GetAll

        public IEnumerable<TEntityBase> GetAll()
        {
            return DbSet.ToArray();
        }

        #endregion

        #region GetMany

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

        #endregion



        #region Count

        public int Count => DbSet.Count();

        #endregion

        #region Delete

        public void Delete(TEntityBase entity)
        {
            TEntityBase removingEntity = GetById(entity.Id);
            if (removingEntity == null) return;
            DbSet.Remove(entity);
        }



        #endregion

    }
}
