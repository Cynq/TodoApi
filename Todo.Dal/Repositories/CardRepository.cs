using Todo.Common.Models;
using Todo.Dal.Interfaces;

namespace Todo.Dal.Repositories
{
    public class CardRepository : BaseRepository<TodoCard>, ICardRepository<TodoCard>
    {
        public CardRepository(TodoContext context) : base(context)
        {
        }
    }
}
