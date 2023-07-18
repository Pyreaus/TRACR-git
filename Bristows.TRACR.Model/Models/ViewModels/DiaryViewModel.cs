namespace Bristows.TRACR.Model.Models.ViewModels
{
    public partial class DiaryViewModel
    {
        public int Pfid { get; set; }
        public int DiaryId { get; set; }
        public string? WeekBeginning { get; set; }
        // public string? PracticeArea { get; set; }
        public string? LearningPoints { get; set; }
        public string? ProfessionalDevelopmentUndertaken { get; set; }
        public string? ProfessionalConductIssues { get; set; }
        public bool? SignOffSubmitted { get; set; }
        public string? SignedOffBy { get; set; } = string.Empty;
        public bool? Show { get; set; }
    }
}