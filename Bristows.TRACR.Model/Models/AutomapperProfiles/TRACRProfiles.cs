using AutoMapper;
using Bristows.TRACR.Model.DTOs;
using Bristows.TRACR.Model.Models.Entities;
using Bristows.TRACR.Model.Models.Entities.Employees;
using Bristows.TRACR.Model.Models.ViewModels;

namespace Bristows.TRACR.Model.Models.AutomapperProfiles;
public class TRACRProfiles : Profile
{
    public TRACRProfiles()
    {
        CreateMap<PeopleFinderUser, UserViewModel>().ForMember((dest) => dest.Role, (opt) => opt.Ignore());
        CreateMap<Diary, DiaryViewModel>();
        CreateMap<Employee, EmployeeViewModel>();
        CreateMap<DiaryTask, DiaryTaskViewModel>();
        CreateMap<PeopleFinderUser, TraineeViewModel>()
            .ForMember((dest) => dest.FirstName, (opt) => opt.MapFrom((src) => src.FirstName)
            ).ForMember((dest) => dest.LastName, (opt) => opt.MapFrom((src) => src.LastName)
            ).ForMember((dest) => dest.Email, (opt) => opt.MapFrom((src) => src.Email)
            ).ForMember((dest) => dest.Telephone, (opt) => opt.MapFrom((src) => src.Telephone)
            ).ForMember((dest) => dest.Photo, (opt) => opt.MapFrom((src) => src.Photo)
            ).ForMember((dest) => dest.TRAINEE_PFID, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.REVIEWER_PFID, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.ACTIVE, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.SHOW, (opt) => opt.Ignore());
        CreateMap<AddModifyEmpReq, Employee>()
            .ForMember((dest) => dest.Id, (opt) => opt.NullSubstitute(System.Guid.NewGuid())
            ).ForMember((dest) => dest.EntryCreated, (opt) => opt.NullSubstitute(DateTime.Now)
            ).ForMember((dest) => dest.LastUpdated, (opt) => opt.MapFrom(_ => DateTime.Now)
            ).ForMember((dest) => dest.Show, (opt) => opt.NullSubstitute(true));
        CreateMap<AddModifyTraineeReq, Trainee>()
            .ForMember((dest) => dest.TRAINEE_ID, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.OTHER_PFID, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.TRAINEE_PFID, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.ACTIVE, (opt) => opt.MapFrom((src) => src.ACTIVE)
            ).ForMember((dest) => dest.SHOW, (opt) => opt.MapFrom((src) => src.SHOW));
        CreateMap<Trainee, TraineeViewModel>()
           .ForMember((dest) => dest.TRAINEE_PFID , (opt) => opt.Ignore());
        CreateMap<AddModifyDiaryReq, Diary>()
            .ForMember((dest) => dest.SIGNED_OFF_TIMESTAMP, 
            (opt) => opt.MapFrom((src, dest) => ((src.SIGN_OFF_SUBMITTED == "true") && 
            (dest.SIGNED_OFF_TIMESTAMP == null)) ? dest.SIGNED_OFF_TIMESTAMP = DateTime.Now : null)
            ).ForMember((dest) => dest.DIARY_ID, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.TIMESTAMP, (opt) => opt.MapFrom(_ => DateTime.Now)
            ).ForMember((dest) => dest.DIARY_ID, (opt) => opt.Ignore());
        // CreateMap<SkillDTO, Skill>()
        //     .ForMember((dest) => dest.SkillId,(opt) => opt.Ignore()
        //     ).ForMember((dest) => dest.LocalId, (opt) => opt.MapFrom((_, dest) => dest.LocalId ?? System.Guid.NewGuid()));
        CreateMap<AddModifyDiaryTaskReq, DiaryTask>()
            .ForMember((dest) => dest.SKILLS, (opt) => opt.MapFrom((src) => src.SKILLS)
            ).ForMember((dest) => dest.DIARY_ID, (opt) => opt.MapFrom((src) => src.DIARY_ID)
            ).ForMember((dest) => dest.DIARY_TASK_ID, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.TIMESTAMP, (opt) => opt.MapFrom(_ => DateTime.Now));
    }
}