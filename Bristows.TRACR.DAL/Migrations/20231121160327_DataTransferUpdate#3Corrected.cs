using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DataTransferUpdate3Corrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "TRAINEES",
            columns: new[] { "TRAINEE_ID", "TRAINEE_PFID", "REVIEWER_PFID", "OTHER_PFID", "ACTIVE", "SHOW" },
            values: new object[,]
            {
                { 1, "11", "5", null, "true", "true" },
                { 2, "12", "3", null, "true", "true" },
                { 3, "13", "8", null, "true", "true" },
                { 4, "14", "2", null, "true", "true" },
                { 5, "15", "10", null, "true", "true" },
                { 6, "16", "7", null, "true", "true" },
                { 7, "17", "1", null, "true", "true" },
                { 8, "18", "9", null, "true", "true" },
                { 9, "19", "4", null, "true", "true" },
                { 10, "20", "6", null, "true", "true" },
                { 11, "21", "1", null, "true", "true" },
                { 12, "22", "8", null, "true", "true" },
                { 13, "23", "2", null, "true", "true" },
                { 14, "24", "5", null, "true", "true" },
                { 15, "25", "9", null, "true", "true" },
                { 16, "26", "3", null, "true", "true" },
                { 17, "27", "7", null, "true", "true" },
                { 18, "28", "10", null, "true", "true" },
                { 19, "29", "6", null, "true", "true" },
                { 20, "30", "4", null, "true", "true" }
            }, schema: "dbo");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
