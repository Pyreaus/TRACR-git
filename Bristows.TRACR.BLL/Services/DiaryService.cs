using Bristows.TRACR.Model.Models.ValidationAttributes;
using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.Model.Models.Entities;
using Bristows.TRACR.DAL.Infrastructure;
using Bristows.TRACR.DAL.Repositories;
using Bristows.TRACR.Model.Contexts;
using System.Linq.Expressions;

namespace Bristows.TRACR.BLL.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly IDiaryRepository diaryRepository;
        public readonly IDiaryTaskRepository diaryTaskRepository;
        private readonly UnitOfWork<TRACRContext> TRACRUnitOfWork;

        public DiaryService(IDiaryRepository diaryRepository, UnitOfWork<TRACRContext> TRACRUnitOfWork, IDiaryTaskRepository diaryTaskRepository)
        {
            this.diaryRepository = diaryRepository ?? throw new ArgumentNullException(nameof(diaryRepository));
            this.diaryTaskRepository = diaryTaskRepository ?? throw new ArgumentNullException(nameof(diaryTaskRepository));
            this.TRACRUnitOfWork = TRACRUnitOfWork ?? throw new ArgumentNullException(nameof(TRACRUnitOfWork));
        }
        #region [Diary methods] - DiaryRepository
        public Diary? GetDiaryByPfid([ValidPfid] int pfid) => diaryRepository.FirstOrDefault(d => d.Pfid == pfid);
        public async Task<Diary?> GetDiaryByPfidAsync([ValidPfid] int pfid) => await diaryRepository.FirstOrDefaultAsync(d => d.Pfid == pfid);
        public async Task<Diary?> GetDiaryByLocalIdAsync(Guid id) => await diaryRepository.FirstOrDefaultAsync(d => d.LocalId == id);
        public IEnumerable<Diary?> GetDiaries() => diaryRepository.GetAll();
        public async Task<IEnumerable<Diary?>> GetDiariesAsync() => await diaryRepository.GetAllAsync();
        public Diary? GetDiaryByDiaryId(int id) => diaryRepository.FirstOrDefault(d => d.DiaryId == id);
        public async Task<Diary?> GetDiaryByDiaryIdAsync(int id) => await diaryRepository.FirstOrDefaultAsync(d => d.DiaryId == id);
        public Diary? UpdateDiary(Diary diary, bool commit=true)
        {
            diaryRepository.Update(diary);
            TRACRUnitOfWork.Commit();
            return diary;
        }
        public void DeleteDiary(Diary diary, bool commit=true)
        {
            diaryRepository.Delete(diary);
            TRACRUnitOfWork.Commit();
        }
        public Diary? CreateDiary(Diary diary, bool commit=true)
        {
            diaryRepository.Add(diary);
            TRACRUnitOfWork.Commit();
            return diary;
        }
        public void DeleteDiary(Expression<Func<Diary, bool>> predicate, bool commit=true)
        {
            diaryRepository.Delete(predicate);
            TRACRUnitOfWork.Commit();
        }
        #endregion
        #region [DiaryTask methods] - DiaryTaskRepository
        public async Task<DiaryTask?> DiaryTaskByLocalIdAsync(Guid id) => await diaryTaskRepository.FirstOrDefaultAsync(d => d.LocalId == id);
        public DiaryTask? DiaryTaskByTaskId(int id) => diaryTaskRepository.FirstOrDefault(d => d.DiaryTaskId == id);
        public async Task<DiaryTask?> DiaryTaskByTaskIdAsync(int id) => await diaryTaskRepository.FirstOrDefaultAsync(d => d.DiaryTaskId == id);
        public IEnumerable<DiaryTask?> DiaryTasksByDiaryId(int id) => diaryTaskRepository.GetMany(d => d.DiaryId == id);
        public async Task<IEnumerable<DiaryTask?>> DiaryTasksByDiaryIdAsync(int id) => await diaryTaskRepository.GetManyAsync(d => d.DiaryId == id);
        public async Task<IEnumerable<DiaryTask?>> DiaryTasksByPfidAsync([ValidPfid] int pfid)
        {
            Diary? UserDiary = await this.GetDiaryByPfidAsync(pfid);
            return await this.DiaryTasksByDiaryIdAsync(UserDiary!.DiaryId);
        }
        public void DeleteDiaryTask(Expression<Func<DiaryTask, bool>> predicate, bool commit=true)
        {
            diaryTaskRepository.Delete(predicate);
            TRACRUnitOfWork.Commit();
        }
        public DiaryTask? UpdateDiaryTask(DiaryTask diaryTask, bool commit=true)
        {
            diaryTaskRepository.Update(diaryTask);
            TRACRUnitOfWork.Commit();
            return diaryTask;
        }
        public void DeleteDiaryTask(DiaryTask diaryTask, bool commit=true)
        {
            diaryTaskRepository.Delete(diaryTask);
            TRACRUnitOfWork.Commit();
        }
        public DiaryTask? CreateDiaryTask(DiaryTask diaryTask, bool commit=true)
        {
            diaryTaskRepository.Add(diaryTask);
            TRACRUnitOfWork.Commit();
            return diaryTask;
        }
        #endregion
    }
}