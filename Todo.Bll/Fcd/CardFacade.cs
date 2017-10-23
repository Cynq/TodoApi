using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
