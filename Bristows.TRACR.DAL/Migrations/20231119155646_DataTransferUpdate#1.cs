using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DataTransferUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PFUser",
                newName: "PFUser",
                newSchema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamped",
                schema: "dbo",
                table: "PFUser",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
            table: "PFUser",
            columns: new[] {"FirstName", "LastName", "Email", "Telephone", "Photo", "WinUser", "PFID" },
            values: new object[,]
            {
                { "John", "Doe", "john.doe@example.com", "1234567890", "photo1.jpg", "john.d", 1 },
                { "Jane", "Smith", "jane.smith@example.com", "0987654321", "photo2.jpg", "jane.s", 2 },
                { "Bob", "Johnson", "bob.johnson@example.com", "9876543210", "photo3.jpg", "bob.j", 3 },
                { "Alice", "Williams", "alice.williams@example.com", "0123456789", "photo4.jpg", "alice.w", 4 },
                { "Mike", "Brown", "mike.brown@example.com", "9876543210", "photo5.jpg", "mike.b", 5 },
                { "Emily", "Davis", "emily.davis@example.com", "0123456789", "photo6.jpg", "emily.d", 6 },
                { "Tom", "Hanks", "tom.hanks@example.com", "9876543210", "photo7.jpg", "tom.h", 7 },
                { "Lily", "Smith", "lily.smith@example.com", "0123456789", "photo8.jpg", "lily.s", 8 },
                { "Chris", "Evans", "chris.evans@example.com", "9876543210", "photo9.jpg", "chris.e", 9 },
                { "Sophie", "Turner", "sophie.turner@example.com", "0123456789", "photo10.jpg", "sophie.t", 10 },
                { "Matthew", "McConaughey", "matthew.mcconaughey@example.com", "9876543210", "photo11.jpg", "matthew.mc", 11 },
                { "Jennifer", "Lawrence", "jennifer.lawrence@example.com", "0123456789", "photo12.jpg", "jennifer.l", 12 },
                { "Daniel", "Radcliffe", "daniel.radcliffe@example.com", "9876543210", "photo13.jpg", "daniel.r", 13 },
                { "Emma", "Watson", "emma.watson@example.com", "0123456789", "photo14.jpg", "emma.w", 14 },
                { "Ryan", "Reynolds", "ryan.reynolds@example.com", "9876543210", "photo15.jpg", "ryan.r", 15 },
                { "Olivia", "Munn", "olivia.munn@example.com", "0123456789", "photo16.jpg", "olivia.m", 16 },
                { "Chris", "Hemsworth", "chris.hemsworth@example.com", "9876543210", "photo17.jpg", "chris.h", 17 },
                { "Jennifer", "Aniston", "jennifer.aniston@example.com", "0123456789", "photo18.jpg", "jennifer.a", 18 },
                { "Tom", "Cruise", "tom.cruise@example.com", "9876543210", "photo19.jpg", "tom.c", 19 },
                { "Margot", "Robbie", "margot.robbie@example.com", "0123456789", "photo20.jpg", "margot.r", 20 },
                { "Robert", "Downey Jr.", "robert.downey@example.com", "9876543210", "photo21.jpg", "robert.d", 21 },
                { "Zendaya", "Coleman", "zendaya.coleman@example.com", "0123456789", "photo22.jpg", "zendaya.c", 22 },
                { "Dwayne", "Johnson", "dwayne.johnson@example.com", "9876543210", "photo23.jpg", "dwayne.j", 23 },
                { "Gal", "Gadot", "gal.gadot@example.com", "0123456789", "photo24.jpg", "gal.g", 24 },
                { "Brad", "Pitt", "brad.pitt@example.com", "9876543210", "photo25.jpg", "brad.p", 25 },
                { "Emma", "Stone", "emma.stone@example.com", "0123456789", "photo26.jpg", "emma.s", 26 },
                { "Leonardo", "DiCaprio", "leonardo.dicaprio@example.com", "9876543210", "photo27.jpg", "leonardo.d", 27 },
                { "Angelina", "Jolie", "angelina.jolie@example.com", "0123456789", "photo28.jpg", "angelina.j", 28 },
                { "Johnny", "Depp", "johnny.depp@example.com", "9876543210", "photo29.jpg", "johnny.d", 29 },
                { "Kate", "Winslet", "kate.winslet@example.com", "0123456789", "photo30.jpg", "kate.w", 30 }
            }, schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PFUser",
                schema: "dbo",
                newName: "PFUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamped",
                table: "PFUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
