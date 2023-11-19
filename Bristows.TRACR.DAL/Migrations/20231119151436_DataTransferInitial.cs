using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DataTransferInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ADMINS",
                schema: "dbo",
                columns: table => new
                {
                    AID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WINUSER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FULL_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ADMIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ACTIVE_USER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ROLE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMINS", x => x.AID);
                });

            migrationBuilder.CreateTable(
                name: "DIARY",
                schema: "dbo",
                columns: table => new
                {
                    DIARY_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PFID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PRACTICE_AREA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WEEK_BEGINNING = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LEARNING_POINTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PROFESSIONAL_DEVELOPMENT_UNDERTAKEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PROFESSIONAL_CONDUCT_ISSUES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SIGN_OFF_SUBMITTED = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SIGNED_OFF_BY = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SIGNED_OFF_TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SHOW = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIARY", x => x.DIARY_ID);
                });

            migrationBuilder.CreateTable(
                name: "DIARY_TASKS",
                schema: "dbo",
                columns: table => new
                {
                    DIARY_TASK_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DIARY_ID = table.Column<int>(type: "int", nullable: false),
                    MATTER = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FEE_EARNERS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TASK_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SKILLS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SHOW = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIARY_TASKS", x => x.DIARY_TASK_ID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EntryCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Show = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PFUser",
                columns: table => new
                {
                    PFID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WinUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecretaryID = table.Column<int>(type: "int", nullable: true),
                    LocationID = table.Column<int>(type: "int", nullable: true),
                    RoomID = table.Column<int>(type: "int", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaternityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaternityDirection = table.Column<bool>(type: "bit", nullable: true),
                    SecondmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondmentCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveUser = table.Column<bool>(type: "bit", nullable: true),
                    Partner = table.Column<bool>(type: "bit", nullable: true),
                    Human = table.Column<bool>(type: "bit", nullable: true),
                    Timestamped = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorID = table.Column<int>(type: "int", nullable: true),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeEarner = table.Column<bool>(type: "bit", nullable: true),
                    Show = table.Column<bool>(type: "bit", nullable: true),
                    FeeEarnerChargeOutRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Upn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SabbaticalBeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SabbaticalEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PronouncedAs = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SKILLS",
                schema: "dbo",
                columns: table => new
                {
                    SKILL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKILL_NAME = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SKILL_DESCRIPTION = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SHOW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COLOUR = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKILLS", x => x.SKILL_ID);
                });

            migrationBuilder.CreateTable(
                name: "TRAINEES",
                schema: "dbo",
                columns: table => new
                {
                    TRAINEE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TRAINEE_PFID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    REVIEWER_PFID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OTHER_PFID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ACTIVE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SHOW = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRAINEES", x => x.TRAINEE_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADMINS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DIARY",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DIARY_TASKS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PFUser");

            migrationBuilder.DropTable(
                name: "SKILLS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TRAINEES",
                schema: "dbo");
        }
    }
}
