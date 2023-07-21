using Bristows.TRACR.DAL.Repositories;
using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.Model.Contexts;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.Model.Models.Entities;
using System.Linq.Expressions;
using Bristows.TRACR.Model.Models.ValidationAttributes;

namespace Bristows.TRACR.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IAdminRepository adminRepository;
        private readonly IPeopleRepository peopleRepository;
        private readonly ITraineeRepository traineeRepository;
        private readonly UnitOfWork<TRACRContext> TRACRUnitOfWork;

        public UserService(IPeopleRepository peopleRepository,
        UnitOfWork<TRACRContext> TRACRUnitOfWork, IAdminRepository adminRepository,
        ITraineeRepository traineeRepository)
        {
            this.peopleRepository = peopleRepository ?? throw new ArgumentNullException(nameof(peopleRepository));
            this.adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
            this.traineeRepository = traineeRepository ?? throw new ArgumentNullException(nameof(traineeRepository));
            this.TRACRUnitOfWork = TRACRUnitOfWork ?? throw new ArgumentNullException(nameof(TRACRUnitOfWork));
        }
        public async Task<PeopleFinderUser?> GetByDomainAsync([ValidWinUser] string domainUsername) => await peopleRepository.FirstOrDefaultAsync(u => u.WinUser == domainUsername);
        public async Task<PeopleFinderUser?> GetPFUserAsync([ValidPfid] int pfid) => await peopleRepository.FirstOrDefaultAsync(u => u.PFID == pfid);
        public async Task<IEnumerable<PeopleFinderUser?>> GetPFUsersAsync() => await peopleRepository.GetAllAsync();
        public async Task<IEnumerable<Trainee?>> GetTraineesAsync() => await traineeRepository.GetAllAsync();
        public async Task<IEnumerable<Trainee?>> GetTrainees() => await traineeRepository.GetAllAsync();
        public async Task<Trainee?> GetTraineeByPfidAsync([ValidPfid] int pfid) => await traineeRepository.FirstOrDefaultAsync(u => u.TRAINEE_PFID == pfid.ToString());
        public async Task<IEnumerable<Trainee?>> TraineesByReviewerAsync([ValidPfid] int pfid) => await traineeRepository.GetManyAsync(u => u.REVIEWER_PFID == pfid.ToString());
        public async Task<IEnumerable<PeopleFinderUser?>> UsersByTraineeAsync([ValidPfid] int pfid) => await peopleRepository.GetManyAsync(u => u.PFID == pfid);
        public async Task<bool> IsAdminPfidAsync([ValidPfid] int pfid) => await adminRepository.AnyAsync(u => u.ROLE == pfid.ToString());
        public async Task<bool> IsReviewerPfidAsync([ValidPfid] int pfid) => await traineeRepository.AnyAsync(u => u.REVIEWER_PFID == pfid.ToString());
        public async Task<bool> IsTraineePfidAsync([ValidPfid] int pfid) => await traineeRepository.AnyAsync(u => u.TRAINEE_PFID == pfid.ToString());
        public async Task<string?> GetRoleByPfidAsync([ValidPfid] int pfid)
        {
            return await IsAdminPfidAsync(pfid) ? "Admin" : 
            await IsReviewerPfidAsync(pfid) ? "Reviewer" :
            await IsTraineePfidAsync(pfid) ? "Trainee" : null;
        }
        public async Task<IEnumerable<PeopleFinderUser?>> GetReviewersAsync()
        {
            IEnumerable<PeopleFinderUser?> reviewers = new List<PeopleFinderUser?>();
            IEnumerable<PeopleFinderUser?> users = await peopleRepository.GetAllAsync();
            foreach (PeopleFinderUser? usr in users)
            {
                 string? role = await GetRoleByPfidAsync(usr?.PFID ?? 0);
                 if (role == "Reviewer" && role != null) ((List<PeopleFinderUser?>)reviewers).Add(usr);
            }
            return reviewers;
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