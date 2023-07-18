using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDummyEmployeeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "Employee",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(nullable: false),
            //         Name = table.Column<string>(maxLength: 50, nullable: false),
            //         Phone = table.Column<string>(nullable: true),
            //         Email = table.Column<string>(nullable: true),
            //         EntryCreated = table.Column<DateTime>(nullable: true),
            //         LastUpdated = table.Column<DateTime>(nullable: true),
            //         Show = table.Column<bool>(nullable: false)
            //     },
            //     schema: "dbo",
            //     constraints: table => table.PrimaryKey("PK_Employee", x => x.Id));

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Name", "Phone", "Email", "EntryCreated", "LastUpdated", "Show" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "John Doe", "+123456789", "john.doe@example.com", DateTime.Now, null, true },
                    { Guid.NewGuid(), "Jane Smith", "+987654321", "jane.smith@example.com", DateTime.Now, null, true },
                    { Guid.NewGuid(), "Michael Johnson", "+111111111", "michael.johnson@example.com", DateTime.Now, null, true },
                    { Guid.NewGuid(), "Emily Davis", "+222222222", "emily.davis@example.com", DateTime.Now, null, true },
                    { Guid.NewGuid(), "Christopher Wilson", "+333333333", "christopher.wilson@example.com", DateTime.Now, null, true },
                    { Guid.NewGuid(), "Emma Thompson", "+444444444", "emma.thompson@example.com", DateTime.Now, null, true },
                    { Guid.NewGuid(), "James Brown", "+555555555", "james.brown@example.com", DateTime.Now, null, true },
                    { Guid.NewGuid(), "Olivia Taylor", "+666666666", "olivia.taylor@example.com", DateTime.Now, null, true },
                    { Guid.NewGuid(), "Daniel Martinez", "+777777777", "daniel.martinez@example.com", DateTime.Now, null, true },
                    { Guid.NewGuid(), "Sophia Anderson", "+888888888", "sophia.anderson@example.com", DateTime.Now, null, true }
                },
                schema: "dbo");
        }                          /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
             name: "Employee",
             schema: "dbo");
        }
    }
}

