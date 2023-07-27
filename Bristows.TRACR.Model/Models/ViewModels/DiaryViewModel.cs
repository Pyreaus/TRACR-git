namespace Bristows.TRACR.Model.Models.ViewModels
{
    public partial class DiaryViewModel
    {
        public string? PFID { get; set; }
        public int? DIARY_ID { get; set; }
        public string? PRACTICE_AREA { get; set; }
        public DateTime? WEEK_BEGINNING { get; set; }
        // public string? PracticeArea { get; set; }
        public string? LEARNING_POINTS { get; set; }
        public string? PROFESSIONAL_DEVELOPMENT_UNDERTAKEN { get; set; }
        public string? PROFESSIONAL_CONDUCT_ISSUES { get; set; }
        public string? SIGN_OFF_SUBMITTED { get; set; }
        public string? SIGNED_OFF_BY { get; set; } = string.Empty;
        public DateTime? SIGNED_OFF_TIMESTAMP { get; set; }
        public string? SHOW { get; set; }
    }
}