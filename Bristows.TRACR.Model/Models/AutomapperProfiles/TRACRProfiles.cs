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
            ).ForMember((dest) => dest.TraineeId, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.TraineePfid, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.ReviewerPfid, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.Active, (opt) => opt.Ignore());
        CreateMap<AddModifyEmpReq, Employee>()
            .ForMember((dest) => dest.Id, (opt) => opt.NullSubstitute(System.Guid.NewGuid())
            ).ForMember((dest) => dest.EntryCreated, (opt) => opt.NullSubstitute(DateTime.Now)
            ).ForMember((dest) => dest.LastUpdated, (opt) => opt.MapFrom(_ => DateTime.Now)
            ).ForMember((dest) => dest.Show, (opt) => opt.NullSubstitute(true));
        CreateMap<AddModifyTraineeReq, Trainee>()
            .ForMember((dest) => dest.LocalId, (opt) => opt.MapFrom((_, dest) => dest.LocalId ?? System.Guid.NewGuid())
            ).ForMember((dest) => dest.TraineeId, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.OtherPfid, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.EntryCreated, (opt) => opt.MapFrom((_, dest) => dest.EntryCreated ?? DateTime.Now)
            ).ForMember((dest) => dest.LastUpdated, (opt) => opt.MapFrom(_ => DateTime.Now)
            ).ForMember((dest) => dest.Active, (opt) => opt.MapFrom((src) => src.Active)
            ).ForMember((dest) => dest.Show, (opt) => opt.MapFrom((src) => src.Show));
        CreateMap<Trainee, TraineeViewModel>()
           .ForMember((dest) => dest.Photo, (opt) => opt.Ignore()
           ).ForMember((dest) => dest.FirstName, (opt) => opt.Ignore()
           ).ForMember((dest) => dest.LastName, (opt) => opt.Ignore()
           ).ForMember((dest) => dest.Email, (opt) => opt.Ignore()
           ).ForMember((dest) => dest.Telephone, (opt) => opt.Ignore());
        CreateMap<AddModifyDiaryReq, Diary>()
            .ForMember((dest) => dest.SignedOffTimestamp, (opt) =>
            opt.MapFrom((src, dest) => (src.SignOffSubmitted == true) ?
                dest.SignedOffTimestamp = DateTime.Now : null)
            ).ForMember((dest) => dest.DiaryId, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.Timestamp, (opt) => opt.MapFrom(_ => DateTime.Now)
            ).ForMember((dest) => dest.LocalId, (opt) => opt.MapFrom((_, dest) => dest.LocalId ?? System.Guid.NewGuid()));
        CreateMap<SkillDTO, Skill>()
            .ForMember((dest) => dest.SkillId, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.LocalId, (opt) => opt.MapFrom((_, dest) => dest.LocalId ?? System.Guid.NewGuid()));
        CreateMap<AddModifyDiaryTaskReq, DiaryTask>()
            .ForMember((dest) => dest.Skills, (opt) => opt.MapFrom((src) => src.Skills)
            ).ForMember((dest) => dest.DiaryId, (opt) => opt.MapFrom((src) => src.DiaryId)
            ).ForMember((dest) => dest.LocalId, (opt) => opt.MapFrom((_, dest) => dest.LocalId ?? System.Guid.NewGuid())
            ).ForMember((dest) => dest.DiaryTaskId, (opt) => opt.Ignore()
            ).ForMember((dest) => dest.Timestamp, (opt) => opt.MapFrom(_ => DateTime.Now));
    }
}