using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class AddModifyDiaryTaskReq
    {
        [Required]
        public int DiaryId { get; set; }
        public ICollection<SkillDTO>? Skills { get; set; }
        [MaxLength(25)]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string? Matter { get; set; }
        [MaxLength(100)]
        public string? TaskDescription { get; set; }
        public bool? Show { get; set; } = true;
    }
}

//   example request  --->

// {
//   "diaryId": 26,
//   "skills": [
//     {
//       "skillName": "first skill test",
//       "skillDescription": "skillDescription skillDescription skillDescription skillDescription skillDescription",
//       "show": true,
//       "colour": "blue"
//     },
//     {
//       "skillName": "Second skill test",
//       "skillDescription": "skillDescription skillDescription skillDescription skillDescription skillDescription",
//       "show": true,
//       "colour": "green"
//     },
//     {
//       "skillName": "third skill test",
//       "skillDescription": "skillDescription skillDescription skillDescription skillDescription skillDescription",
//       "show": true,
//       "colour": "red"
//     }
//   ],
//   "matter": "Matter title",
//   "taskDescription": "taskDescription taskDescription taskDescription taskDescription taskDescription",
//   "show": true
// }