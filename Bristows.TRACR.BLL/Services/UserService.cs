using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Bristows.TRACR.DAL.Repositories;
using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.Model.Contexts;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.Model.Models.Entities;
using System.Linq.Expressions;
using Bristows.TRACR.Model.Models.POCOs;
using Bristows.TRACR.Model.Models.ValidationAttributes;

namespace Bristows.TRACR.BLL.Services
{
    public sealed class UserService : IUserService
    {
        private readonly UnitOfWork<TRACRContext> TRACRUnitOfWork;
        private readonly ITraineeRepository traineeRepository;
        private readonly IPeopleRepository peopleRepository;
        private readonly IAdminRepository adminRepository;
        #region -------------------------------------------------> [dev only]
        private User storedUser = new(); 
        #endregion ------------------------------------------------------------------------------------------------------------------------------------------------------
        public UserService(UnitOfWork<TRACRContext> TRACRUnitOfWork,
        IPeopleRepository peopleRepository, IAdminRepository adminRepository,ITraineeRepository traineeRepository)
        {
            this.TRACRUnitOfWork = TRACRUnitOfWork ?? throw new ArgumentNullException(nameof(TRACRUnitOfWork));
            this.peopleRepository = peopleRepository ?? throw new ArgumentNullException(nameof(peopleRepository));
            this.adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            this.traineeRepository = traineeRepository ?? throw new ArgumentNullException(nameof(traineeRepository));
        }
        #region -------------------------------------------------> [dev only]
        public void RegisterUser(User user, CancellationToken ct) => storedUser = user; 
        public async Task<User?> GetUserAsync([EmailAddress] string email, CancellationToken ct) => await Task.Run(() => storedUser?.Email == email ? storedUser : null);
        #endregion ------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<PeopleFinderUser?> GetByDomainAsync([ValidWinUser] string domainUsername, CancellationToken ct) => await peopleRepository.FirstOrDefaultAsync(u => u.WinUser == domainUsername);
        public async Task<PeopleFinderUser?> GetPFUserAsync([ValidPfid] int pfid, CancellationToken ct) => await peopleRepository.FirstOrDefaultAsync(u => u.PFID == pfid);
        public async Task<IEnumerable<PeopleFinderUser?>> GetPFUsersAsync(CancellationToken ct) => await peopleRepository.GetAllAsync();
        public async Task<IEnumerable<Trainee?>> GetTraineesAsync(CancellationToken ct) => await traineeRepository.GetAllAsync();
        public async Task<IEnumerable<Trainee?>> GetTrainees(CancellationToken ct) => await traineeRepository.GetAllAsync();
        public async Task<Trainee?> GetTraineeByPfidAsync([ValidPfid] int pfid, CancellationToken ct) => await traineeRepository.FirstOrDefaultAsync(u => u.TRAINEE_PFID == pfid.ToString());
        public async Task<IEnumerable<Trainee?>> TraineesByReviewerAsync([ValidPfid] int pfid, CancellationToken ct) => await traineeRepository.GetManyAsync(u => u.REVIEWER_PFID == pfid.ToString());
        public async Task<IEnumerable<PeopleFinderUser?>> UsersByTraineeAsync([ValidPfid] int pfid, CancellationToken ct) => await peopleRepository.GetManyAsync(u => u.PFID == pfid);
        public async Task<bool> IsAdminPfidAsync([ValidPfid] int pfid, CancellationToken ct) => await adminRepository.AnyAsync(u => u.ROLE == pfid.ToString());
        public async Task<bool> IsReviewerPfidAsync([ValidPfid] int pfid, CancellationToken ct) => await traineeRepository.AnyAsync(u => u.REVIEWER_PFID == pfid.ToString());
        public async Task<bool> IsTraineePfidAsync([ValidPfid] int pfid, CancellationToken ct) => await traineeRepository.AnyAsync(u => u.TRAINEE_PFID == pfid.ToString());
        public async Task<string?> GetRoleByPfidAsync([ValidPfid] int pfid, CancellationToken ct)
        {
            return await IsAdminPfidAsync(pfid, ct) ? "Admin" : 
            await IsReviewerPfidAsync(pfid, ct) ? "Reviewer" :
            await IsTraineePfidAsync(pfid, ct) ? "Trainee" : null;
        }
        public async Task<IEnumerable<PeopleFinderUser?>> GetReviewersAsync(CancellationToken ct)
        {
            IEnumerable<PeopleFinderUser?> reviewers = new List<PeopleFinderUser?>();
            IEnumerable<PeopleFinderUser?> users = await peopleRepository.GetAllAsync();
            foreach (PeopleFinderUser? usr in users)
            {
                 string? role = await GetRoleByPfidAsync(usr?.PFID ?? 0, ct);
                 if (role == "Reviewer" && role != null) ((List<PeopleFinderUser?>)reviewers).Add(usr);
            }
            return reviewers;
        }
        public async Task<PeopleFinderUser?> ReviewerByTraineeAsync([ValidPfid] int pfid, CancellationToken ct)
        {
            Trainee? reviewerPartial = await traineeRepository.FirstOrDefaultAsync(trn => trn.TRAINEE_PFID == pfid.ToString());
            PeopleFinderUser? reviewer = await peopleRepository.FirstOrDefaultAsync(usr => usr.PFID.ToString() == reviewerPartial!.REVIEWER_PFID);
            return reviewer;
        }
        public void DeleteTrainee(Expression<Func<Trainee, bool>> predicate, bool commit=true)
        {
            traineeRepository.Delete(predicate);
            TRACRUnitOfWork.Commit();
        }
        public void DeleteTrainee(Trainee trainee, bool commit=true)
        {
            traineeRepository.Delete(trainee);
            TRACRUnitOfWork.Commit();
        }
        public Trainee? SetPair(Trainee trainee, bool commit=true)
        {
            traineeRepository.Update(trainee);
            TRACRUnitOfWork.Commit();
            return trainee;
        }
        public Trainee? AssignTrainees(Trainee trainee, bool commit=true)
        {
            traineeRepository.Add(trainee);
            TRACRUnitOfWork.Commit();
            return trainee;
        }
        public Trainee? UpdateTrainee(Trainee trainee, bool commit=true)
        {
            traineeRepository.Update(trainee);
            TRACRUnitOfWork.Commit();
            return trainee;
        }
    }
}