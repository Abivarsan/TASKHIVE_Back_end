using AutoMapper;
using TASKHIVE.DTO.Category;
using TASKHIVE.DTO.Role;
using TASKHIVE.DTO.Users;
using TASKHIVE.Model;
using TASKHIVE.DTO.Report;

namespace TASKHIVE.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<Role, CreateRoleDto>().ReverseMap();
            CreateMap<Role, UpdateRoleDto>().ReverseMap();
            CreateMap<Role, GetRoleByIdDto>().ReverseMap();
            CreateMap<Role, GetAllRoleDto>().ReverseMap();

            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, GetUserByIdDto>().ReverseMap();
            CreateMap<User, GetAllUserDto>().ReverseMap();

            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();
            CreateMap<Category, GetAllCategoryDto>().ReverseMap();

            CreateMap<Report, CreateReportDto>().ReverseMap();
            CreateMap<Report, UpdateReportDto>().ReverseMap();
            CreateMap<Report, GetReportByIdDto>().ReverseMap();
            CreateMap<Report, GetAllReportDto>().ReverseMap();

        }
        
    }
}
