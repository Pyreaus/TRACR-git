namespace Bristows.TRACR.Model.Models.ViewModels
{
    public partial class TraineeViewModel
    {
        
        public int TRAINEE_ID { get; set; }
        public string? TRAINEE_PFID { get; set; } 
        public string? REVIEWER_PFID { get; set; }
        public string? OTHER_PFID { get; set; }
        public string? ACTIVE { get; set; }
        public string? SHOW { get; set; }
    }
}