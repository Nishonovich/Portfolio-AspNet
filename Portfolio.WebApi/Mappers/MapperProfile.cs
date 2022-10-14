using AutoMapper;
using Portfolio.WebApi.Models;
using Portfolio.WebApi.ViewModels.Users;

namespace Portfolio.WebApi.Mappers
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserCreateViewModel>().ReverseMap();
        }
    }
}
