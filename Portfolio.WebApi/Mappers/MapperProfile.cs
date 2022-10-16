using AutoMapper;
using Portfolio.WebApi.Models;
using Portfolio.WebApi.ViewModels.Projects;
using Portfolio.WebApi.ViewModels.Users;

namespace Portfolio.WebApi.Mappers
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserCreateViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<Project, ProjectCreateViewModel>().ReverseMap();
            CreateMap<Project, ProjectViewModel>().ReverseMap();
        }
    }
}
