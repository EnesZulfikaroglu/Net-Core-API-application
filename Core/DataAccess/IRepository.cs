using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
