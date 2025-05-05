using AutoMapper;
using TASKHIVE.DTO.Meeting;
using TASKHIVE.DTO.Project;
using TASKHIVE.DTO.Report;
using TASKHIVE.DTO.Role;
using TASKHIVE.DTO.TimeLog;
using TASKHIVE.DTO.User;
using TASKHIVE.DTO.Work;
using TASKHIVE.DTO.WorkSpace;
using TASKHIVE.Model;

namespace TASKHIVE.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, GetRoleByIdDto>().ReverseMap();
            CreateMap<Role, GetAllRoleDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, GetAllUserDto>().ReverseMap();
            CreateMap<User, GetUserByIdDto>().ReverseMap();
            CreateMap<User,LoginDto>().ReverseMap();
            CreateMap<User, LoginWithGoogleDto>().ReverseMap();    

            CreateMap<Meeting, MeetingDto>().ReverseMap();
            CreateMap<Meeting, GetMeetingByIdDto>().ReverseMap();
            CreateMap<Meeting, GetAllMeetingDto>().ReverseMap();

            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, GetProjectByIdDto>().ReverseMap();
            CreateMap<Project,  GetAllProjectDto>().ReverseMap();

            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<Report, GetReportByIdDto>().ReverseMap();
            CreateMap<Report,  GetAllReportDto>().ReverseMap();

            CreateMap<TimeLog, TimeLogDto>().ReverseMap();
            CreateMap<TimeLog, GetTimeLogByIdDto>().ReverseMap();
            CreateMap<TimeLog, GetAllTimeLogDto>().ReverseMap();

            CreateMap<Work, WorkDto>().ReverseMap();
            CreateMap<Work, GetWorkByIdDto>().ReverseMap();
            CreateMap<Work, GetAllWorkDto>().ReverseMap();

            CreateMap<WorkSpace, WorkSpaceDto>().ReverseMap();
            CreateMap<WorkSpace, GetWorkSpaceByIdDto>().ReverseMap();
            CreateMap<WorkSpace, GetAllWorkSpaceDto>().ReverseMap();
        }
        
    }
}
