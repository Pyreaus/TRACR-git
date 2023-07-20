using System.ComponentModel.DataAnnotations;

namespace Bristows.TRACR.Model.DTOs
{
    public partial class AddModifyDiaryTaskReq
    {
        [Required]
        public int DIARY_ID { get; set; }
        [MaxLength(250)]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string? MATTER { get; set; }
        public string? FEE_EARNERS { get; set; }
        [MaxLength(300)]
        public string? TASK_DESCRIPTION { get; set; }
        public string? SKILLS { get; set; }
        [MaxLength(50)]
        public string? SHOW { get; set; }
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