using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;

namespace Repository.Interfaces
{
    public interface IDbRepository<TEntityBase> where TEntityBase : BaseModel
    {
        int Count { get; }

        void Add(TEntityBase entity);
        void Delete(TEntityBase entity);
        void Edit(TEntityBase entity);
        IEnumerable<TEntityBase> GetAll();

        TEntityBase GetById(int id);
        TEntityBase GetById<TResult>(int id, Expression<Func<TEntityBase, TResult>> selector);
        IQueryable<TEntityBase> GetMany(Expression<Func<TEntityBase, bool>> predicate);
        IQueryable<TResult> GetMany<TResult>(Expression<Func<TEntityBase, TResult>> selector);
        IQueryable<TResult> GetMany<TResult>(Expression<Func<TEntityBase, TResult>> selector, Expression<Func<TEntityBase, bool>> predicate);
    }
}