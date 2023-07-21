using System.Linq.Expressions;
using Bristows.TRACR.Model.Models.Entities;
using Bristows.TRACR.Model.Models.ValidationAttributes;

namespace Bristows.TRACR.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Trainee? SetPair(Trainee trainee, bool commit=true);
        public Trainee? AssignTrainees(Trainee trainee, bool commit=true);
        public void DeleteTrainee(Expression<Func<Trainee, bool>> predicate, bool commit=true);
        public void DeleteTrainee(Trainee trainee, bool commit=true);
        public Trainee? UpdateTrainee(Trainee trainee, bool commit=true);
        public Task<PeopleFinderUser?> GetByDomainAsync(string domainUsername);
        public Task<IEnumerable<Trainee?>> GetTraineesAsync();
        public Task<Trainee?> GetTraineeByPfidAsync([ValidPfid] int pfid);
        public Task<IEnumerable<Trainee?>> TraineesByReviewerAsync([ValidPfid] int pfid);
        public Task<IEnumerable<PeopleFinderUser?>> GetPFUsersAsync();
        public Task<PeopleFinderUser?> GetPFUserAsync([ValidPfid] int pfid);
        public Task<IEnumerable<PeopleFinderUser?>> GetReviewersAsync();
        public Task<bool> IsAdminPfidAsync([ValidPfid] int pfid);
        public Task<bool> IsReviewerPfidAsync([ValidPfid] int pfid);
        public Task<bool> IsTraineePfidAsync([ValidPfid] int pfid);
        public Task<string?> GetRoleByPfidAsync([ValidPfid] int pfid);
    }
}