using System.Collections.Generic;
using Todo.Common.Models;

namespace Todo.Bll.Interfaces.Facades
{
    public interface ICardFacade : IBaseFacade
    {
        IEnumerable<Card> Get();
    }
}
