using Todo.Common.Models;
using Todo.Dal.Interfaces;

namespace Todo.Dal.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository<Card>
    {
        public CardRepository(TodoContext context) : base(context)
        {
        }
    }
}
