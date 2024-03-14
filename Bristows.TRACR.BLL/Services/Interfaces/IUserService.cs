using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Bristows.TRACR.Model.Models.Entities;
using Bristows.TRACR.Model.Models.POCOs;
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
        public Task<PeopleFinderUser?> GetByDomainAsync(string domainUsername, CancellationToken ct);
        public Task<IEnumerable<Trainee?>> GetTraineesAsync(CancellationToken ct);
        public Task<IEnumerable<PeopleFinderUser?>> GetPFUsersAsync(CancellationToken ct);
        public Task<IEnumerable<PeopleFinderUser?>> GetReviewersAsync(CancellationToken ct);
        public Task<IEnumerable<Trainee?>> TraineesByReviewerAsync([ValidPfid] int pfid, CancellationToken ct);
        public Task<PeopleFinderUser?> ReviewerByTraineeAsync([ValidPfid] int pfid, CancellationToken ct);
        public Task<PeopleFinderUser?> GetPFUserAsync([ValidPfid] int pfid, CancellationToken ct);
        public Task<Trainee?> GetTraineeByPfidAsync([ValidPfid] int pfid, CancellationToken ct);
        public Task<bool> IsAdminPfidAsync([ValidPfid] int pfid, CancellationToken ct);
        public Task<bool> IsReviewerPfidAsync([ValidPfid] int pfid, CancellationToken ct);
        public Task<bool> IsTraineePfidAsync([ValidPfid] int pfid, CancellationToken ct);
        public Task<string?> GetRoleByPfidAsync([ValidPfid] int pfid, CancellationToken ct);
        #region -----------------------------------------------------> [dev only]
        public void RegisterUser(User user, CancellationToken ct);
        public Task<User?> GetUserAsync([EmailAddress] string email, CancellationToken ct);
        #endregion --------------------------------------------------
    }
}