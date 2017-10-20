namespace Todo.Dal.Interfaces
{
    public interface ITodoRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {

    }
}
