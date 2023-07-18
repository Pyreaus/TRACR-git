namespace Bristows.TRACR.Model.Models.ViewModels
{
    public partial class TraineeViewModel
    {
        public int TraineeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string? Photo { get; set; }
        public int TraineePfid { get; set; }
        public int ReviewerPfid { get; set; }
        public bool? Active { get; set; }
    }
}