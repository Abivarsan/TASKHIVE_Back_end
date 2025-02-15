using AutoMapper;
using TASKHIVE.DTO.Category;
using TASKHIVE.DTO.Role;
using TASKHIVE.DTO.Users;
using TASKHIVE.Model;

namespace TASKHIVE.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<Role, CreateLabelDto>().ReverseMap();
            CreateMap<Role, UpdateLabelDto>().ReverseMap();
            CreateMap<Role, GetLabelByIdDto>().ReverseMap();
            CreateMap<Role, GetAllLabelDto>().ReverseMap();

            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, GetUserByIdDto>().ReverseMap();
            CreateMap<User, GetAllUserDto>().ReverseMap();

            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();
            CreateMap<Category, GetAllCategoryDto>().ReverseMap();

        }
        
    }
}
