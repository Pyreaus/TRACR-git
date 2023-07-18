using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PFUserstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeopleFinderUser",
                schema: "dbo",
                columns: table => new
                {
                    PfId = table.Column<int>(type: "int", maxLength: 5, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 30, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    OtherPfid = table.Column<int>(type: "int", maxLength: 30, nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WinUser = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleFinderUser", x => x.PfId);
                });

            migrationBuilder.InsertData(
                table: "PeopleFinderUser",
                columns: new[] { "LocalId", "FirstName", "LastName", "Email", "Telephone", "OtherPfid", "Photo", "WinUser" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "John", "Doe", "johndoe@example.com", "1234567890", null, "photo1.jpg", "johnd" },
                    { Guid.NewGuid(), "Jane", "Smith", "janesmith@example.com", "0987654321", null, "photo2.jpg", "janes" },
                    { Guid.NewGuid(), "Bob", "Johnson", "bobjohnson@example.com", "9876543210", null, "photo3.jpg", "bobj" },
                    { Guid.NewGuid(), "Alice", "Williams", "alicewilliams@example.com", "0123456789", null, "photo4.jpg", "alicew" },
                    { Guid.NewGuid(), "Mike", "Brown", "mikebrown@example.com", "9876543210", null, "photo5.jpg", "mikeb" },
                    { Guid.NewGuid(), "Emily", "Davis", "emilydavis@example.com", "0123456789", null, "photo6.jpg", "emilyd" },
                    { Guid.NewGuid(), "Tom", "Taylor", "tomtaylor@example.com", "9876543210", null, "photo7.jpg", "tomt" },
                    { Guid.NewGuid(), "Sarah", "Anderson", "sarahanderson@example.com", "0123456789", null, "photo8.jpg", "saraha" },
                    { Guid.NewGuid(), "Chris", "Lee", "chrislee@example.com", "9876543210", null, "photo9.jpg", "chrisl" },
                    { Guid.NewGuid(), "Olivia", "Martinez", "oliviamartinez@example.com", "0123456789", null, "photo10.jpg", "oliviam" },
                    { Guid.NewGuid(), "Daniel", "Garcia", "danielgarcia@example.com", "9876543210", null, "photo11.jpg", "danielg" },
                    { Guid.NewGuid(), "Sophia", "Lopez", "sophialopez@example.com", "0123456789", null, "photo12.jpg", "sophial" },
                    { Guid.NewGuid(), "Alex", "Harris", "alexharris@example.com", "9876543210", null, "photo13.jpg", "alexh" },
                    { Guid.NewGuid(), "Mia", "Clark", "miaclark@example.com", "0123456789", null, "photo14.jpg", "miac" },
                    { Guid.NewGuid(), "David", "Lewis", "davidlewis@example.com", "9876543210", null, "photo15.jpg", "davidl" }
                }, schema: "dbo");
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
                name: "Employee",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PeopleFinderUser",
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
