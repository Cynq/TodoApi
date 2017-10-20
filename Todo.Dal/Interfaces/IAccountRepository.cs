namespace Todo.Dal.Interfaces
{
    public interface IAccountRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        
    }
}
