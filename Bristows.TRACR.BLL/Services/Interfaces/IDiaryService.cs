using System.Linq.Expressions;
using Bristows.TRACR.Model.Models.Entities;
using Bristows.TRACR.Model.Models.ValidationAttributes;

namespace Bristows.TRACR.BLL.Services.Interfaces
{
    public interface IDiaryService
    {
        #region [DiaryTask methods] - DiaryTaskRepository
        public Task<IEnumerable<Skill?>> GetSkills();
        public Task<IEnumerable<DiaryTask?>> DiaryTasksByPfidAsync([ValidPfid] int pfid);
        public Task<IEnumerable<DiaryTask?>> DiaryTasksByDiaryIdAsync(int id);
        public IEnumerable<DiaryTask?> DiaryTasksByDiaryId(int id);
        public DiaryTask? DiaryTaskByTaskId(int id);
        public Task<DiaryTask?> DiaryTaskByTaskIdAsync(int id);
        public void DeleteDiaryTask(Expression<Func<DiaryTask, bool>> predicate, bool commit=true);
        public void DeleteDiaryTask(DiaryTask diaryTask, bool commit=true);
        public DiaryTask? UpdateDiaryTask(DiaryTask diaryTask, bool commit=true);
        public DiaryTask? CreateDiaryTask(DiaryTask diaryTask, bool commit=true);
        #endregion
        #region [Diary methods] - DiaryRepository
        public IEnumerable<Diary?> GetDiaries();
        public Task<IEnumerable<Diary?>> GetDiariesAsync([ValidPfid] int pfid);
        public Diary? GetDiaryByDiaryId(int id);
        public Task<Diary?> GetDiaryByDiaryIdAsync(int id);
        public Diary? GetDiaryByPfid([ValidPfid] int pfid);
        public Task<Diary?> GetDiaryByPfidAsync([ValidPfid] int pfid);
        public void DeleteDiary(Expression<Func<Diary, bool>> predicate, bool commit=true);
        public void DeleteDiary(Diary diary, bool commit=true);
        public Diary? UpdateDiary(Diary diary, bool commit=true);
        public Diary? CreateDiary(Diary diary, bool commit=true);

        // public void DeleteDiary(Expression<Func<Employee, bool>> predicate, bool commit=true);
        #endregion
    }
}