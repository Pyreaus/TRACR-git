using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TRACRDummydata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "LocalId", "ReviewerPfid", "EntryCreated", "LastUpdated", "OtherPfid", "Active", "Show" },
                values: new object[,]
                {
                    { Guid.NewGuid(), 123456789, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 987654321, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 111111111, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 222222222, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 333333333, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 444444444, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 555555555, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 666666666, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 777777777, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 888888888, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 999999999, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 123123123, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 456456456, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 789789789, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 321321321, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { Guid.NewGuid(), 654654654, DateTime.UtcNow, DateTime.UtcNow, null, true, true }
                }
                ,schema: "dbo");

            migrationBuilder.InsertData(
                table: "Diary",
                columns: new[] { "LocalId", "Pfid", "WeekBeginning", "LearningPoints", "ProfessionalDevelopmentUndertaken", "ProfessionalConductIssues", "SignOffSubmitted", "SignedOffBy", "SignedOffTimestamp", "Timestamp", "Show" },
                values: new object[,]
                {
                    { Guid.NewGuid(), 12345, DateTime.UtcNow, "Learning points 1", "Development undertaken 1", "Conduct issues 1", true, "User1", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 67890, DateTime.UtcNow, "Learning points 2", "Development undertaken 2", "Conduct issues 2", false, "User2", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 11111, DateTime.UtcNow, "Learning points 3", "Development undertaken 3", "Conduct issues 3", true, "User3", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 22222, DateTime.UtcNow, "Learning points 4", "Development undertaken 4", "Conduct issues 4", false, "User4", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 33333, DateTime.UtcNow, "Learning points 5", "Development undertaken 5", "Conduct issues 5", true, "User5", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 44444, DateTime.UtcNow, "Learning points 6", "Development undertaken 6", "Conduct issues 6", false, "User6", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 55555, DateTime.UtcNow, "Learning points 7", "Development undertaken 7", "Conduct issues 7", true, "User7", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 66666, DateTime.UtcNow, "Learning points 8", "Development undertaken 8", "Conduct issues 8", false, "User8", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 77777, DateTime.UtcNow, "Learning points 9", "Development undertaken 9", "Conduct issues 9", true, "User9", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 88888, DateTime.UtcNow, "Learning points 10", "Development undertaken 10", "Conduct issues 10", false, "User10", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 99999, DateTime.UtcNow, "Learning points 11", "Development undertaken 11", "Conduct issues 11", true, "User11", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 12312, DateTime.UtcNow, "Learning points 12", "Development undertaken 12", "Conduct issues 12", false, "User12", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 45645, DateTime.UtcNow, "Learning points 13", "Development undertaken 13", "Conduct issues 13", true, "User13", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 78978, DateTime.UtcNow, "Learning points 14", "Development undertaken 14", "Conduct issues 14", false, "User14", DateTime.UtcNow, DateTime.UtcNow, true },
                    { Guid.NewGuid(), 32132, DateTime.UtcNow, "Learning points 15", "Development undertaken 15", "Conduct issues 15", true, "User15", DateTime.UtcNow, DateTime.UtcNow, true }
                }
                ,schema: "dbo");

            migrationBuilder.InsertData(
                table: "DiaryTask",
                columns: new[] { "DiaryId", "LocalId", "Matter", "TaskDescription", "Timestamp", "Show" },
                values: new object[,]
                {
                    { 1, Guid.NewGuid(), "Matter 1", "Task description 1", DateTime.UtcNow, true },
                    { 1, Guid.NewGuid(), "Matter 2", "Task description 2", DateTime.UtcNow, true },
                    { 2, Guid.NewGuid(), "Matter 3", "Task description 3", DateTime.UtcNow, true },
                    { 2, Guid.NewGuid(), "Matter 4", "Task description 4", DateTime.UtcNow, true },
                    { 3, Guid.NewGuid(), "Matter 5", "Task description 5", DateTime.UtcNow, true },
                    { 3, Guid.NewGuid(), "Matter 6", "Task description 6", DateTime.UtcNow, true },
                    { 4, Guid.NewGuid(), "Matter 7", "Task description 7", DateTime.UtcNow, true },
                    { 4, Guid.NewGuid(), "Matter 8", "Task description 8", DateTime.UtcNow, true },
                    { 5, Guid.NewGuid(), "Matter 9", "Task description 9", DateTime.UtcNow, true },
                    { 5, Guid.NewGuid(), "Matter 10", "Task description 10", DateTime.UtcNow, true },
                    { 6, Guid.NewGuid(), "Matter 11", "Task description 11", DateTime.UtcNow, true },
                    { 6, Guid.NewGuid(), "Matter 12", "Task description 12", DateTime.UtcNow, true },
                    { 7, Guid.NewGuid(), "Matter 13", "Task description 13", DateTime.UtcNow, true },
                    { 7, Guid.NewGuid(), "Matter 14", "Task description 14", DateTime.UtcNow, true },
                    { 8, Guid.NewGuid(), "Matter 15", "Task description 15", DateTime.UtcNow, true }
                }
                ,schema: "dbo");

            migrationBuilder.InsertData(
                table: "Trainee",
                columns: new[] { "TraineeId", "LocalId", "TraineePfid", "ReviewerPfid", "EntryCreated", "LastUpdated", "OtherPfid", "Active", "Show" },
                values: new object[,]
                {
                    { 1, Guid.NewGuid(), 12345, 54321, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 2, Guid.NewGuid(), 23456, 65432, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 3, Guid.NewGuid(), 34567, 76543, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 4, Guid.NewGuid(), 45678, 87654, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 5, Guid.NewGuid(), 56789, 98765, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 6, Guid.NewGuid(), 67890, 09876, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 7, Guid.NewGuid(), 78901, 10987, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 8, Guid.NewGuid(), 89012, 21098, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 9, Guid.NewGuid(), 90123, 32109, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 10, Guid.NewGuid(), 01234, 43210, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 11, Guid.NewGuid(), 12345, 54321, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 12, Guid.NewGuid(), 23456, 65432, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 13, Guid.NewGuid(), 34567, 76543, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 14, Guid.NewGuid(), 45678, 87654, DateTime.UtcNow, DateTime.UtcNow, null, true, true },
                    { 15, Guid.NewGuid(), 56789, 98765, DateTime.UtcNow, DateTime.UtcNow, null, true, true }
                }
                ,schema: "dbo");

            migrationBuilder.InsertData(
                table: "Skill",
                columns: new[] { "SkillId", "LocalId", "SkillName", "SkillDescription", "Show", "Colour", "DiaryTaskId" },
                values: new object[,]
                {
                    { 1, Guid.NewGuid(), "Skill 1", "Description for Skill 1", true, null, null },
                    { 2, Guid.NewGuid(), "Skill 2", "Description for Skill 2", true, null, null },
                    { 3, Guid.NewGuid(), "Skill 3", "Description for Skill 3", true, null, null },
                    { 4, Guid.NewGuid(), "Skill 4", "Description for Skill 4", true, null, null },
                    { 5, Guid.NewGuid(), "Skill 5", "Description for Skill 5", true, null, null },
                    { 6, Guid.NewGuid(), "Skill 6", "Description for Skill 6", true, null, null },
                    { 7, Guid.NewGuid(), "Skill 7", "Description for Skill 7", true, null, null },
                    { 8, Guid.NewGuid(), "Skill 8", "Description for Skill 8", true, null, null },
                    { 9, Guid.NewGuid(), "Skill 9", "Description for Skill 9", true, null, null },
                    { 10, Guid.NewGuid(), "Skill 10", "Description for Skill 10", true, null, null },
                    { 11, Guid.NewGuid(), "Skill 11", "Description for Skill 11", true, null, null },
                    { 12, Guid.NewGuid(), "Skill 12", "Description for Skill 12", true, null, null },
                    { 13, Guid.NewGuid(), "Skill 13", "Description for Skill 13", true, null, null },
                    { 14, Guid.NewGuid(), "Skill 14", "Description for Skill 14", true, null, null },
                    { 15, Guid.NewGuid(), "Skill 15", "Description for Skill 15", true, null, null }
                }
                ,schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
