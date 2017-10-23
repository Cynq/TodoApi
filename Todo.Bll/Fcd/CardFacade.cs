using System.Collections.Generic;
using AutoMapper;
using Todo.Bll.Interfaces.Facades;
using Todo.Common.Models;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Fcd
{
    public class CardFacade : BaseFacade, ICardFacade
    {
        public CardFacade(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public IEnumerable<Card> Get()
        {
            return UnitOfWork.CardRepository.Get();
        }
    }
}
