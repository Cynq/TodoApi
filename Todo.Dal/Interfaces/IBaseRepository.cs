using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Todo.Dal.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> DbSet { get; set; }

        Task<TEntity> GetById(object id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        void Delete(TEntity item);
        Task Add(TEntity item);
        void Update(TEntity item);
    }
}