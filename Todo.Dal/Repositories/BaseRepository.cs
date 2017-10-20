using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Dal.Interfaces;

namespace Todo.Dal.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public TodoContext Context { get; set; }

        public BaseRepository(TodoContext context)
        {
            Context = context;
        }

        public virtual DbSet<TEntity> DbSet { get; set; }

        public virtual Task<TEntity> GetById(object id) => Context.FindAsync<TEntity>(id);

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy?.Invoke(query).ToList() ?? query.ToList();
        }

        public virtual void Delete(TEntity item) => Context.Remove(item);

        public virtual Task Add(TEntity item) => Context.AddAsync(item);

        public virtual void Update(TEntity item) => Context.Update(item);
    }
}