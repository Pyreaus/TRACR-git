using Bristows.TRACR.Model.Models.Entities;

namespace Bristows.TRACR.Model.Models.ViewModels
{
    public partial class DiaryTaskViewModel
    {
        public int DiaryTaskId { get; set; }
        public int DiaryId { get; set; }
        public ICollection<Skill>? Skills { get; set; }
        public string? Matter { get; set; }
        public string? TaskDescription { get; set; }
        public bool? Show { get; set; }
    }
}