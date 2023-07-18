using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class TRACRtablesnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                schema: "dbo",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", maxLength: 15, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReviewerPfid = table.Column<int>(type: "int", maxLength: 15, nullable: false),
                    EntryCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OtherPfid = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Show = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Diary",
                schema: "dbo",
                columns: table => new
                {
                    DiaryId = table.Column<int>(type: "int", maxLength: 5, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Pfid = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    WeekBeginning = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LearningPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessionalDevelopmentUndertaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessionalConductIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignOffSubmitted = table.Column<bool>(type: "bit", nullable: true),
                    SignedOffBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignedOffTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Show = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diary", x => x.DiaryId);
                });

            migrationBuilder.CreateTable(
                name: "DiaryTask",
                schema: "dbo",
                columns: table => new
                {
                    DiaryTaskId = table.Column<int>(type: "int", maxLength: 5, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaryId = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    LocalId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 5, nullable: false),
                    Matter = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    TaskDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Show = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryTask", x => x.DiaryTaskId);
                });


            migrationBuilder.CreateTable(
                name: "Trainee",
                schema: "dbo",
                columns: table => new
                {
                    TraineeId = table.Column<int>(type: "int", maxLength: 15, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TraineePfid = table.Column<int>(type: "int", maxLength: 15, nullable: false),
                    ReviewerPfid = table.Column<int>(type: "int", maxLength: 15, nullable: false),
                    EntryCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OtherPfid = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Show = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainee", x => x.TraineeId);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                schema: "dbo",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", maxLength: 5, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SkillName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SkillDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Show = table.Column<bool>(type: "bit", nullable: true),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaryTaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_Skill_DiaryTask_DiaryTaskId",
                        column: x => x.DiaryTaskId,
                        principalSchema: "dbo",
                        principalTable: "DiaryTask",
                        principalColumn: "DiaryTaskId");
                });


            migrationBuilder.CreateIndex(
                name: "IX_Skill_DiaryTaskId",
                schema: "dbo",
                table: "Skill",
                column: "DiaryTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Diary",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Skill",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Trainee",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DiaryTask",
                schema: "dbo");
        }
    }
}
