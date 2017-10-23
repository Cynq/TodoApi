namespace Todo.Dal.Interfaces
{
    public interface ICardRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        
    }
}
