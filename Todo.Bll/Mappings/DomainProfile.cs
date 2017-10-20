using AutoMapper;
using Todo.Common.Models;
using Todo.Common.ViewModels;

namespace Todo.Bll.Mappings
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
        }
    }
}
